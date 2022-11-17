// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="LanguageDropdowm.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using System.Text;

    /// <summary>
    /// Class LanguageDropdown.
    /// </summary>
    public class LanguageDropdown
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

            output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
            output.AppendLine("<option value=\"EN-US\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "EN-US" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("English (United States)") + "</option>");
            output.AppendLine("<option value=\"NL-NL\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "NL-NL" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Dutch (The Netherlands)") + "</option>");
            output.AppendLine("<option value=\"FR-CA\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "FR-CA" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("French (Canada)") + "</option>");
            output.AppendLine("<option value=\"FR-FR\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "FR-FR" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("French (France)") + "</option>");
            output.AppendLine("<option value=\"PT-BR\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "PT-BR" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Portuguese (Brazil)") + "</option>");
            output.AppendLine("<option value=\"ES-MX\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "ES-MX" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Spanish (Mexico)") + "</option>");
            output.AppendLine("<option value=\"ES-ES\"" + SepCommon.Core.SepCore.Strings.ToString(Text == "ES-ES" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Spanish (Spain)") + "</option>");
            output.AppendLine("</select>");
            return output.ToString();
        }
    }
}