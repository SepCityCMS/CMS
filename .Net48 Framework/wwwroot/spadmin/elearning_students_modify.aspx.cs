// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_students_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class elearning_students_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_students_modify : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv assignments
        /// </summary>
        private DataView dvAssignments = new DataView();

        /// <summary>
        /// The dv exams
        /// </summary>
        private DataView dvExams = new DataView();

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
                    CourseID.Items[0].Text = SepFunctions.LangText("Select a Course");
                    Active.Items[0].Text = SepFunctions.LangText("Yes");
                    Active.Items[1].Text = SepFunctions.LangText("No");
                    AssignmentGrid.Columns[0].HeaderText = SepFunctions.LangText("Title");
                    AssignmentGrid.Columns[1].HeaderText = SepFunctions.LangText("Description");
                    AssignmentGrid.Columns[2].HeaderText = SepFunctions.LangText("Date Submitted");
                    AssignmentGrid.Columns[3].HeaderText = SepFunctions.LangText("Due Date");
                    AssignmentGrid.Columns[4].HeaderText = SepFunctions.LangText("Grade");
                    ExamGrid.Columns[0].HeaderText = SepFunctions.LangText("Exam Name");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Student");
                    CourseIDLabel.InnerText = SepFunctions.LangText("Select a Course:");
                    UserNameLabel.InnerText = SepFunctions.LangText("User Name:");
                    DateEnrolledLabel.InnerText = SepFunctions.LangText("Date Enrolled:");
                    ActiveLabel.InnerText = SepFunctions.LangText("Active:");
                    UserNotesLabel.InnerText = SepFunctions.LangText("User Notes:");
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
                if (dvExams != null)
                {
                    dvExams.Dispose();
                }

                if (dvAssignments != null)
                {
                    dvAssignments.Dispose();
                }
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

                dvAssignments = BindDataAssignments();
                AssignmentGrid.DataSource = dvAssignments;
                AssignmentGrid.DataBind();

                dvExams = BindDataExams();
                ExamGrid.DataSource = dvExams;
                ExamGrid.DataBind();
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("StudentID")))
            {
                var jStudents = SepCommon.DAL.ELearning.Student_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("StudentID")));

                if (jStudents.StudentID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Student~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Student");
                    StudentID.Value = SepCommon.SepCore.Request.Item("StudentID");
                    CourseID.Value = Strings.ToString(jStudents.CourseID);
                    UserID.Value = jStudents.UserID;
                    UserName.Value = jStudents.UserName;
                    DateEnrolled.Value = Strings.ToString(jStudents.DateEnrolled);
                    Active.Value = Strings.ToString(jStudents.Active);
                    UserNotes.Value = jStudents.UserNotes;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(StudentID.Value)) StudentID.Value = Strings.ToString(SepFunctions.GetIdentity());

                if (!Page.IsPostBack)
                {
                    CourseID.Value = SepCommon.SepCore.Request.Item("CourseID");
                    UserID.Value = SepCommon.SepCore.Request.Item("UserID");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            int intReturn;
            if (Active.Value == "1")
                intReturn = SepCommon.DAL.ELearning.Student_Save(SepFunctions.toLong(StudentID.Value), SepFunctions.toLong(CourseID.Value), UserID.Value, true, SepFunctions.toDate(DateEnrolled.Value), UserNotes.Value);
            else
                intReturn = SepCommon.DAL.ELearning.Student_Save(SepFunctions.toLong(StudentID.Value), SepFunctions.toLong(CourseID.Value), UserID.Value, false, SepFunctions.toDate(DateEnrolled.Value), UserNotes.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty, "Student");
        }

        /// <summary>
        /// Binds the data assignments.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindDataAssignments()
        {
            var dELearningAssignments = SepCommon.DAL.ELearning.GetELearningAssignments(StudentID: SepFunctions.toLong(StudentID.Value));

            dvAssignments = new DataView(SepFunctions.ListToDataTable(dELearningAssignments));
            return dvAssignments;
        }

        /// <summary>
        /// Binds the data exams.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindDataExams()
        {
            var dELearningExams = SepCommon.DAL.ELearning.GetELearningExams(StudentID: SepFunctions.toLong(StudentID.Value));

            dvExams = new DataView(SepFunctions.ListToDataTable(dELearningExams));
            return dvExams;
        }
    }
}