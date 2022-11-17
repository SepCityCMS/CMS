// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Invoices.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.Models;
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Invoices.
    /// </summary>
    public static class Invoices
    {
        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="InvoiceID">The invoice identifier.</param>
        /// <param name="RequireUserID">if set to <c>true</c> [require user identifier].</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>List&lt;Models.Invoices&gt;.</returns>
        public static List<Models.Invoices> GetInvoices(string SortExpression = "OrderDate", string SortDirection = "DESC", string searchWords = "", string UserID = "", long InvoiceID = 0, bool RequireUserID = false, long StoreID = 0)
        {
            var lInvoices = new List<Models.Invoices>();

            var wClause = string.Empty;

            double i1Month = 0;
            double i3Month = 0;
            double i6Month = 0;
            double i1Year = 0;
            double RecurringPrice = 0;
            var RecurringCycle = string.Empty;
            var SqlStr = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "OrderDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND INV.InvoiceID LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND INV.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (InvoiceID > 0)
            {
                wClause += " AND INV.InvoiceID='" + SepFunctions.FixWord(Strings.ToString(InvoiceID)) + "'";
            }

            if (StoreID > 0)
            {
                wClause += " AND INV.InvoiceID IN (SELECT InvoiceID FROM Invoices_Products WHERE StoreID='" + SepFunctions.FixWord(Strings.ToString(StoreID)) + "')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    if (RequireUserID)
                    {
                        SqlStr = "SELECT INV.InvoiceID,INV.Status,INV.OrderDate,M.UserID,M.UserName,M.FirstName,M.LastName,(SELECT (Sum((Cast(UnitPrice as money) + Cast(Handling as money)) * Quantity) + Sum((Cast(RecurringPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalPaid,(SELECT (Sum((Cast(RecurringPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalRecurring,(SELECT (Sum((Cast(UnitPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalSetup FROM Invoices AS INV,Members AS M WHERE INV.UserID=M.UserID AND INV.UserID <> '' AND INV.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(UserID))
                        {
                            SqlStr = "SELECT INV.InvoiceID,INV.Status,INV.OrderDate,M.UserID,M.UserName,M.FirstName,M.LastName,(SELECT (Sum((Cast(UnitPrice as money) + Cast(Handling as money)) * Quantity) + Sum((Cast(RecurringPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalPaid,(SELECT (Sum((Cast(RecurringPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalRecurring,(SELECT (Sum((Cast(UnitPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalSetup FROM Invoices AS INV,Members AS M WHERE INV.UserID=M.UserID AND INV.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection;
                        }
                        else
                        {
                            SqlStr = "SELECT INV.UserID,INV.InvoiceID,INV.Status,INV.OrderDate,(SELECT (Sum((Cast(UnitPrice as money) + Cast(Handling as money)) * Quantity) + Sum((Cast(RecurringPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalPaid,(SELECT (Sum((Cast(RecurringPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalRecurring,(SELECT (Sum((Cast(UnitPrice as money) + Cast(Handling as money)) * Quantity)) FROM Invoices_Products WHERE InvoiceID=INV.InvoiceID) AS TotalSetup FROM Invoices AS INV WHERE INV.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection;
                        }
                    }

                    using (var cmd = new SqlCommand(SqlStr, conn))
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

                    var dInvoices = new Models.Invoices { InvoiceID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["InvoiceID"])) };
                    if (!string.IsNullOrWhiteSpace(UserID))
                    {
                        dInvoices.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                        dInvoices.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                        dInvoices.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    }
                    else
                    {
                        dInvoices.FirstName = SepFunctions.GetUserInformation("FirstName", SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                        dInvoices.LastName = SepFunctions.GetUserInformation("LastName", SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                        dInvoices.UserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    }

                    dInvoices.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    switch (SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"])))
                    {
                        case 1:
                            dInvoices.StatusText = SepFunctions.LangText("Paid");
                            break;

                        case 2:
                            dInvoices.StatusText = SepFunctions.LangText("Shipped (All)");
                            break;

                        case 3:
                            dInvoices.StatusText = SepFunctions.LangText("Shipped (Partial)");
                            break;

                        case 4:
                            dInvoices.StatusText = SepFunctions.LangText("Shipped (Multiple Packages)");
                            break;

                        case 5:
                            dInvoices.StatusText = SepFunctions.LangText("Temporarity Out of Stock");
                            break;

                        case 6:
                            dInvoices.StatusText = SepFunctions.LangText("Shipment Returned");
                            break;

                        case 7:
                            dInvoices.StatusText = SepFunctions.LangText("Back Order");
                            break;

                        case 8:
                            dInvoices.StatusText = SepFunctions.LangText("Voided");
                            break;

                        default:
                            dInvoices.StatusText = SepFunctions.LangText("Not Paid");
                            break;
                    }

                    dInvoices.OrderDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["OrderDate"]));
                    dInvoices.TotalPaid = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalPaid"]));

                    // Get Custom Field Option Pricing
                    decimal customPrice = 0;
                    using (var ds2 = new DataSet())
                    {
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            using (var cmd = new SqlCommand("SELECT IP.ProductID,IP.Quantity FROM Invoices_Products AS IP WHERE IP.InvoiceID='" + SepFunctions.FixWord(SepFunctions.openNull(ds.Tables[0].Rows[i]["InvoiceID"])) + "' ORDER BY IP.StoreID,IP.ProductName", conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection.Open();
                                using (var da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(ds2);
                                }
                            }
                        }

                        for (var j = 0; j <= ds2.Tables[0].Rows.Count - 1; j++)
                        {
                            if (ds2.Tables[0].Rows.Count == j)
                            {
                                break;
                            }

                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("SELECT FieldValue FROM CustomFieldUsers WHERE UniqueID=@UniqueID AND UserID=@UserID AND ModuleID='41' AND Status <> -1", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(SepFunctions.openNull(ds2.Tables[0].Rows[j]["ProductID"])));
                                    if (!string.IsNullOrWhiteSpace(UserID))
                                    {
                                        cmd.Parameters.AddWithValue("@UserID", UserID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@UserID", string.Empty);
                                    }

                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (Strings.InStr(SepFunctions.openNull(RS["FieldValue"]), "||") > 0)
                                            {
                                                customPrice += SepFunctions.toDecimal(Strings.Split(SepFunctions.openNull(RS["FieldValue"]), "||")[1]) * SepFunctions.toDecimal(SepFunctions.openNull(ds2.Tables[0].Rows[j]["Quantity"]));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    dInvoices.TotalUnitPrice = SepFunctions.Format_Currency((SepFunctions.toDecimal(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalSetup"])) + customPrice));

                    RecurringPrice = 0;
                    RecurringCycle = string.Empty;
                    using (var ds2 = new DataSet())
                    {
                        using (var conn2 = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            using (var cmd2 = new SqlCommand("SELECT (Cast(RecurringPrice as money) * Quantity) AS RecurringPrice,RecurringCycle FROM Invoices_Products WHERE InvoiceID='" + SepFunctions.FixWord(SepFunctions.openNull(ds.Tables[0].Rows[i]["InvoiceID"])) + "' AND Status <> -1", conn2))
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.Connection.Open();
                                using (var da2 = new SqlDataAdapter(cmd2))
                                {
                                    da2.Fill(ds2);
                                }
                            }
                        }

                        for (var j = 0; j <= ds2.Tables[0].Rows.Count - 1; j++)
                        {
                            if (ds2.Tables[0].Rows.Count == j)
                            {
                                break;
                            }

                            if (SepFunctions.toDouble(SepFunctions.openNull(ds2.Tables[0].Rows[j]["RecurringPrice"])) > 0)
                            {
                                switch (SepFunctions.openNull(ds2.Tables[0].Rows[j]["RecurringCycle"]))
                                {
                                    case "1m":
                                        i1Month += SepFunctions.toDouble(SepFunctions.openNull(ds2.Tables[0].Rows[j]["RecurringPrice"]));
                                        break;

                                    case "3m":
                                        i3Month += SepFunctions.toDouble(SepFunctions.openNull(ds2.Tables[0].Rows[j]["RecurringPrice"]));
                                        break;

                                    case "6m":
                                        i6Month += SepFunctions.toDouble(SepFunctions.openNull(ds2.Tables[0].Rows[j]["RecurringPrice"]));
                                        break;

                                    case "1y":
                                        i1Year += SepFunctions.toDouble(SepFunctions.openNull(ds2.Tables[0].Rows[j]["RecurringPrice"]));
                                        break;
                                }
                            }
                        }
                    }

                    if (i1Year > 0)
                    {
                        RecurringPrice = i1Year;
                        RecurringCycle = "1y";
                    }

                    if (i6Month > 0)
                    {
                        RecurringCycle = "6m";
                        RecurringPrice = i6Month;
                        if (i1Year > 0)
                        {
                            RecurringPrice += i3Month / 2;
                        }
                    }

                    if (i3Month > 0)
                    {
                        RecurringCycle = "3m";
                        RecurringPrice = i3Month;
                        if (i6Month > 0)
                        {
                            RecurringPrice += i3Month / 2;
                        }

                        if (i1Year > 0)
                        {
                            RecurringPrice += i3Month / 4;
                        }
                    }

                    if (i1Month > 0)
                    {
                        RecurringCycle = "1m";
                        RecurringPrice = i1Month;
                        if (i3Month > 0)
                        {
                            RecurringPrice += i3Month / 3;
                        }

                        if (i6Month > 0)
                        {
                            RecurringPrice += i3Month / 6;
                        }

                        if (i1Year > 0)
                        {
                            RecurringPrice += i3Month / 12;
                        }
                    }

                    dInvoices.TotalRecurring = SepFunctions.Format_Currency(RecurringPrice);
                    dInvoices.RecurringCycle = RecurringCycle;

                    lInvoices.Add(dInvoices);
                }
            }

            return lInvoices;
        }

        /// <summary>
        /// Gets the invoices monthly totals.
        /// </summary>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>List&lt;Models.Invoices&gt;.</returns>
        public static List<Models.Invoices> GetInvoicesMonthlyTotals(long StoreID = 0)
        {
            var lInvoices = new List<Models.Invoices>();

            var wClause = string.Empty;

            if (StoreID > 0)
            {
                wClause += " AND IP.StoreID='" + SepFunctions.FixWord(Strings.ToString(StoreID)) + "'";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                for (var i = 0; i <= 11; i++)
                {
                    using (var cmd = new SqlCommand("SELECT Sum(Cast(UnitPrice as Money) * Quantity) AS T_UnitPrice,Sum(Cast(RecurringPrice as Money) * Quantity) AS T_RecurringPrice,Sum(Cast(Handling as Money) * Quantity) AS T_Handling,Sum(Cast(Handling as Money) + Cast(RecurringPrice as Money) + Cast(UnitPrice as Money) * Quantity) AS T_TotalPrice,Count(INV.InvoiceID) AS T_TotalInvoices FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND Month(INV.OrderDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -i, DateTime.Now)) + "' AND Year(INV.OrderDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -i, DateTime.Now)) + "' AND INV.Status > '0' AND INV.inCart='0' AND INV.PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty + wClause, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            var dInvoices = new Models.Invoices { MonthName = DateAndTime.MonthName(DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -i, DateTime.Now))) };
                            dInvoices.TotalRecurring = SepFunctions.Format_Currency(SepFunctions.openNull(RS["T_RecurringPrice"]));
                            dInvoices.TotalUnitPrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["T_UnitPrice"]));
                            dInvoices.TotalHandlingPrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["T_Handling"]));
                            dInvoices.TotalPaid = SepFunctions.Format_Currency(SepFunctions.openNull(RS["T_TotalPrice"]));
                            dInvoices.TotalInvoices = SepFunctions.openNull(RS["T_TotalInvoices"]);
                            if (SepFunctions.toDouble(SepFunctions.openNull(RS["T_TotalPrice"])) > 0 && SepFunctions.toDouble(SepFunctions.openNull(RS["T_TotalInvoices"])) > 0)
                            {
                                dInvoices.AveragePrice = SepFunctions.Format_Currency(SepFunctions.toDouble(SepFunctions.openNull(RS["T_TotalPrice"])) / SepFunctions.toDouble(SepFunctions.openNull(RS["T_TotalInvoices"])));
                            }
                            else
                            {
                                dInvoices.AveragePrice = SepFunctions.Format_Currency("0");
                            }

                            lInvoices.Add(dInvoices);
                        }
                    }
                }
            }

            return lInvoices;
        }

        /// <summary>
        /// Gets the invoices products.
        /// </summary>
        /// <param name="InvoiceID">The invoice identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>List&lt;Models.InvoicesProducts&gt;.</returns>
        public static List<InvoicesProducts> GetInvoicesProducts(long InvoiceID, long StoreID = -1)
        {
            var lInvoicesProducts = new List<InvoicesProducts>();

            var wclause = string.Empty;

            if (StoreID >= 0)
            {
                wclause = " AND IP.StoreID='" + SepFunctions.FixWord(Strings.ToString(StoreID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT IP.ProductID,IP.InvoiceProductID,IP.ProductName,IP.UnitPrice,IP.RecurringPrice,IP.RecurringCycle,IP.Quantity,(SELECT ItemWeight FROM ShopProducts WHERE Status <> -1 AND ProductID=IP.ProductID) AS ItemWeight,(SELECT WeightType FROM ShopProducts WHERE Status <> -1 AND ProductID=IP.ProductID) AS WeightType,IP.StoreID,(SELECT StoreName FROM ShopStores WHERE StoreID=IP.StoreID) AS StoreName,(SELECT ProductName FROM ShopProducts WHERE ProductID=IP.ProductID) AS Mall_ProductName,IP.Handling FROM Invoices_Products AS IP WHERE IP.InvoiceID='" + SepFunctions.FixWord(Strings.ToString(InvoiceID)) + "'" + wclause + " ORDER BY IP.StoreID,IP.ProductName", conn))
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

                    var dInvoicesProducts = new Models.InvoicesProducts { InvoiceProductID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["InvoiceProductID"])) };
                    dInvoicesProducts.Handling = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["Handling"]));
                    dInvoicesProducts.Quantity = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Quantity"]));
                    dInvoicesProducts.ProductID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductID"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["Mall_ProductName"])))
                    {
                        dInvoicesProducts.ProductName = SepFunctions.openNull(ds.Tables[0].Rows[i]["Mall_ProductName"]);
                    }
                    else
                    {
                        dInvoicesProducts.ProductName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductName"]);
                    }

                    if (SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ItemWeight"])) > 0)
                    {
                        dInvoicesProducts.Weight = SepFunctions.openNull(ds.Tables[0].Rows[i]["ItemWeight"]);
                        dInvoicesProducts.WeightType = SepFunctions.openNull(ds.Tables[0].Rows[i]["WeightType"]);
                    }

                    // Get Custom Field Option Pricing
                    double customPrice = 0;
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT FieldValue FROM CustomFieldUsers WHERE UniqueID=@UniqueID AND UserID=@UserID AND ModuleID='41' AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductID"])));
                            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UserID", string.Empty);
                            }

                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (Strings.InStr(SepFunctions.openNull(RS["FieldValue"]), "||") > 0)
                                    {
                                        customPrice = SepFunctions.toDouble(Strings.Split(SepFunctions.openNull(RS["FieldValue"]), "||")[1]);
                                    }
                                }
                            }
                        }
                    }

                    dInvoicesProducts.UnitPrice = SepFunctions.Format_Currency(SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"])) + SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"])));
                    dInvoicesProducts.TotalPrice = SepFunctions.Format_Currency((SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"])) + SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"])) + SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["Handling"])) + customPrice) * SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Quantity"])));
                    dInvoicesProducts.TotalPriceNoHandling = SepFunctions.Format_Currency((SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"])) + SepFunctions.toDouble(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"])) + customPrice) * SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Quantity"])));
                    dInvoicesProducts.StoreID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreID"]));
                    dInvoicesProducts.StoreName = SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreName"]);
                    lInvoicesProducts.Add(dInvoicesProducts);
                }
            }

            return lInvoicesProducts;
        }

        /// <summary>
        /// Gets the invoices unique stores.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="InvoiceID">The invoice identifier.</param>
        /// <returns>List&lt;Models.Invoices&gt;.</returns>
        public static List<Models.Invoices> GetInvoicesUniqueStores(string UserID = "", long InvoiceID = 0)
        {
            var lInvoices = new List<Models.Invoices>();

            var wClause = string.Empty;

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND INV.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (InvoiceID > 0)
            {
                wClause += " AND INV.InvoiceID='" + SepFunctions.FixWord(Strings.ToString(InvoiceID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT INVP.StoreID, (SELECT StoreName FROM ShopStores WHERE StoreID=INVP.StoreID AND Status <> -1) AS StoreName FROM Invoices_Products AS INVP, Invoices AS INV WHERE INV.InvoiceID=INVP.InvoiceID" + wClause + " GROUP BY INVP.StoreID ORDER BY INVP.StoreID", conn))
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

                    var dInvoices = new Models.Invoices { StoreID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreID"])) };
                    if (SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreID"])) == 0)
                    {
                        dInvoices.StoreName = SepFunctions.Setup(992, "WebSiteName");
                    }
                    else
                    {
                        dInvoices.StoreName = SepFunctions.openNull(ds.Tables[0].Rows[i]["StoreName"]);
                    }

                    lInvoices.Add(dInvoices);
                }
            }

            return lInvoices;
        }

        /// <summary>
        /// Invoices the add shipping method.
        /// </summary>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="MethodID">The method identifier.</param>
        /// <returns>System.String.</returns>
        public static string Invoice_Add_Shipping_Method(long StoreID, long MethodID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Invoices_Products SET ShippingMethodID='" + SepFunctions.FixWord(Strings.ToString(MethodID)) + "' WHERE StoreID='" + SepFunctions.FixWord(Strings.ToString(StoreID)) + "'", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            sReturn = SepFunctions.LangText("Invoice(s) has been successfully marked as with a Method ID.");

            return sReturn;
        }

        /// <summary>
        /// Invoices the delete.
        /// </summary>
        /// <param name="InvoiceIDs">The invoice i ds.</param>
        /// <returns>System.String.</returns>
        public static string Invoice_Delete(string InvoiceIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrInvoiceIDs = Strings.Split(InvoiceIDs, ",");

                if (arrInvoiceIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrInvoiceIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Invoices SET Status='-1', DateDeleted=@DateDeleted WHERE InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceID", arrInvoiceIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Invoices_Products SET Status='-1', DateDeleted=@DateDeleted WHERE InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceID", arrInvoiceIDs[i]);
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

            sReturn = SepFunctions.LangText("Invoice(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Invoices the get.
        /// </summary>
        /// <param name="InvoiceID">The invoice identifier.</param>
        /// <returns>Models.Invoices.</returns>
        public static Models.Invoices Invoice_Get(long InvoiceID)
        {
            var returnXML = new Models.Invoices();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Invoices WHERE InvoiceID=@InvoiceID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.InvoiceID = SepFunctions.toLong(SepFunctions.openNull(RS["InvoiceID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.UserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.DiscountCode = SepFunctions.openNull(RS["DiscountCode"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            returnXML.OrderDate = SepFunctions.toDate(SepFunctions.openNull(RS["OrderDate"]));
                            switch (SepFunctions.toInt(SepFunctions.openNull(RS["Status"])))
                            {
                                case 1:
                                    returnXML.StatusText = SepFunctions.LangText("Paid");
                                    break;

                                case 2:
                                    returnXML.StatusText = SepFunctions.LangText("Shipped (All)");
                                    break;

                                case 3:
                                    returnXML.StatusText = SepFunctions.LangText("Shipped (Partial)");
                                    break;

                                case 4:
                                    returnXML.StatusText = SepFunctions.LangText("Shipped (Multiple Packages)");
                                    break;

                                case 5:
                                    returnXML.StatusText = SepFunctions.LangText("Temporarity Out of Stock");
                                    break;

                                case 6:
                                    returnXML.StatusText = SepFunctions.LangText("Shipment Returned");
                                    break;

                                case 7:
                                    returnXML.StatusText = SepFunctions.LangText("Back Order");
                                    break;

                                case 8:
                                    returnXML.StatusText = SepFunctions.LangText("Voided");
                                    break;

                                default:
                                    returnXML.StatusText = SepFunctions.LangText("Not Paid");
                                    break;
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Invoices the mark not paid.
        /// </summary>
        /// <param name="InvoiceIDs">The invoice i ds.</param>
        /// <returns>System.String.</returns>
        public static string Invoice_Mark_Not_Paid(string InvoiceIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrInvoiceIDs = Strings.Split(InvoiceIDs, ",");

                if (arrInvoiceIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrInvoiceIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Invoices SET Status='0' WHERE InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceID", arrInvoiceIDs[i]);
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

            sReturn = SepFunctions.LangText("Invoice(s) has been successfully marked as not paid.");

            return sReturn;
        }

        /// <summary>
        /// Invoices the mark paid.
        /// </summary>
        /// <param name="InvoiceIDs">The invoice i ds.</param>
        /// <returns>System.String.</returns>
        public static string Invoice_Mark_Paid(string InvoiceIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrInvoiceIDs = Strings.Split(InvoiceIDs, ",");

                if (arrInvoiceIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrInvoiceIDs); i++)
                    {
                        using (var cmd = new SqlCommand("SELECT INV.InvoiceID, M.EmailAddress FROM Invoices AS INV,Members AS M WHERE INV.UserID=M.UserID AND INV.InvoiceID='" + SepFunctions.FixWord(arrInvoiceIDs[i]) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();

                                    SepFunctions.PUB_Mark_Order_Paid(SepFunctions.openNull(RS["InvoiceID"]), string.Empty, "Manual", SepFunctions.openNull(RS["EmailAddress"]));
                                }
                            }
                        }
                    }
                }
            }

            SepFunctions.Session_Clear_Invoice_ID();

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Invoice(s) has been successfully marked as paid.");

            return sReturn;
        }

        /// <summary>
        /// Invoices the save.
        /// </summary>
        /// <param name="InvoiceID">The invoice identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Status">The status.</param>
        /// <param name="OrderDate">The order date.</param>
        /// <param name="iModuleID">The i module identifier.</param>
        /// <param name="AddProductIDs">The add product i ds.</param>
        /// <param name="AddProductQtys">The add product qtys.</param>
        /// <param name="ModifyProductIDs">The modify product i ds.</param>
        /// <param name="ModifyQty">The modify qty.</param>
        /// <param name="EmailInvoice">if set to <c>true</c> [email invoice].</param>
        /// <param name="CustomProductNames">The custom product names.</param>
        /// <param name="CustomProductPrices">The custom product prices.</param>
        /// <param name="CustomProductRecurringPrices">The custom product recurring prices.</param>
        /// <param name="CustomProductRecurringCycle">The custom product recurring cycle.</param>
        /// <param name="CustomProductQtys">The custom product qtys.</param>
        /// <param name="LinkID">The link identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Invoice_Save(long InvoiceID, string UserID, long Status, DateTime OrderDate, int iModuleID, string AddProductIDs, string AddProductQtys, string ModifyProductIDs, string ModifyQty, bool EmailInvoice, string CustomProductNames, string CustomProductPrices, string CustomProductRecurringPrices, string CustomProductRecurringCycle, string CustomProductQtys, long LinkID, long StoreID, long PortalID)
        {
            var sRecurringID = SepFunctions.GetIdentity();

            var sQty = 0;

            var GetAffiliateID = string.Empty;
            var GetRecurring = 0;
            var GetExcludeAffiliate = 0;
            var GetPaymentMethod = string.Empty;
            decimal iUnitPrice = 0;

            var updateInvoice = false;
            var doUpdate = false;
            var SqlStr = string.Empty;

            decimal iGrandTotal = 0;
            decimal sHandling = 0;
            long iQty = 0;
            var GetEmailAddress = string.Empty;
            var GetEmailBody = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + SepFunctions.GetMasterDomain(false) + "/skins/public/styles/colors.css\"/>";

            decimal CustomFieldPrice = 0;

            GetEmailBody += SepFunctions.LangText("Greetings ~~" + SepFunctions.GetUserInformation("FirstName", UserID) + " " + SepFunctions.GetUserInformation("LastName", UserID) + "~~,") + "<br/><br/>" + SepFunctions.LangText("Thank you for your purchase at ~~" + SepFunctions.Setup(992, "WebSiteName") + "~~. Below is a copy of your invoice.") + "<br/><br/>";
            GetEmailBody += "<table class=\"FieldsetBox\" width=\"500\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
            GetEmailBody += "<tr class=\"FieldsetBoxTitle\"><td width=\"290\">" + SepFunctions.LangText("Product Name") + "</td><td width=\"80\">" + SepFunctions.LangText("Unit Price") + "</td><td width=\"50\">" + SepFunctions.LangText("Qty") + "</td><td width=\"80\">" + SepFunctions.LangText("Total Price") + "</td></tr>";

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (iModuleID == 41)
                {
                    if (!string.IsNullOrWhiteSpace(ModifyProductIDs) && !string.IsNullOrWhiteSpace(ModifyQty))
                    {
                        var arrModifyQtyIDs = Strings.Split(ModifyProductIDs, ",");
                        var arrModifyQty = Strings.Split(ModifyQty, ",");

                        if (arrModifyQtyIDs != null)
                        {
                            for (var i = 0; i <= Information.UBound(arrModifyQtyIDs); i++)
                            {
                                sQty = SepFunctions.toInt(arrModifyQty[i]);
                                using (var cmd = new SqlCommand("SELECT FieldValue FROM CustomFieldUsers WHERE UniqueID=@ProductID AND UserID=@UserID AND Status=1", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", SepFunctions.toLong(arrModifyQtyIDs[i]));
                                    cmd.Parameters.AddWithValue("@UserID", UserID);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        while (RS.Read())
                                        {
                                            if (Strings.InStr(SepFunctions.openNull(RS["FieldValue"]), "||") > 0)
                                            {
                                                var arrValue = Strings.Split(SepFunctions.openNull(RS["FieldValue"]), "||");
                                                CustomFieldPrice = CustomFieldPrice + SepFunctions.toDecimal(arrValue[1]) * sQty;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(AddProductIDs))
                    {
                        var arrNewProducts = Strings.Split(AddProductIDs, ",");
                        var arrNewQtys = Strings.Split(AddProductQtys, ",");
                        Array.Resize(ref arrNewQtys, Information.UBound(arrNewProducts) + 1);

                        if (arrNewProducts != null)
                        {
                            for (var i = 0; i <= Information.UBound(arrNewProducts); i++)
                            {
                                if (!string.IsNullOrWhiteSpace(arrNewProducts[i]))
                                {
                                    sQty = SepFunctions.toInt(arrNewQtys[i]);
                                    using (var cmd = new SqlCommand("SELECT FieldValue FROM CustomFieldUsers WHERE UniqueID=@ProductID AND UserID=@UserID AND Status=1", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", SepFunctions.toLong(arrNewProducts[i]));
                                        cmd.Parameters.AddWithValue("@UserID", UserID);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                while (RS.Read())
                                                {
                                                    if (Strings.InStr(SepFunctions.openNull(RS["FieldValue"]), "||") > 0)
                                                    {
                                                        var arrValue = Strings.Split(SepFunctions.openNull(RS["FieldValue"]), "||");
                                                        CustomFieldPrice += SepFunctions.toDecimal(arrValue[1]) * sQty;
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

                if (Status > 0 || !string.IsNullOrWhiteSpace(UserID))
                {
                    using (var cmd = new SqlCommand("SELECT ReferralID,EmailAddress FROM Members WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader InvRS = cmd.ExecuteReader())
                        {
                            if (InvRS.HasRows)
                            {
                                InvRS.Read();
                                if (Status == 1)
                                {
                                    GetPaymentMethod = "Manual";
                                }

                                GetAffiliateID = SepFunctions.openNull(InvRS["ReferralID"]);
                                GetEmailAddress = SepFunctions.openNull(InvRS["EmailAddress"]);
                            }
                            else
                            {
                                return SepFunctions.LangText("Invalid UserID was entered.");
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(ModifyProductIDs) && !string.IsNullOrWhiteSpace(ModifyQty))
                {
                    var arrModifyQtyIDs = Strings.Split(ModifyProductIDs, ",");
                    var arrModifyQty = Strings.Split(ModifyQty, ",");

                    if (arrModifyQtyIDs != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrModifyQtyIDs); i++)
                        {
                            sQty = SepFunctions.toInt(arrModifyQty[i]);
                            if (sQty == 0)
                            {
                                using (var cmd = new SqlCommand("DELETE FROM Invoices_Products WHERE ProductID=@ProductID AND InvoiceID=@InvoiceID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", arrModifyQtyIDs[i]);
                                    cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (var cmd = new SqlCommand("SELECT IP.InvoiceProductID,IP.ProductID,IP.ProductName,IP.UnitPrice,IP.RecurringPrice,IP.Quantity,INV.Status,M.EmailAddress,(SELECT Handling FROM ShopProducts WHERE ProductID=IP.ProductID) AS Handling FROM Invoices AS INV,Invoices_Products AS IP,Members AS M WHERE INV.InvoiceID=IP.InvoiceID AND INV.UserID=M.UserID AND IP.ProductID=@ProductID AND IP.InvoiceID=@InvoiceID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", arrModifyQtyIDs[i]);
                                    cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                    using (SqlDataReader InvRS = cmd.ExecuteReader())
                                    {
                                        if (InvRS.HasRows)
                                        {
                                            InvRS.Read();
                                            iUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["UnitPrice"]));
                                            iGrandTotal = iGrandTotal + (iUnitPrice + SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"]))) * SepFunctions.toLong(SepFunctions.openNull(InvRS["Quantity"]));
                                            if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["Handling"])) > 0)
                                            {
                                                sHandling = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["Handling"])) * SepFunctions.toLong(SepFunctions.openNull(InvRS["Quantity"]));
                                                iGrandTotal = iGrandTotal + sHandling;
                                            }

                                            using (var cmd2 = new SqlCommand("UPDATE Invoices_Products SET Quantity=@Quantity WHERE ProductID=@ProductID AND InvoiceID=@InvoiceID", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@Quantity", sQty);
                                                cmd2.Parameters.AddWithValue("@ProductID", arrModifyQtyIDs[i]);
                                                cmd2.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                                cmd2.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(AddProductIDs))
                {
                    var arrNewProducts = Strings.Split(AddProductIDs, ",");
                    var arrNewQtys = Strings.Split(AddProductQtys, ",");
                    Array.Resize(ref arrNewQtys, Information.UBound(arrNewProducts) + 1);

                    if (arrNewProducts != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrNewProducts); i++)
                        {
                            if (!string.IsNullOrWhiteSpace(arrNewProducts[i]))
                            {
                                switch (iModuleID)
                                {
                                    case 37:
                                        SqlStr = "SELECT ProductID,ProductName,SalePrice,UnitPrice,RecurringPrice,RecurringCycle,AffiliateUnitPrice,AffiliateRecurringPrice,ExcludeAffiliate,Handling FROM ShopProducts WHERE ModelNumber=@ProductID";
                                        break;

                                    default:
                                        SqlStr = "SELECT ProductID,ProductName,SalePrice,UnitPrice,RecurringPrice,RecurringCycle,AffiliateUnitPrice,AffiliateRecurringPrice,ExcludeAffiliate,Handling FROM ShopProducts WHERE ProductID=@ProductID";
                                        break;
                                }

                                using (var cmd = new SqlCommand(SqlStr, conn))
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", arrNewProducts[i]);
                                    using (SqlDataReader InvRS = cmd.ExecuteReader())
                                    {
                                        if (InvRS.HasRows)
                                        {
                                            InvRS.Read();
                                            doUpdate = false;
                                            if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["SalePrice"])) > 0 && SepFunctions.toDecimal(SepFunctions.openNull(InvRS["SalePrice"])) < SepFunctions.toDecimal(SepFunctions.openNull(InvRS["UnitPrice"])))
                                            {
                                                iUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["SalePrice"]));
                                            }
                                            else
                                            {
                                                iUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["UnitPrice"]));
                                            }

                                            iGrandTotal = iGrandTotal + iUnitPrice + SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"]));
                                            if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["Handling"])) > 0)
                                            {
                                                sHandling = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["Handling"])) * 1;
                                                iGrandTotal = iGrandTotal + sHandling;
                                            }

                                            if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"])) > 0)
                                            {
                                                GetRecurring = 1;
                                            }

                                            if (SepFunctions.openBoolean(InvRS["ExcludeAffiliate"]))
                                            {
                                                GetExcludeAffiliate = 1;
                                            }

                                            using (var cmd2 = new SqlCommand("SELECT Quantity FROM Invoices_Products WHERE InvoiceID=@InvoiceID AND ProductID=@ProductID", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                                cmd2.Parameters.AddWithValue("@ProductID", arrNewProducts[i]);
                                                using (SqlDataReader InvRS2 = cmd2.ExecuteReader())
                                                {
                                                    if (InvRS2.HasRows)
                                                    {
                                                        InvRS2.Read();
                                                        doUpdate = true;
                                                        iQty = SepFunctions.toLong(SepFunctions.toLong(arrNewQtys[i]) == 0 ? "1" : arrNewQtys[i]) + SepFunctions.toLong(SepFunctions.openNull(InvRS2["Quantity"]));
                                                    }
                                                    else
                                                    {
                                                        iQty = SepFunctions.toLong(SepFunctions.toLong(arrNewQtys[i]) == 0 ? "1" : arrNewQtys[i]);
                                                    }
                                                }
                                            }

                                            if (doUpdate)
                                            {
                                                SqlStr = "UPDATE Invoices_Products SET Quantity=@Quantity WHERE InvoiceID=@InvoiceID AND ProductID=@ProductID";
                                            }
                                            else
                                            {
                                                SqlStr = "INSERT INTO Invoices_Products (InvoiceProductID, InvoiceID, ProductID, ModuleID, ProductName, UnitPrice, RecurringPrice, RecurringCycle, isRecurring, Quantity, Handling, AffiliateUnitPrice, AffiliateRecurringPrice, ExcludeAffiliate, LinkID, StoreID, PortalID, Status) VALUES (@InvoiceProductID, @InvoiceID, @ProductID, @ModuleID, @ProductName, @UnitPrice, @RecurringPrice, @RecurringCycle, @isRecurring, @Quantity, @Handling, @AffiliateUnitPrice, @AffiliateRecurringPrice, @ExcludeAffiliate, @LinkID, @StoreID, @PortalID, '1')";
                                            }

                                            using (var cmd2 = new SqlCommand(SqlStr, conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@InvoiceProductID", SepFunctions.GetIdentity());
                                                cmd2.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                                cmd2.Parameters.AddWithValue("@ProductID", arrNewProducts[i]);
                                                cmd2.Parameters.AddWithValue("@ModuleID", iModuleID);
                                                cmd2.Parameters.AddWithValue("@ProductName", SepFunctions.openNull(InvRS["ProductName"]));
                                                cmd2.Parameters.AddWithValue("@UnitPrice", iUnitPrice);
                                                cmd2.Parameters.AddWithValue("@RecurringPrice", SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"])));
                                                cmd2.Parameters.AddWithValue("@RecurringCycle", !string.IsNullOrWhiteSpace(SepFunctions.openNull(InvRS["RecurringCycle"])) ? SepFunctions.openNull(InvRS["RecurringCycle"]) : "1m");
                                                cmd2.Parameters.AddWithValue("@isRecurring", GetRecurring);
                                                cmd2.Parameters.AddWithValue("@Quantity", iQty);
                                                cmd2.Parameters.AddWithValue("@Handling", SepFunctions.toDecimal(!string.IsNullOrWhiteSpace(SepFunctions.openNull(InvRS["Handling"])) ? SepFunctions.openNull(InvRS["Handling"]) : "0"));
                                                if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["AffiliateUnitPrice"])) == 0)
                                                {
                                                    cmd2.Parameters.AddWithValue("@AffiliateUnitPrice", SepFunctions.toDecimal(SepFunctions.openNull(InvRS["UnitPrice"])));
                                                }
                                                else
                                                {
                                                    cmd2.Parameters.AddWithValue("@AffiliateUnitPrice", SepFunctions.toDecimal(SepFunctions.openNull(InvRS["AffiliateUnitPrice"])));
                                                }

                                                if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["AffiliateRecurringPrice"])) == 0)
                                                {
                                                    cmd2.Parameters.AddWithValue("@AffiliateRecurringPrice", SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"])));
                                                }
                                                else
                                                {
                                                    cmd2.Parameters.AddWithValue("@AffiliateRecurringPrice", SepFunctions.toDecimal(SepFunctions.openNull(InvRS["AffiliateRecurringPrice"])));
                                                }

                                                cmd2.Parameters.AddWithValue("@ExcludeAffiliate", GetExcludeAffiliate);
                                                cmd2.Parameters.AddWithValue("@LinkID", LinkID);
                                                cmd2.Parameters.AddWithValue("@StoreID", StoreID);
                                                cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                                cmd2.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(CustomProductNames))
                {
                    var arrCustomProductNames = Strings.Split(CustomProductNames, "||");
                    var arrCustomProductPrices = Strings.Split(CustomProductPrices, "||");
                    var arrCustomProductRecurringPrices = Strings.Split(CustomProductRecurringPrices, "||");
                    var arrCustomProductRecurringCycle = Strings.Split(CustomProductRecurringCycle, "||");
                    var arrCustomProductQtys = Strings.Split(CustomProductQtys, "||");
                    Array.Resize(ref arrCustomProductPrices, Information.UBound(arrCustomProductNames) + 1);
                    Array.Resize(ref arrCustomProductRecurringPrices, Information.UBound(arrCustomProductNames) + 1);
                    Array.Resize(ref arrCustomProductRecurringCycle, Information.UBound(arrCustomProductNames) + 1);
                    Array.Resize(ref arrCustomProductQtys, Information.UBound(arrCustomProductNames) + 1);

                    if (arrCustomProductNames != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrCustomProductNames); i++)
                        {
                            if (!string.IsNullOrWhiteSpace(arrCustomProductNames[i]))
                            {
                                iGrandTotal += SepFunctions.toDecimal(arrCustomProductPrices[i]) + SepFunctions.toDecimal(arrCustomProductRecurringPrices[i]);
                                using (var cmd2 = new SqlCommand("SELECT Quantity FROM Invoices_Products WHERE InvoiceID=@InvoiceID AND ProductName=@ProductName AND ModuleID=@ModuleID AND UnitPrice=@UnitPrice", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                    cmd2.Parameters.AddWithValue("@ProductName", arrCustomProductNames[i]);
                                    cmd2.Parameters.AddWithValue("@ModuleID", iModuleID);
                                    cmd2.Parameters.AddWithValue("@UnitPrice", SepFunctions.toDouble(arrCustomProductPrices[i]));
                                    using (SqlDataReader InvRS2 = cmd2.ExecuteReader())
                                    {
                                        if (InvRS2.HasRows)
                                        {
                                            InvRS2.Read();
                                            doUpdate = true;
                                            if (SepFunctions.toInt(arrCustomProductQtys[i]) == 0)
                                            {
                                                iQty = 1 + SepFunctions.toLong(SepFunctions.openNull(InvRS2["Quantity"]));
                                            }
                                            else
                                            {
                                                iQty = SepFunctions.toInt(arrCustomProductQtys[i]) + SepFunctions.toLong(SepFunctions.openNull(InvRS2["Quantity"]));
                                            }
                                        }
                                        else
                                        {
                                            if (SepFunctions.toInt(arrCustomProductQtys[i]) == 0)
                                            {
                                                iQty = 1;
                                            }
                                            else
                                            {
                                                iQty = SepFunctions.toInt(arrCustomProductQtys[i]);
                                            }
                                        }
                                    }
                                }

                                if (doUpdate)
                                {
                                    SqlStr = "UPDATE Invoices_Products SET Quantity=@Quantity WHERE InvoiceID=@InvoiceID AND ProductName=@ProductName AND ModuleID=@ModuleID AND UnitPrice=@UnitPrice";
                                }
                                else
                                {
                                    SqlStr = "INSERT INTO Invoices_Products (InvoiceProductID, InvoiceID, ProductID, ModuleID, ProductName, UnitPrice, RecurringPrice, RecurringCycle, isRecurring, Quantity, Handling, AffiliateUnitPrice, AffiliateRecurringPrice, ExcludeAffiliate, LinkID, StoreID, PortalID, Status) VALUES (@InvoiceProductID, @InvoiceID, @ProductID, @ModuleID, @ProductName, @UnitPrice, @RecurringPrice, @RecurringCycle, @isRecurring, @Quantity, @Handling, @AffiliateUnitPrice, @AffiliateRecurringPrice, @ExcludeAffiliate, @LinkID, @StoreID, @PortalID, '1')";
                                }

                                using (var cmd2 = new SqlCommand(SqlStr, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@InvoiceProductID", SepFunctions.GetIdentity());
                                    cmd2.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                                    cmd2.Parameters.AddWithValue("@ProductID", string.Empty);
                                    cmd2.Parameters.AddWithValue("@ModuleID", iModuleID);
                                    cmd2.Parameters.AddWithValue("@ProductName", arrCustomProductNames[i]);
                                    cmd2.Parameters.AddWithValue("@UnitPrice", SepFunctions.toDouble(arrCustomProductPrices[i]));
                                    cmd2.Parameters.AddWithValue("@RecurringPrice", SepFunctions.toDouble(arrCustomProductRecurringPrices[i]));
                                    if (string.IsNullOrWhiteSpace(arrCustomProductRecurringCycle[i]))
                                    {
                                        cmd2.Parameters.AddWithValue("@RecurringCycle", string.Empty);
                                    }
                                    else
                                    {
                                        cmd2.Parameters.AddWithValue("@RecurringCycle", arrCustomProductRecurringCycle[i]);
                                    }

                                    cmd2.Parameters.AddWithValue("@isRecurring", 0);
                                    cmd2.Parameters.AddWithValue("@Quantity", iQty);
                                    cmd2.Parameters.AddWithValue("@Handling", "0");
                                    cmd2.Parameters.AddWithValue("@AffiliateUnitPrice", "0");
                                    cmd2.Parameters.AddWithValue("@AffiliateRecurringPrice", "0");
                                    cmd2.Parameters.AddWithValue("@ExcludeAffiliate", 0);
                                    cmd2.Parameters.AddWithValue("@LinkID", LinkID);
                                    cmd2.Parameters.AddWithValue("@StoreID", StoreID);
                                    cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE InvoiceID=@InvoiceID", conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    using (SqlDataReader InvRS = cmd.ExecuteReader())
                    {
                        if (InvRS.HasRows)
                        {
                            updateInvoice = true;
                        }
                    }
                }

                if (updateInvoice)
                {
                    SqlStr = "UPDATE Invoices SET Status=@Status,OrderDate=@OrderDate,UserID=@UserID,inCart=@inCart WHERE InvoiceID=@InvoiceID";
                }
                else
                {
                    SqlStr = "INSERT INTO Invoices (InvoiceID,UserID,OrderDate,DatePaid,Status,TransactionID,PaymentMethod,AffiliateID,DiscountCode,isRecurring,RecurringID,inCart,PortalID) VALUES (@InvoiceID,@UserID,@OrderDate,@DatePaid,@Status,@TransactionID,@PaymentMethod,@AffiliateID,@DiscountCode,@isRecurring,@RecurringID,@inCart,@PortalID)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    if (!string.IsNullOrWhiteSpace(UserID))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UserID", string.Empty);
                    }

                    cmd.Parameters.AddWithValue("@OrderDate", OrderDate);
                    cmd.Parameters.AddWithValue("@DatePaid", string.Empty);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@TransactionID", string.Empty);
                    cmd.Parameters.AddWithValue("@PaymentMethod", GetPaymentMethod);
                    cmd.Parameters.AddWithValue("@AffiliateID", !string.IsNullOrWhiteSpace(GetAffiliateID) ? GetAffiliateID : string.Empty);
                    cmd.Parameters.AddWithValue("@DiscountCode", string.Empty);
                    cmd.Parameters.AddWithValue("@isRecurring", GetRecurring);
                    cmd.Parameters.AddWithValue("@RecurringID", sRecurringID);
                    if (Status == 0)
                    {
                        cmd.Parameters.AddWithValue("@inCart", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@inCart", "0");
                    }

                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                if (Status == 1)
                {
                    SepFunctions.PUB_Mark_Order_Paid(Strings.ToString(InvoiceID), Strings.ToString(InvoiceID), "Manual", GetEmailAddress);
                }

                if (EmailInvoice && !string.IsNullOrWhiteSpace(GetEmailAddress))
                {
                    iUnitPrice = 0;
                    iGrandTotal = 0;
                    sHandling = 0;
                    using (var cmd = new SqlCommand("SELECT IP.InvoiceProductID,IP.ProductID,IP.ProductName,IP.UnitPrice,IP.RecurringPrice,IP.Quantity,INV.Status,M.EmailAddress,(SELECT Handling FROM ShopProducts WHERE ProductID=IP.ProductID) AS Handling FROM Invoices AS INV,Invoices_Products AS IP,Members AS M WHERE INV.InvoiceID=IP.InvoiceID AND INV.UserID=M.UserID AND IP.InvoiceID=@InvoiceID", conn))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                        using (SqlDataReader InvRS = cmd.ExecuteReader())
                        {
                            if (InvRS.HasRows)
                            {
                                InvRS.Read();
                                iUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["UnitPrice"]));
                                iGrandTotal = iGrandTotal + (iUnitPrice + SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"]))) * SepFunctions.toLong(SepFunctions.openNull(InvRS["Quantity"]));
                                GetEmailBody += "<tr><td>" + SepFunctions.openNull(InvRS["ProductName"]);
                                if (SepFunctions.toDecimal(SepFunctions.openNull(InvRS["Handling"])) > 0)
                                {
                                    sHandling = SepFunctions.toDecimal(SepFunctions.openNull(InvRS["Handling"])) * SepFunctions.toLong(SepFunctions.openNull(InvRS["Quantity"]));
                                    GetEmailBody += "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + SepFunctions.LangText("Plus ~~" + SepFunctions.Format_Currency(sHandling) + "~~ Handling Fee");
                                    iGrandTotal = iGrandTotal + sHandling;
                                }

                                GetEmailBody += "</td><td>" + SepFunctions.Format_Currency(iUnitPrice + SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"]))) + "</td><td>" + SepFunctions.openNull(InvRS["Quantity"]) + "</td><td>" + SepFunctions.Format_Currency((iUnitPrice + SepFunctions.toDecimal(SepFunctions.openNull(InvRS["RecurringPrice"]))) * SepFunctions.toLong(SepFunctions.openNull(InvRS["Quantity"]))) + "</td></tr>";
                            }
                        }
                    }

                    GetEmailBody += "<tr class=\"FieldsetBoxTitle\"><td colspan=\"3\" align=\"right\">" + SepFunctions.LangText("Grand Total:") + "</td><td align=\"center\">" + SepFunctions.Format_Currency(iGrandTotal) + "</td></tr>";
                    GetEmailBody += "</table><br/>";
                    GetEmailBody += SepFunctions.LangText("Thank you,") + "<br/>";
                    GetEmailBody += SepFunctions.Setup(992, "WebsiteName") + " Sales";

                    SepFunctions.Send_Email(SepFunctions.GetUserInformation("EmailAddress", UserID), SepFunctions.Setup(992, "AdminEmailAddress"), SepFunctions.LangText("Your Invoice"), GetEmailBody, 985);
                }
            }

            var sReturn = SepFunctions.LangText("Invoice has been successfully saved.");
            return sReturn;
        }

        /// <summary>
        /// Monthlies the orders.
        /// </summary>
        /// <returns>List&lt;Models.ChartData&gt;.</returns>
        public static List<ChartData> MonthlyOrders()
        {
            var lData = new List<ChartData>();
            int iMonths = 12;

            decimal Total = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                // Populate series data
                for (var i = 0; i <= 11; i++)
                {
                    iMonths = iMonths - 1;
                    using (var cmd = new SqlCommand("SELECT (Sum(Cast(UnitPrice as money) * Quantity) + Sum(Cast(RecurringPrice as money) * Quantity)) AS TotalPrice FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND Month(OrderDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND Year(OrderDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND INV.Status > '0' AND INV.inCart='0' AND INV.PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            Total = Total + SepFunctions.toDecimal(SepFunctions.openNull(RS["TotalPrice"]));
                        }
                    }
                }

                iMonths = 12;

                for (var i = 0; i <= 11; i++)
                {
                    iMonths = iMonths - 1;
                    using (var cmd = new SqlCommand("SELECT (Sum(Cast(UnitPrice as money) * Quantity) + Sum(Cast(RecurringPrice as money) * Quantity)) AS TotalPrice FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND Month(OrderDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND Year(OrderDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND INV.Status > '0' AND INV.inCart='0' AND INV.PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            var dData = new Models.ChartData { MonthName = DateAndTime.MonthName(DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -i, DateTime.Now))) };
                            dData.NumOrders = Strings.Replace(SepFunctions.Format_Currency(SepFunctions.openNull(RS["TotalPrice"])), "$", string.Empty);
                            if (Total > 0)
                            {
                                dData.Percentage = SepFunctions.toDecimal(SepFunctions.openNull(RS["TotalPrice"])) / Total * 100;
                            }
                            else
                            {
                                dData.Percentage = 0;
                            }

                            lData.Add(dData);
                        }
                    }
                }
            }

            return lData;
        }
    }
}