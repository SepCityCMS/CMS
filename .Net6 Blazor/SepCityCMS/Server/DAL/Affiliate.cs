// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Affiliate.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Affiliate.
    /// </summary>
    public static class Affiliate
    {
        /// <summary>
        /// Affiliates the reset.
        /// </summary>
        /// <param name="AffiliateIDs">The affiliate i ds.</param>
        /// <returns>System.String.</returns>
        public static string Affiliate_Reset(string AffiliateIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                double sAmountPaid = 0;
                double AffiliatePayout = 0;

                var arrAffiliateIDs = Strings.Split(AffiliateIDs, ",");

                if (arrAffiliateIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAffiliateIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Members SET AffiliatePaid=@AffiliatePaid WHERE AffiliateID=@AffiliateID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AffiliateID", arrAffiliateIDs[i]);
                            cmd.Parameters.AddWithValue("@AffiliatePaid", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        sAmountPaid = SepFunctions.PUB_Affiliate_Five_Level_Sales(SepFunctions.toLong(arrAffiliateIDs[i]), ref AffiliatePayout, 0);

                        // Get Level 1 Affiliates
                        using (var cmd = new SqlCommand("SELECT INV.InvoiceID,M.AffiliateID FROM Members AS M,Invoices AS INV WHERE INV.AffiliateID=M.AffiliateID AND INV.Status > 0 AND M.ReferralID='" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "' AND M.Status=1" + SepFunctions.Affiliate_JoinKeys() + " AND INV.InvoiceID NOT IN (SELECT InvoiceID FROM AffiliatePaid WHERE InvoiceID=INV.InvoiceID AND AffiliateID='" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "')", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    using (var cmd12 = new SqlCommand("INSERT INTO AffiliatePaid (PaidID,AffiliateID,InvoiceID,DatePaid,AmountPaid) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "','" + SepFunctions.openNull(RS["InvoiceID"]) + "','" + SepFunctions.FixWord(Strings.ToString(DateTime.Now)) + "','" + SepFunctions.FixWord(Strings.ToString(sAmountPaid)) + "')", conn))
                                    {
                                        cmd12.ExecuteNonQuery();
                                    }

                                    // Get Levelt 2 Affiliates
                                    using (var cmd2 = new SqlCommand("SELECT INV.InvoiceID,M.AffiliateID FROM Members AS M,Invoices AS INV WHERE INV.AffiliateID=M.AffiliateID AND INV.Status > 0 AND M.ReferralID='" + SepFunctions.FixWord(SepFunctions.openNull(RS["AffiliateID"])) + "' AND M.Status=1" + SepFunctions.Affiliate_JoinKeys() + " AND INV.InvoiceID NOT IN (SELECT InvoiceID FROM AffiliatePaid WHERE InvoiceID=INV.InvoiceID AND AffiliateID='" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "')", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            while (RS2.Read())
                                            {
                                                using (var cmd12 = new SqlCommand("INSERT INTO AffiliatePaid (PaidID,AffiliateID,InvoiceID,DatePaid,AmountPaid) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "','" + SepFunctions.openNull(RS2["InvoiceID"]) + "','" + SepFunctions.FixWord(Strings.ToString(DateTime.Now)) + "','" + SepFunctions.FixWord(Strings.ToString(sAmountPaid)) + "')", conn))
                                                {
                                                    cmd12.ExecuteNonQuery();
                                                }

                                                // Get Levelt 3 Affiliates
                                                using (var cmd3 = new SqlCommand("SELECT INV.InvoiceID,M.AffiliateID FROM Members AS M,Invoices AS INV WHERE INV.AffiliateID=M.AffiliateID AND INV.Status > 0 AND M.ReferralID='" + SepFunctions.FixWord(SepFunctions.openNull(RS2["AffiliateID"])) + "' AND M.Status=1" + SepFunctions.Affiliate_JoinKeys() + " AND INV.InvoiceID NOT IN (SELECT InvoiceID FROM AffiliatePaid WHERE InvoiceID=INV.InvoiceID AND AffiliateID='" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "')", conn))
                                                {
                                                    using (SqlDataReader RS3 = cmd3.ExecuteReader())
                                                    {
                                                        while (RS3.Read())
                                                        {
                                                            using (var cmd12 = new SqlCommand("INSERT INTO AffiliatePaid (PaidID,AffiliateID,InvoiceID,DatePaid,AmountPaid) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "','" + SepFunctions.openNull(RS3["InvoiceID"]) + "','" + SepFunctions.FixWord(Strings.ToString(DateTime.Now)) + "','" + SepFunctions.FixWord(Strings.ToString(sAmountPaid)) + "')", conn))
                                                            {
                                                                cmd12.ExecuteNonQuery();
                                                            }

                                                            // Get Levelt 4 Affiliates
                                                            using (var cmd4 = new SqlCommand("SELECT INV.InvoiceID,M.AffiliateID FROM Members AS M,Invoices AS INV WHERE INV.AffiliateID=M.AffiliateID AND INV.Status > 0 AND M.ReferralID='" + SepFunctions.FixWord(SepFunctions.openNull(RS3["AffiliateID"])) + "' AND M.Status=1" + SepFunctions.Affiliate_JoinKeys() + " AND INV.InvoiceID NOT IN (SELECT InvoiceID FROM AffiliatePaid WHERE InvoiceID=INV.InvoiceID AND AffiliateID='" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "')", conn))
                                                            {
                                                                using (SqlDataReader RS4 = cmd4.ExecuteReader())
                                                                {
                                                                    while (RS4.Read())
                                                                    {
                                                                        using (var cmd12 = new SqlCommand("INSERT INTO AffiliatePaid (PaidID,AffiliateID,InvoiceID,DatePaid,AmountPaid) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "','" + SepFunctions.openNull(RS4["InvoiceID"]) + "','" + SepFunctions.FixWord(Strings.ToString(DateTime.Now)) + "','" + SepFunctions.FixWord(Strings.ToString(sAmountPaid)) + "')", conn))
                                                                        {
                                                                            cmd12.ExecuteNonQuery();
                                                                        }

                                                                        // Get Levelt 5 Affiliates
                                                                        using (var cmd5 = new SqlCommand("SELECT INV.InvoiceID,M.AffiliateID FROM Members AS M,Invoices AS INV WHERE INV.AffiliateID=M.AffiliateID AND INV.Status > 0 AND M.ReferralID='" + SepFunctions.FixWord(SepFunctions.openNull(RS4["AffiliateID"])) + "' AND M.Status=1" + SepFunctions.Affiliate_JoinKeys() + " AND INV.InvoiceID NOT IN (SELECT InvoiceID FROM AffiliatePaid WHERE InvoiceID=INV.InvoiceID AND AffiliateID='" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "')", conn))
                                                                        {
                                                                            using (SqlDataReader RS5 = cmd5.ExecuteReader())
                                                                            {
                                                                                while (RS5.Read())
                                                                                {
                                                                                    using (var cmd12 = new SqlCommand("INSERT INTO AffiliatePaid (PaidID,AffiliateID,InvoiceID,DatePaid,AmountPaid) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.FixWord(arrAffiliateIDs[i]) + "','" + SepFunctions.openNull(RS5["InvoiceID"]) + "','" + SepFunctions.FixWord(Strings.ToString(DateTime.Now)) + "','" + SepFunctions.FixWord(Strings.ToString(sAmountPaid)) + "')", conn))
                                                                                    {
                                                                                        cmd12.ExecuteNonQuery();
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Affiliate(s) has been successfully reset.");

            return sReturn;
        }

        /// <summary>
        /// Affiliates the totals.
        /// </summary>
        /// <returns>List&lt;Models.AffiliateDownline&gt;.</returns>
        public static List<AffiliateDownline> AffiliateTotals()
        {
            var lData = new List<AffiliateDownline>();

            var sAffiliateID = SepFunctions.toLong(SepFunctions.GetUserInformation("AffiliateID"));

            double TotalVolume1 = 0;
            double TotalVolume2 = 0;

            double TotalEarnings1 = 0;
            double TotalEarnings2 = 0;

            TotalVolume1 = SepFunctions.Format_Double(Strings.ToString(SepFunctions.PUB_Affiliate_Five_Level_Sales(sAffiliateID, ref TotalEarnings1, 1)));
            TotalVolume2 = SepFunctions.Format_Double(Strings.ToString(SepFunctions.PUB_Affiliate_Five_Level_Sales(sAffiliateID, ref TotalEarnings2, 2)));

            var dData = new Models.AffiliateDownline();

            // Populate series data
            if (SepFunctions.toLong(SepFunctions.Setup(39, "AffiliateLVL1")) != 0)
            {
                dData.Level = "Level 1";
                dData.TotalEarnings = Math.Round(TotalEarnings1, 0);
                dData.TotalVolume = Math.Round(TotalVolume1, 0);
            }

            if (SepFunctions.toLong(SepFunctions.Setup(39, "AffiliateLVL2")) != 0)
            {
                dData.Level = "Level 2";
                dData.TotalEarnings = Math.Round(TotalEarnings2, 0);
                dData.TotalVolume = Math.Round(TotalVolume2, 0);
            }

            lData.Add(dData);

            return lData;
        }

        /// <summary>
        /// Gets the affiliate downline.
        /// </summary>
        /// <param name="AffiliateID">The affiliate identifier.</param>
        /// <param name="Level">The level.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns>List&lt;Models.AffiliateDownline&gt;.</returns>
        public static List<AffiliateDownline> GetAffiliateDownline(long AffiliateID, int Level, string SortExpression = "Username", string SortDirection = "ASC")
        {
            var lAffiliateDownline = new List<AffiliateDownline>();

            if (AffiliateID == 0)
            {
                return lAffiliateDownline;
            }

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Username";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT AffiliateID,ReferralID,Username,FirstName,LastName,EmailAddress,UserID,CreateDate,(SELECT TOP 1 AffiliateID FROM Members WHERE ReferralID=M.AffiliateID AND Status=1" + SepFunctions.PUB_Affiliate_JoinKeys() + ") AS HasLevels,(SELECT TOP 1 ReferralID FROM Members WHERE AffiliateID=M.ReferralID AND Status=1" + SepFunctions.PUB_Affiliate_JoinKeys() + ") AS PrevAffiliateID FROM Members AS M WHERE ReferralID='" + SepFunctions.FixWord(Strings.ToString(AffiliateID)) + "' AND Status=1" + SepFunctions.PUB_Affiliate_JoinKeys() + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dAffiliateDownline = new Models.AffiliateDownline { AffiliateID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AffiliateID"])) };
                    dAffiliateDownline.PrevAffiliateID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PrevAffiliateID"]));
                    dAffiliateDownline.Level = Strings.ToString(Level);
                    dAffiliateDownline.HasLevels = SepFunctions.openNull(ds.Tables[0].Rows[i]["HasLevels"]);
                    dAffiliateDownline.ReferralID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ReferralID"]));
                    dAffiliateDownline.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dAffiliateDownline.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dAffiliateDownline.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dAffiliateDownline.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dAffiliateDownline.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dAffiliateDownline.DateJoined = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["CreateDate"]));
                    lAffiliateDownline.Add(dAffiliateDownline);
                }
            }

            return lAffiliateDownline;
        }

        /// <summary>
        /// Gets the affiliate history.
        /// </summary>
        /// <param name="AffiliateID">The affiliate identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns>List&lt;Models.AffiliateHistory&gt;.</returns>
        public static List<AffiliateHistory> GetAffiliateHistory(long AffiliateID, string SortExpression = "DatePaid", string SortDirection = "DESC")
        {
            var lAffiliateHistory = new List<AffiliateHistory>();

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePaid";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT DISTINCT AffiliateID,DatePaid,AmountPaid FROM AffiliatePaid WHERE AffiliateID='" + SepFunctions.FixWord(Strings.ToString(AffiliateID)) + "' ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dAffiliateHistory = new Models.AffiliateHistory { AffiliateID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AffiliateID"])) };
                    dAffiliateHistory.DatePaid = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePaid"]));
                    dAffiliateHistory.AmountPaid = SepFunctions.openNull(ds.Tables[0].Rows[i]["AmountPaid"]);
                    lAffiliateHistory.Add(dAffiliateHistory);
                }
            }

            return lAffiliateHistory;
        }

        /// <summary>
        /// Gets the affiliate images.
        /// </summary>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>List&lt;Models.AffiliateImages&gt;.</returns>
        public static List<AffiliateImages> GetAffiliateImages(long PortalID = -1)
        {
            var lAffiliateImages = new List<AffiliateImages>();

            var wClause = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (PortalID >= 0)
            {
                wClause = " AND (PortalID=" + PortalID + " OR PortalID = -1)";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT UploadID,UniqueID,FileName FROM Uploads WHERE ModuleID='39'" + wClause, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dAffiliateImages = new Models.AffiliateImages { ImageID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])) };
                    dAffiliateImages.UniqueID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]));
                    dAffiliateImages.FileName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FileName"]);
                    dAffiliateImages.ImageURL = sImageFolder + "spadmin/show_image.aspx?ModuleID=39&ImageID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        dAffiliateImages.HTMLCode = "<a href=\"" + SepFunctions.GetMasterDomain(true) + "default.aspx?AffiliateID=" + SepFunctions.GetUserInformation("AffiliateID") + "\"><img src=\"" + SepFunctions.GetMasterDomain(true) + "spadmin/show_image.aspx?ModuleID=39&ImageID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]) + "\" alt=\"Affiliate Image\" title=\"Affiliate Image\" border=\"0\" /></a>";
                    }

                    lAffiliateImages.Add(dAffiliateImages);
                }
            }

            return lAffiliateImages;
        }

        /// <summary>
        /// Gets the affiliate members.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.AffiliateMembers&gt;.</returns>
        public static List<AffiliateMembers> GetAffiliateMembers(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "")
        {
            var lAffiliateMembers = new List<AffiliateMembers>();

            var wClause = string.Empty;

            double AffiliatePayout = 0;
            double RevenueGenerated = 0;

            var dbConnection = SepFunctions.Database_Connection();
            var level1Percent = SepFunctions.toDouble(SepFunctions.Setup(39, "AffiliateLVL1"));
            var level2Percent = SepFunctions.toDouble(SepFunctions.Setup(39, "AffiliateLVL2"));

            double iLevel1Price = 0;
            double iLevel2Price = 0;

            var SqlStr = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "root.Username";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (root.Username LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR root.FirstName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR root.LastName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR root.EmailAddress LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            SqlStr += "select root.UserName  as UserName ";
            SqlStr += "     , root.UserID As UserID ";
            SqlStr += "     , root.AffiliateID As AffiliateID ";
            SqlStr += "     , root.PayPal as PayPal ";
            SqlStr += "     , root.WebsiteURL As WebsiteURL ";
            SqlStr += "     , root.AffiliatePaid as AffiliatePaid ";
            SqlStr += "     , down1.AffiliateID As AffiliateID1 ";
            SqlStr += "     , down2.AffiliateID as AffiliateID2 ";
            SqlStr += "  , (SELECT (SUM(CAST(AffiliateUnitPrice as decimal(8,2))) + SUM(CAST(AffiliateRecurringPrice as decimal(8,2))) + SUM(CAST(Handling as decimal(8,2))) * SUM(Quantity)) AS AffiliatePrice1 FROM Invoices,Invoices_Products WHERE Invoices_Products.InvoiceID=Invoices.InvoiceID And Invoices.AffiliateID=down1.AffiliateID And Invoices.Status=1 AND Invoices_Products.ExcludeAffiliate='0') AS AffiliatePrice1 ";
            SqlStr += "  , (Select (SUM(CAST(AffiliateUnitPrice As Decimal(8,2))) + SUM(CAST(AffiliateRecurringPrice As Decimal(8,2))) + SUM(CAST(Handling as decimal(8,2))) * SUM(Quantity)) As AffiliatePrice2 FROM Invoices,Invoices_Products WHERE Invoices_Products.InvoiceID=Invoices.InvoiceID And Invoices.AffiliateID=down2.AffiliateID And Invoices.Status=1 AND Invoices_Products.ExcludeAffiliate='0') AS AffiliatePrice2 ";
            SqlStr += "  From Members As root ";
            SqlStr += "Left outer ";
            SqlStr += "  Join members As down1 ";
            SqlStr += "    On down1.AffiliateID = root.ReferralID And down1.AffiliateID IN (SELECT AffiliateID FROM Invoices WHERE AffiliateID=down1.AffiliateID And Status=1) ";
            SqlStr += "Left outer ";
            SqlStr += "  Join members as down2 ";
            SqlStr += "    On down2.AffiliateID = down1.ReferralID And down2.AffiliateID In (Select AffiliateID FROM Invoices WHERE AffiliateID=down2.AffiliateID And Status=1) ";
            SqlStr += " where root.AffiliateID <> '' AND root.Status <> -1";

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(dbConnection))
                {
                    using (var cmd = new SqlCommand(SqlStr + SepFunctions.Affiliate_JoinKeys("root.") + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dAffiliateMembers = new Models.AffiliateMembers { AffiliateID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AffiliateID"])) };
                    dAffiliateMembers.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dAffiliateMembers.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dAffiliateMembers.PayPal = SepFunctions.openNull(ds.Tables[0].Rows[i]["PayPal"]);
                    dAffiliateMembers.WebsiteURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["WebsiteURL"]);
                    dAffiliateMembers.AffiliatePaid = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["AffiliatePaid"]));
                    dAffiliateMembers.PaymentFrom = SepFunctions.Setup(992, "WebSiteName");

                    AffiliatePayout = 0;
                    iLevel1Price = SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["AffiliatePrice1"]));
                    iLevel2Price = SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["AffiliatePrice2"]));

                    RevenueGenerated = iLevel1Price + iLevel2Price;

                    if (level1Percent > 0 && iLevel1Price > 0)
                    {
                        AffiliatePayout += iLevel1Price / 100 * level1Percent;
                    }

                    if (level2Percent > 0 && iLevel2Price > 0)
                    {
                        AffiliatePayout += iLevel1Price / 100 * level2Percent; // -V3127
                    }

                    dAffiliateMembers.Payout = SepFunctions.Format_Currency(AffiliatePayout);
                    dAffiliateMembers.RevenueGenerated = SepFunctions.Format_Currency(RevenueGenerated);

                    lAffiliateMembers.Add(dAffiliateMembers);
                }
            }

            return lAffiliateMembers;
        }

        /// <summary>
        /// Images the delete.
        /// </summary>
        /// <param name="ImageIDs">The image i ds.</param>
        /// <returns>System.String.</returns>
        public static string Image_Delete(string ImageIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrImageIDs = Strings.Split(ImageIDs, ",");

                if (arrImageIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrImageIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UploadID=@ImageID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ImageID", arrImageIDs[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Image(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Images the get.
        /// </summary>
        /// <param name="ImageID">The image identifier.</param>
        /// <returns>Models.AffiliateImages.</returns>
        public static AffiliateImages Image_Get(long ImageID)
        {
            var returnXML = new Models.AffiliateImages();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UploadID,FileData,ContentType,PortalID FROM Uploads WHERE UploadID=@ImageID", conn))
                {
                    cmd.Parameters.AddWithValue("@ImageID", ImageID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ImageID = SepFunctions.toLong(SepFunctions.openNull(RS["UploadID"]));
                            returnXML.ImageData = Strings.ToString(Information.IsDBNull(RS["FileData"]) ? string.Empty : SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"])));

                            returnXML.ImageType = SepFunctions.openNull(RS["ContentType"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Images the save.
        /// </summary>
        /// <param name="ImageID">The image identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="ImageData">The image data.</param>
        /// <param name="ImageType">Type of the image.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Image_Save(long ImageID, string UserID, int ModuleID, string FileName, string ImageData, string ImageType, long PortalID)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ImageID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE UploadID=@ImageID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ImageID", ImageID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    ImageID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Uploads SET FileName=@FileName, FileData=@FileData, ContentType=@ContentType, PortalID=@PortalID WHERE UploadID=@ImageID";
                }
                else
                {
                    SqlStr = "INSERT INTO Uploads (UploadID, UniqueID, ModuleID, FileName, UserID, FileData, ContentType, PortalID, Approved, isTemp, DatePosted) VALUES (@ImageID, @UniqueID, @ModuleID, @FileName, @UserID, @FileData, @ContentType, @PortalID, '1', '0', @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ImageID", ImageID);
                    cmd.Parameters.AddWithValue("@UniqueID", ImageID);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    cmd.Parameters.AddWithValue("@FileName", FileName);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    if (!string.IsNullOrWhiteSpace(ImageData))
                    {
                        cmd.Parameters.AddWithValue("@FileData", SepFunctions.StringToBytes(SepFunctions.Base64Decode(ImageData)));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FileData", string.Empty);
                    }

                    cmd.Parameters.AddWithValue("@ContentType", ImageType);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Affiliate image has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Affiliate image has been successfully added.");

            return sReturn;
        }
    }
}