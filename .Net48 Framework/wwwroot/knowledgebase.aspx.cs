// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="knowledgebase.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class knowledgebase.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class knowledgebase : Page
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

            if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack" && SepFunctions.Setup(67, "STKBEnable") != "Yes" && SepFunctions.Setup(67, "CRMVersion") != "SuiteCRM") SepFunctions.Redirect("tickets.aspx");

            try
            {
                if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack")
                {
                    FoldersRow.Visible = false;
                    PageBrowse.Visible = false;
                }
                else
                {
                    var cCRM = new CRM();

                    var rootNodes = cCRM.ST_KnowledgeBase_Folders(1);

                    for (var i = 0; i <= rootNodes.Count - 1; i++)
                        try
                        {
                            var name = SepFunctions.ParseXML("Name", rootNodes[i].OuterXml);
                            var id = SepFunctions.toLong(SepFunctions.ParseXML("KbFolderID", rootNodes[i].OuterXml));
                            Folders.Items.Add(new ListItem("|-- " + name, Strings.ToString(id)));
                            try
                            {
                                var SubNodes = cCRM.ST_KnowledgeBase_Folders(id);
                                for (var j = 0; j <= SubNodes.Count - 1; j++)
                                {
                                    var name2 = SepFunctions.ParseXML("Name", rootNodes[j].OuterXml);
                                    var id2 = SepFunctions.toLong(SepFunctions.ParseXML("KbFolderID", rootNodes[j].OuterXml));
                                    Folders.Items.Add(new ListItem("|---- " + name2, Strings.ToString(id2)));
                                }
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                        }

                    cCRM.Dispose();
                }
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

        /// <summary>
        /// Handles the Click event of the SearchFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                var sReturn = new StringBuilder();

                var cCRM = new CRM();

                if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack")
                {
                    var rootNodes = cCRM.Suite_KnowledgeBase_Search(Keywords.Value);

                    sReturn.AppendLine("<table class=\"GridViewStyle table table-striped table-bordered\" id=\"ManageGridView\">");
                    sReturn.AppendLine("<tr>");
                    sReturn.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Subject") + "</th>");
                    sReturn.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Date Entered") + "</th>");
                    sReturn.AppendLine("</tr>");
                    for (var i = 0; i <= rootNodes.Count - 1; i++)
                        try
                        {
                            var sValue = SepFunctions.ParseXML("name", rootNodes[i].OuterXml);
                            if (!string.IsNullOrWhiteSpace(sValue))
                            {
                                sReturn.AppendLine("<tr>");
                                sReturn.AppendLine("<td><a href=\"knowledgebase_view.aspx?ArticleID=" + SepFunctions.ParseXML("id", rootNodes[i].OuterXml) + "\">" + sValue + "</a></td>");
                                sReturn.AppendLine("<td>" + SepFunctions.ParseXML("date_entered", rootNodes[i].OuterXml) + "</td>");
                                sReturn.AppendLine("</tr>");
                            }
                        }
                        catch
                        {
                            // Error message
                        }

                    sReturn.AppendLine("</table>");
                }
                else
                {
                    var rootNodes = cCRM.ST_KnowledgeBase_Search(Keywords.Value);

                    sReturn.AppendLine("<table class=\"GridViewStyle table table-striped table-bordered\" id=\"ManageGridView\">");
                    sReturn.AppendLine("<tr>");
                    sReturn.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Subject") + "</th>");
                    sReturn.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Relevancy") + "</th>");
                    sReturn.AppendLine("</tr>");
                    for (var i = 0; i <= rootNodes.Count - 1; i++)
                        try
                        {
                            var sValue = SepFunctions.ParseXML("Subject", rootNodes[i].OuterXml);
                            if (!string.IsNullOrWhiteSpace(sValue))
                            {
                                sReturn.AppendLine("<tr>");
                                sReturn.AppendLine("<td><a href=\"knowledgebase_view.aspx?ArticleID=" + SepFunctions.ParseXML("KBArticleID", rootNodes[i].OuterXml) + "\">" + sValue + "</a></td>");
                                sReturn.AppendLine("<td>" + decimal.Round(decimal.Parse(SepFunctions.ParseXML("Relevance", rootNodes[i].OuterXml)), 0) + "%</td>");
                                sReturn.AppendLine("</tr>");
                            }
                        }
                        catch
                        {
                        }

                    sReturn.AppendLine("</table>");
                }

                cCRM.Dispose();

                KBContent.InnerHtml = Strings.ToString(sReturn);
            }
            catch
            {
                KBContent.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("No results were found.") + "</div>";
            }
        }
    }
}