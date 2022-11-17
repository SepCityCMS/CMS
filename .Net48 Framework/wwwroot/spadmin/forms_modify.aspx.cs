// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forms_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class forms_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forms_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Form");
                    FormNameLabel.InnerText = SepFunctions.LangText("Form Name:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email address(es) to send form submissions to (Seperate by semicolons):");
                    PageBodyLabel.InnerText = SepFunctions.LangText("Page Body:");
                    CompletionURLLabel.InnerText = SepFunctions.LangText("Completion URL:");
                    FormNameRequired.ErrorMessage = SepFunctions.LangText("~~Form Name~~ is required.");
                    EmailSubjectRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
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

            GlobalVars.ModuleID = 13;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("FormsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("FormsAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FormID")))
            {
                var jForms = SepCommon.DAL.Forms.Form_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FormID")));

                if (jForms.FormID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Form~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Form");
                    FormID.Value = SepCommon.SepCore.Request.Item("FormID");
                    FormName.Value = jForms.Title;
                    Portal.Text = Strings.ToString(jForms.PortalID);
                    EmailAddress.Value = jForms.Email;
                    PageBody.Text = jForms.Description;
                    CompletionURL.Value = jForms.CompletionURL;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (string.IsNullOrWhiteSpace(FormID.Value)) FormID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    Portal.Text = Strings.ToString(SepFunctions.Get_Portal_ID());
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
            if (string.IsNullOrWhiteSpace(Portal.Text))
            {
                Portal.Text = Strings.ToString(SepFunctions.Get_Portal_ID());
            }
            var intReturn = SepCommon.DAL.Forms.Form_Save(SepFunctions.toLong(FormID.Value), FormName.Value, SepFunctions.Session_User_ID(), PageBody.Text, EmailAddress.Value, CompletionURL.Value, SepFunctions.toLong(Portal.Text));

            if (intReturn == 3)
            {
                SepFunctions.Redirect("forms_questions.aspx?FormID=" + FormID.Value + "&ModuleID=13&PortalID=" + SepFunctions.Get_Portal_ID());
            }
            else
            {
                ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
            }
        }
    }
}