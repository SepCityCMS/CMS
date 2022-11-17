// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="RatingGraph.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class RatingGraph.
    /// </summary>
    public class RatingGraph
    {
        /// <summary>
        /// The m lookup identifier
        /// </summary>
        private string m_LookupID;

        /// <summary>
        /// Gets or sets the lookup identifier.
        /// </summary>
        /// <value>The lookup identifier.</value>
        public string LookupID
        {
            get => Strings.ToString(m_LookupID);

            set => m_LookupID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Ratings the adverage.
        /// </summary>
        /// <param name="intStars1">The int stars1.</param>
        /// <param name="intStars2">The int stars2.</param>
        /// <param name="intStars3">The int stars3.</param>
        /// <param name="intStars4">The int stars4.</param>
        /// <param name="intStars5">The int stars5.</param>
        /// <returns>System.Int64.</returns>
        public long Rating_Adverage(long intStars1, long intStars2, long intStars3, long intStars4, long intStars5)
        {
            long intTotalStars = intStars1 + intStars2 + intStars3 + intStars4 + intStars5;
            return Convert.ToInt64(intTotalStars / 5);
        }

        /// <summary>
        /// Ratings the totals.
        /// </summary>
        /// <param name="intModuleID">The int module identifier.</param>
        /// <param name="intUniqueID">The int unique identifier.</param>
        /// <param name="intStars">The int stars.</param>
        /// <param name="intDays">The int days.</param>
        /// <returns>System.Int64.</returns>
        public long Rating_Totals(int intModuleID, string intUniqueID, long intStars, int intDays)
        {
            long iTotal = 0;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Count(ID) AS Counter FROM Ratings WHERE Stars=@Stars AND DatePosted BETWEEN '" + Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -intDays, DateTime.Today), Strings.DateNamedFormat.ShortDate) + "' AND '" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate) + " 23:59:55' AND ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                {
                    cmd.Parameters.AddWithValue("@Stars", intStars);
                    cmd.Parameters.AddWithValue("@ModuleID", intModuleID);
                    cmd.Parameters.AddWithValue("@UniqueID", intUniqueID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            iTotal = SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                        }
                    }
                }
            }

            return iTotal;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();
            if (ModuleID == 0)
            {
                output.AppendLine("ModuleID is Required");
                return output.ToString();
            }

            if (string.IsNullOrWhiteSpace(LookupID))
            {
                output.AppendLine("LookupID is Required");
                return output.ToString();
            }

            if (SepFunctions.Setup(8, "CNRREnable") != "Yes" || SepFunctions.Setup(8, "RGraphEnable") != "Yes")
            {
                return output.ToString();
            }

            var intTotalRecords = 0;
            long selStars1 = 0;
            long selStars2 = 0;
            long selStars3 = 0;
            long selStars4 = 0;
            long selStars5 = 0;
            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Stars FROM Ratings WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    cmd.Parameters.AddWithValue("@UniqueID", LookupID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            intTotalRecords += 1;
                            switch (SepFunctions.toInt(SepFunctions.openNull(RS["Stars"])))
                            {
                                case 5:
                                    selStars5 += 1;
                                    break;

                                case 4:
                                    selStars4 += 1;
                                    break;

                                case 3:
                                    selStars3 += 1;
                                    break;

                                case 2:
                                    selStars2 += 1;
                                    break;

                                case 1:
                                    selStars1 += 1;
                                    break;
                            }
                        }
                    }
                }
            }

            if (intTotalRecords > 0)
            {
                double DivideSize = 100 / (double)intTotalRecords;
                double selStar5Size = selStars5 * DivideSize;
                double selStar4Size = selStars4 * DivideSize;
                double selStar3Size = selStars3 * DivideSize;
                double selStar2Size = selStars2 * DivideSize;
                double selStar1Size = selStars1 * DivideSize;
                output.AppendLine("<table width=\"370\">");
                output.AppendLine("<tr>");

                output.AppendLine("<td><table width=\"240\" class=\"RatingGraph\">");
                output.AppendLine("<tr>");
                output.AppendLine("<th colspan=\"7\" align=\"center\"><b>" + SepFunctions.LangText("Rating Breakdown by Period") + "</b></th>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"100\" align=\"center\" class=\"RatingGraphTitle\">" + SepFunctions.LangText("Last 7 Days") + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 1, 7) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 2, 7) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 3, 7) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 4, 7) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 5, 7) + "</td>");
                output.AppendLine("<td width=\"40\" align=\"center\">" + Rating_Adverage(Rating_Totals(ModuleID, LookupID, 1, 7), Rating_Totals(ModuleID, LookupID, 2, 7), Rating_Totals(ModuleID, LookupID, 3, 7), Rating_Totals(ModuleID, LookupID, 4, 7), Rating_Totals(ModuleID, LookupID, 5, 7)) + "</td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"100\" align=\"center\" class=\"RatingGraphTitle\">" + SepFunctions.LangText("Last Month") + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 1, 30) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 2, 30) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 3, 30) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 4, 30) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 5, 30) + "</td>");
                output.AppendLine("<td width=\"40\" align=\"center\">" + Rating_Adverage(Rating_Totals(ModuleID, LookupID, 1, 30), Rating_Totals(ModuleID, LookupID, 2, 30), Rating_Totals(ModuleID, LookupID, 3, 30), Rating_Totals(ModuleID, LookupID, 4, 30), Rating_Totals(ModuleID, LookupID, 5, 30)) + "</td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"100\" align=\"center\" class=\"RatingGraphTitle\">" + SepFunctions.LangText("Last Year") + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 1, 365) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 2, 365) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 3, 365) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 4, 365) + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + Rating_Totals(ModuleID, LookupID, 5, 365) + "</td>");
                output.AppendLine("<td width=\"40\" align=\"center\">" + Rating_Adverage(Rating_Totals(ModuleID, LookupID, 1, 365), Rating_Totals(ModuleID, LookupID, 2, 365), Rating_Totals(ModuleID, LookupID, 3, 365), Rating_Totals(ModuleID, LookupID, 4, 365), Rating_Totals(ModuleID, LookupID, 5, 365)) + "</td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"100\" align=\"center\" class=\"RatingGraphTitle\">" + SepFunctions.LangText("Overall") + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + selStars1 + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + selStars2 + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + selStars3 + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + selStars4 + "</td>");
                output.AppendLine("<td width=\"20\" align=\"center\">" + selStars5 + "</td>");
                output.AppendLine("<td width=\"40\" align=\"center\">" + Rating_Adverage(selStars1, selStars2, selStars3, selStars4, selStars5) + "</td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"100\" align=\"center\" class=\"RatingGraphTitle\"></td>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">1</td>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">2</td>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">3</td>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">4</td>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">5</td>");
                output.AppendLine("<td width=\"40\" align=\"center\" class=\"RatingGraphTitle\">AVG</td>");
                output.AppendLine("</tr>");
                output.AppendLine("</table></td>");

                output.AppendLine("<td width=\"10\">&nbsp;</td>");

                output.AppendLine("<td align=\"right\"><table width=\"120\" class=\"RatingGraph\">");
                output.AppendLine("<tr>");
                output.AppendLine("<th colspan=\"2\" align=\"center\"><b>" + SepFunctions.LangText("Rating Totals") + "</b></th>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">1</td>");
                output.AppendLine("<td width=\"100\"><div style=\"width:" + selStar1Size + "px;height:11px;\" class=\"RatingGraphBar\"></div></td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">2</td>");
                output.AppendLine("<td width=\"100\"><div style=\"width:" + selStar2Size + "px;height:11px;\" class=\"RatingGraphBar\"></div></td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">3</td>");
                output.AppendLine("<td width=\"100\"><div style=\"width:" + selStar3Size + "px;height:11px;\" class=\"RatingGraphBar\"></div></td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">4</td>");
                output.AppendLine("<td width=\"100\"><div style=\"width:" + selStar4Size + "px;height:11px;\" class=\"RatingGraphBar\"></div></td>");
                output.AppendLine("</tr><tr>");
                output.AppendLine("<td width=\"20\" align=\"center\" class=\"RatingGraphTitle\">5</td>");
                output.AppendLine("<td width=\"100\"><div style=\"width:" + selStar5Size + "px;height:11px;\" class=\"RatingGraphBar\"></div></td>");
                output.AppendLine("</tr>");
                output.AppendLine("</table></td>");

                output.AppendLine("</tr>");
                output.AppendLine("</table>");
            }

            return output.ToString();
        }
    }
}