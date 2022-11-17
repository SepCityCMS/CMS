// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="numbers.aspx.cs" company="SepCity, Inc.">
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
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class numbers.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class numbers : Page
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

            if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAccountSID")) && string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAuthToken")))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must setup the API information first.") + "</div>";
                PageManageGridView.Visible = false;
                return;
            }

            TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));

            var incomingPhoneNumbers = IncomingPhoneNumberResource.Read();

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            string sXML = string.Empty;
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
            {
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
                        if (root.SelectSingleNode("/Root/AppSID") == null || string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/AppSID").InnerText))
                        {
                            TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));

                            var application = ApplicationResource.Create(voiceMethod: global::Twilio.Http.HttpMethod.Post, smsMethod: global::Twilio.Http.HttpMethod.Post, smsFallbackMethod: global::Twilio.Http.HttpMethod.Post, voiceFallbackMethod: global::Twilio.Http.HttpMethod.Post, statusCallbackMethod: global::Twilio.Http.HttpMethod.Post, voiceUrl: new Uri(SepFunctions.GetMasterDomain(true) + "TwilioVoice"), voiceFallbackUrl: new Uri(SepFunctions.GetMasterDomain(true) + "TwilioVoice"), smsUrl: new Uri(SepFunctions.GetMasterDomain(true) + "TwilioVoice"), smsFallbackUrl: new Uri(SepFunctions.GetMasterDomain(true) + "TwilioVoice"), statusCallback: new Uri(SepFunctions.GetMasterDomain(true) + "TwilioVoice"), smsStatusCallback: new Uri(SepFunctions.GetMasterDomain(true) + "TwilioVoice"), friendlyName: "SepCity CMS");
                            sXML += "<AppSID>" + application.Sid + "</AppSID>" + Environment.NewLine;
                        }
                        else
                        {
                            sXML += "<AppSID>" + root.SelectSingleNode("/Root/AppSID").InnerText + "</AppSID>" + Environment.NewLine;
                        }
                    }
                }

                sXML += "</Root>" + Environment.NewLine;
            }
            else
            {
                sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                sXML += "<Root>" + Environment.NewLine;
                sXML += "<File></File>";
                sXML += "<ReadText></ReadText>";

                TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));

                var application = ApplicationResource.Create(voiceMethod: global::Twilio.Http.HttpMethod.Get, voiceUrl: new Uri(SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx"), friendlyName: "SepCity CMS");
                sXML += "<AppSID>" + application.Sid + "</AppSID>" + Environment.NewLine;
                sXML += "</Root>" + Environment.NewLine;
            }

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
            {
                outfile.Write(sXML);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            var tbl = new DataTable();
            var col1 = new DataColumn("PhoneNumber", typeof(string));
            var col2 = new DataColumn("Mms", typeof(string));
            var col3 = new DataColumn("Sms", typeof(string));
            var col4 = new DataColumn("Voice", typeof(string));
            var col5 = new DataColumn("FlowID", typeof(string));
            var col6 = new DataColumn("FlowName", typeof(string));
            tbl.Columns.Add(col1);
            tbl.Columns.Add(col2);
            tbl.Columns.Add(col3);
            tbl.Columns.Add(col4);
            tbl.Columns.Add(col5);
            tbl.Columns.Add(col6);
            foreach (var record in incomingPhoneNumbers)
            {
                var row = tbl.NewRow();
                row["PhoneNumber"] = record.PhoneNumber;
                row["Mms"] = record.Capabilities.Mms;
                row["Sms"] = record.Capabilities.Sms;
                row["Voice"] = record.Capabilities.Voice;
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT FlowID, NumberID, (SELECT TOP 1 FlowName FROM TwilioFlows WHERE FlowID=TwilioNumbers.FlowID) AS FlowName FROM TwilioNumbers WHERE PhoneNumber=@PhoneNumber", conn))
                    {
                        Console.Write(record.VoiceApplicationSid);
                        cmd.Parameters.AddWithValue("@PhoneNumber", SepCommon.SepCore.Strings.ToString(record.PhoneNumber));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                using (var cmd2 = new SqlCommand("INSERT INTO TwilioNumbers (NumberID, PhoneNumber, SID, FlowID) VALUES(@NumberID, @PhoneNumber, @SID, '0')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@NumberID", SepFunctions.GetIdentity());
                                    cmd2.Parameters.AddWithValue("@PhoneNumber", SepCommon.SepCore.Strings.ToString(record.PhoneNumber));
                                    cmd2.Parameters.AddWithValue("@SID", record.Sid);
                                    cmd2.ExecuteNonQuery();
                                }

                                row["FlowID"] = string.Empty;
                                row["FlowName"] = string.Empty;
                            }
                            else
                            {
                                RS.Read();
                                row["FlowID"] = SepFunctions.openNull(RS["FlowID"]);
                                row["FlowName"] = SepFunctions.openNull(RS["FlowName"]);
                            }

                        }
                    }

                    tbl.Rows.Add(row);
                }
            }

            ManageGridView.DataSource = tbl;
            ManageGridView.DataBind();
        }
    }
}