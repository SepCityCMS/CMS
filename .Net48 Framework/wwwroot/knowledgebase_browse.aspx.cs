// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="knowledgebase_browse.aspx.cs" company="SepCity, Inc.">
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
    /// Class knowledgebase_browse.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class knowledgebase_browse : Page
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
            GlobalVars.ModuleID = 67;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack" && SepFunctions.Setup(67, "STKBEnable") != "Yes") SepFunctions.Redirect("tickets.aspx");

            var output = new StringBuilder();

            try
            {
                var cCRM = new CRM();

                long folderId = 1;

                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("FolderID")) > 0)
                {
                    folderId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("FolderID"));
                    output.AppendLine("<a href=\"knowledgebase_browse.aspx\">" + SepFunctions.LangText(".. Parent") + "</a><br/>");
                }

                var rootNodes = cCRM.ST_KnowledgeBase_Folders(folderId);

                for (var i = 0; i <= rootNodes.Count - 1; i++)
                    try
                    {
                        var name = SepFunctions.ParseXML("Name", rootNodes[i].OuterXml);
                        var id = SepFunctions.toLong(SepFunctions.ParseXML("KbFolderID", rootNodes[i].OuterXml));
                        output.AppendLine("<a href=\"knowledgebase_browse.aspx?FolderID=" + id + "\">" + name + "</a><br/>");
                    }
                    catch
                    {
                    }

                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("FolderID")) > 0)
                {
                    var articleNodes = cCRM.ST_KnowledgeBase_List_Articles(folderId);

                    if (articleNodes.Count > 0)
                    {
                        output.AppendLine("<table class=\"GridViewStyle table table-striped table-bordered\" id=\"ManageGridView\">");
                        output.AppendLine("<tr>");
                        output.AppendLine("<th scope=\"col\">Subject</th>");
                        output.AppendLine("</tr>");
                        for (var i = 0; i <= articleNodes.Count - 1; i++)
                            try
                            {
                                var sValue = SepFunctions.ParseXML("Subject", articleNodes[i].OuterXml);
                                if (!string.IsNullOrWhiteSpace(sValue))
                                {
                                    output.AppendLine("<tr>");
                                    output.AppendLine("<td><a href=\"knowledgebase_view.aspx?ArticleID=" + SepFunctions.ParseXML("KBArticleID", articleNodes[i].OuterXml) + "\">" + sValue + "</a></td>");
                                    output.AppendLine("</tr>");
                                }
                            }
                            catch
                            {
                            }

                        output.AppendLine("</table>");
                    }
                }

                cCRM.Dispose();

                KBContent.InnerHtml = Strings.ToString(output);
            }
            catch
            {
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
    }
}