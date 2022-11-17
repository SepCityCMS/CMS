// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forms_questions.aspx.cs" company="SepCity, Inc.">
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
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class forms_questions.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forms_questions : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

        /// <summary>
        /// The has sections
        /// </summary>
        private bool hasSections;

        /// <summary>
        /// Gets the type of the answer.
        /// </summary>
        /// <param name="TypeID">The type identifier.</param>
        /// <returns>System.String.</returns>
        public static string GetAnswerType(string TypeID)
        {
            switch (TypeID)
            {
                case "YN":
                    return "Yes/No";

                case "TF":
                    return "True/False";

                case "CB":
                    return "Checkboxes";

                case "RB":
                    return "Radio Buttons";

                case "LA":
                    return "Long Answer";

                case "FU":
                    return "File Upload";

                case "HE":
                    return "HTML Editor";

                case "DD":
                    return "Dropdown";

                default:
                    return "Short Answer";
            }
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    FilterDoAction.Items[0].Text = SepFunctions.LangText("Select an Action");
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Questions");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("Question");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Question Type");
                    ManageGridView.Columns[4].HeaderText = SepFunctions.LangText("Required");
                    ManageGridView.Columns[5].HeaderText = SepFunctions.LangText("Order");
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
                if (dv != null)
                {
                    dv.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ManageGridView.PageIndex = e.NewPageIndex;
            ManageGridView.DataSource = BindData();
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the RowCommand event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MoveUp" || e.CommandName == "MoveDown")
            {
                var cmdArguments = Strings.Split(Strings.ToString(e.CommandArgument), "||");

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    Array.Resize(ref cmdArguments, 4);

                    var sQuestionID = cmdArguments[0];
                    var sNewWeight = SepFunctions.toLong(cmdArguments[1]);
                    var sFormID = cmdArguments[2];
                    var sSectionID = cmdArguments[3];

                    long UpdateWeight = 0;

                    string[] arrCommands = null;
                    var arrCount = 0;

                    if (e.CommandName == "MoveDown")
                        sNewWeight += 1;
                    else
                        sNewWeight -= 1;

                    using (var cmd = new SqlCommand("SELECT QuestionID,FormID,Weight FROM FormQuestions WHERE QuestionID <> @QuestionID AND FormID=@FormID AND SectionID=@SectionID AND Status <> -1 ORDER BY Weight, QuestionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@QuestionID", sQuestionID);
                        cmd.Parameters.AddWithValue("@FormID", sFormID);
                        cmd.Parameters.AddWithValue("@SectionID", sSectionID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                UpdateWeight += 1;
                                if (UpdateWeight == sNewWeight)
                                    UpdateWeight += 1;
                                Array.Resize(ref arrCommands, arrCount + 1);
                                arrCommands[arrCount] = "UPDATE FormQuestions SET Weight='" + SepFunctions.FixWord(Strings.ToString(UpdateWeight)) + "' WHERE QuestionID='" + SepFunctions.openNull(RS["QuestionID"], true) + "' AND Status <> -1";
                                arrCount += 1;
                            }

                        }
                    }

                    Array.Resize(ref arrCommands, arrCount + 1);
                    if (arrCommands != null)
                    {
                        arrCommands[arrCount] = "UPDATE FormQuestions SET Weight='" + SepFunctions.FixWord(Strings.ToString(sNewWeight)) + "' WHERE QuestionID='" + SepFunctions.FixWord(sQuestionID) + "' AND Status <> -1";
                        arrCount += 1;

                        for (var i = 0; i <= Information.UBound(arrCommands); i++)
                            using (var cmd = new SqlCommand(arrCommands[i], conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                    }
                }

                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }
        }

        /// <summary>
        /// Handles the Click event of the ModuleSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ModuleSearchButton_Click(object sender, EventArgs e)
        {
            DeleteResult.InnerHtml = string.Empty;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your search has returned no results.") + "</div>";
                PageManageGridView.Visible = false;
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

            var sInstallFolder = SepFunctions.GetInstallFolder();

            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));
            List<FormsSections> jSections = SepCommon.DAL.Forms.GetFormsSections();
            if (jSections.Count > 0)
            {
                hasSections = true;
                var str = new StringBuilder();
                str.AppendLine("<li class=\"nav-item\" role=\"presentation\" id =\"tabNoSection\"" + Strings.ToString(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SectionID")) == 0 ? " class=\"active\"" : string.Empty) + ">");
                str.AppendLine("<a class=\"nav-link\" href=\"" + sInstallFolder + "spadmin/forms_questions.aspx?FormID=" + SepCommon.SepCore.Request.Item("FormID") + "&SectionID=0&ModuleID=" + SepCommon.SepCore.Request.Item("ModuleID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">Not on a Section</a>");
                str.AppendLine("</li>");
                for (var i = 0; i <= jSections.Count - 1; i++)
                {
                    str.AppendLine("<li class=\"nav-item\" role=\"presentation\" id =\"tab" + jSections[i].SectionID + "\"" + Strings.ToString(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SectionID")) == jSections[i].SectionID ? " class=\"active\"" : string.Empty) + ">");
                    str.AppendLine("<a class=\"nav-link\" href=\"" + sInstallFolder + "spadmin/forms_questions.aspx?FormID=" + SepCommon.SepCore.Request.Item("FormID") + "&SectionID=" + jSections[i].SectionID + "&ModuleID=" + SepCommon.SepCore.Request.Item("ModuleID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + jSections[i].SectionName + "</a>");
                    str.AppendLine("</li>");
                }

                FormSections.InnerHtml = Strings.ToString(str);
            }

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No questions have been added.") + "</div>";
                PageManageGridView.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the RunAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RunAction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterDoAction.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                return;
            }

            var sIDs = UniqueIDs.Value;

            if (Strings.Len(sIDs) > 0)
            {
                try
                {
                    var sDeleteResult = SepCommon.DAL.Forms.Question_Delete(sIDs);
                    DeleteResult.InnerHtml = Strings.InStr(sDeleteResult, SepFunctions.LangText("Successfully")) > 0 ? "<div class=\"alert alert-success\" role=\"alert\">" + sDeleteResult + "</div>" : "<div class=\"alert alert-danger\" role=\"alert\">" + sDeleteResult + "</div>";
                }
                catch
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dForms = SepCommon.DAL.Forms.GetFormsQuestions(searchWords: ModuleSearch.Value, sectionId: Convert.ToInt64(hasSections ? SepFunctions.toLong(SepCommon.SepCore.Request.Item("SectionID")) : -1), FormID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));

            dv = new DataView(SepFunctions.ListToDataTable(dForms));
            return dv;
        }
    }
}