// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="celebrities.aspx.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class conference.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class conference : Page
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
            var cProfiles = SepCommon.DAL.UserProfiles.GetUserProfiles(ModuleID: 64);

            if (cProfiles.Count > 0)
            {
                ListContent.DataSource = cProfiles.ToArray();
                ListContent.DataBind();

                if (cProfiles.Count <= SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage"))) PagerTemplate.Visible = false;
            }
            else
            {
                PagerTemplate.Visible = false;
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
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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
            GlobalVars.ModuleID = 64;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            if (SepCommon.SepCore.Request.Item("DoAction") == "CallSucccessful") ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Call has been successfully scheduled.") + "</div>";

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