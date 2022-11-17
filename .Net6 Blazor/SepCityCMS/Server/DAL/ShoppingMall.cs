// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ShoppingMall.cs" company="SepCity, Inc.">
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
    /// Class ShoppingMall.
    /// </summary>
    public static class ShoppingMall
    {
        /// <summary>
        /// Gets the shop products.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="showSalesOnly">if set to <c>true</c> [show sales only].</param>
        /// <param name="ShowOnlyAvailable">if set to <c>true</c> [show only available].</param>
        /// <param name="hideImportedItems">if set to <c>true</c> [hide imported items].</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>List&lt;Models.ShopProducts&gt;.</returns>
        public static List<ShopProducts> GetShopProducts(string SortExpression = "ProductName", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1, bool showSalesOnly = true, bool ShowOnlyAvailable = false, bool hideImportedItems = false, string StartDate = "", string UserID = "")
        {
            var lShopProducts = new List<ShopProducts>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Mod.ProductName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            wClause = "Mod.ModuleID='41'";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Mod.ProductName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (showSalesOnly)
            {
                wClause += " AND Mod.SalePrice <> '0'";
            }

            if (hideImportedItems)
            {
                wClause += " AND (Mod.ImportID IS NULL OR Mod.ImportID='')";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            if (ShowOnlyAvailable)
            {
                wClause += " AND Mod.Status=1";
            }
            else
            {
                wClause += " AND Mod.Status <> -1";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.ProductID,Mod.CatID,Mod.ProductName,Mod.ShortDesc,Mod.SalePrice,Mod.UnitPrice,Mod.RecurringPrice,Mod.RecurringCycle,Mod.Handling,Mod.StoreID,Mod.DatePosted,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='41' AND UniqueID=Mod.ProductID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID FROM ShopProducts AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dShopProducts = new Models.ShopProducts { ProductID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductID"])) };
                    dShopProducts.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dShopProducts.ProductName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductName"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["ShortDesc"])))
                    {
                        dShopProducts.ShortDescription = SepFunctions.openNull(ds.Tables[0].Rows[i]["ShortDesc"]);
                    }
                    else
                    {
                        dShopProducts.ShortDescription = string.Empty;
                    }

                    dShopProducts.SalePrice = SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"]));
                    dShopProducts.UnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"]));
                    if (SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"])) < SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"])) && SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"])) > 0)
                    {
                        dShopProducts.DisplayPrice = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"]));
                        dShopProducts.LongPrice = SepFunctions.Pricing_Long_Price(SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["SalePrice"])), SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"])), SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringCycle"]));
                    }
                    else
                    {
                        dShopProducts.DisplayPrice = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"]));
                        dShopProducts.LongPrice = SepFunctions.Pricing_Long_Price(SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"])), SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"])), SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringCycle"]));
                    }

                    dShopProducts.RecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringCycle"])))
                    {
                        dShopProducts.RecurringCycle = SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringCycle"]);
                    }
                    else
                    {
                        dShopProducts.RecurringCycle = "1m";
                    }

                    dShopProducts.Handling = SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["Handling"]));
                    dShopProducts.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dShopProducts.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=41&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        dShopProducts.DefaultPicture = sImageFolder + "images/public/no-photo.jpg";
                    }

                    dShopProducts.StoreID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreID"]));
                    lShopProducts.Add(dShopProducts);
                }
            }

            return lShopProducts;
        }

        /// <summary>
        /// Gets the shop shipping methods.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>List&lt;Models.ShopShippingMethods&gt;.</returns>
        public static List<ShopShippingMethods> GetShopShippingMethods(string SortExpression = "MethodName", string SortDirection = "ASC", string searchWords = "", long StoreID = 0)
        {
            var lShopShipping = new List<ShopShippingMethods>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "MethodName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND MethodName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (StoreID > 0)
            {
                wClause += " AND StoreID='" + SepFunctions.FixWord(Strings.ToString(StoreID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT MethodID, MethodName, Carrier, ShippingService FROM ShopShippingMethods WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dShopShipping = new Models.ShopShippingMethods { MethodID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["MethodID"])) };
                    dShopShipping.MethodName = SepFunctions.openNull(ds.Tables[0].Rows[i]["MethodName"]);
                    dShopShipping.Carrier = SepFunctions.openNull(ds.Tables[0].Rows[i]["Carrier"]);
                    dShopShipping.ShippingService = SepFunctions.openNull(ds.Tables[0].Rows[i]["ShippingService"]);
                    lShopShipping.Add(dShopShipping);
                }
            }

            return lShopShipping;
        }

        /// <summary>
        /// Gets the shop stores.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.ShopStores&gt;.</returns>
        public static List<ShopStores> GetShopStores(string SortExpression = "StoreName", string SortDirection = "ASC", string searchWords = "")
        {
            var lShopStores = new List<ShopStores>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "StoreName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND StoreName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ShopStores.StoreID, ShopStores.StoreName, ShopStores.CompanyName, Members.UserName FROM ShopStores, Members WHERE ShopStores.UserID=Members.UserID " + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dShopStores = new Models.ShopStores { StoreID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreID"])) };
                    dShopStores.StoreName = SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreName"]);
                    dShopStores.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dShopStores.CompanyName = SepFunctions.openNull(ds.Tables[0].Rows[i]["CompanyName"]);
                    lShopStores.Add(dShopStores);
                }
            }

            return lShopStores;
        }

        /// <summary>
        /// Gets the shop wholesale2b.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns>List&lt;Models.Wholesale2bFeeds&gt;.</returns>
        public static List<Wholesale2bFeeds> GetShopWholesale2b(string SortExpression = "FeedName", string SortDirection = "ASC")
        {
            var lWholesale2bFeeds = new List<Wholesale2bFeeds>();

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "FeedName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Wholesale2b_Feeds ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dWholesale2bFeeds = new Models.Wholesale2bFeeds { FeedID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FeedID"])) };
                    dWholesale2bFeeds.FeedName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FeedName"]);
                    dWholesale2bFeeds.FeedURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["FeedURL"]);
                    lWholesale2bFeeds.Add(dWholesale2bFeeds);
                }
            }

            return lWholesale2bFeeds;
        }

        /// <summary>
        /// Products the delete.
        /// </summary>
        /// <param name="ProductIDs">The product i ds.</param>
        /// <returns>System.String.</returns>
        public static string Product_Delete(string ProductIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrProductIDs = Strings.Split(ProductIDs, ",");

                if (arrProductIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrProductIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ShopProducts SET Status='-1', DateDeleted=@DateDeleted WHERE ProductID=@ProductID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", arrProductIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(41, arrProductIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Product(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Products the get.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.ShopProducts.</returns>
        public static ShopProducts Product_Get(long ProductID, long ChangeID = 0)
        {
            var returnXML = new Models.ShopProducts();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ProductID=@ProductID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
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
                                returnXML.ProductID = SepFunctions.toLong(SepFunctions.openNull(RS["ProductID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.ProductName = SepFunctions.openNull(RS["ProductName"]);
                                returnXML.FullDescription = SepFunctions.openNull(RS["Description"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.UnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]));
                                returnXML.SalePrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["SalePrice"]));
                                returnXML.RecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"]));
                                returnXML.RecurringCycle = SepFunctions.openNull(RS["RecurringCycle"]);
                                returnXML.AssocID = SepFunctions.openNull(RS["AssocID"]);
                                returnXML.NewsletID = SepFunctions.openNull(RS["NewsletID"]);
                                returnXML.ItemWeight = SepFunctions.toDecimal(SepFunctions.openNull(RS["ItemWeight"]));
                                returnXML.WeightType = SepFunctions.openNull(RS["WeightType"]);
                                returnXML.DimH = SepFunctions.toDecimal(SepFunctions.openNull(RS["DimH"]));
                                returnXML.DimW = SepFunctions.toDecimal(SepFunctions.openNull(RS["DimW"]));
                                returnXML.DimL = SepFunctions.toDecimal(SepFunctions.openNull(RS["DimL"]));
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.UseInventory = SepFunctions.toBoolean(SepFunctions.openNull(RS["UseInventory"]));
                                returnXML.Inventory = SepFunctions.toInt(SepFunctions.openNull(RS["Inventory"]));
                                returnXML.MinOrderQty = SepFunctions.toInt(SepFunctions.openNull(RS["MinQuantity"]));
                                returnXML.MaxOrderQty = SepFunctions.toInt(SepFunctions.openNull(RS["MaxQuantity"]));
                                returnXML.ShippingOption = SepFunctions.openNull(RS["ShipOption"]);
                                returnXML.TaxExempt = SepFunctions.toBoolean(SepFunctions.openNull(RS["TaxExempt"]));
                                returnXML.Handling = SepFunctions.toDecimal(SepFunctions.openNull(RS["Handling"]));
                                returnXML.Manufacturer = SepFunctions.openNull(RS["Manufacture"]);
                                returnXML.ModelNumber = SepFunctions.openNull(RS["ModelNumber"]);
                                returnXML.SKU = SepFunctions.openNull(RS["SKU"]);
                                returnXML.ShortDescription = SepFunctions.openNull(RS["ShortDesc"]);
                                returnXML.AffiliateUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["AffiliateUnitPrice"]));
                                returnXML.AffiliateRecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["AffiliateRecurringPrice"]));
                                returnXML.ExcludeAffiliate = SepFunctions.toBoolean(SepFunctions.openNull(RS["ExcludeAffiliate"]));
                                returnXML.StoreID = SepFunctions.toLong(SepFunctions.openNull(RS["StoreID"]));
                                returnXML.ModuleID = SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Products the get by import identifier.
        /// </summary>
        /// <param name="ImportID">The import identifier.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>Models.ShopProducts.</returns>
        public static ShopProducts Product_Get_By_ImportID(string ImportID, long FeedID)
        {
            var returnXML = new Models.ShopProducts();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ImportID=@ImportID AND FeedID=@FeedID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ImportID", ImportID);
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ProductID = SepFunctions.toLong(SepFunctions.openNull(RS["ProductID"]));
                            returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                            returnXML.ProductName = SepFunctions.openNull(RS["ProductName"]);
                            returnXML.FullDescription = SepFunctions.openNull(RS["Description"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.UnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]));
                            returnXML.SalePrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["SalePrice"]));
                            returnXML.RecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"]));
                            returnXML.RecurringCycle = SepFunctions.openNull(RS["RecurringCycle"]);
                            returnXML.AssocID = SepFunctions.openNull(RS["AssocID"]);
                            returnXML.NewsletID = SepFunctions.openNull(RS["NewsletID"]);
                            returnXML.ItemWeight = SepFunctions.toDecimal(SepFunctions.openNull(RS["ItemWeight"]));
                            returnXML.WeightType = SepFunctions.openNull(RS["WeightType"]);
                            returnXML.DimH = SepFunctions.toDecimal(SepFunctions.openNull(RS["DimH"]));
                            returnXML.DimW = SepFunctions.toDecimal(SepFunctions.openNull(RS["DimW"]));
                            returnXML.DimL = SepFunctions.toDecimal(SepFunctions.openNull(RS["DimL"]));
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.UseInventory = SepFunctions.toBoolean(SepFunctions.openNull(RS["UseInventory"]));
                            returnXML.Inventory = SepFunctions.toInt(SepFunctions.openNull(RS["Inventory"]));
                            returnXML.ShippingOption = SepFunctions.openNull(RS["ShipOption"]);
                            returnXML.TaxExempt = SepFunctions.toBoolean(SepFunctions.openNull(RS["TaxExempt"]));
                            returnXML.Handling = SepFunctions.toDecimal(SepFunctions.openNull(RS["Handling"]));
                            returnXML.Manufacturer = SepFunctions.openNull(RS["Manufacture"]);
                            returnXML.ModelNumber = SepFunctions.openNull(RS["ModelNumber"]);
                            returnXML.SKU = SepFunctions.openNull(RS["SKU"]);
                            returnXML.ShortDescription = SepFunctions.openNull(RS["ShortDesc"]);
                            returnXML.AffiliateUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["AffiliateUnitPrice"]));
                            returnXML.AffiliateRecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["AffiliateRecurringPrice"]));
                            returnXML.ExcludeAffiliate = SepFunctions.toBoolean(SepFunctions.openNull(RS["ExcludeAffiliate"]));
                            returnXML.ModuleID = SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]));
                            returnXML.ImportID = SepFunctions.openNull(RS["ImportID"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Products the save.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="ProductName">Name of the product.</param>
        /// <param name="ShortDesc">The short desc.</param>
        /// <param name="Description">The description.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="UnitPrice">The unit price.</param>
        /// <param name="RecurringPrice">The recurring price.</param>
        /// <param name="RecurringCycle">The recurring cycle.</param>
        /// <param name="SalePrice">The sale price.</param>
        /// <param name="AssocID">The assoc identifier.</param>
        /// <param name="NewsletID">The newslet identifier.</param>
        /// <param name="ItemWeight">The item weight.</param>
        /// <param name="WeightType">Type of the weight.</param>
        /// <param name="DimH">The dim h.</param>
        /// <param name="DimW">The dim w.</param>
        /// <param name="DimL">The dim l.</param>
        /// <param name="UseInventory">if set to <c>true</c> [use inventory].</param>
        /// <param name="Inventory">The inventory.</param>
        /// <param name="MinQuantity">The minimum quantity.</param>
        /// <param name="MaxQuantity">The maximum quantity.</param>
        /// <param name="ShipOption">The ship option.</param>
        /// <param name="TaxExempt">if set to <c>true</c> [tax exempt].</param>
        /// <param name="Handling">The handling.</param>
        /// <param name="Manufacturer">The manufacturer.</param>
        /// <param name="ModelNumber">The model number.</param>
        /// <param name="SKU">The sku.</param>
        /// <param name="AffiliateUnitPrice">The affiliate unit price.</param>
        /// <param name="AffiliateRecurringPrice">The affiliate recurring price.</param>
        /// <param name="ExcludeAffiliate">if set to <c>true</c> [exclude affiliate].</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="DeleteCustomFieldIds">The delete custom field ids.</param>
        /// <param name="DeleteCustomFieldOptions">The delete custom field options.</param>
        /// <param name="CustomFieldData">The custom field data.</param>
        /// <param name="ImportID">The import identifier.</param>
        /// <param name="Status">The status.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Product_Save(long ProductID, long CatID, string UserID, long StoreID, string ProductName, string ShortDesc, string Description, long PortalID, decimal UnitPrice, decimal RecurringPrice, string RecurringCycle, decimal SalePrice, string AssocID, string NewsletID, decimal ItemWeight, string WeightType, decimal DimH, decimal DimW, decimal DimL, bool UseInventory, int Inventory, int MinQuantity, int MaxQuantity, string ShipOption, bool TaxExempt, decimal Handling, string Manufacturer, string ModelNumber, string SKU, decimal AffiliateUnitPrice, decimal AffiliateRecurringPrice, bool ExcludeAffiliate, int ModuleID, string DeleteCustomFieldIds, string DeleteCustomFieldOptions, string CustomFieldData, string ImportID, int Status, long FeedID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ProductID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ProductID=@ProductID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", ProductID);
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
                    ProductID = SepFunctions.GetIdentity();
                }

                if (UnitPrice < SalePrice)
                {
                    UnitPrice = SalePrice;
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ShopProducts SET CatID=@CatID, ProductName=@ProductName, UserID=@UserID, StoreID=@StoreID, Description=@Description, ShortDesc=@ShortDesc, UnitPrice=@UnitPrice, RecurringPrice=@RecurringPrice, RecurringCycle=@RecurringCycle, SalePrice=@SalePrice, AssocID=@AssocID, NewsletID=@NewsletID, ItemWeight=@ItemWeight, WeightType=@WeightType, DimH=@DimH, DimW=@DimW, DimL=@DimL, UseInventory=@UseInventory, Inventory=@Inventory, MinQuantity=@MinQuantity, MaxQuantity=@MaxQuantity, ShipOption=@ShipOption, TaxExempt=@TaxExempt, Handling=@Handling, Manufacture=@Manufacture, ModelNumber=@ModelNumber, SKU=@SKU, AffiliateUnitPrice=@AffiliateUnitPrice, AffiliateRecurringPrice=@AffiliateRecurringPrice, ExcludeAffiliate=@ExcludeAffiliate, ModuleID=@ModuleID, ImportID=@ImportID, FeedID=@FeedID, Status=@Status WHERE ProductID=@ProductID";
                }
                else
                {
                    SqlStr = "INSERT INTO ShopProducts (ProductID, CatID, ProductName, UserID, StoreID, Description, ShortDesc, PortalID, UnitPrice, RecurringPrice, RecurringCycle, SalePrice, AssocID, NewsletID, ItemWeight, WeightType, DimH, DimW, DimL, UseInventory, Inventory, MinQuantity, MaxQuantity, ShipOption, TaxExempt, Handling, Manufacture, ModelNumber, SKU, AffiliateUnitPrice, AffiliateRecurringPrice, ExcludeAffiliate, DatePosted, ModuleID, ImportID, FeedID, Status) VALUES (@ProductID, @CatID, @ProductName, @UserID, @StoreID, @Description, @ShortDesc, @PortalID, @UnitPrice, @RecurringPrice, @RecurringCycle, @SalePrice, @AssocID, @NewsletID, @ItemWeight, @WeightType, @DimH, @DimW, @DimL, @UseInventory, @Inventory, @MinQuantity, @MaxQuantity, @ShipOption, @TaxExempt, @Handling, @Manufacture, @ModelNumber, @SKU, @AffiliateUnitPrice, @AffiliateRecurringPrice, @ExcludeAffiliate, @DatePosted, @ModuleID, @ImportID, @FeedID, @Status)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@StoreID", StoreID);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@ShortDesc", ShortDesc);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd.Parameters.AddWithValue("@RecurringPrice", RecurringPrice);
                    cmd.Parameters.AddWithValue("@RecurringCycle", RecurringCycle);
                    cmd.Parameters.AddWithValue("@SalePrice", SalePrice);
                    cmd.Parameters.AddWithValue("@AssocID", AssocID);
                    cmd.Parameters.AddWithValue("@NewsletID", NewsletID);
                    cmd.Parameters.AddWithValue("@ItemWeight", ItemWeight);
                    cmd.Parameters.AddWithValue("@WeightType", WeightType);
                    cmd.Parameters.AddWithValue("@DimH", DimH);
                    cmd.Parameters.AddWithValue("@DimW", DimW);
                    cmd.Parameters.AddWithValue("@DimL", DimL);
                    cmd.Parameters.AddWithValue("@UseInventory", UseInventory);
                    cmd.Parameters.AddWithValue("@Inventory", Inventory);
                    cmd.Parameters.AddWithValue("@MinQuantity", MinQuantity);
                    cmd.Parameters.AddWithValue("@MaxQuantity", MaxQuantity);
                    cmd.Parameters.AddWithValue("@ShipOption", ShipOption);
                    cmd.Parameters.AddWithValue("@TaxExempt", TaxExempt);
                    cmd.Parameters.AddWithValue("@Handling", Handling);
                    cmd.Parameters.AddWithValue("@Manufacture", Manufacturer);
                    cmd.Parameters.AddWithValue("@ModelNumber", ModelNumber);
                    cmd.Parameters.AddWithValue("@SKU", SKU);
                    cmd.Parameters.AddWithValue("@AffiliateUnitPrice", AffiliateUnitPrice);
                    cmd.Parameters.AddWithValue("@AffiliateRecurringPrice", AffiliateRecurringPrice);
                    cmd.Parameters.AddWithValue("@ExcludeAffiliate", ExcludeAffiliate);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    cmd.Parameters.AddWithValue("@ImportID", ImportID);
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.ExecuteNonQuery();
                }

                if (string.IsNullOrWhiteSpace(ImportID) && FeedID == 0)
                {
                    intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 41, Strings.ToString(ProductID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "ShopMall", "Shopping Mall", string.Empty);
                }

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ProductID=@ProductID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", ProductID);
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
                        SepFunctions.Update_Change_Log(41, Strings.ToString(ProductID), ProductName, Strings.ToString(writeLog));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(CustomFieldData))
            {
                var arrCustom = Strings.Split(CustomFieldData, "|$$|");
                Array.Resize(ref arrCustom, 6);
                var arrFieldIDs = Strings.Split(arrCustom[0], "|%|");
                var arrFieldNames = Strings.Split(arrCustom[1], "|%|");
                var arrAnswerTypes = Strings.Split(arrCustom[2], "|%|");
                var arrCustomFieldOptions = Strings.Split(arrCustom[3], "|%|");
                var arrOrders = Strings.Split(arrCustom[4], "|%|");
                var arrRequires = Strings.Split(arrCustom[5], "|%|");

                if (arrFieldIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFieldIDs); i++)
                    {
                        CustomFields.Field_Save(SepFunctions.toLong(arrFieldIDs[i]), 0, arrFieldNames[i], arrAnswerTypes[i], arrRequires[i] == "1" ? true : false, false, SepFunctions.toLong(arrOrders[i]), "|41|", "|" + PortalID + "|", "|" + ProductID + "|");
                        switch (arrAnswerTypes[i])
                        {
                            case "DropdownM":
                            case "DropdownS":
                            case "Radio":
                            case "Checkbox":
                                var arrOptions = Strings.Split(arrCustomFieldOptions[i], "|%%|");

                                if (arrOptions != null)
                                {
                                    for (var j = 0; j <= Information.UBound(arrOptions); j++)
                                    {
                                        var arrOptions2 = Strings.Split(arrOptions[j], "|!|");
                                        Array.Resize(ref arrOptions2, 6);
                                        if (arrOptions2[0] == arrFieldIDs[i])
                                        {
                                            CustomFields.Field_Option_Save(SepFunctions.toLong(arrOptions2[1]), SepFunctions.toLong(arrFieldIDs[i]), arrOptions2[2], arrOptions2[2], SepFunctions.toDecimal(arrOptions2[4]), SepFunctions.toDecimal(arrOptions2[5]), SepFunctions.toLong(arrOptions2[3]));
                                        }
                                    }
                                }

                                break;
                        }
                    }
                }
            }

            // === Save Custom Field Information ==============
            if (!string.IsNullOrWhiteSpace(DeleteCustomFieldIds) || !string.IsNullOrWhiteSpace(DeleteCustomFieldOptions))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    var arrCustomIds = Strings.Split(DeleteCustomFieldIds, "|%|");

                    if (arrCustomIds != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrCustomIds); i++)
                        {
                            using (var cmd = new SqlCommand("DELETE FROM CustomFields WHERE ModuleIDs LIKE '|41|' AND FieldID=@FieldID", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", arrCustomIds[i]);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM CustomFieldOptions WHERE FieldID=@FieldID", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", arrCustomIds[i]);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM CustomFieldUsers WHERE ModuleID='41' AND FieldID=@FieldID", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", arrCustomIds[i]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    var arrDeleteCustomFieldOptions = Strings.Split(DeleteCustomFieldOptions, "|%|");
                    if (arrDeleteCustomFieldOptions != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrCustomIds); i++)
                        {
                            using (var cmd = new SqlCommand("DELETE FROM CustomFieldOptions WHERE OptionID=@OptionID", conn))
                            {
                                cmd.Parameters.AddWithValue("@OptionID", arrDeleteCustomFieldOptions[i]);
                                cmd.ExecuteNonQuery();
                            }
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
        /// Shippings the delete.
        /// </summary>
        /// <param name="MethodIDs">The method i ds.</param>
        /// <returns>System.String.</returns>
        public static string Shipping_Delete(string MethodIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrMethodIDs = Strings.Split(MethodIDs, ",");

                if (arrMethodIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrMethodIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ShopShippingMethods SET Status='-1', DateDeleted=@DateDeleted WHERE MethodID=@MethodID", conn))
                        {
                            cmd.Parameters.AddWithValue("@MethodID", arrMethodIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
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

            sReturn = SepFunctions.LangText("Shipping Method(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Shippings the get.
        /// </summary>
        /// <param name="MethodID">The method identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.ShopShippingMethods.</returns>
        public static ShopShippingMethods Shipping_Get(long MethodID, string UserID)
        {
            var returnXML = new Models.ShopShippingMethods();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ShopShippingMethods WHERE MethodID=@MethodID AND StoreID=@StoreID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@MethodID", MethodID);
                    cmd.Parameters.AddWithValue("@StoreID", Store_Get_StoreID(UserID));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.MethodID = SepFunctions.toLong(SepFunctions.openNull(RS["MethodID"]));
                            returnXML.MethodName = SepFunctions.openNull(RS["MethodName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.Carrier = SepFunctions.openNull(RS["Carrier"]);
                            returnXML.ShippingService = SepFunctions.openNull(RS["ShippingService"]);
                            returnXML.DeliveryTime = SepFunctions.openNull(RS["DeliveryTime"]);
                            returnXML.WeightLimit = SepFunctions.openNull(RS["WeightLimit"]);
                            returnXML.StoreID = SepFunctions.toLong(SepFunctions.openNull(RS["StoreID"]));
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Shippings the save.
        /// </summary>
        /// <param name="MethodID">The method identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="MethodName">Name of the method.</param>
        /// <param name="Description">The description.</param>
        /// <param name="Carrier">The carrier.</param>
        /// <param name="ShippingService">The shipping service.</param>
        /// <param name="DeliveryTime">The delivery time.</param>
        /// <param name="WeightLimit">The weight limit.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Shipping_Save(long MethodID, long StoreID, string MethodName, string Description, string Carrier, string ShippingService, string DeliveryTime, string WeightLimit, long PortalID)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (MethodID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT MethodID FROM ShopShippingMethods WHERE MethodID=@MethodID", conn))
                    {
                        cmd.Parameters.AddWithValue("@MethodID", MethodID);
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
                    MethodID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ShopShippingMethods SET MethodName=@MethodName, Description=@Description, Carrier=@Carrier, ShippingService=@ShippingService, DeliveryTime=@DeliveryTime, WeightLimit=@WeightLimit, PortalID=@PortalID, Status=@Status WHERE MethodID=@MethodID";
                }
                else
                {
                    SqlStr = "INSERT INTO ShopShippingMethods (MethodID, MethodName, Description, Carrier, ShippingService, DeliveryTime, WeightLimit, StoreID, PortalID, Status) VALUES (@MethodID, @MethodName, @Description, @Carrier, @ShippingService, @DeliveryTime, @WeightLimit, @StoreID, @PortalID, @Status)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@MethodID", MethodID);
                    cmd.Parameters.AddWithValue("@MethodName", MethodName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Carrier", Carrier);
                    cmd.Parameters.AddWithValue("@ShippingService", ShippingService);
                    cmd.Parameters.AddWithValue("@DeliveryTime", DeliveryTime);
                    cmd.Parameters.AddWithValue("@WeightLimit", WeightLimit);
                    cmd.Parameters.AddWithValue("@StoreID", StoreID);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Shipping Method has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Shipping Method has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Stores the delete.
        /// </summary>
        /// <param name="StoreIDs">The store i ds.</param>
        /// <returns>System.String.</returns>
        public static string Store_Delete(string StoreIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrStoreIDs = Strings.Split(StoreIDs, ",");

                if (arrStoreIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrStoreIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ShopStores SET Status='-1', DateDeleted=@DateDeleted WHERE StoreID=@StoreID", conn))
                        {
                            cmd.Parameters.AddWithValue("@StoreID", arrStoreIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
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

            sReturn = SepFunctions.LangText("Stores(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Stores the get.
        /// </summary>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>Models.ShopStores.</returns>
        public static ShopStores Store_Get(long StoreID)
        {
            var returnXML = new Models.ShopStores();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ShopStores WHERE StoreID=@StoreID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@StoreID", StoreID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.StoreID = SepFunctions.toLong(SepFunctions.openNull(RS["StoreID"]));
                            returnXML.StoreName = SepFunctions.openNull(RS["StoreName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.CompanyName = SepFunctions.openNull(RS["CompanyName"]);
                            returnXML.StreetAddress = SepFunctions.openNull(RS["StreetAddress"]);
                            returnXML.City = SepFunctions.openNull(RS["City"]);
                            returnXML.State = SepFunctions.openNull(RS["State"]);
                            returnXML.Country = SepFunctions.openNull(RS["Country"]);
                            returnXML.PostalCode = SepFunctions.openNull(RS["PostalCode"]);
                            returnXML.PhoneNumber = SepFunctions.openNull(RS["PhoneNumber"]);
                            returnXML.FaxNumber = SepFunctions.openNull(RS["FaxNumber"]);
                            returnXML.SiteURL = SepFunctions.openNull(RS["WebsiteURL"]);
                            returnXML.ContactEmail = SepFunctions.openNull(RS["ContactEmail"]);
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Stores the get store identifier.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.Int64.</returns>
        public static long Store_Get_StoreID(string UserID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT StoreID FROM ShopStores WHERE UserID=@UserID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            return SepFunctions.toLong(SepFunctions.openNull(RS["StoreID"]));
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Stores the get user identifier.
        /// </summary>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>System.String.</returns>
        public static string Store_Get_UserID(long StoreID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserID FROM ShopStores WHERE StoreID=@StoreID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@StoreID", StoreID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            return SepFunctions.openNull(RS["UserID"]);
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Stores the save.
        /// </summary>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StoreName">Name of the store.</param>
        /// <param name="Description">The description.</param>
        /// <param name="CompanyName">Name of the company.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="Country">The country.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="FaxNumber">The fax number.</param>
        /// <param name="SiteURL">The site URL.</param>
        /// <param name="ContactEmail">The contact email.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Store_Save(long StoreID, string UserID, string StoreName, string Description, string CompanyName, string StreetAddress, string City, string State, string Country, string PostalCode, string PhoneNumber, string FaxNumber, string SiteURL, string ContactEmail, long PortalID)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (StoreID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT StoreID FROM ShopStores WHERE StoreID=@StoreID", conn))
                    {
                        cmd.Parameters.AddWithValue("@StoreID", StoreID);
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
                    StoreID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ShopStores SET StoreName=@StoreName, Description=@Description, CompanyName=@CompanyName, StreetAddress=@StreetAddress, City=@City, State=@State, Country=@Country, PostalCode=@PostalCode, PhoneNumber=@PhoneNumber, FaxNumber=@FaxNumber, WebsiteURL=@WebsiteURL, ContactEmail=@ContactEmail, UserID=@UserID, PortalID=@PortalID, Status=@Status WHERE StoreID=@StoreID";
                }
                else
                {
                    SqlStr = "INSERT INTO ShopStores (StoreID, StoreName, Description, CompanyName, StreetAddress, City, State, Country, PostalCode, PhoneNumber, FaxNumber, WebsiteURL, ContactEmail, UserID, PortalID, DateAdded, Status) VALUES (@StoreID, @StoreName, @Description, @CompanyName, @StreetAddress, @City, @State, @Country, @PostalCode, @PhoneNumber, @FaxNumber, @WebsiteURL, @ContactEmail, @UserID, @PortalID, @DateAdded, @Status)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@StoreID", StoreID);
                    cmd.Parameters.AddWithValue("@StoreName", StoreName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                    cmd.Parameters.AddWithValue("@StreetAddress", StreetAddress);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    cmd.Parameters.AddWithValue("@FaxNumber", FaxNumber);
                    cmd.Parameters.AddWithValue("@WebsiteURL", SiteURL);
                    cmd.Parameters.AddWithValue("@ContactEmail", ContactEmail);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Store has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Store has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Wholesale2bs the feed delete.
        /// </summary>
        /// <param name="FeedIDs">The feed i ds.</param>
        /// <returns>System.String.</returns>
        public static string Wholesale2b_Feed_Delete(string FeedIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFeedIDs = Strings.Split(FeedIDs, ",");

                if (arrFeedIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFeedIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM BG_Processes WHERE ProcessID=@ProcessID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ProcessID", arrFeedIDs[i]);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM Wholesale2b_Feeds WHERE FeedID=@FeedID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FeedID", arrFeedIDs[i]);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("SELECT CatID FROM Categories WHERE FeedID=@FeedID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FeedID", arrFeedIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    using (var cmd2 = new SqlCommand("DELETE FROM CategoriesModules WHERE CatID=@CatID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS["CatID"]));
                                        cmd2.ExecuteNonQuery();
                                    }

                                    using (var cmd2 = new SqlCommand("DELETE FROM CategoriesPortals WHERE CatID=@CatID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS["CatID"]));
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        using (var cmd = new SqlCommand("DELETE FROM Categories WHERE FeedID=@FeedID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FeedID", arrFeedIDs[i]);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE FeedID=@FeedID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FeedID", arrFeedIDs[i]);
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

            sReturn = SepFunctions.LangText("Feed(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Wholesale2bs the feed get.
        /// </summary>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>Models.Wholesale2bFeeds.</returns>
        public static Wholesale2bFeeds Wholesale2b_Feed_Get(long FeedID)
        {
            var returnXML = new Models.Wholesale2bFeeds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Wholesale2b_Feeds WHERE FeedID=@FeedID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.FeedID = SepFunctions.toLong(SepFunctions.openNull(RS["FeedID"]));
                            returnXML.FeedName = SepFunctions.openNull(RS["FeedName"]);
                            returnXML.FeedURL = SepFunctions.openNull(RS["FeedURL"]);
                            returnXML.AccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                            returnXML.AccessHide = SepFunctions.openBoolean(RS["AccessHide"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.Sharing = SepFunctions.openBoolean(RS["Sharing"]);
                            returnXML.ExcPortalSecurity = SepFunctions.openBoolean(RS["ExcPortalSecurity"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Wholesale2bs the feed save.
        /// </summary>
        /// <param name="FeedID">The feed identifier.</param>
        /// <param name="FeedName">Name of the feed.</param>
        /// <param name="FeedURL">The feed URL.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="AccessKeysHide">if set to <c>true</c> [access keys hide].</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="ShareContent">if set to <c>true</c> [share content].</param>
        /// <param name="ExcludePortalSecurity">if set to <c>true</c> [exclude portal security].</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Wholesale2b_Feed_Save(long FeedID, string FeedName, string FeedURL, string AccessKeys, bool AccessKeysHide, string PortalIDs, bool ShareContent, bool ExcludePortalSecurity, int Status)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (FeedID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT FeedID FROM Wholesale2b_Feeds WHERE FeedID=@FeedID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
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
                    FeedID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Wholesale2b_Feeds SET FeedName=@FeedName, FeedURL=@FeedURL, AccessKeys=@AccessKeys, AccessHide=@AccessHide, ExcPortalSecurity=@ExcPortalSecurity, Sharing=@Sharing, PortalIDs=@PortalIDs, Status=@Status WHERE FeedID=@FeedID";
                }
                else
                {
                    SqlStr = "INSERT INTO Wholesale2b_Feeds (FeedID, FeedName, FeedURL, AccessKeys,  AccessHide, ExcPortalSecurity, Sharing, PortalIDs, DatePosted, Status) VALUES (@FeedID, @FeedName, @FeedURL, @AccessKeys, @AccessHide, @ExcPortalSecurity, @Sharing, @PortalIDs, @DatePosted, @Status)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    cmd.Parameters.AddWithValue("@FeedName", FeedName);
                    cmd.Parameters.AddWithValue("@FeedURL", FeedURL);
                    cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                    cmd.Parameters.AddWithValue("@AccessHide", AccessKeysHide);
                    cmd.Parameters.AddWithValue("@ExcPortalSecurity", ExcludePortalSecurity);
                    cmd.Parameters.AddWithValue("@Sharing", ShareContent);
                    cmd.Parameters.AddWithValue("@PortalIDs", PortalIDs);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.ExecuteNonQuery();
                }

                if (bUpdate)
                {
                    using (var cmd = new SqlCommand("UPDATE BG_Processes SET Status=@Status WHERE ProcessID=@ProcessID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessID", FeedID);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("INSERT INTO BG_Processes (ProcessID, ProcessName, IntervalSeconds, Status, RecurringDays) VALUES(@ProcessID, @ProcessName, @IntervalSeconds, @Status, '1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessID", FeedID);
                        cmd.Parameters.AddWithValue("@ProcessName", "Wholesale2b");
                        cmd.Parameters.AddWithValue("@IntervalSeconds", 5);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Feed has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Feed has been successfully added.");

            return sReturn;
        }
    }
}