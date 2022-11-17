// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_questions_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class elearning_questions_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_questions_modify : Page
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
                    QuestionType.Items[0].Text = SepFunctions.LangText("Multiple choice");
                    QuestionType.Items[1].Text = SepFunctions.LangText("Multiple choice /w footer");
                    QuestionType.Items[2].Text = SepFunctions.LangText("Multiple choice fill in the blank");
                    QuestionType.Items[3].Text = SepFunctions.LangText("Correct the sentence");
                    QuestionType.Items[4].Text = SepFunctions.LangText("Long abbreviation");
                    QuestionType.Items[5].Text = SepFunctions.LangText("Short abbreviation");
                    QuestionType.Items[6].Text = SepFunctions.LangText("Fill in the blank");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Question");
                    QuestionTypeLabel.InnerText = SepFunctions.LangText("Question Type:");
                    QuestionNoLabel.InnerText = SepFunctions.LangText("Question Order:");
                    QuestionHeaderLabel.InnerText = SepFunctions.LangText("Question Header:");
                    QuestionFooterLabel.InnerText = SepFunctions.LangText("Question Footer:");
                    Answer1Label.InnerText = SepFunctions.LangText("Answer 1:");
                    Answer2Label.InnerText = SepFunctions.LangText("Answer 2:");
                    Answer3Label.InnerText = SepFunctions.LangText("Answer 3:");
                    Answer4Label.InnerText = SepFunctions.LangText("Answer 4:");
                    Answer5Label.InnerText = SepFunctions.LangText("Answer 5:");
                    CorrectAnswerLabel.InnerText = SepFunctions.LangText("Correct Answer:");
                    QuestionTypeRequired.ErrorMessage = SepFunctions.LangText("~~Question Type~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BackButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect("elearning_exams_modify.aspx?ExamID=" + ExamID.Value);
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("QuestionID")))
            {
                var jQuestions = SepCommon.DAL.ELearning.Question_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("QuestionID")));

                if (jQuestions.QuestionID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Question~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Question");
                    QuestionID.Value = SepCommon.SepCore.Request.Item("QuestionID");
                    ExamID.Value = Strings.ToString(jQuestions.ExamID);
                    QuestionType.Value = jQuestions.QuestionType;
                    QuestionNo.Value = Strings.ToString(jQuestions.QuestionNo);
                    QuestionHeader.Value = jQuestions.QuestionHeader;
                    QuestionFooter.Value = jQuestions.QuestionFooter;
                    Answer1.Value = jQuestions.Answer1;
                    Answer2.Value = jQuestions.Answer2;
                    Answer3.Value = jQuestions.Answer3;
                    Answer4.Value = jQuestions.Answer4;
                    Answer5.Value = jQuestions.Answer5;
                    CorrectAnswer.Value = jQuestions.RightAnswer;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(QuestionID.Value)) QuestionID.Value = Strings.ToString(SepFunctions.GetIdentity());

                if (!Page.IsPostBack)
                {
                    ExamID.Value = SepCommon.SepCore.Request.Item("ExamID");
                    var dELearningQuestions = SepCommon.DAL.ELearning.GetELearningExamQuestions(SepFunctions.toLong(ExamID.Value));
                    QuestionNo.Value = (dELearningQuestions.Count + Strings.ToString(1));
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
            var intReturn = SepCommon.DAL.ELearning.Question_Save(SepFunctions.toLong(QuestionID.Value), SepFunctions.toLong(ExamID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(QuestionNo.Value), QuestionHeader.Value, QuestionFooter.Value, QuestionType.Value, CorrectAnswer.Value, Answer1.Value, Answer2.Value, Answer3.Value, Answer4.Value, Answer5.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty, "Question");
        }
    }
}