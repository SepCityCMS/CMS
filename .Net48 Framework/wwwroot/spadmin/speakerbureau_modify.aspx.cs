// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="speakerbureau_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class speakerbureau_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class speakerbureau_modify : Page
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
                    NameTitle.Items[0].Text = SepFunctions.LangText("N/A");
                    NameTitle.Items[1].Text = SepFunctions.LangText("Dr.");
                    NameTitle.Items[2].Text = SepFunctions.LangText("Miss");
                    NameTitle.Items[3].Text = SepFunctions.LangText("Mr.");
                    NameTitle.Items[4].Text = SepFunctions.LangText("Mrs.");
                    NameTitle.Items[5].Text = SepFunctions.LangText("Ms.");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Speaker");
                    FirstNameLabel.InnerText = SepFunctions.LangText("First Name:");
                    LastNameLabel.InnerText = SepFunctions.LangText("Last Name:");
                    NameTitleLabel.InnerText = SepFunctions.LangText("Title:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    CredentialsLabel.InnerText = SepFunctions.LangText("Credentials:");
                    BioLabel.InnerText = SepFunctions.LangText("Bio:");
                    PictureLabel.InnerText = SepFunctions.LangText("Picture:");
                    FirstNameRequired.ErrorMessage = SepFunctions.LangText("~~First Name~~ is required.");
                    LastNameRequired.ErrorMessage = SepFunctions.LangText("~~Last Name~~ is required.");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
                    CredentialsRequired.ErrorMessage = SepFunctions.LangText("~~Credentials~~ is required.");
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

            GlobalVars.ModuleID = 50;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("SpeakerAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("SpeakerAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SpeakerID")))
            {
                var jSpeaker = SepCommon.DAL.SpeakersBureau.Speaker_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SpeakerID")));

                if (jSpeaker.SpeakerID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Speaker~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Speaker");
                    SpeakerID.Value = SepCommon.SepCore.Request.Item("SpeakerID");
                    FirstName.Value = jSpeaker.FirstName;
                    LastName.Value = jSpeaker.LastName;
                    NameTitle.Value = jSpeaker.Title;
                    EmailAddress.Value = jSpeaker.EmailAddress;
                    Credentials.Value = jSpeaker.Cred;
                    Bio.Text = jSpeaker.Bio;
                    Picture.ContentID = SepCommon.SepCore.Request.Item("SpeakerID");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(SpeakerID.Value)) SpeakerID.Value = Strings.ToString(SepFunctions.GetIdentity());
                Picture.ContentID = SpeakerID.Value;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.SpeakersBureau.Speaker_Save(SepFunctions.toLong(SpeakerID.Value), SepFunctions.Session_User_ID(), FirstName.Value, LastName.Value, EmailAddress.Value, Credentials.Value, Bio.Text, NameTitle.Value, SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}