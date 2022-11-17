// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userpages_pages_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.DAL;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    ///     Class userpages_pages_modify.
    ///     Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userpages_pages_modify : Page
    {
        /// <summary>
        ///     Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Web Page");
                    PageTitleLabel.InnerText = SepFunctions.LangText("Page Title:");
                    PagePasswordLabel.InnerText = SepFunctions.LangText("Password for this Page:");
                    PageTitleRequired.ErrorMessage = SepFunctions.LangText("~~Page Title~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        ///     The c common
        /// </summary>
        /// <summary>
        ///     Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID())) ViewStateUserKey = SepFunctions.Session_User_ID();

            base.OnInit(e);
        }

        /// <summary>
        ///     Handles the Load event of the Page control.
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

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu1")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu2")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu3")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu4")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu5")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu6")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu7")) || SepFunctions.Setup(7, "UPagesMainMenu1") == "Yes" || SepFunctions.Setup(7, "UPagesMainMenu2") == "Yes" || SepFunctions.Setup(7, "UPagesMainMenu3") == "Yes" || SepFunctions.Setup(7, "UPagesMainMenu4") == "Yes" || SepFunctions.Setup(7, "UPagesMainMenu5") == "Yes" || SepFunctions.Setup(7, "UPagesMainMenu6") == "Yes" || SepFunctions.Setup(7, "UPagesMainMenu7") == "Yes") MenuRow.Visible = true;
            else MenuRow.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PageID")))
            {
                var jUserPages = UserPages.Page_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PageID")));

                if (jUserPages.PageID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Page~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Web Page");
                    PageID.Value = SepCommon.SepCore.Request.Item("PageID");
                    MenuID.MenuID = Strings.ToString(jUserPages.MenuID);
                    PageTitle.Value = jUserPages.PageTitle;
                    PageText.Text = jUserPages.PageText;
                    PagePassword.Value = jUserPages.Password;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                    if (string.IsNullOrWhiteSpace(PageID.Value))
                        PageID.Value = Strings.ToString(SepFunctions.GetIdentity());
            }
        }

        /// <summary>
        ///     Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        ///     Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }

        /// <summary>
        ///     Handles the Click event of the SaveButton control.
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

            var intReturn = UserPages.Save_Web_Page(SepFunctions.toLong(PageID.Value), SepFunctions.Session_User_ID(), SepFunctions.toInt(MenuID.MenuID), PageTitle.Value, PageText.Text, 0, 200, PagePassword.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, PageTitle.Value);
        }
    }
}