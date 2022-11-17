// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_view_candidate.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Net;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Xml;

    /// <summary>
    /// Class careers_view_candidate.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_view_candidate : Page
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
            SepFunctions.RequireLogin(SepFunctions.Security("PCRBrowse"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewCandidate", "GetViewCandidate", SepFunctions.GetUserInformation("PCRCandidateId"), false) == false)
            {
                SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                return;
            }

            cPCR.Members2PCR();

            var sessionId = cPCR.GetSessionId();

            if (SepCommon.SepCore.Request.Item("DoAction") == "ViewResume")
            {
                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "candidates/" + SepCommon.SepCore.Request.Item("CandidateId") + "/resumes");
                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                WRequest.Method = "GET";
                WRequest.ContentType = "application/json";
                WRequest.Accept = "application/json";

                WResponse = (HttpWebResponse)WRequest.GetResponse();
                WReader = new StreamReader(WResponse.GetResponseStream());
                var jsonString2 = WReader.ReadToEnd();

                var pcrResumeResults = JsonConvert.DeserializeObject<PCRecruiterCandidateResume>(jsonString2);

                if (!string.IsNullOrWhiteSpace(pcrResumeResults.FileName) && !string.IsNullOrWhiteSpace(pcrResumeResults.Resume))
                {
                    Response.Clear();
                    Response.ContentType = SepFunctions.GetContentType(Strings.LCase(Path.GetExtension(pcrResumeResults.FileName)));
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + pcrResumeResults.FileName);
                    Response.BinaryWrite(SepFunctions.StringToBytes(SepFunctions.Base64Decode(pcrResumeResults.Resume)));
                    Response.End();
                    return;
                }
            }

            var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Candidates, "CanDetail", "detailsDefault");
            var getFields = string.Empty;
            var arrCount = 0;

            foreach (var item in arrData)
            {
                if (arrCount > 0) getFields += ",";
                getFields += Strings.ToString(item);
                arrCount += 1;
            }

            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "candidates/" + SepCommon.SepCore.Request.Item("CandidateId") + "?Fields=CandidateId,CompanyId," + getFields);
            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
            WRequest.Method = "GET";
            WRequest.ContentType = "application/json";
            WRequest.Accept = "application/json";

            WResponse = (HttpWebResponse)WRequest.GetResponse();
            WReader = new StreamReader(WResponse.GetResponseStream());
            var jsonString = WReader.ReadToEnd();

            var pcrResults = JsonConvert.DeserializeObject<PCRecruiterCandidateFields>(jsonString);
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        var root = doc.DocumentElement;
                        Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", pcrResults.FirstName + " " + pcrResults.LastName);

                        foreach (var item in arrData)
                        {

                            string drawScreenHTML;
                            try
                            {
                                var ret = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                drawScreenHTML = "<p class=\"CandidateRow\">";
                                drawScreenHTML += "  <label class=\"CandidateInfoLabel\">" + root.SelectSingleNode("/pcrfields/CandidateFields/field[@name='" + item + "']").InnerText + "</label>";
                                if (!string.IsNullOrWhiteSpace(ret))
                                    switch (Strings.ToString(item))
                                    {
                                        case "EmailAddress":
                                            drawScreenHTML += "<a href=\"mailto:" + ret + "\">" + ret + "</a>";
                                            break;

                                        case "BillRate":
                                            drawScreenHTML += SepFunctions.Format_Currency(pcrResults.BillRate.Value);
                                            break;

                                        case "PayRate":
                                            drawScreenHTML += SepFunctions.Format_Currency(pcrResults.PayRate.Value);
                                            break;

                                        case "DesiredSalary":
                                            drawScreenHTML += SepFunctions.Format_Currency(pcrResults.DesiredSalary.Value);
                                            break;

                                        case "CurrentSalary":
                                            drawScreenHTML += SepFunctions.Format_Currency(pcrResults.CurrentSalary.Value);
                                            break;

                                        case "FirstName":
                                            FullName.InnerText += pcrResults.FirstName + " ";
                                            drawScreenHTML = string.Empty;
                                            break;

                                        case "LastName":
                                            FullName.InnerText += pcrResults.LastName + " ";
                                            drawScreenHTML = string.Empty;
                                            break;

                                        case "CompanyName":
                                            drawScreenHTML += "<a href=\"" + sInstallFolder + "careers_company.aspx?CompanyId=" + pcrResults.CompanyId + "\">" + pcrResults.CompanyName + "</a>";
                                            break;

                                        case "HasResume":
                                            if (pcrResults.HasResume)
                                            {
                                                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "candidates/" + SepCommon.SepCore.Request.Item("CandidateId") + "/resumes");
                                                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                                                WRequest.Method = "GET";
                                                WRequest.ContentType = "application/json";
                                                WRequest.Accept = "application/json";

                                                WResponse = (HttpWebResponse)WRequest.GetResponse();
                                                WReader = new StreamReader(WResponse.GetResponseStream());
                                                jsonString = WReader.ReadToEnd();

                                                var pcrResumeResults = JsonConvert.DeserializeObject<PCRecruiterCandidateResume>(jsonString);

                                                if (!string.IsNullOrWhiteSpace(pcrResumeResults.FileName) && !string.IsNullOrWhiteSpace(pcrResumeResults.Resume)) drawScreenHTML += "<a href=\"" + sInstallFolder + "careers_view_candidate.aspx?DoAction=ViewResume&CandidateId=" + SepCommon.SepCore.Request.Item("CandidateId") + "\" target=\"_blank\">" + pcrResumeResults.FileName + "</a>";
                                            }
                                            else
                                            {
                                                drawScreenHTML += SepFunctions.LangText("No Resume Uploaded");
                                            }

                                            break;

                                        default:
                                            if (root.SelectSingleNode("/pcrfields/CandidateFields/field[@name='" + item + "']").Attributes["type"].InnerText == "date")
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