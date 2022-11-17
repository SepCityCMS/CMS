// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="links.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class links.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class links : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The current page
        /// </summary>
        private int CurrentPage;

        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var cLinks = SepCommon.DAL.LinkDirectory.GetLinksWebsite(CategoryId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")));

            if (cLinks.Count > 0)
            {
                ListContent.Visible = true;
                NewestContent.Visible = false;

                ListContent.DataSource = cLinks.ToArray();
                ListContent.DataBind();

                if (cLinks.Count <= SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage"))) PagerTemplate.Visible = false;
            }
            else
            {
                PagerTemplate.Visible = false;
                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) == 0)
                {
                    ListContent.Visible = false;
                    NewestContent.Visible = true;

                    if (SepFunctions.Setup(GlobalVars.ModuleID, "LinksDisplayNewest") == "Yes")
                    {
                        var cLinksN = SepCommon.DAL.LinkDirectory.GetLinksWebsite("Mod.DatePosted", "DESC", UserID: SepFunctions.isUserPage() && SepFunctions.Setup(7, "UPagesTop10") == "Yes" ? SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")) : string.Empty);

                        NewestContent.DataSource = cLinksN.Take(10);
                        NewestContent.DataBind();

                        if (SepFunctions.Setup(993, "RSSTop") == "Yes") NewestContent.Caption = "<table border=\"0\" width=\"99%\" cellpadding=\"2\" cellspacing=\"0\"><tr><td width=\"100%\">" + SepFunctions.LangText("Latest Web Site Postings") + "</td><td><a href=\"" + sInstallFolder + "rss.aspx?DoAction=Links\" target=\"_blank\"><img src=\"" + SepFunctions.GetInstallFolder(true) + "images/public/rss.png\" border=\"0\" /></a></td></tr></table>";
                    }
                    else
                    {
                        NewestContent.Visible = false;
                    }
                }
            }
        }

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
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang))) NewestContent.Columns[0].HeaderText = SepFunctions.LangText("Site Name");
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = sender as HtmlSelect;
            CurrentPage = int.Parse(ddl.Value);
            var PageSize = PagerTemplate.PageSize;
            PagerTemplate.SetPageProperties(CurrentPage * PageSize, PageSize, true);
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
        /// Handles the PagePropertiesChanging event of the ListContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PagePropertiesChangingEventArgs" /> instance containing the event data.</param>
        protected void ListContent_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                CurrentPage = e.StartRowIndex / e.MaximumRows + 1;
            }
            catch
            {
                CurrentPage = 1;
            }
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

            GlobalVars.ModuleID = 19;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "LinksEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("LinksAccess"));

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
                BindData();
                PagerTemplate.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));
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
        /// Handles the PreRender event of the PagerTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PagerTemplate_PreRender(object sender, EventArgs e)
        {
            BindData();
        }
    }
}