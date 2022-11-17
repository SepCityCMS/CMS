// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Newsletters.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Newsletters.
    /// </summary>
    public static class Newsletters
    {
        /// <summary>
        /// Gets the newsletters.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Newsletters&gt;.</returns>
        public static List<Models.Newsletters> GetNewsletters(string SortExpression = "NewsletName", string SortDirection = "ASC", string searchWords = "")
        {
            var lNewsletters = new List<Models.Newsletters>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "NewsletName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND NewsletName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT LetterID,NewsletName,Description FROM Newsletters WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dNewsletters = new Models.Newsletters { LetterID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["LetterID"])) };
                    dNewsletters.LetterName = SepFunctions.openNull(ds.Tables[0].Rows[i]["NewsletName"]);
                    dNewsletters.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    lNewsletters.Add(dNewsletters);
                }
            }

            return lNewsletters;
        }

        /// <summary>
        /// Gets the newsletters members.
        /// </summary>
        /// <param name="LetterID">The letter identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.NewslettersMembers&gt;.</returns>
        public static List<NewslettersMembers> GetNewslettersMembers(long LetterID, string SortExpression = "EmailAddress", string SortDirection = "ASC", string searchWords = "")
        {
            var lNewslettersMembers = new List<NewslettersMembers>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "EmailAddress";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (UserName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR EmailAddress LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT NUserID,EmailAddress,(SELECT TOP 1 Username FROM Members WHERE UserID=NewslettersUsers.UserID) AS Username,(SELECT TOP 1 FirstName FROM Members WHERE UserID=NewslettersUsers.UserID) AS FirstName,(SELECT TOP 1 LastName FROM Members WHERE UserID=NewslettersUsers.UserID) AS LastName FROM NewslettersUsers WHERE LetterID='" + SepFunctions.FixWord(Strings.ToString(LetterID)) + "' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dNewslettersMembers = new Models.NewslettersMembers { UserLetterID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["NUserID"])) };
                    dNewslettersMembers.UserName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"])) ? SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]) : SepFunctions.LangText("N/A"));
                    dNewslettersMembers.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dNewslettersMembers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dNewslettersMembers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    lNewslettersMembers.Add(dNewslettersMembers);
                }
            }

            return lNewslettersMembers;
        }

        /// <summary>
        /// Gets the newsletters sent.
        /// </summary>
        /// <param name="LetterID">The letter identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.NewslettersSent&gt;.</returns>
        public static List<NewslettersSent> GetNewslettersSent(long LetterID, string SortExpression = "DateSent", string SortDirection = "DESC", string searchWords = "")
        {
            var lNewslettersSent = new List<NewslettersSent>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DateSent";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND NewsletName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT SentID,LetterID,DateSent,EmailSubject FROM NewslettersSent WHERE LetterID='" + SepFunctions.FixWord(Strings.ToString(LetterID)) + "' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dNewslettersSent = new Models.NewslettersSent { SentID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SentID"])) };
                    dNewslettersSent.EmailSubject = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailSubject"]);
                    dNewslettersSent.DateSent = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateSent"]));
                    lNewslettersSent.Add(dNewslettersSent);
                }
            }

            return lNewslettersSent;
        }

        /// <summary>
        /// Newsletters the delete.
        /// </summary>
        /// <param name="LetterIDs">The letter i ds.</param>
        /// <returns>System.String.</returns>
        public static string Newsletter_Delete(string LetterIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrLetterIDs = Strings.Split(LetterIDs, ",");

                if (arrLetterIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrLetterIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Newsletters SET Status='-1', DateDeleted=@DateDeleted WHERE LetterID=@LetterID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LetterID", arrLetterIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE NewslettersSent SET Status='-1', DateDeleted=@DateDeleted WHERE LetterID=@LetterID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LetterID", arrLetterIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE NewslettersUsers SET Status='-1', DateDeleted=@DateDeleted WHERE LetterID=@LetterID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LetterID", arrLetterIDs[i]);
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

            sReturn = SepFunctions.LangText("Newsletter(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Newsletters the get.
        /// </summary>
        /// <param name="LetterID">The letter identifier.</param>
        /// <returns>Models.Newsletters.</returns>
        public static Models.Newsletters Newsletter_Get(long LetterID)
        {
            var returnXML = new Models.Newsletters();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Newsletters WHERE LetterID=@LetterID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@LetterID", LetterID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.LetterID = LetterID;
                            returnXML.LetterName = SepFunctions.openNull(RS["NewsletName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.JoinKeys = SepFunctions.openNull(RS["JoinKeys"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Newsletters the members delete.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Newsletter_Members_Delete(string UserIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE NewslettersUsers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
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

            sReturn = SepFunctions.LangText("Newsletter user(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Newsletters the save.
        /// </summary>
        /// <param name="LetterID">The letter identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="LetterName">Name of the letter.</param>
        /// <param name="Description">The description.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="JoinKeys">The join keys.</param>
        /// <returns>System.Int32.</returns>
        public static int Newsletter_Save(long LetterID, string UserID, string LetterName, string Description, string PortalIDs, string JoinKeys)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (LetterID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT LetterID FROM Newsletters WHERE LetterID=@LetterID", conn))
                    {
                        cmd.Parameters.AddWithValue("@LetterID", LetterID);
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
                    LetterID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Newsletters SET NewsletName=@NewsletName, Description=@Description, PortalIDs=@PortalIDs, JoinKeys=@JoinKeys WHERE LetterID=@LetterID";
                }
                else
                {
                    SqlStr = "INSERT INTO Newsletters (LetterID, NewsletName, Description, PortalIDs, JoinKeys, Status) VALUES (@LetterID, @NewsletName, @Description, @PortalIDs, @JoinKeys, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@LetterID", LetterID);
                    cmd.Parameters.AddWithValue("@NewsletName", LetterName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@PortalIDs", PortalIDs);
                    cmd.Parameters.AddWithValue("@JoinKeys", JoinKeys);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 24, Strings.ToString(LetterID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Newsletter", "Newsletter", string.Empty);

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
        /// Newsletters the sent delete.
        /// </summary>
        /// <param name="SentIDs">The sent i ds.</param>
        /// <returns>System.String.</returns>
        public static string Newsletter_Sent_Delete(string SentIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrSentIDs = Strings.Split(SentIDs, ",");

                if (arrSentIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSentIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE NewslettersSent SET Status='-1', DateDeleted=@DateDeleted WHERE SentID=@SentID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SentID", arrSentIDs[i]);
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

            sReturn = SepFunctions.LangText("Sent newsletter(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Newsletters the sent get.
        /// </summary>
        /// <param name="SentID">The sent identifier.</param>
        /// <returns>Models.NewslettersSent.</returns>
        public static NewslettersSent Newsletter_Sent_Get(long SentID)
        {
            var returnXML = new Models.NewslettersSent();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM NewslettersSent WHERE SentID=@SentID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@SentID", SentID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.SentID = SentID;
                            returnXML.EmailSubject = SepFunctions.openNull(RS["EmailSubject"]);
                            returnXML.EmailBody = SepFunctions.openNull(RS["EmailBody"]);
                            returnXML.DateSent = SepFunctions.toDate(SepFunctions.openNull(RS["DateSent"]));
                        }
                    }
                }
            }

            return returnXML;
        }
    }
}