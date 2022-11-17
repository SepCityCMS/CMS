// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="WHMCS.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using Newtonsoft.Json;
    using SepCommon.SepCore;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Class CRM.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class CRM
    {
        /// <summary>
        /// WHMCSs the create account.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList WHMCS_Create_Account()
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = WHMCS_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var WHMCSclient = new WebClient();

                var form = new NameValueCollection
                {
                    { "username", STUser },
                    { "password", STPass },
                    { "action", "addcontact" },
                    { "clientid", "1" },
                    { "firstname", SepFunctions.GetUserInformation("FirstName") },
                    { "lastname", SepFunctions.GetUserInformation("LastName") },
                    { "email", SepFunctions.GetUserInformation("EmailAddress") },
                    { "address1", SepFunctions.GetUserInformation("StreetAddress") },
                    { "city", SepFunctions.GetUserInformation("City") },
                    { "state", SepFunctions.GetUserInformation("State") },
                    { "postcode", SepFunctions.GetUserInformation("ZipCode") },
                    { "country", SepFunctions.GetUserInformation("Country") },
                    { "phonenumber", SepFunctions.GetUserInformation("PhoneNumber") },
                    { "responsetype", "json" }
                };

                var responseData = WHMCSclient.UploadValues(WHMCS_API_URL(), form);

                var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + Encoding.ASCII.GetString(responseData) + "}", "root");
                var rootNodes = xmlRoot.SelectNodes("//root/Row");
                WHMCSclient.Dispose();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// WHMCSs the create ticket.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList WHMCS_Create_Ticket(string subject, string message)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = WHMCS_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var WHMCSclient = new WebClient();

                var form = new NameValueCollection
                {
                    { "username", STUser },
                    { "password", STPass },
                    { "action", "OpenTicket" },
                    { "clientid", "1" },
                    { "deptid", "1" },
                    { "subject", subject },
                    { "message", message },
                    { "priority", "High" },
                    { "responsetype", "json" }
                };

                var responseData = WHMCSclient.UploadValues(WHMCS_API_URL(), form);

                var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + Encoding.ASCII.GetString(responseData) + "}", "root");
                var rootNodes = xmlRoot.SelectNodes("//root/Row");

                WHMCSclient.Dispose();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// WHMCSs the get ticket.
        /// </summary>
        /// <param name="ticketNumber">The ticket number.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList WHMCS_Get_Ticket(string ticketNumber)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = WHMCS_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var WHMCSclient = new WebClient();

                var form = new NameValueCollection
                {
                    { "username", STUser },
                    { "password", STPass },
                    { "action", "GetTicket" },
                    { "ticketnum", ticketNumber },
                    { "responsetype", "json" }
                };

                var responseData = WHMCSclient.UploadValues(WHMCS_API_URL(), form);

                var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + Encoding.ASCII.GetString(responseData) + "}", "root");
                var rootNodes = xmlRoot.SelectNodes("//root/Row");
                WHMCSclient.Dispose();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// WHMCSs the list tickets.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList WHMCS_List_Tickets()
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = WHMCS_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var WHMCSclient = new WebClient();

                var form = new NameValueCollection
                {
                    { "username", STUser },
                    { "password", STPass },
                    { "action", "GetTickets" },
                    { "email", SepFunctions.GetUserInformation("EmailAddress") },
                    { "responsetype", "json" }
                };

                var responseData = WHMCSclient.UploadValues(WHMCS_API_URL(), form);

                var xmlRoot = JsonConvert.DeserializeXmlNode("{\"Row\":" + Encoding.ASCII.GetString(responseData) + "}", "root");
                var rootNodes = xmlRoot.SelectNodes("//root/Row");
                WHMCSclient.Dispose();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// WHMCSs the API URL.
        /// </summary>
        /// <returns>System.String.</returns>
        private string WHMCS_API_URL()
        {
            var sURL = SepFunctions.Format_URL(SepFunctions.Setup(ModuleID, "WHMCSURL"));
            if (Strings.Right(sURL, 1) != "/")
            {
                sURL += "/";
            }

            return sURL;
        }

        /// <summary>
        /// WHMCSs the load configuration.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool WHMCS_Load_Config(ref string UserName, ref string Password)
        {
            UserName = SepFunctions.Setup(67, "WHMCSUser");
            Password = SepFunctions.Decrypt(SepFunctions.Setup(67, "WHMCSPass"));

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return true;
        }
    }
}