// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Favorites.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using SepCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Favorites.
    /// </summary>
    public static class Favorites
    {
        /// <summary>
        /// Favorites the delete.
        /// </summary>
        /// <param name="FavoriteIDs">The favorite i ds.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.String.</returns>
        public static string Favorite_Delete(string FavoriteIDs, string UserID)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFavoriteIDs = Strings.Split(FavoriteIDs, ",");

                if (arrFavoriteIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFavoriteIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM Favorites WHERE ID=@FavoriteID AND UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FavoriteID", arrFavoriteIDs[i]);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
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

            sReturn = SepFunctions.LangText("Items(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Gets the favorites.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="wishListItems">if set to <c>true</c> [wish list items].</param>
        /// <returns>List&lt;Models.Favorites&gt;.</returns>
        public static List<Models.Favorites> GetFavorites(string SortExpression = "PageTitle", string SortDirection = "ASC", string searchWords = "", string UserID = "", bool wishListItems = false)
        {
            var lFavorites = new List<Models.Favorites>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "PageTitle";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (wishListItems)
            {
                wClause = " Left(PageURL, 9) = 'WISHLIST:'";
            }
            else
            {
                wClause = " Left(PageURL, 9) <> 'WISHLIST:'";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (PageTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND (UserID='" + SepFunctions.FixWord(UserID) + "')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ID,PageURL,PageTitle,DatePosted,LastAccessed FROM Favorites WHERE" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }

                    for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (ds.Tables[0].Rows.Count == i)
                        {
                            break;
                        }

                        var dFavorites = new Models.Favorites { FavoriteID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                        if (wishListItems)
                        {
                            using (var cmd = new SqlCommand("SELECT ProductID,ProductName FROM ShopProducts WHERE ProductID=@ProductID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Strings.Replace(SepFunctions.openNull(ds.Tables[0].Rows[i]["PageURL"]), "WISHLIST:", string.Empty));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        dFavorites.PageURL = "/shopping/" + SepFunctions.openNull(RS["ProductID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["ProductName"])) + "/";
                                        dFavorites.PageTitle = SepFunctions.openNull(RS["ProductName"]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            dFavorites.PageURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["PageURL"]);
                            dFavorites.PageTitle = SepFunctions.openNull(ds.Tables[0].Rows[i]["PageTitle"]);
                        }

                        dFavorites.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                        dFavorites.LastAccessed = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["LastAccessed"]));
                        lFavorites.Add(dFavorites);
                    }
                }
            }

            return lFavorites;
        }
    }
}