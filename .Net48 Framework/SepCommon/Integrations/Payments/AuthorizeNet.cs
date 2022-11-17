// ***********************************************************************
// Assembly         : sepcity.com
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AuthorizeNet.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using SepCommon.SepCore;
    using System;
    using System.Xml;

    /// <summary>
    /// Class AuthorizeNet.
    /// </summary>
    public class AuthorizeNet
    {
        /// <summary>
        /// The b debug mode
        /// </summary>
        private bool bDebugMode;

        /// <summary>
        /// The API URL
        /// </summary>
        private string gstrApiUrl = "https://api.authorize.net/xml/v1/request.api";

        /// <summary>
        /// Gets or sets the name of the login.
        /// </summary>
        /// <value>The name of the login.</value>
        public string gstrLoginName { get; set; }

        /// <summary>
        /// Gets or sets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string gstrTransactionKey { get; set; }

        /// <summary>
        /// Creates the payment.
        /// </summary>
        /// <param name="AuthorizeProfileID">The authorize profile identifier.</param>
        /// <param name="AuthorizePaymentID">The authorize payment identifier.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="InvoiceId">The invoice identifier.</param>
        /// <param name="ItemName">Name of the item.</param>
        /// <param name="Amount">The amount.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Create_Payment(string AuthorizeProfileID, string AuthorizePaymentID, string UserName, string InvoiceId, string ItemName, string Amount)
        {
            if (SepFunctions.DebugMode)
            {
                bDebugMode = true;
            }

            if (bDebugMode)
            {
                gstrApiUrl = "https://apitest.authorize.net/xml/v1/request.api";
            }

            string itemId;
            switch (ItemName)
            {
                case "HostingStandard":
                    itemId = "ITEM0001";
                    ItemName = "Standard Portal Hosting";
                    break;

                case "HostingEnterprise":
                    itemId = "ITEM0002";
                    ItemName = "Enterprise Portal Hosting";
                    break;

                case "Domain":
                    itemId = "ITEM0003";
                    ItemName = "Domain Name";
                    break;

                default:
                    itemId = "ITEM0004";
                    break;
            }

            string strReq = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "<createCustomerProfileTransactionRequest xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\">" + "<merchantAuthentication>" + "  <name>" + gstrLoginName + "</name>" + "  <transactionKey>" + gstrTransactionKey + "</transactionKey>" + "</merchantAuthentication>" + "<transaction>" + "  <profileTransAuthCapture>" + "    <amount>" + Amount + "</amount>" + "    <lineItems>" + "      <itemId>" + itemId + "</itemId>" + "      <name>" + ItemName + "</name>" + "      <description>" + ItemName + "</description>" + "      <quantity>1</quantity>" + "      <unitPrice>" + Amount + "</unitPrice>" + "      <taxable>false</taxable>" + "    </lineItems>" + "    <customerProfileId>" + AuthorizeProfileID + "</customerProfileId>" + "    <customerPaymentProfileId>" + AuthorizePaymentID + "</customerPaymentProfileId>" + "    <order>" + "      <invoiceNumber>" + InvoiceId + "</invoiceNumber>" + "      <description>" + ItemName + "</description>" + "    </order>" + "  </profileTransAuthCapture>" + "</transaction>" + "</createCustomerProfileTransactionRequest>";
            string objResponse = SepFunctions.Send_XML(gstrApiUrl, strReq, "text/xml");
            var doc = new XmlDocument();
            doc.LoadXml(objResponse);
            var root = doc.DocumentElement;
            string xmlResult = SepFunctions.ParseXML("resultCode", root.OuterXml);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (xmlResult == "Ok")
            {
                SepFunctions.Activity_Write("PAYMENT", UserName + " has successfully created payment Request on Authorize.Net. Invoice #" + InvoiceId, 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
                return true;
            }

            SepFunctions.Activity_Write("PAYMENT", UserName + " has failed to create payment Request on Authorize.Net. Invoice #" + InvoiceId, 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
            return false;
        }

        /// <summary>
        /// Creates the payment profile.
        /// </summary>
        /// <param name="AuthorizeProfileID">The authorize profile identifier.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="CompanyName">Name of the company.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="CardNumber">The card number.</param>
        /// <param name="CardExpireMonth">The card expire month.</param>
        /// <param name="CardExpireYear">The card expire year.</param>
        /// <param name="CardCode">The card code.</param>
        /// <param name="AuthorizePaymentID">The authorize payment identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Create_Payment_Profile(string AuthorizeProfileID, string UserName, string CompanyName, string FirstName, string LastName, string StreetAddress, string City, string State, string PostalCode, string Country, string PhoneNumber, string CardNumber, string CardExpireMonth, string CardExpireYear, string CardCode, ref string AuthorizePaymentID)
        {
            var validationMode = "liveMode";
            if (SepFunctions.DebugMode)
            {
                bDebugMode = true;
            }

            if (bDebugMode)
            {
                gstrApiUrl = "https://apitest.authorize.net/xml/v1/request.api";
                validationMode = "testMode";
            }

            string strReq = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "<createCustomerPaymentProfileRequest xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\">" + "<merchantAuthentication>" + "  <name>" + gstrLoginName + "</name>" + "  <transactionKey>" + gstrTransactionKey + "</transactionKey>" + "</merchantAuthentication>" + "<customerProfileId>" + AuthorizeProfileID + "</customerProfileId>" + "\r\n" + "<paymentProfile>" + "\r\n" + "  <customerType>individual</customerType>" + "\r\n" + "     <billTo>" + "\r\n" + "        <firstName>" + FirstName + "</firstName>" + "\r\n" + "        <lastName>" + LastName + "</lastName>" + "\r\n" + "        <company>" + CompanyName + "</company>" + "\r\n" + "        <address>" + StreetAddress + "</address>" + "\r\n" + "        <city>" + City + "</city>" + "\r\n" + "        <state>" + State + "</state>" + "\r\n" + "        <zip>" + PostalCode + "</zip>" + "\r\n" + "        <country>" + Country + "</country>" + "\r\n" + "        <phoneNumber>" + PhoneNumber + "</phoneNumber>" + "\r\n" + "     </billTo>" + "\r\n" + "     <payment>" + "\r\n" + "         <creditCard>" + "\r\n" + "             <cardNumber>" + CardNumber + "</cardNumber>" + "\r\n" + "             <expirationDate>" + CardExpireYear + "-" + (CardExpireMonth.Length == 1 ? "0" : string.Empty) + CardExpireMonth + "</expirationDate>" + "\r\n" + "             <cardCode>" + CardCode + "</cardCode>" + "\r\n" + "         </creditCard>" + "\r\n" + "     </payment>" + "\r\n" + "</paymentProfile>" + "\r\n" + "<validationMode>" + validationMode + "</validationMode>" + "\r\n" + "</createCustomerPaymentProfileRequest>";
            string objResponse = SepFunctions.Send_XML(gstrApiUrl, strReq, "text/xml");
            var doc = new XmlDocument();
            doc.LoadXml(objResponse);
            var root = doc.DocumentElement;
            string xmlResult = SepFunctions.ParseXML("resultCode", root.OuterXml);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (xmlResult == "Ok")
            {
                SepFunctions.Activity_Write("PAYMENT", UserName + " has successfully created payment profile on Authorize.Net.", 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
                AuthorizePaymentID = SepFunctions.ParseXML("customerPaymentProfileId", root.OuterXml);
                return true;
            }

            SepFunctions.Activity_Write("PAYMENT", UserName + " has failed to create payment profile on Authorize.Net.", 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
            return false;
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <param name="CustomerId">The customer identifier.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="CardNumber">The card number.</param>
        /// <param name="CardExpireMonth">The card expire month.</param>
        /// <param name="CardExpireYear">The card expire year.</param>
        /// <param name="CardCode">The card code.</param>
        /// <param name="AuthorizeProfileID">The authorize profile identifier.</param>
        /// <param name="AuthorizePaymentID">The authorize payment identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Create_Profile(string CustomerId, string UserName, string EmailAddress, string FirstName, string LastName, string StreetAddress, string City, string State, string PostalCode, string Country, string PhoneNumber, string CardNumber, string CardExpireMonth, string CardExpireYear, string CardCode, ref string AuthorizeProfileID, ref string AuthorizePaymentID)
        {
            var validationMode = "liveMode";
            if (SepFunctions.DebugMode)
            {
                bDebugMode = true;
            }

            if (bDebugMode)
            {
                gstrApiUrl = "https://apitest.authorize.net/xml/v1/request.api";
                validationMode = "testMode";
            }

            string strReq = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n" + "<createCustomerProfileRequest xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\">" + "\r\n" + "<merchantAuthentication>" + "\r\n" + "  <name>" + gstrLoginName + "</name>" + "\r\n" + "  <transactionKey>" + gstrTransactionKey + "</transactionKey>" + "\r\n" + "</merchantAuthentication>" + "\r\n" + "<profile>" + "\r\n" + "  <merchantCustomerId>" + CustomerId + "</merchantCustomerId>" + "\r\n" + "  <description>Invoice #" + SepFunctions.Session_Invoice_ID() + "</description>" + "\r\n" + "  <email>" + EmailAddress + "</email>" + "\r\n" + "  <paymentProfiles>" + "\r\n" + "     <customerType>individual</customerType>" + "\r\n" + "     <billTo>" + "\r\n" + "        <firstName>" + FirstName + "</firstName>" + "\r\n" + "        <lastName>" + LastName + "</lastName>" + "\r\n" + "        <company></company>" + "\r\n" + "        <address>" + StreetAddress + "</address>" + "\r\n" + "        <city>" + City + "</city>" + "\r\n" + "        <state>" + State + "</state>" + "\r\n" + "        <zip>" + PostalCode + "</zip>" + "\r\n" + "        <country>" + Country + "</country>" + "\r\n" + "        <phoneNumber>" + PhoneNumber + "</phoneNumber>" + "\r\n" + "     </billTo>" + "\r\n" + "     <payment>" + "\r\n" + "         <creditCard>" + "\r\n" + "             <cardNumber>" + CardNumber + "</cardNumber>" + "\r\n" + "             <expirationDate>" + CardExpireYear + "-" + (CardExpireMonth.Length == 1 ? "0" : string.Empty) + CardExpireMonth + "</expirationDate>" + "\r\n" + "             <cardCode>" + CardCode + "</cardCode>" + "\r\n" + "         </creditCard>" + "\r\n" + "     </payment>" + "\r\n" + "  </paymentProfiles>" + "\r\n" + "</profile>" + "\r\n" + "<validationMode>" + validationMode + "</validationMode>" + "\r\n" + "</createCustomerProfileRequest>" + "\r\n";
            string objResponse = SepFunctions.Send_XML(gstrApiUrl, strReq, "text/xml");
            var doc = new XmlDocument();
            doc.LoadXml(objResponse);
            var root = doc.DocumentElement;

            string xmlResult = SepFunctions.ParseXML("resultCode", root.OuterXml);
            if (xmlResult == "Ok")
            {
                SepFunctions.Activity_Write("PAYMENT", UserName + " has successfully created profile on Authorize.Net.", 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
                AuthorizeProfileID = SepFunctions.ParseXML("customerProfileId", root.OuterXml);
                AuthorizePaymentID = SepFunctions.ParseXML("numericString", SepFunctions.ParseXML("customerPaymentProfileIdList", root.OuterXml));
                return true;
            }

            if (Strings.InStr(SepFunctions.ParseXML("text", root.OuterXml), "duplicate") > 0)
            {
                AuthorizeProfileID = Strings.Replace(Strings.Replace(Strings.Replace(SepFunctions.ParseXML("text", root.OuterXml), "A duplicate record with ID", string.Empty), "already exists.", string.Empty), " ", string.Empty);
            }
            else
            {
                SepFunctions.Activity_Write("PAYMENT", UserName + " has failed to create profile on Authorize.Net.", 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return false;
        }

        /// <summary>
        /// Updates the payment profile.
        /// </summary>
        /// <param name="AuthorizeProfileID">The authorize profile identifier.</param>
        /// <param name="AuthorizePaymentID">The authorize payment identifier.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="CompanyName">Name of the company.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="CardNumber">The card number.</param>
        /// <param name="CardExpireMonth">The card expire month.</param>
        /// <param name="CardExpireYear">The card expire year.</param>
        /// <param name="CardCode">The card code.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Update_Payment_Profile(string AuthorizeProfileID, string AuthorizePaymentID, string UserName, string CompanyName, string FirstName, string LastName, string StreetAddress, string City, string State, string PostalCode, string Country, string PhoneNumber, string CardNumber, string CardExpireMonth, string CardExpireYear, string CardCode)
        {
            var validationMode = "liveMode";
            if (SepFunctions.DebugMode)
            {
                bDebugMode = true;
            }

            if (bDebugMode)
            {
                gstrApiUrl = "https://apitest.authorize.net/xml/v1/request.api";
                validationMode = "testMode";
            }

            string strReq = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "<updateCustomerPaymentProfileRequest xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\">" + "<merchantAuthentication>" + "  <name>" + gstrLoginName + "</name>" + "  <transactionKey>" + gstrTransactionKey + "</transactionKey>" + "</merchantAuthentication>" + "<customerProfileId>" + AuthorizeProfileID + "</customerProfileId>" + "\r\n" + "<paymentProfile>" + "\r\n" + "  <customerType>individual</customerType>" + "\r\n" + "     <billTo>" + "\r\n" + "        <firstName>" + FirstName + "</firstName>" + "\r\n" + "        <lastName>" + LastName + "</lastName>" + "\r\n" + "        <company>" + CompanyName + "</company>" + "\r\n" + "        <address>" + StreetAddress + "</address>" + "\r\n" + "        <city>" + City + "</city>" + "\r\n" + "        <state>" + State + "</state>" + "\r\n" + "        <zip>" + PostalCode + "</zip>" + "\r\n" + "        <country>" + Country + "</country>" + "\r\n" + "        <phoneNumber>" + PhoneNumber + "</phoneNumber>" + "\r\n" + "     </billTo>" + "\r\n" + "     <payment>" + "\r\n" + "         <creditCard>" + "\r\n" + "             <cardNumber>" + CardNumber + "</cardNumber>" + "\r\n" + "             <expirationDate>" + CardExpireYear + "-" + (CardExpireMonth.Length == 1 ? "0" : string.Empty) + CardExpireMonth + "</expirationDate>" + "\r\n" + "             <cardCode>" + CardCode + "</cardCode>" + "\r\n" + "         </creditCard>" + "\r\n" + "     </payment>" + "\r\n" + "   <customerPaymentProfileId>" + AuthorizePaymentID + "</customerPaymentProfileId>" + "\r\n" + "</paymentProfile>" + "\r\n" + "<validationMode>" + validationMode + "</validationMode>" + "\r\n" + "</updateCustomerPaymentProfileRequest>";
            string objResponse = SepFunctions.Send_XML(gstrApiUrl, strReq, "text/xml");
            var doc = new XmlDocument();
            doc.LoadXml(objResponse);
            var root = doc.DocumentElement;

            string xmlResult = SepFunctions.ParseXML("resultCode", root.OuterXml);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (xmlResult == "Ok")
            {
                SepFunctions.Activity_Write("PAYMENT", UserName + " has successfully updated payment profile on Authorize.Net.", 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
                return true;
            }

            SepFunctions.Activity_Write("PAYMENT", UserName + " has failed to update payment profile on Authorize.Net.", 995, SepFunctions.Session_Invoice_ID(), SepFunctions.Session_User_ID());
            return false;
        }
    }
}