// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="celebrities_profile_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class conference_profile_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class conference_profile_modify : Page
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
        /// Gets the custom fields.
        /// </summary>
        public void GetCustomFields()
        {
            CustomFieldsAnswers jCustomFields;

            // Causes Supported
            jCustomFields = SepCommon.DAL.CustomFields.Answer_Get(179115432969858, SepFunctions.Session_User_ID());
            Custom179115432969858.Value = jCustomFields.FieldValue;

            // Charities & Foundations Supported
            jCustomFields = SepCommon.DAL.CustomFields.Answer_Get(117193851274426, SepFunctions.Session_User_ID());
            Custom117193851274426.Value = jCustomFields.FieldValue;

            // Your Occupation
            jCustomFields = SepCommon.DAL.CustomFields.Answer_Get(906800028608884, SepFunctions.Session_User_ID());
            Custom906800028608884.Value = jCustomFields.FieldValue;

            // Donation Amount (Points)
            jCustomFields = SepCommon.DAL.CustomFields.Answer_Get(847562837400918, SepFunctions.Session_User_ID());
            Custom847562837400918.Value = jCustomFields.FieldValue;

            // Available Hours
            // jCustomFields = SepCommon.DAL.CustomFields.Answer_Get(485736292234456, SepFunctions.Session_User_ID());
            // string[] arrHours = SepCommon.SepCore.Strings.Split(jCustomFields.FieldValue, "$$");
            // Array.Resize(ref arrHours, 10);

            // string[] Days1 = SepCommon.SepCore.Strings.Split(arrHours[0], "||");
            // string[] Days2 = SepCommon.SepCore.Strings.Split(arrHours[1], "||");
            // string[] Days3 = SepCommon.SepCore.Strings.Split(arrHours[2], "||");
            // string[] Days4 = SepCommon.SepCore.Strings.Split(arrHours[3], "||");
            // string[] Days5 = SepCommon.SepCore.Strings.Split(arrHours[4], "||");
            // string[] Days6 = SepCommon.SepCore.Strings.Split(arrHours[5], "||");
            // string[] Days7 = SepCommon.SepCore.Strings.Split(arrHours[6], "||");
            // string[] Days8 = SepCommon.SepCore.Strings.Split(arrHours[7], "||");
            // string[] Days9 = SepCommon.SepCore.Strings.Split(arrHours[8], "||");
            // string[] Days10 = SepCommon.SepCore.Strings.Split(arrHours[9], "||");

            // Array.Resize(ref Days1, 2);
            // Array.Resize(ref Days2, 2);
            // Array.Resize(ref Days3, 2);
            // Array.Resize(ref Days4, 2);
            // Array.Resize(ref Days5, 2);
            // Array.Resize(ref Days6, 2);
            // Array.Resize(ref Days7, 2);
            // Array.Resize(ref Days8, 2);
            // Array.Resize(ref Days9, 2);
            // Array.Resize(ref Days10, 2);

            // Day1.Text = Days1[0];
            // Day2.Text = Days2[0];
            // Day3.Text = Days2[0];
            // Day4.Text = Days4[0];
            // Day5.Text = Days5[0];
            // Day6.Text = Days6[0];
            // Day7.Text = Days7[0];
            // Day8.Text = Days8[0];
            // Day9.Text = Days9[0];
            // Day10.Text = Days10[0];

            // Hour1.Text = Days1[1];
            // Hour2.Text = Days2[1];
            // Hour3.Text = Days3[1];
            // Hour4.Text = Days4[1];
            // Hour5.Text = Days5[1];
            // Hour6.Text = Days6[1];
            // Hour7.Text = Days7[1];
            // Hour8.Text = Days8[1];
            // Hour9.Text = Days9[1];
            // Hour10.Text = Days10[1];
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
                    Custom906800028608884Label.InnerText = SepFunctions.LangText("Your Occupation: (Ex. Singer / Songwriter)");
                    AboutMeLabel.InnerText = SepFunctions.LangText("Enter your bio below:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Select an Image to Upload:");
                    Custom117193851274426Label.InnerText = SepFunctions.LangText("Charities & Foundations Supported: (One per a Line)");
                    Custom179115432969858Label.InnerText = SepFunctions.LangText("Causes Supported: (One per a Line)");
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
            TranslatePage();

            GlobalVars.ModuleID = 64;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                SepFunctions.RequireLogin("|2|");

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                if (SepFunctions.toLong(SepFunctions.GetUserInformation("AccessClass", SepFunctions.Session_User_ID())) == SepFunctions.toLong(SepFunctions.Setup(64, "ModeratorClass")) || SepFunctions.toLong(SepFunctions.GetUserInformation("AccessClass", SepFunctions.Session_User_ID())) == 2)
                {
                    if (!Page.IsPostBack)
                    {
                        var sProfileId = SepCommon.DAL.UserProfiles.Profile_UserID_To_ProfileID(SepFunctions.Session_User_ID());
                        var jProfile = SepCommon.DAL.UserProfiles.Profile_Get(sProfileId);

                        if (jProfile.ProfileID == 0)
                        {
                            ProfileID.Value = Strings.ToString(SepFunctions.GetIdentity());
                        }
                        else
                        {
                            ModifyLegend.InnerText = SepFunctions.LangText("Edit Profile");
                            AboutMe.Text = jProfile.AboutMe;
                            ProfileID.Value = Strings.ToString(jProfile.ProfileID);
                        }

                        var cCredits = SepCommon.DAL.Credits.GetCreditPricing();
                        for (var i = 0; i <= cCredits.Count - 1; i++) Custom847562837400918.Items.Add(new ListItem("Donation of " + SepFunctions.Format_Currency(cCredits[i].Price), Strings.ToString(cCredits[i].CreditID)));

                        GetCustomFields();

                        Pictures.ContentID = ProfileID.Value;
                    }
                }
                else
                {
                    PageContent.InnerHtml = "<h1>" + SepFunctions.LangText("Access Denied") + "</h1>";
                }
            }
            else
            {
                PageContent.InnerHtml = "<h1>" + SepFunctions.LangText("Access Denied") + "</h1>";
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
            var sHours = string.Empty;

            var intReturn = SepCommon.DAL.UserProfiles.Profile_Save(SepFunctions.toLong(ProfileID.Value), SepFunctions.Session_User_ID(), AboutMe.Text, false, false, 0, string.Empty, string.Empty, string.Empty, true, SepFunctions.Get_Portal_ID());

            // sHours += Day1.Text + "||" + Hour1.Text + "&&" + Day2.Text + "||" + Hour2.Text + "&&" + Day3.Text + "||" + Hour3.Text + "&&" + Day4.Text + "||" + Hour4.Text + "&&" + Day5.Text + "||" + Hour5.Text;
            // sHours += "&&" + Day6.Text + "||" + Hour6.Text + "&&" + Day7.Text + "||" + Hour7.Text + "&&" + Day8.Text + "||" + Hour8.Text + "&&" + Day9.Text + "||" + Hour9.Text + "&&" + Day10.Text + "||" + Hour10.Text;
            SepCommon.DAL.CustomFields.Answers_Save(SepFunctions.Session_User_ID(), 63, 485736292234456, 0, sHours);
            SepCommon.DAL.CustomFields.Answers_Save(SepFunctions.Session_User_ID(), 63, 847562837400918, 0, Custom847562837400918.Value);

            GetCustomFields();

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}