// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Vouchers.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class Vouchers.
    /// </summary>
    public static class Vouchers
    {
        /// <summary>
        /// Gets the vouchers.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="onlyAvailable">if set to <c>true</c> [only available].</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.Vouchers&gt;.</returns>
        public static List<Models.Vouchers> GetVouchers(string SortExpression = "BuyTitle", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1, string UserID = "", bool onlyAvailable = false, string StartDate = "")
        {
            var lVouchers = new List<Models.Vouchers>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "BuyTitle";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            wClause = "Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (Mod.BuyTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Mod.PurchaseCode LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND (Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "')";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND (Mod.UserID='" + SepFunctions.FixWord(UserID) + "')";
            }

            if (onlyAvailable)
            {
                wClause += " AND Mod.BuyEndDate > getDate()";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.VoucherID,Mod.CatID,Mod.BuyTitle,Mod.PurchaseCode,Mod.ShortDescription,Mod.SalePrice,Mod.DatePosted FROM Vouchers AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dVouchers = new Models.Vouchers { VoucherID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["VoucherID"])) };
                    dVouchers.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dVouchers.BuyTitle = SepFunctions.openNull(ds.Tables[0].Rows[i]["BuyTitle"]);
                    dVouchers.PurchaseCode = SepFunctions.openNull(ds.Tables[0].Rows[i]["PurchaseCode"]);
                    dVouchers.ShortDescription = SepFunctions.openNull(ds.Tables[0].Rows[i]["ShortDescription"]);
                    dVouchers.SalePrice = SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"]));
                    dVouchers.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lVouchers.Add(dVouchers);
                }
            }

            return lVouchers;
        }

        /// <summary>
        /// Gets the vouchers purchased.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="onlyAvailable">if set to <c>true</c> [only available].</param>
        /// <returns>List&lt;Models.Vouchers&gt;.</returns>
        public static List<Models.Vouchers> GetVouchersPurchased(string SortExpression = "BuyTitle", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1, string UserID = "", bool onlyAvailable = false)
        {
            var lVouchers = new List<Models.Vouchers>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "BuyTitle";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            wClause = "Mod.VoucherID=VouchersPurchased.VoucherID";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (Mod.BuyTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Mod.PurchaseCode LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND (Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "')";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND (Mod.UserID='" + SepFunctions.FixWord(UserID) + "')";
            }

            if (onlyAvailable)
            {
                wClause += " AND Mod.BuyEndDate > getDate()";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.VoucherID,Mod.CatID,Mod.BuyTitle,Mod.PurchaseCode,Mod.ShortDescription,Mod.SalePrice,VouchersPurchased.Redeemed,VouchersPurchased.UserID AS BoughtBy,Mod.UserID AS SoldBy FROM Vouchers AS Mod,VouchersPurchased" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dVouchers = new Models.Vouchers { VoucherID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["VoucherID"])) };
                    dVouchers.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dVouchers.BuyTitle = SepFunctions.openNull(ds.Tables[0].Rows[i]["BuyTitle"]);
                    dVouchers.PurchaseCode = SepFunctions.openNull(ds.Tables[0].Rows[i]["PurchaseCode"]);
                    dVouchers.ShortDescription = SepFunctions.openNull(ds.Tables[0].Rows[i]["ShortDescription"]);
                    dVouchers.SalePrice = SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"]));
                    dVouchers.Redeemed = SepFunctions.openNull(ds.Tables[0].Rows[i]["Redeemed"]);
                    dVouchers.BoughtBy = SepFunctions.openNull(ds.Tables[0].Rows[i]["BoughtBy"]);
                    dVouchers.SoldBy = SepFunctions.openNull(ds.Tables[0].Rows[i]["SoldBy"]);
                    lVouchers.Add(dVouchers);
                }
            }

            return lVouchers;
        }

        /// <summary>
        /// Vouchers the delete.
        /// </summary>
        /// <param name="VoucherIDs">The voucher i ds.</param>
        /// <returns>System.String.</returns>
        public static string Voucher_Delete(string VoucherIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrVoucherIDs = Strings.Split(VoucherIDs, ",");

                if (arrVoucherIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrVoucherIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Vouchers SET Status='-1', DateDeleted=@DateDeleted WHERE VoucherID=@VoucherID", conn))
                        {
                            cmd.Parameters.AddWithValue("@VoucherID", arrVoucherIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(65, arrVoucherIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Voucher(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Vouchers the get.
        /// </summary>
        /// <param name="VoucherID">The voucher identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.Vouchers.</returns>
        public static Models.Vouchers Voucher_Get(long VoucherID, long ChangeID = 0)
        {
            var returnXML = new Models.Vouchers();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Vouchers WHERE VoucherID=@VoucherID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@VoucherID", VoucherID);
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
                                returnXML.VoucherID = SepFunctions.toLong(SepFunctions.openNull(RS["VoucherID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.BuyTitle = SepFunctions.openNull(RS["BuyTitle"]);
                                returnXML.ShortDescription = SepFunctions.openNull(RS["ShortDescription"]);
                                returnXML.LongDescription = SepFunctions.openNull(RS["LongDescription"]);
                                returnXML.SalePrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["SalePrice"]));
                                returnXML.RegularPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["RegularPrice"]));
                                returnXML.Quantity = SepFunctions.toLong(SepFunctions.openNull(RS["Quantity"]));
                                returnXML.MaxNumPerUser = SepFunctions.toLong(SepFunctions.openNull(RS["MaxNumPerUser"]));
                                returnXML.RedemptionStart = SepFunctions.toDate(SepFunctions.openNull(RS["RedemptionStart"]));
                                returnXML.RedemptionEnd = SepFunctions.toDate(SepFunctions.openNull(RS["RedemptionEnd"]));
                                returnXML.PurchaseCode = SepFunctions.openNull(RS["PurchaseCode"]);
                                returnXML.BusinessName = SepFunctions.openNull(RS["BusinessName"]);
                                returnXML.Address = SepFunctions.openNull(RS["Address"]);
                                returnXML.City = SepFunctions.openNull(RS["City"]);
                                returnXML.State = SepFunctions.openNull(RS["State"]);
                                returnXML.ZipCode = SepFunctions.openNull(RS["ZipCode"]);
                                returnXML.Country = SepFunctions.openNull(RS["Country"]);
                                returnXML.ContactEmail = SepFunctions.openNull(RS["ContactEmail"]);
                                returnXML.ContactName = SepFunctions.openNull(RS["ContactName"]);
                                returnXML.PhoneNumber = SepFunctions.openNull(RS["PhoneNumber"]);
                                returnXML.Disclaimer = SepFunctions.openNull(RS["Disclaimer"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.TotalPurchases = SepFunctions.toLong(SepFunctions.openNull(RS["TotalPurchases"]));
                                returnXML.BuyEndDate = SepFunctions.toDate(SepFunctions.openNull(RS["BuyEndDate"]));
                                returnXML.FinePrint = SepFunctions.openNull(RS["FinePrint"]);
                                returnXML.BuyEmailID = SepFunctions.toLong(SepFunctions.openNull(RS["BuyEmailID"]));
                                returnXML.ApproveEmailID = SepFunctions.toLong(SepFunctions.openNull(RS["ApproveEmailID"]));
                                returnXML.AdminEmailID = SepFunctions.toLong(SepFunctions.openNull(RS["AdminEmailID"]));
                                returnXML.TwitterHTML = SepFunctions.openNull(RS["TwitterHTML"]);
                                returnXML.CategoryURL = SepFunctions.openNull(RS["CategoryURL"]);
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.SavePercent = Strings.FormatNumber(SepFunctions.toDouble(SepFunctions.openNull(RS["SalePrice"])) / SepFunctions.toDouble(SepFunctions.openNull(RS["RegularPrice"])) * 100, 0) + "%";
                                returnXML.SavePrice = SepFunctions.Format_Currency(SepFunctions.toDouble(SepFunctions.openNull(RS["RegularPrice"])) - SepFunctions.toDouble(SepFunctions.openNull(RS["SalePrice"])));
                            }

                            using (var cmd2 = new SqlCommand("SELECT Count(PurchaseID) AS Counter FROM VouchersPurchased WHERE VoucherID='" + SepFunctions.FixWord(SepFunctions.openNull(RS["VoucherID"])) + "'", conn))
                            {
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    if (RS2.HasRows)
                                    {
                                        RS2.Read();
                                        returnXML.DealsBought = SepFunctions.toLong(SepFunctions.openNull(RS2["Counter"]));
                                    }
                                }
                            }
                        }
                    }
                }

                if (returnXML.VoucherID > 0)
                {
                    var sImageFolder = SepFunctions.GetInstallFolder(true);
                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE UniqueID=@VoucherID AND ModuleID='65' AND isTemp='0' AND Approved='1' AND ControlID='LogoImageUpload'", conn))
                    {
                        cmd.Parameters.AddWithValue("@VoucherID", VoucherID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.LogoImage = sImageFolder + "spadmin/show_image.aspx?ModuleID=5&Size=thumb&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Vouchers the save.
        /// </summary>
        /// <param name="VoucherID">The voucher identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="BuyTitle">The buy title.</param>
        /// <param name="ShortDescription">The short description.</param>
        /// <param name="LongDescription">The long description.</param>
        /// <param name="SalePrice">The sale price.</param>
        /// <param name="RegularPrice">The regular price.</param>
        /// <param name="Quantity">The quantity.</param>
        /// <param name="MaxNumPerUser">The maximum number per user.</param>
        /// <param name="RedemptionStart">The redemption start.</param>
        /// <param name="RedemptionEnd">The redemption end.</param>
        /// <param name="PurchaseCode">The purchase code.</param>
        /// <param name="BusinessName">Name of the business.</param>
        /// <param name="Address">The address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="ZipCode">The zip code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="ContactEmail">The contact email.</param>
        /// <param name="ContactName">Name of the contact.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="Disclaimer">The disclaimer.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="TotalPurchases">The total purchases.</param>
        /// <param name="BuyEndDate">The buy end date.</param>
        /// <param name="FinePrint">The fine print.</param>
        /// <param name="BuyEmailID">The buy email identifier.</param>
        /// <param name="ApproveEmailID">The approve email identifier.</param>
        /// <param name="AdminEmailID">The admin email identifier.</param>
        /// <param name="TwitterHTML">The twitter HTML.</param>
        /// <param name="CategoryURL">The category URL.</param>
        /// <returns>System.Int32.</returns>
        public static int Voucher_Save(long VoucherID, long CatID, string BuyTitle, string ShortDescription, string LongDescription, decimal SalePrice, decimal RegularPrice, long Quantity, long MaxNumPerUser, DateTime RedemptionStart, DateTime RedemptionEnd, string PurchaseCode, string BusinessName, string Address, string City, string State, string ZipCode, string Country, string ContactEmail, string ContactName, string PhoneNumber, string Disclaimer, long PortalID, string UserID, long TotalPurchases, DateTime BuyEndDate, string FinePrint, long BuyEmailID, long ApproveEmailID, long AdminEmailID, string TwitterHTML, string CategoryURL)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            if (!Information.IsDate(BuyEndDate))
            {
                if (SepFunctions.toLong(SepFunctions.Setup(65, "VoucherExpireDays")) > 0)
                {
                    BuyEndDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Day, SepFunctions.toInt(SepFunctions.Setup(65, "VoucherExpireDays")), DateTime.Now);
                }
                else
                {
                    BuyEndDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 30, DateTime.Now);
                }
            }

            if (!Information.IsDate(RedemptionEnd))
            {
                if (SepFunctions.toLong(SepFunctions.Setup(65, "VoucherExpireDays")) > 0)
                {
                    RedemptionEnd = DateAndTime.DateAdd(DateAndTime.DateInterval.Day, SepFunctions.toInt(SepFunctions.Setup(65, "VoucherExpireDays")), DateTime.Now);
                }
                else
                {
                    RedemptionEnd = DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 30, DateTime.Now);
                }
            }

            if (!Information.IsDate(RedemptionStart))
            {
                RedemptionStart = DateTime.Now;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (VoucherID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Vouchers WHERE VoucherID=@VoucherID", conn))
                    {
                        cmd.Parameters.AddWithValue("@VoucherID", VoucherID);
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
                    VoucherID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Vouchers SET CatID=@CatID, BuyTitle=@BuyTitle, ShortDescription=@ShortDescription, LongDescription=@LongDescription, SalePrice=@SalePrice, RegularPrice=@RegularPrice, Quantity=@Quantity, MaxNumPerUser=@MaxNumPerUser, RedemptionStart=@RedemptionStart, RedemptionEnd=@RedemptionEnd, PurchaseCode=@PurchaseCode, BusinessName=@BusinessName, Address=@Address, City=@City, State=@State, ZipCode=@ZipCode, Country=@Country, ContactEmail=@ContactEmail, ContactName=@ContactName, PhoneNumber=@PhoneNumber, Disclaimer=@Disclaimer, PortalID=@PortalID, UserID=@UserID, TotalPurchases=@TotalPurchases, BuyEndDate=@BuyEndDate, FinePrint=@FinePrint, BuyEmailID=@BuyEmailID, ApproveEmailID=@ApproveEmailID, AdminEmailID=@AdminEmailID, TwitterHTML=@TwitterHTML, CategoryURL=@CategoryURL WHERE VoucherID=@VoucherID";
                }
                else
                {
                    SqlStr = "INSERT INTO Vouchers (VoucherID, CatID, BuyTitle, ShortDescription, LongDescription, SalePrice, RegularPrice, Quantity, MaxNumPerUser, RedemptionStart, RedemptionEnd, PurchaseCode, BusinessName, Address, City, State, ZipCode, Country, ContactEmail, ContactName, PhoneNumber, Disclaimer, PortalID, UserID, TotalPurchases, BuyEndDate, FinePrint, BuyEmailID, ApproveEmailID, AdminEmailID, TwitterHTML, CategoryURL, DatePosted, Status) VALUES (@VoucherID, @CatID, @BuyTitle, @ShortDescription, @LongDescription, @SalePrice, @RegularPrice, @Quantity, @MaxNumPerUser, @RedemptionStart, @RedemptionEnd, @PurchaseCode, @BusinessName, @Address, @City, @State, @ZipCode, @Country, @ContactEmail, @ContactName, @PhoneNumber, @Disclaimer, @PortalID, @UserID, @TotalPurchases, @BuyEndDate, @FinePrint, @BuyEmailID, @ApproveEmailID, @AdminEmailID, @TwitterHTML, @CategoryURL, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@VoucherID", VoucherID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@BuyTitle", BuyTitle);
                    cmd.Parameters.AddWithValue("@ShortDescription", ShortDescription);
                    cmd.Parameters.AddWithValue("@LongDescription", LongDescription);
                    cmd.Parameters.AddWithValue("@SalePrice", SalePrice);
                    cmd.Parameters.AddWithValue("@RegularPrice", RegularPrice);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@MaxNumPerUser", MaxNumPerUser);
                    cmd.Parameters.AddWithValue("@RedemptionStart", RedemptionStart);
                    cmd.Parameters.AddWithValue("@RedemptionEnd", RedemptionEnd);
                    cmd.Parameters.AddWithValue("@PurchaseCode", PurchaseCode);
                    cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@ContactEmail", ContactEmail);
                    cmd.Parameters.AddWithValue("@ContactName", ContactName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    cmd.Parameters.AddWithValue("@Disclaimer", Disclaimer);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@TotalPurchases", TotalPurchases);
                    cmd.Parameters.AddWithValue("@BuyEndDate", BuyEndDate);
                    cmd.Parameters.AddWithValue("@FinePrint", FinePrint);
                    cmd.Parameters.AddWithValue("@BuyEmailID", BuyEmailID);
                    cmd.Parameters.AddWithValue("@ApproveEmailID", ApproveEmailID);
                    cmd.Parameters.AddWithValue("@AdminEmailID", AdminEmailID);
                    cmd.Parameters.AddWithValue("@TwitterHTML", TwitterHTML);
                    cmd.Parameters.AddWithValue("@CategoryURL", CategoryURL);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 65, Strings.ToString(VoucherID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Voucher", "Voucher", string.Empty);

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM Vouchers WHERE VoucherID=@VoucherID", conn))
                    {
                        cmd.Parameters.AddWithValue("@VoucherID", VoucherID);
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
                        SepFunctions.Update_Change_Log(65, Strings.ToString(VoucherID), BuyTitle, Strings.ToString(writeLog));
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
    }
}