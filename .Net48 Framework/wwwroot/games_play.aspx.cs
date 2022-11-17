// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="games_play.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class games_play.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class games_play : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            GlobalVars.ModuleID = 47;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "GamesEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("GamesPlay"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("GameID")))
            {
                var jGames = SepCommon.DAL.Games.Game_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("GameID")));

                if (jGames.GameID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Game~~ does not exist.") + "</div>";
                    GameName.Visible = false;
                    GameContent.Visible = false;
                }
                else
                {
                    GameName.InnerText = jGames.GameName;

                    var str = new StringBuilder();

                    str.Append("<p align=\"center\">");
                    switch (SepCommon.SepCore.Request.Item("GameID"))
                    {
                        case "6":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=conejo width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/conejo.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/conejo.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=conejo TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "7":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=dattack width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/dattack.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/dattack.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=dattack TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "8":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=detroiter width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/detroiter.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/detroiter.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=detroiter TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "9":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=jackpot width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/jackpot.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" VALUE=#000000>");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/jackpot.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=jackpot TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "10":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=jewels width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/jewels.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/jewels.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=jewels TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "11":
                            str.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"452\" height=\"312\" id=\"memory\" align=\"\">");
                            str.Append("<param name=\"movie\" value=\"" + sImageFolder + "games/memory.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<param name=\"bgcolor\" value=#ffff99>");
                            str.Append("<embed src=\"" + sImageFolder + "games/memory.swf\" quality=\"high\" bgcolor=#ffff99  width=\"452\" height=\"312\" name=\"memory\" align=\"\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\">");
                            str.Append("</embed>");
                            str.Append("</object>");
                            break;

                        case "12":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=pacman width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/pacman.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" VALUE=#000000>");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/pacman.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=pacman TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "13":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=spuzzle width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/spuzzle.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/spuzzle.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=spuzzle TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "14":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=pong width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/pong.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/pong.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=pong TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "15":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=rcube width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/rcube.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/rcube.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=rcube TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "16":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=tetris width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/tetris.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/tetris.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=tetris TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "17":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=wsearches width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/wsearches.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/wsearches.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=wsearches TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;

                        case "18":
                            str.Append("<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ID=tdrally width=500 height=\"400\">");
                            str.Append("<PARAM name=\"movie\" value=\"" + sImageFolder + "games/tdrally.swf\">");
                            str.Append("<PARAM name=\"quality\" value=\"high\">");
                            str.Append("<PARAM name=\"bgcolor\" value=\"#FFFFFF\">");
                            str.Append("<EMBED src=\"" + sImageFolder + "games/tdrally.swf\" quality=\"high\" bgcolor=\"#FFFFFF\"  width=500 height=\"400\" swLiveConnect=true NAME=tdrally TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED>");
                            str.Append("</OBJECT>");
                            break;
                    }

                    str.Append("</p>");

                    GameContent.InnerHtml = Strings.ToString(str);
                }
            }
            else
            {
                if (!Page.IsPostBack) ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid Game") + "</div>";
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