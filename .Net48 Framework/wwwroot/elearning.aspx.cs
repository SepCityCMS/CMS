// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning.aspx.cs" company="SepCity, Inc.">
//     Copyright ? SepCity, Inc. 2019
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
    /// Class elearning1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning1 : Page
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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            if (SepFunctions.toDate(sDate) == SepFunctions.toDate("1/1/2099"))
                return string.Empty;
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
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
        public string GetInstallFolder(bool excludePortal)
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
                    ListContent.Columns[1].HeaderText = SepFunctions.LangText("Course Name");
                    ListContent.Columns[2].HeaderText = SepFunctions.LangText("Instructor");
                    ListContent.Columns[3].HeaderText = SepFunctions.LangText("Credits");
                    ListContent.Columns[4].HeaderText = SepFunctions.LangText("Start/End Date");
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

            GlobalVars.ModuleID = 37;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ELearningEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ELearningAccess"));

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

            var cCourses = SepCommon.DAL.ELearning.GetELearningCourses(CategoryId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")), showAvailable: true);

            if (cCourses.Count > 0)
            {
                ListContent.Visible = true;
                NewestContent.Visible = false;

                dv = new DataView(SepFunctions.ListToDataTable(cCourses));
                return dv;
            }

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) == 0)
            {
                ListContent.Visible = false;
                NewestContent.Visible = true;

                if (SepFunctions.Setup(GlobalVars.ModuleID, "ELearningNewest") == "Yes")
                {
                    var cCoursesN = SepCommon.DAL.ELearning.GetELearningCourses("Mod.StartDate", "DESC", showAvailable: true);

                    NewestContent.DataSource = cCoursesN.Take(10);
                    NewestContent.DataBind();

                    if (SepFunctions.Setup(993, "RSSTop") == "Yes") NewestContent.Caption = "<table border=\"0\" width=\"99%\" cellpadding=\"2\" cellspacing=\"0\"><tr><td width=\"100%\">" + SepFunctions.LangText("Newest Courses") + "</td><td><a href=\"" + sInstallFolder + "rss.aspx?DoAction=ELearning\" target=\"_blank\"><img src=\"" + SepFunctions.GetInstallFolder(true) + "images/public/rss.png\" border=\"0\" /></a></td></tr></table>";
                }
                else
                {
                    NewestContent.Visible = false;
                }
            }

            return null;
        }
    }
}