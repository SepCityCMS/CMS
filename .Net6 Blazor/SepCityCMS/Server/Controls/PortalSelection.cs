// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PortalSelection.cs" company="SepCity, Inc.">
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
    /// Class PortalSelection.
    /// </summary>
    public class PortalSelection
    {
        /// <summary>
        /// The m exclude master
        /// </summary>
        private bool m_ExcludeMaster;

        /// <summary>
        /// The m only include
        /// </summary>
        private string m_OnlyInclude;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets a value indicating whether [exclude master].
        /// </summary>
        /// <value><c>true</c> if [exclude master]; otherwise, <c>false</c>.</value>
        public bool ExcludeMaster
        {
            get
            {
                var s = Convert.ToBoolean(m_ExcludeMaster);
                return s;
            }

            set => m_ExcludeMaster = value;
        }

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

                if (string.IsNullOrWhiteSpace(s))
                {
                    return "|-1|";
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

            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable")
            {
                return output.ToString();
            }

            double iHalf = 0;
            long aCount = 0;

            var wclause = string.Empty;

            if (!string.IsNullOrWhiteSpace(OnlyInclude))
            {
                wclause += " AND PortalID IN ('" + Strings.Replace(Strings.Replace(OnlyInclude, "|", string.Empty), ",", "','") + "')";
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Count(PortalID) AS RCount FROM Portals WHERE Status <> -1" + wclause, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            iHalf = Math.Ceiling((SepFunctions.toDouble(SepFunctions.openNull(RS["RCount"])) - 2) / 2);
                        }
                    }
                }

                output.AppendLine("<a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', 'checked');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', null);\">" + SepFunctions.LangText("Deselect All") + "</a>");

                output.AppendLine("<div class=\"MultiCheckboxDiv\" id=\"" + ID + "Checkboxes\">");
                output.AppendLine("<div class=\"MultiCheckboxDivLeft\">");

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    if (Strings.InStr(OnlyInclude, "|-1|") > 0)
                    {
                        aCount = aCount + 1;
                        output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Portals") + "</label><br/>");
                    }

                    if (Strings.InStr(OnlyInclude, "|0|") > 0 && ExcludeMaster == false)
                    {
                        aCount = aCount + 1;
                        output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|0|\"" + Strings.ToString(Strings.InStr(Text, "|0|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("Main Portal") + "</label><br/>");
                    }
                }
                else
                {
                    aCount = aCount + 1;
                    output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Portals") + "</label><br/>");
                    if (ExcludeMaster == false)
                    {
                        aCount = aCount + 1;
                        output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|0|\"" + Strings.ToString(Strings.InStr(Text, "|0|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("Main Portal") + "</label><br/>");
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT PortalID,PortalTitle FROM Portals WHERE Status <> -1" + wclause + " ORDER BY PortalTitle", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            aCount = aCount + 1;
                            output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + SepFunctions.openNull(RS["PortalID"]) + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + SepFunctions.openNull(RS["PortalID"]) + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.openNull(RS["PortalTitle"]) + "</label><br/>");
                            if (aCount == iHalf)
                            {
                                // -V3024
                                output.AppendLine("</div><div class=\"MultiCheckboxDivRight\">");
                            }
                        }
                    }
                }

                output.AppendLine("</div>");
                output.AppendLine("</div>");
            }

            return output.ToString();
        }
    }
}