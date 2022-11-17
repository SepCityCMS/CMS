// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="knowledgebase_view.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class knowledgebase_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class knowledgebase_view : Page
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

            if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack" && SepFunctions.Setup(67, "STKBEnable") != "Yes" && SepFunctions.Setup(67, "CRMVersion") != "SuiteCRM") SepFunctions.Redirect("tickets.aspx");

            try
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ArticleID")))
                {
                    var cCRM = new CRM();
                    if (SepFunctions.Setup(67, "CRMVersion") != "SmarterTrack")
                    {
                        var rootNodes = cCRM.Suite_KnowledgeBase_View_Item(SepCommon.SepCore.Request.Item("ArticleID"));

                        Page.Title = SepFunctions.ParseXML("name", rootNodes[0].OuterXml);
                        ArticleSubject.InnerText = SepFunctions.ParseXML("name", rootNodes[0].OuterXml);
                        ArticleContent.InnerHtml = SepFunctions.HTMLDecode(SepFunctions.ParseXML("description", rootNodes[0].OuterXml));
                        ArticleFooter.InnerHtml = "Created On: " + Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.ParseXML("date_entered", rootNodes[0].OuterXml)), Strings.DateNamedFormat.ShortDate) + ", Modified: " + Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.ParseXML("date_modified", rootNodes[0].OuterXml)), Strings.DateNamedFormat.ShortDate);
                    }
                    else
                    {
                        var rootNodes = cCRM.ST_KnowledgeBase_View_Item(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ArticleID")));

                        Page.Title = SepFunctions.ParseXML("Subject", rootNodes[0].OuterXml);
                        Page.MetaDescription = SepFunctions.ParseXML("ShortSummary", rootNodes[0].OuterXml);
                        Page.MetaKeywords = SepFunctions.ParseXML("OtherKeywords", rootNodes[0].OuterXml);
                        ArticleSubject.InnerText = SepFunctions.ParseXML("Subject", rootNodes[0].OuterXml);
                        ArticleContent.InnerHtml = SepFunctions.HTMLDecode(SepFunctions.ParseXML("Body", rootNodes[0].OuterXml));
                        ArticleFooter.InnerHtml = "Article ID: " + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ArticleID")) + ", Created On: " + Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.ParseXML("DateCreatedUTC", rootNodes[0].OuterXml)), Strings.DateNamedFormat.ShortDate) + ", Modified: " + Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.ParseXML("DateModifiedUTC", rootNodes[0].OuterXml)), Strings.DateNamedFormat.ShortDate);
                    }

                    cCRM.Dispose();
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Knowledge Base article~~ does not exist.") + "</div>";
                    PageContent.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log(ex.Message);
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Knowledge Base article~~ does not exist.") + "</div>";
                PageContent.Visible = false;
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