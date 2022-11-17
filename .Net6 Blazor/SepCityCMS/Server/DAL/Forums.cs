// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Forums.cs" company="SepCity, Inc.">
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
    /// Class Forums.
    /// </summary>
    public static class Forums
    {
        /// <summary>
        /// Gets the forum topics.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CatId">The cat identifier.</param>
        /// <param name="TopicID">The topic identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.ForumTopics&gt;.</returns>
        public static List<ForumTopics> GetForumTopics(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "", long CatId = 0, long TopicID = 0, string UserID = "", string StartDate = "")
        {
            var lForumTopics = new List<ForumTopics>();

            var wClause = string.Empty;
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "Mod.Status <> -1 AND Mod.UserID=M.UserID AND ReplyID='" + SepFunctions.FixWord(Strings.ToString(TopicID)) + "'";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (Mod.Subject LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR M.Username LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (CatId > 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CatId)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.*,M.Username,M.CreateDate,(SELECT Count(TopicID) FROM ForumsMessages WHERE UserID=Mod.UserID) AS TotalPosts,(SELECT TOP 1 FileName FROM Uploads WHERE ModuleID='12' AND UniqueID=Mod.TopicID) AS Attachment FROM ForumsMessages AS Mod,Members AS M" + SepFunctions.Category_SQL_Manage_Select(CatId, wClause, true) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dForumTopics = new Models.ForumTopics { TopicID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TopicID"])) };
                    dForumTopics.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dForumTopics.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    dForumTopics.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dForumTopics.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dForumTopics.ProfileID = SepFunctions.userProfileID(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    dForumTopics.Message = SepFunctions.openNull(ds.Tables[0].Rows[i]["Message"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["Attachment"])))
                    {
                        dForumTopics.Message += "<br/><br/>" + SepFunctions.LangText("Attachment:") + " <a href=\"" + sInstallFolder + "download_attachment.aspx?ModuleID=12&UniqueID=" + SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TopicID"])) + "\" target=\"_blank\">" + SepFunctions.openNull(ds.Tables[0].Rows[i]["Attachment"]) + "</a>";
                    }

                    dForumTopics.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dForumTopics.DateRegistered = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["CreateDate"]));
                    dForumTopics.TotalPosts = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalPosts"]));
                    dForumTopics.ProfileImage = SepFunctions.userProfileImage(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    dForumTopics.OnlineStatus = SepFunctions.userOnlineStatus(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    lForumTopics.Add(dForumTopics);
                }
            }

            return lForumTopics;
        }

        /// <summary>
        /// Topics the delete.
        /// </summary>
        /// <param name="TopicIDs">The topic i ds.</param>
        /// <returns>System.String.</returns>
        public static string Topic_Delete(string TopicIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrTopicIDs = Strings.Split(TopicIDs, ",");

                if (arrTopicIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTopicIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ForumsMessages SET Status='-1', DateDeleted=@DateDeleted WHERE TopicID=@TopicID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TopicID", arrTopicIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ForumsMessages SET Status='-1', DateDeleted=@DateDeleted WHERE ReplyID=@ReplyID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ReplyID", arrTopicIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(12, arrTopicIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Topic(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Topics the get.
        /// </summary>
        /// <param name="TopicID">The topic identifier.</param>
        /// <returns>Models.ForumTopics.</returns>
        public static ForumTopics Topic_Get(long TopicID)
        {
            var returnXML = new Models.ForumTopics();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE ForumsMessages SET Hits=Hits+1 WHERE TopicID=@TopicID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TopicID", TopicID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT FM.*,Members.UserName,Members.CreateDate,(SELECT TOP 1 FileName FROM Uploads WHERE ModuleID='12' AND UniqueID=FM.TopicID) AS Attachment,(SELECT TOP 1 ProfileID FROM Profiles WHERE UserID=FM.UserID) AS ProfileID,(SELECT Count(TopicID) AS Counter FROM ForumsMessages WHERE UserID=FM.UserID) AS TotalPosts FROM ForumsMessages AS FM,Members WHERE FM.TopicID=@TopicID AND FM.UserID=Members.UserID AND FM.Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TopicID", TopicID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TopicID = SepFunctions.toLong(SepFunctions.openNull(RS["TopicID"]));
                            returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                            returnXML.ProfileID = SepFunctions.toLong(SepFunctions.openNull(RS["ProfileID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.Username = SepFunctions.openNull(RS["UserName"]);
                            returnXML.Subject = SepFunctions.openNull(RS["Subject"]);
                            returnXML.Message = SepFunctions.openNull(RS["Message"]);
                            returnXML.Attachment = SepFunctions.openNull(RS["Attachment"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.ReplyID = SepFunctions.toLong(SepFunctions.openNull(RS["ReplyID"]));
                            returnXML.Replies = SepFunctions.toLong(SepFunctions.openNull(RS["Replies"]));
                            returnXML.Hits = SepFunctions.toLong(SepFunctions.openNull(RS["Hits"]));
                            returnXML.EmailReply = SepFunctions.openBoolean(RS["EmailReply"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.ExpireDate = SepFunctions.toDate(SepFunctions.openNull(RS["ExpireDate"]));
                            returnXML.DateRegistered = SepFunctions.toDate(SepFunctions.openNull(RS["CreateDate"]));
                            returnXML.TotalPosts = SepFunctions.toLong(SepFunctions.openNull(RS["TotalPosts"]));
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
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="ReplyID">The reply identifier.</param>
        /// <param name="EmailReply">if set to <c>true</c> [email reply].</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="Message">The message.</param>
        /// <param name="FileInfo">The file information.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        // TODO
        //public static int Topic_Save(long TopicID, string UserID, long CatID, long ReplyID, bool EmailReply, string Subject, string Message, FileUpload FileInfo, long PortalID)
        //{
        //    var bUpdate = false;
        //    var isNewRecord = false;
        //    var intReturn = 0;

        //    var GetCatInfo = string.Empty;
        //    var EmailSubject = string.Empty;
        //    var EmailBody = string.Empty;
        //    var GetAdminEmail = SepFunctions.Setup(991, "AdminEmailAddress");
        //    var tmpUsername = string.Empty;

        //    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
        //    {
        //        conn.Open();

        //        if (TopicID > 0)
        //        {
        //            using (var cmd = new SqlCommand("SELECT TopicID FROM ForumsMessages WHERE TopicID=@TopicID", conn))
        //            {
        //                cmd.Parameters.AddWithValue("@TopicID", TopicID);
        //                using (SqlDataReader RS = cmd.ExecuteReader())
        //                {
        //                    if (RS.HasRows)
        //                    {
        //                        bUpdate = true;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            TopicID = SepFunctions.GetIdentity();
        //        }

        //        if (CatID > 0)
        //        {
        //            using (var cmd = new SqlCommand("SELECT CategoryName,Moderator FROM Categories WHERE CatID=@CatID", conn))
        //            {
        //                cmd.Parameters.AddWithValue("@CatID", CatID);
        //                using (SqlDataReader RS = cmd.ExecuteReader())
        //                {
        //                    if (RS.HasRows)
        //                    {
        //                        RS.Read();
        //                        GetCatInfo = SepFunctions.openNull(RS["CategoryName"]);
        //                        if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["Moderator"])))
        //                        {
        //                            using (var cmd2 = new SqlCommand("SELECT Username,EmailAddress FROM Members WHERE Username='" + SepFunctions.openNull(RS["Moderator"], true) + "'", conn))
        //                            {
        //                                cmd2.Parameters.AddWithValue("@TopicID", TopicID);
        //                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
        //                                {
        //                                    if (RS2.HasRows)
        //                                    {
        //                                        RS2.Read();
        //                                        EmailSubject = SepFunctions.Session_User_Name() + " " + SepFunctions.LangText("has posted a new topic on") + " " + SepFunctions.Setup(992, "WebSiteName");
        //                                        EmailBody = "<b>" + SepFunctions.LangText("Forum") + "</b> " + GetCatInfo + "<br/><b>" + SepFunctions.LangText("Subject") + "</b> " + Subject + "<br/><br/><b>" + SepFunctions.LangText("Topic Link") + "</b> <a href=\"" + SepFunctions.GetSiteDomain() + "forum/" + TopicID + "/" + SepFunctions.Format_ISAPI(Subject) + "/\">" + SepFunctions.GetSiteDomain() + "forum/" + TopicID + "/" + SepFunctions.Format_ISAPI(Subject) + "/</a>";
        //                                        SepFunctions.Send_Email(SepFunctions.openNull(RS2["EmailAddress"]), GetAdminEmail, EmailSubject, EmailBody, 12);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (ReplyID > 0)
        //        {
        //            using (var cmd = new SqlCommand("SELECT ReplyID,UserID,EmailReply FROM ForumsMessages WHERE TopicID=@TopicID", conn))
        //            {
        //                cmd.Parameters.AddWithValue("@TopicID", ReplyID);
        //                using (SqlDataReader RS = cmd.ExecuteReader())
        //                {
        //                    if (RS.HasRows)
        //                    {
        //                        while (RS.Read())
        //                        {
        //                            if (SepFunctions.openBoolean(RS["EmailReply"]) && !string.IsNullOrWhiteSpace(GetAdminEmail))
        //                            {
        //                                using (var cmd2 = new SqlCommand("SELECT Username,EmailAddress FROM Members WHERE UserID='" + SepFunctions.openNull(RS["UserID"], true) + "'", conn))
        //                                {
        //                                    cmd2.Parameters.AddWithValue("@TopicID", TopicID);
        //                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
        //                                    {
        //                                        if (RS2.HasRows)
        //                                        {
        //                                            RS2.Read();
        //                                            if (tmpUsername != SepFunctions.openNull(RS2["Username"]))
        //                                            {
        //                                                EmailSubject = SepFunctions.Session_User_Name() + " " + SepFunctions.LangText("has replied to your post at") + " " + SepFunctions.Setup(992, "WebSiteName");
        //                                                EmailBody = "<b>" + SepFunctions.LangText("Forum") + "</b> " + GetCatInfo + "<br/><b>" + SepFunctions.LangText("Subject") + "</b> " + Subject + "<br/><br/><b>" + SepFunctions.LangText("Topic Link") + "</b> <a href=\"" + SepFunctions.GetSiteDomain() + "forum/" + TopicID + "/" + SepFunctions.Format_ISAPI(Subject) + "/\">" + SepFunctions.GetSiteDomain() + "forum/" + SepFunctions.UrlEncode(Strings.ToString(TopicID)) + "/" + SepFunctions.Format_ISAPI(Subject) + "/</a>";
        //                                                SepFunctions.Send_Email(SepFunctions.openNull(RS2["EmailAddress"]), GetAdminEmail, EmailSubject, EmailBody, 12);
        //                                            }

        //                                            tmpUsername = SepFunctions.openNull(RS2["Username"]);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        var ContentType = FileInfo.PostedFile.ContentType;
        //        var fileName = FileInfo.FileName;
        //        var FileData = new byte[SepFunctions.toInt(Strings.ToString(FileInfo.PostedFile.InputStream.Length)) + 1];
        //        FileInfo.PostedFile.InputStream.Read(FileData, 0, FileData.Length);

        //        if (!string.IsNullOrWhiteSpace(fileName))
        //        {
        //            using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, FileData, PortalID) VALUES (@UploadID, @UniqueID, @UserID, '12', @FileName, @FileSize, @ContentType, '0', '1', @DatePosted, @FileData, @PortalID)", conn))
        //            {
        //                cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
        //                cmd.Parameters.AddWithValue("@UniqueID", TopicID);
        //                cmd.Parameters.AddWithValue("@UserID", UserID);
        //                cmd.Parameters.AddWithValue("@FileName", fileName);
        //                cmd.Parameters.AddWithValue("@FileSize", FileData.Length);
        //                cmd.Parameters.AddWithValue("@ContentType", ContentType);
        //                cmd.Parameters.AddWithValue("@FileData", FileData);
        //                cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
        //                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
        //                cmd.ExecuteNonQuery();
        //            }
        //        }

        //        var SqlStr = string.Empty;
        //        if (bUpdate)
        //        {
        //            SqlStr = "UPDATE ForumsMessages SET CatID=@CatID, Subject=@Subject, Message=@Message, EmailReply=@EmailReply WHERE TopicID=@TopicID";
        //        }
        //        else
        //        {
        //            SqlStr = "INSERT INTO ForumsMessages (TopicID, CatID, DatePosted, ExpireDate, UserID, Subject, Message, ReplyID, Hits, Replies, EmailReply, PortalID, Status) VALUES (@TopicID, @CatID, @DatePosted, @ExpireDate, @UserID, @Subject, @Message, @ReplyID, '0', '0', @EmailReply, @PortalID, '1')";
        //        }

        //        using (var cmd = new SqlCommand(SqlStr, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@TopicID", TopicID);
        //            cmd.Parameters.AddWithValue("@CatID", CatID);
        //            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@ExpireDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Day, SepFunctions.toInt(SepFunctions.Setup(12, "ForumsDeleteDays")), DateTime.Now));
        //            cmd.Parameters.AddWithValue("@UserID", UserID);
        //            cmd.Parameters.AddWithValue("@Subject", Subject);
        //            cmd.Parameters.AddWithValue("@Message", Message);
        //            cmd.Parameters.AddWithValue("@ReplyID", ReplyID);
        //            cmd.Parameters.AddWithValue("@EmailReply", EmailReply);
        //            cmd.Parameters.AddWithValue("@PortalID", PortalID);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //    // Write Additional Data
        //    if (bUpdate == false)
        //    {
        //        isNewRecord = true;
        //    }

        //    if (ReplyID > 0)
        //    {
        //        intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 12, Strings.ToString(TopicID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Forum", "Forums", "ReplyTopic");
        //    }
        //    else
        //    {
        //        intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 12, Strings.ToString(TopicID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Forum", "Forums", "PostTopic");
        //    }

        //    if (intReturn == 0)
        //    {
        //        if (bUpdate)
        //        {
        //            return 2;
        //        }

        //        return 3;
        //    }
        //    else
        //    {
        //        return intReturn;
        //    }
        //}
    }
}