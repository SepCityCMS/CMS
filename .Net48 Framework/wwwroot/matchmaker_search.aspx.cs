// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="matchmaker_search.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class matchmaker_search.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class matchmaker_search : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The current page
        /// </summary>
        private int CurrentPage;

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
                    IAm.Items[0].Text = SepFunctions.LangText("Male");
                    IAm.Items[1].Text = SepFunctions.LangText("Female");
                    SearchingFor.Items[0].Text = SepFunctions.LangText("Male");
                    SearchingFor.Items[1].Text = SepFunctions.LangText("Female");
                }
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
            TranslatePage();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 18;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "MatchEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("MatchAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.GetUserCountry() == "us") MilesText.InnerHtml = "miles";
            else MilesText.InnerHtml = "kilometers";

            if (SepFunctions.Setup(GlobalVars.ModuleID, "SearchRadius") == "No") RadiusSearching.Visible = false;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "SearchCountry") == "No")
            {
                SearchCountry1.Visible = false;
                SearchCountry2.Visible = false;
                NoCountryHtml.InnerHtml = "<div class=\"row\"></div>";
            }

            if (!Page.IsPostBack)
            {
                SearchingFor.Value = SepFunctions.LangText("Female");

                for (var i = 18; i <= 100; i++)
                {
                    StartAge.Items.Add(new ListItem(Strings.ToString(i), Strings.ToString(i)));
                    EndAge.Items.Add(new ListItem(Strings.ToString(i), Strings.ToString(i)));
                }

                EndAge.Value = "21";

                ListContent.Visible = false;
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
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            var cProfiles = SepCommon.DAL.MatchMaker.GetMatchmakerProfiles(Sex: SearchingFor.Value, StartAge: StartAge.Value, EndAge: EndAge.Value);

            if (cProfiles.Count > 0)
            {
                ListContent.Visible = true;
                ListContent.DataSource = cProfiles.ToArray();
                ListContent.DataBind();

                if (cProfiles.Count <= SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage"))) PagerTemplate.Visible = false;
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Sorry, no results found.") + "</div>";
                PagerTemplate.Visible = false;
            }
        }
    }
}