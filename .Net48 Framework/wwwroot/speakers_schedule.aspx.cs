// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="speakers_schedule.aspx.cs" company="SepCity, Inc.">
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
    /// Class speakers_schedule.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class speakers_schedule : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Request a Speaker Form");
                    SpeakersNameLabel.InnerText = SepFunctions.LangText("Speaker's Name:");
                    YourNameLabel.InnerText = SepFunctions.LangText("Your Name:");
                    SpeechIDLabel.InnerText = SepFunctions.LangText("Subject:");
                    CompanyNameLabel.InnerText = SepFunctions.LangText("Company Name:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    SpeakerDateLabel.InnerText = SepFunctions.LangText("Date for Speaker:");
                    SpeakerTimeLabel.InnerText = SepFunctions.LangText("Time for Speaker:");
                    SpeakersNameRequired.ErrorMessage = SepFunctions.LangText("~~Speaker's Name~~ is required.");
                    YourNameRequired.ErrorMessage = SepFunctions.LangText("~~Your Name~~ is required.");
                    SpeechIDRequired.ErrorMessage = SepFunctions.LangText("~~Subject~~ is required.");
                    StreetAddressRequired.ErrorMessage = SepFunctions.LangText("~~Street Address~~ is required.");
                    CityRequired.ErrorMessage = SepFunctions.LangText("~~City~~ is required.");
                    PhoneNumberRequired.ErrorMessage = SepFunctions.LangText("~~Phone Number~~ is required.");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
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

            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 50;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "SpeakerEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("SpeakerAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SpeakerID")))
                {
                    var jSpeaker = SepCommon.DAL.SpeakersBureau.Speaker_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SpeakerID")));

                    if (jSpeaker.SpeakerID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Speaker~~ does not exist.") + "</div>";
                        ModFormDiv.Visible = false;
                    }
                    else
                    {
                        SpeakerID.Value = SepCommon.SepCore.Request.Item("SpeakerID");
                        SpeakersName.Value = jSpeaker.FirstName + " " + jSpeaker.LastName;
                    }

                    var dSpeeches = SepCommon.DAL.SpeakersBureau.GetSpeakerBureauSpeeches(SpeakerID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("SpeakerID")));

                    if (dSpeeches.Count == 0)
                    {
                        ModFormDiv.Visible = false;
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This speaker does not have any available speeches.") + "</div>";
                    }
                    else
                    {
                        for (var i = 0; i <= dSpeeches.Count - 1; i++) SpeechID.Items.Add(new ListItem(dSpeeches[i].Subject, dSpeeches[i].Subject));

                        if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                        {
                            YourName.Value = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName");
                            StreetAddress.Value = SepFunctions.GetUserInformation("StreetAddress");
                            City.Value = SepFunctions.GetUserInformation("City");
                            PhoneNumber.Value = SepFunctions.GetUserInformation("PhoneNumber");
                            EmailAddress.Value = SepFunctions.GetUserInformation("EmailAddress");
                        }
                    }
                }
                else
                {
                    ModFormDiv.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Speaker does not exist in our database.") + "</div>";
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

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var jSpeaker = SepCommon.DAL.SpeakersBureau.Speaker_Get(SepFunctions.toLong(SpeakerID.Value));

            if (jSpeaker.SpeakerID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Speaker~~ does not exist.") + "</div>";
                ModFormDiv.Visible = false;
            }
            else
            {
                string EmailBody = "Speaker Name: " + SpeakersName.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Subject") + " " + SpeechID.Value + "<br/>";
                EmailBody += SepFunctions.LangText("User Information") + "<br/>";
                EmailBody += SepFunctions.LangText("Full Name") + " " + YourName.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Organization Name") + " " + CompanyName.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Street Address") + " " + StreetAddress.Value + "<br/>";
                EmailBody += SepFunctions.LangText("City") + " " + City.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Phone Number") + " " + PhoneNumber.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Email Address") + " " + EmailAddress.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Date for Speaker") + " " + SpeakerDate.Value + "<br/>";
                EmailBody += SepFunctions.LangText("Time for Speaker") + " " + SpeakerTime.Value;

                SepFunctions.Send_Email(jSpeaker.EmailAddress, EmailAddress.Value, SepFunctions.LangText("Speaker request from ~~" + SepFunctions.WebsiteName(SepFunctions.Get_Portal_ID()) + "~~"), EmailBody, GlobalVars.ModuleID);

                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully requested a speaker.") + "</div>";
                ModFormDiv.Visible = false;
            }
        }
    }
}