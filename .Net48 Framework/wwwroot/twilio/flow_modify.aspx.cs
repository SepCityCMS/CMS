// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="flow_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Data.SqlClient;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class flow_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class flow_modify : Page
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
        /// Handles the SelectedIndex event of the IncomingCall control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void IncomingCall_SelectedIndex(object sender, EventArgs e)
        {
            NumberIDsDiv.InnerHtml = string.Empty;
            Draw_Phone_Numbers(FlowID.Value);
            Draw_Option(IncomingCall.SelectedValue);
        }

        /// <summary>
        /// Keeps the uploaded file.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Keep_Uploaded_File()
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioFlows WHERE FlowID=@FlowID", conn))
                {
                    cmd.Parameters.AddWithValue("@FlowID", FlowID.Value);
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
                                    if (root.SelectSingleNode("/Root/RecordingUpload") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/RecordingUpload").InnerText))
                                    {
                                        return SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/RecordingUpload").InnerText;
                                    }
                                }
                            }
                        }

                    }
                }
            }

            return string.Empty;
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

            if (SepCommon.SepCore.Request.Item("DoAction") == "DeleteRecording" && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FlowID")))
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
                                        if (root.SelectSingleNode("/Root/RecordingUpload") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/RecordingUpload").InnerText))
                                        {
                                            if (File.Exists(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/RecordingUpload").InnerText))
                                            {
                                                File.Delete(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/RecordingUpload").InnerText);
                                            }

                                            root.SelectSingleNode("/Root/RecordingUpload").InnerText = string.Empty;

                                            using (var cmd2 = new SqlCommand("UPDATE TwilioFlows SET FlowConfig=@FlowConfig WHERE FlowID=@FlowID", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@FlowConfig", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + root.OuterXml);
                                                cmd2.Parameters.AddWithValue("@FlowID", SepCommon.SepCore.Request.Item("FlowID"));
                                                cmd2.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FlowID")))
            {
                Draw_Phone_Numbers(SepCommon.SepCore.Request.Item("FlowID"));
                Draw_Flow_Screen(SepCommon.SepCore.Request.Item("FlowID"));
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Draw_Phone_Numbers();
                    Draw_Option(string.Empty);
                }
            }

            if (string.IsNullOrWhiteSpace(FlowID.Value))
            {
                FlowID.Value = SepCommon.SepCore.Strings.ToString(SepFunctions.GetIdentity());
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sXML = string.Empty;
            switch (IncomingCall.SelectedValue)
            {
                case "Group":
                    sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                    sXML += "<Root>" + Environment.NewLine;
                    sXML += "<Action>Group</Action>" + Environment.NewLine;
                    sXML += "<UniqueID>" + SepCommon.SepCore.Request.PairValue("CallGroup") + "</UniqueID>" + Environment.NewLine;
                    sXML += "</Root>" + Environment.NewLine;
                    break;

                case "User":
                    sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                    sXML += "<Root>" + Environment.NewLine;
                    sXML += "<Action>User</Action>" + Environment.NewLine;
                    sXML += "<UniqueID>" + SepCommon.SepCore.Request.PairValue("CallUser") + "</UniqueID>" + Environment.NewLine;
                    sXML += "</Root>" + Environment.NewLine;
                    break;

                case "Device":
                    sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                    sXML += "<Root>" + Environment.NewLine;
                    sXML += "<Action>Device</Action>" + Environment.NewLine;
                    sXML += "<UniqueID>" + SepCommon.SepCore.Request.PairValue("CallDevice") + "</UniqueID>" + Environment.NewLine;
                    sXML += "</Root>" + Environment.NewLine;
                    break;

                default:
                    sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                    sXML += "<Root>" + Environment.NewLine;
                    sXML += "<Action>Menu</Action>" + Environment.NewLine;
                    sXML += "<RecordingText>" + SepCommon.SepCore.Request.PairValue("RecordingText") + "</RecordingText>" + Environment.NewLine;
                    try
                    {
                        HttpPostedFile PostedFile = Request.Files[0];
                        if (PostedFile.ContentLength > 0)
                        {
                            var sFileExt = string.Empty;
                            var sFileName = string.Empty;
                            sFileExt = SepCommon.SepCore.Strings.LCase(Path.GetExtension(PostedFile.FileName));
                            sFileName = SepFunctions.GetIdentity() + sFileExt;
                            if (sFileExt == ".mp3")
                            {
                                PostedFile.SaveAs(SepFunctions.GetDirValue("images") + sFileName);
                                sXML += "<RecordingUpload>" + sFileName + "</RecordingUpload>" + Environment.NewLine;
                                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                                {
                                    conn.Open();
                                    using (var cmd = new SqlCommand("SELECT * FROM TwilioFlows WHERE FlowID=@FlowID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@FlowID", FlowID.Value);
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
                                                        if (root.SelectSingleNode("/Root/RecordingUpload") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/RecordingUpload").InnerText))
                                                        {
                                                            if (File.Exists(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/RecordingUpload").InnerText))
                                                            {
                                                                File.Delete(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/RecordingUpload").InnerText);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                sXML += "<RecordingUpload>" + Keep_Uploaded_File() + "</RecordingUpload>" + Environment.NewLine;
                            }
                        }
                        else
                        {
                            sXML += "<RecordingUpload>" + Keep_Uploaded_File() + "</RecordingUpload>" + Environment.NewLine;
                        }
                    }
                    catch
                    {
                        sXML += "<RecordingUpload>" + Keep_Uploaded_File() + "</RecordingUpload>" + Environment.NewLine;
                    }

                    sXML += "<MenuOptions>" + Environment.NewLine;
                    for (var i = 1; i < 10; i++)
                    {
                        sXML += "<Option" + i + ">" + Environment.NewLine;
                        sXML += "<Number>" + SepCommon.SepCore.Request.PairValue("PressNumber" + i) + "</Number>" + Environment.NewLine;
                        if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.PairValue("PressNumber" + i)))
                        {
                            sXML += "<Action>" + SepCommon.SepCore.Request.PairValue("PerformAction" + i) + "</Action>" + Environment.NewLine;
                        }
                        else
                        {
                            sXML += "<Action></Action>" + Environment.NewLine;
                        }

                        sXML += "</Option" + i + ">" + Environment.NewLine;
                    }

                    sXML += "</MenuOptions>" + Environment.NewLine;
                    sXML += "</Root>" + Environment.NewLine;
                    break;
            }

            bool bUpdate = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioFlows WHERE FlowID=@FlowID", conn))
                {
                    cmd.Parameters.AddWithValue("@FlowID", FlowID.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bUpdate = true;
                        }

                    }
                }

                string SqlStr;
                if (bUpdate)
                {
                    SqlStr = "UPDATE TwilioFlows SET FlowName=@FlowName, FlowConfig=@FlowConfig WHERE FlowID=@FlowID";
                }
                else
                {
                    SqlStr = "INSERT INTO TwilioFlows (FlowID, FlowName, FlowConfig) VALUES(@FlowID, @FlowName, @FlowConfig)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@FlowID", FlowID.Value);
                    cmd.Parameters.AddWithValue("@FlowName", FlowName.Value);
                    cmd.Parameters.AddWithValue("@FlowConfig", sXML);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("UPDATE TwilioNumbers SET FlowID='0' WHERE FlowID=@FlowID", conn))
                {
                    cmd.Parameters.AddWithValue("@FlowID", FlowID.Value);
                    cmd.ExecuteNonQuery();
                }

                string[] arrNumbers = null;
                arrNumbers = SepCommon.SepCore.Strings.Split(SepCommon.SepCore.Request.Item("NumberIDs"), ",");
                for (var i = 0; i <= SepCommon.SepCore.Information.UBound(arrNumbers); i++)
                {
                    using (var cmd = new SqlCommand("UPDATE TwilioNumbers SET FlowID=@FlowID WHERE NumberID=@NumberID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FlowID", FlowID.Value);
                        cmd.Parameters.AddWithValue("@NumberID", arrNumbers[i]);
                        cmd.ExecuteNonQuery();
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
                                if (root.SelectSingleNode("/Root/AppSID") != null)
                                {
                                    using (var cmd = new SqlCommand("SELECT * FROM TwilioNumbers WHERE NumberID=@NumberID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@NumberID", arrNumbers[i]);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));

                                                IncomingPhoneNumberResource.Update(accountSid: SepFunctions.Setup(989, "TwilioAccountSID"), smsUrl: new Uri(SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?FlowID=" + FlowID.Value), voiceUrl: new Uri(SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?FlowID=" + FlowID.Value), smsFallbackUrl: new Uri(SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?FlowID=" + FlowID.Value), voiceFallbackUrl: new Uri(SepFunctions.GetMasterDomain(true) + "twilio/twilio_voice.aspx?FlowID=" + FlowID.Value), voiceApplicationSid: string.Empty, smsApplicationSid: string.Empty, pathSid: SepFunctions.openNull(RS["SID"]));
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            NumberIDsDiv.InnerHtml = string.Empty;

            Draw_Phone_Numbers(FlowID.Value);
            Draw_Flow_Screen(FlowID.Value);

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Flow has been successfully saved.") + "</div>";
        }

        /// <summary>
        /// Draws the call device.
        /// </summary>
        /// <param name="sConfig">The s configuration.</param>
        private void Draw_Call_Device(string sConfig = "")
        {
            var selectedValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(sConfig))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (XmlReader reader = XmlReader.Create(new StringReader(sConfig)))
                {
                    doc.Load(reader);
                    var root = doc.DocumentElement;
                    if (root.SelectSingleNode("/Root/Action") != null && root.SelectSingleNode("/Root/UniqueID") != null)
                    {
                        if (root.SelectSingleNode("/Root/Action").InnerText == "Device")
                        {
                            selectedValue = root.SelectSingleNode("/Root/UniqueID").InnerText;
                        }
                    }
                }
            }

            var lBreak = new HtmlGenericControl("hr");
            lBreak.Attributes.Add("style", "display: block;height: 1px;border: 0;border-top: 1px solid #000000;margin: 1em 0;padding:0;");
            CallActions.Controls.Add(lBreak);

            var divWrapper = new HtmlGenericControl("div");
            divWrapper.Attributes.Add("style", "padding-left:60px;");

            var hHeader = new HtmlGenericControl("h2")
            {
                InnerText = "Call Device"
            };
            divWrapper.Controls.Add(hHeader);

            DropDownList dlAction = new DropDownList
            {
                ID = "CallDevice"
            };
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioDevices ORDER BY DeviceName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            dlAction.Items.Add(new ListItem(SepFunctions.openNull(RS["DeviceName"]), "Device:" + SepFunctions.openNull(RS["DeviceID"])));
                        }

                    }
                }
            }

            divWrapper.Controls.Add(dlAction);
            if (!string.IsNullOrWhiteSpace(selectedValue))
            {
                dlAction.SelectedValue = selectedValue;
            }

            CallActions.Controls.Add(divWrapper);
        }

        /// <summary>
        /// Draws the call group.
        /// </summary>
        /// <param name="sConfig">The s configuration.</param>
        private void Draw_Call_Group(string sConfig = "")
        {
            var selectedValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(sConfig))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (XmlReader reader = XmlReader.Create(new StringReader(sConfig)))
                {
                    doc.Load(reader);
                    var root = doc.DocumentElement;
                    if (root.SelectSingleNode("/Root/Action") != null && root.SelectSingleNode("/Root/UniqueID") != null)
                    {
                        if (root.SelectSingleNode("/Root/Action").InnerText == "Group")
                        {
                            selectedValue = root.SelectSingleNode("/Root/UniqueID").InnerText;
                        }
                    }
                }
            }

            var lBreak = new HtmlGenericControl("hr");
            lBreak.Attributes.Add("style", "display: block;height: 1px;border: 0;border-top: 1px solid #000000;margin: 1em 0;padding:0;");
            CallActions.Controls.Add(lBreak);

            var divWrapper = new HtmlGenericControl("div");
            divWrapper.Attributes.Add("style", "padding-left:60px;");

            var hHeader = new HtmlGenericControl("h2")
            {
                InnerText = "Call Group"
            };
            divWrapper.Controls.Add(hHeader);

            DropDownList dlAction = new DropDownList
            {
                ID = "CallGroup"
            };
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioGroups ORDER BY GroupName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            dlAction.Items.Add(new ListItem(SepFunctions.openNull(RS["GroupName"]), "Group:" + SepFunctions.openNull(RS["GroupID"])));
                        }

                    }
                }
            }

            divWrapper.Controls.Add(dlAction);
            if (!string.IsNullOrWhiteSpace(selectedValue))
            {
                dlAction.SelectedValue = selectedValue;
            }

            CallActions.Controls.Add(divWrapper);
        }

        /// <summary>
        /// Draws the call user.
        /// </summary>
        /// <param name="sConfig">The s configuration.</param>
        private void Draw_Call_User(string sConfig = "")
        {
            var selectedValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(sConfig))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (XmlReader reader = XmlReader.Create(new StringReader(sConfig)))
                {
                    doc.Load(reader);
                    var root = doc.DocumentElement;
                    if (root.SelectSingleNode("/Root/Action") != null && root.SelectSingleNode("/Root/UniqueID") != null)
                    {
                        if (root.SelectSingleNode("/Root/Action").InnerText == "User")
                        {
                            selectedValue = root.SelectSingleNode("/Root/UniqueID").InnerText;
                        }
                    }
                }
            }

            var lBreak = new HtmlGenericControl("hr");
            lBreak.Attributes.Add("style", "display: block;height: 1px;border: 0;border-top: 1px solid #000000;margin: 1em 0;padding:0;");
            CallActions.Controls.Add(lBreak);

            var divWrapper = new HtmlGenericControl("div");
            divWrapper.Attributes.Add("style", "padding-left:60px;");

            var hHeader = new HtmlGenericControl("h2")
            {
                InnerText = "Call User"
            };
            divWrapper.Controls.Add(hHeader);

            DropDownList dlAction = new DropDownList
            {
                ID = "CallUser"
            };
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TU.UserID, Members.UserName FROM TwilioUsers AS TU, Members WHERE TU.UserID=Members.UserID ORDER BY Members.UserName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            dlAction.Items.Add(new ListItem(SepFunctions.openNull(RS["UserName"]), "User:" + SepFunctions.openNull(RS["UserID"])));
                        }

                    }
                }
            }

            divWrapper.Controls.Add(dlAction);
            if (!string.IsNullOrWhiteSpace(selectedValue))
            {
                dlAction.SelectedValue = selectedValue;
            }

            CallActions.Controls.Add(divWrapper);
        }

        /// <summary>
        /// Draws the flow screen.
        /// </summary>
        /// <param name="FlowId">The flow identifier.</param>
        private void Draw_Flow_Screen(string FlowId)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioFlows WHERE FlowID=@FlowID", conn))
                {
                    cmd.Parameters.AddWithValue("@FlowID", FlowId);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            FlowID.Value = SepFunctions.openNull(RS["FlowID"]);
                            FlowName.Value = SepFunctions.openNull(RS["FlowName"]);
                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["FlowConfig"])))
                            {
                                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                                using (XmlReader reader = XmlReader.Create(new StringReader(SepFunctions.openNull(RS["FlowConfig"]))))
                                {
                                    doc.Load(reader);
                                    var root = doc.DocumentElement;
                                    if (root.SelectSingleNode("/Root/Action") != null)
                                    {
                                        IncomingCall.SelectedValue = root.SelectSingleNode("/Root/Action").InnerText;
                                    }
                                }
                            }

                            ModifyLegend.InnerText = SepFunctions.LangText("Edit Flow");
                            Draw_Option(IncomingCall.SelectedValue, SepFunctions.openNull(RS["FlowConfig"]));
                        }
                        else
                        {
                            Draw_Option(string.Empty);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Draws the main menu.
        /// </summary>
        /// <param name="sConfig">The s configuration.</param>
        private void Draw_Main_Menu(string sConfig = "")
        {
            var RecordingTextValue = string.Empty;
            var RecordingUploadValue = string.Empty;
            XmlElement root = null;
            bool hasConfig = false;

            if (!string.IsNullOrWhiteSpace(sConfig))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (XmlReader reader = XmlReader.Create(new StringReader(sConfig)))
                {
                    doc.Load(reader);
                    root = doc.DocumentElement;
                    if (root.SelectSingleNode("/Root/Action") != null && root.SelectSingleNode("/Root/MenuOptions") != null)
                    {
                        if (root.SelectSingleNode("/Root/Action").InnerText == "Menu")
                        {
                            hasConfig = true;
                            if (root.SelectSingleNode("/Root/RecordingText") != null)
                            {
                                RecordingTextValue = root.SelectSingleNode("/Root/RecordingText").InnerText;
                            }

                            if (root.SelectSingleNode("/Root/RecordingUpload") != null)
                            {
                                RecordingUploadValue = root.SelectSingleNode("/Root/RecordingUpload").InnerText;
                            }
                        }
                    }
                }
            }

            var lBreak = new HtmlGenericControl("hr");
            lBreak.Attributes.Add("style", "display: block;height: 1px;border: 0;border-top: 1px solid #000000;margin: 1em 0;padding:0;");
            CallActions.Controls.Add(lBreak);

            var divWrapper = new HtmlGenericControl("div");
            divWrapper.Attributes.Add("style", "padding-left:60px;");

            var hHeader = new HtmlGenericControl("h2")
            {
                InnerText = "Main Menu Options"
            };
            divWrapper.Controls.Add(hHeader);

            if (!string.IsNullOrWhiteSpace(RecordingUploadValue))
            {
                var lblRecording = new Label
                {
                    Text = "<div>Recording File Name: " + RecordingUploadValue + "</div>",
                    ID = "UploadRecordingLabel"
                };
                divWrapper.Controls.Add(lblRecording);

                var lblRecordingDelete = new Label
                {
                    Text = "<div><a href=\"flow_modify.aspx?DoAction=DeleteRecording&FlowID=" + SepCommon.SepCore.Request.Item("FlowID") + "\">" + SepFunctions.LangText("Delete File") + "</a></div>",
                    ID = "DeleteRecordingLabel"
                };
                divWrapper.Controls.Add(lblRecordingDelete);
            }
            else
            {
                var lblRecording = new Label
                {
                    Text = SepFunctions.LangText("Upload Recording (MP3 File)"),
                    ID = "UploadRecordingLabel",
                    AssociatedControlID = "UploadRecording"
                };
                divWrapper.Controls.Add(lblRecording);

                var txtUploadRecording = new FileUpload
                {
                    ID = "UploadRecording",
                    ClientIDMode = ClientIDMode.Static
                };
                divWrapper.Controls.Add(txtUploadRecording);
            }

            var lblRecordingOr = new Label
            {
                Text = SepFunctions.LangText("-- Or --"),
                ID = "UploadRecordingOrLabel"
            };
            divWrapper.Controls.Add(lblRecordingOr);

            var lblRecordingText = new Label
            {
                Text = SepFunctions.LangText("Play Text"),
                ID = "RecordingTextLabel",
                AssociatedControlID = "RecordingText"
            };
            divWrapper.Controls.Add(lblRecordingText);

            var txtRecordingText = new TextBox
            {
                ID = "RecordingText",
                CssClass = "form-control"
            };
            if (!string.IsNullOrWhiteSpace(RecordingTextValue))
            {
                txtRecordingText.Text = RecordingTextValue;
            }

            divWrapper.Controls.Add(txtRecordingText);

            var pSpliter = new HtmlGenericControl("hr");
            pSpliter.Attributes.Add("style", "display: block;height: 1px;border: 0;border-top: 1px solid #000000;margin: 1em 0;padding:0;");
            divWrapper.Controls.Add(pSpliter);

            for (var i = 1; i < 10; i++)
            {
                var lblLabel = new Label
                {
                    Text = SepFunctions.LangText("Number to Press (Leave blank to disable)"),
                    ID = "PressNumberLabel" + i,
                    AssociatedControlID = "PressNumber" + i
                };
                divWrapper.Controls.Add(lblLabel);

                var txtBox = new TextBox
                {
                    ID = "PressNumber" + i,
                    CssClass = "form-control",
                    ClientIDMode = ClientIDMode.Static,
                    MaxLength = 1
                };
                if (hasConfig && root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Number") != null)
                {
                    txtBox.Text = root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Number").InnerText;
                }
                else
                {
                    if (i == 1)
                    {
                        txtBox.Text = SepCommon.SepCore.Strings.ToString(i);
                    }
                }

                divWrapper.Controls.Add(txtBox);

                lblLabel = new Label
                {
                    Text = SepFunctions.LangText("Action to Perform"),
                    ID = "PerformActionLabel" + i,
                    AssociatedControlID = "PerformAction" + i
                };
                divWrapper.Controls.Add(lblLabel);

                DropDownList dlAction = new DropDownList
                {
                    ID = "PerformAction" + i
                };
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM TwilioDevices ORDER BY DeviceName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                dlAction.Items.Add(new ListItem("Call Device (" + SepFunctions.openNull(RS["DeviceName"]) + ")", "Device:" + SepFunctions.openNull(RS["DeviceID"])));
                            }

                        }
                    }

                    using (var cmd = new SqlCommand("SELECT * FROM TwilioGroups ORDER BY GroupName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                dlAction.Items.Add(new ListItem("Call Group (" + SepFunctions.openNull(RS["GroupName"]) + ")", "Group:" + SepFunctions.openNull(RS["GroupID"])));
                            }

                        }
                    }

                    using (var cmd = new SqlCommand("SELECT TU.UserID, Members.UserName FROM TwilioUsers AS TU, Members WHERE TU.UserID=Members.UserID ORDER BY Members.UserName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                dlAction.Items.Add(new ListItem("Call User (" + SepFunctions.openNull(RS["UserName"]) + ")", "User:" + SepFunctions.openNull(RS["UserID"])));
                            }

                        }
                    }
                }

                divWrapper.Controls.Add(dlAction);
                if (hasConfig && root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action") != null)
                {
                    dlAction.SelectedValue = root.SelectSingleNode("/Root/MenuOptions/Option" + i + "/Action").InnerText;
                }
            }

            CallActions.Controls.Add(divWrapper);
        }

        /// <summary>
        /// Draws the option.
        /// </summary>
        /// <param name="optionName">Name of the option.</param>
        /// <param name="sConfig">The s configuration.</param>
        private void Draw_Option(string optionName, string sConfig = "")
        {
            switch (optionName)
            {
                case "Group":
                    Draw_Call_Group(sConfig);
                    break;

                case "User":
                    Draw_Call_User(sConfig);
                    break;

                case "Device":
                    Draw_Call_Device(sConfig);
                    break;

                default:
                    Draw_Main_Menu(sConfig);
                    break;
            }
        }

        /// <summary>
        /// Draws the phone numbers.
        /// </summary>
        /// <param name="FlowID">The flow identifier.</param>
        private void Draw_Phone_Numbers(string FlowID = "")
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioNumbers WHERE FlowID='0' OR FlowID=@FlowID ORDER BY PhoneNumber", conn))
                {
                    cmd.Parameters.AddWithValue("@FlowID", FlowID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You do not have any available numbers.") + "</div>";
                            ModFormDiv.Visible = false;
                        }
                        else
                        {
                            while (RS.Read())
                            {
                                var sChecked = string.Empty;
                                if (FlowID == SepFunctions.openNull(RS["FlowID"]))
                                {
                                    sChecked = " checked=\"checked\"";
                                }
                                else
                                {
                                    if (SepCommon.SepCore.Strings.InStr(SepCommon.SepCore.Request.Item("NumberIDs"), SepFunctions.openNull(RS["NumberID"])) > 0)
                                    {
                                        sChecked = " checked=\"checked\"";
                                    }
                                }

                                NumberIDsDiv.InnerHtml += "<div><input id=\"NumberIDs\" type=\"checkbox\" name=\"NumberIDs\" class=\"checkbox-inline\" style=\"height: 16px; width: 16px; margin: 0px;\" value=\"" + SepFunctions.openNull(RS["NumberID"]) + "\"" + sChecked + "> ";
                                NumberIDsDiv.InnerHtml += "<label for=\"GroupIDs\" style=\"display: inline-block; margin-left: 8px;\">" + SepFunctions.openNull(RS["PhoneNumber"]) + "</label></div>";
                            }
                        }

                    }
                }
            }
        }
    }
}