// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="FAQs.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class FAQs.
    /// </summary>
    public static class FAQs
    {
        /// <summary>
        /// FAQs the delete.
        /// </summary>
        /// <param name="FAQIDs">The faqi ds.</param>
        /// <returns>System.String.</returns>
        public static string FAQ_Delete(string FAQIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrFAQIDs = Strings.Split(FAQIDs, ",");

                if (arrFAQIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFAQIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE FAQ SET Status='-1', DateDeleted=@DateDeleted WHERE FAQID=@FAQID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FAQID", arrFAQIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(9, arrFAQIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("FAQ(s) has been successfully deleted.");
        }

        /// <summary>
        /// FAQs the get.
        /// </summary>
        /// <param name="FAQID">The faqid.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.FAQs.</returns>
        public static Models.FAQs FAQ_Get(long FAQID, long ChangeID = 0)
        {
            var returnXML = new Models.FAQs();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM FAQ WHERE FAQID=@FAQID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@FAQID", FAQID);
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
                                returnXML.FAQID = SepFunctions.toLong(SepFunctions.openNull(RS["FAQID"]));
                                returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                returnXML.Question = SepFunctions.openNull(RS["Question"]);
                                returnXML.Answer = SepFunctions.openNull(RS["Answer"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
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
        /// FAQs the save.
        /// </summary>
        /// <param name="FAQID">The faqid.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Question">The question.</param>
        /// <param name="Answer">The answer.</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int FAQ_Save(long FAQID, string UserID, long CatID, string Question, string Answer, long Weight, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (FAQID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM FAQ WHERE FAQID=@FAQID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FAQID", FAQID);
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
                    FAQID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE FAQ SET CatID=@CatID, Question=@Question, Answer=@Answer, Weight=@Weight, PortalID=@PortalID WHERE FAQID=@FAQID";
                }
                else
                {
                    SqlStr = "INSERT INTO FAQ (FAQID, CatID, Question, Answer, Weight, PortalID, DatePosted, Status) VALUES (@FAQID, @CatID, @Question, @Answer, @Weight, @PortalID, @DatePosted, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@FAQID", FAQID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@Question", Question);
                    cmd.Parameters.AddWithValue("@Answer", Answer);
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 9, Strings.ToString(FAQID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "FAQ", "FAQ", string.Empty);

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM FAQ WHERE FAQID=@FAQID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FAQID", FAQID);
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
                        SepFunctions.Update_Change_Log(9, Strings.ToString(FAQID), Question, Strings.ToString(writeLog));
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
        /// Gets the fa qs.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="CategoryId">The category identifier.</param>
        /// <returns>List&lt;Models.FAQs&gt;.</returns>
        public static List<Models.FAQs> GetFAQs(string SortExpression = "MOD.Weight", string SortDirection = "ASC", string searchWords = "", long CategoryId = -1)
        {
            var lFAQs = new List<Models.FAQs>();
            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "MOD.Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            string wClause = "Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND Question LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.FAQID,Mod.CatID,Mod.Question,Mod.Answer,Mod.DatePosted FROM FAQ AS Mod" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dFAQs = new Models.FAQs { FAQID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FAQID"])) };
                    dFAQs.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dFAQs.Question = SepFunctions.openNull(ds.Tables[0].Rows[i]["Question"]);
                    dFAQs.Answer = SepFunctions.openNull(ds.Tables[0].Rows[i]["Answer"]);
                    dFAQs.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lFAQs.Add(dFAQs);
                }
            }

            return lFAQs;
        }
    }
}