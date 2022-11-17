// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forums_display.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class forums_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forums_display : Page
    {
        /// <summary>
        /// The s cat identifier
        /// </summary>
        public static string sCatID = string.Empty;

        /// <summary>
        /// The s install folder
        /// </summary>
        public static string sInstallFolder = string.Empty;

        /// <summary>
        /// The s profile identifier
        /// </summary>
        public static string sProfileID = string.Empty;

        /// <summary>
        /// The s profile image
        /// </summary>
        public static string sProfileImage = string.Empty;

        /// <summary>
        /// The s topic identifier
        /// </summary>
        public static string sTopicID = string.Empty;

        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

        /// <summary>
        /// The s user name
        /// </summary>
        public static string sUserName = string.Empty;

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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortal">if set to <c>true</c> [exclude portal].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortal = false)
        {
            return SepFunctions.GetInstallFolder(excludePortal);
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
                    ReplyContent.Columns[0].HeaderText = SepFunctions.LangText("Reply Info");
                    ReplyContent.Columns[1].HeaderText = SepFunctions.LangText("Message");
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

            GlobalVars.ModuleID = 12;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ForumsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ForumsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var jForums = SepCommon.DAL.Forums.Topic_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("TopicID")));

            if (jForums.TopicID > 0)
            {
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jForums.Subject);
                sUserID = jForums.UserID;
                sUserName = jForums.Username;
                sCatID = Strings.ToString(jForums.CatID);
                sTopicID = Strings.ToString(jForums.TopicID);
                sProfileID = Strings.ToString(jForums.ProfileID);
                Message.InnerHtml = jForums.Message;
                Username.InnerHtml = jForums.Username;
                DatePosted.InnerHtml = Strings.ToString(jForums.DatePosted);
                TotalPosts.InnerHtml = Strings.ToString(jForums.TotalPosts);
                DateRegistered.InnerHtml = Strings.ToString(jForums.DateRegistered);
                OnlineStatus.InnerHtml = SepFunctions.userOnlineStatus(sUserID);
                sProfileImage = SepFunctions.userProfileImage(sUserID);

                if (!string.IsNullOrWhiteSpace(jForums.Attachment)) Attachment.InnerHtml = SepFunctions.LangText("Attachment:") + " <a href=\"" + SepFunctions.GetInstallFolder(true) + "download_attachment.aspx?ModuleID=12&UniqueID=" + sTopicID + "\" target=\"_blank\">" + jForums.Attachment + "</a>";
                else AttachmentRow.Visible = false;

                var cForumReplies = SepCommon.DAL.Forums.GetForumTopics(TopicID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("TopicID")));

                ReplyContent.DataSource = cForumReplies.Take(50);
                ReplyContent.DataBind();
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
    }
}