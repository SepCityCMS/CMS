// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="WhosOnline.cs" company="SepCity, Inc.">
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
    /// Class WhosOnline.
    /// </summary>
    public static class WhosOnline
    {
        /// <summary>
        /// Gets the whos online.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns>List&lt;Models.WhosOnline&gt;.</returns>
        public static List<Models.WhosOnline> GetWhosOnline(string SortExpression = "UserName", string SortDirection = "ASC")
        {
            var lWhosOnline = new List<Models.WhosOnline>();

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "UserName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT DISTINCT O.UserID,M.Username,O.Location FROM OnlineUsers AS O,Members AS M WHERE M.UserID=O.UserID AND O.Location <> 'Logout' AND (O.CurrentStatus <> 'DELETED' OR O.CurrentStatus IS NULL) AND M.PortalID='" + SepFunctions.Get_Portal_ID() + "' ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dWhosOnline = new Models.WhosOnline { UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]) };
                    dWhosOnline.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dWhosOnline.Location = SepFunctions.openNull(ds.Tables[0].Rows[i]["Location"]);
                    dWhosOnline.DefaultPicture = SepFunctions.userProfileImage(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    lWhosOnline.Add(dWhosOnline);
                }
            }

            return lWhosOnline;
        }
    }
}