// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CustomFields.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class CustomFields.
    /// </summary>
    public static class CustomFields
    {
        /// <summary>
        /// Answers the get.
        /// </summary>
        /// <param name="FieldID">The field identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.CustomFieldsAnswers.</returns>
        public static CustomFieldsAnswers Answer_Get(long FieldID, string UserID)
        {
            var returnXML = new Models.CustomFieldsAnswers();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM CustomFieldUsers WHERE FieldID=@FieldID AND Status <> -1 AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@FieldID", FieldID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.UserFieldID = SepFunctions.toLong(SepFunctions.openNull(RS["UserFieldID"]));
                            returnXML.UniqueID = SepFunctions.toLong(SepFunctions.openNull(RS["UniqueID"]));
                            returnXML.FieldID = SepFunctions.toLong(SepFunctions.openNull(RS["FieldID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.ModuleID = SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]));
                            returnXML.FieldValue = SepFunctions.openNull(RS["FieldValue"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Answerses the save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="UniqueID">The unique identifier.</param>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>System.String.</returns>
        public static string Answers_Save(string UserID, int ModuleID, long UniqueID, long ProductID, string customData)
        {
            var intPortalID = SepFunctions.Get_Portal_ID();

            var updateRecord = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var SqlStr = string.Empty;

                var sValue = string.Empty;

                decimal dUnitPrice = 0;
                decimal dRecurringPrice = 0;

                if (ModuleID == 41 && ProductID > 0)
                {
                    SqlStr = "SELECT FieldID,AnswerType FROM CustomFields WHERE UniqueIDs LIKE '%|" + SepFunctions.FixWord(Strings.ToString(ProductID)) + "|%' AND (PortalIDs LIKE '%|" + intPortalID + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0)";
                }
                else
                {
                    SqlStr = "SELECT FieldID,AnswerType FROM CustomFields WHERE ModuleIDs LIKE '%|" + ModuleID + "|%' AND (PortalIDs LIKE '%|" + intPortalID + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            dUnitPrice = 0;
                            dRecurringPrice = 0;
                            if (SepFunctions.openNull(RS["AnswerType"]) == "Image")
                            {
                                sValue = SepFunctions.ParseXML("FileName", customData);
                            }
                            else
                            {
                                sValue = SepFunctions.ParseXML("Custom" + SepFunctions.openNull(RS["FieldID"]), customData);
                            }

                            if (!string.IsNullOrWhiteSpace(sValue))
                            {
                                if (ModuleID == 41 && ProductID > 0)
                                {
                                    if (SepFunctions.openNull(RS["AnswerType"]) == "DropdownM" || SepFunctions.openNull(RS["AnswerType"]) == "DropdownS" || SepFunctions.openNull(RS["AnswerType"]) == "Radio" || SepFunctions.openNull(RS["AnswerType"]) == "Checkbox")
                                    {
                                        using (var cmd2 = new SqlCommand("SELECT Price,RecurringPrice FROM CustomFieldOptions WHERE OptionValue='" + SepFunctions.FixWord(sValue) + "'", conn))
                                        {
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (RS2.HasRows)
                                                {
                                                    RS2.Read();
                                                    if (SepFunctions.toDecimal(SepFunctions.openNull(RS2["Price"])) > 0)
                                                    {
                                                        dUnitPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS2["Price"]));
                                                    }

                                                    if (SepFunctions.toDecimal(SepFunctions.openNull(RS2["RecurringPrice"])) > 0)
                                                    {
                                                        dRecurringPrice = SepFunctions.toDecimal(SepFunctions.openNull(RS2["RecurringPrice"]));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (dUnitPrice > 0)
                                {
                                    sValue += "||" + dUnitPrice;
                                }

                                if (dRecurringPrice > 0)
                                {
                                    sValue += "||" + dRecurringPrice;
                                }

                                using (var cmd2 = new SqlCommand("SELECT UniqueID FROM CustomFieldUsers WHERE UniqueID='" + SepFunctions.FixWord(Strings.ToString(UniqueID)) + "' AND ModuleID='" + ModuleID + "' AND FieldID='" + SepFunctions.openNull(RS["FieldID"], true) + "' AND PortalID='" + intPortalID + "' AND UserID='" + SepFunctions.FixWord(UserID) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            updateRecord = true;
                                        }
                                        else
                                        {
                                            updateRecord = false;
                                        }
                                    }
                                }

                                if (updateRecord == false)
                                {
                                    using (var cmd2 = new SqlCommand("INSERT INTO CustomFieldUsers (UserFieldID, UniqueID, FieldID, UserID, ModuleID, FieldValue, PortalID, Status) VALUES('" + SepFunctions.GetIdentity() + "','" + UniqueID + "','" + SepFunctions.openNull(RS["FieldID"], true) + "','" + SepFunctions.FixWord(UserID) + "','" + ModuleID + "','" + SepFunctions.FixWord(SepFunctions.RemoveHTML(sValue)) + "','" + intPortalID + "', '1')", conn))
                                    {
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    using (var cmd2 = new SqlCommand("UPDATE CustomFieldUsers SET FieldValue='" + SepFunctions.FixWord(SepFunctions.RemoveHTML(sValue)) + "' WHERE UserID='" + SepFunctions.FixWord(UserID) + "' AND UniqueID='" + SepFunctions.FixWord(Strings.ToString(UniqueID)) + "' AND ModuleID='" + ModuleID + "' AND FieldID='" + SepFunctions.openNull(RS["FieldID"], true) + "' AND PortalID='" + intPortalID + "'", conn))
                                    {
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string sReturn;

            sReturn = SepFunctions.LangText("Custom field answer have been saved successfully.");

            return sReturn;
        }

        /// <summary>
        /// Fields the delete.
        /// </summary>
        /// <param name="FieldIDs">The field i ds.</param>
        /// <returns>System.String.</returns>
        public static string Field_Delete(string FieldIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFieldIDs = Strings.Split(FieldIDs, ",");

                if (arrFieldIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFieldIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE CustomFields SET Status='-1', DateDeleted=@DateDeleted WHERE FieldID=@FieldID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FieldID", arrFieldIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE CustomFieldUsers SET Status='-1', DateDeleted=@DateDeleted WHERE FieldID=@FieldID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FieldID", arrFieldIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Field(s) has been successfully deleted.");
        }

        /// <summary>
        /// Fields the get.
        /// </summary>
        /// <param name="FieldID">The field identifier.</param>
        /// <returns>Models.CustomFields.</returns>
        public static Models.CustomFields Field_Get(long FieldID)
        {
            var returnXML = new Models.CustomFields();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM CustomFields WHERE FieldID=@FieldID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@FieldID", FieldID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.FieldID = SepFunctions.toLong(SepFunctions.openNull(RS["FieldID"]));
                            returnXML.SectionID = SepFunctions.toLong(SepFunctions.openNull(RS["SectionID"]));
                            returnXML.FieldName = SepFunctions.openNull(RS["FieldName"]);
                            returnXML.AnswerType = SepFunctions.openNull(RS["AnswerType"]);
                            returnXML.FieldType = SepFunctions.openNull(RS["FieldType"]);
                            returnXML.ListUnder = SepFunctions.openNull(RS["ListUnder"]);
                            returnXML.Required = SepFunctions.openBoolean(RS["Required"]);
                            returnXML.ModuleIDs = SepFunctions.openNull(RS["ModuleIDs"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.UniqueIDs = SepFunctions.openNull(RS["UniqueIDs"]);
                            returnXML.Searchable = SepFunctions.toBoolean(SepFunctions.openNull(RS["Searchable"]));
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Fields the option save.
        /// </summary>
        /// <param name="OptionID">The option identifier.</param>
        /// <param name="FieldID">The field identifier.</param>
        /// <param name="OptionName">Name of the option.</param>
        /// <param name="OptionValue">The option value.</param>
        /// <param name="SetupPrice">The setup price.</param>
        /// <param name="RecurringPrice">The recurring price.</param>
        /// <param name="Weight">The weight.</param>
        /// <returns>System.String.</returns>
        public static string Field_Option_Save(long OptionID, long FieldID, string OptionName, string OptionValue, decimal SetupPrice, decimal RecurringPrice, long Weight)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (OptionID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT FieldID FROM CustomFieldOptions WHERE OptionID=@OptionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@OptionID", OptionID);
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
                    OptionID = SepFunctions.GetIdentity();
                }

                if (Weight == 0)
                {
                    using (var cmd = new SqlCommand("SELECT Count(FieldID) AS TotalRows FROM CustomFieldOptions WHERE FieldID=@FieldID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FieldID", FieldID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                Weight = SepFunctions.toLong(SepFunctions.openNull(RS["TotalRows"])) + 1;
                            }
                        }
                    }
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE CustomFieldOptions SET OptionName=@OptionName, OptionValue=@OptionValue, Price=@Price, RecurringPrice=@RecurringPrice, Weight=@Weight WHERE OptionID=@OptionID AND FieldID=@FieldID";
                }
                else
                {
                    SqlStr = "INSERT INTO CustomFieldOptions (OptionID, FieldID, OptionName, OptionValue, Price, RecurringPrice, Weight) VALUES (@OptionID, @FieldID, @OptionName, @OptionValue, @Price, @RecurringPrice, @Weight)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@OptionID", OptionID);
                    cmd.Parameters.AddWithValue("@FieldID", FieldID);
                    cmd.Parameters.AddWithValue("@OptionName", OptionName);
                    cmd.Parameters.AddWithValue("@OptionValue", OptionValue);
                    cmd.Parameters.AddWithValue("@Price", SetupPrice);
                    cmd.Parameters.AddWithValue("@RecurringPrice", RecurringPrice);
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Custom Field Option has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Custom Field Option has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Fields the save.
        /// </summary>
        /// <param name="FieldID">The field identifier.</param>
        /// <param name="SectionID">The section identifier.</param>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="AnswerType">Type of the answer.</param>
        /// <param name="Required">if set to <c>true</c> [required].</param>
        /// <param name="Searchable">if set to <c>true</c> [searchable].</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="ModuleIDs">The module i ds.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="UniqueIDs">The unique i ds.</param>
        /// <returns>System.String.</returns>
        public static string Field_Save(long FieldID, long SectionID, string FieldName, string AnswerType, bool Required, bool Searchable, long Weight, string ModuleIDs, string PortalIDs, string UniqueIDs)
        {
            var bUpdate = false;
            var wc = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (FieldID > 0)
                {
                    if (!string.IsNullOrWhiteSpace(UniqueIDs))
                    {
                        wc = " AND UniqueIDs LIKE '%" + SepFunctions.FixWord(UniqueIDs) + "%'";
                    }

                    using (var cmd = new SqlCommand("SELECT FieldID FROM CustomFields WHERE FieldID=@FieldID" + wc, conn))
                    {
                        cmd.Parameters.AddWithValue("@FieldID", FieldID);
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
                    FieldID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE CustomFields SET SectionID=@SectionID, FieldName=@FieldName, AnswerType=@AnswerType, Required=@Required, Searchable=@Searchable, ModuleIDs=@ModuleIDs, PortalIDs=@PortalIDs, Weight=@Weight, UniqueIDs=@UniqueIDs WHERE FieldID=@FieldID";
                }
                else
                {
                    SqlStr = "INSERT INTO CustomFields (FieldID, SectionID, FieldName, AnswerType, Required, Searchable, ModuleIDs, PortalIDs, Weight, UniqueIDs, Status) VALUES (@FieldID, @SectionID, @FieldName, @AnswerType, @Required, @Searchable, @ModuleIDs, @PortalIDs, @Weight, @UniqueIDs, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@FieldID", FieldID);
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    cmd.Parameters.AddWithValue("@FieldName", FieldName);
                    cmd.Parameters.AddWithValue("@AnswerType", AnswerType);
                    cmd.Parameters.AddWithValue("@Required", Required ? "1" : "0");
                    cmd.Parameters.AddWithValue("@Searchable", Searchable ? "1" : "0");
                    cmd.Parameters.AddWithValue("@ModuleIDs", ModuleIDs);
                    cmd.Parameters.AddWithValue("@PortalIDs", !string.IsNullOrWhiteSpace(PortalIDs) ? PortalIDs : "|-1|");
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.Parameters.AddWithValue("@UniqueIDs", UniqueIDs);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Custom Field has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Custom Field has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Gets the custom field options.
        /// </summary>
        /// <param name="FieldID">The field identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.CustomFieldsOptions&gt;.</returns>
        public static List<CustomFieldsOptions> GetCustomFieldOptions(long FieldID, string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "")
        {
            var lCustomFieldsOptions = new List<CustomFieldsOptions>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (FieldName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM CustomFieldOptions WHERE FieldID='" + SepFunctions.FixWord(Strings.ToString(FieldID)) + "'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dCustomFieldsOptions = new Models.CustomFieldsOptions { OptionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["OptionID"])) };
                    dCustomFieldsOptions.FieldID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FieldID"]));
                    dCustomFieldsOptions.OptionName = SepFunctions.openNull(ds.Tables[0].Rows[i]["OptionName"]);
                    dCustomFieldsOptions.OptionValue = SepFunctions.openNull(ds.Tables[0].Rows[i]["OptionValue"]);
                    dCustomFieldsOptions.SetupPrice = SepFunctions.openNull(ds.Tables[0].Rows[i]["Price"]);
                    dCustomFieldsOptions.RecurringPrice = SepFunctions.openNull(ds.Tables[0].Rows[i]["RecurringPrice"]);
                    dCustomFieldsOptions.Weight = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                    lCustomFieldsOptions.Add(dCustomFieldsOptions);
                }
            }

            return lCustomFieldsOptions;
        }

        /// <summary>
        /// Gets the custom fields.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="uniqueIds">The unique ids.</param>
        /// <returns>List&lt;Models.CustomFields&gt;.</returns>
        public static List<Models.CustomFields> GetCustomFields(string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "", int ModuleID = 0, string uniqueIds = "")
        {
            var lCustomFields = new List<Models.CustomFields>();

            var wClause = string.Empty;
            long iOffset = 0;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (FieldName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (ModuleID > 0)
            {
                wClause = " AND (ModuleIDs LIKE '%|" + SepFunctions.FixWord(Strings.ToString(ModuleID)) + "|%')";
            }

            if (!string.IsNullOrWhiteSpace(uniqueIds))
            {
                wClause = " AND (UniqueIds LIKE '%" + SepFunctions.FixWord(uniqueIds) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT FieldID,FieldName,AnswerType,Required,Weight FROM CustomFields WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dCustomFields = new Models.CustomFields { FieldID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FieldID"])) };
                    iOffset += 1;
                    dCustomFields.Offset = iOffset;
                    dCustomFields.FieldName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FieldName"]);
                    dCustomFields.AnswerType = SepFunctions.openNull(ds.Tables[0].Rows[i]["AnswerType"]);
                    dCustomFields.Required = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["Required"]));
                    dCustomFields.Weight = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                    lCustomFields.Add(dCustomFields);
                }
            }

            return lCustomFields;
        }

        /// <summary>
        /// Gets the custom field sections.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.CustomFieldSections&gt;.</returns>
        public static List<CustomFieldSections> GetCustomFieldSections(string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "")
        {
            var lCustomFieldSections = new List<CustomFieldSections>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (SectionName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT SectionID,SectionName FROM CustomSections WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dCustomFieldSections = new Models.CustomFieldSections { SectionID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SectionID"])) };
                    dCustomFieldSections.SectionName = SepFunctions.openNull(ds.Tables[0].Rows[i]["SectionName"]);
                    lCustomFieldSections.Add(dCustomFieldSections);
                }
            }

            return lCustomFieldSections;
        }

        /// <summary>
        /// Sections the delete.
        /// </summary>
        /// <param name="SectionIDs">The section i ds.</param>
        /// <returns>System.String.</returns>
        public static string Section_Delete(string SectionIDs)
        {
            var arrSectionIDs = Strings.Split(SectionIDs, ",");

            if (arrSectionIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrSectionIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE CustomSections SET Status='-1', DateDeleted=@DateDeleted WHERE SectionID=@SectionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SectionID", arrSectionIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("SELECT FieldID FROM CustomFields WHERE SectionID=@SectionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SectionID", arrSectionIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    using (var cmd2 = new SqlCommand("UPDATE CustomFieldUsers SET Status='-1', DateDeleted=@DateDeleted WHERE FieldID=@FieldID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@FieldID", SepFunctions.openNull(RS["FieldID"]));
                                        cmd2.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        using (var cmd = new SqlCommand("UPDATE CustomFields SET Status='-1', DateDeleted=@DateDeleted WHERE SectionID=@SectionID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SectionID", arrSectionIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Section(s) has been successfully deleted.");
        }

        /// <summary>
        /// Sections the get.
        /// </summary>
        /// <param name="SectionID">The section identifier.</param>
        /// <returns>Models.CustomFieldSections.</returns>
        public static CustomFieldSections Section_Get(long SectionID)
        {
            var returnXML = new Models.CustomFieldSections();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM CustomSections WHERE SectionID=@SectionID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.SectionID = SepFunctions.toLong(SepFunctions.openNull(RS["SectionID"]));
                            returnXML.SectionName = SepFunctions.openNull(RS["SectionName"]);
                            returnXML.SectionText = SepFunctions.openNull(RS["SectionText"]);
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Sections the save.
        /// </summary>
        /// <param name="SectionID">The section identifier.</param>
        /// <param name="SectionName">Name of the section.</param>
        /// <param name="SectionText">The section text.</param>
        /// <param name="Weight">The weight.</param>
        /// <returns>System.String.</returns>
        public static string Section_Save(long SectionID, string SectionName, string SectionText, long Weight)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SectionID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT SectionID FROM CustomSections WHERE SectionID=@SectionID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SectionID", SectionID);
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
                    SectionID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE CustomSections SET SectionName=@SectionName, SectionText=@SectionText WHERE SectionID=@SectionID";
                }
                else
                {
                    SqlStr = "INSERT INTO CustomSections (SectionID, SectionName, SectionText, Weight, Status) VALUES (@SectionID, @SectionName, @SectionText, @Weight, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    cmd.Parameters.AddWithValue("@SectionName", SectionName);
                    cmd.Parameters.AddWithValue("@SectionText", SectionText);
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Custom Field Section has been successfully added.");
            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Custom Field Section has been successfully updated.");
            }

            return sReturn;
        }
    }
}