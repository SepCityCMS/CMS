// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userpages_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class userpages_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userpages_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Edit Site");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    SiteNameLabel.InnerText = SepFunctions.LangText("Site Name:");
                    SiteLogoLabel.InnerText = SepFunctions.LangText("Site Logo:");
                    SiteSloganLabel.InnerText = SepFunctions.LangText("Site Slogan:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    SelectTemplateLabel.InnerText = SepFunctions.LangText("Select Template:");
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
            TranslatePage();

            GlobalVars.ModuleID = 7;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("UPagesManage")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("UPagesManage"), true) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SiteID")))
            {
                var jUserPages = SepCommon.DAL.UserPages.Site_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SiteID")));

                if (jUserPages.SiteID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Site~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    SiteID.Value = SepCommon.SepCore.Request.Item("SiteID");
                    Category.CatID = Strings.ToString(jUserPages.CatID);
                    UserID.Value = jUserPages.UserID;
                    SiteName.Value = jUserPages.SiteName;
                    SiteSlogan.Value = jUserPages.Slogan;
                    Description.Value = jUserPages.Description;
                    TemplateID.Text = Strings.ToString(jUserPages.TemplateID);
                    EnableGuestbook.Checked = jUserPages.Guestbook;
                    SiteLogo.UserID = jUserPages.UserID;
                    SiteLogo.ContentID = Strings.ToString(jUserPages.SiteID);
                    PortalSelection.Text = Strings.ToString(jUserPages.PortalID);
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(SiteID.Value)) SiteID.Value = Strings.ToString(SepFunctions.GetIdentity());
                }
            }

            if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes") CategoryRequired.ErrorMessage = SepFunctions.LangText("You must select the lowest level of category available.");
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
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var intReturn = SepCommon.DAL.UserPages.Save_Web_Site(SepFunctions.toLong(SiteID.Value), SepFunctions.toLong(Category.CatID), UserID.Value, SiteName.Value, SiteSlogan.Value, Description.Value, SepFunctions.toLong(TemplateID.Text), SepFunctions.toBoolean(ShowList.Value), SepFunctions.toBoolean(InviteOnly.Value), EnableGuestbook.Checked, SepFunctions.toLong(PortalSelection.Text));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, SiteName.Value);
        }
    }
}