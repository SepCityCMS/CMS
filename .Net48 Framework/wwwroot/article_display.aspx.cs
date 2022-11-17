// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="article_display.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class article_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class article_display : Page
    {
        /// <summary>
        /// The s user name
        /// </summary>
        public static string sUserName = string.Empty;

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
            SepFunctions.RequireLogin(SepFunctions.Security("ArticlesAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0] + " - " + SepFunctions.LangText("Display Article");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepCommon.SepCore.Request.Item("DoAction") == "Print") PrintButtonRow.Visible = true;
            else PrintButtonRow.Visible = false;

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ArticleID")))
                {
                    var sArticleId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ArticleID"));

                    var jArticles = SepCommon.DAL.Articles.Article_Get(sArticleId);
                    if (jArticles.ArticleID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Article~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                    else
                    {
                        Page.MetaKeywords = jArticles.Meta_Keywords;
                        Page.MetaDescription = jArticles.Meta_Description;
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewArticle", "GetViewArticle", Strings.ToString(sArticleId), false) == false)
                        {
                            SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                            return;
                        }

                        if (SepFunctions.Setup(35, "ArticleShowPic") == "Yes")
                        {
                            if (!string.IsNullOrWhiteSpace(jArticles.UserID)) ProfilePic.InnerHtml = "<img src=\"" + SepFunctions.userProfileImage(jArticles.UserID) + "\" border=\"0\" />";
                        }
                        else
                        {
                            ProfilePic.Visible = false;
                        }

                        Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jArticles.Headline);
                        Headline.InnerHtml = jArticles.Headline;
                        if (!string.IsNullOrWhiteSpace(jArticles.Author))
                        {
                            if (!string.IsNullOrWhiteSpace(jArticles.UserID)) Author.InnerHtml = "<a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + jArticles.UserID + "\">" + jArticles.Author + "</a>";
                            else Author.InnerHtml = jArticles.Author;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(jArticles.UserID)) Author.InnerHtml = "<a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + jArticles.UserID + "\">" + SepFunctions.GetUserInformation("UserName", jArticles.UserID) + "</a>";
                        }

                        var jCategories = SepCommon.DAL.Categories.Category_Get(jArticles.CatID);
                        CategoryName.InnerHtml = jCategories.CategoryName;
                        if (string.IsNullOrWhiteSpace(CategoryName.InnerHtml))
                        {
                            CategoryColumn.Visible = false;
                        }

                        if (!string.IsNullOrWhiteSpace(jArticles.Source))
                        {
                            if (!string.IsNullOrWhiteSpace(jArticles.Article_URL)) ArticleURL.InnerHtml = "<a href=\"" + jArticles.Article_URL + "\" target=\"_blank\">" + jArticles.Source + "</a>";
                            else ArticleURL.InnerHtml = jArticles.Source;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(jArticles.Article_URL)) ArticleURL.InnerHtml = "<a href=\"" + jArticles.Article_URL + "\" target=\"_blank\">" + jArticles.Article_URL + "</a>";
                        }

                        if (string.IsNullOrWhiteSpace(ArticleURL.InnerHtml)) SourceInfo.Visible = false;
                        if (jArticles.Visits > 1) Visits.InnerHtml = jArticles.Visits + " times.";
                        else Visits.InnerHtml = jArticles.Visits + " time.";
                        Full_Article.InnerHtml = jArticles.Full_Article;
                        HeadlineDate.InnerHtml = Strings.FormatDateTime(jArticles.Headline_Date, Strings.DateNamedFormat.LongDate);

                        // Custom Fields
                        sUserName = jArticles.UserID;

                        // Show Images
                        ArticleImages.ContentUniqueID = Strings.ToString(sArticleId);
                        ArticleImages.ModuleID = GlobalVars.ModuleID;
                        ArticleImages.UserID = jArticles.UserID;

                        if (SepCommon.SepCore.Request.Item("DoAction") != "Print")
                        {
                            RatingStars.LookupID = Strings.ToString(sArticleId);
                            RatingGraph.LookupID = Strings.ToString(sArticleId);

                            List<Articles> jArticles2 = SepCommon.DAL.Articles.GetArticles("Headline", "ASC", string.Empty, 0, jArticles.ArticleID, UserID: SepFunctions.Session_User_ID());
                            if (jArticles2.Count > 0)
                            {
                                if (jArticles2.Count > 1) ArtAuthor.Items.Add(new ListItem(SepFunctions.LangText("Select from ~~" + jArticles2.Count + "~~ other articles"), string.Empty));
                                else ArtAuthor.Items.Add(new ListItem(SepFunctions.LangText("Select from ~~" + jArticles2.Count + "~~ other article"), string.Empty));
                                for (var i = 0; i <= jArticles2.Count - 1; i++) ArtAuthor.Items.Add(new ListItem(jArticles2[i].Headline, sInstallFolder + "article/" + jArticles2[i].ArticleID + "/" + SepFunctions.Format_ISAPI(jArticles2[i].Headline) + "/"));
                            }
                            else
                            {
                                ArtAuthorRow.Visible = false;
                            }

                            jArticles2 = SepCommon.DAL.Articles.GetArticles("Headline", "ASC", string.Empty, jArticles.CatID, jArticles.ArticleID);
                            if (jArticles2.Count > 0)
                            {
                                if (jArticles2.Count > 1) CatArticles.Items.Add(new ListItem(SepFunctions.LangText("Select from ~~" + jArticles2.Count + "~~ other articles"), string.Empty));
                                else CatArticles.Items.Add(new ListItem(SepFunctions.LangText("Select from ~~" + jArticles2.Count + "~~ other article"), string.Empty));
                                for (var i = 0; i <= jArticles2.Count - 1; i++) CatArticles.Items.Add(new ListItem(jArticles2[i].Headline, sInstallFolder + "article/" + jArticles2[i].ArticleID + "/" + SepFunctions.Format_ISAPI(jArticles2[i].Headline) + "/"));
                            }
                            else
                            {
                                CatArticlesRow.Visible = false;
                            }
                        }
                        else
                        {
                            RatingStars.Visible = false;
                            RatingGraph.Visible = false;
                            ArtAuthorRow.Visible = false;
                            CatArticlesRow.Visible = false;
                        }
                    }
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Article~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
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