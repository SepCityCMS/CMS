// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ContentRotator.cs" company="SepCity, Inc.">
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
    /// Class ContentRotator.
    /// </summary>
    public static class ContentRotator
    {
        /// <summary>
        /// Contents the delete.
        /// </summary>
        /// <param name="ContentIDs">The content i ds.</param>
        /// <returns>System.String.</returns>
        public static string Content_Delete(string ContentIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrContentIDs = Strings.Split(ContentIDs, ",");

                if (arrContentIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrContentIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ContentRotator SET Status='-1', DateDeleted=@DateDeleted WHERE ContentID=@ContentID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ContentID", arrContentIDs[i]);
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

            sReturn = SepFunctions.LangText("Content has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Contents the get.
        /// </summary>
        /// <param name="ContentID">The content identifier.</param>
        /// <returns>Models.ContentRotator.</returns>
        public static Models.ContentRotator Content_Get(long ContentID)
        {
            var returnXML = new Models.ContentRotator();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM ContentRotator WHERE ContentID=@ContentID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ContentID", ContentID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ContentID = SepFunctions.toLong(SepFunctions.openNull(RS["ContentID"]));
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.ZoneID = SepFunctions.toLong(SepFunctions.openNull(RS["ZoneID"]));
                            returnXML.CatIDs = SepFunctions.openNull(RS["CatIDs"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.PageIDs = SepFunctions.openNull(RS["PageIDs"]);
                            returnXML.HTMLContent = SepFunctions.openNull(RS["HTMLContent"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Contents the save.
        /// </summary>
        /// <param name="ContentID">The content identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Description">The description.</param>
        /// <param name="ZoneID">The zone identifier.</param>
        /// <param name="HTMLCode">The HTML code.</param>
        /// <param name="CatIDs">The cat i ds.</param>
        /// <param name="PageIDs">The page i ds.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <returns>System.Int32.</returns>
        public static int Content_Save(long ContentID, string UserID, string Description, long ZoneID, string HTMLCode, string CatIDs, string PageIDs, string PortalIDs)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ContentID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ContentID FROM ContentRotator WHERE ContentID=@ContentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ContentID", ContentID);
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
                    ContentID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE ContentRotator SET Description=@Description, ZoneID=@ZoneID, HTMLContent=@HTMLContent, CatIDs=@CatIDs, PageIDs=@PageIDs, PortalIDs=@PortalIDs WHERE ContentID=@ContentID";
                }
                else
                {
                    SqlStr = "INSERT INTO ContentRotator (ContentID, Description, ZoneID, HTMLContent, CatIDs, PageIDs, PortalIDs, Status, DatePosted) VALUES (@ContentID, @Description, @ZoneID, @HTMLContent, @CatIDs, @PageIDs, @PortalIDs, '0', @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ContentID", ContentID);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                    cmd.Parameters.AddWithValue("@HTMLContent", HTMLCode);
                    cmd.Parameters.AddWithValue("@CatIDs", !string.IsNullOrWhiteSpace(CatIDs) ? CatIDs : string.Empty);
                    cmd.Parameters.AddWithValue("@PageIDs", !string.IsNullOrWhiteSpace(PageIDs) ? PageIDs : string.Empty);
                    cmd.Parameters.AddWithValue("@PortalIDs", !string.IsNullOrWhiteSpace(PortalIDs) ? PortalIDs : string.Empty);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 1, Strings.ToString(ContentID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Content", "Content", string.Empty);

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
        /// Gets the content rotator.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.ContentRotator&gt;.</returns>
        public static List<Models.ContentRotator> GetContentRotator(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "")
        {
            var lContentRotator = new List<Models.ContentRotator>();

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
                wClause = " AND HTMLContent LIKE '%" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ContentID,Description,DatePosted FROM ContentRotator WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dContentRotator = new Models.ContentRotator { ContentID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ContentID"])) };
                    dContentRotator.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dContentRotator.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lContentRotator.Add(dContentRotator);
                }
            }

            return lContentRotator;
        }
    }
}