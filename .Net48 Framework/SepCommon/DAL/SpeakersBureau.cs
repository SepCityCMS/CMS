// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SpeakersBureau.cs" company="SepCity, Inc.">
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
    /// Class SpeakersBureau.
    /// </summary>
    public static class SpeakersBureau
    {
        /// <summary>
        /// Gets the speaker bureau speeches.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="TopicID">The topic identifier.</param>
        /// <param name="SpeakerID">The speaker identifier.</param>
        /// <returns>List&lt;Models.SpeakerBureauSpeeches&gt;.</returns>
        public static List<SpeakerBureauSpeeches> GetSpeakerBureauSpeeches(string SortExpression = "Subject", string SortDirection = "ASC", string searchWords = "", long TopicID = 0, long SpeakerID = 0)
        {
            var lSpeakerBureauSpeeches = new List<SpeakerBureauSpeeches>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Subject";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND Subject LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (TopicID > 0)
            {
                wClause = " AND TopicID='" + SepFunctions.FixWord(Strings.ToString(TopicID)) + "'";
            }

            if (SpeakerID > 0)
            {
                wClause = " AND SpeakerID='" + SepFunctions.FixWord(Strings.ToString(SpeakerID)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT SpeechID,SpeakerID,TopicID,Subject FROM SpeakSpeeches WHERE PortalID=@PortalID AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dSpeakerBureauSpeeches = new Models.SpeakerBureauSpeeches { SpeechID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SpeechID"])) };
                    dSpeakerBureauSpeeches.SpeakerID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SpeakerID"]));
                    dSpeakerBureauSpeeches.TopicID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TopicID"]));
                    dSpeakerBureauSpeeches.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    lSpeakerBureauSpeeches.Add(dSpeakerBureauSpeeches);
                }
            }

            return lSpeakerBureauSpeeches;
        }

        /// <summary>
        /// Gets the speaker bureau topics.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.SpeakerBureauTopics&gt;.</returns>
        public static List<SpeakerBureauTopics> GetSpeakerBureauTopics(string SortExpression = "TopicName", string SortDirection = "ASC", string searchWords = "")
        {
            var lSpeakerBureauTopics = new List<SpeakerBureauTopics>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "TopicName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND TopicName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT TopicID,TopicName FROM SpeakTopics WHERE PortalID=@PortalID AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dSpeakerBureauTopics = new Models.SpeakerBureauTopics { TopicID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TopicID"])) };
                    dSpeakerBureauTopics.TopicName = SepFunctions.openNull(ds.Tables[0].Rows[i]["TopicName"]);
                    lSpeakerBureauTopics.Add(dSpeakerBureauTopics);
                }
            }

            return lSpeakerBureauTopics;
        }

        /// <summary>
        /// Gets the speakers.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Speakers&gt;.</returns>
        public static List<Speakers> GetSpeakers(string SortExpression = "LastName", string SortDirection = "ASC", string searchWords = "")
        {
            var lSpeakers = new List<Speakers>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "LastName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (LastName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR FirstName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT SpeakerID,LastName,FirstName FROM Speakers WHERE PortalID=@PortalID AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dSpeakers = new Models.Speakers { SpeakerID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SpeakerID"])) };
                    dSpeakers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dSpeakers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    lSpeakers.Add(dSpeakers);
                }
            }

            return lSpeakers;
        }

        /// <summary>
        /// Speakers the delete.
        /// </summary>
        /// <param name="SpeakerIDs">The speaker i ds.</param>
        /// <returns>System.String.</returns>
        public static string Speaker_Delete(string SpeakerIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrSpeakerIDs = Strings.Split(SpeakerIDs, ",");

                if (arrSpeakerIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSpeakerIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Speakers SET Status='-1', DateDeleted=@DateDeleted WHERE SpeakerID=@SpeakerID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SpeakerID", arrSpeakerIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE SpeakSpeeches SET Status='-1', DateDeleted=@DateDeleted WHERE SpeakerID=@SpeakerID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SpeakerID", arrSpeakerIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(50, arrSpeakerIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Speaker(s) has been successfully deleted.");
        }

        /// <summary>
        /// Speakers the get.
        /// </summary>
        /// <param name="SpeakerID">The speaker identifier.</param>
        /// <returns>Models.Speakers.</returns>
        public static Speakers Speaker_Get(long SpeakerID)
        {
            var returnXML = new Models.Speakers();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT Speakers.*,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='50' AND UniqueID=Speakers.SpeakerID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID FROM Speakers WHERE SpeakerID=@SpeakerID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@SpeakerID", SpeakerID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.SpeakerID = SepFunctions.toLong(SepFunctions.openNull(RS["SpeakerID"]));
                            returnXML.FirstName = SepFunctions.openNull(RS["FirstName"]);
                            returnXML.LastName = SepFunctions.openNull(RS["LastName"]);
                            returnXML.EmailAddress = SepFunctions.openNull(RS["EmailAddress"]);
                            returnXML.Title = SepFunctions.openNull(RS["Title"]);
                            returnXML.Cred = SepFunctions.openNull(RS["Cred"]);
                            returnXML.Bio = SepFunctions.openNull(RS["Bio"]);
                            returnXML.Photo = sImageFolder + "spadmin/show_image.aspx?ModuleID=35&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }

                if (returnXML.SpeakerID == 0)
                {
                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE UniqueID=@SpeakerID AND ModuleID='50' AND isTemp='0' AND Approved='1'", conn))
                    {
                        cmd.Parameters.AddWithValue("@SpeakerID", SpeakerID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.Photo = sImageFolder + "spadmin/show_image.aspx?ModuleID=50&Size=thumb&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Speakers the save.
        /// </summary>
        /// <param name="SpeakerID">The speaker identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="Cred">The cred.</param>
        /// <param name="Bio">The bio.</param>
        /// <param name="Title">The title.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Speaker_Save(long SpeakerID, string UserID, string FirstName, string LastName, string EmailAddress, string Cred, string Bio, string Title, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SpeakerID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT SpeakerID FROM Speakers WHERE SpeakerID=@SpeakerID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SpeakerID", SpeakerID);
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
                    SpeakerID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Speakers SET FirstName=@FirstName, LastName=@LastName, EmailAddress=@EmailAddress, Cred=@Cred, Bio=@Bio, Title=@Title, PortalID=@PortalID WHERE SpeakerID=@SpeakerID";
                }
                else
                {
                    SqlStr = "INSERT INTO Speakers (SpeakerID, FirstName, LastName, EmailAddress, Cred, Bio, Title, PortalID, Status) VALUES (@SpeakerID, @FirstName, @LastName, @EmailAddress, @Cred, @Bio, @Title, @PortalID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@SpeakerID", SpeakerID);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                    cmd.Parameters.AddWithValue("@Cred", Cred);
                    cmd.Parameters.AddWithValue("@Bio", Bio);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 50, Strings.ToString(SpeakerID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Speaker", "Speaker", string.Empty);
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
        /// Speeches the delete.
        /// </summary>
        /// <param name="SpeechIDs">The speech i ds.</param>
        /// <returns>System.String.</returns>
        public static string Speech_Delete(string SpeechIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrSpeechIDs = Strings.Split(SpeechIDs, ",");

                if (arrSpeechIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSpeechIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE SpeakSpeeches SET Status='-1', DateDeleted=@DateDeleted WHERE SpeechID=@SpeechID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SpeechID", arrSpeechIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Speech has been successfully deleted.");
        }

        /// <summary>
        /// Speeches the save.
        /// </summary>
        /// <param name="SpeechID">The speech identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="SpeakerID">The speaker identifier.</param>
        /// <param name="TopicID">The topic identifier.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Speech_Save(long SpeechID, string UserID, long SpeakerID, long TopicID, string Subject, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SpeechID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT SpeechID FROM SpeakSpeeches WHERE SpeechID=@SpeechID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SpeechID", SpeechID);
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
                    SpeechID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE SpeakSpeeches SET SpeakerID=@SpeakerID, TopicID=@TopicID, Subject=@Subject, PortalID=@PortalID WHERE SpeechID=@SpeechID";
                }
                else
                {
                    SqlStr = "INSERT INTO SpeakSpeeches (SpeechID, SpeakerID, TopicID, Subject, PortalID, Status) VALUES (@SpeechID, @SpeakerID, @TopicID, @Subject, @PortalID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@SpeechID", SpeechID);
                    cmd.Parameters.AddWithValue("@SpeakerID", SpeakerID);
                    cmd.Parameters.AddWithValue("@TopicID", TopicID);
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 50, Strings.ToString(SpeechID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Speech", "Speaker Bureau Speech", string.Empty);
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
        /// Topics the delete.
        /// </summary>
        /// <param name="TopicIDs">The topic i ds.</param>
        /// <returns>System.String.</returns>
        public static string Topic_Delete(string TopicIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrTopicIDs = Strings.Split(TopicIDs, ",");

                if (arrTopicIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTopicIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE SpeakTopics SET Status='-1', DateDeleted=@DateDeleted WHERE TopicID=@TopicID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TopicID", arrTopicIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE SpeakSpeeches SET Status='-1', DateDeleted=@DateDeleted WHERE TopicID=@TopicID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TopicID", arrTopicIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Topic(s) has been successfully deleted.");
        }

        /// <summary>
        /// Topics the get.
        /// </summary>
        /// <param name="TopicID">The topic identifier.</param>
        /// <returns>Models.SpeakerBureauTopics.</returns>
        public static SpeakerBureauTopics Topic_Get(long TopicID)
        {
            var returnXML = new Models.SpeakerBureauTopics();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM SpeakTopics WHERE TopicID=@TopicID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TopicID", TopicID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TopicID = SepFunctions.toLong(SepFunctions.openNull(RS["TopicID"]));
                            returnXML.TopicName = SepFunctions.openNull(RS["TopicName"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Topics the save.
        /// </summary>
        /// <param name="TopicID">The topic identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="TopicName">Name of the topic.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Topic_Save(long TopicID, string UserID, string TopicName, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (TopicID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT TopicID FROM SpeakTopics WHERE TopicID=@TopicID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TopicID", TopicID);
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
                    TopicID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE SpeakTopics SET TopicName=@TopicName, PortalID=@PortalID WHERE TopicID=@TopicID";
                }
                else
                {
                    SqlStr = "INSERT INTO SpeakTopics (TopicID, TopicName, PortalID, Status) VALUES (@TopicID, @TopicName, @PortalID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@TopicID", TopicID);
                    cmd.Parameters.AddWithValue("@TopicName", TopicName);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 50, Strings.ToString(TopicID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Topic", "Speaker Bureau Topic", string.Empty);
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