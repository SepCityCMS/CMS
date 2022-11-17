// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Guestbook.cs" company="SepCity, Inc.">
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
    /// Class Guestbook.
    /// </summary>
    public static class Guestbook
    {
        /// <summary>
        /// Entries the delete.
        /// </summary>
        /// <param name="EntryIDs">The entry i ds.</param>
        /// <returns>System.String.</returns>
        public static string Entry_Delete(string EntryIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrEntryIDs = Strings.Split(EntryIDs, ",");

                if (arrEntryIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrEntryIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Guestbook SET Status='-1', DateDeleted=@DateDeleted WHERE EntryID=@EntryID", conn))
                        {
                            cmd.Parameters.AddWithValue("@EntryID", arrEntryIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(14, arrEntryIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Guestbook Entry(s) has been successfully deleted.");
        }

        /// <summary>
        /// Entries the get.
        /// </summary>
        /// <param name="EntryID">The entry identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.GuestbookEntries.</returns>
        public static GuestbookEntries Entry_Get(long EntryID, long ChangeID = 0)
        {
            var returnXML = new Models.GuestbookEntries();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Guestbook WHERE EntryID=@EntryID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@EntryID", EntryID);
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
                                returnXML.EntryID = SepFunctions.toLong(SepFunctions.openNull(RS["EntryID"]));
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.EmailAddress = SepFunctions.openNull(RS["EmailAddress"]);
                                returnXML.SiteURL = SepFunctions.openNull(RS["SiteURL"]);
                                returnXML.Message = SepFunctions.openNull(RS["Message"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.ExpireDate = SepFunctions.toDate(SepFunctions.openNull(RS["ExpireDate"]));
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Entries the save.
        /// </summary>
        /// <param name="EntryID">The entry identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="SiteURL">The site URL.</param>
        /// <param name="Message">The message.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Entry_Save(long EntryID, string UserID, string EmailAddress, string SiteURL, string Message, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (EntryID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Guestbook WHERE EntryID=@EntryID", conn))
                    {
                        cmd.Parameters.AddWithValue("@EntryID", EntryID);
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
                    EntryID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Guestbook SET EmailAddress=@EmailAddress, SiteURL=@SiteURL, Message=@Message, PortalID=@PortalID WHERE EntryID=@EntryID";
                }
                else
                {
                    SqlStr = "INSERT INTO Guestbook (EntryID, UserID, EmailAddress, SiteURL, Message, PortalID, DatePosted, Status) VALUES (@EntryID, @UserID, @EmailAddress, @SiteURL, @Message, @PortalID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@EntryID", EntryID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                    cmd.Parameters.AddWithValue("@SiteURL", SepFunctions.Format_URL(SiteURL));
                    cmd.Parameters.AddWithValue("@Message", Message);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 14, Strings.ToString(EntryID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Entry", "Guestbook Entry", "SignGuestbook");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM Guestbook WHERE EntryID=@EntryID", conn))
                    {
                        cmd.Parameters.AddWithValue("@EntryID", EntryID);
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
                        SepFunctions.Update_Change_Log(14, Strings.ToString(EntryID), EmailAddress, Strings.ToString(writeLog));
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
        /// Gets the guestbook entries.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.GuestbookEntries&gt;.</returns>
        public static List<GuestbookEntries> GetGuestbookEntries(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "")
        {
            var lGuestbookEntries = new List<GuestbookEntries>();

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
                wClause = " AND M.Username LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT G.EntryID,G.Message,G.DatePosted,G.UserID,G.SiteURL,M.Username FROM Guestbook AS G,Members AS M WHERE G.UserID=M.UserID AND G.PortalID=@PortalID AND G.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dGuestbookEntries = new Models.GuestbookEntries { EntryID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["EntryID"])) };
                    dGuestbookEntries.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dGuestbookEntries.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dGuestbookEntries.SiteURL = Strings.Left(SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteURL"]), 7) != "http://" ? "http://" : string.Empty + SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteURL"]);
                    dGuestbookEntries.Message = SepFunctions.openNull(ds.Tables[0].Rows[i]["Message"]);
                    dGuestbookEntries.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lGuestbookEntries.Add(dGuestbookEntries);
                }
            }

            return lGuestbookEntries;
        }
    }
}