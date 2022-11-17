// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="SignupAccount.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using SepCommon.Core.SepCore;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Shows the newsletters.
        /// </summary>
        /// <param name="UserID">Identifier for the user.</param>
        /// <returns>A string.</returns>
        public static string Show_Newsletters(string UserID)
        {
            var str = new StringBuilder();

            if (Setup(24, "NewsLetEnable") == "Enable")
            {
                var SqlStr = string.Empty;
                long recordCount = 0;

                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();

                    if (!string.IsNullOrWhiteSpace(UserID))
                    {
                        SqlStr = "SELECT NL.LetterID,NL.NewsletName,(SELECT TOP 1 LetterID FROM NewslettersUsers WHERE UserID='" + Session_User_ID() + "' AND LetterID=NL.LetterID) AS isChecked FROM Newsletters AS NL WHERE (NL.PortalIDs LIKE '%|' + @PortalIDs + '|%' OR NL.PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0) AND Status <> -1 ORDER BY NL.NewsletName";
                    }
                    else
                    {
                        SqlStr = "SELECT LetterID,NewsletName,'' AS isChecked FROM Newsletters WHERE (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0) AND Status <> -1 ORDER BY NewsletName";
                    }

                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalIDs", Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                str.Append("<table>");
                                while (RS.Read())
                                {
                                    recordCount = recordCount + 1;
                                    if (recordCount % 2 == 1)
                                    {
                                        str.Append("<tr>");
                                    }

                                    var sChecked = !string.IsNullOrWhiteSpace(openNull(RS["isChecked"])) || Strings.InStr(Request.Item("LetterIDs"), openNull(RS["LetterID"])) > 0 ? " checked=\"checked\"" : string.Empty;
                                    str.Append("<td width=\"50%\"><input type=\"checkbox\" name=\"LetterIDs\" id=\"LetterID" + openNull(RS["LetterID"]) + "\" value=\"" + openNull(RS["LetterID"]) + "\"" + sChecked + " /> " + openNull(RS["NewsletName"]) + "</td>");
                                }

                                str.Append("</table>");
                            }
                        }
                    }
                }
            }

            return Strings.ToString(str);
        }
    }
}