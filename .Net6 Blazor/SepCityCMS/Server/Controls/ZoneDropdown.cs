// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ZoneDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using SepCore;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ZoneDropdown.
    /// </summary>
    public class ZoneDropdown
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
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

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

            if (ModuleID == 0)
            {
                output.AppendLine("ModuleID is Required");
                return output.ToString();
            }

            var wclause = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    wclause += " AND ZoneID IN ('" + Strings.Replace(Strings.Replace(OnlyInclude, "|", string.Empty), ",", "','") + "')";
                }

                if (ModuleID > 0)
                {
                    wclause += " AND ModuleID='" + SepFunctions.FixWord(Strings.ToString(ModuleID)) + "'";
                }

                using (SqlCommand cmd = new SqlCommand("SELECT ZoneID,ZoneName FROM TargetZones WHERE Status <> -1" + wclause + " ORDER BY ZoneName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
                            output.AppendLine("<option value=\"\">" + SepFunctions.LangText("--- Select a Zone ---") + "</option>");
                            while (RS.Read())
                            {
                                output.AppendLine("<option value=\"" + SepFunctions.openNull(RS["ZoneID"]) + "\"" + Strings.ToString(SepFunctions.openNull(RS["ZoneID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["ZoneName"]) + "</option>");
                            }

                            output.AppendLine("</select>");
                        }
                        else
                        {
                            output.AppendLine("<span class=\"failureNotification\">" + SepFunctions.LangText("You do not have any zones specified.") + "</span>");
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}