// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="messenger_display.aspx.cs" company="SepCity, Inc.">
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
    /// Class messenger_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class messenger_display : Page
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
        /// Handles the Click event of the BlockSenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BlockSenderButton_Click(object sender, EventArgs e)
        {
            var sReturn = SepCommon.DAL.Messenger.Message_Block_User_Save(SepFunctions.Session_User_ID(), msgFrom.InnerHtml);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
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
        /// Handles the Click event of the ForwardButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ForwardButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect("messenger_compose.aspx?MessageID=" + SepFunctions.UrlEncode("FW:" + SepCommon.SepCore.Request.Item("MessageID")));
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

            GlobalVars.ModuleID = 17;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "MessengerEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("MessengerRead"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "messenger_display.aspx?MessageID=" + SepCommon.SepCore.Request.Item("MessageID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0] + " - " + SepFunctions.LangText("Display Message");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("MessageID")))
            {
                var jMessenger = SepCommon.DAL.Messenger.Message_Get(SepFunctions.Session_User_ID(), SepFunctions.toLong(SepCommon.SepCore.Request.Item("MessageID")));

                if (jMessenger.MessageID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Message~~ does not exist.") + "</div>";
                    MsgDisplay.Visible = false;
                }
                else
                {
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jMessenger.Subject);
                    msgFrom.InnerHtml = jMessenger.FromUsername;
                    msgTo.InnerHtml = jMessenger.ToUsername;
                    msgDate.InnerHtml = Strings.ToString(jMessenger.DateSent);
                    msgSubject.InnerHtml = jMessenger.Subject;
                    if (SepFunctions.Setup(63, "ProfilesEnable") == "Enable")
                        if (!string.IsNullOrWhiteSpace(jMessenger.UserPhoto))
                            msgFromUserImage.InnerHtml = "<img src=\"" + jMessenger.UserPhoto + "\" border=\"0\" />";
                    msgBody.InnerHtml = jMessenger.Message;
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Message~~ does not exist.") + "</div>";
                MsgDisplay.Visible = false;
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
        /// Handles the Click event of the ReplyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ReplyButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect("messenger_compose.aspx?MessageID=" + SepFunctions.UrlEncode("RE:" + SepCommon.SepCore.Request.Item("MessageID")));
        }
    }
}