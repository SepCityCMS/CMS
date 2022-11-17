// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_my_assignments.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class elearning_my_assignments.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_my_assignments : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv available
        /// </summary>
        private DataView dvAvailable = new DataView();

        /// <summary>
        /// The dv submitted
        /// </summary>
        private DataView dvSubmitted = new DataView();

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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
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
                    AvailableAssignments.Columns[0].HeaderText = SepFunctions.LangText("Title");
                    AvailableAssignments.Columns[1].HeaderText = SepFunctions.LangText("Description");
                    AvailableAssignments.Columns[2].HeaderText = SepFunctions.LangText("Due Date");
                    AvailableAssignments.Columns[3].HeaderText = SepFunctions.LangText("Download");
                    AvailableAssignments.Columns[4].HeaderText = SepFunctions.LangText("View Presentation");
                    SubmittedAssignments.Columns[0].HeaderText = SepFunctions.LangText("Title");
                    SubmittedAssignments.Columns[1].HeaderText = SepFunctions.LangText("Description");
                    SubmittedAssignments.Columns[2].HeaderText = SepFunctions.LangText("Due Date");
                    SubmittedAssignments.Columns[3].HeaderText = SepFunctions.LangText("Grade");
                    SubmittedAssignments.Columns[4].HeaderText = SepFunctions.LangText("Notes");
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
                if (dvAvailable != null)
                {
                    dvAvailable.Dispose();
                }

                if (dvSubmitted != null)
                {
                    dvSubmitted.Dispose();
                }
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

            GlobalVars.ModuleID = 37;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ELearningEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "elearning_my_assignments.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            SepFunctions.RequireLogin(SepFunctions.Security("ELearningAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                dvAvailable = BindDataAvailable();
                AvailableAssignments.DataSource = dvAvailable;
                AvailableAssignments.DataBind();

                dvSubmitted = BindDataSubmitted();
                SubmittedAssignments.DataSource = dvSubmitted;
                SubmittedAssignments.DataBind();
            }

            if (AvailableAssignments.Rows.Count == 0) DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("You currently have no assignments due.") + "</div>";
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
        /// Binds the data available.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindDataAvailable()
        {
            var dELearningAssignments = SepCommon.DAL.ELearning.GetELearningAssignments(ShowAssignments: SepCommon.DAL.ELearning.AssignmentType.AvailableAssignments);

            dvAvailable = new DataView(SepFunctions.ListToDataTable(dELearningAssignments));
            return dvAvailable;
        }

        /// <summary>
        /// Binds the data submitted.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindDataSubmitted()
        {
            var dELearningAssignments = SepCommon.DAL.ELearning.GetELearningAssignments(ShowAssignments: SepCommon.DAL.ELearning.AssignmentType.SubmittedAssignments);

            dvSubmitted = new DataView(SepFunctions.ListToDataTable(dELearningAssignments));
            return dvSubmitted;
        }
    }
}