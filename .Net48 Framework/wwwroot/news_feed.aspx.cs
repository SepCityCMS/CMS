// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="news_feed.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class news_feed.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class news_feed : Page
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

            GlobalVars.ModuleID = 67;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack" || SepFunctions.Setup(67, "STKBNewsEnable") != "Yes") SepFunctions.Redirect("tickets.aspx");

            var output = new StringBuilder();
            var rssTemplate = new StringBuilder();

            var cCRM = new CRM();

            output.AppendLine("<table class=\"GridViewStyle\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"ListContent\" style=\"border-collapse:collapse;width:100%;\">");
            output.AppendLine("<tbody>");

            rssTemplate.AppendLine("<tr>");
            rssTemplate.AppendLine("<td valign=\"top\">");
            rssTemplate.AppendLine("<div class=\"ArticleList\">");
            rssTemplate.AppendLine("<div class=\"ArticleHeadline\">");
            rssTemplate.AppendLine("<a href=\"[[LINK]]\" target=\"_blank\">[[TITLE]]</a>");
            rssTemplate.AppendLine("</div>");
            rssTemplate.AppendLine("<div class=\"ArticleSummary\">");
            rssTemplate.AppendLine("[[DESCRIPTION]]");
            rssTemplate.AppendLine("</div>");
            rssTemplate.AppendLine("<div class=\"ArticleSummaryFooter\">");
            rssTemplate.AppendLine("<img src=\"/images/admin/icn_clock.gif\" border=\"0\" alt=\"Date/Time\">");
            rssTemplate.AppendLine("[[DATE]]");
            rssTemplate.AppendLine("<span class=\"ReadMore\"><a href=\"[[LINK]]\" target=\"_blank\">");
            rssTemplate.AppendLine("<img src=\"/images/admin/icn_paper.gif\" border=\"0\" alt=\"Date/Time\">");
            rssTemplate.AppendLine("Read More</a></span>");
            rssTemplate.AppendLine("</div>");
            rssTemplate.AppendLine("</div>");
            rssTemplate.AppendLine("</td>");
            rssTemplate.AppendLine("</tr>");

            output.AppendLine(SepFunctions.RSS_Feed_Get(cCRM.ST_API_URL() + "RSS.ashx?type=news", Strings.ToString(rssTemplate)));

            output.AppendLine("</tbody>");
            output.AppendLine("</table>");

            NewsContent.InnerHtml = Strings.ToString(output);

            cCRM.Dispose();
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
    }
}