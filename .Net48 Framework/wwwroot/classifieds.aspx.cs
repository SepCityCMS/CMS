// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="classifieds.aspx.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class classifieds.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class classifieds : Page
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
        /// Formats the ISAPI.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public string Format_ISAPI(object sText)
        {
            return SepFunctions.Format_ISAPI(Strings.ToString(sText));
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
                    ListContent.Columns[0].HeaderText = SepFunctions.LangText("Thumbnail");
                    ListContent.Columns[1].HeaderText = SepFunctions.LangText("Title");
                    ListContent.Columns[2].HeaderText = SepFunctions.LangText("Price");
                    NewestContent.Columns[0].HeaderText = SepFunctions.LangText("Thumbnail");
                    NewestContent.Columns[1].HeaderText = SepFunctions.LangText("Title");
                    NewestContent.Columns[2].HeaderText = SepFunctions.LangText("Price");
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
        /// Handles the PageIndexChanging event of the ListContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ListContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListContent.PageIndex = e.NewPageIndex;
            ListContent.DataSource = BindData();
            ListContent.DataBind();
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

            GlobalVars.ModuleID = 44;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                var jClassifieds = SepCommon.DAL.Classifieds.Classified_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")));

                if (jClassifieds.AdID > 0) SepFunctions.Redirect(sInstallFolder + "classified/" + SepCommon.SepCore.Request.Item("AdID") + "/" + Format_ISAPI(jClassifieds.Title));
            }

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ClassifiedAccess"));

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

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ListContent.DataSource = dv;
                ListContent.DataBind();
                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) == 0 && ListContent.VirtualItemCount == 0 && SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedDisplayNewest") == "Yes")
                {
                    dv = BindData();
                    NewestContent.DataSource = dv;
                    NewestContent.DataBind();
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var cClassifieds = SepCommon.DAL.Classifieds.GetClassifiedAds(CategoryId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")), availableItems: true);

            if (cClassifieds.Count > 0)
            {
                ListContent.Visible = true;
                NewestContent.Visible = false;

                dv = new DataView(SepFunctions.ListToDataTable(cClassifieds));
                return dv;
            }

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) == 0)
            {
                ListContent.Visible = false;
                NewestContent.Visible = true;

                if (SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedDisplayNewest") == "Yes")
                {
                    var cClassifiedsN = SepCommon.DAL.Classifieds.GetClassifiedAds(availableItems: true, userId: SepFunctions.isUserPage() && SepFunctions.Setup(7, "UPagesTop10") == "Yes" ? SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")) : string.Empty);

                    if (SepFunctions.Setup(993, "RSSTop") == "Yes") NewestContent.Caption = "<table border=\"0\" width=\"99%\" cellpadding=\"2\" cellspacing=\"0\"><tr><td width=\"100%\">" + SepFunctions.LangText("Latest Classified Ad Postings") + "</td><td><a href=\"" + sInstallFolder + "rss.aspx?DoAction=Classified\" target=\"_blank\"><img src=\"" + SepFunctions.GetInstallFolder(true) + "images/public/rss.png\" border=\"0\" /></a></td></tr></table>";

                    dv = new DataView(SepFunctions.ListToDataTable(cClassifiedsN));
                    return dv;
                }

                NewestContent.Visible = false;
            }

            return null;
        }
    }
}