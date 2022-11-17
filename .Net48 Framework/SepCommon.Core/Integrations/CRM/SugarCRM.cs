// ***********************************************************************
// Assembly         : SepCommon.Core
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

namespace SepCommon.Core
{
    using Newtonsoft.Json;
    using SepCommon.Core.SepCore;
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

                var account = new Account();
                account.Name = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName");
                account.Email = SepFunctions.GetUserInformation("EmailAddress");
                account.AddressStreet = SepFunctions.GetUserInformation("StreetAddress");
                account.AddressCity = SepFunctions.GetUserInformation("City");
                account.AddressState = SepFunctions.GetUserInformation("State");
                account.AddressPostalcode = SepFunctions.GetUserInformation("ZipCode");
                account.AddressCountry = SepFunctions.GetUserInformation("Country");
                account.PhoneOffice = SepFunctions.GetUserInformation("PhoneNumber");
                var request = new SugarRestRequest("Accounts", RequestType.Create);
                request.Parameter = account;

                var selectedFields = new List<string>();

                selectedFields.Add(nameof(Account.Name));
                selectedFields.Add(nameof(Account.Email));
                selectedFields.Add(nameof(Account.AddressStreet));
                selectedFields.Add(nameof(Account.AddressCity));
                selectedFields.Add(nameof(Account.AddressState));
                selectedFields.Add(nameof(Account.AddressPostalcode));
                selectedFields.Add(nameof(Account.AddressCountry));
                selectedFields.Add(nameof(Account.PhoneOffice));

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

                var setValues = new Case();
                setValues.Name = Subject;
                setValues.AccountId = Sugar_AccountID();
                setValues.DateEntered = DateTime.Now;
                setValues.Deleted = 0;
                setValues.Description = Description;
                setValues.Status = "Open_New";

                var request = new SugarRestRequest("Cases", RequestType.Create);
                request.Parameter = setValues;

                var selectedFields = new List<string>();

                selectedFields.Add(nameof(setValues.Name));
                selectedFields.Add(nameof(setValues.AccountId));
                selectedFields.Add(nameof(setValues.DateEntered));
                selectedFields.Add(nameof(setValues.Deleted));
                selectedFields.Add(nameof(setValues.Description));
                selectedFields.Add(nameof(setValues.Status));

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

                    var request = new SugarRestRequest(RequestType.ReadById);
                    request.Parameter = ticketId;

                    var selectedFields = new List<string>();

                    selectedFields.Add(nameof(Case.Id));
                    selectedFields.Add(nameof(Case.Name));
                    selectedFields.Add(nameof(Case.Description));
                    selectedFields.Add(nameof(Case.Status));
                    selectedFields.Add(nameof(Case.DateModified));
                    selectedFields.Add(nameof(Case.DateEntered));
                    selectedFields.Add(nameof(Case.CaseNumber));
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
                request.Options.QueryPredicates = new List<QueryPredicate>();
                request.Options.QueryPredicates.Add(new QueryPredicate(nameof(Case.AccountId), QueryOperator.Equal, Sugar_AccountID()));

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