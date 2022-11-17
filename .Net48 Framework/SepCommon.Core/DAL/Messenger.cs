// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Messenger.cs" company="SepCity, Inc.">
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
    /// Class Messenger.
    /// </summary>
    public static class Messenger
    {
        /// <summary>
        /// Blocks the user delete.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="BlockIDs">The block i ds.</param>
        /// <returns>System.String.</returns>
        public static string Block_User_Delete(string UserID, string BlockIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrBlockIDs = Strings.Split(BlockIDs, ",");

                if (arrBlockIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrBlockIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM MessengerBlocked WHERE ID=@BlockID AND UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@BlockID", arrBlockIDs[i]);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
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

            sReturn = SepFunctions.LangText("User(s) has been successfully un-blocked.");

            return sReturn;
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>List&lt;Models.Messages&gt;.</returns>
        public static List<Messages> GetMessages(string SortExpression = "Subject", string SortDirection = "ASC", string searchWords = "", string UserID = "")
        {
            var lMessages = new List<Messages>();

            var wClause = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

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
                wClause += "(Subject LIKE '%" + SepFunctions.FixWord(searchWords) + "%') ";
            }

            if (!string.IsNullOrWhiteSpace(searchWords) && !string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND ";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += "ToUserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (!string.IsNullOrWhiteSpace(wClause))
            {
                wClause = " WHERE " + wClause;
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ID, ToUserID, (SELECT TOP 1 Username FROM Members WHERE UserID=Messenger.FromUserID) AS Username, Subject, DateSent, ReadNew,(SELECT TOP 1 UploadID FROM Uploads WHERE Uploads.ModuleID='63' AND Uploads.UserID=Messenger.FromUserID AND Uploads.isTemp='0' AND Uploads.Approved='1' AND (Right(FileName,4) = '.png' OR Right(FileName,4) = '.gif' OR Right(FileName,4) = '.jpg' OR Right(FileName,5) = '.jpeg') ORDER BY Weight) AS UploadID FROM Messenger" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dMessages = new Models.Messages { MessageID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dMessages.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=63&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        dMessages.DefaultPicture = sImageFolder + "images/public/no-photo.jpg";
                    }

                    dMessages.ToUserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["ToUserID"]);
                    dMessages.FromUsername = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dMessages.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    dMessages.DateSent = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateSent"]));
                    dMessages.ReadNew = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["ReadNew"]));
                    lMessages.Add(dMessages);
                }
            }

            return lMessages;
        }

        /// <summary>
        /// Gets the messages blocked users.
        /// </summary>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>List&lt;Models.MessagesBlockedUsers&gt;.</returns>
        public static List<MessagesBlockedUsers> GetMessagesBlockedUsers(string searchWords = "", string UserID = "")
        {
            var lMessagesBlockedUsers = new List<MessagesBlockedUsers>();

            var wClause = string.Empty;

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (M.UserName LIKE '" + SepFunctions.FixWord(searchWords) + "%') ";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND MB.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT MB.ID,MB.UserID,MB.FromUserID,M.Username,M.FirstName,M.LastName FROM MessengerBlocked AS MB,Members AS M WHERE MB.FromUserID=M.UserID" + wClause + " ORDER BY Username ASC", conn))
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

                    var dMessagesBlockedUsers = new Models.MessagesBlockedUsers { BlockID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                    dMessagesBlockedUsers.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dMessagesBlockedUsers.FromUserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["FromUserID"]);
                    dMessagesBlockedUsers.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dMessagesBlockedUsers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dMessagesBlockedUsers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    lMessagesBlockedUsers.Add(dMessagesBlockedUsers);
                }
            }

            return lMessagesBlockedUsers;
        }

        /// <summary>
        /// Gets the messages sent.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>List&lt;Models.Messages&gt;.</returns>
        public static List<Messages> GetMessagesSent(string SortExpression = "Subject", string SortDirection = "ASC", string searchWords = "", string UserID = "")
        {
            var lMessages = new List<Messages>();

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
                wClause += "(Subject LIKE '%" + SepFunctions.FixWord(searchWords) + "%') ";
            }

            if (!string.IsNullOrWhiteSpace(searchWords) && !string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND ";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += "FromUserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (!string.IsNullOrWhiteSpace(wClause))
            {
                wClause = " WHERE " + wClause;
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ID, ToUserID, (SELECT TOP 1 Username FROM Members WHERE UserID=MessengerSent.FromUserID) AS Username, (SELECT TOP 1 Username FROM Members WHERE UserID=MessengerSent.ToUserID) AS ToUsername, Subject, DateSent FROM MessengerSent" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dMessages = new Models.Messages { MessageID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                    dMessages.ToUserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["ToUserID"]);
                    dMessages.FromUsername = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dMessages.ToUsername = SepFunctions.openNull(ds.Tables[0].Rows[i]["ToUsername"]);
                    dMessages.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    dMessages.DateSent = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateSent"]));
                    lMessages.Add(dMessages);
                }
            }

            return lMessages;
        }

        /// <summary>
        /// Messages the block user save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="BlockUserName">Name of the block user.</param>
        /// <returns>System.String.</returns>
        public static string Message_Block_User_Save(string UserID, string BlockUserName)
        {
            var sReturn = string.Empty;

            var BlockUserID = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserName=@UserName AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", BlockUserName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            sReturn = SepFunctions.LangText("Sorry, no such user exists in our system.");
                        }
                        else
                        {
                            RS.Read();
                            BlockUserID = SepFunctions.openNull(RS["UserID"]);
                        }
                    }
                }

                if (BlockUserID == UserID)
                {
                    sReturn = SepFunctions.LangText("You cannot block yourself from receiving messages.");
                }

                using (var cmd = new SqlCommand("SELECT UserID FROM MessengerBlocked WHERE UserID=@UserID AND FromUserID=@FromUserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@FromUserID", BlockUserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            sReturn = SepFunctions.LangText("You have already blocked this username.");
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(sReturn))
                {
                    using (var cmd = new SqlCommand("INSERT INTO MessengerBlocked(UserID, FromUserID) VALUES(@UserID, @FromUserID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@FromUserID", BlockUserID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(sReturn))
            {
                return sReturn;
            }

            sReturn = SepFunctions.LangText("You have successfully blocked ~~" + BlockUserName + "~~.");

            return sReturn;
        }

        /// <summary>
        /// Messages the delete.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="MessageIDs">The message i ds.</param>
        /// <returns>System.String.</returns>
        public static string Message_Delete(string UserID, string MessageIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrMessageIDs = Strings.Split(MessageIDs, ",");

                if (arrMessageIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrMessageIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM Messenger WHERE ID=@MessageID AND ToUserID=@ToUserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@MessageID", arrMessageIDs[i]);
                            cmd.Parameters.AddWithValue("@ToUserID", UserID);
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

            sReturn = SepFunctions.LangText("Message(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Messages the get.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="MessageID">The message identifier.</param>
        /// <returns>Models.Messages.</returns>
        public static Messages Message_Get(string UserID, long MessageID)
        {
            var returnXML = new Models.Messages();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT *,(SELECT Username FROM Members WHERE UserID=Messenger.ToUserID) AS ToUserName,(SELECT Username FROM Members WHERE UserID=Messenger.FromUserID) AS FromUserName FROM Messenger WHERE ID=@MessageID AND (ToUserID=@ToUserID OR FromUserID=@FromUserID)", conn))
                {
                    cmd.Parameters.AddWithValue("@MessageID", MessageID);
                    cmd.Parameters.AddWithValue("@ToUserID", UserID);
                    cmd.Parameters.AddWithValue("@FromUserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.MessageID = MessageID;
                            returnXML.ToUserID = SepFunctions.openNull(RS["ToUserID"]);
                            returnXML.FromUserID = SepFunctions.openNull(RS["FromUserID"]);
                            returnXML.ToUsername = SepFunctions.openNull(RS["ToUserName"]);
                            returnXML.FromUsername = SepFunctions.openNull(RS["FromUserName"]);
                            returnXML.Subject = SepFunctions.openNull(RS["Subject"]);
                            returnXML.Message = SepFunctions.openNull(RS["Message"]);
                            returnXML.ReadNew = SepFunctions.toBoolean(SepFunctions.openNull(RS["ReadNew"]));
                            returnXML.DateSent = SepFunctions.toDate(SepFunctions.openNull(RS["DateSent"]));
                            returnXML.UserPhoto = SepFunctions.userProfileImage(SepFunctions.openNull(RS["FromUserID"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Messages the send.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ToUserName">Converts to username.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="Message">The message.</param>
        /// <param name="WriteSentItems">if set to <c>true</c> [write sent items].</param>
        /// <returns>System.String.</returns>
        public static string Message_Send(string UserID, string ToUserName, string Subject, string Message, bool WriteSentItems)
        {
            var sReturn = string.Empty;

            var ToUserID = string.Empty;
            var ToEmailAddress = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserID,EmailAddress FROM Members WHERE UserName=@UserName AND Status=1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", ToUserName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            sReturn = SepFunctions.LangText("Sorry, no such user exists in our system.");
                        }
                        else
                        {
                            RS.Read();
                            ToEmailAddress = SepFunctions.openNull(RS["EmailAddress"]);
                            if (Strings.InStr(ToEmailAddress, "@") == 0 && Strings.InStr(ToEmailAddress, ".") == 0)
                            {
                                sReturn = SepFunctions.LangText("User has an invalid email address.");
                            }

                            ToUserID = SepFunctions.openNull(RS["UserID"]);
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT UserID FROM MessengerBlocked WHERE UserID=@UserID AND FromUserID=@FromUserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", ToUserID);
                    cmd.Parameters.AddWithValue("@FromUserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            sReturn = SepFunctions.LangText("You have been blocked by this user.");
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(sReturn))
                {
                    var EmailSubject = SepFunctions.LangText("You have a new message at ~~" + SepFunctions.Setup(992, "WebSiteName") + "~~");
                    var EmailBody = SepFunctions.LangText("You have received a new message from ~~" + SepFunctions.Session_User_Name() + "~~") + "<br/><br/>" + SepFunctions.LangText("To read this message please login to") + " <a href=\"" + SepFunctions.GetMasterDomain(true) + "/messenger.aspx\">" + SepFunctions.GetMasterDomain(true) + "/messenger.aspx</a>.";

                    if (Strings.InStr(Subject, "would like a call back.") == 0)
                    {
                        SepFunctions.Send_Email(ToEmailAddress, SepFunctions.Setup(17, "AdminEmailAddress"), EmailSubject, EmailBody, 17);
                    }

                    using (var cmd = new SqlCommand("INSERT INTO Messenger(ToUserID, FromUserID, DateSent, ExpireDate, Subject, Message, ReadNew) VALUES(@ToUserID, @FromUserID, @DateSent, @ExpireDate, @Subject, @Message, 0)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToUserID", ToUserID);
                        cmd.Parameters.AddWithValue("@FromUserID", UserID);
                        cmd.Parameters.AddWithValue("@DateSent", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ExpireDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Day, Convert.ToInt32(SepFunctions.toDouble(SepFunctions.Setup(17, "MessengerDeleteDays"))), DateTime.Now));
                        cmd.Parameters.AddWithValue("@Subject", Subject);
                        cmd.Parameters.AddWithValue("@Message", Message);
                        cmd.ExecuteNonQuery();
                    }

                    if (WriteSentItems)
                    {
                        using (var cmd = new SqlCommand("INSERT INTO MessengerSent(ToUserID, FromUserID, DateSent, ExpireDate, Subject, Message) VALUES(@ToUserID, @FromUserID, @DateSent, @ExpireDate, @Subject, @Message)", conn))
                        {
                            cmd.Parameters.AddWithValue("@ToUserID", ToUserID);
                            cmd.Parameters.AddWithValue("@FromUserID", UserID);
                            cmd.Parameters.AddWithValue("@DateSent", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ExpireDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Day, Convert.ToInt32(SepFunctions.toDouble(SepFunctions.Setup(17, "MessengerDeleteDays"))), DateTime.Now));
                            cmd.Parameters.AddWithValue("@Subject", Subject);
                            cmd.Parameters.AddWithValue("@Message", Message);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (SepFunctions.Setup(17, "MessengerSMS") == "Yes" && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAccountSID")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAuthToken")))
                    {
                        var cTwilio = new TwilioGlobal();
                        cTwilio.Send_SMS(SepFunctions.GetUserInformation("PhoneNumber", ToUserID), SepFunctions.RemoveHTML(Message));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(sReturn))
            {
                return sReturn;
            }

            sReturn = SepFunctions.LangText("Your message has been successfully sent to ~~" + SepFunctions.GetUserInformation("Username", ToUserID) + "~~.");

            return sReturn;
        }

        /// <summary>
        /// Messages the send mass message.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ToUserName">Converts to username.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="Message">The message.</param>
        /// <returns>System.String.</returns>
        public static string Message_Send_Mass_Message(string UserID, string ToUserName, string Subject, string Message)
        {
            if (Strings.Trim(ToUserName) != "[MASS_MESSAGE]")
            {
                return Message_Send(UserID, ToUserName, Subject, Message, true);
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserName FROM Members WHERE Status=1", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            Message_Send(UserID, ToUserName, Subject, Message, false);
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            sReturn = SepFunctions.LangText("Your message has been successfully sent to everyone.");

            return sReturn;
        }

        /// <summary>
        /// Messages the sent delete.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="MessageIDs">The message i ds.</param>
        /// <returns>System.String.</returns>
        public static string Message_Sent_Delete(string UserID, string MessageIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrMessageIDs = Strings.Split(MessageIDs, ",");

                if (arrMessageIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrMessageIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM MessengerSent WHERE ID=@MessageID AND FromUserID=@FromUserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@MessageID", arrMessageIDs[i]);
                            cmd.Parameters.AddWithValue("@FromUserID", UserID);
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

            sReturn = SepFunctions.LangText("Message(s) has been successfully deleted.");

            return sReturn;
        }
    }
}