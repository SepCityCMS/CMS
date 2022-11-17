// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="VideoConference.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class VideoConference.
    /// </summary>
    public static class VideoConference
    {

        /// <summary>
        /// Videoes the schedule get.
        /// </summary>
        /// <param name="MeetingID">The meeting identifier.</param>
        /// <returns>Models.VideoSchedule.</returns>
        public static Models.VideoSchedule VideoSchedule_Get(long MeetingID)
        {
            var returnXML = new Models.VideoSchedule();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM VideoSchedule WHERE MeetingID=@MeetingID", conn))
                {
                    cmd.Parameters.AddWithValue("@MeetingID", MeetingID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.MeetingID = SepFunctions.toLong(SepFunctions.openNull(RS["MeetingID"]));
                            returnXML.StartDate = SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"]));
                            returnXML.Subject = SepFunctions.openNull(RS["Subject"]);
                            returnXML.Message = SepFunctions.openNull(RS["Message"]);
                            returnXML.Accepted = SepFunctions.toBoolean(SepFunctions.openNull(RS["Accepted"]));
                            returnXML.DateAccepted = SepFunctions.toDate(SepFunctions.openNull(RS["DateAccepted"]));
                            returnXML.Notes = SepFunctions.openNull(RS["Notes"]);
                            returnXML.FromUserID = SepFunctions.openNull(RS["FromUserID"]);
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Videoes the schedule save.
        /// </summary>
        /// <param name="MeetingID">The meeting identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="Message">The message.</param>
        /// <param name="Accepted">if set to <c>true</c> [accepted].</param>
        /// <param name="DateAccepted">The date accepted.</param>
        /// <param name="Notes">The notes.</param>
        /// <param name="FromUserID">From user identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int VideoSchedule_Save(long MeetingID, DateTime StartDate, string Subject, string Message, bool Accepted, DateTime DateAccepted, string Notes, string FromUserID, string UserID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (MeetingID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM VideoSchedule WHERE MeetingID=@MeetingID", conn))
                    {
                        cmd.Parameters.AddWithValue("@MeetingID", MeetingID);
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
                    MeetingID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE VideoSchedule SET StartDate=@StartDate, Subject=@Subject, UserID=@UserID, Message=@Message, Accepted=@Accepted, DateAccepted=@DateAccepted, Notes=@Notes, FromUserID=@FromUserID WHERE MeetingID=@MeetingID";
                }
                else
                {
                    SqlStr = "INSERT INTO VideoSchedule (MeetingID, StartDate, Subject, UserID, Message, Accepted, DateAccepted, Notes, FromUserID) VALUES (@MeetingID, @StartDate, @Subject, @UserID, @Message, @Accepted, @DateAccepted, @Notes, @FromUserID)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@MeetingID", MeetingID);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@Message", Message);
                    cmd.Parameters.AddWithValue("@Accepted", Accepted);
                    cmd.Parameters.AddWithValue("@DateAccepted", DateAccepted);
                    cmd.Parameters.AddWithValue("@Notes", Notes);
                    cmd.Parameters.AddWithValue("@FromUserID", FromUserID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                if (SepFunctions.Setup(69, "VideoConferenceSendSMS") == "Yes")
                {
                    if (bUpdate)
                    {
                        var cTwilio = new SepCommon.TwilioGlobal();
                        cTwilio.Send_SMS(SepFunctions.FormatPhone(SepFunctions.GetUserInformation("PhoneNUmber", UserID)), SepFunctions.GetUserInformation("UserName", FromUserID) + " has rescheduled a video call with you on " + StartDate);
                    }
                    else
                    {
                        var cTwilio = new SepCommon.TwilioGlobal();
                        cTwilio.Send_SMS(SepFunctions.FormatPhone(SepFunctions.GetUserInformation("PhoneNUmber", UserID)), SepFunctions.GetUserInformation("UserName", FromUserID) + " has scheduled a video call with you on " + StartDate);
                        if (SepFunctions.toInt(SepFunctions.Setup(69, "VideoConferenceSMSOffset")) > 0)
                        {
                            BackgroundProcesses.BackgroundSMSSave(UserID, "You have a video meeting with " + SepFunctions.GetUserInformation("UserName", FromUserID) + " in " + SepFunctions.toInt(SepFunctions.Setup(69, "VideoConferenceSMSOffset")) + " minutes. Click the link below to start the meeting " + SepFunctions.GetMasterDomain(false) + "video/default.aspx?MeetingID=" + MeetingID, SepCore.DateAndTime.DateAdd(DateAndTime.DateInterval.Minute, -SepFunctions.toInt(SepFunctions.Setup(69, "VideoConferenceSMSOffset")), DateTime.Now));
                            BackgroundProcesses.BackgroundSMSSave(FromUserID, "You have a video meeting with " + SepFunctions.GetUserInformation("UserName", UserID) + " in " + SepFunctions.toInt(SepFunctions.Setup(69, "VideoConferenceSMSOffset")) + " minutes. Click the link below to start the meeting " + SepFunctions.GetMasterDomain(false) + "video/default.aspx?MeetingID=" + MeetingID, SepCore.DateAndTime.DateAdd(DateAndTime.DateInterval.Minute, -SepFunctions.toInt(SepFunctions.Setup(69, "VideoConferenceSMSOffset")), DateTime.Now));
                        }
                    }
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 69, Strings.ToString(MeetingID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "VideoSchedule", "Video Schedule", "AddMeeting");

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
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="FromUserId"></param>
        /// <param name="SortExpression"></param>
        /// <param name="SortDirection"></param>
        /// <param name="searchWords"></param>
        /// <returns></returns>
        public static List<Models.VideoSchedule> GetSchedule(string UserId = "", string FromUserId = "", string SortExpression = "StartDate", string SortDirection = "ASC", string searchWords = "")
        {
            var lVideoSchedule = new List<Models.VideoSchedule>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "StartDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(UserId))
            {
                if (!string.IsNullOrWhiteSpace(wClause))
                {
                    wClause += " AND ";
                }
                wClause += " UserID='" + SepFunctions.FixWord(UserId) + "'";
            }

            if (!string.IsNullOrWhiteSpace(FromUserId))
            {
                if (!string.IsNullOrWhiteSpace(wClause))
                {
                    wClause += " AND ";
                }
                wClause += " FromUserID='" + SepFunctions.FixWord(FromUserId) + "'";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                if (!string.IsNullOrWhiteSpace(wClause))
                {
                    wClause += " AND ";
                }
                wClause += " Subject LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(wClause))
            {
                wClause = " WHERE " + wClause;
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT MeetingID,Subject,StartDate,UserID,FromUserID FROM VideoSchedule" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dVideoSchedule = new Models.VideoSchedule { MeetingID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["MeetingID"])) };
                    dVideoSchedule.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    dVideoSchedule.StartDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["StartDate"]));
                    dVideoSchedule.FromUserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["FromUserID"]);
                    dVideoSchedule.FromUserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(ds.Tables[0].Rows[i]["FromUserID"]));
                    dVideoSchedule.ToUserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    lVideoSchedule.Add(dVideoSchedule);
                }
            }

            return lVideoSchedule;
        }

        /// <summary>
        /// Videoes the configuration get.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.VideoConfig.</returns>
        public static Models.VideoConfig VideoConfig_Get(string UserID)
        {
            var returnXML = new Models.VideoConfig();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM VideoConfig WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.ContactOnline = SepFunctions.toBoolean(SepFunctions.openNull(RS["ContactOnline"]));
                            returnXML.SundayAvailableStart = SepFunctions.openNull(RS["SundayAvailableStart"]);
                            returnXML.SundayAvailableEnd = SepFunctions.openNull(RS["SundayAvailableEnd"]);
                            returnXML.MondayAvailableStart = SepFunctions.openNull(RS["MondayAvailableStart"]);
                            returnXML.MondayAvailableEnd = SepFunctions.openNull(RS["MondayAvailableEnd"]);
                            returnXML.TuesdayAvailableStart = SepFunctions.openNull(RS["TuesdayAvailableStart"]);
                            returnXML.TuesdayAvailableEnd = SepFunctions.openNull(RS["TuesdayAvailableEnd"]);
                            returnXML.WednesdayAvailableStart = SepFunctions.openNull(RS["WednesdayAvailableStart"]);
                            returnXML.WednesdayAvailableEnd = SepFunctions.openNull(RS["WednesdayAvailableEnd"]);
                            returnXML.ThursdayAvailableStart = SepFunctions.openNull(RS["ThursdayAvailableStart"]);
                            returnXML.ThursdayAvailableEnd = SepFunctions.openNull(RS["ThursdayAvailableEnd"]);
                            returnXML.FridayAvailableStart = SepFunctions.openNull(RS["FridayAvailableStart"]);
                            returnXML.FridayAvailableEnd = SepFunctions.openNull(RS["FridayAvailableEnd"]);
                            returnXML.SaturdayAvailableStart = SepFunctions.openNull(RS["SaturdayAvailableStart"]);
                            returnXML.SaturdayAvailableEnd = SepFunctions.openNull(RS["SaturdayAvailableEnd"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        public static int VideoConfig_Save(string UserID, bool ContactOnline, string SundayAvailableStart, string SundayAvailableEnd, string MondayAvailableStart, string MondayAvailableEnd, string TuesdayAvailableStart, string TuesdayAvailableEnd, string WednesdayAvailableStart, string WednesdayAvailableEnd, string ThursdayAvailableStart, string ThursdayAvailableEnd, string FridayAvailableStart, string FridayAvailableEnd, string SaturdayAvailableStart, string SaturdayAvailableEnd)
        {
            var bUpdate = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(UserID))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM VideoConfig WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
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
                    return 0;
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE VideoConfig SET ContactOnline=@ContactOnline, SundayAvailableStart=@SundayAvailableStart, SundayAvailableEnd=@SundayAvailableEnd " +
                        ", MondayAvailableStart=@MondayAvailableStart, MondayAvailableEnd=@MondayAvailableEnd " +
                        ", TuesdayAvailableStart=@TuesdayAvailableStart, TuesdayAvailableEnd=@TuesdayAvailableEnd " +
                        ", WednesdayAvailableStart=@WednesdayAvailableStart, WednesdayAvailableEnd=@WednesdayAvailableEnd " +
                        ", ThursdayAvailableStart=@ThursdayAvailableStart, ThursdayAvailableEnd=@ThursdayAvailableEnd " +
                        ", FridayAvailableStart=@FridayAvailableStart, FridayAvailableEnd=@FridayAvailableEnd " +
                        ", SaturdayAvailableStart=@SaturdayAvailableStart, SaturdayAvailableEnd=@SaturdayAvailableEnd WHERE UserID=@UserID";
                }
                else
                {
                    SqlStr = "INSERT INTO VideoConfig (UserID, ContactOnline, SundayAvailableStart, SundayAvailableEnd" +
                        ", MondayAvailableStart, MondayAvailableEnd" +
                        ", TuesdayAvailableStart, TuesdayAvailableEnd" +
                        ", WednesdayAvailableStart, WednesdayAvailableEnd" +
                        ", ThursdayAvailableStart, ThursdayAvailableEnd" +
                        ", FridayAvailableStart, FridayAvailableEnd" +
                        ", SaturdayAvailableStart, SaturdayAvailableEnd) VALUES (@UserID, @ContactOnline, @SundayAvailableStart, @SundayAvailableEnd" +
                        ", @MondayAvailableStart, @MondayAvailableEnd" +
                        ", @TuesdayAvailableStart, @TuesdayAvailableEnd" +
                        ", @WednesdayAvailableStart, @WednesdayAvailableEnd" +
                        ", @ThursdayAvailableStart, @ThursdayAvailableEnd" +
                        ", @FridayAvailableStart, @FridayAvailableEnd" +
                        ", @SaturdayAvailableStart, @SaturdayAvailableEnd)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@ContactOnline", ContactOnline);
                    cmd.Parameters.AddWithValue("@SundayAvailableStart", SundayAvailableStart);
                    cmd.Parameters.AddWithValue("@SundayAvailableEnd", SundayAvailableEnd);
                    cmd.Parameters.AddWithValue("@MondayAvailableStart", MondayAvailableStart);
                    cmd.Parameters.AddWithValue("@MondayAvailableEnd", MondayAvailableEnd);
                    cmd.Parameters.AddWithValue("@TuesdayAvailableStart", TuesdayAvailableStart);
                    cmd.Parameters.AddWithValue("@TuesdayAvailableEnd", TuesdayAvailableEnd);
                    cmd.Parameters.AddWithValue("@WednesdayAvailableStart", WednesdayAvailableStart);
                    cmd.Parameters.AddWithValue("@WednesdayAvailableEnd", WednesdayAvailableEnd);
                    cmd.Parameters.AddWithValue("@ThursdayAvailableStart", ThursdayAvailableStart);
                    cmd.Parameters.AddWithValue("@ThursdayAvailableEnd", ThursdayAvailableEnd);
                    cmd.Parameters.AddWithValue("@FridayAvailableStart", FridayAvailableStart);
                    cmd.Parameters.AddWithValue("@FridayAvailableEnd", FridayAvailableEnd);
                    cmd.Parameters.AddWithValue("@SaturdayAvailableStart", SaturdayAvailableStart);
                    cmd.Parameters.AddWithValue("@SaturdayAvailableEnd", SaturdayAvailableEnd);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
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
    }
}
