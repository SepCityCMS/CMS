// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="news_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class news_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class news_modify : Page
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
                    AdditionalOptions.Items[0].Text = SepFunctions.LangText("Parent Window (Default)");
                    AdditionalOptions.Items[1].Text = SepFunctions.LangText("New Window");
                    AdditionalOptions.Items[2].Text = SepFunctions.LangText("No Link to Story");
                    AdditionalOptions.Items[3].Text = SepFunctions.LangText("Forward to URL");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add News");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    TopicNameLabel.InnerText = SepFunctions.LangText("Topic Name:");
                    AdditionalOptionsLabel.InnerText = SepFunctions.LangText("Additional Options:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Pictures:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Start Date:");
                    EndDateLabel.InnerText = SepFunctions.LangText("End Date:");
                    NewsHeadlineLabel.InnerText = SepFunctions.LangText("News Headline:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    TopicNameRequired.ErrorMessage = SepFunctions.LangText("~~Topic Name~~ is required.");
                    NewsHeadlineRequired.ErrorMessage = SepFunctions.LangText("~~News Headline~~ is required.");
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
            TranslatePage();

            GlobalVars.ModuleID = 23;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("NewsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("NewsAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("NewsID")))
            {
                var jNews = SepCommon.DAL.News.News_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("NewsID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jNews.NewsID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~News~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit News");
                    NewsID.Value = SepCommon.SepCore.Request.Item("NewsID");
                    Pictures.ContentID = SepCommon.SepCore.Request.Item("NewsID");
                    Category.CatID = Strings.ToString(jNews.CatID);
                    TopicName.Value = jNews.Topic;
                    AdditionalOptions.Value = jNews.DisplayType;
                    Portal.Text = Strings.ToString(jNews.PortalID);
                    NewsHeadline.Value = jNews.Headline;
                    NewsStory.Text = jNews.Message;
                    StartDate.Value = Strings.FormatDateTime(jNews.Start_Date, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(jNews.End_Date, Strings.DateNamedFormat.ShortDate);

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("NewsID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                    Portal.Text = Request.Form["Portal"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(NewsID.Value)) NewsID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    Pictures.ContentID = NewsID.Value;
                    StartDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 1, DateTime.Today), Strings.DateNamedFormat.ShortDate);
                }
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
            var intReturn = SepCommon.DAL.News.News_Save(SepFunctions.toLong(NewsID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), TopicName.Value, NewsHeadline.Value, NewsStory.Text, AdditionalOptions.Value, SepFunctions.toDate(StartDate.Value), SepFunctions.toDate(EndDate.Value), SepFunctions.toLong(Request.Form["Portal"]));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}