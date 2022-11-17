// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="hotornot.aspx.cs" company="SepCity, Inc.">
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
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class hotornot1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class hotornot1 : Page
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
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            GlobalVars.ModuleID = 40;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "HotNotEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("HotNotAccess"));

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

            if (SepCommon.SepCore.Request.Item("DoAction") == "Rate")
            {
                var returnRating = SepCommon.DAL.HotOrNot.Rate_Picture(SepFunctions.toInt(SepCommon.SepCore.Request.Item("Rate")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("UploadID")));

                RandomPic.InnerHtml += "<p align=\"center\">" + SepFunctions.LangText("Average Rating for this picture:") + " (" + returnRating + ") <a href=\"" + sInstallFolder + "hotornot.aspx?Show=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("Show")) + "\">" + SepFunctions.LangText("Next Photo") + "</a></p>";
                RandomPic.InnerHtml += "<p align=\"center\"><img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("UploadID")) + "&Size=thumb&ModuleID=40\" border=\"0\" /></p>";
            }
            else
            {
                var returnXML = SepCommon.DAL.HotOrNot.Random_Picture(SepCommon.SepCore.Request.Item("Show"));

                if (returnXML.UploadID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~File~~ does not exist.") + "</div>";
                    RandomPic.Visible = false;
                }
                else
                {
                    if (returnXML.UploadID > 0)
                    {
                        RandomPic.InnerHtml += "<p align=\"center\">" + SepFunctions.LangText("Not so Hot") + " ";
                        for (var i = 1; i <= 10; i++) RandomPic.InnerHtml += "<a href=\"" + sInstallFolder + "hotornot.aspx?DoAction=Rate&Rate=" + i + "&UploadID=" + SepFunctions.UrlEncode(Strings.ToString(returnXML.UploadID)) + "&Show=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("Show")) + "\">" + Strings.ToString(i) + "</a> . ";
                        RandomPic.InnerHtml += SepFunctions.LangText("HOT") + " &gt;&gt; <a href=\"" + sInstallFolder + "hotornot.aspx?UploadID=" + SepFunctions.UrlEncode(Strings.ToString(returnXML.UploadID)) + "&Show=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("Show")) + "\">" + SepFunctions.LangText("Skip Photo") + "</a></p>";
                        RandomPic.InnerHtml += "<p align=\"center\"><img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + returnXML.UploadID + "&Size=thumb&ModuleID=40\" border=\"0\" /></p>";

                        RandomPic.InnerHtml += "<script type=\"text/javascript\">";
                        RandomPic.InnerHtml += "document.getElementById('HotNotReport').href='report_listing.aspx?URL=" + SepFunctions.UrlEncode("/hotornot.aspx?DoAction=ViewPicture&UploadID=" + returnXML.UploadID) + "&UniqueID=" + SepFunctions.UrlEncode(Strings.ToString(returnXML.UploadID)) + "&ModuleID=" + GlobalVars.ModuleID + "';";
                        RandomPic.InnerHtml += "</script>";
                    }
                    else
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("There are not any images that are uploaded to rate.") + "</div>";
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
    }
}