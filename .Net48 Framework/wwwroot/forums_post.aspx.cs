// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forums_post.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class forums_post.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forums_post : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Post a Topic");
                    SubjectLabel.InnerText = SepFunctions.LangText("Subject:");
                    AttachmentLabel.InnerText = SepFunctions.LangText("Add an Attachment:");
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

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ForumsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ForumsPost"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("CatID")))
                {
                    CatID.Value = SepCommon.SepCore.Request.Item("CatID");
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TopicID")))
                    {
                        var jForums = SepCommon.DAL.Forums.Topic_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("TopicID")));

                        ReplyID.Value = SepCommon.SepCore.Request.Item("TopicID");
                        Subject.Value = "RE: " + jForums.Subject;
                        Subject.Attributes.Add("readonly", "readonly");
                        ModifyLegend.InnerHtml = SepFunctions.LangText("Post Reply");

                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostReplyTopic", "GetReplyTopic", TopicID.Value, true) == false) SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                    }
                    else
                    {
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostPostTopic", "GetPostTopic", TopicID.Value, true) == false)
                        {
                            SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                            return;
                        }

                        TopicID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    }
                }
                else
                {
                    ModFormDiv.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must go back and post from a category. (Invalid Category ID)") + "</div>";
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var intReturn = SepCommon.DAL.Forums.Topic_Save(SepFunctions.toLong(TopicID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(CatID.Value), SepFunctions.toLong(ReplyID.Value), EmailReply.Checked, Subject.Value, Message.Text, Attachment, SepFunctions.Get_Portal_ID());

            if (intReturn == 3)
            {
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Added", Subject.Value);
            }
            else
            {
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Reply", Subject.Value);
            }

            if (SepFunctions.toLong(ReplyID.Value) > 0)
                SepFunctions.Redirect(sInstallFolder + "forum/" + ReplyID.Value + "/" + SepFunctions.Format_ISAPI(Subject.Value) + "/");
            else
                SepFunctions.Redirect(sInstallFolder + "forum/" + TopicID.Value + "/" + SepFunctions.Format_ISAPI(Subject.Value) + "/");
        }
    }
}