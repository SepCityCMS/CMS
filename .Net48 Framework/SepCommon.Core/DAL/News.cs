// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="News.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class News.
    /// </summary>
    public static class News
    {
        /// <summary>
        /// Gets the news.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="showAvailable">if set to <c>true</c> [show available].</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.News&gt;.</returns>
        public static List<Models.News> GetNews(long CategoryId = -1, string SortExpression = "Mod.DatePosted", string SortDirection = "DESC", string searchWords = "", bool showAvailable = false, string StartDate = "")
        {
            var lNews = new List<Models.News>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Mod.DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Mod.Headline LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (showAvailable)
            {
                wClause += " AND Mod.StartDate <= GETDATE() AND Mod.EndDate >= GETDATE()";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.NewsID,Mod.Headline,Mod.Topic,Mod.DatePosted FROM News AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dNews = new Models.News { NewsID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["NewsID"])) };
                    dNews.Topic = SepFunctions.openNull(ds.Tables[0].Rows[i]["Topic"]);
                    dNews.Headline = SepFunctions.openNull(ds.Tables[0].Rows[i]["Headline"]);
                    dNews.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lNews.Add(dNews);
                }
            }

            return lNews;
        }

        /// <summary>
        /// Newses the delete.
        /// </summary>
        /// <param name="NewsIDs">The news i ds.</param>
        /// <returns>System.String.</returns>
        public static string News_Delete(string NewsIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrNewsIDs = Strings.Split(NewsIDs, ",");

                if (arrNewsIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrNewsIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE News SET Status='-1', DateDeleted=@DateDeleted WHERE NewsID=@NewsID", conn))
                        {
                            cmd.Parameters.AddWithValue("@NewsID", arrNewsIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(23, arrNewsIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("News Item(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Newses the get.
        /// </summary>
        /// <param name="NewsID">The news identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.News.</returns>
        public static Models.News News_Get(long NewsID, long ChangeID = 0)
        {
            var returnXML = new Models.News();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM News WHERE NewsID=@NewsID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@NewsID", NewsID);
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
                                returnXML.NewsID = SepFunctions.toLong(SepFunctions.openNull(RS["NewsID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.Topic = SepFunctions.openNull(RS["Topic"]);
                                returnXML.Headline = SepFunctions.openNull(RS["Headline"]);
                                returnXML.Message = SepFunctions.openNull(RS["Message"]);
                                returnXML.DisplayType = SepFunctions.openNull(RS["DisplayType"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Start_Date = SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"]));
                                returnXML.End_Date = SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"]));
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.ExpireDate = SepFunctions.toDate(SepFunctions.openNull(RS["ExpireDate"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Newses the save.
        /// </summary>
        /// <param name="NewsID">The news identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Topic">The topic.</param>
        /// <param name="Headline">The headline.</param>
        /// <param name="Story">The story.</param>
        /// <param name="DisplayType">The display type.</param>
        /// <param name="Start_Date">The start date.</param>
        /// <param name="End_Date">The end date.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int News_Save(long NewsID, string UserID, long CatID, string Topic, string Headline, string Story, string DisplayType, DateTime Start_Date, DateTime End_Date, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (NewsID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM News WHERE NewsID=@NewsID", conn))
                    {
                        cmd.Parameters.AddWithValue("@NewsID", NewsID);
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
                    NewsID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE News SET Topic=@Topic, CatID=@CatID, Headline=@Headline, Message=@Message, ExpireDate=@ExpireDate, DisplayType=@DisplayType, StartDate=@StartDate, EndDate=@EndDate, PortalID=@PortalID WHERE NewsID=@NewsID";
                }
                else
                {
                    SqlStr = "INSERT INTO News (NewsID, CatID, Topic, Headline, Message, ExpireDate, DisplayType, StartDate, EndDate, PortalID, DatePosted, Status) VALUES (@NewsID, @CatID, @Topic, @Headline, @Message, @ExpireDate, @DisplayType, @StartDate, @EndDate, @PortalID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@NewsID", NewsID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@Topic", Topic);
                    cmd.Parameters.AddWithValue("@Headline", Headline);
                    cmd.Parameters.AddWithValue("@Message", Story);
                    cmd.Parameters.AddWithValue("@ExpireDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Day, SepFunctions.toInt(SepFunctions.Setup(23, "NewsDeleteDays")), DateTime.Now));
                    cmd.Parameters.AddWithValue("@DisplayType", DisplayType);
                    cmd.Parameters.AddWithValue("@StartDate", Start_Date);
                    cmd.Parameters.AddWithValue("@EndDate", End_Date);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 23, Strings.ToString(NewsID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "News", "News story", string.Empty);

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM News WHERE NewsID=@NewsID", conn))
                    {
                        cmd.Parameters.AddWithValue("@NewsID", NewsID);
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
                        SepFunctions.Update_Change_Log(23, Strings.ToString(NewsID), Topic, Strings.ToString(writeLog));
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
    }
}