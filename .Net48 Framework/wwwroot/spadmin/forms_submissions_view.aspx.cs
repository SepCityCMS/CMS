// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forms_submissions_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using SepControls;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class forms_submissions_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forms_submissions_view : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText(string.Empty);
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ForumsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ForumsAdmin"), true) == false)
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

            var jForms = SepCommon.DAL.Forms.Form_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));

            if (jForms.FormID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Form~~ does not exist.") + "</div>";
                ModFormDiv.Visible = false;
            }
            else
            {
                SubmissionID.Value = SepCommon.SepCore.Request.Item("SubmissionID");
                FormID.Value = SepCommon.SepCore.Request.Item("FormID");
                ModifyLegend.InnerHtml = jForms.Title;

                var jAnswerEmail = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), 0);

                EmailAddress.Value = jAnswerEmail.Answer;

                var dFormsSections = SepCommon.DAL.Forms.GetFormsSections();
                if (dFormsSections.Count > 0)
                    for (var m = 0; m <= dFormsSections.Count - 1; m++)
                    {
                        var pSection = new HtmlGenericControl("h6");
                        pSection.Style.Add("margin-top", "10px");
                        pSection.Style.Add("margin-bottom", "5px");
                        pSection.InnerHtml = dFormsSections[m].SectionName;
                        QuestionsPanel.Controls.Add(pSection);
                        var dFormsQuestions = SepCommon.DAL.Forms.GetFormsQuestions(sectionId: dFormsSections[m].SectionID);
                        for (var i = 0; i <= dFormsQuestions.Count - 1; i++)
                            using (var pRow = new HtmlGenericControl("p"))
                            {
                                var lblDynamic = new Label
                                {
                                    Text = dFormsQuestions[i].Question,
                                    ID = "Question" + dFormsQuestions[i].QuestionID + "Label",
                                    AssociatedControlID = "txtQuestion" + dFormsQuestions[i].QuestionID
                                };
                                pRow.Controls.Add(lblDynamic);

                                switch (dFormsQuestions[i].TypeID)
                                {
                                    case "YN":
                                        using (var txtDynamic = new DropDownList())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            txtDynamic.CssClass = "form-control";
                                            txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("Yes"), "true"));
                                            txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("No"), "false"));
                                            var jAnswer = new SepCommon.Models.FormsAnswers();
                                            txtDynamic.SelectedValue = jAnswer.Answer;
                                            jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "TF":
                                        using (var txtDynamic = new DropDownList())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            txtDynamic.CssClass = "form-control";
                                            txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("True"), "true"));
                                            txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("False"), "false"));
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.SelectedValue = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "CB":
                                        using (var txtDynamic = new CheckBoxList())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            var dFormsQuestionsOptions = SepCommon.DAL.Forms.GetFormsQuestionsOptions(dFormsQuestions[i].QuestionID);
                                            for (var j = 0; j <= dFormsQuestionsOptions.Count - 1; j++) txtDynamic.Items.Add(new ListItem(dFormsQuestionsOptions[j].OptionValue, dFormsQuestionsOptions[j].OptionValue));
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.SelectedValue = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "RB":
                                        using (var txtDynamic = new RadioButtonList())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            var dFormsQuestionsOptions = SepCommon.DAL.Forms.GetFormsQuestionsOptions(dFormsQuestions[i].QuestionID);
                                            for (var j = 0; j <= dFormsQuestionsOptions.Count - 1; j++) txtDynamic.Items.Add(new ListItem(dFormsQuestionsOptions[j].OptionValue, dFormsQuestionsOptions[j].OptionValue));
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.SelectedValue = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "SA":
                                        using (var txtDynamic = new TextBox())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            txtDynamic.CssClass = "form-control";
                                            txtDynamic.MaxLength = 50;
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.Text = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "LA":
                                        using (var txtDynamic = new TextBox())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            txtDynamic.CssClass = "form-control";
                                            txtDynamic.TextMode = TextBoxMode.MultiLine;
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.Text = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "FU":
                                        using (var txtDynamic = new FileUpload())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            txtDynamic.CssClass = "form-control";
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "HE":
                                        using (var txtDynamic = new WYSIWYGEditor())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.Text = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;

                                    case "DD":
                                        using (var txtDynamic = new DropDownList())
                                        {
                                            txtDynamic.ID = "txtQuestion" + dFormsQuestions[i].QuestionID;
                                            txtDynamic.CssClass = "form-control";
                                            var dFormsQuestionsOptions = SepCommon.DAL.Forms.GetFormsQuestionsOptions(dFormsQuestions[i].QuestionID);
                                            for (var j = 0; j <= dFormsQuestionsOptions.Count - 1; j++) txtDynamic.Items.Add(new ListItem(dFormsQuestionsOptions[j].OptionValue, dFormsQuestionsOptions[j].OptionValue));
                                            var jAnswer = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions[i].QuestionID);
                                            txtDynamic.SelectedValue = jAnswer.Answer;
                                            pRow.Controls.Add(txtDynamic);
                                        }

                                        QuestionsPanel.Controls.Add(pRow);
                                        break;
                                }
                            }
                    }

                var dFormsQuestions2 = SepCommon.DAL.Forms.GetFormsQuestions(sectionId: 0);
                for (var i = 0; i <= dFormsQuestions2.Count - 1; i++)
                {
                    var pRow = new HtmlGenericControl("p");

                    using (var lblDynamic = new Label())
                    {
                        lblDynamic.Text = dFormsQuestions2[i].Question;
                        lblDynamic.ID = "Question" + dFormsQuestions2[i].QuestionID + "Label";
                        lblDynamic.AssociatedControlID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                        pRow.Controls.Add(lblDynamic);
                    }

                    switch (dFormsQuestions2[i].TypeID)
                    {
                        case "YN":
                            using (var txtDynamic = new DropDownList())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                txtDynamic.CssClass = "form-control";
                                txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("Yes"), "true"));
                                txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("No"), "false"));
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.SelectedValue = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "TF":
                            using (var txtDynamic = new DropDownList())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                txtDynamic.CssClass = "form-control";
                                txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("True"), "true"));
                                txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("False"), "false"));
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.SelectedValue = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "CB":
                            using (var txtDynamic = new CheckBoxList())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                var dFormsQuestionsOptions = SepCommon.DAL.Forms.GetFormsQuestionsOptions(dFormsQuestions2[i].QuestionID);
                                for (var j = 0; j <= dFormsQuestionsOptions.Count - 1; j++) txtDynamic.Items.Add(new ListItem(dFormsQuestionsOptions[j].OptionValue, dFormsQuestionsOptions[j].OptionValue));
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.SelectedValue = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "RB":
                            using (var txtDynamic = new RadioButtonList())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                var dFormsQuestionsOptions = SepCommon.DAL.Forms.GetFormsQuestionsOptions(dFormsQuestions2[i].QuestionID);
                                for (var j = 0; j <= dFormsQuestionsOptions.Count - 1; j++) txtDynamic.Items.Add(new ListItem(dFormsQuestionsOptions[j].OptionValue, dFormsQuestionsOptions[j].OptionValue));
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.SelectedValue = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "SA":
                            using (var txtDynamic = new TextBox())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                txtDynamic.CssClass = "form-control";
                                txtDynamic.MaxLength = 50;
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.Text = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "LA":
                            using (var txtDynamic = new TextBox())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                txtDynamic.CssClass = "form-control";
                                txtDynamic.TextMode = TextBoxMode.MultiLine;
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.Text = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "FU":
                            using (var txtDynamic = new FileUpload())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                txtDynamic.CssClass = "form-control";
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "HE":
                            using (var txtDynamic = new WYSIWYGEditor())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.Text = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;

                        case "DD":
                            using (var txtDynamic = new DropDownList())
                            {
                                txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                                txtDynamic.CssClass = "form-control";
                                var dFormsQuestionsOptions = SepCommon.DAL.Forms.GetFormsQuestionsOptions(dFormsQuestions2[i].QuestionID);
                                for (var j = 0; j <= dFormsQuestionsOptions.Count - 1; j++) txtDynamic.Items.Add(new ListItem(dFormsQuestionsOptions[j].OptionValue, dFormsQuestionsOptions[j].OptionValue));
                                var jAnswer2 = SepCommon.DAL.Forms.Answer_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SubmissionID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")), dFormsQuestions2[i].QuestionID);
                                txtDynamic.Text = jAnswer2.Answer;
                                pRow.Controls.Add(txtDynamic);
                            }

                            QuestionsPanel.Controls.Add(pRow);
                            break;
                    }

                    pRow.Dispose();
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
            var jForms = SepCommon.DAL.Forms.Form_Get(SepFunctions.toLong(FormID.Value));

            if (jForms.FormID == 0)
            {
                var ctrls = Strings.ToString(Request.Form).Split('&');
                var dFormsQuestions = SepCommon.DAL.Forms.GetFormsQuestions();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE FormAnswers SET Answer=@Answer WHERE SubmissionID=@SubmissionID AND FormID=@FormID AND QuestionID=@QuestionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SubmissionID", SubmissionID.Value);
                        cmd.Parameters.AddWithValue("@FormID", FormID.Value);
                        cmd.Parameters.AddWithValue("@QuestionID", 0);
                        cmd.Parameters.AddWithValue("@Answer", EmailAddress.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                for (var l = 0; l <= ctrls.Length - 1; l++)
                {
                    string ctrlValue = string.Empty;
                    long strQuestionID = 0;
                    for (var i = 0; i <= dFormsQuestions.Count - 1; i++)
                        if (ctrls[l].Contains("txtQuestion" + dFormsQuestions[i].QuestionID))
                        {
                            strQuestionID = dFormsQuestions[i].QuestionID;
                            ctrlValue = Strings.Replace(ctrls[l].Split('=')[1], "+", " ");
                            break;
                        }

                    if (!string.IsNullOrWhiteSpace(ctrlValue))
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE FormAnswers SET Answer=@Answer WHERE SubmissionID=@SubmissionID AND FormID=@FormID AND QuestionID=@QuestionID", conn))
                            {
                                cmd.Parameters.AddWithValue("@SubmissionID", SubmissionID.Value);
                                cmd.Parameters.AddWithValue("@FormID", FormID.Value);
                                cmd.Parameters.AddWithValue("@QuestionID", strQuestionID);
                                cmd.Parameters.AddWithValue("@Answer", ctrlValue);
                                cmd.ExecuteNonQuery();
                            }
                        }
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You form has been successfully saved.") + "</div>";
        }
    }
}