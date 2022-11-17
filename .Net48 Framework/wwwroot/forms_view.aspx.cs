// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forms_view.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class forms_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forms_view : Page
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

            GlobalVars.ModuleID = 13;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "FormsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("FormsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var jForms = SepCommon.DAL.Forms.Form_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));

            if (jForms.FormID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Form~~ does not exist.") + "</div>";
                ModFieldset.Visible = false;
            }
            else
            {
                if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostSubmitForm", "PostSubmitForm", FormID.Value, false) == false)
                {
                    SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                    return;
                }

                FormID.Value = SepCommon.SepCore.Request.Item("FormID");
                ModifyLegend.InnerHtml = jForms.Title;
                EmailAddress.Value = SepFunctions.GetUserInformation("EmailAddress");
                PageText.InnerHtml = jForms.Description;
            }

            var dFormsSections = SepCommon.DAL.Forms.GetFormsSections(FormId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));
            if (dFormsSections.Count > 0)
                for (var m = 0; m <= dFormsSections.Count - 1; m++)
                {
                    var pSection = new HtmlGenericControl("div");
                    pSection.Attributes.Add("class", "FormSection");
                    pSection.InnerHtml = dFormsSections[m].SectionName;
                    QuestionsPanel.Controls.Add(pSection);
                    var dFormsQuestions = SepCommon.DAL.Forms.GetFormsQuestions(sectionId: dFormsSections[m].SectionID, FormID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));
                    for (var i = 0; i <= dFormsQuestions.Count - 1; i++)
                    {
                        var pRow = new HtmlGenericControl("div");
                        pRow.Attributes.Add("class", "mb-3");

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
                                    txtDynamic.RelativeURLs = true;
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
                                    pRow.Controls.Add(txtDynamic);
                                }

                                QuestionsPanel.Controls.Add(pRow);
                                break;
                        }

                        pRow.Dispose();
                    }
                }

            var dFormsQuestions2 = SepCommon.DAL.Forms.GetFormsQuestions(sectionId: 0, FormID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));
            for (var i = 0; i <= dFormsQuestions2.Count - 1; i++)
            {
                var pRow = new HtmlGenericControl("div");
                pRow.Attributes.Add("class", "mb-3");

                var lblDynamic = new Label
                {
                    Text = dFormsQuestions2[i].Question,
                    ID = "Question" + dFormsQuestions2[i].QuestionID + "Label",
                    AssociatedControlID = "txtQuestion" + dFormsQuestions2[i].QuestionID
                };
                pRow.Controls.Add(lblDynamic);

                switch (dFormsQuestions2[i].TypeID)
                {
                    case "YN":
                        using (var txtDynamic = new DropDownList())
                        {
                            txtDynamic.ID = "txtQuestion" + dFormsQuestions2[i].QuestionID;
                            txtDynamic.CssClass = "form-control";
                            txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("Yes"), "true"));
                            txtDynamic.Items.Add(new ListItem(SepFunctions.LangText("No"), "false"));
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
                            pRow.Controls.Add(txtDynamic);
                        }

                        QuestionsPanel.Controls.Add(pRow);
                        break;
                }

                pRow.Dispose();
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

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Recaptcha1.Validate() == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid reCaptcha.") + "</div>";
                return;
            }

            var GetAttachment = string.Empty;

            var sSubmissionId = Strings.ToString(SepFunctions.GetIdentity());

            var jForms = SepCommon.DAL.Forms.Form_Get(SepFunctions.toLong(FormID.Value));

            if (jForms.FormID > 0)
            {
                ErrorMessage.InnerHtml = string.Empty;
                PageText.Visible = false;

                string sActDesc = SepFunctions.LangText("Form Submission Successfully Saved") + Environment.NewLine;
                sActDesc += SepFunctions.LangText("Form Name:") + " " + SepFunctions.openNull(jForms.Title) + Environment.NewLine;
                SepFunctions.Activity_Write("SUBMITFORM", sActDesc, GlobalVars.ModuleID);

                string EmailSubject = SepFunctions.LangText("New Submission ~~" + SepFunctions.openNull(jForms.Title) + "~~ Form");
                string EmailBody = SepFunctions.LangText("A new submission has been made to the ~~" + SepFunctions.openNull(jForms.Title) + "~~ Form.") + "<br/><br/>";

                var ctrls = Strings.ToString(Request.Form).Split('&');
                var dFormsQuestions = SepCommon.DAL.Forms.GetFormsQuestions();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    EmailBody += "<b>" + SepFunctions.LangText("Email Address") + ":</b> " + EmailAddress.Value + "<br/>";
                    using (var cmd = new SqlCommand("INSERT INTO FormAnswers (SubmissionID, AnswerID, SubmitDate, FormID, QuestionID, UserID, Answer, Status) VALUES (@SubmissionID, @AnswerID, @SubmitDate, @FormID, @QuestionID, @UserID, @Answer, '1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@SubmissionID", sSubmissionId);
                        cmd.Parameters.AddWithValue("@AnswerID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@SubmitDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@FormID", FormID.Value);
                        cmd.Parameters.AddWithValue("@QuestionID", 0);
                        if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                        {
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@UserID", string.Empty);
                        }

                        cmd.Parameters.AddWithValue("@Answer", EmailAddress.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                for (var l = 0; l <= ctrls.Length - 1; l++)
                {
                    string ctrlValue = string.Empty;
                    string strQuestion = string.Empty;
                    string strQuestionID = string.Empty;
                    for (var i = 0; i <= dFormsQuestions.Count - 1; i++)
                        if (ctrls[l].Contains("txtQuestion" + dFormsQuestions[i].QuestionID))
                        {
                            strQuestion = dFormsQuestions[i].Question;
                            strQuestionID = Strings.ToString(dFormsQuestions[i].QuestionID);
                            ctrlValue = ctrls[l].Split('=')[1];
                            break;
                        }

                    if (!string.IsNullOrWhiteSpace(ctrlValue))
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            EmailBody += "<b>" + SepFunctions.RemoveHTML(strQuestion) + ":</b> " + Server.UrlDecode(ctrlValue) + "<br/>";
                            using (var cmd = new SqlCommand("INSERT INTO FormAnswers (SubmissionID, AnswerID, SubmitDate, FormID, QuestionID, UserID, Answer, Status) VALUES (@SubmissionID, @AnswerID, @SubmitDate, @FormID, @QuestionID, @UserID, @Answer, '1')", conn))
                            {
                                cmd.Parameters.AddWithValue("@SubmissionID", sSubmissionId);
                                cmd.Parameters.AddWithValue("@AnswerID", SepFunctions.GetIdentity());
                                cmd.Parameters.AddWithValue("@SubmitDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@FormID", FormID.Value);
                                cmd.Parameters.AddWithValue("@QuestionID", strQuestionID);
                                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                                {
                                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@UserID", string.Empty);
                                }

                                cmd.Parameters.AddWithValue("@Answer", Server.UrlDecode(ctrlValue));
                                cmd.ExecuteNonQuery();
                            }
                        }
                }

                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Submit", jForms.Title);

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FileName")))
                    GetAttachment = SepFunctions.GetDirValue("attachments") + SepCommon.SepCore.Request.Item("FileName");
                SepFunctions.Send_Email(jForms.Email, EmailAddress.Value, EmailSubject, EmailBody, 13, GetAttachment);
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FileName")))
                    if (File.Exists(GetAttachment))
                        File.Delete(GetAttachment);
                if (!string.IsNullOrWhiteSpace(jForms.CompletionURL))
                {
                    SepFunctions.Redirect(SepFunctions.Format_URL(jForms.CompletionURL));
                    return;
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Form has been successfully submitted.") + "</div>";
            ModFieldset.Visible = false;
            SaveButton.Visible = false;
        }
    }
}