// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ListNewMembers.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ListNewMembers.
    /// </summary>
    public class ListNewMembers
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 UserID,Username FROM Members WHERE Status=1 ORDER BY CreateDate DESC", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.AppendLine("<a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.openNull(RS["UserID"]) + "\">" + SepFunctions.openNull(RS["Username"]) + "</a><br/>");
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}