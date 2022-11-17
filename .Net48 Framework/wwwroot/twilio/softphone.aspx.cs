// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="softphone.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.twilio
{
    using global::Twilio;
    using global::Twilio.Rest.Api.V2010.Account;
    using SepCommon;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class softphone.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class softphone : Page
    {
        /// <summary>
        /// Handles the SelectedIndex event of the CallFromNumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void CallFromNumber_SelectedIndex(object sender, EventArgs e)
        {
            Write_Caller_Id();
        }

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
        /// Formats the number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>System.String.</returns>
        public string FormatNumber(string number)
        {
            return SepFunctions.FormatPhone(number);
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
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
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), false) == false)
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

            if (!IsPostBack)
            {
                TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));

                var incomingPhoneNumbers = IncomingPhoneNumberResource.Read();
                foreach (var record in incomingPhoneNumbers)
                {
                    CallFromNumber.Items.Add(new ListItem("Call From: " + record.PhoneNumber, SepCommon.SepCore.Strings.ToString(record.PhoneNumber)));
                }

                if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            if (root.SelectSingleNode("/Root/CallerId") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/CallerId").InnerText))
                            {
                                CallFromNumber.SelectedValue = root.SelectSingleNode("/Root/CallerId").InnerText;
                            }
                        }
                    }
                }

                Write_Caller_Id();
            }
        }

        /// <summary>
        /// Writes the caller identifier.
        /// </summary>
        private void Write_Caller_Id()
        {
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
            {
                string sXML = string.Empty;
                sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                sXML += "<Root>" + Environment.NewLine;
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);
                        var root = doc.DocumentElement;
                        sXML += "<ReadText>" + root.SelectSingleNode("/Root/ReadText").InnerText + "</ReadText>" + Environment.NewLine;
                        sXML += "<File>" + root.SelectSingleNode("/Root/File").InnerText + "</File>" + Environment.NewLine;
                        sXML += "<AppSID>" + root.SelectSingleNode("/Root/AppSID").InnerText + "</AppSID>" + Environment.NewLine;
                        sXML += "<CallerId>" + CallFromNumber.SelectedValue + "</CallerId>" + Environment.NewLine;
                        sXML += "</Root>" + Environment.NewLine;
                        using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                        {
                            outfile.Write(sXML);
                        }
                    }
                }
            }
        }
    }
}