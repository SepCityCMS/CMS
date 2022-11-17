// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ActivityTypeDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ActivityTypeDropdown.
    /// </summary>
    public class ActivityTypeDropdown
    {
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
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                var s = SepCommon.Core.SepCore.Request.Item(ID);
                if (s == null)
                {
                    s = SepCommon.Core.SepCore.Strings.ToString(m_Text);
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminActivities"), true) == false)
            {
                return output.ToString();
            }

            var isSystem = false;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
                output.AppendLine("<option value=\"\">" + SepFunctions.LangText("--- Select an Activity Type ---") + "</option>");

                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT ActType FROM Activities WHERE Status <> -1 ORDER BY ActType", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                if (SepFunctions.openNull(RS["ActType"]) == "SYSTEM")
                                {
                                    isSystem = true;
                                }

                                output.AppendLine("<option value=\"" + SepFunctions.openNull(RS["ActType"]) + "\"" + SepCommon.Core.SepCore.Strings.ToString(SepFunctions.openNull(RS["ActType"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["ActType"]) + "</option>");
                            }
                        }
                    }
                }

                if (isSystem == false)
                {
                    output.AppendLine("<option value=\"SYSTEM\">SYSTEM</option>");
                }

                output.AppendLine("</select>");
            }

            return output.ToString();
        }
    }
}