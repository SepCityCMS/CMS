// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_job_apply.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class careers_job_apply.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_job_apply : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The c PCR
        /// </summary>
        private readonly PCRecruiter cPCR = new PCRecruiter();

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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Send Resume");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    FirstNameLabel.InnerText = SepFunctions.LangText("First Name:");
                    LastNameLabel.InnerText = SepFunctions.LangText("Last Name:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    UploadResumeLabel.InnerText = SepFunctions.LangText("Upload a Resume:");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
                    FirstNameRequired.ErrorMessage = SepFunctions.LangText("~~First Name~~ is required.");
                    LastNameRequired.ErrorMessage = SepFunctions.LangText("~~Last Name~~ is required.");
                    StreetAddressRequired.ErrorMessage = SepFunctions.LangText("~~Street Address~~ is required.");
                    CityRequired.ErrorMessage = SepFunctions.LangText("~~City~~ is required.");
                    PostalCodeRequired.ErrorMessage = SepFunctions.LangText("~~Zip/Postal Code~~ is required.");
                    PhoneNumberRequired.ErrorMessage = SepFunctions.LangText("~~Phone Number~~ is required.");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ApplyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ApplyButton_Click(object sender, EventArgs e)
        {
            SepCommon.DAL.Members.Member_Save(SepFunctions.Session_User_ID(), SepFunctions.GetUserInformation("UserName"), string.Empty, string.Empty, string.Empty, FirstName.Value, LastName.Value, StreetAddress.Value, City.Value, State.Text, PostalCode.Value, Country.Text, EmailAddress.Value, PhoneNumber.Value, SepFunctions.GetUserInformation("PayPal"), SepFunctions.toLong(SepFunctions.GetUserInformation("UserPoints")), SepFunctions.toLong(SepFunctions.GetUserInformation("AccessClass")), SepFunctions.GetUserInformation("AccessKeys"), SepFunctions.toDate(SepFunctions.GetUserInformation("BirthDate")), SepFunctions.toInt(SepFunctions.GetUserInformation("Male")), SepFunctions.GetUserInformation("ReferralID"), SepFunctions.GetUserInformation("WebSiteURL"), SepFunctions.toLong(SepFunctions.GetUserInformation("PortalID")), SepFunctions.GetUserInformation("ApproveFriends"), null, 1, SepFunctions.GetUserInformation("Facebook_Token"), SepFunctions.GetUserInformation("Facebook_Id"), SepFunctions.GetUserInformation("Facebook_User"), false, SepFunctions.toLong(SepFunctions.GetUserInformation("SiteID")), string.Empty);
            var sessionId = cPCR.GetSessionId();

            // Lookup Company Id from Position
            HttpWebRequest WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions/" + SepCommon.SepCore.Request.Item("JobId") + "?Fields=JobId,CompanyId,JobTitle");
            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
            WRequest.Method = "GET";
            WRequest.ContentType = "application/json";
            WRequest.Accept = "application/json";

            HttpWebResponse WResponse = (HttpWebResponse)WRequest.GetResponse();

            StreamReader WReader = new StreamReader(WResponse.GetResponseStream());
            var jsonString = WReader.ReadToEnd();
            WReader.Dispose();

            var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobs>(jsonString);
            string sCompanyId = pcrResults.CompanyId;
            string sJobTitle = pcrResults.JobTitle;

            // End Lookup

            // Write Interview Record
            var cInterviews = new SepCommon.Models.PCRecruiterInterviewFields
            {
                CandidateId = SepFunctions.GetUserInformation("PCRCandidateId"),
                CompanyId = sCompanyId,
                JobId = SepCommon.SepCore.Request.Item("JobId"),
                InterviewType = "5",
                CurrentInterviewType = "5",
                InterviewStatus = "On-Line Job Inquiry",
                CurrentStatus = "On-Line Job Inquiry",
                WrittenBy = SepFunctions.Setup(GlobalVars.ModuleID, "PCRUserName")
            };

            var postData = JsonConvert.SerializeObject(cInterviews);

            var byteArray = Encoding.UTF8.GetBytes(postData);

            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "interviews");
            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
            WRequest.Method = "POST";
            WRequest.ContentLength = byteArray.Length;
            WRequest.ContentType = "application/json";
            WRequest.Accept = "application/json";
            using (var dataStream = WRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            // End Write Interview

            // Write Resume
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TOP 1 FileName,FileData FROM Uploads WHERE UniqueID='1' AND ModuleID=@ModuleID AND UserID=@UserID ORDER BY DatePosted DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", GlobalVars.ModuleID);
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var cResume = new SepCommon.Models.PCRecruiterCandidateResume();
                            if (!SepCommon.SepCore.Information.IsDBNull(RS["FileData"]))
                            {
                                cResume.Resume = SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"]));
                            }

                            postData = JsonConvert.SerializeObject(cResume);

                            byteArray = Encoding.UTF8.GetBytes(postData);

                            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "candidates/" + SepFunctions.GetUserInformation("PCRCandidateId") + "/resumes");
                            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                            WRequest.Method = "PUT";
                            WRequest.ContentLength = byteArray.Length;
                            WRequest.ContentType = "application/json";
                            WRequest.Accept = "application/json";
                            using (var dataStream = WRequest.GetRequestStream())
                            {
                                dataStream.Write(byteArray, 0, byteArray.Length);
                            }
                        }

                    }
                }

                using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID='1' AND ModuleID=@ModuleID AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", GlobalVars.ModuleID);
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                }
            }

            // End Write Resume
            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully applied for ~~" + sJobTitle + "~~") + "</div>";
            ModFormDiv.Visible = false;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (cPCR != null)
                {
                    cPCR.Dispose();
                }
            }
        }

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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 66;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PCREnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PCRApply"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            cPCR.Members2PCR();

            if (!IsPostBack)
            {
                if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostApplyJobs", "GetApplyJobs", SepFunctions.GetUserInformation("PCRCandidateId"), true) == false)
                {
                    SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                    return;
                }

                EmailAddress.Value = SepFunctions.GetUserInformation("EmailAddress");
                FirstName.Value = SepFunctions.GetUserInformation("FirstName");
                LastName.Value = SepFunctions.GetUserInformation("LastName");
                Country.Text = SepFunctions.GetUserInformation("Country");
                StreetAddress.Value = SepFunctions.GetUserInformation("StreetAddress");
                City.Value = SepFunctions.GetUserInformation("City");
                State.Text = SepFunctions.GetUserInformation("State");
                PostalCode.Value = SepFunctions.GetUserInformation("ZipCode");
                PhoneNumber.Value = SepFunctions.GetUserInformation("PhoneNumber");

                UploadResume.ContentID = "1";
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
    }
}