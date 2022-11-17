// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_job_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using ASPSnippets.LinkedInAPI;
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Xml;

    /// <summary>
    /// Class careers_job_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_job_view : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The c PCR
        /// </summary>
        private readonly PCRecruiter cPCR = new PCRecruiter();

        /// <summary>
        /// The w reader
        /// </summary>
        private StreamReader WReader;

        /// <summary>
        /// The w request
        /// </summary>
        private HttpWebRequest WRequest;

        /// <summary>
        /// The w response
        /// </summary>
        private HttpWebResponse WResponse;

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
        /// Authorizes the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Authorize(object sender, EventArgs e)
        {
            LinkedInConnect.Authorize();
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

                if (WReader != null)
                {
                    WReader.Dispose();
                }

                if (WResponse != null)
                {
                    WResponse.Dispose();
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

            GlobalVars.ModuleID = 66;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PCREnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PCRAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            cPCR.Members2PCR();

            var sessionId = cPCR.GetSessionId();
            var LinkedInAPI = SepFunctions.Setup(989, "LinkedInAPI");
            var LinkedInSecret = SepFunctions.Setup(989, "LinkedInSecret");

            SepFunctions.Session_Password();
            var JobId = SepCommon.SepCore.Request.Item("JobId");
            if (string.IsNullOrWhiteSpace(JobId))
                JobId = SepCommon.SepCore.Session.getSession("PCRJobId");

            if (!string.IsNullOrWhiteSpace(LinkedInAPI) && !string.IsNullOrWhiteSpace(LinkedInSecret))
            {
                LinkedInConnect.APIKey = LinkedInAPI;
                LinkedInConnect.APISecret = LinkedInSecret;
                LinkedInConnect.RedirectUrl = Request.Url.AbsoluteUri.Split('?')[0];
            }
            else
            {
                LinkedInButton.Visible = false;
            }

            if (!string.IsNullOrWhiteSpace(JobId))
            {
                SepCommon.SepCore.Session.setSession("PCRJobId", JobId);

                var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, "JobDetail", "detailsDefault");
                var getFields = string.Empty;
                var arrCount = 0;

                foreach (var item in arrData)
                {
                    if (arrCount > 0) getFields += ",";
                    getFields += Strings.ToString(item);
                    arrCount += 1;
                }

                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions/" + JobId + "?Fields=JobId,CompanyId," + getFields);
                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                WRequest.Method = "GET";
                WRequest.ContentType = "application/json";
                WRequest.Accept = "application/json";

                WResponse = (HttpWebResponse)WRequest.GetResponse();
                WReader = new StreamReader(WResponse.GetResponseStream());
                var jsonString = WReader.ReadToEnd();

                var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobs>(jsonString);
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", pcrResults.JobTitle);

                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            var root = doc.DocumentElement;

                            foreach (var item in arrData)
                            {

                                string drawScreenHTML;
                                try
                                {
                                    var ret = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                    drawScreenHTML = "<p class=\"CandidateRow\">";
                                    drawScreenHTML += "  <label class=\"CandidateInfoLabel\">" + root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").InnerText + "</label>";
                                    if (!string.IsNullOrWhiteSpace(ret))
                                        switch (Strings.ToString(item))
                                        {
                                            case "MinSalary":
                                                drawScreenHTML += SepFunctions.Format_Currency(pcrResults.MinSalary.Value);
                                                break;

                                            case "MaxSalary":
                                                drawScreenHTML += SepFunctions.Format_Currency(pcrResults.MaxSalary.Value);
                                                break;

                                            case "BillRate":
                                                drawScreenHTML += SepFunctions.Format_Currency(pcrResults.BillRate.Value);
                                                break;

                                            case "PayRate":
                                                drawScreenHTML += SepFunctions.Format_Currency(pcrResults.PayRate.Value);
                                                break;

                                            case "JobTitle":
                                                JobTitle.InnerText = pcrResults.JobTitle;
                                                drawScreenHTML = string.Empty;
                                                break;

                                            case "DirectApplyLink":
                                                drawScreenHTML += "<a href=\"" + pcrResults.DirectApplyLink + "\" target=\"_blank\">" + SepFunctions.LangText("Click Here to Apply") + "</a>";
                                                break;

                                            case "DirectJobLink":
                                                drawScreenHTML += "<a href=\"" + pcrResults.DirectJobLink + "\" target=\"_blank\">" + SepFunctions.LangText("Click Here for Details") + "</a>";
                                                break;

                                            case "CompanyName":
                                                drawScreenHTML += "<a href=\"" + sInstallFolder + "careers_company.aspx?CompanyId=" + pcrResults.CompanyId + "\">" + pcrResults.CompanyName + "</a>";
                                                break;

                                            case "ContactEmail":
                                                drawScreenHTML += "<a href=\"mailto:" + pcrResults.ContactEmail + "\">" + pcrResults.ContactEmail + "</a>";
                                                break;

                                            case "InternalJobDescription":
                                                drawScreenHTML = "<h3>" + SepFunctions.LangText("Internal Job Description") + "</h3><p>";
                                                if (Strings.InStr(pcrResults.InternalJobDescription, "<body") > 0)
                                                    drawScreenHTML += SepFunctions.ParseXML("body", pcrResults.InternalJobDescription);
                                                else
                                                    drawScreenHTML += pcrResults.InternalJobDescription;
                                                break;

                                            case "JobDescription":
                                                drawScreenHTML = "<h3>" + SepFunctions.LangText("Job Description") + "</h3><p>";
                                                if (Strings.InStr(pcrResults.JobDescription, "<body") > 0)
                                                    drawScreenHTML += SepFunctions.ParseXML("body", pcrResults.JobDescription);
                                                else
                                                    drawScreenHTML += pcrResults.JobDescription;
                                                break;

                                            case "JobType":
                                                drawScreenHTML += SepFunctions.formatJobType(pcrResults.JobType);
                                                break;

                                            default:
                                                if (root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").Attributes["type"].InnerText == "date")
                                                    drawScreenHTML += SepFunctions.toDate(ret);
                                                else
                                                    drawScreenHTML += ret;
                                                break;
                                        }
                                    else
                                        drawScreenHTML += SepFunctions.LangText("Not Specified");

                                    if (!string.IsNullOrWhiteSpace(drawScreenHTML)) drawScreenHTML += "</p>";
                                }
                                catch
                                {
                                    drawScreenHTML = string.Empty;
                                }

                                ScreenHTML.InnerHtml += drawScreenHTML;
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(LinkedInAPI) && !string.IsNullOrWhiteSpace(LinkedInSecret))
                    if (LinkedInConnect.IsAuthorized)
                    {
                        SepFunctions.RequireLogin(SepFunctions.Security("PCRApply"));
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostApplyJobs", "GetApplyJobs", SepFunctions.GetUserInformation("PCRCandidateId"), false) == false)
                        {
                            SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                            return;
                        }

                        var sCompanyId = string.Empty;
                        var sJobTitle = string.Empty;

                        try
                        {
                            var ds = LinkedInConnect.Fetch();
                            var sResume = string.Empty;
                            var sHTML = string.Empty;

                            try
                            {
                                sHTML += "<p><img src=\"" + ds.Tables["person"].Rows[0]["picture-url"] + "\" border=\"0\" /></p>" + Environment.NewLine;
                                sHTML += "<h1>" + ds.Tables["person"].Rows[0]["first-name"] + " " + ds.Tables["person"].Rows[0]["last-name"] + "</h1>" + Environment.NewLine;
                                sHTML += "<h3>" + ds.Tables["person"].Rows[0]["headline"] + "</h3>" + Environment.NewLine;
                                sHTML += "<h5>" + ds.Tables["person"].Rows[0]["industry"] + "</h5>" + Environment.NewLine;
                                sHTML += "<div></div>" + Environment.NewLine;
                                sHTML += "<div><strong>LinkedIn Profile URL:</strong> <a href=\"" + ds.Tables["person"].Rows[0]["public-profile-url"] + "\">" + ds.Tables["person"].Rows[0]["public-profile-url"] + "</a></div>" + Environment.NewLine;
                                sHTML += "<div><strong>Contact Email Address:</strong> <a href=\"" + ds.Tables["person"].Rows[0]["email-address"] + "\">" + ds.Tables["person"].Rows[0]["email-address"] + "</a></div>" + Environment.NewLine;
                                sHTML += "<div><strong>Location:</strong> " + ds.Tables["location"].Rows[0]["name"] + "</div>" + Environment.NewLine;
                                sResume = SepFunctions.Base64Encode(SepFunctions.HTMLToPDF(sHTML));
                            }
                            catch
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("LinkedIn Error") + "</div>";
                            }

                            // Lookup Company Id from Position
                            try
                            {
                                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions/" + JobId + "?Fields=JobId,CompanyId,JobTitle");
                                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                                WRequest.Method = "GET";
                                WRequest.ContentType = "application/json";
                                WRequest.Accept = "application/json";

                                WResponse = (HttpWebResponse)WRequest.GetResponse();
                                WReader = new StreamReader(WResponse.GetResponseStream());
                                jsonString = WReader.ReadToEnd();

                                pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobs>(jsonString);
                                sCompanyId = pcrResults.CompanyId;
                                sJobTitle = pcrResults.JobTitle;
                            }
                            catch
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Company Error") + "</div>";
                            }

                            // End Lookup

                            // Write Interview Record
                            try
                            {
                                var cInterviews = new SepCommon.Models.PCRecruiterInterviewFields
                                {
                                    CandidateId = SepFunctions.GetUserInformation("PCRCandidateId"),
                                    CompanyId = sCompanyId,
                                    JobId = JobId,
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
                                var dataStream = WRequest.GetRequestStream();
                                dataStream.Write(byteArray, 0, byteArray.Length);
                                dataStream.Dispose();

                                WResponse = (HttpWebResponse)WRequest.GetResponse();
                                WReader = new StreamReader(WResponse.GetResponseStream());
                            }
                            catch
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Interview Error") + "</div>";
                            }

                            // End Write Interview

                            // Write Resume
                            try
                            {
                                var cResume = new SepCommon.Models.PCRecruiterCandidateResume
                                {
                                    Resume = sResume
                                };

                                var postData = JsonConvert.SerializeObject(cResume);

                                var byteArray = Encoding.UTF8.GetBytes(postData);

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

                                WResponse = (HttpWebResponse)WRequest.GetResponse();
                                WReader = new StreamReader(WResponse.GetResponseStream());
                            }
                            catch
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Resume Error") + "</div>";
                            }

                            // End Write Resume
                        }
                        catch
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Apply Error") + "</div>";
                        }

                        if (!string.IsNullOrWhiteSpace(sJobTitle))
                        {
                            JobDetails.Visible = false;
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully applied for ~~" + sJobTitle + "~~.") + "</div>";
                        }
                        else
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error while applying through LinkedIn.") + "</div>";
                        }
                    }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This position no longer exists in our database.") + "</div>";
                JobDetails.Visible = false;
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
        /// Sends the resume.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SendResume(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            SepFunctions.Redirect(sInstallFolder + "careers_job_apply.aspx?JobId=" + SepCommon.SepCore.Request.Item("JobId"));
        }
    }
}