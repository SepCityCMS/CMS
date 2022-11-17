// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="blogs_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class blogs_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class blogs_modify : Page
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
                    AllowComments.Items[0].Text = SepFunctions.LangText("Yes");
                    AllowComments.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Blog");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    BlogNameLabel.InnerText = SepFunctions.LangText("Blog Name:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Start Date:");
                    EndDateLabel.InnerText = SepFunctions.LangText("End Date:");
                    AllowCommentsLabel.InnerText = SepFunctions.LangText("Allow other users to leave you comments on this blog:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    BlogNameRequired.ErrorMessage = SepFunctions.LangText("~~Blog Name~~ is required.");
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

            GlobalVars.ModuleID = 61;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("BlogsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("BlogsAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("BlogID")))
            {
                var jBlogs = SepCommon.DAL.Blogs.Blog_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("BlogID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jBlogs.BlogID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Blog~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Blog");
                    BlogID.Value = SepCommon.SepCore.Request.Item("BlogID");
                    Category.CatID = Strings.ToString(jBlogs.CatID);
                    BlogName.Value = jBlogs.BlogName;
                    BlogText.Text = jBlogs.Message;
                    AllowComments.Value = Strings.ToString(jBlogs.Comments);
                    StartDate.Value = Strings.FormatDateTime(jBlogs.StartDate, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(jBlogs.EndDate, Strings.DateNamedFormat.ShortDate);

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("BlogID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");
                }

                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(BlogID.Value)) BlogID.Value = Strings.ToString(SepFunctions.GetIdentity());
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
            var intReturn = SepCommon.DAL.Blogs.Blog_Save(SepFunctions.toLong(BlogID.Value), SepFunctions.toLong(Category.CatID), SepFunctions.Session_User_ID(), BlogName.Value, BlogText.Text, SepFunctions.toBoolean(AllowComments.Value), SepFunctions.toDate(StartDate.Value), SepFunctions.toDate(EndDate.Value), SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}