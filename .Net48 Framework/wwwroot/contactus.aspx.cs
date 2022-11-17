// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="contactus.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using SepControls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class contactus.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class contactus : Page
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
                    YourNameLabel.InnerText = SepFunctions.LangText("Your Name:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    AddressLabel.InnerText = SepFunctions.LangText("City/State:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    FaxNumberLabel.InnerText = SepFunctions.LangText("Fax Number:");
                    FileUploadLabel.InnerText = SepFunctions.LangText("Attach file(s) to your message to us:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    CommentsLabel.InnerText = SepFunctions.LangText("Comments:");
                    YourNameRequired.ErrorMessage = SepFunctions.LangText("~~Your Name~~ is required.");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
                    CommentsRequired.ErrorMessage = SepFunctions.LangText("~~Comments~~ is required.");
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

            GlobalVars.ModuleID = 4;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            if (!IsPostBack)
            {
                PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);
            }

            cReplace.Dispose();

            switch (SepFunctions.Setup(GlobalVars.ModuleID, "ContactFileTypes"))
            {
                case "Any":
                    FileUpload.FileType = UploadFiles.EFileTypes.Any;
                    break;

                case "Audio":
                    FileUpload.FileType = UploadFiles.EFileTypes.Audio;
                    break;

                case "Document":
                    FileUpload.FileType = UploadFiles.EFileTypes.Document;
                    break;

                case "Images":
                    FileUpload.FileType = UploadFiles.EFileTypes.Images;
                    break;

                case "Software":
                    FileUpload.FileType = UploadFiles.EFileTypes.Software;
                    break;

                case "Video":
                    FileUpload.FileType = UploadFiles.EFileTypes.Video;
                    break;

                default:
                    ContactUploadRow.Visible = false;
                    break;
            }

            FileUpload.UserID = SepFunctions.Session_User_ID();
            if (string.IsNullOrWhiteSpace(UploadID.Value)) UploadID.Value = Strings.ToString(SepFunctions.GetIdentity());
            FileUpload.ContentID = UploadID.Value;
            if (SepFunctions.toLong(SepFunctions.Setup(4, "ContactMaxFiles")) > 1) FileUpload.Mode = UploadFiles.EInputMode.MultipleFiles;
            else FileUpload.Mode = UploadFiles.EInputMode.SingleFile;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactStreetAddress") != "Yes") StreetAddressRow.Visible = false;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactAddress") != "Yes") AddressRow.Visible = false;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactPhoneNumber") != "Yes") PhoneNumberRow.Visible = false;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactFaxNumber") != "Yes") FaxNumberRow.Visible = false;

            if (!Page.IsPostBack) EmailAddress.Value = SepFunctions.GetUserInformation("EmailAddress");
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
            if (Recaptcha1.Validate() == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid reCaptcha.") + "</div>";
                return;
            }

            var GetContactEmail = SepFunctions.Setup(GlobalVars.ModuleID, "ContactEmail");
            var GetEmailSubject = SepFunctions.Setup(GlobalVars.ModuleID, "ContactEmailSubject");
            var GetEmailBody = SepFunctions.Setup(GlobalVars.ModuleID, "ContactEmailBody");

            string EmailBody;
            string EmailSubject;
            if (!string.IsNullOrWhiteSpace(GetEmailSubject) && !string.IsNullOrWhiteSpace(GetEmailBody))
            {

                string FromEmailAddress;
                if (!string.IsNullOrWhiteSpace(GetContactEmail))
                    FromEmailAddress = GetContactEmail;
                else
                    FromEmailAddress = SepFunctions.Setup(GlobalVars.ModuleID, "AdminEmailAddress");
                if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("EmailAddress")) && SepFunctions.Get_Portal_ID() > 0)
                {
                    FromEmailAddress = SepFunctions.PortalSetup("EmailAddress");
                }

                EmailSubject = GetEmailSubject;
                EmailBody = GetEmailBody + "<br/><br/>" + SepFunctions.LangText("Body of your email:") + "<br/><br/>" + Strings.Replace(Comments.Value, Environment.NewLine, "<br/>");
                SepFunctions.Send_Email(EmailAddress.Value, FromEmailAddress, EmailSubject, EmailBody, GlobalVars.ModuleID);
            }

            if (SepFunctions.isUserPage()) GetContactEmail = SepFunctions.GetUserInformation("EmailAddress", SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")));


            string ToEmailAddress;
            if (!string.IsNullOrWhiteSpace(GetContactEmail))
                ToEmailAddress = GetContactEmail;
            else
                ToEmailAddress = SepFunctions.Setup(GlobalVars.ModuleID, "AdminEmailAddress");
            if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("EmailAddress")) && SepFunctions.Get_Portal_ID() > 0)
            {
                ToEmailAddress = SepFunctions.PortalSetup("EmailAddress");
            }

            EmailSubject = SepFunctions.LangText("Contact Us Form");
            EmailBody = string.Empty;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactShowName") == "Yes") EmailBody += SepFunctions.LangText("~~" + YourName.Value + "~~ has sent you an email") + "<br/><br/>";
            EmailBody += Strings.Replace(Comments.Value, Environment.NewLine, "<br/>") + "<br/><br/>";
            if (!string.IsNullOrWhiteSpace(YourName.Value)) EmailBody += "<br/>" + SepFunctions.LangText("Full Name:") + " " + YourName.Value;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactStreetAddress") == "Yes") EmailBody += "<br/>" + SepFunctions.LangText("Street Address:") + " " + StreetAddress.Value;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactAddress") == "Yes") EmailBody += "<br/>" + SepFunctions.LangText("City, State, Zip Code:") + " " + Address.Value;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactPhoneNumber") == "Yes") EmailBody += "<br/>" + SepFunctions.LangText("Phone Number:") + " " + PhoneNumber.Value;
            if (SepFunctions.Setup(GlobalVars.ModuleID, "ContactFaxNumber") == "Yes") EmailBody += "<br/>" + SepFunctions.LangText("Fax Number:") + " " + FaxNumber.Value;

            SepFunctions.Send_Email(ToEmailAddress, EmailAddress.Value, EmailSubject, EmailBody, GlobalVars.ModuleID, UniqueID: SepFunctions.toLong(UploadID.Value));

            ContactDiv.Visible = false;
            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Thank you for contacting us, we will get back to your email shortly") + "</div>";
        }
    }
}