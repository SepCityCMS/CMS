// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ModuleReviews.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ModuleReviews.
    /// </summary>
    public class ModuleReviews
    {
        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => SepCommon.SepCore.Strings.ToString(m_UserID);

            set => m_UserID = value;
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();
            long CurrentUserCount = 0;
            var DisableSave = false;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT ReviewID FROM Reviews WHERE ModuleIDs LIKE '%|" + ModuleID + "|%' ORDER BY Weight,Question", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.AppendLine("<script type=\"text/javascript\">");
                            output.AppendLine("function saveReview() {");
                            while (RS.Read())
                            {
                                output.AppendLine("\t$.ajax({");
                                output.AppendLine("\t\turl: '" + sInstallFolder + "review_save.aspx?ModuleID=" + ModuleID + "&UserID=" + UserID + "&UniqueID=" + SepFunctions.openNull(RS["ReviewID"]) + "&Rating=' + $('#Review" + SepFunctions.openNull(RS["ReviewID"]) + "').val(),");
                                output.AppendLine("\t\ttype: 'POST'");
                                output.AppendLine("\t});");
                            }

                            output.AppendLine("alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("You have successfully rated this user.")) + "'));");
                            output.AppendLine("document.location.reload();");
                            output.AppendLine("}");
                            output.AppendLine("</script>");
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Reviews WHERE ModuleIDs LIKE '%|" + ModuleID + "|%' ORDER BY Weight,Question", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.AppendLine("<div class=\"alert alert-secondary\">" + SepFunctions.LangText("Review Me") + "</div>");

                            output.AppendLine("<table class=\"Table\" width=\"100%\" align=\"center\">");
                            while (RS.Read())
                            {
                                output.AppendLine("<tr class=\"TableBody1\">");
                                output.AppendLine("<td valign=\"top\" width=\"180\"><b>" + SepFunctions.openNull(RS["Question"]) + "</b></td>");
                                using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM ReviewsUsers WHERE ReviewID=@ReviewID AND UserID=@UserID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ReviewID", SepFunctions.openNull(RS["ReviewID"]));
                                    cmd2.Parameters.AddWithValue("@UserID", UserID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            RS2.Read();
                                            double TotalStars = SepFunctions.toDouble(SepFunctions.openNull(RS2["TotalStars"])) / SepFunctions.toDouble(SepFunctions.openNull(RS2["TotalUsers"]));
                                            output.AppendLine("<td valign=\"top\">" + SepFunctions.Rating_Stars_Display(TotalStars) + "</td>");
                                        }
                                        else
                                        {
                                            output.AppendLine("<td valign=\"top\">" + SepFunctions.LangText("Not Rated") + "</td>");
                                        }
                                    }
                                }

                                output.AppendLine("<td valign=\"top\">");
                                if (SepFunctions.Check_Rating(ModuleID, SepFunctions.openNull(RS["ReviewID"]), UserID) == false)
                                {
                                    DisableSave = false;
                                    output.AppendLine("<select name=\"Review" + SepFunctions.openNull(RS["ReviewID"]) + "\" id=\"Review" + SepFunctions.openNull(RS["ReviewID"]) + "\" class=\"inline-block\" style=\"width:100px;\">");
                                    output.AppendLine("<option value=\"5\">" + SepFunctions.LangText("5 Stars") + "</option>");
                                    output.AppendLine("<option value=\"4\">" + SepFunctions.LangText("4 Stars") + "</option>");
                                    output.AppendLine("<option value=\"3\">" + SepFunctions.LangText("3 Stars") + "</option>");
                                    output.AppendLine("<option value=\"2\">" + SepFunctions.LangText("2 Stars") + "</option>");
                                    output.AppendLine("<option value=\"1\">" + SepFunctions.LangText("1 Stars") + "</option>");
                                    output.AppendLine("</select>");
                                }
                                else
                                {
                                    DisableSave = true;
                                }

                                output.AppendLine("</td>");
                                output.AppendLine("</tr>");
                                using (SqlCommand cmd2 = new SqlCommand("SELECT ActivityID FROM Activities WHERE ModuleID=@ModuleID AND UniqueID=@ReviewID AND UserID=@UserID AND ActType='RATING'", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ModuleID", ModuleID);
                                    cmd2.Parameters.AddWithValue("@ReviewID", SepFunctions.openNull(RS["ReviewID"]));
                                    cmd2.Parameters.AddWithValue("@UserID", UserID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            CurrentUserCount += 1;
                                        }
                                    }
                                }
                            }

                            if (DisableSave == false)
                            {
                                output.AppendLine("<tr class=\"TableBody1\">");
                                output.AppendLine("<td colspan=\"2\"></td>");
                                output.AppendLine("<td><input type=\"button\" name=\"SaveReview\" id=\"SaveReviewBUtton\" onclick=\"saveReview()\" class=\"btn btn-light\" value=\"" + SepFunctions.LangText("Save") + "\" /></td>");
                                output.AppendLine("</tr>");
                            }

                            output.AppendLine("</table>");
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}