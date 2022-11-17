// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Shipping.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System.IO;
    using System.Xml;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public partial class SepFunctions
    {
        /// <summary>
        /// Queries if a fed ex is enabled.
        /// </summary>
        /// <param name="StoreID">Identifier for the store.</param>
        /// <returns>True if the fed ex is enabled, false if not.</returns>
        public static bool isFedExEnabled(long StoreID)
        {
            if (StoreID > 0)
            {
                var sXML = Load_Store_Config(StoreID);
                if (!string.IsNullOrWhiteSpace(ParseXML("FedExAccountNum", sXML)) && !string.IsNullOrWhiteSpace(ParseXML("FedExMeterNum", sXML)) && !string.IsNullOrWhiteSpace(ParseXML("FedExServiceKey", sXML)) && !string.IsNullOrWhiteSpace(Decrypt(ParseXML("FedExServicePass", sXML))))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Setup(989, "FedExAccountNum")) && !string.IsNullOrWhiteSpace(Setup(989, "FedExMeterNum")) && !string.IsNullOrWhiteSpace(Setup(989, "FedExServiceKey")) && !string.IsNullOrWhiteSpace(Decrypt(Setup(989, "FedExServicePass"))))
            {
                return true;
            }

            return false;
        }

        public static bool isCanadaPostEnabled(long StoreID)
        {
            if (StoreID > 0)
            {
                var sXML = Load_Store_Config(StoreID);
                if (!string.IsNullOrWhiteSpace(ParseXML("CanadaPostUserName", sXML)) && !string.IsNullOrWhiteSpace(ParseXML("CanadaPostPassword", sXML)) && !string.IsNullOrWhiteSpace(ParseXML("CanadaPostCustomerNumber", sXML)))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Setup(989, "CanadaPostUserName")) && !string.IsNullOrWhiteSpace(Setup(989, "CanadaPostPassword")) && !string.IsNullOrWhiteSpace(Setup(989, "CanadaPostCustomerNumber")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Queries if a shipping is enabled.
        /// </summary>
        /// <param name="StoreID">Identifier for the store.</param>
        /// <returns>True if the shipping is enabled, false if not.</returns>
        public static bool isShippingEnabled(long StoreID)
        {
            if (isFedExEnabled(StoreID) || isUPSEnabled(StoreID) || isUSPSEnabled(StoreID) || isCanadaPostEnabled(StoreID))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Queries if the ups is enabled.
        /// </summary>
        /// <param name="StoreID">Identifier for the store.</param>
        /// <returns>True if the ups is enabled, false if not.</returns>
        public static bool isUPSEnabled(long StoreID)
        {
            if (StoreID > 0)
            {
                var sXML = Load_Store_Config(StoreID);
                if (!string.IsNullOrWhiteSpace(ParseXML("UPSAccountNum", sXML)) && !string.IsNullOrWhiteSpace(ParseXML("UPSUserName", sXML)) && !string.IsNullOrWhiteSpace(Decrypt(ParseXML("UPSPassword", sXML))) && !string.IsNullOrWhiteSpace(ParseXML("UPSShipperNum", sXML)))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Setup(989, "UPSAccountNum")) && !string.IsNullOrWhiteSpace(Setup(989, "UPSUserName")) && !string.IsNullOrWhiteSpace(Decrypt(Setup(989, "UPSPassword"))) && !string.IsNullOrWhiteSpace(Setup(989, "UPSShipperNum")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Queries if the usps is enabled.
        /// </summary>
        /// <param name="StoreID">Identifier for the store.</param>
        /// <returns>True if the usps is enabled, false if not.</returns>
        public static bool isUSPSEnabled(long StoreID)
        {
            if (StoreID > 0)
            {
                var sXML = Load_Store_Config(StoreID);
                if (!string.IsNullOrWhiteSpace(ParseXML("USPSUserID", sXML)))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Setup(989, "USPSUserID")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads store configuration.
        /// </summary>
        /// <param name="StoreID">Identifier for the store.</param>
        /// <returns>The store configuration.</returns>
        public static string Load_Store_Config(long StoreID)
        {
            if (File.Exists(GetDirValue("app_data") + "\\settings-store-" + StoreID + ".xml"))
            {
                var doc = new XmlDocument();

                doc.Load(GetDirValue("app_data") + "\\settings-store-" + StoreID + ".xml");

                // Select the book node with the matching attribute value.
                var root = doc.DocumentElement;

                return root.OuterXml;
            }

            return string.Empty;
        }
    }
}