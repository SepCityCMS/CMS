// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="downloads.aspx.cs" company="SepCity, Inc.">
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
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class downloads1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class downloads1 : Page
    {
        /// <summary>
        /// The s library download
        /// </summary>
        public static string sLibraryDownload = string.Empty;

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
        /// Formats the currency.
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns>System.Object.</returns>
        public object Format_Currency(string price)
        {
            return SepFunctions.Format_Currency(price);
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
        /// Gets the name of the grid.
        /// </summary>
        /// <returns>GridView.</returns>
        public GridView GetGridName()
        {
            var jCategories = SepCommon.DAL.Categories.Category_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")));

            GridView GridName;
            switch (jCategories.CatType)
            {
                case "Audio":
                    AudioContent.Visible = true;
                    GridName = AudioContent;
                    break;

                case "Document":
                    DocumentContent.Visible = true;
                    GridName = DocumentContent;
                    break;

                case "Image":
                    ImageContent.Visible = true;
                    GridName = ImageContent;
                    break;

                case "Software":
                    SoftwareContent.Visible = true;
                    GridName = SoftwareContent;
                    break;

                case "Video":
                    VideoContent.Visible = true;
                    GridName = VideoContent;
                    break;

                default:
                    SoftwareContent.Visible = true;
                    GridName = SoftwareContent;
                    break;
            }

            return GridName;
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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
                    LatestContent.Columns[0].HeaderText = SepFunctions.LangText("Artist Name");
                    LatestContent.Columns[1].HeaderText = SepFunctions.LangText("Name / Title");
                    LatestContent.Columns[2].HeaderText = SepFunctions.LangText("Date Posted");
                    PopularContent.Columns[0].HeaderText = SepFunctions.LangText("Artist Name");
                    PopularContent.Columns[1].HeaderText = SepFunctions.LangText("Name / Title");
                    PopularContent.Columns[2].HeaderText = SepFunctions.LangText("Downloads");
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
        /// Handles the PageIndexChanging event of the ManageGridViewAudio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridViewAudio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetGridName().PageIndex = e.NewPageIndex;
            GetGridName().DataSource = BindData();
            GetGridName().DataBind();
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ManageGridViewDocument control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridViewDocument_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetGridName().PageIndex = e.NewPageIndex;
            GetGridName().DataSource = BindData();
            GetGridName().DataBind();
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ManageGridViewImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridViewImage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetGridName().PageIndex = e.NewPageIndex;
            GetGridName().DataSource = BindData();
            GetGridName().DataBind();
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ManageGridViewSoft control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridViewSoft_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetGridName().PageIndex = e.NewPageIndex;
            GetGridName().DataSource = BindData();
            GetGridName().DataBind();
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ManageGridViewVideo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridViewVideo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetGridName().PageIndex = e.NewPageIndex;
            GetGridName().DataSource = BindData();
            GetGridName().DataBind();
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

            GlobalVars.ModuleID = 10;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "LibraryEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("LibraryAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("CatID")))
            {
                if (!Page.IsPostBack)
                {
                    dv = BindData();
                    GetGridName().DataSource = dv;
                    GetGridName().DataBind();
                }
            }
            else
            {
                var sUserID = string.Empty;
                if (SepFunctions.isUserPage() && SepFunctions.Setup(7, "UPagesTop10") == "Yes")
                    sUserID = SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"));
                if (SepFunctions.Setup(GlobalVars.ModuleID, "LibraryDisplayNewest") == "Yes")
                {
                    LatestContent.Visible = true;
                    LatestContentColumn.Visible = true;
                    LatestContent.Columns[0].Visible = false;
                    var cDownloads = SepCommon.DAL.Downloads.GetDownloads(UserID: sUserID);
                    LatestContent.DataSource = cDownloads.Take(10);
                    LatestContent.DataBind();
                }

                if (SepFunctions.Setup(GlobalVars.ModuleID, "LibraryDisplayNewest") == "Audio")
                {
                    LatestContent.Visible = true;
                    LatestContentColumn.Visible = true;
                    LatestContent.Columns[0].HeaderText = SepFunctions.LangText("Artist Name");
                    LatestContent.Columns[1].HeaderText = SepFunctions.LangText("Song Title");
                    var cDownloads = SepCommon.DAL.Downloads.GetDownloads(CatType: "Audio", UserID: sUserID);
                    LatestContent.DataSource = cDownloads.Take(10);
                    LatestContent.DataBind();
                }

                if (SepFunctions.Setup(GlobalVars.ModuleID, "LibraryDisplayPopular") == "Yes")
                {
                    PopularContent.Visible = true;
                    PopularContentColumn.Visible = true;
                    PopularContent.Columns[0].Visible = false;
                    var cDownloads = SepCommon.DAL.Downloads.GetDownloads("Downloads", UserID: sUserID);
                    PopularContent.DataSource = cDownloads.Take(10);
                    PopularContent.DataBind();
                }

                if (SepFunctions.Setup(GlobalVars.ModuleID, "LibraryDisplayPopular") == "Audio")
                {
                    PopularContent.Visible = true;
                    PopularContentColumn.Visible = true;
                    PopularContent.Columns[0].HeaderText = SepFunctions.LangText("Artist Name");
                    PopularContent.Columns[1].HeaderText = SepFunctions.LangText("Song Title");
                    var cDownloads = SepCommon.DAL.Downloads.GetDownloads("Downloads", CatType: "Audio", UserID: sUserID);
                    PopularContent.DataSource = cDownloads.Take(10);
                    PopularContent.DataBind();
                }

                if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                {
                    LatestContent.Caption = "<table border=\"0\" width=\"99%\" cellpadding=\"2\" cellspacing=\"0\"><tr><td width=\"100%\">" + SepFunctions.LangText("Latest Downloads") + "</td><td><a href=\"" + sInstallFolder + "rss.aspx?DoAction=Downloads\" target=\"_blank\"><img src=\"" + SepFunctions.GetInstallFolder(true) + "images/public/rss.png\" border=\"0\" /></a></td></tr></table>";
                    PopularContent.Caption = "<table border=\"0\" width=\"99%\" cellpadding=\"2\" cellspacing=\"0\"><tr><td width=\"100%\">" + SepFunctions.LangText("Popular Downloads") + "</td><td><a href=\"" + sInstallFolder + "rss.aspx?DoAction=Downloads\" target=\"_blank\"><img src=\"" + SepFunctions.GetInstallFolder(true) + "images/public/rss.png\" border=\"0\" /></a></td></tr></table>";
                }
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
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var cDownloads = SepCommon.DAL.Downloads.GetDownloads(catId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")));
            dv = new DataView(SepFunctions.ListToDataTable(cDownloads));
            return dv;
        }
    }
}