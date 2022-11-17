// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="MultiSafepay.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// A multi safepay.
    /// </summary>
    public class MultiSafepay : IDisposable
    {
        // -V3073
        /// <summary>
        /// The common.
        /// </summary>
        public CustomerStruct Customer = new CustomerStruct();

        /// <summary>
        /// The error code.
        /// </summary>
        public string ErrorCode;

        /// <summary>
        /// Information describing the error.
        /// </summary>
        public string ErrorDescription;

        /// <summary>
        /// The merchant.
        /// </summary>
        public MerchantStruct Merchant;

        /// <summary>
        /// The signature.
        /// </summary>
        public string signature;

        /// <summary>
        /// The transaction.
        /// </summary>
        public TransactionStruct Transaction = new TransactionStruct();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MultiSafepay()
        {
            LoadSettings();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <returns>The status.</returns>
        public string GetStatus()
        {
            string outgoingTRANSaction = null;

            outgoingTRANSaction = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            outgoingTRANSaction += "<status>" + Environment.NewLine;
            outgoingTRANSaction += "  <merchant>" + Environment.NewLine;
            outgoingTRANSaction += "    <account>" + XmlEscape(Merchant.AccountId) + "</account>" + Environment.NewLine;
            outgoingTRANSaction += "    <site_id>" + XmlEscape(Merchant.SiteId) + "</site_id>" + Environment.NewLine;
            outgoingTRANSaction += "    <site_secure_code>" + XmlEscape(Merchant.SiteCode) + "</site_secure_code>" + Environment.NewLine;
            outgoingTRANSaction += "  </merchant>" + Environment.NewLine;
            outgoingTRANSaction += "  <transaction>" + Environment.NewLine;
            outgoingTRANSaction += "    <id>" + XmlEscape(Transaction.ID) + "</id>" + Environment.NewLine;
            outgoingTRANSaction += "  </transaction>" + Environment.NewLine;
            outgoingTRANSaction += "</status>" + Environment.NewLine;

            string apiURl = null;
            if (Merchant.TestAccount)
            {
                apiURl = "https://testapi.multisafepay.com/ewx/";
            }
            else
            {
                apiURl = "https://api.multisafepay.com/ewx/";
            }

            var httpWebRequest = WebRequest.Create(apiURl);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = Encoding.UTF8.GetByteCount(outgoingTRANSaction);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(outgoingTRANSaction);
            }

            var httpWebResponse = httpWebRequest.GetResponse();
            var streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            var stringResult = streamReader.ReadToEnd();
            streamReader.Dispose();
            var xmlstring = stringResult;
            var xd = new XmlDocument();
            xd.LoadXml(xmlstring);

            var root = xd.DocumentElement;
            var result = root.Attributes.GetNamedItem("result").Value;

            if (result == "ok")
            {
                var status = xd.SelectSingleNode("/status/ewallet/status").InnerText;

                xd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return status;
            }

            ErrorCode = xd.SelectSingleNode("/status/error/code").InnerText;
            ErrorDescription = xd.SelectSingleNode("/status/error/description").InnerText;

            xd = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return string.Empty;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadSettings()
        {
            var sMSP = string.Empty;
            var sAccountID = string.Empty;
            var sSiteID = string.Empty;
            var sSiteCode = string.Empty;
            var sRelayURL = string.Empty;
            var sSSLServer = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                var sXML = SepFunctions.openNull(RS["ScriptText"]);
                                sMSP = SepFunctions.ParseXML("MultiSafePay", sXML);
                                sAccountID = SepFunctions.ParseXML("AccountID", sMSP);
                                sSiteID = SepFunctions.ParseXML("SiteID", sMSP);
                                sSiteCode = SepFunctions.ParseXML("SiteCode", sMSP);
                                sSSLServer = SepFunctions.ParseXML("SSLServer", SepFunctions.ParseXML("SSL", sXML));
                            }
                        }
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID='" + SepFunctions.Get_Portal_ID() + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                var sXML = SepFunctions.openNull(RS["ScriptText"]);
                                sAccountID = SepFunctions.ParseXML("MSPAccountID", sXML);
                                sSiteID = SepFunctions.ParseXML("MSPSiteID", sXML);
                                sSiteCode = SepFunctions.ParseXML("MSPSiteCode", sXML);
                                sSSLServer = SepFunctions.ParseXML("SSLServer", sXML);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(sSSLServer))
            {
                sRelayURL = sSSLServer + "/payments.aspx";
            }
            else
            {
                sRelayURL = SepFunctions.GetMasterDomain(true) + "/payments.aspx";
            }

            Merchant.AccountId = sAccountID;
            Merchant.SiteId = sSiteID;
            Merchant.SiteCode = sSiteCode;
            Merchant.NotificationUrl = sRelayURL;
        }

        /// <summary>
        /// Starts a transaction.
        /// </summary>
        /// <returns>A string.</returns>
        public string StartTransaction()
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes = null;
            var encoder = new UTF8Encoding();
            string outgoingTRANSaction = null;

            string ipAddress = null;
            string forwardedIp = null;

            ipAddress = SepCore.Request.ServerVariables("REMOTE_ADDR");
            forwardedIp = SepCore.Request.ServerVariables("HTTP_X_FORWARDED_FOR");

            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Transaction.Amount + Transaction.Currency + Merchant.AccountId + Merchant.SiteId + Transaction.ID));
            string signature = SepCore.Strings.LCase(BitConverter.ToString(hashedBytes).Replace("-", string.Empty));

            md5Hasher.Dispose();

            // create this programatically?
            outgoingTRANSaction = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            outgoingTRANSaction += "<redirecttransaction>" + Environment.NewLine;
            outgoingTRANSaction += "  <merchant>" + Environment.NewLine;
            outgoingTRANSaction += "    <account>" + XmlEscape(Merchant.AccountId) + "</account>" + Environment.NewLine;
            outgoingTRANSaction += "    <site_id>" + XmlEscape(Merchant.SiteId) + "</site_id>" + Environment.NewLine;
            outgoingTRANSaction += "    <site_secure_code>" + XmlEscape(Merchant.SiteCode) + "</site_secure_code>" + Environment.NewLine;
            outgoingTRANSaction += "    <notification_url>" + XmlEscape(Merchant.NotificationUrl) + "</notification_url>" + Environment.NewLine;
            outgoingTRANSaction += "    <redirect_url>" + XmlEscape(Merchant.RedirectUrl) + "</redirect_url>" + Environment.NewLine;
            outgoingTRANSaction += "    <cancel_url>" + XmlEscape(Merchant.CancelUrl) + "</cancel_url>" + Environment.NewLine;
            outgoingTRANSaction += "  </merchant>" + Environment.NewLine;
            outgoingTRANSaction += "  <customer>" + Environment.NewLine;
            outgoingTRANSaction += "    <locale>" + XmlEscape(Customer.Locale) + "</locale>" + Environment.NewLine;
            outgoingTRANSaction += "    <ipaddress>" + ipAddress + "</ipaddress>" + Environment.NewLine;
            outgoingTRANSaction += "    <forwardedip>" + forwardedIp + "</forwardedip>" + Environment.NewLine;
            outgoingTRANSaction += "    <firstname>" + XmlEscape(Customer.Firstname) + "</firstname>" + Environment.NewLine;
            outgoingTRANSaction += "    <lastname>" + XmlEscape(Customer.Lastname) + "</lastname>" + Environment.NewLine;
            outgoingTRANSaction += "    <address1>" + XmlEscape(Customer.Address1) + "</address1>" + Environment.NewLine;
            outgoingTRANSaction += "    <address2>" + XmlEscape(Customer.Address2) + "</address2>" + Environment.NewLine;
            outgoingTRANSaction += "    <housenumber>" + XmlEscape(Customer.Housenumber) + "</housenumber>" + Environment.NewLine;
            outgoingTRANSaction += "    <zipcode>" + XmlEscape(Customer.Zipcode) + "</zipcode>" + Environment.NewLine;
            outgoingTRANSaction += "    <city>" + XmlEscape(Customer.City) + "</city>" + Environment.NewLine;
            outgoingTRANSaction += "    <state>" + XmlEscape(Customer.State) + "</state>" + Environment.NewLine;
            outgoingTRANSaction += "    <country>" + XmlEscape(Customer.Country) + "</country>" + Environment.NewLine;
            outgoingTRANSaction += "    <phone>" + XmlEscape(Customer.Phone) + "</phone>" + Environment.NewLine;
            outgoingTRANSaction += "    <email>" + XmlEscape(Customer.Email) + "</email>" + Environment.NewLine;
            outgoingTRANSaction += "  </customer>" + Environment.NewLine;
            outgoingTRANSaction += "  <transaction>" + Environment.NewLine;
            outgoingTRANSaction += "    <id>" + XmlEscape(Transaction.ID) + "</id>" + Environment.NewLine;
            outgoingTRANSaction += "    <currency>" + XmlEscape(Transaction.Currency) + "</currency>" + Environment.NewLine;
            outgoingTRANSaction += "    <amount>" + XmlEscape(Transaction.Amount) + "</amount>" + Environment.NewLine;
            outgoingTRANSaction += "    <description>" + XmlEscape(Transaction.Description) + "</description>" + Environment.NewLine;
            outgoingTRANSaction += "    <var1>" + XmlEscape(Transaction.Var1) + "</var1>" + Environment.NewLine;
            outgoingTRANSaction += "    <var2>" + XmlEscape(Transaction.Var2) + "</var2>" + Environment.NewLine;
            outgoingTRANSaction += "    <var3>" + XmlEscape(Transaction.Var3) + "</var3>" + Environment.NewLine;
            outgoingTRANSaction += "    <items>" + XmlEscape(Transaction.Items) + "</items>" + Environment.NewLine;
            outgoingTRANSaction += "    <manual>false</manual>" + Environment.NewLine;
            outgoingTRANSaction += "  </transaction>" + Environment.NewLine;
            outgoingTRANSaction += "  <signature>" + signature + "</signature>" + Environment.NewLine;
            outgoingTRANSaction += "</redirecttransaction>" + Environment.NewLine;

            string apiURl = null;
            if (Merchant.TestAccount)
            {
                apiURl = "https://testapi.multisafepay.com/ewx/";
            }
            else
            {
                apiURl = "https://api.multisafepay.com/ewx/";
            }

            var httpWebRequest = WebRequest.Create(apiURl);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = Encoding.UTF8.GetByteCount(outgoingTRANSaction);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(outgoingTRANSaction);
            }

            var httpWebResponse = httpWebRequest.GetResponse();
            var streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            var stringResult = streamReader.ReadToEnd();
            streamReader.Dispose();
            var xmlstring = stringResult;
            var xd = new XmlDocument();
            xd.LoadXml(xmlstring);

            var root = xd.DocumentElement;
            var result = root.Attributes.GetNamedItem("result").Value;

            if (result == "ok")
            {
                var paymentUrl = xd.SelectSingleNode("/redirecttransaction/transaction/payment_url").InnerText;

                xd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return paymentUrl;
            }

            ErrorCode = xd.SelectSingleNode("/redirecttransaction/error/code").InnerText;
            ErrorDescription = xd.SelectSingleNode("/redirecttransaction/error/description").InnerText;

            xd = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return string.Empty;
        }

        /// <summary>
        /// Gets the test.
        /// </summary>
        /// <returns>A string.</returns>
        public string Test()
        {
            return Merchant.AccountId;
        }

        /// <summary>
        /// XML escape.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>A string.</returns>
        public string XmlEscape(string content)
        {
            return SepFunctions.HTMLEncode(content);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the MultiSafepay and optionally
        /// releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }

        /// <summary>
        /// A customer structure.
        /// </summary>
        public struct CustomerStruct
        {
            // Override equals and operator equals on value types
            /// <summary>
            /// The locale.
            /// </summary>
            public string Locale;

            /// <summary>
            /// The IP address.
            /// </summary>
            public string IpAddress;

            /// <summary>
            /// The firstname.
            /// </summary>
            public string Firstname;

            /// <summary>
            /// The lastname.
            /// </summary>
            public string Lastname;

            /// <summary>
            /// The first address.
            /// </summary>
            public string Address1;

            /// <summary>
            /// The second address.
            /// </summary>
            public string Address2;

            /// <summary>
            /// The housenumber.
            /// </summary>
            public string Housenumber;

            /// <summary>
            /// The zipcode.
            /// </summary>
            public string Zipcode;

            /// <summary>
            /// The city.
            /// </summary>
            public string City;

            /// <summary>
            /// The state.
            /// </summary>
            public string State;

            /// <summary>
            /// The country.
            /// </summary>
            public string Country;

            /// <summary>
            /// The phone.
            /// </summary>
            public string Phone;

            /// <summary>
            /// The email.
            /// </summary>
            public string Email;
        }

        /// <summary>
        /// A merchant structure.
        /// </summary>
        public struct MerchantStruct
        {
            // Override equals and operator equals on value types
            /// <summary>
            /// True to test account.
            /// </summary>
            public bool TestAccount;

            /// <summary>
            /// Identifier for the account.
            /// </summary>
            public string AccountId;

            /// <summary>
            /// Identifier for the site.
            /// </summary>
            public string SiteId;

            /// <summary>
            /// The site code.
            /// </summary>
            public string SiteCode;

            /// <summary>
            /// URL of the notification.
            /// </summary>
            public string NotificationUrl;

            /// <summary>
            /// URL of the redirect.
            /// </summary>
            public string RedirectUrl;

            /// <summary>
            /// URL of the return.
            /// </summary>
            public string ReturnUrl;

            /// <summary>
            /// URL of the cancel.
            /// </summary>
            public string CancelUrl;
        }

        /// <summary>
        /// A transaction structure.
        /// </summary>
        public struct TransactionStruct
        {
            // Override equals and operator equals on value types
            /// <summary>
            /// The identifier.
            /// </summary>
            public string ID;

            /// <summary>
            /// The currency.
            /// </summary>
            public string Currency;

            /// <summary>
            /// The amount.
            /// </summary>
            public string Amount;

            /// <summary>
            /// The description.
            /// </summary>
            public string Description;

            /// <summary>
            /// The first variable.
            /// </summary>
            public string Var1;

            /// <summary>
            /// The second variable.
            /// </summary>
            public string Var2;

            /// <summary>
            /// The third variable.
            /// </summary>
            public string Var3;

            /// <summary>
            /// The items.
            /// </summary>
            public string Items;
        }
    }
}