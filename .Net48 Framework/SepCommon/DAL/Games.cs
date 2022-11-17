// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Games.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Games.
    /// </summary>
    public static class Games
    {
        /// <summary>
        /// Games the get.
        /// </summary>
        /// <param name="GameID">The game identifier.</param>
        /// <returns>Models.Games.</returns>
        public static Models.Games Game_Get(long GameID)
        {
            var returnXML = new Models.Games();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Games WHERE GameID=@GameID", conn))
                {
                    cmd.Parameters.AddWithValue("@GameID", GameID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.GameID = SepFunctions.toLong(SepFunctions.openNull(RS["GameID"]));
                            returnXML.GameName = SepFunctions.openNull(RS["GameName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.ImageFile = SepFunctions.openNull(RS["ImageFile"]);
                            returnXML.WindowHeight = SepFunctions.openNull(RS["WindowHeight"]);
                            returnXML.WindowWidth = SepFunctions.openNull(RS["WindowWidth"]);
                            returnXML.PageName = SepFunctions.openNull(RS["PageName"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Games the save.
        /// </summary>
        /// <param name="GameID">The game identifier.</param>
        /// <param name="GameName">Name of the game.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.String.</returns>
        public static string Game_Save(long GameID, string GameName, string Description)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("UPDATE Games SET GameName=@GameName, Description=@Description WHERE GameID=@GameID", conn))
                {
                    cmd.Parameters.AddWithValue("@GameID", GameID);
                    cmd.Parameters.AddWithValue("@GameName", GameName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn;

            sReturn = SepFunctions.LangText("Game has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Games&gt;.</returns>
        public static List<Models.Games> GetGames(string SortExpression = "GameName", string SortDirection = "ASC", string searchWords = "")
        {
            var lGames = new List<Models.Games>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "GameName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " WHERE (GameName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT GameID,GameName,Description,ImageFile FROM Games" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dGames = new Models.Games { GameID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["GameID"])) };
                    dGames.GameName = SepFunctions.openNull(ds.Tables[0].Rows[i]["GameName"]);
                    dGames.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dGames.ImageFile = SepFunctions.openNull(ds.Tables[0].Rows[i]["ImageFile"]);
                    lGames.Add(dGames);
                }
            }

            return lGames;
        }
    }
}