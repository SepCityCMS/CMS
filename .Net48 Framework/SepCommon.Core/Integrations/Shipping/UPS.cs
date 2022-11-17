// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UPS.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// An ups.
    /// </summary>
    public class UPS : IDisposable
    {
        // -V3073
        /// <summary>
        /// True to enable debug mode, false to disable it.
        /// </summary>
        public bool DebugMode = false;

        /// <summary>
        /// The common.
        /// </summary>
        private readonly string m_pickupType;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UPS()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="accessNumber">The access number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="shipperNumber">The shipper number.</param>
        /// <param name="serviceType">Type of the service.</param>
        public UPS(string accessNumber, string userName, string password, string shipperNumber, string serviceType)
        {
            AccessNumber = accessNumber;
            UserName = userName;
            Password = password;
            ShipperNumber = shipperNumber;
            m_pickupType = serviceType;
        }

        /// <summary>
        /// Gets or sets the access number.
        /// </summary>
        /// <value>The access number.</value>
        public string AccessNumber { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the shipper number.
        /// </summary>
        /// <value>The shipper number.</value>
        public string ShipperNumber { get; set; }

        /// <summary>
        /// Gets or sets URI of the document.
        /// </summary>
        /// <value>The URI.</value>
        public string Uri { get; set; } = "https://onlinetools.ups.com/ups.app/xml/Rate";

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Builds ups XML.
        /// </summary>
        /// <param name="weight">.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <param name="shipperZipCode">.</param>
        /// <param name="shipperCountryCode">.</param>
        /// <param name="destinationZipCode">.</param>
        /// <param name="destinationCountryCode">.</param>
        /// <param name="serviceCode">.</param>
        /// <returns>A string.</returns>
        public string buildUPSXml(string weight, string weightType, string shipperZipCode, string shipperCountryCode, string destinationZipCode, string destinationCountryCode, string serviceCode)
        {
            var sb = new StringBuilder();

            sb.Append("<?xml version='1.0'?>");
            sb.Append("\t" + "<AccessRequest xml:lang='en-US'>");
            sb.Append("\t" + "\t" + "<AccessLicenseNumber>").Append(AccessNumber).Append("</AccessLicenseNumber>");
            sb.Append("\t" + "\t" + "<UserId>").Append(UserName).Append("</UserId>");
            sb.Append("\t" + "\t" + "<Password>").Append(Password).Append("</Password>");
            sb.Append("\t" + "</AccessRequest>");
            sb.Append("<?xml version='1.0'?>");
            sb.Append("\t" + "<RatingServiceSelectionRequest xml:lang='en-US'>");
            sb.Append("\t" + "\t" + "<Request>");
            sb.Append("\t" + "\t" + "\t" + "<TransactionReference>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<CustomerContext>Rating and Service</CustomerContext>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<XpciVersion>1.0001</XpciVersion>");
            sb.Append("\t" + "\t" + "\t" + "</TransactionReference>");
            sb.Append("\t" + "\t" + "\t" + "<RequestAction>Rate</RequestAction>");
            sb.Append("\t" + "\t" + "\t" + "<RequestOption>Rate</RequestOption>");
            sb.Append("\t" + "\t" + "</Request>");
            sb.Append("\t" + "\t" + "<PickupType>");
            sb.Append("\t" + "\t" + "\t" + "<Code>").Append(m_pickupType).Append("</Code>");
            sb.Append("\t" + "\t" + "</PickupType>");
            sb.Append("\t" + "\t" + "<Shipment>");
            sb.Append("\t" + "\t" + "\t" + "<Shipper>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<Address>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<PostalCode>").Append(shipperZipCode).Append("</PostalCode>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<CountryCode>").Append(shipperCountryCode).Append("</CountryCode>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "</Address>");
            sb.Append("\t" + "\t" + "\t" + "</Shipper>");
            sb.Append("\t" + "\t" + "\t" + "<ShipTo>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<Address>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<PostalCode>").Append(destinationZipCode).Append("</PostalCode>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<CountryCode>").Append(destinationCountryCode).Append("</CountryCode>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "</Address>");
            sb.Append("\t" + "\t" + "\t" + "</ShipTo>");
            sb.Append("\t" + "\t" + "\t" + "<Service>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<Code>").Append(serviceCode).Append("</Code>");
            sb.Append("\t" + "\t" + "\t" + "</Service>");
            sb.Append("\t" + "\t" + "\t" + "<Package>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<PackagingType>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<Code>02</Code>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<Description>Package</Description>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "</PackagingType>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<Description>Rate Shopping</Description>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "<PackageWeight>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "    <UnitOfMeasurement>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "    <Code>").Append(weightType).Append("</Code>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "    </UnitOfMeasurement>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "\t" + "<Weight>").Append(weight).Append("</Weight>");
            sb.Append("\t" + "\t" + "\t" + "\t" + "</PackageWeight>");
            sb.Append("\t" + "\t" + "\t" + "</Package>");
            sb.Append("\t" + "\t" + "\t" + "<ShipmentServiceOptions/>");
            sb.Append("\t" + "\t" + "</Shipment>");
            sb.Append("</RatingServiceSelectionRequest>");

            return SepCore.Strings.ToString(sb);
        }

        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Function returns the rate.
        /// </summary>
        /// <param name="weight">.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <param name="shipperZipCode">.</param>
        /// <param name="shipperCountryCode">.</param>
        /// <param name="destinationZipCode">.</param>
        /// <param name="destinationCountryCode">.</param>
        /// <param name="serviceCode">.</param>
        /// <returns>The rate.</returns>
        public decimal GetRate(string weight, string weightType, string shipperZipCode, string shipperCountryCode, string destinationZipCode, string destinationCountryCode, string serviceCode)
        {
            if (DebugMode)
            {
                SepFunctions.Debug_Log("UPS URL: " + Uri);
            }

            var req = WebRequest.Create(Uri);
            WebResponse rsp = null;
            req.Method = "POST";
            req.ContentType = "text/xml";
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.WriteLine(buildUPSXml(weight, weightType, shipperZipCode, shipperCountryCode, destinationZipCode, destinationCountryCode, serviceCode));
                if (DebugMode)
                {
                    SepFunctions.Debug_Log("UPS Post Data: " + buildUPSXml(weight, weightType, shipperZipCode, shipperCountryCode, destinationZipCode, destinationCountryCode, serviceCode));
                }
            }
            rsp = req.GetResponse();

            // Get the Posted Data
            var reader = new StreamReader(rsp.GetResponseStream());
            var rawXml = reader.ReadToEnd();
            reader.Dispose();
            var xml = new XmlDocument();
            xml.LoadXml(rawXml);
            if (DebugMode)
            {
                SepFunctions.Debug_Log("UPS Response: " + rawXml);
            }

            decimal rate = 0;

            // Use XPath to Navigate the Document
            var status = Convert.ToInt32(xml.SelectSingleNode("/RatingServiceSelectionResponse/Response/ResponseStatusCode").InnerText);
            if (status == 1)
            {
                rate = Convert.ToDecimal(xml.SelectSingleNode("/RatingServiceSelectionResponse/RatedShipment/TotalCharges/MonetaryValue").InnerText);
            }

            xml = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return rate;
        }

        /// <summary>
        /// IDisposable.
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
    }
}