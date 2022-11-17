// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Auctions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class Auctions.
    /// </summary>
    public static class Auctions
    {
        /// <summary>
        /// Auctions the delete.
        /// </summary>
        /// <param name="AdIDs">The ad i ds.</param>
        /// <returns>System.String.</returns>
        public static string Auction_Delete(string AdIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAdIDs = Strings.Split(AdIDs, ",");

                if (arrAdIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAdIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE AuctionAds SET Status='-1', DateDeleted=@DateDeleted WHERE AdID=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", arrAdIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE AuctionFeedback SET Status='-1', DateDeleted=@DateDeleted WHERE AdID=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", arrAdIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(31, arrAdIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Ads(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Auctions the get.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.AuctionAds.</returns>
        public static AuctionAds Auction_Get(long AdID, long ChangeID = 0)
        {
            var returnXML = new Models.AuctionAds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE AuctionAds SET Visits=Visits+1 WHERE LinkID=@AdID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM AuctionAds WHERE AdID=@AdID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var logData = string.Empty;
                            if (ChangeID > 0)
                            {
                                logData = SepFunctions.Get_Change_Log(ChangeID);
                            }

                            if (ChangeID > 0 && !string.IsNullOrWhiteSpace(logData))
                            {
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    var fieldName = RS.GetName(i);
                                    var fieldType = RS.GetFieldType(i);
                                    var fieldValue = SepFunctions.openNull(RS[i]);
                                    if (Strings.InStr(logData, "<" + fieldName + ">") > 0)
                                    {
                                        var xmlNode = SepFunctions.ParseXML(fieldName, logData);
                                        if (!string.IsNullOrWhiteSpace(xmlNode))
                                        {
                                            fieldValue = xmlNode;
                                        }
                                    }

                                    var prop = returnXML.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        switch (fieldType.Name)
                                        {
                                            case "Double":
                                                prop.SetValue(returnXML, SepFunctions.toLong(fieldValue), null);
                                                break;

                                            case "DateTime":
                                                prop.SetValue(returnXML, SepFunctions.toDate(fieldValue), null);
                                                break;

                                            case "Boolean":
                                                prop.SetValue(returnXML, SepFunctions.toBoolean(fieldValue), null);
                                                break;

                                            case "Int32":
                                                prop.SetValue(returnXML, SepFunctions.toInt(fieldValue), null);
                                                break;

                                            case "Decimal":
                                                prop.SetValue(returnXML, SepFunctions.toDecimal(fieldValue), null);
                                                break;

                                            default:
                                                prop.SetValue(returnXML, fieldValue, null);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                returnXML.AdID = SepFunctions.toLong(SepFunctions.openNull(RS["AdID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.LinkID = SepFunctions.toLong(SepFunctions.openNull(RS["LinkID"]));
                                returnXML.Title = SepFunctions.openNull(RS["Title"]);
                                returnXML.Description = SepFunctions.openNull(RS["Description"]);
                                returnXML.StartBid = SepFunctions.Format_Currency(SepFunctions.openNull(RS["StartBid"]));
                                if (SepFunctions.Format_Currency(SepFunctions.openNull(RS["CurrentBid"])) == SepFunctions.Format_Currency("0"))
                                {
                                    returnXML.CurrentBid = SepFunctions.Format_Currency(SepFunctions.openNull(RS["StartBid"]));
                                }
                                else
                                {
                                    returnXML.CurrentBid = SepFunctions.Format_Currency(SepFunctions.openNull(RS["CurrentBid"]));
                                }

                                returnXML.MaxBid = SepFunctions.openNull(RS["MaxBid"]);
                                returnXML.BidUserID = SepFunctions.openNull(RS["BidUserID"]);
                                returnXML.BidIncrease = SepFunctions.openNull(RS["BidIncrease"]);
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.EndDate = SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"]));
                                returnXML.TotalBids = SepFunctions.toLong(SepFunctions.openNull(RS["TotalBids"]));
                                returnXML.Visits = SepFunctions.toLong(SepFunctions.openNull(RS["Visits"]));
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.OldAd = SepFunctions.openNull(RS["OldAd"]);
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Auctions the save.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Title">The title.</param>
        /// <param name="Description">The description.</param>
        /// <param name="StartBid">The start bid.</param>
        /// <param name="BidIncrease">The bid increase.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Auction_Save(long AdID, string UserID, long CatID, string Title, string Description, double StartBid, double BidIncrease, DateTime EndDate, bool Approved, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (AdID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM AuctionAds WHERE AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", AdID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    oldValues.Add(RS.GetName(i), SepFunctions.openNull(RS[i]));
                                }

                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    AdID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE AuctionAds SET CatID=@CatID, Title=@Title, Description=@Description, StartBid=@StartBid, BidIncrease=@BidIncrease, EndDate=@EndDate WHERE AdID=@AdID";
                }
                else
                {
                    SqlStr = "INSERT INTO AuctionAds (AdID, CatID, LinkID, UserID, Title, Description, StartBid, CurrentBid, MaxBid, BidUserID, BidIncrease, EndDate, TotalBids, Status, PortalID, Visits, DatePosted, OldAd) VALUES (@AdID, @CatID, @LinkID, @UserID, @Title, @Description, @StartBid, '0', '0', '', @BidIncrease, @EndDate, '0', @Status, @PortalID, '0', @DatePosted, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    cmd.Parameters.AddWithValue("@LinkID", AdID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@StartBid", StartBid);
                    cmd.Parameters.AddWithValue("@BidIncrease", BidIncrease);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.Parameters.AddWithValue("@Status", Approved ? "1" : "0");
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    // Write Additional Data
                    if (bUpdate == false)
                    {
                        isNewRecord = true;
                    }

                    intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 31, Strings.ToString(AdID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Auctions", "Auctions", "AddAction");

                    if (oldValues.Count > 0)
                    {
                        var changedValues = new Hashtable();
                        using (var cmd2 = new SqlCommand("SELECT * FROM AuctionAds WHERE AdID=@AdID", conn))
                        {
                            cmd2.Parameters.AddWithValue("@AdID", AdID);
                            using (SqlDataReader RS = cmd2.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    for (var i = 0; i < RS.FieldCount; i++)
                                    {
                                        if (oldValues.ContainsKey(RS.GetName(i)))
                                        {
                                            if (SepFunctions.openNull(RS[i]) != SepFunctions.openNull(oldValues[RS.GetName(i)]))
                                            {
                                                changedValues.Add(RS.GetName(i), SepFunctions.openNull(oldValues[RS.GetName(i)]));
                                            }
                                        }
                                    }

                                    bUpdate = true;
                                }
                            }
                        }

                        if (changedValues.Count > 0)
                        {
                            var writeLog = new StringBuilder();
                            writeLog.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>");
                            writeLog.AppendLine("<root>");
                            for (var e = changedValues.GetEnumerator(); e.MoveNext();)
                            {
                                writeLog.AppendLine("<" + e.Key + ">" + SepFunctions.HTMLEncode(Strings.ToString(e.Value)) + "</" + e.Key + ">");
                            }

                            writeLog.AppendLine("</root>");
                            SepFunctions.Update_Change_Log(31, Strings.ToString(AdID), Title, Strings.ToString(writeLog));
                        }
                    }
                }
            }

            if (intReturn == 0)
            {
                if (bUpdate)
                {
                    return 2;
                }

                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Gets the auction ads.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="Distance">The distance.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="state">The state.</param>
        /// <param name="soldItems">if set to <c>true</c> [sold items].</param>
        /// <param name="availableItems">if set to <c>true</c> [available items].</param>
        /// <param name="boughtUserID">The bought user identifier.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="Country">The country.</param>
        /// <returns>List&lt;Models.AuctionAds&gt;.</returns>
        public static List<AuctionAds> GetAuctionAds(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "", string userId = "", string Distance = "", string postalCode = "", string state = "", bool soldItems = false, bool availableItems = false, string boughtUserID = "", long CategoryId = -1, string StartDate = "", string Country = "")
        {
            var lAuctionAds = new List<AuctionAds>();

            var wClause = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "Mod.UserID=M.UserID AND Mod.AdID=Mod.LinkID AND Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Mod.Title LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(userId) + "'";
            }

            if (soldItems)
            {
                wClause += " AND Mod.EndDate < GETDATE() AND Mod.BidUserID <> ''";
            }

            if (availableItems)
            {
                wClause += " AND Mod.EndDate > GETDATE()";
            }

            if (!string.IsNullOrWhiteSpace(boughtUserID))
            {
                wClause += " AND Mod.BidUserID='" + SepFunctions.FixWord(boughtUserID) + "'";
            }

            if (soldItems == false && string.IsNullOrWhiteSpace(boughtUserID))
            {
                wClause += " AND Mod.LinkID=Mod.AdID";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            if (!string.IsNullOrWhiteSpace(postalCode) && !string.IsNullOrWhiteSpace(Distance))
            {
                var sCountry = string.Empty;
                if (string.IsNullOrWhiteSpace(Country))
                {
                    sCountry = "us";
                }

                var cPostalCodes = Integrations.SepFunctions.PostalCodesInDistance(sCountry, postalCode, Distance, string.Empty);
                var sPostalClause = string.Empty;
                if (cPostalCodes.PostalCodes.Count > 0)
                {
                    for (var i = 0; i <= cPostalCodes.PostalCodes.Count - 1; i++)
                    {
                        if (i > 0)
                        {
                            sPostalClause += ",";
                        }

                        sPostalClause += "'" + cPostalCodes.PostalCodes[i] + "'";
                    }

                    wClause += " AND M.ZipCode IN (" + sPostalClause + ")";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(state))
                {
                    wClause += " AND M.State='" + SepFunctions.FixWord(state) + "'";
                }
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.AdID,Mod.CatID,Mod.DatePosted,Mod.Title,Mod.Status,Mod.UserID,Mod.StartBid,Mod.CurrentBid,Mod.EndDate,(SELECT TOP 1 Featured FROM PricingOptions WHERE ModuleID='31' AND UniqueID=Mod.LinkID) AS Featured,(SELECT TOP 1 BoldTitle FROM PricingOptions WHERE ModuleID='31' AND UniqueID=Mod.LinkID) AS BoldTitle,(SELECT TOP 1 Highlight FROM PricingOptions WHERE ModuleID='31' AND UniqueID=Mod.LinkID) AS Highlight,(SELECT TOP 1 UserName FROM Members WHERE UserID=Mod.BidUserID) AS SoldUserName,(SELECT TOP 1 UserName FROM Members WHERE UserID=Mod.UserID) AS SellerUserName,Mod.BidUserID,(SELECT TOP 1 FeedbackID FROM AuctionFeedback WHERE AdID=Mod.LinkID AND BORS='B' AND ToUserID=Mod.UserID) AS SellerFeedbackID,(SELECT TOP 1 FeedbackID FROM AuctionFeedback WHERE AdID=Mod.LinkID AND BORS='S' AND ToUserID=Mod.BidUserID) AS BuyerFeedbackID,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='31' AND UniqueID=Mod.AdID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID FROM AuctionAds AS Mod,Members AS M" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dAuctionAds = new Models.AuctionAds { AdID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AdID"])) };
                    dAuctionAds.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dAuctionAds.Title = SepFunctions.openNull(ds.Tables[0].Rows[i]["Title"]);
                    if (SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["CurrentBid"])) == SepFunctions.Format_Currency("0"))
                    {
                        dAuctionAds.CurrentBid = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartBid"]));
                    }
                    else
                    {
                        dAuctionAds.CurrentBid = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["CurrentBid"]));
                    }

                    dAuctionAds.SoldUserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["SoldUserName"]);
                    dAuctionAds.BidUserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["BidUserID"]);
                    dAuctionAds.SellerUserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["SellerUserName"]);
                    dAuctionAds.BuyerFeedbackID = SepFunctions.openNull(ds.Tables[0].Rows[i]["BuyerFeedbackID"]);
                    dAuctionAds.SellerFeedbackID = SepFunctions.openNull(ds.Tables[0].Rows[i]["SellerFeedbackID"]);
                    dAuctionAds.EndDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndDate"]));
                    dAuctionAds.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dAuctionAds.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dAuctionAds.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=44&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        dAuctionAds.DefaultPicture = sImageFolder + "images/public/no-photo.jpg";
                    }

                    dAuctionAds.Featured = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Featured"]);
                    dAuctionAds.BoldTitle = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["BoldTitle"]);
                    dAuctionAds.Highlight = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Highlight"]);
                    lAuctionAds.Add(dAuctionAds);
                }
            }

            return lAuctionAds;
        }
    }
}