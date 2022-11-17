// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forms_questions_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class forms_questions_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forms_questions_modify : Page
    {
        /// <summary>
        /// Adds the options row.
        /// </summary>
        /// <param name="iNum">The i number.</param>
        /// <param name="OptionID">The option identifier.</param>
        /// <param name="txtValue">The text value.</param>
        /// <param name="addMode">if set to <c>true</c> [add mode].</param>
        public void Add_Options_Row(int iNum, string OptionID, string txtValue, bool addMode)
        {
            var pRow = new HtmlGenericControl("p")
            {
                ID = "Option" + iNum + "Row",
                ClientIDMode = ClientIDMode.Static
            };
            if (addMode)
                pRow.Style.Add("display", "none");

            using (var lblDynamic = new Label())
            {
                lblDynamic.Text = "Option " + iNum;
                lblDynamic.ID = "Option" + iNum + "Label";
                lblDynamic.AssociatedControlID = "Option" + iNum;
                pRow.Controls.Add(lblDynamic);
            }

            using (var txtDynamic = new TextBox())
            {
                txtDynamic.ID = "Option" + iNum;
                txtDynamic.CssClass = "form-control";
                txtDynamic.MaxLength = 100;
                txtDynamic.Text = txtValue;
                pRow.Controls.Add(txtDynamic);
            }

            using (var hdnDynamic = new HiddenField())
            {
                hdnDynamic.ID = "OptionID" + iNum;
                hdnDynamic.Value = OptionID;
                pRow.Controls.Add(hdnDynamic);
            }

            OptionPanel.Controls.Add(pRow);
        }

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
        /// Loads the custom options.
        /// </summary>
        public void Load_Custom_Options()
        {
            var cOptions = JsonConvert.DeserializeObject<IList<FormsQuestionsOptions>>(JsonConvert.SerializeObject(SepCommon.DAL.Forms.GetFormsQuestionsOptions(SepFunctions.toLong(QuestionID.Value))));

            for (var i = 0; i <= cOptions.Count - 1; i++)
            {
                NumOptions.Value = Strings.ToString(SepFunctions.toInt(NumOptions.Value) + 1);
                Add_Options_Row(i + 1, Strings.ToString(cOptions[i].OptionID), cOptions[i].OptionValue, false);
            }
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
                    QuestionType.Items[0].Text = SepFunctions.LangText("Yes/No");
                    QuestionType.Items[1].Text = SepFunctions.LangText("True/False");
                    QuestionType.Items[2].Text = SepFunctions.LangText("Checkboxes");
                    QuestionType.Items[3].Text = SepFunctions.LangText("Radio Buttons");
                    QuestionType.Items[4].Text = SepFunctions.LangText("Short Answer");
                    QuestionType.Items[5].Text = SepFunctions.LangText("Long Answer");
                    QuestionType.Items[6].Text = SepFunctions.LangText("File Upload");
                    QuestionType.Items[7].Text = SepFunctions.LangText("HTML Editor");
                    QuestionType.Items[8].Text = SepFunctions.LangText("Dropdown");
                    Required.Items[0].Text = SepFunctions.LangText("Yes");
                    Required.Items[1].Text = SepFunctions.LangText("No");
                    SectionID.Items[0].Text = SepFunctions.LangText("None");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Question");
                    QuestionTypeLabel.InnerText = SepFunctions.LangText("Question Type:");
                    RequiredLabel.InnerText = SepFunctions.LangText("Required:");
                    SectionIDLabel.InnerText = SepFunctions.LangText("List question under the following section:");
                    QuestionLabel.InnerText = SepFunctions.LangText("Question Text:");
                    QuestionTypeRequired.ErrorMessage = SepFunctions.LangText("~~Question Type~~ is required.");
                    RequiredRequired.ErrorMessage = SepFunctions.LangText("~~Required~~ is required.");
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

            if (!Page.IsPostBack)
            {
                var dFormsSections = SepCommon.DAL.Forms.GetFormsSections();

                if (dFormsSections.Count > 0)
                    for (var i = 0; i <= dFormsSections.Count - 1; i++)
                        SectionID.Items.Add(new ListItem(dFormsSections[i].SectionName, Strings.ToString(dFormsSections[i].SectionID)));
                else SectionRow.Visible = false;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("QuestionID")))
            {
                var jFormsQuestions = SepCommon.DAL.Forms.Question_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("QuestionID")));

                if (jFormsQuestions.QuestionID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Question~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Question");
                    QuestionID.Value = SepCommon.SepCore.Request.Item("QuestionID");
                    FormID.Value = Strings.ToString(jFormsQuestions.FormID);
                    QuestionType.Value = jFormsQuestions.TypeID;
                    Required.Value = Strings.ToString(jFormsQuestions.Mandatory);
                    SectionID.Value = Strings.ToString(jFormsQuestions.SectionID);
                    Question.Text = jFormsQuestions.Question;
                }

                Load_Custom_Options();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    for (var i = 1; i <= 5; i++) Add_Options_Row(i, Strings.ToString(SepFunctions.GetIdentity()), string.Empty, true);
                    NumOptions.Value = "5";

                    if (string.IsNullOrWhiteSpace(QuestionID.Value)) QuestionID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    FormID.Value = SepCommon.SepCore.Request.Item("FormID");
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
            var intReturn = SepCommon.DAL.Forms.Question_Save(SepFunctions.toLong(QuestionID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(FormID.Value), SepFunctions.toLong(SectionID.Value), QuestionType.Value, "0", SepFunctions.toBoolean(Required.Value), Question.Text);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty, "Question");

            for (var i = 1; i <= SepFunctions.toInt(SepCommon.SepCore.Request.Item("ctl00$MainContent$NumOptions")); i++)
            {
                var sValue = SepCommon.SepCore.Request.Item("ctl00$MainContent$Option" + i);
                if (!string.IsNullOrWhiteSpace(sValue)) SepCommon.DAL.Forms.Question_Option_Save(SepFunctions.toLong(Strings.Left(SepCommon.SepCore.Request.Item("ctl00$MainContent$OptionID" + i), 15)), SepFunctions.toLong(QuestionID.Value), sValue);
            }

            Load_Custom_Options();
        }
    }
}