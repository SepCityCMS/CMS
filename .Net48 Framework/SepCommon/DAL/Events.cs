// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Events.cs" company="SepCity, Inc.">
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
    /// Class Events.
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Events the change status.
        /// </summary>
        /// <param name="EventIDs">The event i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Event_Change_Status(string EventIDs, int Status)
        {
            var arrEventIDs = Strings.Split(EventIDs, ",");
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                for (var i = 0; i <= Information.UBound(arrEventIDs); i++)
                {
                    using (var cmd = new SqlCommand("UPDATE EventCalendar SET Status=@Status WHERE LinkID=@LinkID", conn))
                    {
                        cmd.Parameters.AddWithValue("@LinkID", arrEventIDs[i]);
                        cmd.Parameters.AddWithValue("@Status", Status);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return SepFunctions.LangText("Event(s) status has been successfully saved.");
        }

        /// <summary>
        /// Events the delete.
        /// </summary>
        /// <param name="EventIDs">The event i ds.</param>
        /// <returns>System.String.</returns>
        public static string Event_Delete(string EventIDs)
        {
            var arrEventIDs = Strings.Split(EventIDs, ",");

            if (arrEventIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrEventIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE EventCalendar SET Status='-1', DateDeleted=@DateDeleted WHERE LinkID=@LinkID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LinkID", arrEventIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(46, arrEventIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Event(s) has been successfully deleted.");
        }

        /// <summary>
        /// Events the get.
        /// </summary>
        /// <param name="EventID">The event identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.Events.</returns>
        public static Models.Events Event_Get(long EventID, long ChangeID = 0)
        {
            var returnXML = new Models.Events();
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE EventCalendar SET Hits=Hits+1 WHERE LinkID=@EventID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@EventID", EventID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT *,(SELECT TypeName FROM EventTypes WHERE TypeID=EventCalendar.TypeID) AS EventType FROM EventCalendar WHERE EventID=@EventID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@EventID", EventID);
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
                                returnXML.EventID = SepFunctions.toLong(SepFunctions.openNull(RS["EventID"]));
                                returnXML.LinkID = SepFunctions.toLong(SepFunctions.openNull(RS["LinkID"]));
                                returnXML.TypeID = SepFunctions.toLong(SepFunctions.openNull(RS["TypeID"]));
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.EventDate = SepFunctions.toDate(SepFunctions.openNull(RS["EventDate"]));

                                string GetBegTime;
                                if (Strings.InStr(SepFunctions.openNull(RS["BegTime"]), ",") > 0)
                                {
                                    string[] arrBegTime = Strings.Split(SepFunctions.openNull(RS["BegTime"]), ",");
                                    Array.Resize(ref arrBegTime, 3);
                                    GetBegTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(arrBegTime[0] + ":" + arrBegTime[1] + " " + arrBegTime[2]), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                                }
                                else
                                {
                                    GetBegTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS["BegTime"])), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                                }

                                string GetEndTime;
                                if (Strings.InStr(SepFunctions.openNull(RS["EndTime"]), ",") > 0)
                                {
                                    string[] arrEndTime = Strings.Split(SepFunctions.openNull(RS["EndTime"]), ",");
                                    Array.Resize(ref arrEndTime, 3);
                                    GetEndTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(arrEndTime[0] + ":" + arrEndTime[1] + " " + arrEndTime[2]), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                                }
                                else
                                {
                                    GetEndTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS["EndTime"])), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                                }

                                returnXML.BegTime = GetBegTime;
                                returnXML.EndTime = GetEndTime;
                                returnXML.EventType = SepFunctions.openNull(RS["EventType"]);
                                returnXML.Subject = SepFunctions.openNull(RS["Subject"]);
                                returnXML.EventContent = SepFunctions.openNull(RS["Notes"]);
                                returnXML.Hits = SepFunctions.toLong(SepFunctions.openNull(RS["Hits"]));
                                returnXML.ShareEvent = SepFunctions.toBoolean(SepFunctions.openNull(RS["Shared"]));
                                returnXML.Recurring = SepFunctions.toLong(SepFunctions.openNull(RS["Recurring"]));
                                returnXML.Duration = SepFunctions.toLong(SepFunctions.openNull(RS["Duration"]));
                                returnXML.RecurringCycle = SepFunctions.openNull(RS["RecurringCycle"]);
                                returnXML.EventOnlinePrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["EventOnlinePrice"]));
                                returnXML.EventDoorPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS["EventDoorPrice"]));
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Events the save.
        /// </summary>
        /// <param name="EventID">The event identifier.</param>
        /// <param name="EventType">Type of the event.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="EventDate">The event date.</param>
        /// <param name="BegTime">The beg time.</param>
        /// <param name="EndTime">The end time.</param>
        /// <param name="Recurring">The recurring.</param>
        /// <param name="Duration">The duration.</param>
        /// <param name="RecurringCycle">The recurring cycle.</param>
        /// <param name="EventContent">Content of the event.</param>
        /// <param name="ShareEvent">if set to <c>true</c> [share event].</param>
        /// <param name="EventOnlinePrice">The event online price.</param>
        /// <param name="EventDoorPrice">The event door price.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Event_Save(long EventID, string EventType, string Subject, string UserID, DateTime EventDate, string BegTime, string EndTime, int Recurring, long Duration, string RecurringCycle, string EventContent, bool ShareEvent, decimal EventOnlinePrice, decimal EventDoorPrice, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            long GetHits = 0;
            var GetEventDate = DateTime.Today;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (EventID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM EventCalendar WHERE EventID=@EventID", conn))
                    {
                        cmd.Parameters.AddWithValue("@EventID", EventID);
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
                                GetHits = SepFunctions.toLong(SepFunctions.openNull(RS["Hits"]));
                                using (var cmd2 = new SqlCommand("DELETE FROM EventCalendar WHERE LinkID=@LinkID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@LinkID", EventID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                else
                {
                    EventID = SepFunctions.GetIdentity();
                }

                if (Duration < 2)
                {
                    Duration = 1;
                }

                for (var i = 1; i <= Duration; i++)
                {
                    long GetEventID;
                    if (i > 1)
                    {
                        GetEventID = SepFunctions.GetIdentity();
                    }
                    else
                    {
                        GetEventID = EventID;
                    }

                    long GetLinkID = EventID;
                    switch (RecurringCycle)
                    {
                        case "D":
                            GetEventDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Day, (i - 1) * Recurring, EventDate);
                            break;

                        case "W":
                            GetEventDate = DateAndTime.DateAdd(DateAndTime.DateInterval.WeekOfYear, (i - 1) * Recurring, EventDate);
                            break;

                        case "M":
                            GetEventDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Month, (i - 1) * Recurring, EventDate);
                            break;

                        case "Y":
                            GetEventDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Year, (i - 1) * Recurring, EventDate);
                            break;
                    }

                    using (var cmd = new SqlCommand("INSERT INTO EventCalendar (EventID,LinkID,TypeID,UserID,EventDate,BegTime,EndTime,Subject,Notes,Shared,PortalID,Recurring,Duration,RecurringCycle,EventOnlinePrice,EventDoorPrice,Hits,Status) VALUES(@EventID,@LinkID,@TypeID,@UserID,@EventDate,@BegTime,@EndTime,@Subject,@Notes,@Shared,@PortalID,@Recurring,@Duration,@RecurringCycle,@EventOnlinePrice,@EventDoorPrice,@Hits,'1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@EventID", GetEventID);
                        cmd.Parameters.AddWithValue("@LinkID", GetLinkID);
                        cmd.Parameters.AddWithValue("@TypeID", EventType);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@EventDate", GetEventDate);
                        cmd.Parameters.AddWithValue("@BegTime", BegTime);
                        cmd.Parameters.AddWithValue("@EndTime", EndTime);
                        cmd.Parameters.AddWithValue("@Subject", Subject);
                        cmd.Parameters.AddWithValue("@Notes", EventContent);
                        cmd.Parameters.AddWithValue("@Shared", ShareEvent);
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        cmd.Parameters.AddWithValue("@Recurring", Recurring);
                        cmd.Parameters.AddWithValue("@EventOnlinePrice", EventOnlinePrice);
                        cmd.Parameters.AddWithValue("@EventDoorPrice", EventDoorPrice);
                        cmd.Parameters.AddWithValue("@Duration", Duration);
                        cmd.Parameters.AddWithValue("@RecurringCycle", RecurringCycle);
                        cmd.Parameters.AddWithValue("@Hits", GetHits);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 46, Strings.ToString(EventID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Event", "Event", string.Empty);

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM EventCalendar WHERE EventID=@EventID", conn))
                    {
                        cmd.Parameters.AddWithValue("@EventID", EventID);
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
                        SepFunctions.Update_Change_Log(46, Strings.ToString(EventID), Subject, Strings.ToString(writeLog));
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
        /// Events the type delete.
        /// </summary>
        /// <param name="TypeIDs">The type i ds.</param>
        /// <returns>System.String.</returns>
        public static string Event_Type_Delete(string TypeIDs)
        {
            var arrTypeIDs = Strings.Split(TypeIDs, ",");

            if (arrTypeIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrTypeIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE EventCalendar SET Status='-1', DateDeleted=@DateDeleted WHERE TypeID=@TypeID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TypeID", arrTypeIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE EventTypes SET Status='-1', DateDeleted=@DateDeleted WHERE TypeID=@TypeID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TypeID", arrTypeIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Event Type(s) has been successfully deleted.");
        }

        /// <summary>
        /// Events the type get.
        /// </summary>
        /// <param name="TypeID">The type identifier.</param>
        /// <returns>Models.EventTypes.</returns>
        public static EventTypes Event_Type_Get(long TypeID)
        {
            var returnXML = new Models.EventTypes();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM EventTypes WHERE TypeID=@TypeID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TypeID", TypeID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TypeID = SepFunctions.toLong(SepFunctions.openNull(RS["TypeID"]));
                            returnXML.TypeName = SepFunctions.openNull(RS["TypeName"]);
                            returnXML.AccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                            returnXML.WriteKeys = SepFunctions.openNull(RS["WriteKeys"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Events the type save.
        /// </summary>
        /// <param name="TypeID">The type identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="TypeName">Name of the type.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="WriteKeys">The write keys.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Event_Type_Save(long TypeID, string UserID, string TypeName, string AccessKeys, string WriteKeys, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (TypeID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT TypeID FROM EventTypes WHERE TypeID=@TypeID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TypeID", TypeID);
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
                    TypeID = SepFunctions.GetIdentity();
                }


                string SqlStr;
                if (bUpdate)
                {
                    SqlStr = "UPDATE EventTypes SET TypeName=@TypeName, AccessKeys=@AccessKeys, WriteKeys=@WriteKeys, PortalID=@PortalID WHERE TypeID=@TypeID";
                }
                else
                {
                    SqlStr = "INSERT INTO EventTypes (TypeID, TypeName, AccessKeys, WriteKeys, PortalID, Status) VALUES (@TypeID, @TypeName, @AccessKeys, @WriteKeys, @PortalID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@TypeID", TypeID);
                    cmd.Parameters.AddWithValue("@TypeName", TypeName);
                    cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                    cmd.Parameters.AddWithValue("@WriteKeys", WriteKeys);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 46, Strings.ToString(TypeID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "EventType", "Event type", string.Empty);

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
        /// Gets the events.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="EventDate">The event date.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="EventTypeID">The event type identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.Events&gt;.</returns>
        public static List<Models.Events> GetEvents(string SortExpression = "EventDate", string SortDirection = "DESC", string EventDate = "", string searchWords = "", long EventTypeID = 0, string UserID = "", string StartDate = "")
        {
            var lEvents = new List<Models.Events>();

            var wClause = string.Empty;
            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "EventDate";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Subject LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (Information.IsDate(EventDate))
            {
                wClause += " AND EventDate BETWEEN '" + SepFunctions.toSQLDate(EventDate) + "' AND '" + SepFunctions.toSQLDate(EventDate + "23:59:59") + "'";
            }

            if (EventTypeID > 0)
            {
                wClause += " AND TypeID='" + SepFunctions.FixWord(Strings.ToString(EventTypeID)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND EventDate > '" + SepFunctions.toSQLDate(StartDate) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT EventID,BegTime,EndTime,Subject,EventDate,UserID,Status,(SELECT TypeName FROM EventTypes WHERE TypeID=EventCalendar.TypeID) AS EventType,(SELECT UserName FROM Members WHERE UserID=EventCalendar.UserID AND Status=1) AS UserName FROM EventCalendar WHERE EventID=LinkID AND Status <> -1 AND PortalID=@PortalID" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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


                    string GetBegTime;
                    if (Strings.InStr(SepFunctions.openNull(ds.Tables[0].Rows[i]["BegTime"]), ",") > 0)
                    {
                        string[] arrBegTime = Strings.Split(SepFunctions.openNull(ds.Tables[0].Rows[i]["BegTime"]), ",");
                        Array.Resize(ref arrBegTime, 3);
                        GetBegTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(arrBegTime[0] + ":" + arrBegTime[1] + " " + arrBegTime[2]), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                    }
                    else
                    {
                        GetBegTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["BegTime"])), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                    }

                    string GetEndTime;
                    if (Strings.InStr(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndTime"]), ",") > 0)
                    {
                        string[] arrEndTime = Strings.Split(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndTime"]), ",");
                        Array.Resize(ref arrEndTime, 3);
                        GetEndTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(arrEndTime[0] + ":" + arrEndTime[1] + " " + arrEndTime[2]), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                    }
                    else
                    {
                        GetEndTime = DateTime.Parse(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EndTime"])), Strings.DateNamedFormat.ShortTime)).ToString(@"hh\:mm tt");
                    }

                    var dEvents = new Models.Events { EventID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["EventID"])) };
                    dEvents.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    dEvents.EventType = SepFunctions.openNull(ds.Tables[0].Rows[i]["EventType"]);
                    dEvents.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dEvents.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dEvents.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dEvents.EventDate = SepFunctions.toDate(Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["EventDate"])), Strings.DateNamedFormat.ShortDate));
                    dEvents.BegTime = GetBegTime;
                    dEvents.EndTime = GetEndTime;
                    lEvents.Add(dEvents);
                }
            }

            return lEvents;
        }

        /// <summary>
        /// Gets the event types.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.EventTypes&gt;.</returns>
        public static List<EventTypes> GetEventTypes(string SortExpression = "TypeName", string SortDirection = "ASC", string searchWords = "")
        {
            var lEventTypes = new List<EventTypes>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "TypeName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND TypeName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT TypeID,TypeName FROM EventTypes WHERE Status <> -1 AND PortalID=@PortalID" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dEventTypes = new Models.EventTypes { TypeID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TypeID"])) };
                    dEventTypes.TypeName = SepFunctions.openNull(ds.Tables[0].Rows[i]["TypeName"]);
                    lEventTypes.Add(dEventTypes);
                }
            }

            return lEventTypes;
        }
    }
}