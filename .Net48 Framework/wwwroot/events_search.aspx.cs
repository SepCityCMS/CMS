// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="events_search.aspx.cs" company="SepCity, Inc.">
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
    /// Class events_search.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class events_search : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The paging
        /// </summary>
        protected DataTable Paging { get; set; }

        /// <summary>
        /// The results
        /// </summary>
        protected DataTable Results { get; set; }

        /// <summary>
        /// The summary
        /// </summary>
        protected string Summary { get; set; }

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
                if (Paging != null)
                {
                    Paging.Dispose();
                }

                if (Results != null)
                {
                    Results.Dispose();
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

            GlobalVars.ModuleID = 46;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "EventsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("EventsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("q")))
            {
                q.Value = SepCommon.SepCore.Request.Item("q");
                var cLucene = new LuceneSearch
                {
                    ModuleID = GlobalVars.ModuleID,
                    Keywords = SepCommon.SepCore.Request.Item("q"),
                    InitStartAt = SepFunctions.toInt(SepCommon.SepCore.Request.Item("start")),
                    maxResults = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"))
                };
                Results = cLucene.search();
                Paging = cLucene.Paging();
                Summary = cLucene.Summary();
                DataBind();
            }
            else
            {
                SearchResults.Visible = false;
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
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(q.Value))
            {
                ErrorMessage.InnerHtml = string.Empty;
                SearchResults.Visible = true;
                var cLucene = new LuceneSearch
                {
                    Keywords = q.Value,
                    ModuleID = GlobalVars.ModuleID,
                    UserID = SepFunctions.Session_User_ID(),
                    InitStartAt = SepFunctions.toInt(SepCommon.SepCore.Request.Item("start")),
                    maxResults = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"))
                };
                Results = cLucene.search();
                Paging = cLucene.Paging();
                Summary = cLucene.Summary();
                DataBind();
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Search", q.Value);

                if (Results.Rows.Count == 0)
                {
                    SearchResults.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There are no search results to return.") + "</div>";
                }
                else
                {
                    ErrorMessage.InnerHtml = string.Empty;
                }
            }
            else
            {
                SearchResults.Visible = false;
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must enter a keyword to run the search.") + "</div>";
            }
        }
    }
}