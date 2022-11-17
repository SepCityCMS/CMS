// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Polls.cs" company="SepCity, Inc.">
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
    /// Class Polls.
    /// </summary>
    public static class Polls
    {
        /// <summary>
        /// Gets the polls.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="onlyAvailable">if set to <c>true</c> [only available].</param>
        /// <returns>List&lt;Models.Polls&gt;.</returns>
        public static List<Models.Polls> GetPolls(string SortExpression = "StartDate", string SortDirection = "DESC", string searchWords = "", bool onlyAvailable = false)
        {
            var lPolls = new List<Models.Polls>();

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
                wClause = " AND Question LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (onlyAvailable)
            {
                wClause += " AND CONVERT(varchar, StartDate, 126) <= CONVERT(varchar, GetDate(),126) AND CONVERT(varchar, EndDate, 126) >= CONVERT(varchar, GetDate(),126)";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT PollID,Question,StartDate,EndDate FROM PNQQuestions WHERE PollID IN (SELECT TOP 1 UniqueID FROM Associations WHERE ModuleID='25' AND (PortalID=@PortalID OR PortalID = -1) AND UniqueID=PollID) AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dPolls = new Models.Polls { PollID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PollID"])) };
                    dPolls.Question = SepFunctions.openNull(ds.Tables[0].Rows[i]["Question"]);
                    dPolls.StartDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartDate"]));
                    dPolls.EndDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndDate"]));
                    lPolls.Add(dPolls);
                }
            }

            return lPolls;
        }

        /// <summary>
        /// Polls the delete.
        /// </summary>
        /// <param name="PollIDs">The poll i ds.</param>
        /// <returns>System.String.</returns>
        public static string Poll_Delete(string PollIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrPollIDs = Strings.Split(PollIDs, ",");

                if (arrPollIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrPollIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE PNQQuestions SET Status='-1', DateDeleted=@DateDeleted WHERE PollID=@PollID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PollID", arrPollIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE PNQOptions SET Status='-1', DateDeleted=@DateDeleted WHERE PollID=@PollID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PollID", arrPollIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(25, arrPollIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Poll(s) has been successfully deleted.");
        }

        /// <summary>
        /// Polls the get.
        /// </summary>
        /// <param name="PollID">The poll identifier.</param>
        /// <returns>Models.Polls.</returns>
        public static Models.Polls Poll_Get(long PollID)
        {
            var returnXML = new Models.Polls();

            long aCount = 0;
            var sOptionIds = string.Empty;
            var sOptionValues = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM PNQQuestions WHERE PollID=@PollID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@PollID", PollID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.PollID = SepFunctions.toLong(SepFunctions.openNull(RS["PollID"]));
                            returnXML.Question = SepFunctions.openNull(RS["Question"]);
                            returnXML.StartDate = SepFunctions.toDate(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"])), Strings.DateNamedFormat.ShortDate));
                            returnXML.EndDate = SepFunctions.toDate(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"])), Strings.DateNamedFormat.ShortDate));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }

                if (returnXML.PollID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM PNQOptions WHERE PollID=@PollID AND Status <> -1 ORDER BY PollOption", conn))
                    {
                        cmd.Parameters.AddWithValue("@PollID", PollID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                aCount += 1;
                                if (aCount > 1)
                                {
                                    sOptionIds += "||";
                                    sOptionValues += "||";
                                }

                                sOptionIds += SepFunctions.openNull(RS["OptionID"]);
                                sOptionValues += SepFunctions.openNull(RS["PollOption"]);
                            }
                        }
                    }

                    returnXML.OptionIds = sOptionIds;
                    returnXML.OptionValues = sOptionValues;
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Polls the save.
        /// </summary>
        /// <param name="PollID">The poll identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Question">The question.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="OptionIDs">The option i ds.</param>
        /// <param name="OptionValues">The option values.</param>
        /// <returns>System.Int32.</returns>
        public static int Poll_Save(long PollID, string UserID, string Question, DateTime StartDate, DateTime EndDate, string PortalIDs, string OptionIDs, string OptionValues)
        {
            var bUpdate = false;
            var isNewRecord = false;
            string[] arrPortals = null;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (PollID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT PollID FROM PNQQuestions WHERE PollID=@PollID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PollID", PollID);
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
                    PollID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE PNQQuestions SET Question=@Question, StartDate=@StartDate, EndDate=@EndDate WHERE PollID=@PollID";
                }
                else
                {
                    SqlStr = "INSERT INTO PNQQuestions (PollID, Question, StartDate, EndDate, Status) VALUES (@PollID, @Question, @StartDate, @EndDate, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@PollID", PollID);
                    cmd.Parameters.AddWithValue("@Question", Question);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.ExecuteNonQuery();
                }

                // Save Custom Choices
                using (var cmd = new SqlCommand("DELETE FROM PNQOptions WHERE PollID=@PollID AND OptionID NOT IN (" + SepFunctions.FixWord(Strings.Replace(OptionIDs, "||", ",")) + ")", conn))
                {
                    cmd.Parameters.AddWithValue("@PollID", PollID);
                    cmd.ExecuteNonQuery();
                }

                string[] arrOptionIDs = Strings.Split(OptionIDs, "||");
                string[] arrOptionValues = Strings.Split(OptionValues, "||");
                Array.Resize(ref arrOptionValues, Information.UBound(arrOptionIDs) + 1);

                if (arrOptionIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrOptionIDs); i++)
                    {
                        if (!string.IsNullOrWhiteSpace(arrOptionValues[i]))
                        {
                            bool bUpdateOption = false;
                            using (var cmd = new SqlCommand("SELECT OptionID FROM PNQOptions WHERE OptionID=@OptionID", conn))
                            {
                                cmd.Parameters.AddWithValue("@OptionID", arrOptionIDs[i]);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        bUpdateOption = true;
                                    }
                                }
                            }

                            if (bUpdateOption)
                            {
                                SqlStr = "UPDATE PNQOptions SET PollOption=@PollOption WHERE PollID=@PollID AND OptionID=@OptionID";
                            }
                            else
                            {
                                SqlStr = "INSERT INTO PNQOptions (OptionID, PollID, PollOption, Status) VALUES (@OptionID, @PollID, @PollOption, '1')";
                            }

                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                cmd.Parameters.AddWithValue("@OptionID", arrOptionIDs[i]);
                                cmd.Parameters.AddWithValue("@PollID", PollID);
                                cmd.Parameters.AddWithValue("@PollOption", arrOptionValues[i]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Save Assoiation ID
                using (var cmd = new SqlCommand("DELETE FROM Associations WHERE ModuleID='25' AND UniqueID=@PollID", conn))
                {
                    cmd.Parameters.AddWithValue("@PollID", PollID);
                    cmd.ExecuteNonQuery();
                }

                if (Strings.InStr(PortalIDs, "|-1|") > 0)
                {
                    using (var cmd = new SqlCommand("INSERT INTO Associations (AssociationID,UniqueID,ModuleID,PortalID) VALUES(@AssociationID, @PollID, '25', '-1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@AssociationID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@PollID", PollID);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    arrPortals = Strings.Split(Strings.Replace(PortalIDs, "|", string.Empty), ",");

                    if (arrPortals != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrPortals); i++)
                        {
                            using (var cmd = new SqlCommand("INSERT INTO Associations (AssociationID,UniqueID,ModuleID,PortalID) VALUES(@AssociationID, @PollID, '25', @PortalID)", conn))
                            {
                                cmd.Parameters.AddWithValue("@AssociationID", SepFunctions.GetIdentity());
                                cmd.Parameters.AddWithValue("@PollID", PollID);
                                cmd.Parameters.AddWithValue("@PortalID", arrPortals[i]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 25, Strings.ToString(PollID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Polls", "Polls", string.Empty);
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
        /// Polls the vote.
        /// </summary>
        /// <param name="PollID">The poll identifier.</param>
        /// <param name="OptionID">The option identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Poll_Vote(long PollID, long OptionID, long PortalID)
        {
            var bUpdate = false;
            var UserID = SepFunctions.Session_User_ID();

            if (SepFunctions.Check_User_Points(25, "PostVotePoll", "GetVotePoll", Strings.ToString(PollID), false))
            {
                if (SepFunctions.Check_Rating(25, Strings.ToString(PollID), UserID) == false)
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("SELECT PollOption FROM PNQOptions WHERE OptionID=@OptionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@OptionID", OptionID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    string sActDesc = SepFunctions.LangText("[[Username]] has voted for a poll") + Environment.NewLine;
                                    sActDesc += SepFunctions.LangText("Voted For: ~~" + SepFunctions.openNull(RS["PollOption"]) + "~~") + Environment.NewLine;
                                    SepFunctions.Activity_Write("VOTEPOLL", sActDesc, 25, Strings.ToString(PollID));
                                }
                            }
                        }

                        using (var cmd = new SqlCommand("SELECT PollID FROM PNQVotes WHERE PollID=@PollID AND OptionID=@OptionID AND PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PollID", PollID);
                            cmd.Parameters.AddWithValue("@OptionID", OptionID);
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    bUpdate = true;
                                }
                            }
                        }

                        string SqlStr;
                        if (bUpdate)
                        {
                            SqlStr = "UPDATE PNQVotes SET NumVotes=NumVotes+1 WHERE PollID=@PollID AND OptionID=@OptionID AND PortalID=@PortalID";
                        }
                        else
                        {
                            SqlStr = "INSERT INTO PNQVotes (PollID, OptionID, PortalID, NumVotes) VALUES(@PollID, @OptionID, @PortalID, '1')";
                        }

                        using (var cmd = new SqlCommand(SqlStr, conn))
                        {
                            cmd.Parameters.AddWithValue("@PollID", PollID);
                            cmd.Parameters.AddWithValue("@OptionID", OptionID);
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    SepFunctions.Write_Rating(25, Strings.ToString(PollID), UserID);
                }
                else
                {
                    return "{\"success\": \"false\", \"message\": \"" + SepFunctions.LangText("You have already voted for this poll.") + "\"}";
                }

                if (string.IsNullOrWhiteSpace(UserID))
                {
                    Session.setSession("RATE25" + PollID, "Yes");
                }
            }
            else
            {
                return "{\"success\": \"false\", \"message\": \"" + SepFunctions.LangText("You do not have enough points to vote for this poll.") + "\"}";
            }

            return "{\"success\": \"true\"}";
        }

        /// <summary>
        /// Polls the results.
        /// </summary>
        /// <param name="PollID">The poll identifier.</param>
        /// <returns>List&lt;Models.ChartData&gt;.</returns>
        public static List<ChartData> PollResults(long PollID)
        {
            var lData = new List<ChartData>();
            long Total = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT SUM(NumVotes) AS TotalVotes FROM PNQVotes WHERE PollID=@PollID", conn))
                {
                    cmd.Parameters.AddWithValue("@PollID", PollID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            Total += SepFunctions.toLong(SepFunctions.openNull(RS["TotalVotes"]));
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT PollOption,(SELECT SUM(NumVotes) AS TotalVotes FROM PNQVotes WHERE PollID=PNQOptions.PollID AND OptionID=PNQOptions.OptionID) AS SelectedCount FROM PNQOptions WHERE PollID=@PollID AND Status <> -1 ORDER BY PollOption", conn))
                {
                    cmd.Parameters.AddWithValue("@PollID", PollID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            var dData = new Models.ChartData { PollOption = SepFunctions.openNull(RS["PollOption"]) };
                            dData.NumVotes = SepFunctions.toLong(SepFunctions.openNull(RS["SelectedCount"]));
                            if (Total > 0)
                            {
                                dData.Percentage = SepFunctions.toDecimal(Strings.FormatNumber(SepFunctions.toDecimal(SepFunctions.openNull(RS["SelectedCount"])) / Total * 100, 1));
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