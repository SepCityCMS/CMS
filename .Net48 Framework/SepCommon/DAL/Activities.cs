// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Activities.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class Activities.
    /// </summary>
    public static class Activities
    {
        /// <summary>
        /// Activities the delete.
        /// </summary>
        /// <param name="ActivityIDs">The activity i ds.</param>
        /// <returns>System.String.</returns>
        public static string ActivityDelete(string ActivityIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrActivityIDs = Strings.Split(ActivityIDs, ",");

                if (arrActivityIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrActivityIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Activities SET Status='-1', DateDeleted=@DateDeleted WHERE ActivityID=@ActivityID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ActivityID", arrActivityIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Activity(s) has been successfully deleted.");
        }

        /// <summary>
        /// Activities the get.
        /// </summary>
        /// <param name="ActivityID">The activity identifier.</param>
        /// <returns>Models.Activities.</returns>
        public static Models.Activities ActivityGet(long ActivityID)
        {
            var returnXML = new Models.Activities();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Activities WHERE ActivityID=@ActivityID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ActivityID = SepFunctions.toLong(SepFunctions.openNull(RS["ActivityID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.UserName = SepFunctions.GetUserInformation("Username", SepFunctions.openNull(RS["UserID"]));
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.ActType = SepFunctions.openNull(RS["ActType"]);
                            returnXML.IPAddress = SepFunctions.openNull(RS["IPAddress"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.ModuleID = SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]));
                            returnXML.UniqueID = SepFunctions.toLong(SepFunctions.openNull(RS["UniqueID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Activities the save.
        /// </summary>
        /// <param name="ActivityID">The activity identifier.</param>
        /// <param name="ActUserID">The act user identifier.</param>
        /// <param name="ActivityType">Type of the activity.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.String.</returns>
        public static string ActivitySave(long ActivityID, string ActUserID, string ActivityType, string Description)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ActivityID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ActivityID FROM Activities WHERE ActivityID=@ActivityID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
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
                    ActivityID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Activities SET UserID=@UserID, ActType=@ActivityType, Description=@Description WHERE ActivityID=@ActivityID";
                }
                else
                {
                    SqlStr = "INSERT INTO Activities (ActivityID, UserID, ActType, Description, DatePosted, Status) VALUES (@ActivityID, @UserID, @ActivityType, @Description, @DatePosted, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
                    cmd.Parameters.AddWithValue("@UserID", ActUserID);
                    cmd.Parameters.AddWithValue("@ActivityType", ActivityType);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Activity has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Activity has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns>List&lt;Models.Activities&gt;.</returns>
        public static List<Models.Activities> GetActivities(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "", string UserID = "", int ModuleID = 0)
        {
            var lActivities = new List<Models.Activities>();

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
                wClause = " AND (Activities.ActType LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND (Activities.UserID='" + SepFunctions.FixWord(UserID) + "')";
            }

            if (ModuleID > 0)
            {
                wClause += " AND (Activities.ModuleID='" + SepFunctions.FixWord(Strings.ToString(ModuleID)) + "')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Activities.ActivityID,Activities.DatePosted,Activities.ActType,Activities.Description,Activities.UserID,Activities.IPAddress,Members.UserName FROM Activities, Members WHERE Activities.UserID=Members.UserID AND Activities.Status <> -1 AND Members.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dActivities = new Models.Activities { ActivityID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ActivityID"])) };
                    dActivities.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dActivities.ActType = SepFunctions.openNull(ds.Tables[0].Rows[i]["ActType"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"])))
                    {
                        dActivities.Description = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"])) ? Strings.Replace(SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]), "[[Username]]", SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"])) : "Visitor");
                    }
                    else
                    {
                        dActivities.Description = Strings.Replace(SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]), "[[Username]]", "User");
                    }

                    dActivities.IPAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["IPAddress"]);
                    lActivities.Add(dActivities);
                }
            }

            return lActivities;
        }

        /// <summary>
        /// Gets the error logs.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Activities&gt;.</returns>
        public static List<Models.Activities> GetErrorLogs(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "")
        {
            var lActivities = new List<Models.Activities>();

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
                wClause = " AND (Description LIKE '%" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT DatePosted,Description FROM Activities WHERE ActType='ERROR' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dActivities = new Models.Activities { DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"])) };
                    dActivities.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    lActivities.Add(dActivities);
                }
            }

            return lActivities;
        }

        /// <summary>
        /// Monthlies the activities.
        /// </summary>
        /// <returns>List&lt;Models.ChartData&gt;.</returns>
        public static List<ChartData> MonthlyActivities()
        {
            var lData = new List<ChartData>();
            int iMonths = 12;

            long Total = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                // Populate series data
                for (var i = 0; i <= 11; i++)
                {
                    iMonths -= 1;
                    using (var cmd = new SqlCommand("SELECT Count(ActivityID) AS Counter FROM Activities WHERE Month(DatePosted) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND Year(DatePosted) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            Total += SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                        }
                    }
                }

                iMonths = 12;

                for (var i = 0; i <= 11; i++)
                {
                    iMonths -= 1;
                    using (var cmd = new SqlCommand("SELECT Count(ActivityID) AS Counter FROM Activities WHERE Month(DatePosted) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND Year(DatePosted) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            var dData = new Models.ChartData { MonthName = DateAndTime.MonthName(DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -i, DateTime.Now))) };
                            dData.NumActivities = Math.Round(SepFunctions.toDouble(SepFunctions.openNull(RS["Counter"])), 0);
                            if (Total > 0)
                            {
                                dData.Percentage = SepFunctions.toDecimal(SepFunctions.openNull(RS["Counter"])) / Total * 100;
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