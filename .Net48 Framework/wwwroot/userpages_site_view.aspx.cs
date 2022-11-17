// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userpages_site_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class userpages_site_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userpages_site_view : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Sign the Guestbook");
                    PagePasswordLabel.InnerText = SepFunctions.LangText("Enter Password Below to View this Page:");
                    YourNameLabel.InnerText = SepFunctions.LangText("Your Name:");
                    WebSiteURLLabel.InnerText = SepFunctions.LangText("Web Site URL:");
                    MessageLabel.InnerText = SepFunctions.LangText("Message:");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dv != null)
                {
                    dv.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GuestbookGridView.PageIndex = e.NewPageIndex;
            GuestbookGridView.DataSource = BindData();
            GuestbookGridView.DataBind();
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            TranslatePage();

            GlobalVars.ModuleID = 7;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var sUserId = SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"));

            if (!string.IsNullOrWhiteSpace(sUserId))
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    if (SepCommon.SepCore.Request.Item("PageName") != "logout.aspx" && SepCommon.SepCore.Request.Item("PageName") != "userpages_approve.aspx")
                        using (var cmd = new SqlCommand("SELECT SiteID,InviteOnly FROM UPagesSites WHERE UserID=@UserID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", sUserId);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (SepFunctions.openBoolean(RS["InviteOnly"]))
                                    {
                                        if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                                        {
                                            if (SepFunctions.Session_User_ID() != sUserId)
                                                if (SepFunctions.Check_Rating(7, SepFunctions.openNull(RS["SiteID"]), SepFunctions.Session_User_ID()) == false)
                                                {
                                                    RequestAccessDiv.Visible = true;
                                                    PasswordDiv.Visible = false;
                                                    Guestbook.Visible = false;
                                                    PageText.Visible = false;
                                                    return;
                                                }
                                        }
                                        else
                                        {
                                            var sInstallFolder = SepFunctions.GetInstallFolder(true);
                                            SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "!" + SepCommon.SepCore.Request.Item("UserName") + "/");
                                            SepFunctions.Redirect(sInstallFolder + "login.aspx");
                                            return;
                                        }
                                    }
                                }

                            }
                        }

                    if (SepCommon.SepCore.Request.Item("PageName") == "userpages_approve.aspx") Server.TransferRequest("userpages_approve.aspx?UserID=" + SepCommon.SepCore.Request.Item("UserID") + "&UserName=" + SepCommon.SepCore.Request.Item("UserName"), true);

                    using (var cmd = new SqlCommand("SELECT PageText,Password FROM UPagesPages WHERE PageName=@PageName AND UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PageName", SepCommon.SepCore.Request.Item("PageName"));
                        cmd.Parameters.AddWithValue("@UserID", sUserId);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                RequestAccessDiv.Visible = false;
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["Password"])))
                                    if (SepFunctions.Session_User_ID() != sUserId)
                                    {
                                        PasswordDiv.Visible = true;
                                        Guestbook.Visible = false;
                                        PageText.Visible = false;
                                        return;
                                    }

                                var cReplace = new Replace();
                                PageText.InnerHtml = cReplace.Replace_Widgets(SepFunctions.openNull(RS["PageText"]), 7, true);
                                cReplace.Dispose();

                                if (SepCommon.SepCore.Request.Item("PageName") == "default.aspx")
                                    using (var cmd2 = new SqlCommand("UPDATE UPagesSites SET Visits=Visits+1 WHERE UserID=@UserID AND Status <> -1", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@PageName", SepCommon.SepCore.Request.Item("PageName"));
                                        cmd2.Parameters.AddWithValue("@UserID", sUserId);
                                        cmd2.ExecuteNonQuery();
                                    }

                                if (SepCommon.SepCore.Request.Item("PageName") == "guestbook.aspx")
                                {
                                    EntryID.Value = Strings.ToString(SepFunctions.GetIdentity());
                                    dv = BindData();
                                    GuestbookGridView.DataSource = dv;
                                    GuestbookGridView.DataBind();
                                    Guestbook.Visible = true;
                                    YourName.Value = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName");
                                }
                            }
                            else
                            {
                                var sInstallFolder = SepFunctions.GetInstallFolder(true);
                                var sPageName = SepCommon.SepCore.Request.Item("PageName");

                                var rewritePage = Globals.RedirectURL("/" + sPageName);

                                if (!string.IsNullOrWhiteSpace(rewritePage))
                                    sPageName = rewritePage;
                                else
                                    sPageName = sInstallFolder + sPageName;
                                var sQuery = string.Empty;
                                foreach (var Item in SepCommon.SepCore.Request.QueryString())
                                {
                                    try
                                    {
                                        if (Strings.LCase(Strings.ToString(Item)) != "username" && Strings.LCase(Strings.ToString(Item)) != "pagename") sQuery += "&" + Item + "=" + SepFunctions.HTMLEncode(SepCommon.SepCore.Request.QueryString(Strings.ToString(Item)));
                                    }
                                    catch
                                    {
                                    }
                                }

                                sPageName += Strings.ToString(Strings.InStr(sPageName, "?") > 0 ? "&" : "?") + "UserPage=Yes&UserName=" + SepCommon.SepCore.Request.Item("UserName") + sQuery;
                                Server.TransferRequest(sPageName, true);
                            }

                        }
                    }
                }
            else PageText.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~User page~~ does not exist.") + "</div>";
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }

        /// <summary>
        /// Handles the Click event of the PasswordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PasswordButton_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT PageText FROM UPagesPages WHERE PageName=@PageName AND UserID=@UserID AND Password=@Password", conn))
                {
                    cmd.Parameters.AddWithValue("@PageName", SepCommon.SepCore.Request.Item("PageName"));
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")));
                    cmd.Parameters.AddWithValue("@Password", PagePassword.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            PasswordDiv.Visible = false;
                            RequestAccessDiv.Visible = false;
                            PageText.Visible = true;

                            var cReplace = new Replace();
                            PageText.InnerHtml = cReplace.Replace_Widgets(SepFunctions.openNull(RS["PageText"]), 7, true);
                            cReplace.Dispose();
                        }
                        else
                        {
                            idPasswordErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an Invalid Password.") + "</div>";
                            PasswordDiv.Visible = true;
                            RequestAccessDiv.Visible = false;
                            Guestbook.Visible = false;
                            PageText.Visible = false;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the RequestButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RequestButton_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Members.EmailAddress FROM UPagesSites,Members WHERE UPagesSites.UserID=@UserID AND UPagesSites.UserID=Members.UserID AND UPagesSites.Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var SiteDomain = SepFunctions.GetMasterDomain(false);
                            var sEmailBody = "Dear " + SepCommon.SepCore.Request.Item("UserName") + ",<br/><br/>" + SepFunctions.GetUserInformation("UserName") + " has requested access to your web site at <a href=\"" + SiteDomain + "\">" + SiteDomain + "</a> Click the link below to approve this user.<br/><br/>";
                            sEmailBody += "<a href=\"" + SiteDomain + "userpages_approve.aspx?UserID=" + SepFunctions.Session_User_ID() + "&UserNam=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("UserName")) + "\">" + SiteDomain + "userpages_approve.aspx?UserID=" + SepFunctions.Session_User_ID() + "&UserNam=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("UserName")) + "</a>";
                            SepFunctions.Send_Email(SepFunctions.openNull(RS["EmailAddress"]), SepFunctions.Setup(991, "AdminEmailAddress"), SepFunctions.LangText("User has requested access to your web page"), sEmailBody, GlobalVars.ModuleID);
                            idAccessErrorMsg.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully requested access to view this site.") + "</div>";
                        }
                        else
                        {
                            idAccessErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid User Page.") + "</div>";
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Recaptcha1.Validate() == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid reCaptcha.") + "</div>";
                return;
            }

            var sReturn = SepCommon.DAL.UserPages.Guestbook_Save(SepFunctions.toLong(EntryID.Value), SepFunctions.Session_User_ID(), YourName.Value, WebSiteURL.Value, Message.Value);
            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

            WebSiteURL.Value = string.Empty;
            Message.Value = string.Empty;

            dv = BindData();
            GuestbookGridView.DataSource = dv;
            GuestbookGridView.DataBind();
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var UserPagesGuestbook = SepCommon.DAL.UserPages.GetUserPagesGuestbook(SepFunctions.Session_User_ID());

            dv = new DataView(SepFunctions.ListToDataTable(UserPagesGuestbook));
            return dv;
        }
    }
}