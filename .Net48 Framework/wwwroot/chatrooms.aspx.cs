// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="chatrooms.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using CuteChat;
    using SepCommon;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;
    using ChatProvider = CuteChat.ChatProvider;
    using Strings = SepCommon.SepCore.Strings;

    /// <summary>
    /// Class chatrooms1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class chatrooms1 : Page
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
            var sChatFolder = SepFunctions.GetInstallFolder(true);

            GlobalVars.ModuleID = 42;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ChatEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ChatAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "chatrooms.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            try
            {
                ChatProvider.Instance = new SepCommon.ChatProvider();
                ChatSystem.Start(new AppSystem());

                if (ChatApi.GetLobbyArray().Length == 0)
                {
                    IChatLobby newLobby = null;
                    newLobby = ChatApi.CreateLobbyInstance();
                    newLobby.AllowAnonymous = true;
                    newLobby.Announcement = string.Empty;
                    newLobby.AutoAwayMinute = 10;
                    newLobby.AvatarChatURL = string.Empty;
                    newLobby.Description = "Join our main chat room today!";
                    newLobby.HistoryCount = 0;
                    newLobby.HistoryDay = 0;
                    newLobby.Integration = string.Empty;
                    newLobby.IsAvatarChat = false;
                    newLobby.Locked = false;
                    newLobby.ManagerList = string.Empty;
                    newLobby.MaxIdleMinute = 30;
                    newLobby.MaxOnlineCount = 100;
                    newLobby.Password = string.Empty;
                    newLobby.SortIndex = 1;
                    newLobby.Title = "Lobby";
                    newLobby.Topic = "Lobby";

                    ChatApi.CreateLobbyInstance();
                }

                switch (ChatApi.GetLobbyArray().Length)
                {
                    case 0:
                        chat_script.InnerHtml = "<p align=\"center\">" + SepFunctions.LangText("The administrator has not setup any chat rooms.") + "</p>";
                        break;

                    case 1:
                        foreach (var LobbyInfo in ChatApi.GetLobbyInfoArray())
                        {
                            chat_script.InnerHtml = "<p align=\"center\">" + SepFunctions.LangText("The chat room is loading in a new tab.") + "</p>";
                            chat_script.InnerHtml += "<p align=\"center\">If the chat room does not load please <a href=\"" + sChatFolder + "CuteSoft_Client/CuteChat/Channel.Aspx?Place=Lobby-" + LobbyInfo.Lobby.LobbyId + "\" target=\"_blank\">click here</a>.</p>";
                            chat_script.InnerHtml += "<script type=\"text/javascript\">window.open('" + sChatFolder + "CuteSoft_Client/CuteChat/Channel.Aspx?Place=Lobby-" + LobbyInfo.Lobby.LobbyId + "', '_blank');</script>";
                        }

                        break;

                    default:
                        foreach (var LobbyInfo in ChatApi.GetLobbyInfoArray()) chat_script.InnerHtml += "<a href=\"" + sChatFolder + "CuteSoft_Client/CuteChat/Channel.Aspx?Place=Lobby-" + LobbyInfo.Lobby.LobbyId + "\" target=\"_blank\">" + LobbyInfo.Lobby.Title + "</a><br/>";

                        break;
                }
            }
            catch
            {
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