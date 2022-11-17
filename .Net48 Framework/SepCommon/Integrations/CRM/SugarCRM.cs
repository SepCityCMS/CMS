// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="SugarCRM.cs" company="SepCity, Inc.">
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
        /// Sugars the create account.
        /// </summary>
        public void Sugar_Create_Account()
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && (string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("SugarID")) || SepFunctions.GetUserInformation("SugarID") == "0"))
            {
                var client = new SugarRestClient(Sugar_API_URL(), SepFunctions.Setup(67, "SugarCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SugarCRMPass")));

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
                    using (var cmd = new SqlCommand("UPDATE Members SET SugarID=@SugarID WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SugarID", insertId);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Sugars the create ticket.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.String.</returns>
        public string Sugar_Create_Ticket(string Subject, string Description)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                var client = new SugarRestClient(Sugar_API_URL(), SepFunctions.Setup(67, "SugarCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SugarCRMPass")));

                var setValues = new Case
                {
                    Name = Subject,
                    AccountId = Sugar_AccountID(),
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
        /// Sugars the get ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Sugar_Get_Ticket(string ticketId)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                try
                {
                    var client = new SugarRestClient(Sugar_API_URL(), SepFunctions.Setup(67, "SugarCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SugarCRMPass")));

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
        /// Sugars the list tickets.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Sugar_List_Tickets()
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                var client = new SugarRestClient(Sugar_API_URL(), SepFunctions.Setup(67, "SugarCRMUser"), SepFunctions.Decrypt(SepFunctions.Setup(67, "SugarCRMPass")));

                var request = new SugarRestRequest(RequestType.BulkRead);
                request.Options.QueryPredicates = new List<QueryPredicate>();

                request = new SugarRestRequest(RequestType.BulkRead);
                request.Options.QueryPredicates = new List<QueryPredicate>
                {
                    new QueryPredicate(nameof(Case.AccountId), QueryOperator.Equal, Sugar_AccountID())
                };

                var response = client.Execute<Case>(request);

                var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + response.JData + "}", "root");
                var rootNodes = xmlRoot.SelectNodes("//root/Row");

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sugars the account identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        private string Sugar_AccountID()
        {
            var accountId = SepFunctions.GetUserInformation("SugarID");

            if (string.IsNullOrWhiteSpace(accountId) || accountId == "0")
            {
                Sugar_Create_Account();
            }

            accountId = SepFunctions.GetUserInformation("SugarID");

            return accountId;
        }

        /// <summary>
        /// Sugars the API URL.
        /// </summary>
        /// <returns>System.String.</returns>
        private string Sugar_API_URL()
        {
            var sURL = SepFunctions.Format_URL(SepFunctions.Setup(ModuleID, "SugarCRMURL"));
            if (Strings.Right(sURL, 1) != "/")
            {
                sURL += "/";
            }

            return sURL + "service/v4_1/rest.php";
        }
    }
}