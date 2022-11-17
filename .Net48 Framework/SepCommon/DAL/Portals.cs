// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Portals.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    /// <summary>
    /// Class Portals.
    /// </summary>
    public static class Portals
    {
        /// <summary>
        /// Gets the portal plans.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.PortalsPrices&gt;.</returns>
        public static List<PortalsPrices> GetPortalPlans(string SortExpression = "ProductName", string SortDirection = "ASC", string searchWords = "")
        {
            var lPortalsPrices = new List<PortalsPrices>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ProductName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ProductName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ProductID,ProductName,UnitPrice,RecurringPrice,RecurringCycle,Description FROM ShopProducts WHERE ModuleID='60' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dPortalsPrices = new Models.PortalsPrices { PlanID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductID"])) };
                    dPortalsPrices.PlanName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductName"]);
                    dPortalsPrices.OnetimePrice = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"]));
                    dPortalsPrices.RecurringPrice = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"]));
                    dPortalsPrices.RecurringCycle = SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringCycle"]);
                    dPortalsPrices.ModuleIDs = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    lPortalsPrices.Add(dPortalsPrices);
                }
            }

            return lPortalsPrices;
        }

        /// <summary>
        /// Gets the portals.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="showHidden">if set to <c>true</c> [show hidden].</param>
        /// <returns>List&lt;Models.Portals&gt;.</returns>
        public static List<Models.Portals> GetPortals(string SortExpression = "PortalTitle", string SortDirection = "ASC", string searchWords = "", long CatID = 0, bool showHidden = true)
        {
            var lPortals = new List<Models.Portals>();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "PortalTitle";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (CatID > -1)
            {
                wClause = " AND CatID='" + SepFunctions.FixWord(Strings.ToString(CatID)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND PortalTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (showHidden == false)
            {
                wClause += " AND HideList='0'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT PortalID,PortalTitle,Description,FriendlyName,Status FROM Portals WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dPortals = new Models.Portals { PortalID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PortalID"])) };
                    dPortals.PortalTitle = SepFunctions.openNull(ds.Tables[0].Rows[i]["PortalTitle"]);
                    dPortals.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["FriendlyName"])))
                    {
                        dPortals.PortalUrl = sInstallFolder + "go/" + SepFunctions.openNull(ds.Tables[0].Rows[i]["FriendlyName"]) + "/";
                    }
                    else
                    {
                        dPortals.PortalUrl = sInstallFolder + "portals/" + SepFunctions.openNull(ds.Tables[0].Rows[i]["PortalID"]) + "/";
                    }

                    dPortals.Status = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    lPortals.Add(dPortals);
                }
            }

            return lPortals;
        }

        /// <summary>
        /// Gets the portals pages.
        /// </summary>
        /// <param name="MenuID">The menu identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.WebPages&gt;.</returns>
        public static List<Models.WebPages> GetPortalsPages(long MenuID = 3, long PortalID = 0, string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "")
        {
            var lWebPages = new List<Models.WebPages>();

            var rowCount = 0;

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (LinkText LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (PortalID == 0)
            {
                wClause += " AND (PortalID=0 OR PortalID = -1)";
            }
            else
            {
                wClause += " AND PortalID='" + PortalID + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT [UniqueID],[PageID],[MenuID],[LinkText],[Weight],[Status] FROM PortalPages WHERE MenuID=" + MenuID + " AND PageID <> 0 AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    if (SepFunctions.ModuleActivated(SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["PageID"]))))
                    {
                        rowCount += 1;
                        var dWebPages = new Models.WebPages { UniqueID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"])) };
                        dWebPages.PageID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PageID"]));
                        dWebPages.MenuID = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["MenuID"]));
                        dWebPages.LinkText = SepFunctions.openNull(ds.Tables[0].Rows[i]["LinkText"]);
                        dWebPages.Weight = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                        if (SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"])) == 1)
                        {
                            dWebPages.Enable = true;
                        }
                        else
                        {
                            dWebPages.Enable = false;
                        }

                        dWebPages.RowNumber = rowCount;
                        lWebPages.Add(dWebPages);
                    }
                }
            }

            return lWebPages;
        }

        /// <summary>
        /// Portals the delete.
        /// </summary>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <returns>System.String.</returns>
        public static string Portal_Delete(string PortalIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrPortalIDs = Strings.Split(PortalIDs, ",");

                if (arrPortalIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrPortalIDs); i++)
                    {
                        using (var cmd2 = new SqlCommand("SELECT * FROM PortalPages WHERE PortalID=0", conn))
                        {
                            using (SqlDataReader RS = cmd2.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    using (var cmd = new SqlCommand("UPDATE Portals SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE PortalPages SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Delete module content belonging to deleted portal
                                    using (var cmd = new SqlCommand("UPDATE Articles SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE AuctionAds SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Blog SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE BusinessListings SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE ClassifiedsAds SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE CustomFieldUsers SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE DiscountSystem SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE ELearnCourses SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE EmailTemplates SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE EventTypes SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE EventCalendar SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE FAQ SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Forms SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE ForumsMessages SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Guestbook SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE LinksWebSites SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE MatchMaker SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Members SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE News SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE PhotoAlbums SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Profiles SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE RStateBrokers SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE RStateAgents SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE RStateProperty SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE ShopProducts SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Invoices SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Invoices_Products SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE SpeakSpeeches SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE SpeakTopics SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Speakers SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var cmd = new SqlCommand("UPDATE Vouchers SET Status='-1', DateDeleted=@DateDeleted WHERE PortalID=@PortalID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", arrPortalIDs[i]);
                                        cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        SepFunctions.Additional_Data_Delete(60, arrPortalIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Portals(s) has been successfully deleted.");
        }

        /// <summary>
        /// Portals the get.
        /// </summary>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>Models.Portals.</returns>
        public static Models.Portals Portal_Get(long PortalID)
        {
            var returnXML = new Models.Portals();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Portals WHERE PortalID=@PortalID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.UserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.PortalTitle = SepFunctions.openNull(RS["PortalTitle"]);
                            returnXML.DomainName = SepFunctions.openNull(RS["DomainName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.ManageKeys = SepFunctions.openNull(RS["ManageKeys"]);
                            returnXML.HideList = SepFunctions.toBoolean(SepFunctions.openNull(RS["HideList"]));
                            returnXML.LoginKeys = SepFunctions.openNull(RS["LoginKeys"]);
                            returnXML.PlanID = SepFunctions.toLong(SepFunctions.openNull(RS["PlanID"]));
                            returnXML.FriendlyName = SepFunctions.openNull(RS["FriendlyName"]);
                            returnXML.Status = SepFunctions.toLong(SepFunctions.openNull(RS["Status"]));

                            if (File.Exists(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                            {
                                using (var readfile = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                                {
                                    var sXML = readfile.ReadToEnd();
                                    returnXML.Template = SepFunctions.ParseXML("WebSiteLayout", sXML);
                                }
                            }

                            using (var cmd2 = new SqlCommand("SELECT FileData FROM Uploads WHERE UniqueID=@UniqueID AND PortalID=@PortalID", conn))
                            {
                                cmd2.Parameters.AddWithValue("@UniqueID", PortalID);
                                cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    if (RS2.HasRows)
                                    {
                                        RS2.Read();
                                        if (Information.IsDBNull(RS2["FileData"]))
                                        {
                                            returnXML.SiteLogoURL = string.Empty;
                                        }
                                        else
                                        {
                                            returnXML.SiteLogoURL = "data:image/png;base64," + SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS2["FileData"]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Portals the save.
        /// </summary>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="PortalName">Name of the portal.</param>
        /// <param name="Description">The description.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="DomainName">Name of the domain.</param>
        /// <param name="ManageKeys">The manage keys.</param>
        /// <param name="LoginKeys">The login keys.</param>
        /// <param name="HideList">if set to <c>true</c> [hide list].</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Language">The language.</param>
        /// <param name="Template">The template.</param>
        /// <param name="LogoFileName">Name of the logo file.</param>
        /// <param name="LogoFileData">The logo file data.</param>
        /// <param name="LogoImageType">Type of the logo image.</param>
        /// <param name="Status">The status.</param>
        /// <param name="PlanID">The plan identifier.</param>
        /// <param name="FriendlyName">Name of the friendly.</param>
        /// <returns>System.Int32.</returns>
        public static int Portal_Save(long PortalID, string PortalName, string Description, long CatID, string DomainName, string ManageKeys, string LoginKeys, bool HideList, string UserID, string Language, string Template, string LogoFileName, string LogoFileData, string LogoImageType, int Status, long PlanID, string FriendlyName)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var strXML = string.Empty;
            var PlanModuleIDs = string.Empty;
            var manageKeyId = string.Empty;

            var ManageKeyName = SepFunctions.GetUserInformation("UserName", UserID) + "-Portal";

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (PortalID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
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
                    PortalID = SepFunctions.GetIdentity();
                }

                using (var cmd = new SqlCommand("SELECT * FROM AccessKeys WHERE KeyName=@KeyName", conn))
                {
                    cmd.Parameters.AddWithValue("@KeyName", ManageKeyName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            manageKeyId = SepFunctions.openNull(RS["KeyID"]);
                        }
                        else
                        {
                            manageKeyId = Strings.ToString(SepFunctions.GetIdentity());
                            using (var cmd2 = new SqlCommand("INSERT INTO AccessKeys (KeyID, KeyName, Status) VALUES(@KeyID, @KeyName, '1')", conn))
                            {
                                cmd2.Parameters.AddWithValue("@KeyID", manageKeyId);
                                cmd2.Parameters.AddWithValue("@KeyName", ManageKeyName);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }

                ManageKeys += ",|" + manageKeyId + "|";

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Portals SET PortalTitle=@PortalTitle, DomainName=@DomainName, CatID=@CatID, Description=@Description, ManageKeys=@ManageKeys, LoginKeys=@LoginKeys, HideList=@HideList, PlanID=@PlanID, FriendlyName=@FriendlyName, Status=@Status WHERE PortalID=@PortalID";
                }
                else
                {
                    SqlStr = "INSERT INTO Portals (PortalID, CatID, DomainName, PortalTitle, Description, ManageKeys, LoginKeys, HideList, UserID, PlanID, FriendlyName, Status) VALUES(@PortalID, @CatID, @DomainName, @PortalTitle, @Description, @ManageKeys, @LoginKeys, @HideList, @UserID, @PlanID, @FriendlyName, @Status)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@DomainName", DomainName);
                    cmd.Parameters.AddWithValue("@PortalTitle", PortalName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@ManageKeys", ManageKeys);
                    cmd.Parameters.AddWithValue("@LoginKeys", LoginKeys);
                    cmd.Parameters.AddWithValue("@HideList", HideList);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@PlanID", PlanID);
                    cmd.Parameters.AddWithValue("@FriendlyName", FriendlyName);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.ExecuteNonQuery();
                }

                var arrManageKeys = Strings.Split(ManageKeys, ",");

                if (arrManageKeys != null)
                {
                    for (var i = 0; i <= Information.UBound(arrManageKeys); i++)
                    {
                        if (SepFunctions.toLong(arrManageKeys[i].Replace("|", string.Empty)) != 2)
                        {
                            Members.Member_Add_Access_Key(SepFunctions.toLong(Strings.Replace(arrManageKeys[i], "|", string.Empty)), UserID);
                        }
                    }
                }

                using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE ModuleID='60' AND UniqueID=@UniqueID AND PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", PortalID);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrWhiteSpace(LogoFileData))
                {
                    using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID,UniqueID,PortalID,UserID,ModuleID,FileName,FileSize,ContentType,isTemp,Approved,DatePosted,FileData) VALUES(@UploadID,@UniqueID,@PortalID,@UserID,'60',@FileName,@FileSize,@ContentType,'0','1',@DatePosted,@FileData)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@UniqueID", PortalID);
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@FileName", LogoFileName);
                        cmd.Parameters.AddWithValue("@FileSize", LogoFileData.Length);
                        cmd.Parameters.AddWithValue("@ContentType", LogoImageType);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.Parameters.AddWithValue("@FileData", SepFunctions.StringToBytes(SepFunctions.Base64Decode(LogoFileData)));
                        cmd.ExecuteNonQuery();
                    }
                }

                string GetEnable;
                if (bUpdate == false)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM PortalScripts WHERE PortalID=0", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                using (var cmd2 = new SqlCommand("INSERT INTO PortalScripts (PortalID,ScriptName,ScriptText,ScriptType) VALUES(@PortalID, '" + SepFunctions.openNull(RS["ScriptName"], true) + "', '" + SepFunctions.openNull(RS["ScriptText"], true) + "', '" + SepFunctions.openNull(RS["ScriptType"], true) + "')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    if (PlanID > 0)
                    {
                        var dPortals = Portals_Plan_Get(PlanID);
                        PlanModuleIDs = dPortals.ModuleIDs;
                    }

                    using (var cmd = new SqlCommand("SELECT * FROM PortalPages WHERE PortalID=0", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                var pageId = SepFunctions.toLong(SepFunctions.openNull(RS["PageID"]));
                                if (PlanID > 0)
                                {
                                    if (pageId == 4 || pageId == 16 || pageId == 17 || pageId == 30 || pageId == 11 || pageId == 33 || pageId == 21 || pageId == 29 || pageId == 34 || Strings.InStr(PlanModuleIDs, "|" + pageId + "|") > 0)
                                    {
                                        GetEnable = "1";
                                    }
                                    else
                                    {
                                        GetEnable = "-2";
                                    }
                                }
                                else
                                {
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 1)
                                    {
                                        GetEnable = "1";
                                    }
                                    else
                                    {
                                        GetEnable = "0";
                                    }
                                }

                                using (var cmd2 = new SqlCommand("INSERT INTO PortalPages (UniqueID,PortalID,PageID,LinkText,PageText,MenuID,Keywords,Weight,Status,UserPageName,TargetWindow) VALUES('" + SepFunctions.GetIdentity() + "', @PortalID, '" + SepFunctions.openNull(RS["PageID"], true) + "', '" + SepFunctions.openNull(RS["LinkText"], true) + "', '" + SepFunctions.openNull(RS["PageText"], true) + "', '" + SepFunctions.openNull(RS["MenuID"], true) + "', '', '0', '" + GetEnable + "', '" + SepFunctions.openNull(RS["UserPageName"], true) + "', '')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                    strXML += "<ROOTLEVEL>" + Environment.NewLine;

                    strXML += "<AdminInfo>" + Environment.NewLine;
                    strXML += "<FullName>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName")) + "</FullName>" + Environment.NewLine;
                    strXML += "<EmailAddress>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("EmailAddress")) + "</EmailAddress>" + Environment.NewLine;
                    strXML += "<CompanyName></CompanyName>" + Environment.NewLine;
                    strXML += "<Street>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("StreetAddress")) + "</Street>" + Environment.NewLine;
                    strXML += "<City>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("City")) + "</City>" + Environment.NewLine;
                    strXML += "<State>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("State")) + "</State>" + Environment.NewLine;
                    strXML += "<PostalCode>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("ZipCode")) + "</PostalCode>" + Environment.NewLine;
                    strXML += "<Country>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("Country")) + "</Country>" + Environment.NewLine;
                    strXML += "</AdminInfo>" + Environment.NewLine;

                    strXML += "<SiteSetup>" + Environment.NewLine;
                    strXML += "<SiteCounter>Yes</SiteCounter>" + Environment.NewLine;
                    strXML += "<SiteLang>" + SepFunctions.HTMLEncode(Language) + "</SiteLang>" + Environment.NewLine;
                    strXML += "</SiteSetup>" + Environment.NewLine;

                    strXML += "<SiteLayout>" + Environment.NewLine;
                    strXML += "<SiteMenu1>" + SepFunctions.HTMLEncode("Site Menu 1") + "</SiteMenu1>" + Environment.NewLine;
                    strXML += "<SiteMenu2>" + SepFunctions.HTMLEncode("Site Menu 2") + "</SiteMenu2>" + Environment.NewLine;
                    strXML += "<SiteMenu3>" + SepFunctions.HTMLEncode("Site Menu 3") + "</SiteMenu3>" + Environment.NewLine;
                    strXML += "<SiteMenu4>" + SepFunctions.HTMLEncode("Site Menu 4") + "</SiteMenu4>" + Environment.NewLine;
                    strXML += "<SiteMenu5>" + SepFunctions.HTMLEncode("Site Menu 5") + "</SiteMenu5>" + Environment.NewLine;
                    strXML += "<SiteMenu6>" + SepFunctions.HTMLEncode("Site Menu 6") + "</SiteMenu6>" + Environment.NewLine;
                    strXML += "<SiteMenu7>" + SepFunctions.HTMLEncode("Site Menu 7") + "</SiteMenu7>" + Environment.NewLine;
                    strXML += "<WebSiteLayout>" + SepFunctions.HTMLEncode(Template) + "</WebSiteLayout>" + Environment.NewLine;
                    strXML += "</SiteLayout>" + Environment.NewLine;

                    strXML += "<PaymentGateway>" + Environment.NewLine;
                    strXML += "<UseMasterPortal>1</UseMasterPortal>" + Environment.NewLine;
                    strXML += "<AuthorizeNet></AuthorizeNet>" + Environment.NewLine;
                    strXML += "<PayPal></PayPal>" + Environment.NewLine;
                    strXML += "<GoogleMerchantID></GoogleMerchantID>" + Environment.NewLine;
                    strXML += "<eSelectAPIToken></eSelectAPIToken>" + Environment.NewLine;
                    strXML += "<eSelectStoreID></eSelectStoreID>" + Environment.NewLine;
                    strXML += "<CheckEmailTo></CheckEmailTo>" + Environment.NewLine;
                    strXML += "<CheckAddress></CheckAddress>" + Environment.NewLine;
                    strXML += "<CheckInstructions></CheckInstructions>" + Environment.NewLine;
                    strXML += "<MSPAccountID></MSPAccountID>" + Environment.NewLine;
                    strXML += "<MSPSiteID></MSPSiteID>" + Environment.NewLine;
                    strXML += "<MSPSiteCode></MSPSiteCode>" + Environment.NewLine;
                    strXML += "</PaymentGateway>" + Environment.NewLine;

                    strXML += "</ROOTLEVEL>" + Environment.NewLine;

                    using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                    {
                        outfile.Write(strXML);
                    }
                }
                else
                {
                    if (PlanID > 0)
                    {
                        var dPortals = Portals_Plan_Get(PlanID);
                        PlanModuleIDs = dPortals.ModuleIDs;

                        using (var cmd = new SqlCommand("SELECT PageID FROM PortalPages WHERE PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    var pageId = SepFunctions.toLong(SepFunctions.openNull(RS["PageID"]));
                                    if (pageId >= 200 || pageId == 4 || pageId == 16 || pageId == 17 || pageId == 30 || pageId == 11 || pageId == 33 || pageId == 21 || pageId == 29 || pageId == 34 || Strings.InStr(PlanModuleIDs, "|" + pageId + "|") > 0)
                                    {
                                        GetEnable = "1";
                                    }
                                    else
                                    {
                                        GetEnable = "-2";
                                    }

                                    using (var cmd2 = new SqlCommand("UPDATE PortalPages SET Status=@Status WHERE PageID=@PageID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@Status", GetEnable);
                                        cmd2.Parameters.AddWithValue("@PageID", pageId);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        SepFunctions.Cache_Remove();
                    }

                    if (File.Exists(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                    {
                        using (var readfile = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                        {
                            strXML = readfile.ReadToEnd();
                            strXML = Strings.Replace(strXML, "<WebSiteLayout>" + SepFunctions.ParseXML("WebSiteLayout", strXML) + "</WebSiteLayout>", "<WebSiteLayout>" + Template + "</WebSiteLayout>");
                        }

                        if (!string.IsNullOrWhiteSpace(strXML))
                        {
                            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                            {
                                outfile.Write(strXML);
                            }
                        }
                    }
                    else
                    {
                        strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                        strXML += "<ROOTLEVEL>" + Environment.NewLine;

                        strXML += "<AdminInfo>" + Environment.NewLine;
                        strXML += "<FullName>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName")) + "</FullName>" + Environment.NewLine;
                        strXML += "<EmailAddress>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("EmailAddress")) + "</EmailAddress>" + Environment.NewLine;
                        strXML += "<CompanyName></CompanyName>" + Environment.NewLine;
                        strXML += "<Street>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("StreetAddress")) + "</Street>" + Environment.NewLine;
                        strXML += "<City>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("City")) + "</City>" + Environment.NewLine;
                        strXML += "<State>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("State")) + "</State>" + Environment.NewLine;
                        strXML += "<PostalCode>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("ZipCode")) + "</PostalCode>" + Environment.NewLine;
                        strXML += "<Country>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("Country")) + "</Country>" + Environment.NewLine;
                        strXML += "</AdminInfo>" + Environment.NewLine;

                        strXML += "<SiteSetup>" + Environment.NewLine;
                        strXML += "<SiteCounter>Yes</SiteCounter>" + Environment.NewLine;
                        strXML += "<SiteLang>" + SepFunctions.HTMLEncode(Language) + "</SiteLang>" + Environment.NewLine;
                        strXML += "</SiteSetup>" + Environment.NewLine;

                        strXML += "<SiteLayout>" + Environment.NewLine;
                        strXML += "<SiteMenu1>" + SepFunctions.HTMLEncode("Site Menu 1") + "</SiteMenu1>" + Environment.NewLine;
                        strXML += "<SiteMenu2>" + SepFunctions.HTMLEncode("Site Menu 2") + "</SiteMenu2>" + Environment.NewLine;
                        strXML += "<SiteMenu3>" + SepFunctions.HTMLEncode("Site Menu 3") + "</SiteMenu3>" + Environment.NewLine;
                        strXML += "<SiteMenu4>" + SepFunctions.HTMLEncode("Site Menu 4") + "</SiteMenu4>" + Environment.NewLine;
                        strXML += "<SiteMenu5>" + SepFunctions.HTMLEncode("Site Menu 5") + "</SiteMenu5>" + Environment.NewLine;
                        strXML += "<SiteMenu6>" + SepFunctions.HTMLEncode("Site Menu 6") + "</SiteMenu6>" + Environment.NewLine;
                        strXML += "<SiteMenu7>" + SepFunctions.HTMLEncode("Site Menu 7") + "</SiteMenu7>" + Environment.NewLine;
                        strXML += "<WebSiteLayout>" + SepFunctions.HTMLEncode(Template) + "</WebSiteLayout>" + Environment.NewLine;
                        strXML += "</SiteLayout>" + Environment.NewLine;

                        strXML += "<PaymentGateway>" + Environment.NewLine;
                        strXML += "<UseMasterPortal>1</UseMasterPortal>" + Environment.NewLine;
                        strXML += "<AuthorizeNet></AuthorizeNet>" + Environment.NewLine;
                        strXML += "<PayPal></PayPal>" + Environment.NewLine;
                        strXML += "<GoogleMerchantID></GoogleMerchantID>" + Environment.NewLine;
                        strXML += "<eSelectAPIToken></eSelectAPIToken>" + Environment.NewLine;
                        strXML += "<eSelectStoreID></eSelectStoreID>" + Environment.NewLine;
                        strXML += "<CheckEmailTo></CheckEmailTo>" + Environment.NewLine;
                        strXML += "<CheckAddress></CheckAddress>" + Environment.NewLine;
                        strXML += "<CheckInstructions></CheckInstructions>" + Environment.NewLine;
                        strXML += "<MSPAccountID></MSPAccountID>" + Environment.NewLine;
                        strXML += "<MSPSiteID></MSPSiteID>" + Environment.NewLine;
                        strXML += "<MSPSiteCode></MSPSiteCode>" + Environment.NewLine;
                        strXML += "</PaymentGateway>" + Environment.NewLine;

                        strXML += "</ROOTLEVEL>" + Environment.NewLine;

                        using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                        {
                            outfile.Write(strXML);
                        }
                    }
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 60, Strings.ToString(PortalID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Portal", "Portals", string.Empty);
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
        /// Portalses the plan delete.
        /// </summary>
        /// <param name="PlanIDs">The plan i ds.</param>
        /// <returns>System.String.</returns>
        public static string Portals_Plan_Delete(string PlanIDs)
        {
            // Check Requirements
            if (string.IsNullOrWhiteSpace(PlanIDs))
            {
                return SepFunctions.LangText("PlanIDs is required");
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrPlanIDs = Strings.Split(PlanIDs, ",");

                if (arrPlanIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrPlanIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ShopProducts SET Status='-1', DateDeleted=@DateDeleted WHERE ProductID=@ProductID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", arrPlanIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(60, arrPlanIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Pricing Plan(s) has been successfully deleted.");
        }

        /// <summary>
        /// Portalses the plan get.
        /// </summary>
        /// <param name="PlanID">The plan identifier.</param>
        /// <returns>Models.PortalsPrices.</returns>
        public static PortalsPrices Portals_Plan_Get(long PlanID)
        {
            var returnXML = new Models.PortalsPrices();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ProductID=@PlanID AND ModuleID='60' AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", PlanID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.PlanID = SepFunctions.toLong(SepFunctions.openNull(RS["ProductID"]));
                            returnXML.PlanName = SepFunctions.openNull(RS["ProductName"]);
                            returnXML.Description = SepFunctions.openNull(RS["ShortDesc"]);
                            returnXML.OnetimePrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["UnitPrice"]));
                            returnXML.RecurringPrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["RecurringPrice"]));
                            returnXML.RecurringCycle = SepFunctions.openNull(RS["RecurringCycle"]);
                            returnXML.ModuleIDs = SepFunctions.openNull(RS["Description"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Portalses the plan save.
        /// </summary>
        /// <param name="PlanID">The plan identifier.</param>
        /// <param name="PlanName">Name of the plan.</param>
        /// <param name="Description">The description.</param>
        /// <param name="OnetimePrice">The onetime price.</param>
        /// <param name="RecurringPrice">The recurring price.</param>
        /// <param name="RecurringCycle">The recurring cycle.</param>
        /// <param name="ModuleIDs">The module i ds.</param>
        /// <returns>System.String.</returns>
        public static string Portals_Plan_Save(long PlanID, string PlanName, string Description, string OnetimePrice, string RecurringPrice, string RecurringCycle, string ModuleIDs)
        {
            var bUpdate = false;

            // Check Requirements
            if (string.IsNullOrWhiteSpace(PlanName))
            {
                return SepFunctions.LangText("PlanName is required");
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                return SepFunctions.LangText("Description is required");
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (PlanID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ProductID=@PlanID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlanID", PlanID);
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
                    PlanID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ShopProducts SET ProductName=@ProductName, UnitPrice=@UnitPrice, ShortDesc=@ShortDesc, Description=@Description, RecurringPrice=@RecurringPrice, RecurringCycle=@RecurringCycle WHERE ProductID=@PlanID";
                }
                else
                {
                    SqlStr = "INSERT INTO ShopProducts (ProductID,ProductName,UnitPrice,CatID,PortalID,SalePrice,ModuleID,AffiliateUnitPrice,AffiliateRecurringPrice,ExcludeAffiliate,Status,UseInventory,Inventory,ShortDesc,Description,RecurringPrice,RecurringCycle,DatePosted) VALUES (@PlanID, @ProductName, @UnitPrice, '0', '0', '0', '60', '0', '0', '0', '1', '0', '0', @ShortDesc, @Description, @RecurringPrice, @RecurringCycle, @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", PlanID);
                    cmd.Parameters.AddWithValue("@ProductName", PlanName);
                    cmd.Parameters.AddWithValue("@UnitPrice", SepFunctions.toDecimal(OnetimePrice));
                    cmd.Parameters.AddWithValue("@ShortDesc", Description);
                    cmd.Parameters.AddWithValue("@Description", ModuleIDs);
                    cmd.Parameters.AddWithValue("@RecurringPrice", SepFunctions.toDecimal(RecurringPrice));
                    cmd.Parameters.AddWithValue("@RecurringCycle", RecurringCycle);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Pricing Plan has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Pricing Plan has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Validates the name of the friendly.
        /// </summary>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="FriendlyName">Name of the friendly.</param>
        /// <returns>System.String.</returns>
        public static string Validate_Friendly_Name(long PortalID, string FriendlyName)
        {
            var returnXML = string.Empty;

            if (!string.IsNullOrWhiteSpace(FriendlyName))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                using (var cmd2 = new SqlCommand("SELECT PortalID FROM Portals WHERE FriendlyName=@FriendlyName", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@FriendlyName", FriendlyName);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            returnXML = SepFunctions.LangText("Friendly Name alright exists.");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (var cmd2 = new SqlCommand("SELECT PortalID FROM Portals WHERE PortalID <> @PortalID AND FriendlyName=@FriendlyName", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                    cmd2.Parameters.AddWithValue("@FriendlyName", FriendlyName);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            returnXML = SepFunctions.LangText("Friendly Name alright exists.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return returnXML;
        }
    }
}