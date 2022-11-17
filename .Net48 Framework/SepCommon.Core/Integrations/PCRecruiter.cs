// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PCRecruiter.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using Newtonsoft.Json;
    using SepCommon.Core.Models;
    using SepCommon.Core.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// The cls PC recruiter.
    /// </summary>
    public class PCRecruiter : IDisposable
    {
        // -V3073
        /// <summary>
        /// Identifier for the module.
        /// </summary>
        private const int ModuleID = 66;

        /// <summary>
        /// Values that represent pcr record types.
        /// </summary>
        public enum PCR_RecordType
        {
            /// <summary>
            /// An enum constant representing the candidates option.
            /// </summary>
            Candidates = 0,

            /// <summary>
            /// An enum constant representing the companies option.
            /// </summary>
            Companies = 1,

            /// <summary>
            /// An enum constant representing the positions option.
            /// </summary>
            Positions = 2
        }

        /// <summary>
        /// The common.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets field nodes.
        /// </summary>
        /// <param name="recordType">Type of the record.</param>
        /// <param name="xAttribute">The attribute.</param>
        /// <param name="xAttributeValue">The attribute value.</param>
        /// <param name="hasSetting">True if has setting, false if not.</param>
        /// <returns>The field nodes.</returns>
        public XmlNodeList GetFieldNodes(PCR_RecordType recordType, string xAttribute, string xAttributeValue, bool hasSetting)
        {
            var xmlPath = string.Empty;
            switch (recordType)
            {
                case PCR_RecordType.Candidates:
                    xmlPath = "CandidateFields";
                    break;

                case PCR_RecordType.Companies:
                    xmlPath = "CompanyFields";
                    break;

                case PCR_RecordType.Positions:
                    xmlPath = "PositionFields";
                    break;
            }

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "pcrfields.xml");

                var root = doc.DocumentElement;
                XmlNodeList nodes = null;

                if (hasSetting)
                {
                    nodes = root.SelectNodes("/pcrfields/" + xmlPath + "/field[not(@" + xAttribute + "='D')]");
                }
                else
                {
                    nodes = root.SelectNodes("/pcrfields/" + xmlPath + "/field[@" + xAttribute + "='" + xAttributeValue + "']");
                }

                doc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return nodes;
            }

            return null;
        }

        /// <summary>
        /// Gets PC requiter URL.
        /// </summary>
        /// <returns>The PC requiter URL.</returns>
        public string GetPCRequiterURL()
        {
            return SepFunctions.Setup(ModuleID, "PCRAPIURL");
        }

        /// <summary>
        /// Gets session identifier.
        /// </summary>
        /// <returns>The session identifier.</returns>
        public string GetSessionId()
        {
            HttpWebRequest WRequest = null;
            HttpWebResponse WResponse = null;
            StreamReader WReader = null;

            var sSession = string.Empty;
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            IgnoreBadCertificates();

            var pcrSession = Strings.Replace(Session.getSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "PCRecruiter"), "'", string.Empty);
            if (!string.IsNullOrWhiteSpace(pcrSession))
            {
                sSession = pcrSession;
            }
            else
            {
                WRequest = (HttpWebRequest)WebRequest.Create(GetPCRequiterURL() + "access-token?username=" + SepFunctions.Setup(ModuleID, "PCRUserName") + "&password=" + SepFunctions.Decrypt(SepFunctions.Setup(ModuleID, "PCRPassword")) + "&databaseid=" + SepFunctions.Setup(66, "PCRDatabaseId") + "&AppId=" + SepFunctions.Setup(66, "PCRAppId") + "&ApiKey=" + SepFunctions.Setup(66, "PCRAPIKey"));
                WResponse = (HttpWebResponse)WRequest.GetResponse();

                WReader = new StreamReader(WResponse.GetResponseStream());
                sSession = Strings.Replace(Strings.Replace(WReader.ReadToEnd(), "{\"SessionId\":\"", string.Empty), "\"}", string.Empty);
                WReader.Dispose();

                Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "PCRecruiter", sSession);
            }

            return sSession;
        }

        /// <summary>
        /// Ignore bad certificates.
        /// </summary>
        public void IgnoreBadCertificates()
        {
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
        }

        /// <summary>
        /// Members 2 pcr.
        /// </summary>
        /// <param name="force">(Optional) True to force.</param>
        public void Members2PCR(bool force = false)
        {
            HttpWebRequest WRequest = null;
            HttpWebResponse WResponse = null;
            StreamReader WReader = null;

            IgnoreBadCertificates();

            if ((string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("PCRCandidateId")) || SepFunctions.GetUserInformation("PCRCandidateId") == "0" || force) && !string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                var cCandidates = new PCRecruiterCandidateFields();
                cCandidates.FirstName = SepFunctions.GetUserInformation("FirstName");
                cCandidates.LastName = SepFunctions.GetUserInformation("LastName");
                cCandidates.City = SepFunctions.GetUserInformation("City");
                cCandidates.State = SepFunctions.GetUserInformation("State");
                cCandidates.PostalCode = SepFunctions.GetUserInformation("ZipCode");
                cCandidates.EmailAddress = SepFunctions.GetUserInformation("EmailAddress");
                cCandidates.Country = SepFunctions.GetUserInformation("Country");
                cCandidates.HomePhone = SepFunctions.GetUserInformation("PhoneNumber");
                cCandidates.UserName = "CANDIDATE";
                if (SepFunctions.CompareKeys(SepFunctions.Security("PCREmployer"), false))
                {
                    cCandidates.Status = "Employee";
                }
                else
                {
                    cCandidates.Status = "Candidate";
                }

                cCandidates.DateEntered = DateTime.Today;

                var postData = JsonConvert.SerializeObject(cCandidates);

                var sessionId = GetSessionId();

                var byteArray = Encoding.UTF8.GetBytes(postData);

                try
                {
                    WRequest = (HttpWebRequest)WebRequest.Create(GetPCRequiterURL() + "candidates");
                    WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                    WRequest.Method = "POST";
                    WRequest.ContentLength = byteArray.Length;
                    WRequest.ContentType = "application/json";
                    WRequest.Accept = "application/json";
                    using (var dataStream = WRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    WResponse = (HttpWebResponse)WRequest.GetResponse();
                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var pcrResults = JsonConvert.DeserializeObject<PCRecruiterCandidateFields>(jsonString);

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("UPDATE Members SET PCRCandidateId=@PCRCandidateId WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PCRCandidateId", pcrResults.CandidateId);
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the SepCommon.Core.PCRecruiter and optionally
        /// releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // DO NOT ADD THIS BACK CAUSES ERRORS
            // if (disposing) Dispose();
        }

        /// <summary>
        /// Accept all certifications.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="certification">The certification.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The ssl policy errors.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}