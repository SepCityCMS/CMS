// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userpages_config.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class userpages_config.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userpages_config : Page
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
                    ShowList.Items[0].Text = SepFunctions.LangText("Yes");
                    ShowList.Items[1].Text = SepFunctions.LangText("No");
                    InviteOnly.Items[0].Text = SepFunctions.LangText("Yes");
                    InviteOnly.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Configuration");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    SiteNameLabel.InnerText = SepFunctions.LangText("Site Name:");
                    SiteLogoLabel.InnerText = SepFunctions.LangText("Site Logo:");
                    SiteSloganLabel.InnerText = SepFunctions.LangText("Site Slogan:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    TemplateIDLabel.InnerText = SepFunctions.LangText("Select Template:");
                    ShowListLabel.InnerText = SepFunctions.LangText("Show your site on the site listings:");
                    PortalSelectionLabel.InnerText = SepFunctions.LangText("Portal to associate your user site with:");
                    InviteOnlyLabel.InnerText = SepFunctions.LangText("Make your web site invite only:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    SiteNameRequired.ErrorMessage = SepFunctions.LangText("~~Site Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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

            var mp = Page.Master;
            var form_ = (HtmlForm)mp.FindControl("aspnetForm");
            form_.Attributes.Add("enctype", "multipart/form-data");

            if (SepFunctions.CompareKeys(SepFunctions.Security("UPagesPortalSelection"), false) == false) PortalSelectionRow.Visible = false;

            if (!Page.IsPostBack)
            {
                var jUserPages = SepCommon.DAL.UserPages.Site_Get(SepCommon.DAL.UserPages.UserID_to_SiteID(SepFunctions.Session_User_ID()));

                if (jUserPages.SiteID == 0)
                {
                    SepFunctions.Redirect(sInstallFolder + "userpages_site_modify.aspx");
                }
                else
                {
                    SiteID.Value = Strings.ToString(jUserPages.SiteID);
                    Category.CatID = Strings.ToString(jUserPages.CatID);
                    SiteName.Value = jUserPages.SiteName;
                    SiteSlogan.Value = jUserPages.Slogan;
                    Description.Value = jUserPages.Description;
                    TemplateID.Text = Strings.ToString(jUserPages.TemplateID);
                    EnableGuestbook.Checked = jUserPages.Guestbook;
                    SiteLogo.UserID = SepFunctions.Session_User_ID();
                    SiteLogo.ContentID = Strings.ToString(jUserPages.SiteID);
                    PortalSelection.Text = Strings.ToString(jUserPages.PortalID);
                    if (jUserPages.InviteOnly == false)
                    {
                        InviteOnly.Value = "false";
                    }
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                SiteLogo.showTemp = true;
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var intReturn = SepCommon.DAL.UserPages.Save_Web_Site(SepFunctions.toLong(SiteID.Value), SepFunctions.toLong(Category.CatID), SepFunctions.Session_User_ID(), SiteName.Value, SiteSlogan.Value, Description.Value, SepFunctions.toLong(TemplateID.Text), SepFunctions.toBoolean(ShowList.Value), SepFunctions.toBoolean(InviteOnly.Value), EnableGuestbook.Checked, SepFunctions.toLong(PortalSelection.Text));

            Globals.Save_Template(TemplateID.Text, false, 7, SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}