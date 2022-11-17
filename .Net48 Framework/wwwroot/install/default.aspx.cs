// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="default.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using wwwroot.SepActivation;

    /// <summary>
    /// Class _default1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class _default1 : Page
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
        /// Gets the serial number.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>System.String.</returns>
        public string GetSerialNumber(string xmlData)
        {
            string sReturn;

            using (var soapClient = new activationSoapClient("activationSoap"))
            {
                sReturn = soapClient.New_License("23BDEE03-782D-4137-A05F-2D6E28D476FC", SepFunctions.ParseXML("UserName", xmlData), SepFunctions.MD5Hash_Encrypt(SepFunctions.ParseXML("Password", xmlData)), SepFunctions.ParseXML("EmailAddress", xmlData), SepFunctions.ParseXML("FirstName", xmlData), SepFunctions.ParseXML("LastName", xmlData), SepFunctions.ParseXML("StreetAddress", xmlData), SepFunctions.ParseXML("City", xmlData), SepFunctions.ParseXML("State", xmlData), SepFunctions.ParseXML("ZipCode", xmlData), "US", SepFunctions.ParseXML("PhoneNumber", xmlData), "What is your user name?", SepFunctions.ParseXML("UserName", xmlData), "Microsoft", true, string.Empty);
            }

            try
            {
                Session["LicenseUser"] = SepFunctions.ParseXML("UserName", xmlData);
                Session["LicensePass"] = SepFunctions.MD5Hash_Encrypt(SepFunctions.ParseXML("Password", xmlData));
                Session["LicenseKey"] = Strings.Replace(Strings.Replace(sReturn, "Successful (", string.Empty), ")", string.Empty);
            }
            catch
            {
                sReturn = "Invalid license information.";
            }

            return sReturn;
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            if (LicenceAgree.Checked == false)
            {
                ErrorMessage.InnerHtml = "You must accept the license agreement before you can continue.";
                return;
            }

            ErrorMessage.InnerHtml = string.Empty;

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "install_temp.xml"))
            {
                using (var sr = new StreamReader(SepFunctions.GetDirValue("app_data") + "install_temp.xml"))
                {
                    string xmlData = null;
                    xmlData = sr.ReadToEnd();
                    Session["DBAddress"] = SepFunctions.ParseXML("DBAddress", xmlData);
                    Session["DBName"] = SepFunctions.ParseXML("DBName", xmlData);
                    Session["DBUser"] = SepFunctions.ParseXML("DBUser", xmlData);
                    Session["DBPass"] = SepFunctions.ParseXML("DBPass", xmlData);

                    Session["PUserName"] = SepFunctions.ParseXML("UserName", xmlData);
                    Session["PPassword"] = SepFunctions.ParseXML("Password", xmlData);
                    Session["PEmailAddress"] = SepFunctions.ParseXML("EmailAddress", xmlData);
                    Session["PFirstName"] = SepFunctions.ParseXML("FirstName", xmlData);
                    Session["PLastName"] = SepFunctions.ParseXML("LastName", xmlData);
                    Session["PCountry"] = SepFunctions.ParseXML("Country", xmlData);

                    Session["PStreetAddress"] = SepFunctions.ParseXML("StreetAddress", xmlData);
                    Session["PCity"] = SepFunctions.ParseXML("City", xmlData);
                    Session["PState"] = SepFunctions.ParseXML("State", xmlData);
                    Session["PPostalCode"] = SepFunctions.ParseXML("ZipCode", xmlData);
                    Session["PGender"] = SepFunctions.ParseXML("Male", xmlData);
                    Session["PPhoneNumber"] = SepFunctions.ParseXML("PhoneNumber", xmlData);
                    Session["PBirthDate"] = DateTime.Today;

                    Session["SMTPServer"] = SepFunctions.ParseXML("SMTPServer", xmlData);
                    Session["SMTPUser"] = string.Empty;
                    Session["SMTPPass"] = string.Empty;

                    Session["PSecretQuestion"] = "What is your user name?";
                    Session["PSecretAnswer"] = SepFunctions.ParseXML("UserName", xmlData);

                    ErrorMessage.InnerHtml = GetSerialNumber(xmlData);

                    if (!string.IsNullOrWhiteSpace(ErrorMessage.InnerHtml)) ContinueButton.Enabled = false;
                }

                SepFunctions.Redirect("runinstall.aspx");

                ContinueButton.Text = "Finish Installer";
            }
            else
            {
                SepFunctions.Redirect("requirements.aspx");
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
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml") && !File.Exists(SepFunctions.GetDirValue("app_data") + "install_temp.xml")) SepFunctions.Redirect("installed.aspx");
            Label menuLabel = (Label)Master.FindControl("IntroSpan");
            if (menuLabel != null)
                menuLabel.Font.Bold = true;

            var agreementFile = HostingEnvironment.MapPath("~/install/") + "\\agreement.htm";

            if (File.Exists(agreementFile))
            {
                using (var objReader = new StreamReader(agreementFile))
                {
                    LicenseAgreement.InnerHtml = objReader.ReadToEnd();
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "The agreement.htm file is missing from your install directory.";
                ContinueButton.Visible = false;
            }
        }
    }
}