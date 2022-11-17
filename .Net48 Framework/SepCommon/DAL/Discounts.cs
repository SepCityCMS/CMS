// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Discounts.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class Discounts.
    /// </summary>
    public static class Discounts
    {
        /// <summary>
        /// Discounts the change status.
        /// </summary>
        /// <param name="DiscountIDs">The discount i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Discount_Change_Status(string DiscountIDs, int Status)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrDiscountIDs = Strings.Split(DiscountIDs, ",");

                if (arrDiscountIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrDiscountIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE DiscountSystem SET Status=@Status WHERE DiscountID=@DiscountID", conn))
                        {
                            cmd.Parameters.AddWithValue("@DiscountID", arrDiscountIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Discount(s) status has been successfully saved.");
        }

        /// <summary>
        /// Discounts the delete.
        /// </summary>
        /// <param name="DiscountIDs">The discount i ds.</param>
        /// <returns>System.String.</returns>
        public static string Discount_Delete(string DiscountIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrDiscountIDs = Strings.Split(DiscountIDs, ",");

                if (arrDiscountIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrDiscountIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE DiscountSystem SET Status='-1', DateDeleted=@DateDeleted WHERE DiscountID=@DiscountID", conn))
                        {
                            cmd.Parameters.AddWithValue("@DiscountID", arrDiscountIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(5, arrDiscountIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Discount(s) has been successfully deleted.");
        }

        /// <summary>
        /// Discounts the get.
        /// </summary>
        /// <param name="DiscountID">The discount identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.Discounts.</returns>
        public static Models.Discounts Discount_Get(long DiscountID, long ChangeID = 0)
        {
            var returnXML = new Models.Discounts();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE DiscountSystem SET Visits=Visits+1 WHERE DiscountID=@DiscountID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM DiscountSystem WHERE DiscountID=@DiscountID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
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
                                returnXML.DiscountID = SepFunctions.toLong(SepFunctions.openNull(RS["DiscountID"]));
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.DiscountCode = SepFunctions.openNull(RS["DiscountCode"]);
                                returnXML.MarkOffPrice = SepFunctions.openNull(RS["PriceOff"]);
                                returnXML.Quantity = SepFunctions.toLong(SepFunctions.openNull(RS["Quantity"]));
                                returnXML.ExpireDate = SepFunctions.toDate(SepFunctions.openNull(RS["ExpireDate"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.Disclaimer = SepFunctions.openNull(RS["Disclaimer"]);
                                returnXML.ShowWeb = SepFunctions.toBoolean(SepFunctions.openNull(RS["ShowWeb"]));
                                returnXML.LabelText = SepFunctions.openNull(RS["LabelText"]);
                                returnXML.CompanyName = SepFunctions.openNull(RS["CompanyName"]);
                                returnXML.City = SepFunctions.openNull(RS["City"]);
                                returnXML.State = SepFunctions.openNull(RS["State"]);
                                returnXML.PostalCode = SepFunctions.openNull(RS["PostalCode"]);
                                returnXML.Country = SepFunctions.openNull(RS["Country"]);
                                returnXML.Visits = SepFunctions.toLong(SepFunctions.openNull(RS["Visits"]));
                                returnXML.MarkOffType = SepFunctions.openNull(RS["PriceType"]);
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }

                if (returnXML.DiscountID == 0)
                {
                    var sImageFolder = SepFunctions.GetInstallFolder(true);
                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE UniqueID=@DiscountID AND ModuleID='5' AND isTemp='0' AND Approved='1' AND ControlID='ProductImageUpload'", conn))
                    {
                        cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.ProductImage = sImageFolder + "spadmin/show_image.aspx?ModuleID=5&Size=thumb&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                            }
                        }
                    }

                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE UniqueID=@DiscountID AND ModuleID='5' AND isTemp='0' AND Approved='1' AND ControlID='BarCodeUpload'", conn))
                    {
                        cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.BarCodeImage = sImageFolder + "spadmin/show_image.aspx?ModuleID=5&Size=thumb&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Discounts the save.
        /// </summary>
        /// <param name="DiscountID">The discount identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="DiscountCode">The discount code.</param>
        /// <param name="CompanyName">Name of the company.</param>
        /// <param name="LabelText">The label text.</param>
        /// <param name="Disclaimer">The disclaimer.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="Quantity">The quantity.</param>
        /// <param name="MarkOff">The mark off.</param>
        /// <param name="MarkOffType">Type of the mark off.</param>
        /// <param name="ExpireDate">The expire date.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Discount_Save(long DiscountID, string UserID, long CatID, string DiscountCode, string CompanyName, string LabelText, string Disclaimer, string City, string State, string PostalCode, string Country, long Quantity, string MarkOff, int MarkOffType, DateTime ExpireDate, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (DiscountID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM DiscountSystem WHERE DiscountID=@DiscountID", conn))
                    {
                        cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
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
                    DiscountID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE DiscountSystem SET DiscountCode=@DiscountCode, PriceOff=@PriceOff, Quantity=@Quantity, ExpireDate=@ExpireDate, CatID=@CatID, Disclaimer=@Disclaimer, LabelText=@LabelText, CompanyName=@CompanyName, City=@City, State=@State, PostalCode=@PostalCode, Country=@Country, PriceType=@PriceType, PortalID=@PortalID WHERE DiscountID=@DiscountID";
                }
                else
                {
                    SqlStr = "INSERT INTO DiscountSystem (DiscountID, DiscountCode, PriceOff, Quantity, ExpireDate, CatID, Disclaimer, LabelText, CompanyName, City, State, PostalCode, Country, PriceType, UserID, PortalID, DatePosted, Visits, Status, ShowWeb) VALUES (@DiscountID, @DiscountCode, @PriceOff, @Quantity, @ExpireDate, @CatID, @Disclaimer, @LabelText, @CompanyName, @City, @State, @PostalCode, @Country, @PriceType, @UserID, @PortalID, @DatePosted, '0', '1', '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                    cmd.Parameters.AddWithValue("@DiscountCode", DiscountCode);
                    cmd.Parameters.AddWithValue("@PriceOff", MarkOff);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@ExpireDate", ExpireDate);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@Disclaimer", Disclaimer);
                    cmd.Parameters.AddWithValue("@LabelText", LabelText);
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@PriceType", MarkOffType);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 5, Strings.ToString(DiscountID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Coupon", "Discount Coupon", "AddCoupon");
                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM DiscountSystem WHERE DiscountID=@DiscountID", conn))
                    {
                        cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
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
                        SepFunctions.Update_Change_Log(5, Strings.ToString(DiscountID), LabelText, Strings.ToString(writeLog));
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
        }

        /// <summary>
        /// Gets the discounts.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryID">The category identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="showAvailable">if set to <c>true</c> [show available].</param>
        /// <returns>List&lt;Models.Discounts&gt;.</returns>
        public static List<Models.Discounts> GetDiscounts(string SortExpression = "DiscountCode", string SortDirection = "ASC", string searchWords = "", long CategoryID = -1, string UserID = "", string StartDate = "", bool showAvailable = false)
        {
            var lDiscounts = new List<Models.Discounts>();

            var sImageFolder = SepFunctions.GetInstallFolder(true);
            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DiscountCode";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            string wClause = "Mod.UserID=M.UserID AND Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND DiscountCode LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (CategoryID >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryID)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            if (showAvailable)
            {
                wClause += " AND Mod.ExpireDate > getDate() AND Mod.Quantity > '0' AND Mod.ShowWeb='1' AND Mod.Status=1";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.DiscountID,Mod.CatID,Mod.LabelText,Mod.DiscountCode,Mod.DatePosted,Mod.ExpireDate,Mod.Quantity,Mod.Status,Mod.Disclaimer,Mod.CompanyName,Mod.PriceType,Mod.PriceOff,Mod.UserID,M.UserName,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='5' AND UniqueID=MOD.DiscountID AND Approved='1' AND isTemp='0' AND ControlID='ProductImageUpload') AS ProductImage,(SELECT TOP 1 UploadID FROM Uploads WHERE UniqueID=MOD.DiscountID AND ModuleID='5' AND Approved='1' AND isTemp='0' AND ControlID='BarCodeUpload') AS BarCodeImage FROM DiscountSystem AS Mod,Members AS M" + SepFunctions.Category_SQL_Manage_Select(CategoryID, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dDiscounts = new Models.Discounts { DiscountID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["DiscountID"])) };
                    dDiscounts.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dDiscounts.LabelText = SepFunctions.openNull(ds.Tables[0].Rows[i]["LabelText"]);
                    dDiscounts.DiscountCode = SepFunctions.openNull(ds.Tables[0].Rows[i]["DiscountCode"]);
                    dDiscounts.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dDiscounts.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dDiscounts.ExpireDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["ExpireDate"]));
                    dDiscounts.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dDiscounts.Quantity = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Quantity"]));
                    dDiscounts.CompanyName = SepFunctions.openNull(ds.Tables[0].Rows[i]["CompanyName"]);
                    dDiscounts.MarkOffType = SepFunctions.openNull(ds.Tables[0].Rows[i]["PriceType"]);
                    dDiscounts.MarkOffPrice = SepFunctions.openNull(ds.Tables[0].Rows[i]["PriceOff"]);
                    dDiscounts.Disclaimer = SepFunctions.openNull(ds.Tables[0].Rows[i]["Disclaimer"]);
                    dDiscounts.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductImage"])))
                    {
                        dDiscounts.ProductImage = sImageFolder + "spadmin/show_image.aspx?ModuleID=5&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductImage"]);
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["BarCodeImage"])))
                    {
                        dDiscounts.BarCodeImage = sImageFolder + "spadmin/show_image.aspx?ModuleID=5&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["BarCodeImage"]);
                    }

                    lDiscounts.Add(dDiscounts);
                }
            }

            return lDiscounts;
        }
    }
}