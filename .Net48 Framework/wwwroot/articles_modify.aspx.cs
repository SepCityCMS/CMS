// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="articles_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class articles_modify1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class articles_modify1 : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Post an Article");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    HeadlineLabel.InnerText = SepFunctions.LangText("Headline:");
                    HeadlineDateLabel.InnerText = SepFunctions.LangText("Headline Date:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Start Date:");
                    EndDateLabel.InnerText = SepFunctions.LangText("End Date:");
                    AuthorLabel.InnerText = SepFunctions.LangText("Author:");
                    SummaryLabel.InnerText = SepFunctions.LangText("Summary:");
                    SourceLabel.InnerText = SepFunctions.LangText("Article Source Name:");
                    ArticleURLLabel.InnerText = SepFunctions.LangText("URL to the Article Source:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Pictures:");
                    MetaKeywordsLabel.InnerText = SepFunctions.LangText("Meta Keywords:");
                    MetaDescriptionLabel.InnerText = SepFunctions.LangText("Meta Description:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    HeadlineRequired.ErrorMessage = SepFunctions.LangText("~~Headline~~ is required.");
                    HeadlineDateRequired.ErrorMessage = SepFunctions.LangText("~~Headline Date~~ is required.");
                    AuthorRequired.ErrorMessage = SepFunctions.LangText("~~Author~~ is required.");
                    SummaryRequired.ErrorMessage = SepFunctions.LangText("~~Summary~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 35;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ArticlesEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ArticlesPost"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ArticleShowSource") == "No")
            {
                SourceNameRow.Visible = false;
                SourceURLRow.Visible = false;
            }

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ArticleShowMeta") == "No")
            {
                MetaKeywordsRow.Visible = false;
                MetaDescriptionRow.Visible = false;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ArticleID")))
            {
                var jArticles = SepCommon.DAL.Articles.Article_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ArticleID")));

                if (jArticles.ArticleID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Article~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Article");
                    ArticleID.Value = SepCommon.SepCore.Request.Item("ArticleID");
                    Pictures.ContentID = SepCommon.SepCore.Request.Item("ArticleID");
                    Category.CatID = Strings.ToString(jArticles.CatID);
                    Headline.Value = jArticles.Headline;
                    HeadlineDate.Value = Strings.FormatDateTime(jArticles.Headline_Date, Strings.DateNamedFormat.ShortDate);
                    StartDate.Value = Strings.FormatDateTime(jArticles.Start_Date, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(jArticles.End_Date, Strings.DateNamedFormat.ShortDate);
                    Author.Value = jArticles.Author;
                    Summary.Value = jArticles.Summary;
                    SEOMetaKeywords.Value = jArticles.Meta_Keywords;
                    SEOMetaDescription.Value = jArticles.Meta_Description;
                    ArticleText.Text = jArticles.Full_Article;
                    ArticleURL.Value = jArticles.Article_URL;
                    Source.Value = jArticles.Source;
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                    ArticleText.Text = ArticleText.Text;
                }
                else
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostPostArticle", "GetPostArticle", ArticleID.Value, true) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(ArticleID.Value)) ArticleID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    HeadlineDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    StartDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 1, DateTime.Today), Strings.DateNamedFormat.ShortDate);
                    Author.Value = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName");
                    Category.CatID = SepCommon.SepCore.Request.Item("CatID");
                }
            }

            Pictures.ContentID = ArticleID.Value;

            if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes") CategoryRequired.ErrorMessage = SepFunctions.LangText("You must select the lowest level of category available.");
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                Pictures.showTemp = true;
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var intReturn = SepCommon.DAL.Articles.Article_Save(SepFunctions.toLong(ArticleID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), Headline.Value, Author.Value, SepFunctions.toDate(HeadlineDate.Value), SepFunctions.toDate(StartDate.Value), SepFunctions.toDate(EndDate.Value), Summary.Value, ArticleText.Text, Source.Value, ArticleURL.Value, SEOMetaDescription.Value, SEOMetaKeywords.Value, string.Empty, 1, SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, Headline.Value);

            ModFormDiv.Visible = false;
        }
    }
}