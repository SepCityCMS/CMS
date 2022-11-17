// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="speakerbureau_topics_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class speakerbureau_topics_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class speakerbureau_topics_modify : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

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
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Speeches");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("Subject");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Topic");
                    TopicNameLabel.InnerText = SepFunctions.LangText("Topic Name:");
                    SubjectLabel.InnerText = SepFunctions.LangText("Subject:");
                    SpeakerIDLabel.InnerText = SepFunctions.LangText("Speaker Name:");
                    TopicNameRequired.ErrorMessage = SepFunctions.LangText("~~Topic Name~~ is required.");
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
                if (dv != null)
                {
                    dv.Dispose();
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TopicID")))
            {
                var jTopic = SepCommon.DAL.SpeakersBureau.Topic_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("TopicID")));

                if (jTopic.TopicID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Topic~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Topic");
                    TopicID.Value = SepCommon.SepCore.Request.Item("TopicID");
                    TopicName.Value = jTopic.TopicName;
                    Populate_Speakers();
                    dv = BindData();
                    ManageGridView.DataSource = dv;
                    ManageGridView.DataBind();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TopicID.Value)) TopicID.Value = Strings.ToString(SepFunctions.GetIdentity());

                if (!Page.IsPostBack) ManageSpeeches.Visible = false;
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
                    var sDeleteResult = SepCommon.DAL.SpeakersBureau.Speech_Delete(sIDs);
                    DeleteResult.InnerHtml = Strings.InStr(sDeleteResult, SepFunctions.LangText("Successfully")) > 0 ? "<div class=\"alert alert-success\" role=\"alert\">" + sDeleteResult + "</div>" : "<div class=\"alert alert-danger\" role=\"alert\">" + sDeleteResult + "</div>";
                }
                catch
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                ManageSpeeches.Visible = true;
                Populate_Speakers();
                Subject.Value = string.Empty;

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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.SpeakersBureau.Topic_Save(SepFunctions.toLong(TopicID.Value), SepFunctions.Session_User_ID(), TopicName.Value, SepFunctions.Get_Portal_ID());

            ManageSpeeches.Visible = true;
            Populate_Speakers();

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }

        /// <summary>
        /// Handles the Click event of the SaveSpeechButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveSpeechButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.SpeakersBureau.Speech_Save(SepFunctions.toLong(SpeechID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(SpeakerID.Value), SepFunctions.toLong(TopicID.Value), Subject.Value, SepFunctions.Get_Portal_ID());

            ManageSpeeches.Visible = true;
            Populate_Speakers();
            Subject.Value = string.Empty;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty, "Speech");
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dSpeeches = SepCommon.DAL.SpeakersBureau.GetSpeakerBureauSpeeches(TopicID: SepFunctions.toLong(TopicID.Value));

            dv = new DataView(SepFunctions.ListToDataTable(dSpeeches));
            return dv;
        }

        /// <summary>
        /// Populates the speakers.
        /// </summary>
        private void Populate_Speakers()
        {
            if (!Page.IsPostBack)
            {
                var dSpeakers = SepCommon.DAL.SpeakersBureau.GetSpeakers();

                if (dSpeakers.Count == 0)
                {
                    ManageSpeeches.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must add a speaker before adding a speech.") + "</div>";
                }
                else
                {
                    for (var i = 0; i <= dSpeakers.Count - 1; i++) SpeakerID.Items.Add(new ListItem(dSpeakers[i].FirstName + " " + dSpeakers[i].LastName, Strings.ToString(dSpeakers[i].SpeakerID)));
                }
            }
        }
    }
}