// ***********************************************************************
// Assembly         : SepCommon.Core
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

namespace SepCityCMS.Server.Integrations.Shipping
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
                if (!string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("FedExAccountNum", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("FedExMeterNum", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("FedExServiceKey", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Decrypt(Server.SepFunctions.ParseXML("FedExServicePass", sXML))))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "FedExAccountNum")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "FedExMeterNum")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "FedExServiceKey")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Decrypt(Server.SepFunctions.Setup(989, "FedExServicePass"))))
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
                if (!string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("CanadaPostUserName", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("CanadaPostPassword", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("CanadaPostCustomerNumber", sXML)))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "CanadaPostUserName")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "CanadaPostPassword")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "CanadaPostCustomerNumber")))
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
                if (!string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("UPSAccountNum", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("UPSUserName", sXML)) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Decrypt(Server.SepFunctions.ParseXML("UPSPassword", sXML))) && !string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("UPSShipperNum", sXML)))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "UPSAccountNum")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "UPSUserName")) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Decrypt(Server.SepFunctions.Setup(989, "UPSPassword"))) && !string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "UPSShipperNum")))
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
                if (!string.IsNullOrWhiteSpace(Server.SepFunctions.ParseXML("USPSUserID", sXML)))
                {
                    return true;
                }

                return false;
            }

            if (!string.IsNullOrWhiteSpace(Server.SepFunctions.Setup(989, "USPSUserID")))
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
            if (File.Exists(Server.SepFunctions.GetDirValue("app_data") + "\\settings-store-" + StoreID + ".xml"))
            {
                var doc = new XmlDocument();

                doc.Load(Server.SepFunctions.GetDirValue("app_data") + "\\settings-store-" + StoreID + ".xml");

                // Select the book node with the matching attribute value.
                var root = doc.DocumentElement;

                return root.OuterXml;
            }

            return string.Empty;
        }
    }
}