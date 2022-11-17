// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Classifieds.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    /// Class Classifieds.
    /// </summary>
    public static class Classifieds
    {
        /// <summary>
        /// Classifieds the change status.
        /// </summary>
        /// <param name="AdIDs">The ad i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Classified_Change_Status(string AdIDs, int Status)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAdIDs = Strings.Split(AdIDs, ",");

                if (arrAdIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAdIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ClassifiedsAds SET Status=@Status WHERE AdID=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", arrAdIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = Server.SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = Server.SepFunctions.LangText("Ads(s) status has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Classifieds the delete.
        /// </summary>
        /// <param name="AdIDs">The ad i ds.</param>
        /// <returns>System.String.</returns>
        public static string Classified_Delete(string AdIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAdIDs = Strings.Split(AdIDs, ",");

                if (arrAdIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAdIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ClassifiedsAds SET Status='-1', DateDeleted=@DateDeleted WHERE AdID=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", arrAdIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ClassifiedsFeedback SET Status='-1', DateDeleted=@DateDeleted WHERE AdID=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", arrAdIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        Server.SepFunctions.Additional_Data_Delete(44, arrAdIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = Server.SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = Server.SepFunctions.LangText("Ads(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Classifieds the get.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.ClassifiedAds.</returns>
        public static ClassifiedAds Classified_Get(long AdID, long ChangeID = 0)
        {
            var returnXML = new Models.ClassifiedAds();

            using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("UPDATE ClassifiedsAds SET Visits=Visits+1 WHERE AdID=@AdID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT Ads.*,(SELECT City + ', ' + State AS Location FROM Members WHERE UserID=Ads.UserID) AS Location,(SELECT PayPal FROM Members WHERE UserID=Ads.UserID) AS PayPalEmail,(SELECT Count(AdID) FROM ClassifiedsAds WHERE LinkID=Ads.AdID) AS Quantity FROM ClassifiedsAds AS Ads WHERE Ads.AdID=@AdID AND Ads.Status <> -1", conn))
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
                                logData = Server.SepFunctions.Get_Change_Log(ChangeID);
                            }

                            if (ChangeID > 0 && !string.IsNullOrWhiteSpace(logData))
                            {
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    var fieldName = RS.GetName(i);
                                    var fieldType = RS.GetFieldType(i);
                                    var fieldValue = Server.SepFunctions.openNull(RS[i]);
                                    if (Strings.InStr(logData, "<" + fieldName + ">") > 0)
                                    {
                                        var xmlNode = Server.SepFunctions.ParseXML(fieldName, logData);
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
                                                prop.SetValue(returnXML, Server.SepFunctions.toLong(fieldValue), null);
                                                break;

                                            case "DateTime":
                                                prop.SetValue(returnXML, Server.SepFunctions.toDate(fieldValue), null);
                                                break;

                                            case "Boolean":
                                                prop.SetValue(returnXML, Server.SepFunctions.toBoolean(fieldValue), null);
                                                break;

                                            case "Int32":
                                                prop.SetValue(returnXML, Server.SepFunctions.toInt(fieldValue), null);
                                                break;

                                            case "Decimal":
                                                prop.SetValue(returnXML, Server.SepFunctions.toDecimal(fieldValue), null);
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
                                returnXML.AdID = Server.SepFunctions.toLong(Server.SepFunctions.openNull(RS["AdID"]));
                                returnXML.CatID = Server.SepFunctions.toLong(Server.SepFunctions.openNull(RS["CatID"]));
                                returnXML.Title = Server.SepFunctions.openNull(RS["Title"]);
                                returnXML.Description = Server.SepFunctions.openNull(RS["Description"]);
                                returnXML.Price = Server.SepFunctions.toDecimal(Server.SepFunctions.openNull(RS["Price"]));
                                returnXML.UserID = Server.SepFunctions.openNull(RS["UserID"]);
                                returnXML.Quantity = Server.SepFunctions.toLong(Server.SepFunctions.openNull(RS["Quantity"]));
                                returnXML.Location = Server.SepFunctions.openNull(RS["Location"]);
                                returnXML.DatePosted = Server.SepFunctions.toDate(Server.SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.EndDate = Server.SepFunctions.toDate(Server.SepFunctions.openNull(RS["EndDate"]));
                                returnXML.Visits = Server.SepFunctions.toLong(Server.SepFunctions.openNull(RS["Visits"]));
                                returnXML.PortalID = Server.SepFunctions.toLong(Server.SepFunctions.openNull(RS["PortalID"]));
                                returnXML.LinkID = Server.SepFunctions.toLong(Server.SepFunctions.openNull(RS["LinkID"]));
                                returnXML.SoldUserID = Server.SepFunctions.openNull(RS["SoldUserID"]);
                                returnXML.PayPalEmail = Server.SepFunctions.openNull(RS["PayPalEmail"]);
                                returnXML.SoldDate = Server.SepFunctions.toDate(Server.SepFunctions.openNull(RS["SoldDate"]));
                                returnXML.Soldout = Server.SepFunctions.toBoolean(Server.SepFunctions.openNull(RS["Soldout"]));
                                returnXML.Status = Server.SepFunctions.toInt(Server.SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Classifieds the save.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Title">The title.</param>
        /// <param name="Description">The description.</param>
        /// <param name="Quantity">The quantity.</param>
        /// <param name="Price">The price.</param>
        /// <param name="ExpirationDate">The expiration date.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Classified_Save(long AdID, string UserID, long CatID, string Title, string Description, long Quantity, double Price, DateTime ExpirationDate, bool Approved, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            if (Quantity > 100)
            {
                Quantity = 100;
            }

            using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (AdID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM ClassifiedsAds WHERE AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", AdID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    oldValues.Add(RS.GetName(i), Server.SepFunctions.openNull(RS[i]));
                                }

                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    AdID = Server.SepFunctions.GetIdentity();
                }

                using (var cmd = new SqlCommand("DELETE FROM ClassifiedsAds WHERE AdID <> LinkID AND LinkID=@AdID", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    cmd.ExecuteNonQuery();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ClassifiedsAds SET CatID=@CatID, Title=@Title, Description=@Description, Price=@Price, EndDate=@EndDate WHERE AdID=@AdID";
                }
                else
                {
                    SqlStr = "INSERT INTO ClassifiedsAds (AdID, CatID, LinkID, UserID, Title, Description, Price, EndDate, Status, PortalID, Visits, DatePosted, Soldout) VALUES (@AdID, @CatID, @LinkID, @UserID, @Title, @Description, @Price, @EndDate, @Status, @PortalID, '0', @DatePosted, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    cmd.Parameters.AddWithValue("@LinkID", AdID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@EndDate", Information.IsDate(ExpirationDate) ? ExpirationDate : DateAndTime.DateAdd(DateAndTime.DateInterval.Day, Server.SepFunctions.toInt(Server.SepFunctions.Setup(44, "ClassifiedDeleteDays")), DateTime.Now));
                    cmd.Parameters.AddWithValue("@Status", Approved ? "1" : "0");
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                if (Quantity > 1)
                {
                    for (var i = 1; i <= Quantity - 1; i++)
                    {
                        using (var cmd = new SqlCommand("INSERT INTO ClassifiedsAds (AdID, CatID, LinkID, UserID, Title, Description, Price, EndDate, Status, PortalID, Visits, DatePosted, Soldout) VALUES (@AdID, @CatID, @LinkID, @UserID, @Title, @Description, @Price, @EndDate, @Status, @PortalID, '0', @DatePosted, '0')", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", Server.SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@LinkID", AdID);
                            cmd.Parameters.AddWithValue("@CatID", CatID);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@Title", Title);
                            cmd.Parameters.AddWithValue("@Description", Description);
                            cmd.Parameters.AddWithValue("@Price", Price);
                            cmd.Parameters.AddWithValue("@EndDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Day, Server.SepFunctions.toInt(Server.SepFunctions.Setup(44, "ClassifiedDeleteDays")), DateTime.Now));
                            cmd.Parameters.AddWithValue("@Status", Approved ? "1" : "0");
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = Server.SepFunctions.Additional_Data_Save(isNewRecord, 44, Strings.ToString(AdID), UserID, Server.SepFunctions.GetUserInformation("UserName", UserID), "Classifieds", "Classified Ads", "PostAd");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM ClassifiedsAds WHERE AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", AdID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    if (oldValues.ContainsKey(RS.GetName(i)))
                                    {
                                        if (Server.SepFunctions.openNull(RS[i]) != Server.SepFunctions.openNull(oldValues[RS.GetName(i)]))
                                        {
                                            changedValues.Add(RS.GetName(i), Server.SepFunctions.openNull(oldValues[RS.GetName(i)]));
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
                            writeLog.AppendLine("<" + e.Key + ">" + Server.SepFunctions.HTMLEncode(Strings.ToString(e.Value)) + "</" + e.Key + ">");
                        }

                        writeLog.AppendLine("</root>");
                        Server.SepFunctions.Update_Change_Log(44, Strings.ToString(AdID), Title, Strings.ToString(writeLog));
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
        /// Gets the classified ads.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="Distance">The distance.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="state">The state.</param>
        /// <param name="soldItems">if set to <c>true</c> [sold items].</param>
        /// <param name="availableItems">if set to <c>true</c> [available items].</param>
        /// <param name="boughtUserID">The bought user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="Country">The country.</param>
        /// <returns>List&lt;Models.ClassifiedAds&gt;.</returns>
        public static List<ClassifiedAds> GetClassifiedAds(string SortExpression = "Mod.DatePosted", string SortDirection = "DESC", string searchWords = "", string userId = "", long CategoryId = -1, string Distance = "", string postalCode = "", string state = "", bool soldItems = false, bool availableItems = false, string boughtUserID = "", string StartDate = "", string Country = "")
        {
            var lClassifiedAds = new List<ClassifiedAds>();
            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Mod.DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "M.UserID=Mod.UserID AND Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Mod.Title LIKE '" + Server.SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                wClause += " AND Mod.UserID='" + Server.SepFunctions.FixWord(userId) + "'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + Server.SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (soldItems)
            {
                wClause += " AND SoldOut='1'";
            }

            if (availableItems)
            {
                wClause += " AND EndDate > GETDATE() AND SoldOut='0'";
            }

            if (!string.IsNullOrWhiteSpace(boughtUserID))
            {
                wClause += " AND SoldUserID='" + Server.SepFunctions.FixWord(boughtUserID) + "'";
            }

            if (soldItems == false && string.IsNullOrWhiteSpace(boughtUserID))
            {
                wClause += " AND Mod.LinkID=Mod.AdID";
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
                    wClause += " AND M.State='" + Server.SepFunctions.FixWord(state) + "'";
                }
            }

            var sImageFolder = Server.SepFunctions.GetInstallFolder(true);

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.AdID,Mod.CatID,Mod.Title,Mod.UserID,Mod.Price,Mod.SoldDate,Mod.Status,Mod.Visits,Mod.DatePosted,(SELECT TOP 1 Featured FROM PricingOptions WHERE ModuleID='44' AND UniqueID=Mod.LinkID) AS Featured,(SELECT TOP 1 BoldTitle FROM PricingOptions WHERE ModuleID='44' AND UniqueID=Mod.LinkID) AS BoldTitle,(SELECT TOP 1 Highlight FROM PricingOptions WHERE ModuleID='44' AND UniqueID=Mod.LinkID) AS Highlight,(SELECT TOP 1 UserName FROM Members WHERE UserID=Mod.SoldUserID) AS SoldUserName,(SELECT TOP 1 UserName FROM Members WHERE UserID=Mod.UserID) AS SellerUserName,Mod.SoldUserID,(SELECT TOP 1 FeedbackID FROM ClassifiedsFeedback WHERE AdID=Mod.LinkID AND BORS='B' AND ToUserID=Mod.UserID) AS SellerFeedbackID,(SELECT TOP 1 FeedbackID FROM ClassifiedsFeedback WHERE AdID=Mod.LinkID AND BORS='S' AND ToUserID=Mod.SoldUserID) AS BuyerFeedbackID,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='44' AND UniqueID=Mod.AdID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID FROM ClassifiedsAds AS Mod,Members AS M " + Server.SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dClassifiedAds = new Models.ClassifiedAds { AdID = Server.SepFunctions.toLong(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["AdID"])) };
                    dClassifiedAds.CatID = Server.SepFunctions.toLong(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dClassifiedAds.Title = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["Title"]);
                    dClassifiedAds.UserID = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dClassifiedAds.Price = Server.SepFunctions.toDecimal(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["Price"]));
                    dClassifiedAds.SoldDate = Server.SepFunctions.toDate(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["SoldDate"]));
                    dClassifiedAds.SoldUserName = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["SoldUserName"]);
                    dClassifiedAds.SoldUserID = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["SoldUserID"]);
                    dClassifiedAds.SellerUserName = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["SellerUserName"]);
                    dClassifiedAds.BuyerFeedbackID = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["BuyerFeedbackID"]);
                    dClassifiedAds.SellerFeedbackID = Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["SellerFeedbackID"]);
                    dClassifiedAds.Status = Server.SepFunctions.toInt(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dClassifiedAds.Visits = Server.SepFunctions.toLong(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["Visits"]));
                    dClassifiedAds.DatePosted = Server.SepFunctions.toDate(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    if (!string.IsNullOrWhiteSpace(Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dClassifiedAds.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=44&Size=thumb&UploadID=" + Server.SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        dClassifiedAds.DefaultPicture = sImageFolder + "images/public/no-photo.jpg";
                    }

                    dClassifiedAds.Featured = Server.SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Featured"]);
                    dClassifiedAds.BoldTitle = Server.SepFunctions.openBoolean(ds.Tables[0].Rows[i]["BoldTitle"]);
                    dClassifiedAds.Highlight = Server.SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Highlight"]);
                    lClassifiedAds.Add(dClassifiedAds);
                }
            }

            return lClassifiedAds;
        }
    }
}