// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Newsletters.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Collections;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class Newsletters.
    /// </summary>
    public class Newsletters
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(24, "NewsLetEnable") != "Enable")
            {
                return output.ToString();
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("NewsletJoin"), true) == false)
            {
                return output.ToString();
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            var userEmail = SepFunctions.GetUserInformation("EmailAddress");

            var letterHash = new Hashtable();

            output.AppendLine("<form name=\"frmNewsletters\" method=\"post\" action=\"" + sInstallFolder + "default.aspx?DoAction=JoinNewsletter\">");
            //TODO
            //output.AppendLine(System.Web.Helpers.AntiForgery.GetHtml().ToString());
            output.AppendLine("<div id=\"idNewsLetBox\" class=\"col-lg-12\">");
            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var sNoUser = !string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) ? " AND LetterID NOT IN (SELECT LetterID FROM NewslettersUsers WHERE LetterID=LetterID AND EmailAddress='" + SepFunctions.FixWord(userEmail) + "')" : string.Empty;

                using (SqlCommand cmd = new SqlCommand("SELECT LetterID,JoinKeys,NewsletName FROM Newsletters WHERE (PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0) AND Status <> -1" + sNoUser + " ORDER BY NewsletName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            if (SepFunctions.CompareKeys(SepFunctions.openNull(RS["JoinKeys"]), true))
                            {
                                letterHash.Add(SepFunctions.openNull(RS["LetterID"]), SepFunctions.openNull(RS["NewsletName"]));
                            }
                        }
                    }

                    if (letterHash.Count > 0)
                    {
                        output.AppendLine("<table>");
                        output.AppendLine("<tr>");
                        output.AppendLine("<td>");
                        output.Append(SepFunctions.LangText("Your Email Address"));
                        output.AppendLine("<div align=\"right\"><input type=\"text\" id=\"NLEmailAddress\" name=\"NLEmailAddress\" value=\"" + userEmail + "\" class=\"NewsletField\" /></div>");
                    }

                    if (letterHash.Count == 0)
                    {
                        output.Append("<div class=\"col-lg-12\">" + SepFunctions.LangText("There currently aren't any newsletters.") + "</div>");
                    }
                    else
                    {
                        if (letterHash.Count > 1)
                        {
                            foreach (DictionaryEntry de in letterHash)
                            {
                                output.AppendLine("<input type=\"checkbox\" id=\"Newsletter" + de.Key + "\" name=\"Newsletter" + de.Key + "\" value=\"" + de.Key + "\" /> " + de.Value + "<br/>");
                            }
                        }
                        else
                        {
                            foreach (DictionaryEntry de in letterHash)
                            {
                                output.AppendLine("<input type=\"hidden\" id=\"Newsletter" + de.Key + "\" name=\"Newsletter" + de.Key + "\" value=\"" + de.Key + "\" />");
                            }
                        }

                        output.AppendLine("<div align=\"right\"><input type=\"submit\" class=\"btn btn-secondary\" value=\"" + SepFunctions.LangText("Join") + "\" /></div></td></tr>");
                        output.AppendLine("</table>");
                    }
                }
            }

            output.AppendLine("</div>");
            output.AppendLine("</form>");

            return output.ToString();
        }
    }
}