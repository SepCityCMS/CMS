// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="speakers.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class speakers.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class speakers : Page
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
                    TopicID.Items[0].Text = SepFunctions.LangText("------------- Select a Topic -------------");
                    SpeakerID.Items[0].Text = SepFunctions.LangText("------------- Select a Speaker -------------");
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

            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 50;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "SpeakerEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("SpeakerAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                var cReplace = new Replace();

                PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

                cReplace.Dispose();

                var cTopics = SepCommon.DAL.SpeakersBureau.GetSpeakerBureauTopics();
                for (var i = 0; i <= cTopics.Count - 1; i++) TopicID.Items.Add(new ListItem(cTopics[i].TopicName, Strings.ToString(cTopics[i].TopicID)));

                var cSpeakers = SepCommon.DAL.SpeakersBureau.GetSpeakers();
                for (var i = 0; i <= cSpeakers.Count - 1; i++) SpeakerID.Items.Add(new ListItem(cSpeakers[i].FirstName + " " + cSpeakers[i].LastName, Strings.ToString(cSpeakers[i].SpeakerID)));

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SpeakerID")))
                {
                    SpeakersRow.Visible = true;
                    var jSpeakers = SepCommon.DAL.SpeakersBureau.Speaker_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SpeakerID")));

                    if (jSpeakers.SpeakerID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Speaker~~ does not exist.") + "</div>";
                        SpeakersRow.Visible = false;
                    }
                    else
                    {
                        SpeakerID.Value = SepCommon.SepCore.Request.Item("SpeakerID");
                        SpeakerImage.InnerHtml = !string.IsNullOrWhiteSpace(jSpeakers.Photo) ? "<img src=\"" + jSpeakers.Photo + "\" border=\"0\" />" : string.Empty;
                        SpeakerName.InnerText = jSpeakers.FirstName + " " + jSpeakers.LastName;
                        Cred.InnerText = jSpeakers.Cred;
                        Bio.InnerHtml = jSpeakers.Bio;
                        RequestLink.NavigateUrl = sInstallFolder + "speakers_Schedule.aspx?SpeakerID=" + SepCommon.SepCore.Request.Item("SpeakerID");
                        SpeechList2.InnerHtml = string.Empty;
                        var gSpeakers = SepCommon.DAL.SpeakersBureau.GetSpeakerBureauSpeeches(SpeakerID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("SpeakerID")));
                        for (var i = 0; i <= gSpeakers.Count - 1; i++) SpeechList2.InnerHtml += gSpeakers[i].Subject + "<br/>";
                    }
                }
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
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (SepFunctions.toLong(TopicID.Value) > 0)
            {
                TopicRow.Visible = true;
                var jTopics = SepCommon.DAL.SpeakersBureau.Topic_Get(SepFunctions.toLong(TopicID.Value));

                if (jTopics.TopicID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Topic~~ does not exist.") + "</div>";
                    TopicRow.Visible = false;
                }
                else
                {
                    SpeechList.InnerHtml = string.Empty;
                    TopicName.InnerText = jTopics.TopicName;
                    var gTopics = SepCommon.DAL.SpeakersBureau.GetSpeakerBureauSpeeches(TopicID: SepFunctions.toLong(TopicID.Value));
                    for (var i = 0; i <= gTopics.Count - 1; i++) SpeechList.InnerHtml += "<a href=\"speakers.aspx?SpeakerID=" + gTopics[i].SpeakerID + "\">" + gTopics[i].Subject + "</a><br/>";
                }
            }
            else
            {
                TopicRow.Visible = false;
            }

            if (SepFunctions.toLong(SpeakerID.Value) > 0)
            {
                SpeakersRow.Visible = true;
                var jSpeakers = SepCommon.DAL.SpeakersBureau.Speaker_Get(SepFunctions.toLong(SpeakerID.Value));

                if (jSpeakers.SpeakerID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Speaker~~ does not exist.") + "</div>";
                    SpeakersRow.Visible = false;
                }
                else
                {
                    SpeakerName.InnerText = jSpeakers.FirstName + " " + jSpeakers.LastName;
                    SpeakerImage.InnerHtml = !string.IsNullOrWhiteSpace(jSpeakers.Photo) ? "<img src=\"" + jSpeakers.Photo + "\" border=\"0\" />" : string.Empty;
                    Cred.InnerText = jSpeakers.Cred;
                    Bio.InnerHtml = jSpeakers.Bio;
                    RequestLink.NavigateUrl = sInstallFolder + "speakers_Schedule.aspx?SpeakerID=" + SpeakerID.Value;
                    SpeechList2.InnerHtml = string.Empty;
                    var gSpeakers = SepCommon.DAL.SpeakersBureau.GetSpeakerBureauSpeeches(SpeakerID: SepFunctions.toLong(SpeakerID.Value));
                    for (var i = 0; i <= gSpeakers.Count - 1; i++) SpeechList2.InnerHtml += gSpeakers[i].Subject + "<br/>";
                }
            }
            else
            {
                SpeakersRow.Visible = false;
            }
        }
    }
}