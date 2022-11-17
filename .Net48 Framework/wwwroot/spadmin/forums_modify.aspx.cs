// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forums_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class forums_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forums_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Topic");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    SubjectLabel.InnerText = SepFunctions.LangText("Subject:");
                    AttachmentLabel.InnerText = SepFunctions.LangText("Add an Attachment:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    SubjectRequired.ErrorMessage = SepFunctions.LangText("~~Subject~~ is required.");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 12;

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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TopicID")))
            {
                var jForums = SepCommon.DAL.Forums.Topic_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("TopicID")));

                if (jForums.TopicID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Topic~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Topic");
                    TopicID.Value = SepCommon.SepCore.Request.Item("TopicID");
                    Category.CatID = Strings.ToString(jForums.CatID);
                    Portal.Text = Strings.ToString(jForums.PortalID);
                    Subject.Value = jForums.Subject;
                    if (jForums.EmailReply) EmailReply.Checked = true;
                    Message.Text = jForums.Message;
                    ReplyID.Value = Strings.ToString(jForums.ReplyID);
                    if (!string.IsNullOrWhiteSpace(jForums.Attachment))
                    {
                        AttachmentLabel.InnerText = SepFunctions.LangText("Replace file attachment");
                        FileAttachment.Visible = true;
                        FileAttachment.InnerHtml = "<a href=\"" + sInstallFolder + "download_attachment.aspx?ModuleID=12&UniqueID=" + SepCommon.SepCore.Request.Item("TopicID") + "\" target=\"_blank\">" + jForums.Attachment + "</a>";
                    }
                    else
                    {
                        FileAttachment.Visible = false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TopicID.Value)) TopicID.Value = Strings.ToString(SepFunctions.GetIdentity());
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (string.IsNullOrWhiteSpace(Portal.Text))
            {
                Portal.Text = Strings.ToString(SepFunctions.Get_Portal_ID());
            }
            var intReturn = SepCommon.DAL.Forums.Topic_Save(SepFunctions.toLong(TopicID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), SepFunctions.toLong(ReplyID.Value), EmailReply.Checked, Subject.Value, Message.Text, Attachment, SepFunctions.toLong(Portal.Text));

            if (Attachment.PostedFile == null || string.IsNullOrWhiteSpace(Attachment.PostedFile.FileName))
            {
            }
            else
            {
                FileAttachment.Visible = true;
                FileAttachment.InnerHtml = "<a href=\"" + sInstallFolder + "download_attachment.aspx?ModuleID=12&UniqueID=" + TopicID.Value + "\" target=\"_blank\">" + Attachment.PostedFile.FileName + "</a>";
                AttachmentLabel.InnerText = SepFunctions.LangText("Replace file attachment");
            }

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}