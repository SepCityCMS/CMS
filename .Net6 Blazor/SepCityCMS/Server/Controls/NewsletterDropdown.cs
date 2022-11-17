// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="NewsletterDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class NewsletterDropdown.
    /// </summary>
    public class NewsletterDropdown
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
                var s = SepCore.Request.Item(ID);
                if (s == null)
                {
                    s = SepCore.Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

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

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
                output.AppendLine("<option value=\"\">" + SepFunctions.LangText("Select a Newsletter") + "</option>");
                using (SqlCommand cmd = new SqlCommand("SELECT LetterID,NewsletName FROM Newsletters WHERE PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0 ORDER BY NewsletName", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalIDs", SepCore.Strings.ToString(SepFunctions.Get_Portal_ID()));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.AppendLine("<option value=\"" + SepFunctions.openNull(RS["LetterID"]) + "\"" + SepCore.Strings.ToString(Text == SepFunctions.openNull(RS["LetterID"]) ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["NewsletName"]) + "</option>");
                        }
                    }
                }

                output.AppendLine("</select>");
            }

            return output.ToString();
        }
    }
}