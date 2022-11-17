// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="refer.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class refer.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class refer : Page
    {
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Refer a Friend");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Your Email Address:");
                    FriendsEmail1Label.InnerText = SepFunctions.LangText("Friend's Email Address 1:");
                    FriendsEmail2Label.InnerText = SepFunctions.LangText("Friend's Email Address 2:");
                    FriendsEmail3Label.InnerText = SepFunctions.LangText("Friend's Email Address 3:");
                    FriendsEmail4Label.InnerText = SepFunctions.LangText("Friend's Email Address 4:");
                    FriendsEmail5Label.InnerText = SepFunctions.LangText("Friend's Email Address 5:");
                    EmailSubjectLabel.InnerText = SepFunctions.LangText("Email Subject:");
                    EmailBodyLabel.InnerText = SepFunctions.LangText("Email Body:");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Your Email Address~~ is required.");
                    FriendsEmail1Required.ErrorMessage = SepFunctions.LangText("~~Friend's Email Address 1~~ is required.");
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
            }
        }

        /// <summary>
        /// The c common
        /// </summary>
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 43;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ReferEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ReferAccess"));

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0)
            {
                GlobalVars.ModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                ModuleID.Value = Strings.ToString(GlobalVars.ModuleID);
                var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
                Array.Resize(ref arrHeader, 3);
                Page.Title = arrHeader[0];
            }
            else
            {
                var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
                Array.Resize(ref arrHeader, 3);
                Page.Title = arrHeader[0];
                Page.MetaDescription = arrHeader[1];
                Page.MetaKeywords = arrHeader[2];
            }

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                EmailAddress.Value = SepFunctions.GetUserInformation("EmailAddress");
                EmailSubject.Value = SepFunctions.Setup(GlobalVars.ModuleID, "ReferEmailSubject");
                EmailBody.Value = SepFunctions.Setup(GlobalVars.ModuleID, "ReferEmailBody");
                if (SepCommon.SepCore.Request.Item("URL") == "/refer.aspx") ReferURL.Value = "default.aspx";
                else ReferURL.Value = SepCommon.SepCore.Request.Item("URL");
            }
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
        /// Handles the Click event of the SendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SendButton_Click(object sender, EventArgs e)
        {
            if (Recaptcha1.Validate() == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid reCaptcha.") + "</div>";
                return;
            }

            var sURL = SepFunctions.GetMasterDomain(true);
            sURL += Strings.Left(ReferURL.Value, 1) == "/" ? Strings.Right(ReferURL.Value, Strings.Len(ReferURL.Value) - 1) : ReferURL.Value;

            if (Strings.InStr(sURL, "?") > 0)
                sURL += "&";
            else
                sURL += "?";

            sURL += "ReferralTo=" + SepFunctions.UrlEncode(FriendsEmail1.Value) + "&ReferralFrom=" + SepFunctions.UrlEncode(EmailAddress.Value);

            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (Strings.InStr(FriendsEmail1.Value, "@") > 0 && Strings.InStr(FriendsEmail1.Value, ".") > 0)
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostReferUser", "GetReferUser", FriendsEmail1.Value, false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    using (var cmd = new SqlCommand("SELECT Visited FROM ReferralAddresses WHERE ToEmailAddress=@ToEmailAddress AND FromEmailAddress=@FromEmailAddress", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail1.Value);
                        cmd.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                                using (var cmd2 = new SqlCommand("INSERT INTO ReferralAddresses (ToEmailAddress, FromEmailAddress, Visited) VALUES(@ToEmailAddress, @FromEmailAddress, '0')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail1.Value);
                                    cmd2.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }

                    SepFunctions.Send_Email(FriendsEmail1.Value, EmailAddress.Value, EmailSubject.Value, EmailBody.Value + "<br/><br/><a href=\"" + sURL + "\" target=\"_blank\">" + sURL + "</a>", GlobalVars.ModuleID);
                }

                if (Strings.InStr(FriendsEmail2.Value, "@") > 0 && Strings.InStr(FriendsEmail2.Value, ".") > 0)
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostReferUser", "GetReferUser", FriendsEmail2.Value, false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    using (var cmd = new SqlCommand("SELECT Visited FROM ReferralAddresses WHERE ToEmailAddress=@ToEmailAddress AND FromEmailAddress=@FromEmailAddress", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail2.Value);
                        cmd.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                                using (var cmd2 = new SqlCommand("INSERT INTO ReferralAddresses (ToEmailAddress, FromEmailAddress, Visited) VALUES(@ToEmailAddress, @FromEmailAddress, '0')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail2.Value);
                                    cmd2.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }

                    SepFunctions.Send_Email(FriendsEmail2.Value, EmailAddress.Value, EmailSubject.Value, EmailBody.Value + "<br/><br/><a href=\"" + sURL + "\" target=\"_blank\">" + sURL + "</a>", GlobalVars.ModuleID);
                }

                if (Strings.InStr(FriendsEmail3.Value, "@") > 0 && Strings.InStr(FriendsEmail3.Value, ".") > 0)
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostReferUser", "GetReferUser", FriendsEmail3.Value, false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    using (var cmd = new SqlCommand("SELECT Visited FROM ReferralAddresses WHERE ToEmailAddress=@ToEmailAddress AND FromEmailAddress=@FromEmailAddress", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail3.Value);
                        cmd.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                                using (var cmd2 = new SqlCommand("INSERT INTO ReferralAddresses (ToEmailAddress, FromEmailAddress, Visited) VALUES(@ToEmailAddress, @FromEmailAddress, '0')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail3.Value);
                                    cmd2.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }

                    SepFunctions.Send_Email(FriendsEmail3.Value, EmailAddress.Value, EmailSubject.Value, EmailBody.Value + "<br/><br/><a href=\"" + sURL + "\" target=\"_blank\">" + sURL + "</a>", GlobalVars.ModuleID);
                }

                if (Strings.InStr(FriendsEmail4.Value, "@") > 0 && Strings.InStr(FriendsEmail4.Value, ".") > 0)
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostReferUser", "GetReferUser", FriendsEmail4.Value, false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    using (var cmd = new SqlCommand("SELECT Visited FROM ReferralAddresses WHERE ToEmailAddress=@ToEmailAddress AND FromEmailAddress=@FromEmailAddress", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail4.Value);
                        cmd.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                                using (var cmd2 = new SqlCommand("INSERT INTO ReferralAddresses (ToEmailAddress, FromEmailAddress, Visited) VALUES(@ToEmailAddress, @FromEmailAddress, '0')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail4.Value);
                                    cmd2.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }

                    SepFunctions.Send_Email(FriendsEmail4.Value, EmailAddress.Value, EmailSubject.Value, EmailBody.Value + "<br/><br/><a href=\"" + sURL + "\" target=\"_blank\">" + sURL + "</a>", GlobalVars.ModuleID);
                }

                if (Strings.InStr(FriendsEmail5.Value, "@") > 0 && Strings.InStr(FriendsEmail5.Value, ".") > 0)
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostReferUser", "GetReferUser", FriendsEmail5.Value, false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    using (var cmd = new SqlCommand("SELECT Visited FROM ReferralAddresses WHERE ToEmailAddress=@ToEmailAddress AND FromEmailAddress=@FromEmailAddress", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail5.Value);
                        cmd.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                                using (var cmd2 = new SqlCommand("INSERT INTO ReferralAddresses (ToEmailAddress, FromEmailAddress, Visited) VALUES(@ToEmailAddress, @FromEmailAddress, '0')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ToEmailAddress", FriendsEmail5.Value);
                                    cmd2.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }

                    SepFunctions.Send_Email(FriendsEmail5.Value, EmailAddress.Value, EmailSubject.Value, EmailBody.Value + "<br/><br/><a href=\"" + sURL + "\" target=\"_blank\">" + sURL + "</a>", GlobalVars.ModuleID);
                }

                using (var cmd = new SqlCommand("SELECT ID FROM ReferralStats WHERE FromEmailAddress=@FromEmailAddress", conn))
                {
                    cmd.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                            using (var cmd2 = new SqlCommand("INSERT INTO ReferralStats (FromEmailAddress, Visitors) VALUES(@FromEmailAddress, '0')", conn))
                            {
                                cmd2.Parameters.AddWithValue("@FromEmailAddress", EmailAddress.Value);
                                cmd2.ExecuteNonQuery();
                            }

                    }
                }
            }

            ReferForm.Visible = false;
            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully sent an email to your friend.") + "</div>";
        }
    }
}