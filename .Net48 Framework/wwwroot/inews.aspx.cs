// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="inews.aspx.cs" company="SepCity, Inc.">
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
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class inews.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class inews : Page
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
            var str = new StringBuilder();
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 56;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageContent.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();


            string url;
            if (SepFunctions.Setup(991, "CompanyCountry") == "nz")
            {
                // New Zealand RSS Feed
                url = "http://news.google.co.nz/news?pz=1&ned=nz&hl=en&output=rss";
            }
            else
            {
                // America RSS Feeds
                str.Append("<p align=\"center\">[ <a href=\"" + sInstallFolder + "inews.aspx?DoAction=Business\">" + SepFunctions.LangText("Business") + "</a> | <a href=\"" + sInstallFolder + "inews.aspx?DoAction=Entertainment\">" + SepFunctions.LangText("Entertainment") + "</a> | <a href=\"" + sInstallFolder + "inews.aspx?DoAction=Health\">" + SepFunctions.LangText("Health") + "</a> | <a href=\"" + sInstallFolder + "inews.aspx?DoAction=Sports\">" + SepFunctions.LangText("Sports") + "</a> | <a href=\"" + sInstallFolder + "inews.aspx?DoAction=Technology\">" + SepFunctions.LangText("Technology") + "</a> | <a href=\"" + sInstallFolder + "inews.aspx?DoAction=TopStories\">" + SepFunctions.LangText("Top Stories") + "</a> ]</p>");
                switch (SepCommon.SepCore.Request.Item("DoAction"))
                {
                    case "Business":
                        url = "http://rss.news.yahoo.com/rss/business";
                        break;

                    case "Entertainment":
                        url = "http://rss.news.yahoo.com/rss/entertainment";
                        break;

                    case "Health":
                        url = "http://rss.news.yahoo.com/rss/health";
                        break;

                    case "Sports":
                        url = "http://rss.news.yahoo.com/rss/sports";
                        break;

                    case "Technology":
                        url = "http://rss.news.yahoo.com/rss/tech";
                        break;

                    default:
                        url = "http://rss.news.yahoo.com/rss/highestrated";
                        break;
                }
            }

            var rssTemplate = new StringBuilder();

            rssTemplate.AppendLine("<div class=\"article-bx\">");
            rssTemplate.AppendLine("<div class=\"row\">");
            rssTemplate.AppendLine("<div class=\"col-md-12\">");
            rssTemplate.AppendLine("	<div class=\"article-content-area\">");
            rssTemplate.AppendLine("		<div class=\"article-top-content\">");
            rssTemplate.AppendLine("		<h3><a href=\"[[LINK]]\" target=\"_blank\">[[TITLE]]</a></h3>");
            rssTemplate.AppendLine("		[[DESCRIPTION]]");
            rssTemplate.AppendLine("        </div>");
            rssTemplate.AppendLine("		<div class=\"article-btn-group\">");
            rssTemplate.AppendLine("			<p><span>Date :</span> [[DATE]]</p>");
            rssTemplate.AppendLine("			<a href=\"[[LINK]]\" target=\"_blank\" class=\"btn btn-primary\">Read More</a>                                    ");
            rssTemplate.AppendLine("	  </div>");
            rssTemplate.AppendLine("	</div>");
            rssTemplate.AppendLine("</div>");
            rssTemplate.AppendLine("</div>");
            rssTemplate.AppendLine("</div>");

            ShowNews.InnerHtml = SepFunctions.RSS_Feed_Get(url, Strings.ToString(rssTemplate));

            // str.Append("<table width=\"95%\" align=\"center\"><tr><td>" + SepFunctions.RSS_Feed_Get(url, "") + "</td></tr></table>");

            // ShowNews.InnerHtml = SepCommon.SepCore.Strings.ToString(str);
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