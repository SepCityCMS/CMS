// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="businesses.aspx.cs" company="SepCity, Inc.">
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
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class businesses.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class businesses : Page
    {
        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var cBusinesses = SepCommon.DAL.Businesses.GetBusinesses(CategoryId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")));

            if (cBusinesses.Count > 0)
            {
                ListContent.Visible = true;
                NewestContent.Visible = false;
                NewestListings.Visible = false;

                ListContent.DataSource = cBusinesses.ToArray();
                ListContent.DataBind();

                if (cBusinesses.Count <= SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage"))) PagerTemplate.Visible = false;
            }
            else
            {
                PagerTemplate.Visible = false;
                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) == 0)
                {
                    ListContent.Visible = false;
                    NewestContent.Visible = true;
                    NewestListings.Visible = true;

                    if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessDisplayNewest") == "Yes")
                    {
                        var cBusinessesN = SepCommon.DAL.Businesses.GetBusinesses(userId: SepFunctions.isUserPage() && SepFunctions.Setup(7, "UPagesTop10") == "Yes" ? SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")) : string.Empty);

                        NewestContent.DataSource = cBusinessesN.Take(10);
                        NewestContent.DataBind();

                        if (SepFunctions.Setup(993, "RSSTop") == "Yes") NewestListings.InnerHtml = "<table border=\"0\" width=\"99%\" cellpadding=\"2\" cellspacing=\"0\"><tr><td width=\"100%\"><h5>" + SepFunctions.LangText("Latest Business Listings") + "</h5></td><td><a href=\"" + sInstallFolder + "rss.aspx?DoAction=Business\" target=\"_blank\"><img src=\"" + SepFunctions.GetInstallFolder(true) + "images/public/rss.png\" border=\"0\" /></a></td></tr></table>";
                    }
                    else
                    {
                        NewestContent.Visible = false;
                        NewestListings.Visible = false;
                    }
                }
                else
                {
                    NewestContent.Visible = false;
                    NewestListings.Visible = false;
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
        /// <param name="excludePortal">if set to <c>true</c> [exclude portal].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortal = false)
        {
            return SepFunctions.GetInstallFolder(excludePortal);
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

            GlobalVars.ModuleID = 20;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("BusinessID")))
            {
                var jBusiness = SepCommon.DAL.Businesses.Business_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("BusinessID")));

                if (jBusiness.BusinessID > 0) SepFunctions.Redirect(sInstallFolder + "business/" + SepCommon.SepCore.Request.Item("BusinessID") + "/" + Format_ISAPI(jBusiness.BusinessName));
            }

            if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("BusinessAccess"));

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