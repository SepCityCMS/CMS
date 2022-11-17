// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="messenger_compose.aspx.cs" company="SepCity, Inc.">
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
    /// Class messenger_compose.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class messenger_compose : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Compose a Message");
                    UserNameLabel.InnerText = SepFunctions.LangText("Your User Name:");
                    ToUserNameLabel.InnerText = SepFunctions.LangText("To User Name:");
                    SubjectLabel.InnerText = SepFunctions.LangText("Subject:");
                    ToUserNameRequired.ErrorMessage = SepFunctions.LangText("~~To User Name~~ is required.");
                    SubjectRequired.ErrorMessage = SepFunctions.LangText("~~Subject~~ is required.");
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

            GlobalVars.ModuleID = 17;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "MessengerEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("MessengerCompose"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "messenger_compose.aspx" + Strings.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("MessageID")) ? "?MessageID=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("MessageID")) : string.Empty));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0] + " - " + SepFunctions.LangText("Compose a New Message");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.CompareKeys(SepFunctions.Security("MessengerMass"), false) == false) Mass_Message.Visible = false;

            if (!IsPostBack)
            {
                Username.InnerHtml = SepFunctions.Session_User_Name();

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID"))) ToUserName.Value = SepFunctions.GetUserInformation("UserName", SepCommon.SepCore.Request.Item("UserID"));

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Subject"))) Subject.Value = SepCommon.SepCore.Request.Item("Subject");

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("MessageID")))
                {
                    var jMessenger = SepCommon.DAL.Messenger.Message_Get(SepFunctions.Session_User_ID(), SepFunctions.toLong(Strings.Replace(Strings.Replace(SepCommon.SepCore.Request.Item("MessageID"), "RE:", string.Empty), "FW:", string.Empty)));

                    if (jMessenger.MessageID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Message~~ does not exist.") + "</div>";
                        ComposeForm.Visible = false;
                    }
                    else
                    {
                        if (Strings.Left(SepCommon.SepCore.Request.Item("MessageID"), 3) == "RE:")
                        {
                            ToUserName.Value = jMessenger.FromUsername;
                            Subject.Value = "RE: " + jMessenger.Subject;
                        }

                        if (Strings.Left(SepCommon.SepCore.Request.Item("MessageID"), 3) == "FW:") Subject.Value = "FW: " + jMessenger.Subject;
                        Message.Text = "<br/><br/><hr width=\"100%\" /><br/>" + jMessenger.Message;
                    }
                }

                if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostSendMessage", "GetSendMessage", ToUserName.Value, true) == false) SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
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
        /// Handles the Click event of the SendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SendButton_Click(object sender, EventArgs e)
        {
            string sReturn;

            if (Strings.LCase(ToUserName.Value) == Strings.LCase(SepFunctions.Session_User_Name()))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You cannot send a message to yourself.") + "</div>";
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("MessengerMass"), false) && ToUserName.Value == "[MASS_MESSAGE]")
                sReturn = SepCommon.DAL.Messenger.Message_Send_Mass_Message(SepFunctions.Session_User_ID(), ToUserName.Value, Subject.Value, Message.Text);
            else
                sReturn = SepCommon.DAL.Messenger.Message_Send(SepFunctions.Session_User_ID(), ToUserName.Value, Subject.Value, Message.Text, true);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Compose", Subject.Value);
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
            }

            if (Strings.InStr(ErrorMessage.InnerHtml, SepFunctions.LangText("successfully")) > 0) ComposeForm.Visible = false;
        }
    }
}