// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CategorySelection.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class CategorySelection.
    /// </summary>
    public class CategorySelection
    {
        /// <summary>
        /// The m only include
        /// </summary>
        private string m_OnlyInclude;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the only include.
        /// </summary>
        /// <value>The only include.</value>
        public string OnlyInclude
        {
            get => Strings.ToString(m_OnlyInclude);

            set => m_OnlyInclude = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                var s = Request.Item(ID);
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.showCategories() == false)
            {
                return output.ToString();
            }

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var cUrl = sImageFolder + "js/CategorySelection.js";

            output.AppendLine("<script type=\"text/javascript\" src=\"" + cUrl + "\">");
            long aa = 0;

            var wclause = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    wclause += " AND CatID IN ('" + Strings.Replace(Strings.Replace(OnlyInclude, "|", string.Empty), ",", "','") + "')";
                }

                output.AppendLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"" + Text + "\" />");

                output.AppendLine("<div class=\"input-group\">");
                output.AppendLine("<input type=\"text\" name=\"CatSearch\" id=\"CatSearch\" placeholder=\"" + SepFunctions.LangText("Search for...") + "\" onkeydown=\"if(submitCatKeyPress(event) == '13'){submitCatSearch('" + SepFunctions.Get_Portal_ID() + "', '" + ID + "');return false;};\" />");
                output.AppendLine("<span class=\"input-group-btn\">");
                output.AppendLine("<button class=\"btn btn-default\" type=\"button\" onclick=\"submitCatSearch('" + SepFunctions.Get_Portal_ID() + "', '" + ID + "');\">" + SepFunctions.LangText("Search") + "</button>");
                output.AppendLine("</span>");
                output.AppendLine("</div>");

                output.AppendLine("<br/>");

                output.AppendLine("<div style=\"width:245px;float:left;\">" + SepFunctions.LangText("Available Categories") + "</div>");
                output.AppendLine("<div style=\"width:10px;float:left;\">&nbsp;&nbsp;</div>");
                output.AppendLine("<div style=\"width:245px;float:left;\">" + SepFunctions.LangText("Selected Categories") + "</div><br/>");
                output.AppendLine("<div id=\"SearchCatList\" style=\"width:245px;height:100px;overflow:auto;border:1px solid;border-color:#000000;float:left;\"><span id=\"SearchCatListHelp\">" + SepFunctions.LangText("Enter the beginning part of a category name above to run a search to select one or more categories.") + "<br/><br/>" + SepFunctions.LangText("(Ex. \"Oh\" will show \"Ohio\")") + "</span>");
                output.AppendLine("<table width=\"100%\" class=\"Table\" id=\"availableItems\">");
                output.AppendLine("</table>");
                output.AppendLine("</div>");
                output.AppendLine("<div style=\"width:10px;float:left;\">&nbsp;&nbsp;</div>");
                output.AppendLine("<div id=\"SearchCatSelected\" style=\"width:245px;height:100px;overflow:auto;border:1px solid;border-color:#000000;float:left;\">");
                output.AppendLine("<table width=\"100%\" class=\"Table\" id=\"selectedItems\">");
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    string[] arrCatID = Strings.Split(Text, ",");

                    if (arrCatID != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrCatID); i++)
                        {
                            if (arrCatID[i] != "|0|")
                            {
                                using (SqlCommand cmd = new SqlCommand("SELECT CatID,CategoryName,ListUnder FROM Categories WHERE CatID=@CatID" + wclause, conn))
                                {
                                    cmd.Parameters.AddWithValue("@CatID", Strings.Replace(arrCatID[i], "|", string.Empty));
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (aa % 2 == 0)
                                            {
                                                output.AppendLine("<tr class=\"TableBody1\" id=\"sel" + SepFunctions.openNull(RS["CatID"]) + "\">");
                                            }
                                            else
                                            {
                                                output.AppendLine("<tr class=\"TableBody2\" id=\"sel" + SepFunctions.openNull(RS["CatID"]) + "\">");
                                            }

                                            output.AppendLine("<td style=\"cursor:pointer;\" onclick=\"removeCategory('" + SepFunctions.openNull(RS["CatID"]) + "', '" + ID + "');\" id=\"seltd" + SepFunctions.openNull(RS["CatID"]) + "\"><b>");
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ListUnder"])) && SepFunctions.openNull(RS["ListUnder"]) != "0")
                                            {
                                                using (SqlCommand cmd2 = new SqlCommand("SELECT CategoryName FROM Categories WHERE CatID=@CatID", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS["ListUnder"]));
                                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                    {
                                                        if (RS2.HasRows)
                                                        {
                                                            RS2.Read();
                                                            output.Append(SepFunctions.openNull(RS2["CategoryName"]) + " / ");
                                                        }
                                                    }
                                                }
                                            }

                                            output.Append(SepFunctions.openNull(RS["CategoryName"]));
                                            output.AppendLine("</b></td>");
                                            output.AppendLine("</tr>");
                                            aa += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                output.AppendLine("</table>");
                output.AppendLine("</div>");

                output.AppendLine("<div style=\"clear:left;\"></div>");
            }

            return output.ToString();
        }
    }
}