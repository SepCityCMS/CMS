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
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class articles_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class articles_modify : Page
    {
        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    Status.Items[0].Text = SepFunctions.LangText("Published");
                    Status.Items[1].Text = SepFunctions.LangText("Pending");
                    Status.Items[2].Text = SepFunctions.LangText("Archived");
                    ManageGridView.Columns[0].HeaderText = SepFunctions.LangText("Headline");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("Headline Date");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Article");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    HeadlineLabel.InnerText = SepFunctions.LangText("Headline:");
                    HeadlineDateLabel.InnerText = SepFunctions.LangText("Headline Date:");
                    AuthorLabel.InnerText = SepFunctions.LangText("Author:");
                    SummaryLabel.InnerText = SepFunctions.LangText("Summary:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Start Date:");
                    EndDateLabel.InnerText = SepFunctions.LangText("End Date:");
                    StatusLabel.InnerText = SepFunctions.LangText("Status:");
                    SourceLabel.InnerText = SepFunctions.LangText("Article Source Name:");
                    ArticleURLLabel.InnerText = SepFunctions.LangText("URL to the Article Source:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Pictures:");
                    MetaKeywordsLabel.InnerText = SepFunctions.LangText("Meta Keywords:");
                    MetaDescriptionLabel.InnerText = SepFunctions.LangText("Meta Description:");
                    RelatedArticlesLabel.InnerText = SepFunctions.LangText("Related Articles:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    HeadlineRequired.ErrorMessage = SepFunctions.LangText("~~Headline~~ is required.");
                    AuthorRequired.ErrorMessage = SepFunctions.LangText("~~Author~~ is required.");
                    SummaryRequired.ErrorMessage = SepFunctions.LangText("~~Summary~~ is required.");
                    StatusRequired.ErrorMessage = SepFunctions.LangText("~~Status~~ is required.");
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

            GlobalVars.ModuleID = 35;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ArticlesAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ArticlesAdmin"), true) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ArticleID")))
            {
                var jArticles = SepCommon.DAL.Articles.Article_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ArticleID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

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
                    Pictures.UserID = jArticles.UserID;
                    Category.CatID = Strings.ToString(jArticles.CatID);
                    Headline.Value = jArticles.Headline;
                    HeadlineDate.Value = Strings.FormatDateTime(jArticles.Headline_Date, Strings.DateNamedFormat.ShortDate);
                    Author.Value = jArticles.Author;
                    Summary.Value = jArticles.Summary;
                    StartDate.Value = Strings.FormatDateTime(jArticles.Start_Date, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(jArticles.End_Date, Strings.DateNamedFormat.ShortDate);
                    Status.Value = Strings.ToString(jArticles.Status);
                    SEOMetaKeywords.Value = jArticles.Meta_Keywords;
                    SEOMetaDescription.Value = jArticles.Meta_Description;
                    ArticleText.Text = jArticles.Full_Article;
                    Portal.Text = Strings.ToString(jArticles.PortalID);
                    ArticleURL.Value = jArticles.Article_URL;
                    RelatedArticles.Value = jArticles.Related_Articles;
                    Source.Value = jArticles.Source;
                    sUserID = jArticles.UserID;

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("ArticleID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");

                    var arrRelated = Strings.Split(jArticles.Related_Articles, ",");
                    if (Information.UBound(arrRelated) > 0)
                    {
                        var tbl = new DataTable();
                        var col1 = new DataColumn("Headline", typeof(string));
                        var col2 = new DataColumn("Author", typeof(string));
                        var col3 = new DataColumn("Status", typeof(string));
                        var col4 = new DataColumn("Headline_Date", typeof(string));
                        long articleCount = 0;
                        tbl.Columns.Add(col1);
                        tbl.Columns.Add(col2);
                        tbl.Columns.Add(col3);
                        tbl.Columns.Add(col4);
                        if (arrRelated != null)
                        {
                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                for (var i = 1; i <= Information.UBound(arrRelated); i++)
                                {
                                    using (var cmd = new SqlCommand("SELECT Headline,Author,Status,Headline_Date FROM Articles WHERE ArticleID=@ArticleID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ArticleID", arrRelated[i]);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            while (RS.Read())
                                            {
                                                articleCount += 1;
                                                var row = tbl.NewRow();
                                                row["Headline"] = SepFunctions.openNull(RS["Headline"]);
                                                row["Author"] = SepFunctions.openNull(RS["Author"]);
                                                row["Status"] = SepFunctions.openNull(RS["Status"]);
                                                row["Headline_Date"] = SepFunctions.openNull(RS["Headline_Date"]);
                                                tbl.Rows.Add(row);
                                                row = tbl.NewRow();
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        ManageGridView.DataSource = tbl;
                        ManageGridView.DataBind();
                        AddArticle2.NavigateUrl = "javascript:addRelatedArticle('" + articleCount + "')";
                    }
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                    Portal.Text = Request.Form["Portal"];
                    ArticleText.Text = ArticleText.Text;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(ArticleID.Value)) ArticleID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    HeadlineDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    StartDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 1, DateTime.Today), Strings.DateNamedFormat.ShortDate);
                    Author.Value = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName");
                }

                Pictures.ContentID = ArticleID.Value;
            }

            if (ManageGridView.Rows.Count == 0)
            {
                var tbl = new DataTable();
                var col1 = new DataColumn("Headline", typeof(string));
                var col2 = new DataColumn("Author", typeof(string));
                var col3 = new DataColumn("Status", typeof(string));
                var col4 = new DataColumn("Headline_Date", typeof(string));
                tbl.Columns.Add(col1);
                tbl.Columns.Add(col2);
                tbl.Columns.Add(col3);
                tbl.Columns.Add(col4);
                var row = tbl.NewRow();
                row["Headline"] = string.Empty;
                row["Author"] = string.Empty;
                row["Status"] = string.Empty;
                row["Headline_Date"] = DateTime.Now;
                tbl.Rows.Add(row);
                ManageGridView.DataSource = tbl;
                ManageGridView.DataBind();
                ManageGridView.Rows[0].Visible = false;
            }

            if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes") CategoryRequired.ErrorMessage = SepFunctions.LangText("You must select the lowest level of category available.");
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

            var intReturn = SepCommon.DAL.Articles.Article_Save(SepFunctions.toLong(ArticleID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), Headline.Value, Author.Value, SepFunctions.toDate(HeadlineDate.Value), SepFunctions.toDate(StartDate.Value), SepFunctions.toDate(EndDate.Value), Summary.Value, ArticleText.Text, Source.Value, ArticleURL.Value, SEOMetaDescription.Value, SEOMetaKeywords.Value, RelatedArticles.Value, SepFunctions.toInt(Status.Value), SepFunctions.toLong(Request.Form["Portal"]));

            if (intReturn > 1)
            {
                ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);

                var jArticles = SepCommon.DAL.Articles.Article_Get(SepFunctions.toLong(ArticleID.Value));

                var arrRelated = Strings.Split(jArticles.Related_Articles, ",");
                if (Information.UBound(arrRelated) > 0)
                {
                    var tbl = new DataTable();
                    var col1 = new DataColumn("Headline", typeof(string));
                    var col2 = new DataColumn("Author", typeof(string));
                    var col3 = new DataColumn("Status", typeof(string));
                    var col4 = new DataColumn("Headline_Date", typeof(string));
                    long articleCount = 0;
                    tbl.Columns.Add(col1);
                    tbl.Columns.Add(col2);
                    tbl.Columns.Add(col3);
                    tbl.Columns.Add(col4);
                    if (arrRelated != null)
                    {
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            for (var i = 1; i <= Information.UBound(arrRelated); i++)
                            {
                                using (var cmd = new SqlCommand("SELECT Headline,Author,Status,Headline_Date FROM Articles WHERE ArticleID=@ArticleID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ArticleID", arrRelated[i]);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        while (RS.Read())
                                        {
                                            articleCount += 1;
                                            var row = tbl.NewRow();
                                            row["Headline"] = SepFunctions.openNull(RS["Headline"]);
                                            row["Author"] = SepFunctions.openNull(RS["Author"]);
                                            row["Status"] = SepFunctions.openNull(RS["Status"]);
                                            row["Headline_Date"] = SepFunctions.openNull(RS["Headline_Date"]);
                                            tbl.Rows.Add(row);
                                            row = tbl.NewRow();
                                        }

                                    }
                                }
                            }
                        }
                    }

                    ManageGridView.DataSource = tbl;
                    ManageGridView.DataBind();
                    AddArticle2.NavigateUrl = "javascript:addRelatedArticle('" + articleCount + "')";
                }
            }
            else
            {
                ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
            }
        }
    }
}