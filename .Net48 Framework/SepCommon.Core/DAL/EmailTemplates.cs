// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="EmailTemplates.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class EmailTemplates.
    /// </summary>
    public static class EmailTemplates
    {
        /// <summary>
        /// Gets the email templates.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.EmailTemplates&gt;.</returns>
        public static List<Models.EmailTemplates> GetEmailTemplates(string SortExpression = "TemplateName", string SortDirection = "ASC", string searchWords = "")
        {
            var lEmailTemplates = new List<Models.EmailTemplates>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "TemplateName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (TemplateName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR EmailSubject LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT TemplateID,TemplateName,EmailSubject,DatePosted FROM EmailTemplates WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dEmailTemplates = new Models.EmailTemplates { TemplateID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TemplateID"])) };
                    dEmailTemplates.TemplateName = SepFunctions.openNull(ds.Tables[0].Rows[i]["TemplateName"]);
                    dEmailTemplates.EmailSubject = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailSubject"]);
                    dEmailTemplates.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lEmailTemplates.Add(dEmailTemplates);
                }
            }

            return lEmailTemplates;
        }

        /// <summary>
        /// Templates the delete.
        /// </summary>
        /// <param name="TemplateIDs">The template i ds.</param>
        /// <returns>System.String.</returns>
        public static string Template_Delete(string TemplateIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrTemplateIDs = Strings.Split(TemplateIDs, ",");

                if (arrTemplateIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTemplateIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE EmailTemplates SET Status='-1', DateDeleted=@DateDeleted WHERE TemplateID=@TemplateID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TemplateID", arrTemplateIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
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

            sReturn = SepFunctions.LangText("Email Template(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Templates the get.
        /// </summary>
        /// <param name="TemplateID">The template identifier.</param>
        /// <returns>Models.EmailTemplates.</returns>
        public static Models.EmailTemplates Template_Get(long TemplateID)
        {
            var returnXML = new Models.EmailTemplates();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM EmailTemplates WHERE TemplateID=@TemplateID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TemplateID = SepFunctions.toLong(SepFunctions.openNull(RS["TemplateID"]));
                            returnXML.TemplateName = SepFunctions.openNull(RS["TemplateName"]);
                            returnXML.EmailSubject = SepFunctions.openNull(RS["EmailSubject"]);
                            returnXML.EmailBody = SepFunctions.openNull(RS["EmailBody"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Templates the save.
        /// </summary>
        /// <param name="TemplateID">The template identifier.</param>
        /// <param name="TemplateName">Name of the template.</param>
        /// <param name="EmailSubject">The email subject.</param>
        /// <param name="EmailBody">The email body.</param>
        /// <returns>System.String.</returns>
        public static string Template_Save(long TemplateID, string TemplateName, string EmailSubject, string EmailBody)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (TemplateID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT TemplateID FROM EmailTemplates WHERE TemplateID=@TemplateID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
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
                    TemplateID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE EmailTemplates SET TemplateName=@TemplateName, EmailSubject=@EmailSubject, EmailBody=@EmailBody WHERE TemplateID=@TemplateID";
                }
                else
                {
                    SqlStr = "INSERT INTO EmailTemplates (TemplateID, TemplateName, EmailSubject, EmailBody, PortalID, DatePosted, Status) VALUES (@TemplateID, @TemplateName, @EmailSubject, @EmailBody, @PortalID, @DatePosted, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    cmd.Parameters.AddWithValue("@TemplateName", TemplateName);
                    cmd.Parameters.AddWithValue("@EmailSubject", EmailSubject);
                    cmd.Parameters.AddWithValue("@EmailBody", EmailBody);
                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Email template has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Email template has been successfully added.");

            return sReturn;
        }
    }
}