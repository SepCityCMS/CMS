// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_my_resume.aspx.cs" company="SepCity, Inc.">
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
    /// Class careers_my_resume.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_my_resume : Page
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
        /// Gets the resume.
        /// </summary>
        public void Get_Resume()
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var sessionId = cPCR.GetSessionId();

            try
            {
                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "candidates/" + SepFunctions.GetUserInformation("PCRCandidateId") + "/resumes");
                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                WRequest.Method = "GET";
                WRequest.ContentType = "application/json";
                WRequest.Accept = "application/json";

                WResponse = (HttpWebResponse)WRequest.GetResponse();
                WReader = new StreamReader(WResponse.GetResponseStream());
                var jsonString = WReader.ReadToEnd();

                var pcrResults = JsonConvert.DeserializeObject<PCRecruiterCandidateResume>(jsonString);

                if (SepCommon.SepCore.Request.Item("DoAction") == "ViewResume" && !string.IsNullOrWhiteSpace(pcrResults.Resume) && !string.IsNullOrWhiteSpace(pcrResults.FileName))
                {
                    Response.Clear();
                    Response.ContentType = SepFunctions.GetContentType(Strings.LCase(Path.GetExtension(pcrResults.FileName)));
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + pcrResults.FileName);
                    Response.BinaryWrite(SepFunctions.StringToBytes(SepFunctions.Base64Decode(pcrResults.Resume)));
                    Response.End();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(pcrResults.FileName) && !string.IsNullOrWhiteSpace(pcrResults.Resume))
                    {
                        UploadResume.ContentID = "0";
                        UploadResume.UserID = SepFunctions.Session_User_ID();
                        ResumeFile.InnerHtml = "<a href=\"" + sInstallFolder + "careers_my_resume.aspx?DoAction=ViewResume\" target=\"_blank\">" + pcrResults.FileName + "</a>";
                        UploadResumeLabel.InnerText = SepFunctions.LangText("Replace Current Resume");
                    }
                    else
                    {
                        ResumeFileRow.Visible = false;
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostAddResumes", "GetAddResumes", SepFunctions.GetUserInformation("PCRCandidateId"), true) == false) SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                    }
                }
            }
            catch
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Error connecting to PCRecruiter API.") + "</div>";
                ModFormDiv.Visible = false;
            }
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("My Resume");
                    ResumeFileLabel.InnerText = SepFunctions.LangText("Resume File Name:");
                    UploadResumeLabel.InnerText = SepFunctions.LangText("Upload a Resume:");
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

            TranslatePage();

            GlobalVars.ModuleID = 66;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PCREnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PCRAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "careers_my_resume.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                cPCR.Members2PCR();

                Get_Resume();
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Write Resume
                var sUploadId = string.Empty;

                byte[] byteArray = null;
                var postData = string.Empty;

                var sessionId = cPCR.GetSessionId();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT TOP 1 UploadID,FileName,FileData FROM Uploads WHERE UniqueID='0' AND ModuleID=@ModuleID AND UserID=@UserID ORDER BY DatePosted DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", GlobalVars.ModuleID);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sUploadId = SepFunctions.openNull(RS["UploadID"]);
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

                                WResponse = (HttpWebResponse)WRequest.GetResponse();
                                WReader = new StreamReader(WResponse.GetResponseStream());

                                Get_Resume();
                            }

                        }
                    }

                    using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID='0' AND ModuleID=@ModuleID AND UserID=@UserID AND UploadID <> @UploadID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", GlobalVars.ModuleID);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.Parameters.AddWithValue("@UploadID", sUploadId);
                    }
                }

                // End Write Resume
                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your resume has been successfully saved.") + "</div>";
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Updated", "Resume: " + SepFunctions.Session_User_Name());
            }
            catch
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Error connecting to PCRecruiter API.") + "</div>";
                ModFormDiv.Visible = false;
            }
        }
    }
}