// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="twilio_voice.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.twilio
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web;
    using System.Xml;
    using Twilio.TwiML;
    using Play = Twilio.TwiML.Voice.Play;

    /// <summary>
    /// Class twilio_voice.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class twilio_voice : System.Web.UI.Page
    {
        // public string Lookup_Phone_Number(string sPhone)
        // {
        // SqlDataReader RS = null;

        // var customerId = ""; long cCount = 0;

        // using (var conn = new SqlConnection(SepFunctions.Database_Connection())) { conn.Open();

        // using (var cmd = new SqlCommand("SELECT Customer_Id FROM SEP_Customers WHERE
        // Phone_Number=@Phone_Number", conn)) { cmd.Parameters.AddWithValue("@Phone_Number",
        // sPhone); RS = cmd.ExecuteReader(); if (RS.HasRows) while (RS.Read()) { cCount = cCount +
        // 1; if (cCount > 1) customerId += ","; customerId += SepFunctions.openNull(RS["Customer_Id"]); }

        // } } }

        // return customerId;
        // }

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
            SepCommon.SepCore.Response.Clear();
            SepCommon.SepCore.Response.AddHeader("ContentType", "text/xml");

            if (SepCommon.SepCore.Request.Item("DoAction") == "Voicemail")
            {
                var response = new VoiceResponse();
                try
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            if (root.SelectSingleNode("/Root/File") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/File").InnerText))
                            {
                                Play file = new global::Twilio.TwiML.Voice.Play
                                {
                                    Url = new System.Uri(SepFunctions.GetMasterDomain(true) + "images/" + root.SelectSingleNode("/Root/File").InnerText)
                                };
#pragma warning disable CS0618 // 'VoiceResponse.Play(Play)' is obsolete: 'This method is deprecated, use .Append() instead.'
                                response.Play(file);
#pragma warning restore CS0618 // 'VoiceResponse.Play(Play)' is obsolete: 'This method is deprecated, use .Append() instead.'
                            }
                            else
                            {
                                if (root.SelectSingleNode("/Root/ReadText") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/ReadText").InnerText))
                                {
                                    response.Say(root.SelectSingleNode("/Root/ReadText").InnerText);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    response.Say("Please leave a message after the beep.");
                }

                response.Record();
                response.Hangup();
                HttpContext.Current.Response.Write(SepCommon.SepCore.Strings.ToString(response));
                return;
            }

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FlowID")))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM TwilioFlows WHERE FlowID=@FlowID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FlowID", SepCommon.SepCore.Request.Item("FlowID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["FlowConfig"])))
                                {
                                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                                    using (XmlReader reader = XmlReader.Create(new StringReader(SepFunctions.openNull(RS["FlowConfig"]))))
                                    {
                                        doc.Load(reader);
                                        var root = doc.DocumentElement;
                                        if (root.SelectSingleNode("/Root/Action") != null)
                                        {
                                            switch (root.SelectSingleNode("/Root/Action").InnerText)
                                            {
                                                case "Group":
                                                    Call_Group(SepCommon.SepCore.Strings.Replace(root.SelectSingleNode("/Root/UniqueID").InnerText, "Group:", string.Empty));
                                                    break;

                                                case "User":
                                                    Call_User(SepCommon.SepCore.Strings.Replace(root.SelectSingleNode("/Root/UniqueID").InnerText, "User:", string.Empty));
                                                    break;

                                                case "Device":
                                                    Call_Device(SepCommon.SepCore.Strings.Replace(root.SelectSingleNode("/Root/UniqueID").InnerText, "Device:", string.Empty));
                                                    break;

                                                default:

                                                    // =============================================================================================================
                                                    // IVR Menu
                                                    if (SepCommon.SepCore.Request.Item("DoAction") == "HandleDigits")
                                                    {
                                                        var validNumber = false;
                                                        for (var i = 1; i < 10; i++)
                                                        {
                                                            if (root.SelectSingleNode("/Root/MenuOptions/Option" + i) != null && root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Number") != null && root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Number").InnerText) && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText))
                                                            {
                                                                if (SepCommon.SepCore.Request.Item("Digits") == root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Number").InnerText)
                                                                {
                                                                    if (SepCommon.SepCore.Strings.InStr(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText, "Device:") > 0)
                                                                    {
                                                                        validNumber = true;
                                                                        Call_Device(SepCommon.SepCore.Strings.Replace(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText, "Device:", string.Empty));
                                                                    }

                                                                    if (SepCommon.SepCore.Strings.InStr(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText, "Group:") > 0)
                                                                    {
                                                                        validNumber = true;
                                                                        Call_Group(SepCommon.SepCore.Strings.Replace(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText, "Group:", string.Empty));
                                                                    }

                                                                    if (SepCommon.SepCore.Strings.InStr(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText, "User:") > 0)
                                                                    {
                                                                        validNumber = true;
                                                                        Call_User(SepCommon.SepCore.Strings.Replace(root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText, "User:", string.Empty));
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (validNumber == false)
                                                        {
                                                            HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
                                                            HttpContext.Current.Response.Write("<Response>" + Environment.NewLine);
                                                            HttpContext.Current.Response.Write("<Say voice=\"woman\">You have entered an invalid response.</Say>" + Environment.NewLine);
                                                            HttpContext.Current.Response.Write("<Redirect>twilio_voice.aspx?FlowID=" + SepCommon.SepCore.Request.Item("FlowID") + "</Redirect>" + Environment.NewLine);
                                                            HttpContext.Current.Response.Write("</Response>" + Environment.NewLine);
                                                        }

                                                        SepCommon.SepCore.Response.End();
                                                        return;
                                                    }

                                                    HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
                                                    HttpContext.Current.Response.Write("<Response>" + Environment.NewLine);
                                                    HttpContext.Current.Response.Write("<Gather action=\"twilio_voice.aspx?FlowID=" + SepCommon.SepCore.Request.Item("FlowID") + "&amp;DoAction=HandleDigits\" numDigits=\"1\">" + Environment.NewLine);
                                                    if (root.SelectSingleNode("/Root/RecordingUpload") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/RecordingUpload").InnerText))
                                                    {
                                                        HttpContext.Current.Response.Write("<Play>" + SepFunctions.GetMasterDomain(true) + "images/" + root.SelectSingleNode("/Root/RecordingUpload").InnerText + "</Play>" + Environment.NewLine);
                                                    }
                                                    else
                                                    {
                                                        if (root.SelectSingleNode("/Root/RecordingText") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/RecordingText").InnerText))
                                                        {
                                                            HttpContext.Current.Response.Write("<Say>" + root.SelectSingleNode("/Root/RecordingText").InnerText + "</Say>" + Environment.NewLine);
                                                        }
                                                    }

                                                    HttpContext.Current.Response.Write("</Gather>" + Environment.NewLine);
                                                    HttpContext.Current.Response.Write("<Redirect>twilio_voice.aspx?FlowID=" + SepCommon.SepCore.Request.Item("FlowID") + "</Redirect>" + Environment.NewLine);
                                                    HttpContext.Current.Response.Write("</Response>" + Environment.NewLine);
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            SepCommon.SepCore.Response.End();

            // customerId = Lookup_Phone_Number(SepCommon.SepCore.Request.Item("From")); if
            // (!string.IsNullOrWhiteSpace(customerId)) { var lCommon = new Functions(); var
            // arrCustomerId = Strings.Split(customerId, ","); for (var i = 0; i <=
            // Information.UBound(arrCustomerId); i++) lCommon.WriteActivity(arrCustomerId[i],
            // "Called", "Customer has called into SepCity."); }

            // break;
            // }
        }

        /// <summary>
        /// Calls the device.
        /// </summary>
        /// <param name="DeviceID">The device identifier.</param>
        private void Call_Device(string DeviceID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd2 = new SqlCommand("SELECT * FROM TwilioDevices WHERE DeviceID=@DeviceID", conn))
                {
                    cmd2.Parameters.AddWithValue("@DeviceID", DeviceID);
                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                    {
                        if (RS2.HasRows)
                        {
                            RS2.Read();
                            HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Response>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Dial callerId=\"" + SepCommon.SepCore.Request.Item("Called") + "\" action=\"" + SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?DoAction=Voicemail&amp;GoTo=Device&amp;UniqueID=" + DeviceID + "\" timeout=\"20\" sequential=\"true\">" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Number>" + SepFunctions.FormatPhone(SepFunctions.openNull(RS2["PhoneNumber"])) + "</Number>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("</Dial>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("</Response>" + Environment.NewLine);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calls the group.
        /// </summary>
        /// <param name="GroupIDs">The group i ds.</param>
        private void Call_Group(string GroupIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd2 = new SqlCommand("SELECT Members.PhoneNumber,Members.FirstName FROM TwilioUsers AS TU, Members WHERE Members.UserID=TU.UserID AND TU.GroupIDs LIKE '%" + SepFunctions.FixWord(GroupIDs) + "%'", conn))
                {
                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                    {
                        if (RS2.HasRows)
                        {
                            HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Response>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Dial callerId=\"" + SepCommon.SepCore.Request.Item("Called") + "\" timeout=\"20\" action=\"" + SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?DoAction=Voicemail&amp;GoTo=Group&amp;UniqueID=" + GroupIDs + "\" sequential=\"true\">" + Environment.NewLine);
                            while (RS2.Read())
                            {
                                HttpContext.Current.Response.Write("<Number>" + SepFunctions.FormatPhone(SepFunctions.openNull(RS2["PhoneNumber"])) + "</Number>" + Environment.NewLine);
                            }

                            HttpContext.Current.Response.Write("</Dial>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("</Response>" + Environment.NewLine);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calls the user.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        private void Call_User(string UserID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd2 = new SqlCommand("SELECT Members.PhoneNumber,Members.FirstName FROM TwilioUsers AS TU, Members WHERE Members.UserID=TU.UserID AND TU.UserID=@UserID", conn))
                {
                    cmd2.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                    {
                        if (RS2.HasRows)
                        {
                            RS2.Read();
                            HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Response>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Dial callerId=\"" + SepCommon.SepCore.Request.Item("Called") + "\" timeout=\"20\" action=\"" + SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?DoAction=Voicemail&amp;GoTo=User&amp;UniqueID=" + UserID + "\" sequential=\"true\">" + Environment.NewLine);
                            HttpContext.Current.Response.Write("<Number>" + SepFunctions.FormatPhone(SepFunctions.openNull(RS2["PhoneNumber"])) + "</Number>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("</Dial>" + Environment.NewLine);
                            HttpContext.Current.Response.Write("</Response>" + Environment.NewLine);
                        }
                    }
                }
            }
        }
    }
}