// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Businesses.cs" company="SepCity, Inc.">
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
    /// Class Businesses.
    /// </summary>
    public static class Businesses
    {
        /// <summary>
        /// Businesses the change status.
        /// </summary>
        /// <param name="BusinessIDs">The business i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Business_Change_Status(string BusinessIDs, int Status)
        {
            var bError = string.Empty;

            var arrBusinessIDs = Strings.Split(BusinessIDs, ",");

            if (arrBusinessIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrBusinessIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE BusinessListings SET Status=@Status WHERE LinkID=@LinkID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LinkID", arrBusinessIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
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

            sReturn = SepFunctions.LangText("Business(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Businesses the delete.
        /// </summary>
        /// <param name="BusinessIDs">The business i ds.</param>
        /// <returns>System.String.</returns>
        public static string Business_Delete(string BusinessIDs)
        {
            var bError = string.Empty;

            var arrBusinessIDs = Strings.Split(BusinessIDs, ",");

            if (arrBusinessIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrBusinessIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE BusinessListings SET Status='-1', DateDeleted=@DateDeleted WHERE LinkID=@LinkID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LinkID", arrBusinessIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(20, arrBusinessIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Business(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Businesses the get.
        /// </summary>
        /// <param name="BusinessID">The business identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.Businesses.</returns>
        public static Models.Businesses Business_Get(long BusinessID, long ChangeID = 0)
        {
            var returnXML = new Models.Businesses();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE BusinessListings SET Visits=Visits+1 WHERE BusinessID=@BusinessID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessID", BusinessID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM BusinessListings WHERE BusinessID=@BusinessID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessID", BusinessID);
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
                                returnXML.BusinessID = SepFunctions.toLong(SepFunctions.openNull(RS["BusinessID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.BusinessName = SepFunctions.openNull(RS["BusinessName"]);
                                returnXML.SiteURL = SepFunctions.Format_URL(SepFunctions.openNull(RS["SiteURL"]));
                                returnXML.Description = SepFunctions.openNull(RS["Description"]);
                                returnXML.FullDescription = SepFunctions.openNull(RS["FullDescription"]);
                                returnXML.ContactEmail = SepFunctions.openNull(RS["ContactEmail"]);
                                returnXML.PhoneNumber = SepFunctions.openNull(RS["PhoneNumber"]);
                                returnXML.FaxNumber = SepFunctions.openNull(RS["FaxNumber"]);
                                if (SepFunctions.openBoolean(RS["IncludeProfile"]) || SepFunctions.Setup(20, "BusinessUserAddress") == "Yes")
                                {
                                    returnXML.Address = SepFunctions.GetUserInformation("StreetAddress", SepFunctions.openNull(RS["UserID"]));
                                    returnXML.City = SepFunctions.GetUserInformation("City", SepFunctions.openNull(RS["UserID"]));
                                    returnXML.State = SepFunctions.GetUserInformation("State", SepFunctions.openNull(RS["UserID"]));
                                    returnXML.ZipCode = SepFunctions.GetUserInformation("ZipCode", SepFunctions.openNull(RS["UserID"]));
                                    returnXML.Country = SepFunctions.GetUserInformation("Country", SepFunctions.openNull(RS["UserID"]));
                                }
                                else
                                {
                                    returnXML.Address = SepFunctions.openNull(RS["Address"]);
                                    returnXML.City = SepFunctions.openNull(RS["City"]);
                                    returnXML.State = SepFunctions.openNull(RS["State"]);
                                    returnXML.ZipCode = SepFunctions.openNull(RS["ZipCode"]);
                                    returnXML.Country = SepFunctions.openNull(RS["Country"]);
                                }

                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.Visits = SepFunctions.toLong(SepFunctions.openNull(RS["Visits"]));
                                returnXML.LinkID = SepFunctions.toLong(SepFunctions.openNull(RS["LinkID"]));
                                returnXML.ClaimID = SepFunctions.toLong(SepFunctions.openNull(RS["ClaimID"]));
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.TwitterLink = SepFunctions.Format_URL(SepFunctions.openNull(RS["TwitterLink"]));
                                returnXML.FacebookLink = SepFunctions.Format_URL(SepFunctions.openNull(RS["FacebookLink"]));
                                returnXML.LinkedInLink = SepFunctions.Format_URL(SepFunctions.openNull(RS["LinkedInLink"]));
                                returnXML.OfficeHours = SepFunctions.openNull(RS["OfficeHours"]);
                                returnXML.IncludeProfile = SepFunctions.openBoolean(RS["IncludeProfile"]);
                                returnXML.IncludeMap = SepFunctions.openBoolean(RS["IncludeMap"]);
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Businesses the save.
        /// </summary>
        /// <param name="BusinessID">The business identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="BusinessName">Name of the business.</param>
        /// <param name="ContactEmail">The contact email.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="FaxNumber">The fax number.</param>
        /// <param name="SiteURL">The site URL.</param>
        /// <param name="Description">The description.</param>
        /// <param name="Address">The address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="ZipCode">The zip code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="FullDescription">The full description.</param>
        /// <param name="TwitterLink">The twitter link.</param>
        /// <param name="FacebookLink">The facebook link.</param>
        /// <param name="LinkedInLink">The linked in link.</param>
        /// <param name="OfficeHours">The office hours.</param>
        /// <param name="IncludeProfile">if set to <c>true</c> [include profile].</param>
        /// <param name="IncludeMap">if set to <c>true</c> [include map].</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <returns>System.Int32.</returns>
        public static int Business_Save(long BusinessID, string UserID, long CatID, string BusinessName, string ContactEmail, string PhoneNumber, string FaxNumber, string SiteURL, string Description, string Address, string City, string State, string ZipCode, string Country, string FullDescription, string TwitterLink, string FacebookLink, string LinkedInLink, string OfficeHours, bool IncludeProfile, bool IncludeMap, long PortalID, bool Approved)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (BusinessID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM BusinessListings WHERE BusinessID=@BusinessID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BusinessID", BusinessID);
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
                    BusinessID = SepFunctions.GetIdentity();
                }

                if (SepFunctions.Setup(20, "BusinessUserAddress") == "Yes" || IncludeProfile)
                {
                    Address = SepFunctions.GetUserInformation("StreetAddress", UserID);
                    City = SepFunctions.GetUserInformation("City", UserID);
                    State = SepFunctions.GetUserInformation("State", UserID);
                    ZipCode = SepFunctions.GetUserInformation("ZipCode", UserID);
                    Country = SepFunctions.GetUserInformation("Country", UserID);
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE BusinessListings SET CatID=@CatID, BusinessName=@BusinessName, ContactEmail=@ContactEmail, PhoneNumber=@PhoneNumber, FaxNumber=@FaxNumber, SiteURL=@SiteURL, Description=@Description, Address=@Address, City=@City, State=@State, ZipCode=@ZipCode, Country=@Country, FullDescription=@FullDescription, TwitterLink=@TwitterLink, FacebookLink=@FacebookLink, LinkedInLink=@LinkedInLink, OfficeHours=@OfficeHours, IncludeProfile=@IncludeProfile, IncludeMap=@IncludeMap WHERE BusinessID=@BusinessID";
                }
                else
                {
                    SqlStr = "INSERT INTO BusinessListings (BusinessID, CatID, BusinessName, ContactEmail, PhoneNumber, FaxNumber, SiteURL, Description, Address, City, State, ZipCode, Country, FullDescription, TwitterLink, FacebookLink, LinkedInLink, OfficeHours, IncludeProfile, IncludeMap, PortalID, Status, Visits, DatePosted, LinkID, UserID) VALUES (@BusinessID, @CatID, @BusinessName, @ContactEmail, @PhoneNumber, @FaxNumber, @SiteURL, @Description, @Address, @City, @State, @ZipCode, @Country, @FullDescription, @TwitterLink, @FacebookLink, @LinkedInLink, @OfficeHours, @IncludeProfile, @IncludeMap, @PortalID, @Status, '0', @DatePosted, @LinkID, @UserID)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessID", BusinessID);
                    cmd.Parameters.AddWithValue("@LinkID", BusinessID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
                    cmd.Parameters.AddWithValue("@ContactEmail", ContactEmail);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    cmd.Parameters.AddWithValue("@FaxNumber", FaxNumber);
                    cmd.Parameters.AddWithValue("@SiteURL", SepFunctions.Format_URL(SiteURL));
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Address", !string.IsNullOrWhiteSpace(Address) ? Address : string.Empty);
                    cmd.Parameters.AddWithValue("@City", !string.IsNullOrWhiteSpace(City) ? City : string.Empty);
                    cmd.Parameters.AddWithValue("@State", !string.IsNullOrWhiteSpace(State) ? State : string.Empty);
                    cmd.Parameters.AddWithValue("@ZipCode", !string.IsNullOrWhiteSpace(ZipCode) ? ZipCode : string.Empty);
                    cmd.Parameters.AddWithValue("@Country", !string.IsNullOrWhiteSpace(Country) ? Country : string.Empty);
                    cmd.Parameters.AddWithValue("@FullDescription", !string.IsNullOrWhiteSpace(FullDescription) ? FullDescription : string.Empty);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@TwitterLink", SepFunctions.Format_URL(TwitterLink));
                    cmd.Parameters.AddWithValue("@FacebookLink", SepFunctions.Format_URL(FacebookLink));
                    cmd.Parameters.AddWithValue("@LinkedInLink", SepFunctions.Format_URL(LinkedInLink));
                    cmd.Parameters.AddWithValue("@OfficeHours", OfficeHours);
                    cmd.Parameters.AddWithValue("@IncludeProfile", IncludeProfile);
                    cmd.Parameters.AddWithValue("@IncludeMap", IncludeMap);
                    cmd.Parameters.AddWithValue("@Status", Approved ? "1" : "0");
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 20, Strings.ToString(BusinessID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Businesses", "Business Directory", "PostBusiness");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM BusinessListings WHERE BusinessID=@BusinessID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BusinessID", BusinessID);
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
                        SepFunctions.Update_Change_Log(20, Strings.ToString(BusinessID), BusinessName, Strings.ToString(writeLog));
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
        /// Gets the businesses.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.Businesses&gt;.</returns>
        public static List<Models.Businesses> GetBusinesses(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "", string userId = "", long CategoryId = -1, string StartDate = "")
        {
            var lBusinesses = new List<Models.Businesses>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Mod.DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "Mod.LinkID=Mod.BusinessID AND Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Mod.BusinessName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(userId) + "'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.BusinessID,Mod.CatID,Mod.BusinessName,Mod.Description,Mod.Visits,Mod.Status,Mod.DatePosted,(SELECT Count(CommentID) FROM Comments WHERE ModuleID='20' AND UniqueID=Mod.BusinessID) AS TotalComments,(SELECT TOP 1 Featured FROM PricingOptions WHERE ModuleID='20' AND UniqueID=Mod.LinkID) AS Featured,(SELECT TOP 1 BoldTitle FROM PricingOptions WHERE ModuleID='20' AND UniqueID=Mod.LinkID) AS BoldTitle,(SELECT TOP 1 Highlight FROM PricingOptions WHERE ModuleID='20' AND UniqueID=Mod.LinkID) AS Highlight FROM BusinessListings AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dBusinesses = new Models.Businesses { BusinessID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["BusinessID"])) };
                    dBusinesses.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dBusinesses.BusinessName = SepFunctions.openNull(ds.Tables[0].Rows[i]["BusinessName"]);
                    dBusinesses.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dBusinesses.Visits = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Visits"]));
                    dBusinesses.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dBusinesses.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dBusinesses.TotalComments = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalComments"]));
                    dBusinesses.Featured = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Featured"]);
                    dBusinesses.BoldTitle = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["BoldTitle"]);
                    dBusinesses.Highlight = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Highlight"]);
                    lBusinesses.Add(dBusinesses);
                }
            }

            return lBusinesses;
        }
    }
}