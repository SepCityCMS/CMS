// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_assignments_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class elearning_assignments_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_assignments_modify : Page
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
                    CourseID.Items[0].Text = SepFunctions.LangText("Select a Course");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Assignment");
                    CourseIDLabel.InnerText = SepFunctions.LangText("Select a Course:");
                    VideoFileLabel.InnerText = SepFunctions.LangText("Select a Presentation Video:");
                    TitleLabel.InnerText = SepFunctions.LangText("Title:");
                    DueDateLabel.InnerText = SepFunctions.LangText("Due Date:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    AttachmentLabel.InnerText = SepFunctions.LangText("Assignment Attachment:");
                    CourseIDRequired.ErrorMessage = SepFunctions.LangText("~~Course~~ is required.");
                    TitleRequired.ErrorMessage = SepFunctions.LangText("~~Title~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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

            GlobalVars.ModuleID = 37;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ELearningAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ELearningAdmin"), true) == false)
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

            if (!Page.IsPostBack)
            {
                var dELearningCourses = SepCommon.DAL.ELearning.GetELearningCourses();
                for (var i = 0; i <= dELearningCourses.Count - 1; i++) CourseID.Items.Add(new ListItem(dELearningCourses[i].CourseName, Strings.ToString(dELearningCourses[i].CourseID)));
                CourseID.Value = SepCommon.SepCore.Request.Item("CourseID");
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AssignmentID")))
            {
                var jAssignments = SepCommon.DAL.ELearning.Assignment_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AssignmentID")));

                if (jAssignments.AssignmentID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Assignment~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Assignment");
                    HomeID.Value = SepCommon.SepCore.Request.Item("AssignmentID");
                    CourseID.Value = Strings.ToString(jAssignments.CourseID);
                    CourseTitle.Value = jAssignments.Title;
                    DueDate.Value = jAssignments.DueDate.ToShortDateString();
                    Description.Value = jAssignments.Description;
                    VideoFile.ContentID = SepCommon.SepCore.Request.Item("AssignmentID");
                    Attachment.ContentID = SepCommon.SepCore.Request.Item("AssignmentID");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(HomeID.Value)) HomeID.Value = Strings.ToString(SepFunctions.GetIdentity());
                VideoFile.ContentID = HomeID.Value;
                Attachment.ContentID = HomeID.Value;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.ELearning.Assignment_Save(SepFunctions.toLong(HomeID.Value), SepFunctions.toLong(CourseID.Value), SepFunctions.Session_User_ID(), CourseTitle.Value, SepFunctions.toDate(DueDate.Value), Description.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty, "Assignment");
        }
    }
}