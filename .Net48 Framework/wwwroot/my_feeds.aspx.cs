// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="my_feeds.aspx.cs" company="SepCity, Inc.">
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
    using System.Linq;
    using System.Web.UI;

    /// <summary>
    /// Class my_feeds.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class my_feeds : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder(true);
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
                    ManageGridView.Columns[0].HeaderText = SepFunctions.LangText("Photo");
                    WhatsNewRequired.ErrorMessage = SepFunctions.LangText("~~What's new~~ is required.");
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
            TranslatePage();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 62;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "FeedsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "my_feeds.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            SepFunctions.RequireLogin(SepFunctions.Security("FeedsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            FirstName.InnerHtml = SepFunctions.GetUserInformation("FirstName");

            if (!Page.IsPostBack) BindData();

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("There are currently no feeds to show at this time.") + "</div>";
                ManageGridView.Visible = false;
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
        /// Handles the Click event of the PostButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PostButton_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("INSERT INTO Feeds (FeedID, UniqueID, ModuleID, Title, Description, UserID, MoreLink, DatePosted) VALUES (@FeedID, @UniqueID, @ModuleID, @Title, @Description, @UserID, @MoreLink, @DatePosted)", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@UniqueID", 0);
                    cmd.Parameters.AddWithValue("@ModuleID", 62);
                    cmd.Parameters.AddWithValue("@Title", SepFunctions.Session_User_Name() + " has made a new post.");
                    cmd.Parameters.AddWithValue("@Description", WhatsNew.Value);
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                    cmd.Parameters.AddWithValue("@MoreLink", string.Empty);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            WhatsNew.Value = string.Empty;
            ErrorMessage.InnerHtml = "<script type=\"text/javascript\">reloadFeeds();</script>";
            ErrorMessage.InnerHtml += "<div Class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your post has been successfully added.") + "</div>";
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var dFeeds = SepCommon.DAL.MyFeeds.GetMyFeeds(false, false);

            if (SepCommon.SepCore.Request.Item("DoAction") == "Favorites")
            {
                dFeeds = SepCommon.DAL.MyFeeds.GetMyFeeds(true, true);
            }

            ManageGridView.DataSource = dFeeds.Take(50);
            ManageGridView.DataBind();
        }
    }
}