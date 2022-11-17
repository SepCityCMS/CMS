// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Advertisements.cs" company="SepCity, Inc.">
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
    /// Class Advertisements.
    /// </summary>
    public static class Advertisements
    {
        /// <summary>
        /// Advertisements the delete.
        /// </summary>
        /// <param name="AdIDs">The ad i ds.</param>
        /// <returns>System.String.</returns>
        public static string Advertisement_Delete(string AdIDs)
        {
            var bError = string.Empty;

            var sReturn = string.Empty;

            // Check Requirements
            if (string.IsNullOrWhiteSpace(AdIDs))
            {
                sReturn = SepFunctions.LangText("AdIDs is required");

                return sReturn;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAdIDs = Strings.Split(AdIDs, ",");

                if (arrAdIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAdIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Advertisements SET Status='-1', DateDeleted=@DateDeleted WHERE AdID=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", arrAdIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(2, arrAdIDs[i]);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Advertisement(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Advertisements the get.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        /// <returns>Models.Advertisements.</returns>
        public static Models.Advertisements Advertisement_Get(long AdID)
        {
            var returnXML = new Models.Advertisements();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Advertisements WHERE AdID=@AdID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.AdID = SepFunctions.toLong(SepFunctions.openNull(RS["AdID"]));
                            returnXML.MaximumClicks = SepFunctions.toLong(SepFunctions.openNull(RS["MaxClicks"]));
                            returnXML.MaximumExposures = SepFunctions.toLong(SepFunctions.openNull(RS["MaxExposures"]));
                            returnXML.TotalClicks = SepFunctions.toLong(SepFunctions.openNull(RS["TotalClicks"]));
                            returnXML.TotalExposures = SepFunctions.toLong(SepFunctions.openNull(RS["TotalExposures"]));
                            returnXML.ImageData = Strings.ToString(Information.IsDBNull(RS["ImageData"]) ? string.Empty : SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["ImageData"])));

                            returnXML.ImageType = SepFunctions.openNull(RS["ImageType"]);
                            returnXML.ImageURL = SepFunctions.openNull(RS["ImageURL"]);
                            returnXML.SiteURL = SepFunctions.openNull(RS["SiteURL"]);
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.ZoneID = SepFunctions.toLong(SepFunctions.openNull(RS["ZoneID"]));
                            returnXML.CatIDs = SepFunctions.openNull(RS["CatIDs"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.Country = SepFunctions.openNull(RS["Country"]);
                            returnXML.State = SepFunctions.openNull(RS["State"]);
                            returnXML.PageIDs = SepFunctions.openNull(RS["PageIDs"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.UseHTML = SepFunctions.toBoolean(SepFunctions.openNull(RS["UseHTML"]));
                            returnXML.Status = SepFunctions.openNull(RS["Status"]);
                            returnXML.HTMLCode = SepFunctions.openNull(RS["HTMLCode"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.StartDate = SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"]));
                            returnXML.EndDate = SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Advertisements the price delete.
        /// </summary>
        /// <param name="PlanIDs">The plan i ds.</param>
        /// <returns>System.String.</returns>
        public static string Advertisement_Price_Delete(string PlanIDs)
        {
            var bError = string.Empty;

            var sReturn = string.Empty;

            // Check Requirements
            if (string.IsNullOrWhiteSpace(PlanIDs))
            {
                sReturn = SepFunctions.LangText("PlanIDs is required");

                return sReturn;
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

                        SepFunctions.Additional_Data_Delete(2, arrPlanIDs[i]);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Advertisement Price(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Advertisements the price get.
        /// </summary>
        /// <param name="PriceID">The price identifier.</param>
        /// <returns>Models.AdvertisementPrices.</returns>
        public static AdvertisementPrices Advertisement_Price_Get(long PriceID)
        {
            var returnXML = new Models.AdvertisementPrices();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ProductID=@PriceID AND ModuleID='2' AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PriceID", PriceID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var GetShortDesc = SepFunctions.openNull(RS["ShortDesc"]);
                            returnXML.PriceID = SepFunctions.toLong(SepFunctions.openNull(RS["ProductID"]));
                            returnXML.PlanName = SepFunctions.openNull(RS["ProductName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.OnetimePrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["UnitPrice"]));
                            returnXML.RecurringPrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["RecurringPrice"]));
                            returnXML.RecurringCycle = SepFunctions.openNull(RS["RecurringCycle"]);
                            returnXML.Zones = SepFunctions.ParseXML("ZONE", GetShortDesc);
                            returnXML.Pages = SepFunctions.ParseXML("TARGETPAGE", GetShortDesc);
                            returnXML.Categories = SepFunctions.ParseXML("TARGETCAT", GetShortDesc);
                            returnXML.MaximumClicks = SepFunctions.toLong(SepFunctions.ParseXML("MAXCLICKS", GetShortDesc));
                            returnXML.MaximumExposures = SepFunctions.toLong(SepFunctions.ParseXML("MAXEXPOSURES", GetShortDesc));
                            returnXML.Portals = SepFunctions.ParseXML("TARGETPORTAL", GetShortDesc);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Advertisements the price save.
        /// </summary>
        /// <param name="PriceID">The price identifier.</param>
        /// <param name="PlanName">Name of the plan.</param>
        /// <param name="Description">The description.</param>
        /// <param name="OnetimePrice">The onetime price.</param>
        /// <param name="RecurringPrice">The recurring price.</param>
        /// <param name="RecurringCycle">The recurring cycle.</param>
        /// <param name="MaximumClicks">The maximum clicks.</param>
        /// <param name="MaximumExposures">The maximum exposures.</param>
        /// <param name="Zones">The zones.</param>
        /// <param name="Categories">The categories.</param>
        /// <param name="Pages">The pages.</param>
        /// <param name="Portals">The portals.</param>
        /// <returns>System.String.</returns>
        public static string Advertisement_Price_Save(long PriceID, string PlanName, string Description, decimal OnetimePrice, decimal RecurringPrice, string RecurringCycle, string MaximumClicks, string MaximumExposures, string Zones, string Categories, string Pages, string Portals)
        {
            var bUpdate = false;

            var sReturn = string.Empty;

            // Check Requirements
            if (string.IsNullOrWhiteSpace(PlanName))
            {
                sReturn = SepFunctions.LangText("PlanName is required");

                return sReturn;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                sReturn = SepFunctions.LangText("Description is required");

                return sReturn;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (PriceID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ProductID=@PriceID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PriceID", PriceID);
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
                    PriceID = SepFunctions.GetIdentity();
                }

                var GetShortDesc = "<ZONE>" + Zones + "</ZONE>";
                GetShortDesc += "<TARGETPAGE>" + Strings.ToString(SepFunctions.toLong(Pages) == 1 ? "|-2|" : Pages) + "</TARGETPAGE>";
                GetShortDesc += "<TARGETCAT>" + Strings.ToString(SepFunctions.toLong(Categories) == 1 ? "|-2|" : Categories) + "</TARGETCAT>";
                GetShortDesc += "<MAXCLICKS>" + MaximumClicks + "</MAXCLICKS>";
                GetShortDesc += "<MAXEXPOSURES>" + MaximumExposures + "</MAXEXPOSURES>";
                GetShortDesc += "<TARGETPORTAL>" + Portals + "</TARGETPORTAL>";

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ShopProducts SET ModelNumber=@ModelNumber, ProductName=@ProductName, UnitPrice=@UnitPrice, ShortDesc=@ShortDesc, Description=@Description, RecurringPrice=@RecurringPrice, RecurringCycle=@RecurringCycle WHERE ProductID=@PriceID";
                }
                else
                {
                    SqlStr = "INSERT INTO ShopProducts (ProductID,ModelNumber,ProductName,UnitPrice,CatID,PortalID,SalePrice,ModuleID,AffiliateUnitPrice,AffiliateRecurringPrice,ExcludeAffiliate,Status,UseInventory,Inventory,ShortDesc,Description,RecurringPrice,RecurringCycle,DatePosted) VALUES (@PriceID, @ModelNumber, @ProductName, @UnitPrice, '0', '0', '0', '2', '0', '0', '0', '1', '0', '0', @ShortDesc, @Description, @RecurringPrice, @RecurringCycle, @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@PriceID", PriceID);
                    cmd.Parameters.AddWithValue("@ModelNumber", PriceID);
                    cmd.Parameters.AddWithValue("@ProductName", PlanName);
                    cmd.Parameters.AddWithValue("@UnitPrice", OnetimePrice);
                    cmd.Parameters.AddWithValue("@ShortDesc", GetShortDesc);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@RecurringPrice", RecurringPrice);
                    cmd.Parameters.AddWithValue("@RecurringCycle", RecurringCycle);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Advertisement Price has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Advertisement Price has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Advertisements the save.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="SiteURL">The site URL.</param>
        /// <param name="Description">The description.</param>
        /// <param name="ZoneID">The zone identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="HTMLCode">The HTML code.</param>
        /// <param name="MaximumClicks">The maximum clicks.</param>
        /// <param name="MaximumExposures">The maximum exposures.</param>
        /// <param name="TotalClicks">The total clicks.</param>
        /// <param name="TotalExposures">The total exposures.</param>
        /// <param name="CatIDs">The cat i ds.</param>
        /// <param name="PageIDs">The page i ds.</param>
        /// <param name="Country">The country.</param>
        /// <param name="State">The state.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="ImageData">The image data.</param>
        /// <param name="ImageType">Type of the image.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Advertisement_Save(long AdID, string UserName, string SiteURL, string Description, long ZoneID, DateTime StartDate, DateTime EndDate, string HTMLCode, string MaximumClicks, string MaximumExposures, string TotalClicks, string TotalExposures, string CatIDs, string PageIDs, string Country, string State, string PortalIDs, string ImageData, string ImageType, long Status)
        {
            var bUpdate = false;
            var isNewRecord = false;

            if (string.IsNullOrWhiteSpace(CatIDs))
            {
                CatIDs = "|0|";
            }

            if (string.IsNullOrWhiteSpace(PortalIDs))
            {
                PortalIDs = "|-1|";
            }

            if (string.IsNullOrWhiteSpace(PageIDs))
            {
                PageIDs = "|-1|";
            }

            if (string.IsNullOrWhiteSpace(PortalIDs))
            {
                // -V3022
                PortalIDs = "|-1|";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE Status <> -1 AND UserName=@AdUserName", conn))
                {
                    cmd.Parameters.AddWithValue("@AdUserName", UserName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            UserName = SepFunctions.openNull(RS["UserID"]);
                        }
                        else
                        {
                            var sReturnValue = SepFunctions.LangText("Invalid UserName was entered");
                            return sReturnValue;
                        }
                    }
                }

                if (AdID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT AdID FROM Advertisements WHERE AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", AdID);
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
                    AdID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Advertisements SET UserID=@AdUserName, SiteURL=@SiteURL, Description=@Description, ZoneID=@ZoneID" + Strings.ToString(!string.IsNullOrWhiteSpace(ImageData) ? ", ImageData=@ImageData, ImageType=@ImageType" : string.Empty) + ", StartDate=@StartDate, EndDate=@EndDate, HTMLCode=@HTMLCode, UseHTML=@UseHTML, MaxClicks=@MaximumClicks, MaxExposures=@MaximumExposures, TotalClicks=@TotalClicks, TotalExposures=@TotalExposures, CatIDs=@CatIDs, PageIDs=@PageIDs, PortalIDs=@PortalIDs, Country=@Country, State=@State, Status=@Status WHERE AdID=@AdID";
                }
                else
                {
                    SqlStr = "INSERT INTO Advertisements (AdID, UserID, SiteURL, Description, ZoneID" + Strings.ToString(!string.IsNullOrWhiteSpace(ImageData) ? ", ImageData, ImageType" : string.Empty) + ", StartDate, EndDate, HTMLCode, UseHTML, MaxClicks, MaxExposures, TotalClicks, TotalExposures, CatIDs, PageIDs, PortalIDs, Country, State, Status, DatePosted) VALUES (@AdID, @AdUsername, @SiteURL, @Description, @ZoneID" + Strings.ToString(!string.IsNullOrWhiteSpace(ImageData) ? ", @ImageData, @ImageType" : string.Empty) + ", @StartDate, @EndDate, @HTMLCode, @UseHTML, @MaximumClicks, @MaximumExposures, @TotalClicks, @TotalExposures, @CatIDs, @PageIDs, @PortalIDs, @Country, @State, @Status, @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdID);
                    cmd.Parameters.AddWithValue("@AdUserName", UserName);
                    cmd.Parameters.AddWithValue("@SiteURL", SiteURL);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                    if (!string.IsNullOrWhiteSpace(ImageData))
                    {
                        cmd.Parameters.AddWithValue("@ImageData", SepFunctions.StringToBytes(SepFunctions.Base64Decode(ImageData)));
                        cmd.Parameters.AddWithValue("@ImageType", ImageType);
                    }

                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.Parameters.AddWithValue("@HTMLCode", HTMLCode);
                    if (!string.IsNullOrWhiteSpace(HTMLCode))
                    {
                        cmd.Parameters.AddWithValue("@UseHTML", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UseHTML", "0");
                    }

                    cmd.Parameters.AddWithValue("@MaximumClicks", SepFunctions.toLong(MaximumClicks));
                    cmd.Parameters.AddWithValue("@MaximumExposures", SepFunctions.toLong(MaximumExposures));
                    cmd.Parameters.AddWithValue("@TotalClicks", SepFunctions.toLong(TotalClicks));
                    cmd.Parameters.AddWithValue("@TotalExposures", SepFunctions.toLong(TotalExposures));
                    cmd.Parameters.AddWithValue("@CatIDs", !string.IsNullOrWhiteSpace(CatIDs) ? CatIDs : string.Empty); // -V3022
                    cmd.Parameters.AddWithValue("@PageIDs", !string.IsNullOrWhiteSpace(PageIDs) ? PageIDs : string.Empty); // -V3022
                    cmd.Parameters.AddWithValue("@Country", !string.IsNullOrWhiteSpace(Country) ? Country : string.Empty);
                    cmd.Parameters.AddWithValue("@State", !string.IsNullOrWhiteSpace(State) ? State : string.Empty);
                    cmd.Parameters.AddWithValue("@PortalIDs", !string.IsNullOrWhiteSpace(PortalIDs) ? PortalIDs : string.Empty); // -V3022
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }
            }

            SepFunctions.Additional_Data_Save(isNewRecord, 2, Strings.ToString(AdID), UserName, UserName, "Ad", "Advertisement", string.Empty);

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Advertisement has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Advertisement has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Gets the advertisement prices.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.AdvertisementPrices&gt;.</returns>
        public static List<AdvertisementPrices> GetAdvertisementPrices(string SortExpression = "ProductName", string SortDirection = "ASC", string searchWords = "")
        {
            var lAdvertisementPrices = new List<AdvertisementPrices>();

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
                    using (var cmd = new SqlCommand("SELECT ProductID,ProductName,UnitPrice,RecurringPrice,RecurringCycle FROM ShopProducts WHERE ModuleID='2' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dAdvertisementPrices = new Models.AdvertisementPrices { PriceID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductID"])) };
                    dAdvertisementPrices.PlanName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductName"]);
                    dAdvertisementPrices.OnetimePrice = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"]));
                    dAdvertisementPrices.RecurringPrice = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"]));
                    dAdvertisementPrices.RecurringCycle = SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringCycle"]);
                    lAdvertisementPrices.Add(dAdvertisementPrices);
                }
            }

            return lAdvertisementPrices;
        }

        /// <summary>
        /// Gets the advertisements.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Advertisements&gt;.</returns>
        public static List<Models.Advertisements> GetAdvertisements(string SortExpression = "SiteURL", string SortDirection = "ASC", string searchWords = "")
        {
            var lAdvertisements = new List<Models.Advertisements>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "SiteURL";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (SiteURL LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT AdID,SiteURL,ZoneID,Description,TotalExposures,MaxExposures,TotalClicks,MaxClicks,StartDate,EndDate,ImageData,ImageURL,UseHTML,HTMLCode,(SELECT ZoneName FROM TargetZones WHERE ZoneID=Advertisements.ZoneID) AS ZoneName FROM Advertisements WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dAdvertisements = new Models.Advertisements { AdID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AdID"])) };
                    dAdvertisements.SiteURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteURL"]);
                    dAdvertisements.ZoneID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ZoneID"]));
                    dAdvertisements.ZoneName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ZoneName"]);
                    dAdvertisements.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    try
                    {
                        if (SepFunctions.openBoolean(ds.Tables[0].Rows[i]["UseHTML"]) && !string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["HTMLCode"])))
                        {
                            dAdvertisements.AdvertisementPreview = SepFunctions.openNull(ds.Tables[0].Rows[i]["HTMLCode"]);
                        }
                        else
                        {
                            if (Information.IsDBNull(ds.Tables[0].Rows[i]["ImageData"]))
                            {
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["ImageURL"])))
                                {
                                    dAdvertisements.AdvertisementPreview = "<img src=\"" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ImageURL"]) + "\" border=\"0\" />";
                                }
                            }
                            else
                            {
                                dAdvertisements.AdvertisementPreview = "<img src=\"data:image/png;base64," + SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])ds.Tables[0].Rows[i]["ImageData"])) + "\" border=\"0\" />";
                            }
                        }
                    }
                    catch
                    {
                        if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["ImageURL"])))
                        {
                            dAdvertisements.AdvertisementPreview = "<img src=\"" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ImageURL"]) + "\" border=\"0\" />";
                        }
                    }

                    dAdvertisements.Status = GetAdvertisementsStatus(SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalExposures"])), SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["MaxExposures"])), SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalClicks"])), SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["MaxClicks"])), SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartDate"])), SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndDate"])));
                    lAdvertisements.Add(dAdvertisements);
                }
            }

            return lAdvertisements;
        }

        /// <summary>
        /// Gets the advertisement stats.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>List&lt;Models.Advertisements&gt;.</returns>
        public static List<Models.Advertisements> GetAdvertisementStats(string SortExpression = "StartDate", string SortDirection = "DESC", string searchWords = "", string UserID = "")
        {
            var lAdvertisements = new List<Models.Advertisements>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "StartDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (SiteURL LIKE '%" + SepFunctions.FixWord(searchWords) + "%') ";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT AdID,SiteURL,StartDate,TotalClicks,TotalExposures,MaxClicks FROM Advertisements WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dAdvertisements = new Models.Advertisements { AdID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AdID"])) };
                    if (SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalExposures"])) == 0)
                    {
                        dAdvertisements.Ratio = "0.00%";
                    }
                    else
                    {
                        dAdvertisements.Ratio = (SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalClicks"])) / SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalExposures"])) * 100).ToString("0.00") + "%";
                    }

                    dAdvertisements.TotalExposures = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalExposures"]));
                    dAdvertisements.TotalClicks = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalClicks"]));
                    dAdvertisements.SiteURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteURL"]);
                    dAdvertisements.StartDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartDate"]));
                    dAdvertisements.TotalClicks = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalClicks"]));
                    dAdvertisements.MaximumClicks = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["MaxClicks"]));
                    lAdvertisements.Add(dAdvertisements);
                }
            }

            return lAdvertisements;
        }

        /// <summary>
        /// Gets the advertisements status.
        /// </summary>
        /// <param name="iTotalExposures">The i total exposures.</param>
        /// <param name="iMaxExposures">The i maximum exposures.</param>
        /// <param name="iTotalClicks">The i total clicks.</param>
        /// <param name="iMaxClicks">The i maximum clicks.</param>
        /// <param name="dStartDate">The d start date.</param>
        /// <param name="dEndDate">The d end date.</param>
        /// <returns>System.String.</returns>
        private static string GetAdvertisementsStatus(long iTotalExposures, long iMaxExposures, long iTotalClicks, long iMaxClicks, DateTime dStartDate, DateTime dEndDate)
        {
            if (iTotalExposures >= iMaxExposures && iMaxExposures > -1)
            {
                return "ME";
            }

            if (iTotalClicks >= iMaxClicks && iMaxClicks > -1)
            {
                return "MC";
            }

            if (DateAndTime.DateDiff(DateAndTime.DateInterval.Day, DateTime.Now, dStartDate) >= 0)
            {
                return "SD";
            }

            if (DateAndTime.DateDiff(DateAndTime.DateInterval.Day, dEndDate, DateTime.Now) >= 0)
            {
                return "ED";
            }

            return "AA";
        }
    }
}