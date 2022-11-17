// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Affiliate.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server
{
    using SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Identifier for the get module.
        /// </summary>
        private const int GetModuleID = 39;

        /// <summary>
        /// Pub affiliate five level sales.
        /// </summary>
        /// <param name="sAffiliateID">.</param>
        /// <param name="AffiliatePayout">[in,out].</param>
        /// <param name="intLevel">.</param>
        /// <returns>A double.</returns>
        public static double PUB_Affiliate_Five_Level_Sales(long sAffiliateID, ref double AffiliatePayout, long intLevel)
        {
            double intTotalPrice = 0;
            double iLevel1Price = 0;
            double iLevel2Price = 0;

            // Get Level 1 Affiliates
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT AffiliateID,UserID FROM Members WHERE ReferralID='" + FixWord(Strings.ToString(sAffiliateID)) + "' AND Status=1", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            if (intLevel == 0 || intLevel > 0)
                            {
                                iLevel1Price = iLevel1Price + PUB_Calculate_Invoices(sAffiliateID, openNull(RS["UserID"]));
                            }

                            // Get Levelt 2 Affiliates
                            if ((intLevel == 0 || intLevel > 1) && toDouble(Setup(GetModuleID, "AffiliateLVL2")) > 0)
                            {
                                using (var cmd2 = new SqlCommand("SELECT AffiliateID,UserID FROM Members WHERE ReferralID='" + FixWord(openNull(RS["AffiliateID"])) + "' AND Status=1", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        while (RS2.Read())
                                        {
                                            iLevel2Price = iLevel2Price + PUB_Calculate_Invoices(sAffiliateID, openNull(RS2["UserID"]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (toDouble(Setup(GetModuleID, "AffiliateLVL1")) > 0 && iLevel1Price > 0 && (intLevel == 0 || intLevel == 1))
            {
                intTotalPrice = intTotalPrice + iLevel1Price / 100 * toDouble(Setup(GetModuleID, "AffiliateLVL1"));
            }

            if (toDouble(Setup(GetModuleID, "AffiliateLVL2")) > 0 && iLevel2Price > 0 && (intLevel == 0 || intLevel == 2))
            {
                intTotalPrice = intTotalPrice + iLevel2Price / 100 * toDouble(Setup(GetModuleID, "AffiliateLVL2"));
            }

            AffiliatePayout = intTotalPrice;

            intTotalPrice = 0;

            if (toDouble(Setup(GetModuleID, "AffiliateLVL1")) > 0 && iLevel1Price > 0 && (intLevel == 0 || intLevel == 1))
            {
                intTotalPrice = intTotalPrice + iLevel1Price;
            }

            if (toDouble(Setup(GetModuleID, "AffiliateLVL2")) > 0 && iLevel2Price > 0 && (intLevel == 0 || intLevel == 2))
            {
                intTotalPrice = intTotalPrice + iLevel2Price;
            }

            return intTotalPrice;
        }

        /// <summary>
        /// Pub affiliate join keys.
        /// </summary>
        /// <param name="sPrefix">(Optional) The prefix.</param>
        /// <returns>A string.</returns>
        public static string PUB_Affiliate_JoinKeys(string sPrefix = "")
        {
            var str = new StringBuilder();
            long aa = 0;
            var sJoinKeys = Security("AffiliateJoin");

            if (!string.IsNullOrWhiteSpace(sJoinKeys))
            {
                string[] arrAffiliate = Strings.Split(sJoinKeys, ",");

                str.Append(" AND (");
                if (arrAffiliate != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAffiliate); i++)
                    {
                        if (!string.IsNullOrWhiteSpace(arrAffiliate[i]))
                        {
                            aa = aa + 1;
                            if (aa > 1)
                            {
                                str.Append(" OR ");
                            }

                            str.Append(sPrefix + "AccessKeys LIKE '%" + arrAffiliate[i] + "%'");
                        }
                    }
                }

                str.Append(")");
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Pub calculate invoices.
        /// </summary>
        /// <param name="sAffiliateID">.</param>
        /// <param name="sUserID">.</param>
        /// <returns>A double.</returns>
        public static double PUB_Calculate_Invoices(long sAffiliateID, string sUserID)
        {
            if (string.IsNullOrWhiteSpace(sUserID) || sAffiliateID == 0)
            {
                return 0;
            }

            double iPrice = 0;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT IP.UnitPrice,IP.RecurringPrice,IP.AffiliateUnitPrice,IP.AffiliateRecurringPrice,IP.Handling,IP.Quantity FROM Invoices_Products AS IP,Invoices AS INV WHERE INV.InvoiceID=IP.InvoiceID AND INV.UserID='" + FixWord(sUserID) + "' AND INV.Status > '0' AND INV.InvoiceID NOT IN (SELECT InvoiceID FROM AffiliatePaid WHERE InvoiceID=INV.InvoiceID AND AffiliateID='" + sAffiliateID + "' AND DatePaid > INV.DatePaid)", conn))
                {
                    using (SqlDataReader CalcRS = cmd.ExecuteReader())
                    {
                        while (CalcRS.Read())
                        {
                            iPrice = (Format_Double(openNull(CalcRS["UnitPrice"])) + Format_Double(openNull(CalcRS["RecurringPrice"])) + Format_Double(openNull(CalcRS["Handling"]))) * Format_Double(openNull(CalcRS["Quantity"]));
                            if (Math.Abs(iPrice) > Math.Abs((Format_Double(openNull(CalcRS["AffiliateUnitPrice"])) + Format_Double(openNull(CalcRS["AffiliateRecurringPrice"])) + Format_Double(openNull(CalcRS["Handling"]))) * Format_Double(openNull(CalcRS["Quantity"]))) && Math.Abs(Format_Double(openNull(CalcRS["AffiliateUnitPrice"]))) > 0 && Math.Abs(Format_Double(openNull(CalcRS["AffiliateRecurringPrice"]))) > 0)
                            {
                                iPrice = (Format_Double(openNull(CalcRS["AffiliateUnitPrice"])) + Format_Double(openNull(CalcRS["AffiliateRecurringPrice"])) + Format_Double(openNull(CalcRS["Handling"]))) * Format_Double(openNull(CalcRS["Quantity"]));
                            }
                        }
                    }
                }
            }

            return iPrice;
        }
    }
}