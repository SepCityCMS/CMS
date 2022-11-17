// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="LinkDirectory.cs" company="SepCity, Inc.">
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
    /// Class LinkDirectory.
    /// </summary>
    public static class LinkDirectory
    {
        /// <summary>
        /// Gets the links website.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.LinksWebsite&gt;.</returns>
        public static List<LinksWebsite> GetLinksWebsite(string SortExpression = "LinkName", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1, string UserID = "", string StartDate = "")
        {
            var lLinksWebsite = new List<LinksWebsite>();
            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "LinkName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            string wClause = "Mod.UserID=M.UserID AND Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND LinkName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + SepFunctions.toSQLDate(StartDate) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.LinkID,Mod.CatID,Mod.LinkName,Mod.LinkURL,Mod.Description,Mod.Status,Mod.DatePosted,Mod.UserID,M.UserName FROM LinksWebsites AS Mod,Members AS M" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dLinksWebsite = new Models.LinksWebsite { LinkID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["LinkID"])) };
                    dLinksWebsite.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dLinksWebsite.LinkName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LinkName"]);
                    dLinksWebsite.LinkURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["LinkURL"]);
                    dLinksWebsite.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dLinksWebsite.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dLinksWebsite.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dLinksWebsite.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dLinksWebsite.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lLinksWebsite.Add(dLinksWebsite);
                }
            }

            return lLinksWebsite;
        }

        /// <summary>
        /// Websites the change status.
        /// </summary>
        /// <param name="LinkIDs">The link i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Website_Change_Status(string LinkIDs, int Status)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrLinkIDs = Strings.Split(LinkIDs, ",");

                if (arrLinkIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrLinkIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE LinksWebsites SET Status=@Status WHERE LinkID=@LinkID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LinkID", arrLinkIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Website(s) status has been successfully saved.");
        }

        /// <summary>
        /// Websites the delete.
        /// </summary>
        /// <param name="LinkIDs">The link i ds.</param>
        /// <returns>System.String.</returns>
        public static string Website_Delete(string LinkIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrLinkIDs = Strings.Split(LinkIDs, ",");

                if (arrLinkIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrLinkIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE LinksWebsites SET Status='-1', DateDeleted=@DateDeleted WHERE LinkID=@LinkID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LinkID", arrLinkIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(19, arrLinkIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Website(s) has been successfully deleted.");
        }

        /// <summary>
        /// Websites the get.
        /// </summary>
        /// <param name="LinkID">The link identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.LinksWebsite.</returns>
        public static LinksWebsite Website_Get(long LinkID, long ChangeID = 0)
        {
            var returnXML = new Models.LinksWebsite();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE LinksWebsites SET Visits=Visits+1 WHERE LinkID=@LinkID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@LinkID", LinkID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM LinksWebsites WHERE LinkID=@LinkID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@LinkID", LinkID);
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
                                returnXML.LinkID = SepFunctions.toLong(SepFunctions.openNull(RS["LinkID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.LinkName = SepFunctions.openNull(RS["LinkName"]);
                                returnXML.LinkURL = SepFunctions.openNull(RS["LinkURL"]);
                                returnXML.Visits = SepFunctions.toLong(SepFunctions.openNull(RS["Visits"]));
                                returnXML.Description = SepFunctions.openNull(RS["Description"]);
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
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
        /// Websites the save.
        /// </summary>
        /// <param name="LinkID">The link identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="LinkName">Name of the link.</param>
        /// <param name="LinkURL">The link URL.</param>
        /// <param name="Description">The description.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Website_Save(long LinkID, string UserID, long CatID, string LinkName, string LinkURL, string Description, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (LinkID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM LinksWebsites WHERE LinkID=@LinkID", conn))
                    {
                        cmd.Parameters.AddWithValue("@LinkID", LinkID);
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
                    LinkID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE LinksWebsites SET CatID=@CatID, LinkName=@LinkName, LinkURL=@LinkURL, Description=@Description WHERE LinkID=@LinkID";
                }
                else
                {
                    SqlStr = "INSERT INTO LinksWebsites (LinkID, CatID, PortalID, LinkName, LinkURL, Description, Visits, UserID, DatePosted, Status) VALUES (@LinkID, @CatID, @PortalID, @LinkName, @LinkURL, @Description, @Visits, @UserID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@LinkID", LinkID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@LinkName", LinkName);
                    cmd.Parameters.AddWithValue("@LinkURL", SepFunctions.Format_URL(LinkURL));
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Visits", "0");
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 19, Strings.ToString(LinkID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Link", "Web site", "PostLink");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM LinksWebsites WHERE LinkID=@LinkID", conn))
                    {
                        cmd.Parameters.AddWithValue("@LinkID", LinkID);
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
                        SepFunctions.Update_Change_Log(19, Strings.ToString(LinkID), LinkName, Strings.ToString(writeLog));
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