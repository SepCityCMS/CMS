// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="api.aspx.cs" company="SepCity, Inc.">
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
    using System.Xml;

    /// <summary>
    /// Class api.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class api : Page
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
        /// Outputoes the authentication URL.
        /// </summary>
        public void OutputoAuthURL()
        {
            oAuthURL.InnerHtml = SepFunctions.LangText("Do a HTTP Post to the following URL:") + "<br />" + SepFunctions.GetMasterDomain(false) + "OAuth/Authorize?client_id=" + OAuthClientID.Value + "&scope=|email|profile|&ReturnUrl=";
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("OAuth / API Configuration");
                    OAuthClientIDLabel.InnerText = SepFunctions.LangText("OAuth Client ID:");
                    ClientSecretLabel.InnerText = SepFunctions.LangText("OAuth Client Secret:");
                    CreationDateLabel.InnerText = SepFunctions.LangText("OAuth Creation Date:");
                    AuthorizedDomainsLabel.InnerText = SepFunctions.LangText("Domains Authorized to Access the OAuth API (One per a line):");
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
        /// Handles the Click event of the GenClientID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void GenClientID_Click(object sender, EventArgs e)
        {
            OAuthClientID.Value = SepFunctions.Base64Encode(SepFunctions.MD5Hash_Encrypt(Strings.Replace(SepFunctions.Generate_GUID(), "-", string.Empty)));
            CreationDate.Value = Strings.FormatDateTime(DateTime.Now, Strings.DateNamedFormat.ShortDate);
            using (var readLicense = new StreamReader(SepFunctions.GetDirValue("app_data") + "license.xml"))
            {
                ClientSecret.Value = SepFunctions.Base64Encode(SepFunctions.MD5Hash_Encrypt(Strings.Replace(SepFunctions.ParseXML("LicenseKey", readLicense.ReadToEnd()), "-", string.Empty)));
            }

            GenClientID.Text = SepFunctions.LangText("Regenerate Key");
            OutputoAuthURL();
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), true) == false)
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

            if (!Page.IsPostBack)
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "api.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "api.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            // Select the book node with the matching attribute value.
                            var root = doc.DocumentElement;

                            if (root.SelectSingleNode("/ROOTLEVEL/ClientID") != null) OAuthClientID.Value = root.SelectSingleNode("/ROOTLEVEL/ClientID").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/CreationDate") != null) CreationDate.Value = root.SelectSingleNode("/ROOTLEVEL/CreationDate").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/ClientSecret") != null) ClientSecret.Value = root.SelectSingleNode("/ROOTLEVEL/ClientSecret").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/AuthorizedDomains") != null) AuthorizedDomains.Value = root.SelectSingleNode("/ROOTLEVEL/AuthorizedDomains").InnerText;

                            GenClientID.Text = SepFunctions.LangText("Regenerate Key");
                            OutputoAuthURL();
                        }
                    }
                }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var strXml = string.Empty;

            strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            strXml += "<ROOTLEVEL>" + Environment.NewLine;
            strXml += "<ClientID>" + OAuthClientID.Value + "</ClientID>" + Environment.NewLine;
            strXml += "<CreationDate>" + CreationDate.Value + "</CreationDate>" + Environment.NewLine;
            strXml += "<ClientSecret>" + ClientSecret.Value + "</ClientSecret>" + Environment.NewLine;
            strXml += "<AuthorizedDomains>" + AuthorizedDomains.Value + "</AuthorizedDomains>" + Environment.NewLine;
            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "api.xml"))
            {
                outfile.Write(strXml);
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("API Settings successfully saved.") + "</div>";

            GenClientID.Text = SepFunctions.LangText("Regenerate Key");
            OutputoAuthURL();
        }
    }
}