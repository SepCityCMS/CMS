// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userprofiles_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class userprofiles_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userprofiles_modify : Page
    {
        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Profile");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
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

            GlobalVars.ModuleID = 63;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ProfilesAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ProfilesAdmin"), true) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable" || SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), false) == false)
            {
                PortalsRow.Visible = false;
                Portal.Text = "|" + SepFunctions.Get_Portal_ID() + "|";
            }

            if (!Page.IsPostBack)
            {
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

                if (SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesColor") == "Yes") ProfileColorOptions.Visible = true;
                else ProfileColorOptions.Visible = false;

                if (SepFunctions.toLong(SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesAudio")) == 0) AudioFilesRow.Visible = false;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ProfileID")))
            {
                var jProfile = SepCommon.DAL.UserProfiles.Profile_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ProfileID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jProfile.ProfileID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Profile~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Profile");
                    ProfileID.Value = SepCommon.SepCore.Request.Item("ProfileID");
                    UserID.Value = jProfile.UserID;
                    HotNot.Checked = jProfile.HotOrNot;
                    AllowComments.Checked = jProfile.EnableComment;
                    ProfileType.Value = Strings.ToString(jProfile.ProfileType);
                    AboutMe.Text = jProfile.AboutMe;
                    Pictures.ContentID = Strings.ToString(jProfile.ProfileID);
                    Pictures.UserID = jProfile.UserID;
                    BGColor.Text = jProfile.BGColor;
                    TextColor.Text = jProfile.TextColor;
                    LinkColor.Text = jProfile.LinkColor;

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("ProfileID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }

                sUserID = jProfile.UserID;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ProfileID.Value)) ProfileID.Value = Strings.ToString(SepFunctions.GetIdentity());
                Pictures.ContentID = ProfileID.Value;
                AudioFiles.ContentID = ProfileID.Value;
                UserID.Value = SepFunctions.Session_User_ID();
            }
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

            var intReturn = SepCommon.DAL.UserProfiles.Profile_Save(SepFunctions.toLong(ProfileID.Value), UserID.Value, AboutMe.Text, AllowComments.Checked, HotNot.Checked, SepFunctions.toInt(ProfileType.Value), BGColor.Text, TextColor.Text, LinkColor.Text, true, SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}