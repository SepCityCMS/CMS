// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userpages_pages.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class userpages_pages.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userpages_pages : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
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
                    FilterDoAction.Items[0].Text = SepFunctions.LangText("Select an Action");
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Pages");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("Page Title");
                    RunAction.InnerText = SepFunctions.LangText("GO");
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
        /// Handles the PageIndexChanging event of the ManageContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ManageGridView.PageIndex = e.NewPageIndex;
            ManageGridView.DataSource = BindData();
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the RowCommand event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cmdArguments = Strings.Split(Strings.ToString(e.CommandArgument), "||");

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (e.CommandName == "MoveUp" || e.CommandName == "MoveDown")
                {
                    Array.Resize(ref cmdArguments, 2);

                    var sID = cmdArguments[0];
                    var sNewWeight = SepFunctions.toLong(cmdArguments[1]);

                    long UpdateWeight = 0;

                    string[] arrCommands = null;
                    var arrCount = 0;

                    if (e.CommandName == "MoveDown")
                        sNewWeight += 1;
                    else
                        sNewWeight -= 1;

                    using (var cmd = new SqlCommand("SELECT PageID,UserID FROM UPagesPages WHERE PageID <> @PageID AND UserID=@UserID ORDER BY Weight, PageTitle", conn))
                    {
                        cmd.Parameters.AddWithValue("@PageID", sID);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                UpdateWeight += 1;
                                if (UpdateWeight == sNewWeight)
                                    UpdateWeight += 1;
                                Array.Resize(ref arrCommands, arrCount + 1);
                                arrCommands[arrCount] = "UPDATE UPagesPages SET Weight='" + SepFunctions.FixWord(Strings.ToString(UpdateWeight)) + "' WHERE PageID='" + SepFunctions.openNull(RS["PageID"], true) + "' AND UserID='" + SepFunctions.openNull(RS["UserID"], true) + "'";
                                arrCount += 1;
                            }

                        }
                    }

                    Array.Resize(ref arrCommands, arrCount + 1);
                    if (arrCommands != null)
                    {
                        arrCommands[arrCount] = "UPDATE UPagesPages SET Weight='" + SepFunctions.FixWord(Strings.ToString(sNewWeight)) + "' WHERE PageID='" + SepFunctions.FixWord(sID) + "' AND UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "'";
                        arrCount += 1;

                        for (var i = 0; i <= Information.UBound(arrCommands); i++)
                            using (var cmd = new SqlCommand(arrCommands[i], conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                    }
                }
            }

            SepFunctions.Cache_Remove();
            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 7;

            if ((SepFunctions.Setup(GlobalVars.ModuleID, "UPagesEnable") != "Enable" || SepFunctions.isUserPage()) && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("UPagesCreate"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
            {
                if (SepCommon.SepCore.Request.Item("DoAction") == "SiteAdded") ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully created your web site.") + " " + SepFunctions.LangText("You can add and edit your web pages below.") + "</div>";
                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
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
        /// Handles the Click event of the RunAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RunAction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterDoAction.Value))
            {
                var sReturn = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
                else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

                return;
            }

            var sIDs = UniqueIDs.Value;

            if (Strings.Len(sIDs) > 0)
            {
                try
                {
                    var sReturn = SepCommon.DAL.UserPages.Page_Delete(SepFunctions.Session_User_ID(), sIDs);
                    if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
                    else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
                }
                catch
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var cPages = SepCommon.DAL.UserPages.GetUserPagesPages(SepFunctions.Session_User_ID());

            dv = new DataView(SepFunctions.ListToDataTable(cPages));
            return dv;
        }
    }
}