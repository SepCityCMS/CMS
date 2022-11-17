// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Reviews.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.SepCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Reviews.
    /// </summary>
    public static class Reviews
    {
        /// <summary>
        /// Gets the reviews.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns>List&lt;Models.Reviews&gt;.</returns>
        public static List<Models.Reviews> GetReviews(string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "", int ModuleID = 0)
        {
            var lReviews = new List<Models.Reviews>();

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
                wClause += " Question LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (ModuleID > 0)
            {
                if (!string.IsNullOrWhiteSpace(wClause))
                {
                    wClause += " AND";
                }

                wClause += " ModuleIDs LIKE '%|" + SepFunctions.FixWord(Strings.ToString(ModuleID)) + "|%'";
            }

            if (!string.IsNullOrWhiteSpace(wClause))
            {
                wClause = " WHERE" + wClause;
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ReviewID,ModuleIDs,Question,Weight FROM Reviews" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dReviews = new Models.Reviews { ReviewID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ReviewID"])) };
                    dReviews.ModuleIDs = SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleIDs"]);
                    dReviews.Weight = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                    dReviews.Question = SepFunctions.openNull(ds.Tables[0].Rows[i]["Question"]);
                    lReviews.Add(dReviews);
                }
            }

            return lReviews;
        }

        /// <summary>
        /// Reviews the delete.
        /// </summary>
        /// <param name="ReviewIDs">The review i ds.</param>
        /// <returns>System.String.</returns>
        public static string Review_Delete(string ReviewIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrReviewIDs = Strings.Split(ReviewIDs, ",");

                if (arrReviewIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrReviewIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM Reviews WHERE ReviewID=@ReviewID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ReviewID", arrReviewIDs[i]);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM ReviewsUsers WHERE ReviewID=@ReviewID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ReviewID", arrReviewIDs[i]);
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

            sReturn = SepFunctions.LangText("Review(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Reviews the get.
        /// </summary>
        /// <param name="ReviewID">The review identifier.</param>
        /// <returns>Models.Reviews.</returns>
        public static Models.Reviews Review_Get(long ReviewID)
        {
            var returnXML = new Models.Reviews();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Reviews WHERE ReviewID=@ReviewID", conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", ReviewID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ReviewID = SepFunctions.toLong(SepFunctions.openNull(RS["ReviewID"]));
                            returnXML.ModuleIDs = SepFunctions.openNull(RS["ModuleIDs"]);
                            returnXML.Question = SepFunctions.openNull(RS["Question"]);
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Reviews the save.
        /// </summary>
        /// <param name="ReviewID">The review identifier.</param>
        /// <param name="Question">The question.</param>
        /// <param name="ModuleIDs">The module i ds.</param>
        /// <returns>System.String.</returns>
        public static string Review_Save(long ReviewID, string Question, string ModuleIDs)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ReviewID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ReviewID FROM Reviews WHERE ReviewID=@ReviewID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ReviewID", ReviewID);
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
                    ReviewID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Reviews SET Question=@Question, ModuleIDs=@ModuleIDs WHERE ReviewID=@ReviewID";
                }
                else
                {
                    SqlStr = "INSERT INTO Reviews (ReviewID, Question, ModuleIDs, Weight) VALUES (@ReviewID, @Question, @ModuleIDs, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", ReviewID);
                    cmd.Parameters.AddWithValue("@Question", Question);
                    cmd.Parameters.AddWithValue("@ModuleIDs", ModuleIDs);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Review has been successfully saved.");
            }

            sReturn = SepFunctions.LangText("Review has been successfully added.");

            return sReturn;
        }
    }
}