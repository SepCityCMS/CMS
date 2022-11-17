// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="profiles_modify.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class profiles_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class profiles_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Create Your Profile");
                    ProfileTypeLabel.InnerText = SepFunctions.LangText("Profile Type:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Pictures:");
                    AudioFilesLabel.InnerText = SepFunctions.LangText("Audio Files:");
                    BGColorLabel.InnerText = SepFunctions.LangText("Background Color:");
                    TextColorLabel.InnerText = SepFunctions.LangText("Text Color:");
                    LinkColorLabel.InnerText = SepFunctions.LangText("Link Color:");
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

            GlobalVars.ModuleID = 63;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ProfilesModify"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Setup(40, "HotNotEnable") != "Enable") HotNotRow.Visible = false;

            if (!Page.IsPostBack)
            {
                if (SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesColor") == "Yes") ProfileColorOptions.Visible = true;
                else ProfileColorOptions.Visible = false;

                if (SepFunctions.toLong(SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesAudio")) == 0) AudioFilesRow.Visible = false;

                var sProfileType1 = SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesType1");
                var sProfileType2 = SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesType2");
                var sProfileType3 = SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesType3");

                if (!string.IsNullOrWhiteSpace(sProfileType1) || !string.IsNullOrWhiteSpace(sProfileType2) || !string.IsNullOrWhiteSpace(sProfileType3))
                {
                    if (!string.IsNullOrWhiteSpace(sProfileType1)) ProfileType.Items.Add(new ListItem(sProfileType1, "1"));
                    if (!string.IsNullOrWhiteSpace(sProfileType2)) ProfileType.Items.Add(new ListItem(sProfileType2, "2"));
                    if (!string.IsNullOrWhiteSpace(sProfileType3)) ProfileType.Items.Add(new ListItem(sProfileType3, "3"));
                }
                else
                {
                    ProfileTypeRow.Visible = false;
                }

                var sProfileId = SepCommon.DAL.UserProfiles.Profile_UserID_To_ProfileID(SepFunctions.Session_User_ID());
                var jProfile = SepCommon.DAL.UserProfiles.Profile_Get(sProfileId);

                if (jProfile.ProfileID == 0)
                {
                    ProfileID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostCreateAccount", "GetCreateAccount", ProfileID.Value, true) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Profile");
                    HotNot.Checked = jProfile.HotOrNot;
                    AllowComments.Checked = jProfile.EnableComment;
                    ProfileType.Value = Strings.ToString(jProfile.ProfileType);
                    AboutMe.Text = jProfile.AboutMe;
                    ProfileID.Value = Strings.ToString(jProfile.ProfileID);
                    BGColor.Text = jProfile.BGColor;
                    TextColor.Text = jProfile.TextColor;
                    LinkColor.Text = jProfile.LinkColor;
                }
            }

            Pictures.ContentID = ProfileID.Value;
            AudioFiles.ContentID = ProfileID.Value;
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
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                Pictures.showTemp = true;
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var intReturn = SepCommon.DAL.UserProfiles.Profile_Save(SepFunctions.toLong(ProfileID.Value), SepFunctions.Session_User_ID(), AboutMe.Text, AllowComments.Checked, HotNot.Checked, SepFunctions.toInt(ProfileType.Value), ProfileColorOptions.Visible ? BGColor.Text : string.Empty, ProfileColorOptions.Visible ? TextColor.Text : string.Empty, ProfileColorOptions.Visible ? LinkColor.Text : string.Empty, true, SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}