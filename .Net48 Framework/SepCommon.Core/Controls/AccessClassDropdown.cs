// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AccessClassDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class AccessClassDropdown.
    /// </summary>
    public class AccessClassDropdown
    {
        /// <summary>
        /// The m select width
        /// </summary>
        private string m_SelectWidth;

        /// <summary>
        /// The m show delete account
        /// </summary>
        private string m_showDeleteAccount;

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
        /// Gets or sets the width of the select.
        /// </summary>
        /// <value>The width of the select.</value>
        public string SelectWidth
        {
            get => Strings.ToString(m_SelectWidth);

            set => m_SelectWidth = value;
        }

        /// <summary>
        /// Gets or sets the show delete account.
        /// </summary>
        /// <value>The show delete account.</value>
        public string showDeleteAccount
        {
            get => Strings.ToString(m_showDeleteAccount);

            set => m_showDeleteAccount = value;
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), true) == false)
            {
                return output.ToString();
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var sWidth = !string.IsNullOrWhiteSpace(SelectWidth) ? " style=\"width:" + SelectWidth + "\"" : string.Empty;
                output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\"" + sWidth + ">");
                output.AppendLine("<option value=\"\">" + SepFunctions.LangText("--- Select a Access Class ---") + "</option>");

                if (Strings.LCase(showDeleteAccount) == "true")
                {
                    output.AppendLine("<option value=\"DeleteAccount\"" + Strings.ToString("DeleteAccount" == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Delete Account") + "</option>");
                }

                var excludeAdmin = SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity")) == false ? " AND ClassID <> '2'" : string.Empty;
                using (SqlCommand cmd = new SqlCommand("SELECT ClassID,ClassName FROM AccessClasses WHERE Status <> -1" + excludeAdmin + " ORDER BY ClassName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                output.AppendLine("<option value=\"" + SepFunctions.openNull(RS["ClassID"]) + "\"" + Strings.ToString(SepFunctions.openNull(RS["ClassID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["ClassName"]) + "</option>");
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