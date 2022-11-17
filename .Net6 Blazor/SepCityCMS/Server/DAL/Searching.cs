// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Searching.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Searching.
    /// </summary>
    public static class Searching
    {
        /// <summary>
        /// Gets the meta tags.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.MetaTags&gt;.</returns>
        public static List<MetaTags> GetMetaTags(string SortExpression = "PageTitle", string SortDirection = "ASC", string searchWords = "")
        {
            var lMetaTags = new List<MetaTags>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "PageTitle";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " WHERE (PageTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT TagID,PageURL,PageTitle,Description FROM SEOTitles" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dMetaTags = new Models.MetaTags { TagID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TagID"])) };
                    dMetaTags.PageURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["PageURL"]);
                    dMetaTags.PageTitle = SepFunctions.openNull(ds.Tables[0].Rows[i]["PageTitle"]);
                    dMetaTags.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    lMetaTags.Add(dMetaTags);
                }
            }

            return lMetaTags;
        }

        /// <summary>
        /// Metas the tag delete.
        /// </summary>
        /// <param name="TagIDs">The tag i ds.</param>
        /// <returns>System.String.</returns>
        public static string MetaTag_Delete(string TagIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrTagIDs = Strings.Split(TagIDs, ",");

                if (arrTagIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTagIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM SEOTitles WHERE TagID=@TagID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TagID", arrTagIDs[i]);
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

            sReturn = SepFunctions.LangText("Meta Tag(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Metas the tag get.
        /// </summary>
        /// <param name="TagID">The tag identifier.</param>
        /// <returns>Models.MetaTags.</returns>
        public static MetaTags MetaTag_Get(long TagID)
        {
            var returnXML = new Models.MetaTags();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM SEOTitles WHERE TagID=@TagID", conn))
                {
                    cmd.Parameters.AddWithValue("@TagID", TagID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TagID = TagID;
                            returnXML.PageURL = SepFunctions.openNull(RS["PageURL"]);
                            returnXML.PageTitle = SepFunctions.openNull(RS["PageTitle"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Metas the tag save.
        /// </summary>
        /// <param name="TagID">The tag identifier.</param>
        /// <param name="PageURL">The page URL.</param>
        /// <param name="PageTitle">The page title.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.String.</returns>
        public static string MetaTag_Save(long TagID, string PageURL, string PageTitle, string Description)
        {
            var bUpdate = false;
            var SqlStr = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (TagID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT TagID FROM SEOTitles WHERE TagID=@TagID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TagID", TagID);
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
                    TagID = SepFunctions.GetIdentity();
                }

                if (bUpdate)
                {
                    SqlStr = "UPDATE SEOTitles SET PageURL=@PageURL, PageTitle=@PageTitle, Description=@Description WHERE TagID=@TagID";
                }
                else
                {
                    SqlStr = "INSERT INTO SEOTitles (TagID, PageURL, PageTitle, Description) VALUES (@TagID, @PageURL, @PageTitle, @Description)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@TagID", TagID);
                    cmd.Parameters.AddWithValue("@PageURL", PageURL);
                    cmd.Parameters.AddWithValue("@PageTitle", PageTitle);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Meta tag has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Meta tag has been successfully added.");

            return sReturn;
        }
    }
}