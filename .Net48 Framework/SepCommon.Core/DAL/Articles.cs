// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Articles.cs" company="SepCity, Inc.">
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
    /// Class Articles.
    /// </summary>
    public static class Articles
    {
        /// <summary>
        /// Articles the change status.
        /// </summary>
        /// <param name="ArticleIDs">The article i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Article_Change_Status(string ArticleIDs, int Status)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrArticleIDs = Strings.Split(ArticleIDs, ",");

                if (arrArticleIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrArticleIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Articles SET Status=@Status WHERE ArticleID=@ArticleID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ArticleID", arrArticleIDs[i]);
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

            sReturn = SepFunctions.LangText("Article(s) status has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Articles the delete.
        /// </summary>
        /// <param name="ArticleIDs">The article i ds.</param>
        /// <returns>System.String.</returns>
        public static string Article_Delete(string ArticleIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrArticleIDs = Strings.Split(ArticleIDs, ",");

                if (arrArticleIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrArticleIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Articles SET Status='-1', DateDeleted=@DateDeleted WHERE ArticleID=@ArticleID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ArticleID", arrArticleIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(35, arrArticleIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;
            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Article(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Articles the get.
        /// </summary>
        /// <param name="ArticleID">The article identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.Articles.</returns>
        public static Models.Articles Article_Get(long ArticleID, long ChangeID = 0)
        {
            var returnXML = new Models.Articles();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Articles SET Visits=Visits+1 WHERE ArticleID=@ArticleID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", ArticleID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT * FROM Articles WHERE ArticleID=@ArticleID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", ArticleID);
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
                                returnXML.ArticleID = SepFunctions.toLong(SepFunctions.openNull(RS["ArticleID"]));
                                returnXML.Headline = SepFunctions.openNull(RS["Headline"]);
                                returnXML.Author = SepFunctions.openNull(RS["Author"]);
                                returnXML.Headline_Date = SepFunctions.toDate(SepFunctions.openNull(RS["Headline_Date"]));
                                returnXML.Start_Date = SepFunctions.toDate(SepFunctions.openNull(RS["Start_Date"]));
                                returnXML.End_Date = SepFunctions.toDate(SepFunctions.openNull(RS["End_Date"]));
                                returnXML.Summary = SepFunctions.openNull(RS["Summary"]);
                                returnXML.Full_Article = SepFunctions.openNull(RS["Full_Article"]);
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.Source = SepFunctions.openNull(RS["Source"]);
                                returnXML.Article_URL = SepFunctions.openNull(RS["Article_URL"]);
                                returnXML.Meta_Description = SepFunctions.openNull(RS["Meta_Description"]);
                                returnXML.Meta_Keywords = SepFunctions.openNull(RS["Meta_Keywords"]);
                                returnXML.Related_Articles = SepFunctions.openNull(RS["Related_Articles"]);
                                returnXML.Visits = SepFunctions.toLong(SepFunctions.openNull(RS["Visits"]));
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Articles the save.
        /// </summary>
        /// <param name="ArticleID">The article identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Headline">The headline.</param>
        /// <param name="Author">The author.</param>
        /// <param name="Headline_Date">The headline date.</param>
        /// <param name="Start_Date">The start date.</param>
        /// <param name="End_Date">The end date.</param>
        /// <param name="Summary">The summary.</param>
        /// <param name="Full_Article">The full article.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Article_URL">The article URL.</param>
        /// <param name="Meta_Description">The meta description.</param>
        /// <param name="Meta_Keywords">The meta keywords.</param>
        /// <param name="Related_Articles">The related articles.</param>
        /// <param name="Status">The status.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Article_Save(long ArticleID, string UserID, long CatID, string Headline, string Author, DateTime Headline_Date, DateTime Start_Date, DateTime End_Date, string Summary, string Full_Article, string Source, string Article_URL, string Meta_Description, string Meta_Keywords, string Related_Articles, int Status, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ArticleID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Articles WHERE ArticleID=@ArticleID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ArticleID", ArticleID);
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
                    ArticleID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Articles SET Headline=@Headline, Author=@Author, Headline_Date=@Headline_Date, Start_Date=@Start_Date, End_Date=@End_Date, Summary=@Summary, Full_Article=@Full_Article, UserID=@UserID, CatID=@CatID, Source=@Source, Article_URL=@Article_URL, Meta_Description=@Meta_Description, Meta_Keywords=@Meta_Keywords, Related_Articles=@Related_Articles, Status=@Status, PortalID=@PortalID WHERE ArticleID=@ArticleID";
                }
                else
                {
                    SqlStr = "INSERT INTO Articles (ArticleID, Headline, Author, Headline_Date, Start_Date, End_Date, Summary, Full_Article, UserID, CatID, Source, Article_URL, Meta_Description, Meta_Keywords, Related_Articles, Status, PortalID, Visits) VALUES (@ArticleID, @Headline, @Author, @Headline_Date, @Start_Date, @End_Date, @Summary, @Full_Article, @UserID, @CatID, @Source, @Article_URL, @Meta_Description, @Meta_Keywords, @Related_Articles, @Status, @PortalID, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", ArticleID);
                    cmd.Parameters.AddWithValue("@Headline", Headline);
                    cmd.Parameters.AddWithValue("@Author", Author);
                    cmd.Parameters.AddWithValue("@Headline_Date", Headline_Date);
                    cmd.Parameters.AddWithValue("@Start_Date", Start_Date);
                    cmd.Parameters.AddWithValue("@End_Date", End_Date);
                    cmd.Parameters.AddWithValue("@Summary", Summary);
                    cmd.Parameters.AddWithValue("@Full_Article", Full_Article);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@Source", Source);
                    cmd.Parameters.AddWithValue("@Article_URL", Article_URL);
                    cmd.Parameters.AddWithValue("@Meta_Description", Meta_Description);
                    cmd.Parameters.AddWithValue("@Meta_Keywords", Meta_Keywords);
                    cmd.Parameters.AddWithValue("@Related_Articles", !string.IsNullOrWhiteSpace(Related_Articles) ? Related_Articles : string.Empty);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 35, Strings.ToString(ArticleID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Article", "Articles", "PostArticle");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM Articles WHERE ArticleID=@ArticleID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ArticleID", ArticleID);
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
                        SepFunctions.Update_Change_Log(35, Strings.ToString(ArticleID), Headline, Strings.ToString(writeLog));
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
        /// Gets the articles.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="excludeArticleId">The exclude article identifier.</param>
        /// <param name="showAvailable">if set to <c>true</c> [show available].</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.Articles&gt;.</returns>
        public static List<Models.Articles> GetArticles(string SortExpression = "Headline_Date", string SortDirection = "DESC", string searchWords = "", long CategoryId = -1, long excludeArticleId = 0, bool showAvailable = false, string UserID = "", string StartDate = "")
        {
            var lArticles = new List<Models.Articles>();

            var sImageFolder = SepFunctions.GetInstallFolder(true);
            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Headline_Date";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (showAvailable)
            {
                wClause += "Mod.Status = '1' AND Mod.Start_Date <= GETDATE() AND Mod.End_Date >= GETDATE()";
            }
            else
            {
                wClause = "Mod.Status <> -1";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (Headline LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Author LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (excludeArticleId > 0)
            {
                wClause += " AND Mod.ArticleID <> '" + SepFunctions.FixWord(Strings.ToString(excludeArticleId)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.Start_Date > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.ArticleID,Mod.CatID,Mod.UserID,Mod.Headline_Date,Mod.Headline,Mod.Summary,Mod.Author,Mod.Status,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='35' AND UniqueID=Mod.ArticleID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID FROM Articles AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dArticles = new Models.Articles { ArticleID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ArticleID"])) };
                    dArticles.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dArticles.Headline = SepFunctions.openNull(ds.Tables[0].Rows[i]["Headline"]);
                    dArticles.Author = SepFunctions.openNull(ds.Tables[0].Rows[i]["Author"]);
                    dArticles.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dArticles.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dArticles.Summary = SepFunctions.RemoveHTML(SepFunctions.openNull(ds.Tables[0].Rows[i]["Summary"]));
                    switch (SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"])))
                    {
                        case -1:
                            dArticles.StatusText = SepFunctions.LangText("Deleted");
                            break;

                        case 0:
                            dArticles.StatusText = SepFunctions.LangText("Pending");
                            break;

                        case 1:
                            dArticles.StatusText = SepFunctions.LangText("Published");
                            break;

                        case 2:
                            dArticles.StatusText = SepFunctions.LangText("Archived");
                            break;

                        default:
                            dArticles.StatusText = SepFunctions.LangText("N/A");
                            break;
                    }

                    dArticles.Headline_Date = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["Headline_Date"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dArticles.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=35&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        dArticles.DefaultPicture = sImageFolder + "images/public/no-image.jpg";
                    }

                    lArticles.Add(dArticles);
                }
            }

            return lArticles;
        }
    }
}