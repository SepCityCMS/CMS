// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="SuiteCRM.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using Newtonsoft.Json;
    using SepCommon.SepCore;
    using SugarRestSharp;
    using SugarRestSharp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Xml;

    /// <summary>
    /// Class CRM.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class CRM
    {
        /// <summary>
        /// Suites the create account.
        /// </summary>
        public void Suite_Create_Account()
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && (string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("SuiteID")) || SepFunctions.GetUserInformation("SuiteID") == "0"))
            {
                var client = new SugarRestClient(Suite_API_URL(), SepFunctions.Setup(67, "SuiteCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SuiteCRMPass")));

                var account = new Account
                {
                    Name = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName"),
                    Email = SepFunctions.GetUserInformation("EmailAddress"),
                    AddressStreet = SepFunctions.GetUserInformation("StreetAddress"),
                    AddressCity = SepFunctions.GetUserInformation("City"),
                    AddressState = SepFunctions.GetUserInformation("State"),
                    AddressPostalcode = SepFunctions.GetUserInformation("ZipCode"),
                    AddressCountry = SepFunctions.GetUserInformation("Country"),
                    PhoneOffice = SepFunctions.GetUserInformation("PhoneNumber")
                };
                var request = new SugarRestRequest("Accounts", RequestType.Create)
                {
                    Parameter = account
                };

                var selectedFields = new List<string>
                {
                    nameof(Account.Name),
                    nameof(Account.Email),
                    nameof(Account.AddressStreet),
                    nameof(Account.AddressCity),
                    nameof(Account.AddressState),
                    nameof(Account.AddressPostalcode),
                    nameof(Account.AddressCountry),
                    nameof(Account.PhoneOffice)
                };

                request.Options.SelectFields = selectedFields;

                var response = client.Execute(request);

                var insertId = response.Data == null ? string.Empty : Strings.ToString(response.Data);

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE Members SET SuiteID=@SuiteID WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SuiteID", insertId);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Suites the create ticket.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.String.</returns>
        public string Suite_Create_Ticket(string Subject, string Description)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                var client = new SugarRestClient(Suite_API_URL(), SepFunctions.Setup(67, "SuiteCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SuiteCRMPass")));

                var setValues = new Case
                {
                    Name = Subject,
                    AccountId = Suite_AccountID(),
                    DateEntered = DateTime.Now,
                    Deleted = 0,
                    Description = Description,
                    Status = "Open_New"
                };

                var request = new SugarRestRequest("Cases", RequestType.Create)
                {
                    Parameter = setValues
                };

                var selectedFields = new List<string>
                {
                    nameof(setValues.Name),
                    nameof(setValues.AccountId),
                    nameof(setValues.DateEntered),
                    nameof(setValues.Deleted),
                    nameof(setValues.Description),
                    nameof(setValues.Status)
                };

                request.Options.SelectFields = selectedFields;

                var response = client.Execute(request);

                var insertId = response.Data == null ? string.Empty : Strings.ToString(response.Data);

                return insertId;
            }

            return string.Empty;
        }

        /// <summary>
        /// Suites the get ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Suite_Get_Ticket(string ticketId)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                try
                {
                    var client = new SugarRestClient(Suite_API_URL(), SepFunctions.Setup(67, "SuiteCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SuiteCRMPass")));

                    var request = new SugarRestRequest(RequestType.ReadById)
                    {
                        Parameter = ticketId
                    };

                    var selectedFields = new List<string>
                    {
                        nameof(Case.Id),
                        nameof(Case.Name),
                        nameof(Case.Description),
                        nameof(Case.Status),
                        nameof(Case.DateModified),
                        nameof(Case.DateEntered),
                        nameof(Case.CaseNumber)
                    };
                    request.Options.SelectFields = selectedFields;

                    var response = client.Execute<Case>(request);

                    var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + response.JData + "}", "root");
                    var rootNodes = xmlRoot.SelectNodes("//root");

                    return rootNodes;
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log(ex.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Suites the knowledge base search.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Suite_KnowledgeBase_Search(string keywords)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                try
                {
                    var client = new SugarRestClient(Suite_API_URL(), SepFunctions.Setup(67, "SuiteCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SuiteCRMPass")));

                    var request = new SugarRestRequest(RequestType.BulkRead);
                    request.Options.QueryPredicates = new List<QueryPredicate>();

                    var response = client.Execute<SuiteCRM_KB>(request);

                    request = new SugarRestRequest(RequestType.BulkRead);
                    request.Options.QueryPredicates = new List<QueryPredicate>
                    {
                        new QueryPredicate(nameof(SuiteCRM_KB.Description), QueryOperator.Contains, keywords)
                    };

                    var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + response.JData + "}", "root");
                    var rootNodes = xmlRoot.SelectNodes("//root/Row");

                    return rootNodes;
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log(ex.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Suites the knowledge base view item.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Suite_KnowledgeBase_View_Item(string articleId)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                try
                {
                    var client = new SugarRestClient(Suite_API_URL(), SepFunctions.Setup(67, "SuiteCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SuiteCRMPass")));

                    var request = new SugarRestRequest(RequestType.ReadById)
                    {
                        Parameter = articleId
                    };

                    var selectedFields = new List<string>
                    {
                        nameof(SuiteCRM_KB.Id),
                        nameof(SuiteCRM_KB.Name),
                        nameof(SuiteCRM_KB.Description),
                        nameof(SuiteCRM_KB.Status),
                        nameof(SuiteCRM_KB.DateModified),
                        nameof(SuiteCRM_KB.DateEntered)
                    };
                    request.Options.SelectFields = selectedFields;

                    var response = client.Execute<SuiteCRM_KB>(request);

                    var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + response.JData + "}", "root");
                    var rootNodes = xmlRoot.SelectNodes("//root");

                    return rootNodes;
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log(ex.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Suites the list tickets.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Suite_List_Tickets()
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                var client = new SugarRestClient(Suite_API_URL(), SepFunctions.Setup(67, "SuiteCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SuiteCRMPass")));

                var request = new SugarRestRequest(RequestType.BulkRead);
                request.Options.QueryPredicates = new List<QueryPredicate>();

                var response = client.Execute<Case>(request);

                request = new SugarRestRequest(RequestType.BulkRead);
                request.Options.QueryPredicates = new List<QueryPredicate>
                {
                    new QueryPredicate(nameof(Case.AccountId), QueryOperator.Equal, Suite_AccountID())
                };

                var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + response.JData + "}", "root");
                var rootNodes = xmlRoot.SelectNodes("//root/Row");

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Suites the account identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        private string Suite_AccountID()
        {
            var accountId = SepFunctions.GetUserInformation("SuiteID");

            if (string.IsNullOrWhiteSpace(accountId) || accountId == "0")
            {
                Suite_Create_Account();
            }

            accountId = SepFunctions.GetUserInformation("SuiteID");

            return accountId;
        }

        /// <summary>
        /// Suites the API URL.
        /// </summary>
        /// <returns>System.String.</returns>
        private string Suite_API_URL()
        {
            var sURL = SepFunctions.Format_URL(SepFunctions.Setup(ModuleID, "SuiteCRMURL"));
            if (Strings.Right(sURL, 1) != "/")
            {
                sURL += "/";
            }

            return sURL + "service/v4_1/rest.php";
        }
    }
}