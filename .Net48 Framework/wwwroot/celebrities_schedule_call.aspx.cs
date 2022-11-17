// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="celebrities_schedule_call.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class conference_schedule_call.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class conference_schedule_call : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Schedule Call");
                    EventDateLabel.InnerText = SepFunctions.LangText("Request Call Date:");
                    StartTimeLabel.InnerText = SepFunctions.LangText("Start Time:");
                    SubjectLabel.InnerText = SepFunctions.LangText("Subject:");
                    SubjectRequired.ErrorMessage = SepFunctions.LangText("~~Subject~~ is required.");
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

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                SepFunctions.RequireLogin("|2|");

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            if (SepFunctions.CompareKeys("|2|", true) == false)
            {
                if (SepFunctions.toLong(SepFunctions.GetUserInformation("UserPoints")) > 0)
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT Count(EventID) AS Counter FROM EventCalendar WHERE Status <> -1 AND PortalID=@PortalID AND EventDate >= @EventDate", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            cmd.Parameters.AddWithValue("@EventDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -2, DateTime.Now));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (SepFunctions.toLong(SepFunctions.GetUserInformation("UserPoints")) <= SepFunctions.toLong(SepFunctions.openNull(RS["Counter"])))
                                    {
                                        ModFormDiv.Visible = false;
                                        LoadPayPal();
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    LoadPayPal();
                }
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID())) ProfileID.Value = SepCommon.SepCore.Request.Item("ProfileID");
            else PageContent.InnerHtml = "<h1>" + SepFunctions.LangText("Access Denied") + "</h1>";
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
            var sUserID = string.Empty;
            var sProcessId = Strings.ToString(SepFunctions.GetIdentity());
            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT UserID FROM Profiles WHERE ProfileID=@ProfileID", conn))
                {
                    cmd.Parameters.AddWithValue("@ProfileID", ProfileID.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sUserID = SepFunctions.openNull(RS["UserID"]);
                        }

                    }
                }
            }

            SepCommon.DAL.Events.Event_Save(SepFunctions.toLong(EventID.Value), ProfileID.Value, Subject.Value, SepFunctions.Session_User_ID(), SepFunctions.toDate(EventDate.Value), BegTime.Value, BegTime.Value, 0, 0, string.Empty, EventContent.Text, true, 0, 0, SepFunctions.Get_Portal_ID());

            SepCommon.DAL.Messenger.Message_Send(SepFunctions.Session_User_ID(), SepFunctions.GetUserInformation("UserName", sUserID), Subject.Value, SepFunctions.GetUserInformation("UserName", sUserID) + " has scheduled a call on your schedule<br/><br/>Date: " + EventDate.Value + "<br/>Time: " + BegTime.Value, false);

            if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("PhoneNUmber", sUserID)) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioPhoneNumber")))
            {
                var cTwilio = new SepCommon.TwilioGlobal();
                cTwilio.Send_SMS(SepFunctions.FormatPhone(SepFunctions.GetUserInformation("PhoneNUmber", sUserID)), SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName") + " has scheduled a call with you on " + SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " at " + BegTime.Value));

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("INSERT INTO BG_Processes (ProcessID, ProcessName, IntervalSeconds, Status, RecurringDays) VALUES(@ProcessID, @ProcessName, @IntervalSeconds, @Status, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessID", sProcessId);
                        cmd.Parameters.AddWithValue("@ProcessName", "SMS");
                        cmd.Parameters.AddWithValue("@IntervalSeconds", 1);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO BG_SMS (ProcessID, To_Phone, From_Phone, Message_Body, Send_Date) VALUES(@ProcessID, @To_Phone, @From_Phone, @Message_Body, @Send_Date)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessId", sProcessId);
                        cmd.Parameters.AddWithValue("@To_Phone", SepFunctions.FormatPhone(SepFunctions.GetUserInformation("PhoneNUmber", sUserID)));
                        cmd.Parameters.AddWithValue("@From_Phone", SepFunctions.Setup(989, "TwilioPhoneNumber"));
                        cmd.Parameters.AddWithValue("@Message_Body", SepFunctions.Setup(GlobalVars.ModuleID, "TwilioSMSReminderMsg") + Environment.NewLine + "Call is scheduled for: " + SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " " + BegTime.Value));
                        cmd.Parameters.AddWithValue("@Send_Date", DateAndTime.DateAdd(DateAndTime.DateInterval.Hour, SepFunctions.toInt(SepFunctions.Setup(GlobalVars.ModuleID, "TwilioSMSReminderOffset")), SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " " + BegTime.Value)));
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO BG_SMS (ProcessID, To_Phone, From_Phone, Message_Body, Send_Date) VALUES(@ProcessID, @To_Phone, @From_Phone, @Message_Body, @Send_Date)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessId", sProcessId);
                        cmd.Parameters.AddWithValue("@To_Phone", SepFunctions.FormatPhone(SepFunctions.GetUserInformation("PhoneNUmber")));
                        cmd.Parameters.AddWithValue("@From_Phone", SepFunctions.Setup(989, "TwilioPhoneNumber"));
                        cmd.Parameters.AddWithValue("@Message_Body", SepFunctions.Setup(GlobalVars.ModuleID, "TwilioSMSReminderMsg") + Environment.NewLine + "To view video use the following URL: " + SepFunctions.GetMasterDomain(false) + "uservideo/" + sUserID + "/" + Environment.NewLine + "Call is scheduled for: " + SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " " + BegTime.Value));
                        cmd.Parameters.AddWithValue("@Send_Date", DateAndTime.DateAdd(DateAndTime.DateInterval.Hour, SepFunctions.toInt(SepFunctions.Setup(GlobalVars.ModuleID, "TwilioSMSReminderOffset")), SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " " + BegTime.Value)));
                        cmd.ExecuteNonQuery();
                    }

                    // Send Emails
                    sProcessId = Strings.ToString(SepFunctions.GetIdentity());

                    using (var cmd = new SqlCommand("INSERT INTO BG_Processes (ProcessID, ProcessName, IntervalSeconds, Status, RecurringDays) VALUES(@ProcessID, @ProcessName, @IntervalSeconds, @Status, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessID", sProcessId);
                        cmd.Parameters.AddWithValue("@ProcessName", "Email");
                        cmd.Parameters.AddWithValue("@IntervalSeconds", 1);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO BG_Emails (ProcessID, To_Email_Address, From_Email_Address, Email_Subject, Email_Body) VALUES(@ProcessID, @To_Email_Address, @From_Email_Address, @Email_Subject, @Email_Body)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessId", sProcessId);
                        cmd.Parameters.AddWithValue("@To_Email_Address", SepFunctions.GetUserInformation("EmailAddress", sUserID));
                        cmd.Parameters.AddWithValue("@From_Email_Address", SepFunctions.Setup(991, "AdminEmailAddress"));
                        cmd.Parameters.AddWithValue("@Email_Subject", "Call has been successfully scheduled.");
                        cmd.Parameters.AddWithValue("@Email_Body", SepFunctions.Setup(GlobalVars.ModuleID, "TwilioSMSReminderMsg") + Environment.NewLine + "Call is scheduled for: " + SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " " + BegTime.Value));
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO BG_Emails (ProcessID, To_Email_Address, From_Email_Address, Email_Subject, Email_Body) VALUES(@ProcessID, @To_Email_Address, @From_Email_Address, @Email_Subject, @Email_Body)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessId", sProcessId);
                        cmd.Parameters.AddWithValue("@To_Email_Address", SepFunctions.GetUserInformation("EmailAddress"));
                        cmd.Parameters.AddWithValue("@From_Email_Address", SepFunctions.Setup(991, "AdminEmailAddress"));
                        cmd.Parameters.AddWithValue("@Email_Subject", "Call has been successfully scheduled.");
                        cmd.Parameters.AddWithValue("@Email_Body", SepFunctions.Setup(GlobalVars.ModuleID, "TwilioSMSReminderMsg") + Environment.NewLine + "Call is scheduled for: " + SepFunctions.toDate(SepFunctions.toDate(EventDate.Value) + " " + BegTime.Value));
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Call has been successfully scheduled.") + "</div>";
            ModFormDiv.Visible = false;

            SepFunctions.Redirect(sInstallFolder + "celebrities.aspx?DoAction=CallSucccessful");
        }

        /// <summary>
        /// Loads the pay pal.
        /// </summary>
        private void LoadPayPal()
        {
            var GetInvoiceID = SepFunctions.toLong(SepFunctions.Session_Invoice_ID());

            var jCustom = SepCommon.DAL.CustomFields.Answer_Get(847562837400918, SepCommon.SepCore.Request.Item("UserID"));

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UnitPrice FROM ShopProducts WHERE ProductID=@ProductID", conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", jCustom.FieldValue);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();

                            SepCommon.DAL.Invoices.Invoice_Save(GetInvoiceID, SepFunctions.Session_User_ID(), 0, DateTime.Now, 0, string.Empty, "1", string.Empty, string.Empty, false, "Donation", SepFunctions.openNull(RS["UnitPrice"]), string.Empty, string.Empty, string.Empty, 0, 0, SepFunctions.Get_Portal_ID());
                            var cPayPal = new Submit_PayPal();
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", cPayPal.SendPayPal(GetInvoiceID, SepFunctions.openNull(RS["UnitPrice"]), SepFunctions.GetMasterDomain(true) + "celebrities_schedule_call.aspx?ProfileID=" + SepCommon.SepCore.Request.Item("ProfileID") + "&UserID=" + SepCommon.SepCore.Request.Item("UserID")));
                        }

                    }
                }
            }
        }
    }
}