// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="MemberStatistics.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class MemberStatistics.
    /// </summary>
    public class MemberStatistics
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var SqlStr = string.Empty;

            var sInstallFolder = SepFunctions.GetInstallFolder();

            SqlStr = "SELECT (SELECT Count(UserID) FROM Members WHERE Status=1 AND Month(CreateDate)='" + DateAndTime.Month(DateTime.Today) + "' AND Year(CreateDate)='" + DateAndTime.Year(DateTime.Today) + "' AND Day(CreateDate)='" + DateAndTime.Day(DateTime.Today) + "') AS JoinedToday,";
            SqlStr += "(SELECT Count(UserID) FROM Members WHERE Status=1 AND CreateDate BETWEEN '" + Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -7, DateTime.Today), Strings.DateNamedFormat.ShortDate) + "' AND '" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.GeneralDate) + "') AS JoinedWeek,";
            SqlStr += "(SELECT Count(UserID) FROM Members WHERE Status=1 AND CreateDate BETWEEN '" + Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -30, DateTime.Today), Strings.DateNamedFormat.ShortDate) + "' AND '" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.GeneralDate) + "') AS JoinedMonth,";
            SqlStr += "(SELECT Count(UserID) FROM Members WHERE Status=1 AND CreateDate BETWEEN '" + Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -60, DateTime.Today), Strings.DateNamedFormat.ShortDate) + "' AND '" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.GeneralDate) + "') AS Joined2Months,";
            SqlStr += "(SELECT Count(UserID) FROM Members WHERE Status=1 AND CreateDate BETWEEN '" + Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -90, DateTime.Today), Strings.DateNamedFormat.ShortDate) + "' AND '" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.GeneralDate) + "') AS Joined3Months,";
            SqlStr += "(SELECT Count(UserID) FROM Members WHERE Status=1) AS TotalUsers";

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            output.AppendLine("<b>" + SepFunctions.LangText("Joined Today") + "</b> " + SepFunctions.openNull(RS["JoinedToday"]) + "<br/>");
                            output.AppendLine("<b>" + SepFunctions.LangText("Joined Last 7 Days") + "</b> " + SepFunctions.openNull(RS["JoinedWeek"]) + "<br/>");
                            output.AppendLine("<b>" + SepFunctions.LangText("Joined Last 30 Days") + "</b> " + SepFunctions.openNull(RS["JoinedMonth"]) + "<br/>");
                            output.AppendLine("<b>" + SepFunctions.LangText("Joined Last 60 Days") + "</b> " + SepFunctions.openNull(RS["Joined2Months"]) + "<br/>");
                            output.AppendLine("<b>" + SepFunctions.LangText("Joined Last 90 Days") + "</b> " + SepFunctions.openNull(RS["Joined3Months"]) + "<br/>");
                            output.AppendLine("<b>" + SepFunctions.LangText("Total Users") + "</b> " + SepFunctions.openNull(RS["TotalUsers"]) + "<br/>");
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 20 UserID,Username FROM Members WHERE Status=1 ORDER BY CreateDate DESC", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.AppendLine("<br/><b>" + SepFunctions.LangText("Click below to meet") + "<br/>" + SepFunctions.LangText("our new members") + "</b><br/><br/>");
                            while (RS.Read())
                            {
                                output.AppendLine("<a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.openNull(RS["UserID"]) + "\">" + SepFunctions.openNull(RS["Username"]) + "</a><br/>");
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}