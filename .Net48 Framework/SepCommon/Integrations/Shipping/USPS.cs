// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="USPS.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// An usps.
    /// </summary>
    public class USPS : IDisposable
    {
        // -V3073
        /// <summary>
        /// True to enable debug mode, false to disable it.
        /// </summary>
        public bool DebugMode = false;

        /// <summary>
        /// The common.
        /// </summary>
        private readonly string m_uri = "http://production.shippingapis.com/ShippingAPI.dll?API=RateV4";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userID">The identifier of the user.</param>
        public USPS(string userID)
        {
            UserID = userID;
        }

        /// <summary>
        /// Gets or sets the shipping XML.
        /// </summary>
        /// <value>The shipping XML.</value>
        public string SetShippingXML { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user.
        /// </summary>
        /// <value>The identifier of the user.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="PackageNumber">The package number.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <param name="shipperZipCode">The shipper zip code.</param>
        /// <param name="destinationZipCode">Destination zip code.</param>
        /// <param name="serviceType">Type of the service.</param>
        public void AddItem(int PackageNumber, string weight, string weightType, string shipperZipCode, string destinationZipCode, string serviceType)
        {
            var sb = new StringBuilder();

            if (PackageNumber == 0)
            {
                PackageNumber = 1;
            }

            sb.Append("<Package ID=\"" + SepFunctions.NumberAddOrdinal(PackageNumber) + "\">");
            sb.Append("<Service>" + serviceType + "</Service>");
            sb.Append("<FirstClassMailType>LETTER</FirstClassMailType>");
            if (DebugMode)
            {
                sb.Append("<ZipOrigination>44106</ZipOrigination>");
                sb.Append("<ZipDestination>20770</ZipDestination>");
            }
            else
            {
                sb.Append("<ZipOrigination>" + shipperZipCode + "</ZipOrigination>");
                sb.Append("<ZipDestination>" + destinationZipCode + "</ZipDestination>");
            }

            if (weightType == "LBS")
            {
                sb.Append("<Pounds>" + weight + "</Pounds>");
                sb.Append("<Ounces>0</Ounces>");
            }
            else
            {
                sb.Append("<Pounds>0</Pounds>");
                sb.Append("<Ounces>" + weight + "</Ounces>");
            }

            sb.Append("<Container/>");
            sb.Append("<Size>REGULAR</Size>");
            sb.Append("<Machinable>true</Machinable>");
            sb.Append("</Package>");

            SetShippingXML += SepCore.Strings.ToString(sb);
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
        /// Gets a rate.
        /// </summary>
        /// <param name="sXML">The XML.</param>
        /// <returns>The rate.</returns>
        public decimal GetRate(string sXML)
        {
            if (DebugMode)
            {
                SepFunctions.Debug_Log("USPS URL: " + m_uri + "&XML=" + SepFunctions.UrlEncode(sXML));
            }

            var req = WebRequest.Create(m_uri + "&XML=" + SepFunctions.UrlEncode(sXML));
            req.Method = "POST";
            req.ContentType = "text/xml";
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.WriteLine(sXML);
            }
            WebResponse rsp = req.GetResponse();

            // Get the Posted Data
            var reader = new StreamReader(rsp.GetResponseStream());
            var rawXml = reader.ReadToEnd();
            reader.Dispose();
            var xml = new XmlDocument();
            xml.LoadXml(rawXml);

            if (DebugMode)
            {
                SepFunctions.Debug_Log("USPS Response: " + rawXml);
            }

            decimal rate = 0;

            if (xml.SelectSingleNode("/Error/Number") == null)
            {
                rate = SepFunctions.toDecimal(xml.SelectSingleNode("/RateV4Response/Package/Postage/Rate").InnerText);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return rate;
        }

        /// <summary>
        /// Gets shipping XML.
        /// </summary>
        /// <returns>The shipping XML.</returns>
        public string GetShippingXML()
        {
            var sb = new StringBuilder();
            var packageXML = SetShippingXML;

            sb.Append("<RateV4Request USERID=\"" + UserID + "\">");
            sb.Append("<Revision/>");
            sb.Append(packageXML);
            sb.Append("</RateV4Request>");

            if (DebugMode)
            {
                SepFunctions.Debug_Log(SepCore.Strings.ToString(sb));
            }

            return SepCore.Strings.ToString(sb);
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