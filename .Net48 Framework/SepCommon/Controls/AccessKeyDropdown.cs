// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AccessKeyDropdown.cs" company="SepCity, Inc.">
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
    /// Class AccessKeyDropdown.
    /// </summary>
    public class AccessKeyDropdown
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
                var s = SepCommon.SepCore.Request.Item(ID);
                if (s == null)
                {
                    s = SepCommon.SepCore.Strings.ToString(m_Text);
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), true) == false)
            {
                return output.ToString();
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
                output.AppendLine("<option value=\"\">" + SepFunctions.LangText("--- Select a Access Key ---") + "</option>");

                var sAdminMode = SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity")) == false ? " AND KeyID <> '2'" : string.Empty;

                using (SqlCommand cmd = new SqlCommand("SELECT KeyID,KeyName FROM AccessKeys WHERE Status <> -1" + sAdminMode + " ORDER BY KeyName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                output.AppendLine("<option value=\"" + SepFunctions.openNull(RS["KeyID"]) + "\"" + SepCommon.SepCore.Strings.ToString(SepFunctions.openNull(RS["KeyID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["KeyName"]) + "</option>");
                            }
                        }
                    }
                }

                output.AppendLine("</select>");
            }

            return output.ToString();
        }
    }
}