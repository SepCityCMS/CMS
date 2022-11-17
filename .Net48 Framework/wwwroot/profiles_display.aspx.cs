// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="profiles_display.aspx.cs" company="SepCity, Inc.">
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
    /// Class profiles_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class profiles_display : Page
    {
        /// <summary>
        /// The s profile image
        /// </summary>
        public static string sProfileImage = string.Empty;

        /// <summary>
        /// The c common
        /// </summary>
        public static bool sShowComments = false;

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
            var sProfileId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ProfileID"));

            GlobalVars.ModuleID = 63;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ProfilesView"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Setup(17, "MessengerEnable") != "Enable") Send_Message.Visible = false;
            if (SepFunctions.Setup(33, "FriendsEnable") != "Yes") Add_to_Friends.Visible = false;
            if (SepFunctions.Setup(994, "AskBirthDate") != "Yes") AgeRow.Visible = false;
            if (SepFunctions.Setup(994, "AskGender") != "Yes") GenderRow.Visible = false;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ProfileUser")))
            {
                sUserID = SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("ProfileUser"));
                if (!string.IsNullOrWhiteSpace(sUserID)) sProfileId = SepFunctions.userProfileID(sUserID);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserName")))
                {
                    sUserID = SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"));
                    if (!string.IsNullOrWhiteSpace(sUserID)) sProfileId = SepFunctions.userProfileID(sUserID);
                }
            }

            if (sProfileId > 0)
            {
                var jProfiles = SepCommon.DAL.UserProfiles.Profile_Get(sProfileId);

                if (jProfiles.ProfileID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Profile~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jProfiles.Username);
                    sUserID = jProfiles.UserID;
                    sUserName = jProfiles.Username;
                    UserName.InnerHtml = sUserName;
                    sProfileImage = jProfiles.DefaultPicture;
                    if (jProfiles.Age > 18) Age.InnerHtml = Strings.ToString(jProfiles.Age);
                    else AgeRow.Visible = false;
                    if (jProfiles.Location.Length > 2) Location.InnerHtml = jProfiles.Location;
                    else LocationRow.Visible = false;
                    Gender.InnerHtml = jProfiles.Sex;
                    MemberSince.InnerHtml = Strings.FormatDateTime(jProfiles.MemberSince, Strings.DateNamedFormat.ShortDate);
                    LastLoginDate.InnerHtml = Strings.FormatDateTime(jProfiles.LastLogin, Strings.DateNamedFormat.ShortDate);
                    LastLoginTime.InnerHtml = Strings.FormatDateTime(jProfiles.LastLogin, Strings.DateNamedFormat.ShortTime);
                    Views.InnerHtml = Strings.ToString(jProfiles.Views);
                    AboutMe.InnerHtml = jProfiles.AboutMe;

                    // Change Colors
                    if (!string.IsNullOrWhiteSpace(jProfiles.BGColor))
                    {
                        DisplayContent.Style.Add("background-color", "#" + jProfiles.BGColor);
                        var scriptKey = "bgColorScript";
                        var javaScript = "<script type=\"text/javascript\">$('#UserTbl tr:first').removeClass('TableHeader');$('#AboutMeTbl tr:first').removeClass('TableHeader');</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
                    }

                    if (!string.IsNullOrWhiteSpace(jProfiles.TextColor)) DisplayContent.Style.Add("color", "#" + jProfiles.TextColor);
                    if (!string.IsNullOrWhiteSpace(jProfiles.LinkColor))
                    {
                        var scriptKey = "linkColorScript";
                        var javaScript = "<script type=\"text/javascript\">$('#AboutMeTbl a').css('color', '#" + jProfiles.LinkColor + "');</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
                    }

                    UserReviews.UserID = jProfiles.UserID;

                    // Show Images
                    var cContentImages = new SepCityControls.ContentImages
                    {
                        ContentUniqueID = Strings.ToString(jProfiles.ProfileID),
                        ModuleID = GlobalVars.ModuleID
                    };
                    ProfilePics.InnerHtml = cContentImages.Render();

                    // Show Audio Player
                    if (SepFunctions.toLong(SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesAudio")) == 0) AudioFiles.Visible = false;
                    else AudioFiles.ContentID = Strings.ToString(jProfiles.ProfileID);

                    sShowComments = jProfiles.EnableComment;
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Sorry, this profile~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
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