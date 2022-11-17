// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Blogs.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class Blogs.
    /// </summary>
    public static class Blogs
    {
        /// <summary>
        /// Blogs the change status.
        /// </summary>
        /// <param name="BlogIDs">The blog i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Blog_Change_Status(string BlogIDs, int Status)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrBlogIDs = Strings.Split(BlogIDs, ",");

                if (arrBlogIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrBlogIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Blog SET Status=@Status WHERE BlogID=@BlogID", conn))
                        {
                            cmd.Parameters.AddWithValue("@BlogID", arrBlogIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
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

            sReturn = SepFunctions.LangText("Blog(s) status has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Blogs the delete.
        /// </summary>
        /// <param name="BlogIDs">The blog i ds.</param>
        /// <returns>System.String.</returns>
        public static string Blog_Delete(string BlogIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrBlogIDs = Strings.Split(BlogIDs, ",");

                if (arrBlogIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrBlogIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Blog SET Status='-1', DateDeleted=@DateDeleted WHERE BlogID=@BlogID", conn))
                        {
                            cmd.Parameters.AddWithValue("@BlogID", arrBlogIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(61, arrBlogIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Blog(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Blogs the get.
        /// </summary>
        /// <param name="BlogID">The blog identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.Blogs.</returns>
        public static Models.Blogs Blog_Get(long BlogID, long ChangeID = 0)
        {
            var returnXML = new Models.Blogs();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Blog SET Hits=Hits+1 WHERE BlogID=@BlogID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@BlogID", BlogID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM Blog WHERE BlogID=@BlogID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@BlogID", BlogID);
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
                                returnXML.BlogID = SepFunctions.toLong(SepFunctions.openNull(RS["BlogID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.BlogName = SepFunctions.openNull(RS["BlogName"]);
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.Message = SepFunctions.openNull(RS["Message"]);
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.StartDate = SepFunctions.toDate(SepFunctions.openNull(RS["StartDate"]));
                                returnXML.EndDate = SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"]));
                                returnXML.Comments = SepFunctions.toBoolean(SepFunctions.openNull(RS["Comments"]));
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Hits = SepFunctions.toLong(SepFunctions.openNull(RS["Hits"]));
                                returnXML.Username = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                                returnXML.EmailReply = SepFunctions.toBoolean(SepFunctions.openNull(RS["EmailReply"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Blogs the save.
        /// </summary>
        /// <param name="BlogID">The blog identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="BlogName">Name of the blog.</param>
        /// <param name="Message">The message.</param>
        /// <param name="AllowComments">if set to <c>true</c> [allow comments].</param>
        /// <param name="Start_Date">The start date.</param>
        /// <param name="End_Date">The end date.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Blog_Save(long BlogID, long CatID, string UserID, string BlogName, string Message, bool AllowComments, DateTime Start_Date, DateTime End_Date, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (BlogID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Blog WHERE BlogID=@BlogID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BlogID", BlogID);
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
                    BlogID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Blog SET BlogName=@BlogName, CatID=@CatID, UserID=@UserID, Message=@Message, Comments=@Comments, EmailReply=@EmailReply, StartDate=@StartDate, EndDate=@EndDate, PortalID=@PortalID WHERE BlogID=@BlogID";
                }
                else
                {
                    SqlStr = "INSERT INTO Blog (BlogID, CatID, BlogName, UserID, Message, Comments, EmailReply, StartDate, EndDate, PortalID, DatePosted, Hits, Status) VALUES (@BlogID, @CatID, @BlogName, @UserID, @Message, @Comments, @EmailReply, @StartDate, @EndDate, @PortalID, @DatePosted, '0', '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@BlogID", BlogID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@BlogName", BlogName);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Message", Message);
                    cmd.Parameters.AddWithValue("@Comments", AllowComments);
                    cmd.Parameters.AddWithValue("@EmailReply", "1");
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

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 61, Strings.ToString(BlogID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Blog", "Blog", "AddBlog");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM Blog WHERE BlogID=@BlogID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BlogID", BlogID);
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
                        SepFunctions.Update_Change_Log(61, Strings.ToString(BlogID), BlogName, Strings.ToString(writeLog));
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
        /// Gets the blogs.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="showAvailable">if set to <c>true</c> [show available].</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.Blogs&gt;.</returns>
        public static List<Models.Blogs> GetBlogs(long CategoryId = -1, string SortExpression = "Mod.DatePosted", string SortDirection = "DESC", string searchWords = "", string UserID = "", bool showAvailable = false, string StartDate = "")
        {
            var lBlogs = new List<Models.Blogs>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Mod.DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "Mod.UserID=M.UserID AND Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (M.Username LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Mod.BlogName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
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
                    using (var cmd = new SqlCommand("SELECT Mod.BlogID,Mod.BlogName,Mod.Message,Mod.Status,Mod.DatePosted,M.Username FROM Blog AS Mod,Members AS M" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dBlogs = new Models.Blogs { BlogID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["BlogID"])) };
                    dBlogs.BlogName = SepFunctions.openNull(ds.Tables[0].Rows[i]["BlogName"]);
                    dBlogs.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dBlogs.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dBlogs.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dBlogs.Description = Strings.Left(SepFunctions.RemoveHTML(SepFunctions.openNull(ds.Tables[0].Rows[i]["Message"])), 500) + Strings.ToString(Strings.Len(SepFunctions.RemoveHTML(SepFunctions.openNull(ds.Tables[0].Rows[i]["Message"]))) > 500 ? " ..." : string.Empty);
                    lBlogs.Add(dBlogs);
                }
            }

            return lBlogs;
        }
    }
}