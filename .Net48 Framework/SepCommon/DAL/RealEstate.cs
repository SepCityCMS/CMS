// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="RealEstate.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class RealEstate.
    /// </summary>
    public static class RealEstate
    {
        /// <summary>
        /// Agents the delete.
        /// </summary>
        /// <param name="AgentIDs">The agent i ds.</param>
        /// <returns>System.String.</returns>
        public static string Agent_Delete(string AgentIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrAgentIDs = Strings.Split(AgentIDs, ",");

                if (arrAgentIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAgentIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE RStateAgents SET Status='-1', DateDeleted=@DateDeleted WHERE AgentID=@AgentID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AgentID", arrAgentIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Agent(s) has been successfully deleted.");
        }

        /// <summary>
        /// Agents the get.
        /// </summary>
        /// <param name="AgentID">The agent identifier.</param>
        /// <returns>Models.RealEstateAgents.</returns>
        public static RealEstateAgents Agent_Get(long AgentID)
        {
            var returnXML = new Models.RealEstateAgents();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM RStateAgents WHERE AgentID=@AgentID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@AgentID", AgentID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.AgentID = SepFunctions.toLong(SepFunctions.openNull(RS["AgentID"]));
                            returnXML.BrokerID = SepFunctions.toLong(SepFunctions.openNull(RS["BrokerID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.Username = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Agents the save.
        /// </summary>
        /// <param name="AgentID">The agent identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="BrokerID">The broker identifier.</param>
        /// <param name="AgentUsername">The agent username.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Agent_Save(long AgentID, string UserID, long BrokerID, string AgentUsername, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;

            if (string.IsNullOrWhiteSpace(SepFunctions.GetUserID(AgentUsername)))
            {
                return SepFunctions.LangText("Invalid User Name.");
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (AgentID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT AgentID FROM RStateAgents WHERE AgentID=@AgentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AgentID", AgentID);
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
                    AgentID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE RStateAgents SET BrokerID=@BrokerID, UserID=@UserID, PortalID=@PortalID WHERE AgentID=@AgentID";
                }
                else
                {
                    SqlStr = "INSERT INTO RStateAgents (AgentID, BrokerID, UserID, PortalID, DatePosted, Status) VALUES (@AgentID, @BrokerID, @UserID, @PortalID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@AgentID", AgentID);
                    cmd.Parameters.AddWithValue("@BrokerID", BrokerID);
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(AgentUsername));
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                SepFunctions.Additional_Data_Save(isNewRecord, 32, Strings.ToString(AgentID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Agent", "Agent", string.Empty);
            }

            string sReturn = SepFunctions.LangText("Agent has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Agent has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Brokers the delete.
        /// </summary>
        /// <param name="BrokerIDs">The broker i ds.</param>
        /// <returns>System.String.</returns>
        public static string Broker_Delete(string BrokerIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrBrokerIDs = Strings.Split(BrokerIDs, ",");

                if (arrBrokerIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrBrokerIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE RStateBrokers SET Status='-1', DateDeleted=@DateDeleted WHERE BrokerID=@BrokerID", conn))
                        {
                            cmd.Parameters.AddWithValue("@BrokerID", arrBrokerIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Broker(s) has been successfully deleted.");
        }

        /// <summary>
        /// Brokers the get.
        /// </summary>
        /// <param name="BrokerID">The broker identifier.</param>
        /// <returns>Models.RealEstateBrokers.</returns>
        public static RealEstateBrokers Broker_Get(long BrokerID)
        {
            var returnXML = new Models.RealEstateBrokers();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM RStateBrokers WHERE BrokerID=@BrokerID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@BrokerID", BrokerID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.BrokerID = SepFunctions.toLong(SepFunctions.openNull(RS["BrokerID"]));
                            returnXML.BrokerName = SepFunctions.openNull(RS["BrokerName"]);
                            returnXML.Approval = SepFunctions.openNull(RS["Approval"]);
                            returnXML.ShowCommission = SepFunctions.toBoolean(SepFunctions.openNull(RS["ShowCommission"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.Username = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Brokers the save.
        /// </summary>
        /// <param name="BrokerID">The broker identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="BrokerName">Name of the broker.</param>
        /// <param name="BrokerUsername">The broker username.</param>
        /// <param name="Approval">If set to <see langword="true" />, then ; otherwise, .</param>
        /// <param name="ShowCommission">If set to <see langword="true" />, then ; otherwise, .</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Broker_Save(long BrokerID, string UserID, string BrokerName, string BrokerUsername, bool Approval, bool ShowCommission, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;

            if (string.IsNullOrWhiteSpace(SepFunctions.GetUserID(BrokerUsername)))
            {
                return SepFunctions.LangText("Invalid User Name.");
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (BrokerID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT BrokerID FROM RStateBrokers WHERE BrokerID=@BrokerID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BrokerID", BrokerID);
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
                    BrokerID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE RStateBrokers SET BrokerName=@BrokerName, Approval=@Approval, ShowCommission=@ShowCommission UserID=@UserID, PortalID=@PortalID WHERE BrokerID=@BrokerID";
                }
                else
                {
                    SqlStr = "INSERT INTO RStateBrokers (BrokerID, BrokerName, Approval, ShowCommission, UserID, PortalID, DatePosted, Status) VALUES (@BrokerID, @BrokerName, @Approval, @ShowCommission, @UserID, @PortalID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@BrokerID", BrokerID);
                    cmd.Parameters.AddWithValue("@BrokerName", BrokerName);
                    cmd.Parameters.AddWithValue("@Approval", Approval);
                    cmd.Parameters.AddWithValue("@ShowCommission", ShowCommission);
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(BrokerUsername));
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                SepFunctions.Additional_Data_Save(isNewRecord, 32, Strings.ToString(BrokerID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Agent", "Agent", string.Empty);
            }

            string sReturn = SepFunctions.LangText("Broker has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Broker has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Gets the real estate agents.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.RealEstateAgents&gt;.</returns>
        public static List<RealEstateAgents> GetRealEstateAgents(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "")
        {
            var lRealEstateAgents = new List<RealEstateAgents>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Username";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (Username LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR BrokerName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT RA.DatePosted,RA.AgentID,M.Username,RB.BrokerName FROM RStateAgents AS RA,RStateBrokers AS RB,Members AS M WHERE M.UserID=RA.UserID AND RB.BrokerID=RA.BrokerID AND RA.PortalID=@PortalID AND RA.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
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

                    var dRealEstateAgents = new Models.RealEstateAgents { AgentID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AgentID"])) };
                    dRealEstateAgents.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dRealEstateAgents.BrokerName = SepFunctions.openNull(ds.Tables[0].Rows[i]["BrokerName"]);
                    dRealEstateAgents.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lRealEstateAgents.Add(dRealEstateAgents);
                }
            }

            return lRealEstateAgents;
        }

        /// <summary>
        /// Gets the real estate attachments.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <returns>List&lt;Models.RealEstateAttachments&gt;.</returns>
        public static List<RealEstateAttachments> GetRealEstateAttachments(string SortExpression = "DatePosted", string SortDirection = "DESC", long TenantID = 0)
        {
            var lRealEstateAttachments = new List<RealEstateAttachments>();

            var wClause = string.Empty;

            if (TenantID > 0)
            {
                wClause += " AND UniqueID='" + SepFunctions.FixWord(Strings.ToString(TenantID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Uploads WHERE ModuleID='32' AND ControlID='Attachments' AND isTemp='0' AND Approved='1'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dRealEstateAttachments = new Models.RealEstateAttachments { UploadID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])) };
                    dRealEstateAttachments.FileName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FileName"]);
                    dRealEstateAttachments.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lRealEstateAttachments.Add(dRealEstateAttachments);
                }
            }

            return lRealEstateAttachments;
        }

        /// <summary>
        /// Gets the real estate brokers.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.RealEstateBrokers&gt;.</returns>
        public static List<RealEstateBrokers> GetRealEstateBrokers(string SortExpression = "BrokerName", string SortDirection = "ASC", string searchWords = "")
        {
            var lRealEstateBrokers = new List<RealEstateBrokers>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "BrokerName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (BrokerName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR UserName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT RB.BrokerID,M.UserName,RB.BrokerName,RB.DatePosted FROM RStateBrokers AS RB,Members AS M WHERE M.UserID=RB.UserID AND RB.PortalID=@PortalID AND RB.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
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

                    var dRealEstateBrokers = new Models.RealEstateBrokers { BrokerID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["BrokerID"])) };
                    dRealEstateBrokers.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dRealEstateBrokers.BrokerName = SepFunctions.openNull(ds.Tables[0].Rows[i]["BrokerName"]);
                    dRealEstateBrokers.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lRealEstateBrokers.Add(dRealEstateBrokers);
                }
            }

            return lRealEstateBrokers;
        }

        /// <summary>
        /// Gets the real estate properties.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartPrice">The start price.</param>
        /// <param name="EndPrice">The end price.</param>
        /// <param name="PropertyTypes">The property types.</param>
        /// <param name="BedRooms">The bed rooms.</param>
        /// <param name="BathRooms">The bath rooms.</param>
        /// <param name="Distance">The distance.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="State">The state.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="ForSale">For sale.</param>
        /// <param name="Country">The country.</param>
        /// <returns>List&lt;Models.RealEstateProperties&gt;.</returns>
        public static List<RealEstateProperties> GetRealEstateProperties(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "", string UserID = "", string StartPrice = "", string EndPrice = "", string PropertyTypes = "", double BedRooms = 0, double BathRooms = 0, string Distance = "", string PostalCode = "", string State = "", string StartDate = "", string ForSale = "", string Country = "")
        {
            var lRealEstateProperties = new List<RealEstateProperties>();

            var wClause = string.Empty;
            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (Title LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR City LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR State LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND RS.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (!string.IsNullOrWhiteSpace(StartPrice))
            {
                wClause += " AND Cast(Price AS Money) > '" + SepFunctions.Format_Currency(StartPrice) + "'";
            }

            if (!string.IsNullOrWhiteSpace(EndPrice))
            {
                wClause += " AND Cast(Price AS Money) < '" + SepFunctions.Format_Currency(EndPrice) + "'";
            }

            if (!string.IsNullOrWhiteSpace(PropertyTypes))
            {
                string outputTypes = string.Empty;
                string[] pTypes = Strings.Split(PropertyTypes, ",");
                if (pTypes != null)
                {
                    for (var i = 0; i <= Information.UBound(pTypes); i++)
                    {
                        if (i > 0)
                        {
                            outputTypes += ",";
                        }

                        outputTypes += SepFunctions.toInt(pTypes[i]);
                    }
                }

                if (!string.IsNullOrWhiteSpace(outputTypes))
                {
                    wClause += " AND RS.PropertyType IN (" + outputTypes + ")";
                }
            }

            if (BedRooms > 0)
            {
                wClause += " AND RS.NumBedrooms >= '" + SepFunctions.FixWord(Strings.ToString(BedRooms)) + "'";
            }

            if (BathRooms > 0)
            {
                wClause += " AND RS.NumBathrooms >= '" + SepFunctions.FixWord(Strings.ToString(BathRooms)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(ForSale))
            {
                wClause += " AND RS.ForSale='" + SepFunctions.FixWord(ForSale) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND RS.DatePosted > '" + SepFunctions.toSQLDate(StartDate) + "'";
            }

            if (!string.IsNullOrWhiteSpace(PostalCode) && !string.IsNullOrWhiteSpace(Distance))
            {
                var sCountry = string.Empty;
                if (string.IsNullOrWhiteSpace(Country))
                {
                    sCountry = "us";
                }

                var cPostalCodes = SepFunctions.PostalCodesInDistance(sCountry, PostalCode, Distance, string.Empty);
                if (cPostalCodes.PostalCodes != null)
                {
                    var sPostalClause = string.Empty;
                    if (cPostalCodes.PostalCodes.Count > 0)
                    {
                        for (var i = 0; i <= cPostalCodes.PostalCodes.Count - 1; i++)
                        {
                            if (i > 0)
                            {
                                sPostalClause += ",";
                            }

                            sPostalClause += "'" + SepFunctions.FixWord(cPostalCodes.PostalCodes[i]) + "'";
                        }

                        wClause += " AND RS.PostalCode IN (" + sPostalClause + ")";
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(State))
                {
                    wClause += " AND RS.State='" + SepFunctions.FixWord(State) + "'";
                }
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT RS.PropertyID,RS.ForSale,RS.Title,RS.PropertyType,RS.City,RS.State,RS.Visits,RS.Status,RS.Description,RS.UserID,M.UserName FROM RStateProperty AS RS,Members AS M WHERE M.UserID=RS.UserID AND RS.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dRealEstateProperties = new Models.RealEstateProperties { PropertyID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PropertyID"])) };
                    dRealEstateProperties.Title = SepFunctions.openNull(ds.Tables[0].Rows[i]["Title"]);
                    dRealEstateProperties.PropertyType = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["PropertyType"]));
                    dRealEstateProperties.ForSale = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["ForSale"]);
                    dRealEstateProperties.City = SepFunctions.openNull(ds.Tables[0].Rows[i]["City"]);
                    dRealEstateProperties.State = SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    dRealEstateProperties.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dRealEstateProperties.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dRealEstateProperties.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dRealEstateProperties.Visits = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Visits"]));
                    string sDescription = SepFunctions.RemoveHTML(SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]));
                    if (Strings.Len(sDescription) > 500)
                    {
                        sDescription = Strings.Left(sDescription, 500) + " ...";
                    }

                    dRealEstateProperties.Description = sDescription;
                    lRealEstateProperties.Add(dRealEstateProperties);
                }
            }

            return lRealEstateProperties;
        }

        /// <summary>
        /// Gets the real estate reviews.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <returns>List&lt;Models.RealEstateReviews&gt;.</returns>
        public static List<RealEstateReviews> GetRealEstateReviews(string SortExpression = "DatePosted", string SortDirection = "DESC", long PropertyID = 0, long TenantID = 0)
        {
            var lRealEstateReviews = new List<RealEstateReviews>();

            var wClause = string.Empty;

            if (PropertyID > 0)
            {
                wClause += " AND PropertyID='" + SepFunctions.FixWord(Strings.ToString(PropertyID)) + "'";
            }

            if (TenantID > 0)
            {
                wClause += " AND TenantID='" + SepFunctions.FixWord(Strings.ToString(TenantID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM RStateReviews WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dRealEstateReviews = new Models.RealEstateReviews { ReviewID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ReviewID"])) };
                    dRealEstateReviews.TenantID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TenantID"]));
                    dRealEstateReviews.PropertyID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PropertyID"]));
                    dRealEstateReviews.IsTenant = SepFunctions.openBoolean(ds.Tables[0].Rows[i]["IsTenant"]);
                    dRealEstateReviews.Rating = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Rating"]));
                    dRealEstateReviews.Complaints = SepFunctions.openNull(ds.Tables[0].Rows[i]["Complaints"]);
                    dRealEstateReviews.Compliments = SepFunctions.openNull(ds.Tables[0].Rows[i]["Compliments"]);
                    dRealEstateReviews.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lRealEstateReviews.Add(dRealEstateReviews);
                }
            }

            return lRealEstateReviews;
        }

        /// <summary>
        /// Gets the real estate tenants.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tenantNumber">The tenant number.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <returns>List&lt;Models.RealEstateTenants&gt;.</returns>
        public static List<RealEstateTenants> GetRealEstateTenants(string SortExpression = "TenantName", string SortDirection = "ASC", string searchWords = "", long PropertyID = 0, string userId = "", string tenantNumber = "", string birthDate = "")
        {
            var lRealEstateAgents = new List<RealEstateTenants>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "TenantName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (TenantName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR TenantNumber LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (PropertyID > 0)
            {
                wClause += " AND RP.PropertyID='" + SepFunctions.FixWord(Strings.ToString(PropertyID)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                wClause = " AND RP.PropertyID IN (SELECT PropertyID FROM RStateProperty WHERE UserID='" + SepFunctions.FixWord(userId) + "')";
            }

            if (!string.IsNullOrWhiteSpace(tenantNumber))
            {
                wClause = " AND TenantNumber='" + SepFunctions.FixWord(tenantNumber) + "'";
            }

            if (!string.IsNullOrWhiteSpace(birthDate))
            {
                wClause = " AND BirthDate BETWEEN '" + SepFunctions.toSQLDate(birthDate) + "' AND '" + SepFunctions.toSQLDate(birthDate + " 12:59:59") + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT RT.TenantID,RT.TenantName,RT.TenantNumber,RT.BirthDate,RP.PropertyID FROM RStateTenants AS RT,RStateTenantProperties AS RP WHERE RP.TenantID=RT.TenantID AND RT.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
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

                    var dRealEstateAgents = new Models.RealEstateTenants { TenantID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TenantID"])) };
                    dRealEstateAgents.PropertyID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PropertyID"]));
                    dRealEstateAgents.TenantName = SepFunctions.openNull(ds.Tables[0].Rows[i]["TenantName"]);
                    dRealEstateAgents.TenantNumber = SepFunctions.openNull(ds.Tables[0].Rows[i]["TenantNumber"]);
                    dRealEstateAgents.BirthDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["BirthDate"]));
                    lRealEstateAgents.Add(dRealEstateAgents);
                }
            }

            return lRealEstateAgents;
        }

        /// <summary>
        /// Properties the change status.
        /// </summary>
        /// <param name="PropertyIDs">The property i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Property_Change_Status(string PropertyIDs, int Status)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrPropertyIDs = Strings.Split(PropertyIDs, ",");

                if (arrPropertyIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrPropertyIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE RStateProperty SET Status=@Status WHERE PropertyID=@PropertyID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PropertyID", arrPropertyIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Property(s) status has been successfully saved.");
        }

        /// <summary>
        /// Properties the delete.
        /// </summary>
        /// <param name="PropertyIDs">The property i ds.</param>
        /// <returns>System.String.</returns>
        public static string Property_Delete(string PropertyIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrPropertyIDs = Strings.Split(PropertyIDs, ",");

                if (arrPropertyIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrPropertyIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE RStateProperty SET Status='-1', DateDeleted=@DateDeleted WHERE PropertyID=@PropertyID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PropertyID", arrPropertyIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(32, arrPropertyIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Property(s) has been successfully deleted.");
        }

        /// <summary>
        /// Properties the get.
        /// </summary>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.RealEstateProperties.</returns>
        public static RealEstateProperties Property_Get(long PropertyID, long ChangeID = 0)
        {
            var returnXML = new Models.RealEstateProperties();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE RStateProperty SET Visits=Visits+1 WHERE PropertyID=@PropertyID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM RStateProperty WHERE PropertyID=@PropertyID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
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
                                returnXML.PropertyID = SepFunctions.toLong(SepFunctions.openNull(RS["PropertyID"]));
                                returnXML.AgentID = SepFunctions.toLong(SepFunctions.openNull(RS["AgentID"]));
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.ListingID = SepFunctions.toLong(SepFunctions.openNull(RS["ListingID"]));
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Title = SepFunctions.openNull(RS["Title"]);
                                returnXML.Description = SepFunctions.openNull(RS["Description"]);
                                returnXML.Price = SepFunctions.openNull(RS["Price"]);
                                returnXML.RecurringCycle = SepFunctions.openNull(RS["RecurringCycle"]);
                                returnXML.ForSale = SepFunctions.openBoolean(RS["ForSale"]);
                                returnXML.PropertyType = SepFunctions.toInt(SepFunctions.openNull(RS["PropertyType"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                                returnXML.StreetAddress = SepFunctions.openNull(RS["StreetAddress"]);
                                returnXML.City = SepFunctions.openNull(RS["City"]);
                                returnXML.State = SepFunctions.openNull(RS["State"]);
                                returnXML.PostalCode = SepFunctions.openNull(RS["PostalCode"]);
                                returnXML.County = SepFunctions.openNull(RS["County"]);
                                returnXML.Country = SepFunctions.openNull(RS["Country"]);
                                returnXML.YearBuilt = SepFunctions.openNull(RS["YearBuilt"]);
                                returnXML.NumBedrooms = SepFunctions.toDecimal(SepFunctions.openNull(RS["NumBedrooms"]));
                                returnXML.NumBathrooms = SepFunctions.toDecimal(SepFunctions.openNull(RS["NumBathrooms"]));
                                returnXML.NumRooms = SepFunctions.toInt(SepFunctions.openNull(RS["NumRooms"]));
                                returnXML.SQFeet = SepFunctions.openNull(RS["SQFeet"]);
                                returnXML.Type = SepFunctions.toInt(SepFunctions.openNull(RS["Type"]));
                                returnXML.Style = SepFunctions.toInt(SepFunctions.openNull(RS["Style"]));
                                returnXML.SizeMBedroom = SepFunctions.openNull(RS["SizeMBedroom"]);
                                returnXML.SizeLivingRoom = SepFunctions.openNull(RS["SizeLivingRoom"]);
                                returnXML.SizeDiningRoom = SepFunctions.openNull(RS["SizeDiningRoom"]);
                                returnXML.SizeKitchen = SepFunctions.openNull(RS["SizeKitchen"]);
                                returnXML.SizeLot = SepFunctions.openNull(RS["SizeLot"]);
                                returnXML.Garage = SepFunctions.openNull(RS["Garage"]);
                                returnXML.Heating = SepFunctions.openNull(RS["Heating"]);
                                returnXML.FeatureInterior = SepFunctions.openNull(RS["FeatureInterior"]);
                                returnXML.FeatureExterior = SepFunctions.openNull(RS["FeatureExterior"]);
                                returnXML.MLSNumber = SepFunctions.openNull(RS["MLSNumber"]);
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.Visits = SepFunctions.toLong(SepFunctions.openNull(RS["Visits"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Properties the save.
        /// </summary>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="AgentID">The agent identifier.</param>
        /// <param name="Title">The title.</param>
        /// <param name="Description">The description.</param>
        /// <param name="Price">The price.</param>
        /// <param name="ForSale">For sale.</param>
        /// <param name="MLSNumber">The MLS number.</param>
        /// <param name="PropertyType">Type of the property.</param>
        /// <param name="Status">The status.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="County">The county.</param>
        /// <param name="Country">The country.</param>
        /// <param name="YearBuilt">The year built.</param>
        /// <param name="NumBedrooms">The number bedrooms.</param>
        /// <param name="NumBathrooms">The number bathrooms.</param>
        /// <param name="NumRooms">The number rooms.</param>
        /// <param name="SQFeet">The sq feet.</param>
        /// <param name="Type">The type.</param>
        /// <param name="Style">The style.</param>
        /// <param name="SizeMBedroom">The size m bedroom.</param>
        /// <param name="SizeLivingRoom">The size living room.</param>
        /// <param name="SizeDiningRoom">The size dining room.</param>
        /// <param name="SizeKitchen">The size kitchen.</param>
        /// <param name="SizeLot">The size lot.</param>
        /// <param name="Garage">The garage.</param>
        /// <param name="Heating">The heating.</param>
        /// <param name="FeatureInterior">The feature interior.</param>
        /// <param name="FeatureExterior">The feature exterior.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="RecurringCycle">The recurring cycle.</param>
        /// <returns>System.Int32.</returns>
        public static int Property_Save(long PropertyID, string UserID, long AgentID, string Title, string Description, string Price, string ForSale, string MLSNumber, string PropertyType, string Status, string StreetAddress, string City, string State, string PostalCode, string County, string Country, string YearBuilt, string NumBedrooms, string NumBathrooms, string NumRooms, string SQFeet, string Type, string Style, string SizeMBedroom, string SizeLivingRoom, string SizeDiningRoom, string SizeKitchen, string SizeLot, string Garage, string Heating, string FeatureInterior, string FeatureExterior, long PortalID, string RecurringCycle)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (PropertyID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM RStateProperty WHERE PropertyID=@PropertyID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
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
                    PropertyID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE RStateProperty SET AgentID=@AgentID, UserID=@UserID, Title=@Title, MLSNumber=@MLSNumber, Description=@Description, PropertyType=@PropertyType, Status=@Status, Price=@Price, RecurringCycle=@RecurringCycle, ForSale=@ForSale, StreetAddress=@StreetAddress, City=@City, State=@State, PostalCode=@PostalCode, County=@County, Country=@Country, YearBuilt=@YearBuilt, NumBedrooms=@NumBedrooms, NumBathrooms=@NumBathrooms, NumRooms=@NumRooms, SQFeet=@SQFeet, Type=@Type, Style=@Style, SizeMBedroom=@SizeMBedroom, SizeLivingRoom=@SizeLivingRoom, SizeDiningRoom=@SizeDiningRoom, SizeKitchen=@SizeKitchen, SizeLot=@SizeLot, Garage=@Garage, Heating=@Heating, FeatureInterior=@FeatureInterior, FeatureExterior=@FeatureExterior, PortalID=@PortalID WHERE PropertyID=@PropertyID";
                }
                else
                {
                    SqlStr = "INSERT INTO RStateProperty (PropertyID, AgentID, UserID, Title, MLSNumber, Description, PropertyType, Status, Price, RecurringCycle, ForSale, StreetAddress, City, State, PostalCode, County, Country, YearBuilt, NumBedrooms, NumBathrooms, NumRooms, SQFeet, Type, Style, SizeMBedroom, SizeLivingRoom, SizeDiningRoom, SizeKitchen, SizeLot, Garage, Heating, FeatureInterior, FeatureExterior, Visits, ListingID, PortalID, DatePosted) VALUES (@PropertyID, @AgentID, @UserID, @Title, @MLSNumber, @Description, @PropertyType, @Status, @Price, @RecurringCycle, @ForSale, @StreetAddress, @City, @State, @PostalCode, @County, @Country, @YearBuilt, @NumBedrooms, @NumBathrooms, @NumRooms, @SQFeet, @Type, @Style, @SizeMBedroom, @SizeLivingRoom, @SizeDiningRoom, @SizeKitchen, @SizeLot, @Garage, @Heating, @FeatureInterior, @FeatureExterior, @Visits, @ListingID, @PortalID, @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
                    cmd.Parameters.AddWithValue("@AgentID", AgentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@MLSNumber", MLSNumber);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@PropertyType", PropertyType);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@RecurringCycle", RecurringCycle);
                    cmd.Parameters.AddWithValue("@ForSale", ForSale);
                    cmd.Parameters.AddWithValue("@StreetAddress", StreetAddress);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                    cmd.Parameters.AddWithValue("@County", County);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@YearBuilt", YearBuilt);
                    cmd.Parameters.AddWithValue("@NumBedrooms", SepFunctions.toDecimal(NumBedrooms));
                    cmd.Parameters.AddWithValue("@NumBathrooms", SepFunctions.toDecimal(NumBathrooms));
                    cmd.Parameters.AddWithValue("@NumRooms", SepFunctions.toLong(NumRooms));
                    cmd.Parameters.AddWithValue("@SQFeet", SQFeet);
                    cmd.Parameters.AddWithValue("@Type", Type);
                    cmd.Parameters.AddWithValue("@Style", Style);
                    cmd.Parameters.AddWithValue("@SizeMBedroom", SizeMBedroom);
                    cmd.Parameters.AddWithValue("@SizeLivingRoom", SizeLivingRoom);
                    cmd.Parameters.AddWithValue("@SizeDiningRoom", SizeDiningRoom);
                    cmd.Parameters.AddWithValue("@SizeKitchen", SizeKitchen);
                    cmd.Parameters.AddWithValue("@SizeLot", SizeLot);
                    cmd.Parameters.AddWithValue("@Garage", Garage);
                    cmd.Parameters.AddWithValue("@Heating", Heating);
                    cmd.Parameters.AddWithValue("@FeatureInterior", FeatureInterior);
                    cmd.Parameters.AddWithValue("@FeatureExterior", FeatureExterior);
                    cmd.Parameters.AddWithValue("@Visits", "0");
                    cmd.Parameters.AddWithValue("@ListingID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 32, Strings.ToString(PropertyID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Property", "Real Estate property", "PostProperty");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM RStateProperty WHERE PropertyID=@PropertyID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
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
                        SepFunctions.Update_Change_Log(32, Strings.ToString(PropertyID), Title, Strings.ToString(writeLog));
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
        /// Reviews the get.
        /// </summary>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <returns>Models.RealEstateReviews.</returns>
        public static RealEstateReviews Review_Get(long PropertyID, long TenantID)
        {
            var returnXML = new Models.RealEstateReviews();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM RStateReviews WHERE PropertyID=@PropertyID AND TenantID=@TenantID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
                    cmd.Parameters.AddWithValue("@TenantID", TenantID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ReviewID = SepFunctions.toLong(SepFunctions.openNull(RS["ReviewID"]));
                            returnXML.PropertyID = SepFunctions.toLong(SepFunctions.openNull(RS["PropertyID"]));
                            returnXML.TenantID = SepFunctions.toLong(SepFunctions.openNull(RS["TenantID"]));
                            returnXML.IsTenant = SepFunctions.toBoolean(SepFunctions.openNull(RS["IsTenant"]));
                            returnXML.Rating = SepFunctions.toInt(SepFunctions.openNull(RS["Rating"]));
                            returnXML.Complaints = SepFunctions.openNull(RS["Complaints"]);
                            returnXML.Compliments = SepFunctions.openNull(RS["Compliments"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Reviews the save.
        /// </summary>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="Rating">The rating.</param>
        /// <param name="Complaints">The complaints.</param>
        /// <param name="Compliments">The compliments.</param>
        /// <returns>System.String.</returns>
        public static string Review_Save(long TenantID, long PropertyID, int Rating, string Complaints, string Compliments)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("INSERT INTO RStateReviews (ReviewID, TenantID, PropertyID, Rating, Complaints, Compliments, IsTenant, DatePosted, Status) VALUES (@ReviewID, @TenantID, @PropertyID, @Rating, @Complaints, @Compliments, '0', @DatePosted, '1')", conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@TenantID", TenantID);
                    cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
                    cmd.Parameters.AddWithValue("@Rating", Rating);
                    cmd.Parameters.AddWithValue("@Complaints", Complaints);
                    cmd.Parameters.AddWithValue("@Compliments", Compliments);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = SepFunctions.LangText("Review has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Tenants the delete.
        /// </summary>
        /// <param name="TenantIDs">The tenant i ds.</param>
        /// <returns>System.String.</returns>
        public static string Tenant_Delete(string TenantIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrTenantIDs = Strings.Split(TenantIDs, ",");

                if (arrTenantIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTenantIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE RStateReviews SET Status='-1', DateDeleted=@DateDeleted WHERE TenantID=@TenantID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TenantID", arrTenantIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE RStateTenantProperties SET Status='-1', DateDeleted=@DateDeleted WHERE TenantID=@TenantID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TenantID", arrTenantIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE RStateTenants SET Status='-1', DateDeleted=@DateDeleted WHERE TenantID=@TenantID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TenantID", arrTenantIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(32, arrTenantIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Tenant(s) has been successfully deleted.");
        }

        /// <summary>
        /// Tenants the get.
        /// </summary>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <param name="PropertyID">The property identifier.</param>
        /// <returns>Models.RealEstateTenants.</returns>
        public static RealEstateTenants Tenant_Get(long TenantID, long PropertyID)
        {
            var returnXML = new Models.RealEstateTenants();

            var wClause = string.Empty;

            if (PropertyID > 0)
            {
                wClause = " AND RP.PropertyID='" + PropertyID + "'";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT RT.TenantID,RT.TenantName,RT.TenantNumber,RT.BirthDate,RP.PropertyID,RP.MovedIn,RP.MovedOut,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='32' AND UniqueID=RT.TenantID AND Uploads.isTemp='0' AND Uploads.Approved='1' AND ControlID='Photo') AS UploadID FROM RStateTenants AS RT,RStateTenantProperties AS RP WHERE RT.TenantID=RP.TenantID AND RT.TenantID=@TenantID AND RT.Status <> -1" + wClause, conn))
                {
                    cmd.Parameters.AddWithValue("@TenantID", TenantID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TenantID = SepFunctions.toLong(SepFunctions.openNull(RS["TenantID"]));
                            returnXML.PropertyID = SepFunctions.toLong(SepFunctions.openNull(RS["PropertyID"]));
                            returnXML.TenantName = SepFunctions.openNull(RS["TenantName"]);
                            returnXML.TenantNumber = SepFunctions.openNull(RS["TenantNumber"]);
                            returnXML.BirthDate = SepFunctions.toDate(SepFunctions.openNull(RS["BirthDate"]));
                            returnXML.MovedIn = SepFunctions.toDate(SepFunctions.openNull(RS["MovedIn"]));
                            returnXML.MovedOut = SepFunctions.toDate(SepFunctions.openNull(RS["MovedOut"]));
                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["UploadID"])))
                            {
                                returnXML.DefaultPicture = "/spadmin/show_image.aspx?ModuleID=32&Size=thumb&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                            }

                            double UserRating = 0;
                            double intTotalRecords = 0;

                            using (var cmd2 = new SqlCommand("SELECT Rating FROM RStateReviews WHERE TenantID=@TenantID AND PropertyID=@PropertyID AND Status <> -1", conn))
                            {
                                cmd2.Parameters.AddWithValue("@TenantID", TenantID);
                                cmd2.Parameters.AddWithValue("@PropertyID", PropertyID);
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    while (RS2.Read())
                                    {
                                        intTotalRecords += 1;
                                        UserRating += SepFunctions.toDouble(SepFunctions.openNull(RS2["Rating"]));
                                    }
                                }
                            }

                            if (intTotalRecords > 0)
                            {
                                returnXML.AverageRating = UserRating / intTotalRecords;
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Tenants the save.
        /// </summary>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="TenantName">Name of the tenant.</param>
        /// <param name="TenantNumber">The tenant number.</param>
        /// <param name="BirthDate">The birth date.</param>
        /// <param name="MoveInDate">The move in date.</param>
        /// <param name="MoveOutDate">The move out date.</param>
        /// <returns>System.String.</returns>
        public static string Tenant_Save(long TenantID, long PropertyID, string TenantName, string TenantNumber, DateTime BirthDate, DateTime MoveInDate, DateTime MoveOutDate)
        {
            var bUpdate = false;
            var isNewRecord = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (TenantID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT TenantID FROM RStateTenants WHERE TenantID=@TenantID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TenantID", TenantID);
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
                    TenantID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                var SqlStr2 = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE RStateTenants SET TenantName=@TenantName, TenantNumber=@TenantNumber, BirthDate=@BirthDate WHERE TenantID=@TenantID";
                    SqlStr2 = "UPDATE RStateTenantProperties SET PropertyID=@PropertyID, MovedIn=@MovedIn, MovedOut=@MovedOut WHERE TenantID=@TenantID";
                }
                else
                {
                    SqlStr = "INSERT INTO RStateTenants (TenantID, TenantName, TenantNumber, BirthDate, DateAdded, Status) VALUES (@TenantID, @TenantName, @TenantNumber, @BirthDate, @DateAdded, '1')";
                    SqlStr2 = "INSERT INTO RStateTenantProperties (TenantID, PropertyID, MovedIn, MovedOut, DateAdded, Status) VALUES (@TenantID, @PropertyID, @MovedIn, @MovedOut, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@TenantID", TenantID);
                    cmd.Parameters.AddWithValue("@TenantName", TenantName);
                    cmd.Parameters.AddWithValue("@TenantNumber", TenantNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(SqlStr2, conn))
                {
                    cmd.Parameters.AddWithValue("@TenantID", TenantID);
                    cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
                    cmd.Parameters.AddWithValue("@MovedIn", MoveInDate);
                    cmd.Parameters.AddWithValue("@MovedOut", MoveOutDate);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                SepFunctions.Additional_Data_Save(isNewRecord, 32, Strings.ToString(TenantID), SepFunctions.Session_User_ID(), SepFunctions.GetUserInformation("UserName", SepFunctions.Session_User_ID()), "Tenant", "Tenant", string.Empty);
            }

            string sReturn = SepFunctions.LangText("Tenant has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Tenant has been successfully updated.");
            }

            return sReturn;
        }
    }
}