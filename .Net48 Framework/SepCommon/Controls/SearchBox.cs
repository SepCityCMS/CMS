// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SearchBox.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class SearchBox.
    /// </summary>
    public class SearchBox
    {
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
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("<script type=\"text/javascript\">");
            output.AppendLine("function submitSearch" + ID + "(keywords) {");
            output.AppendLine("  document.location.href='" + SepFunctions.GetInstallFolder() + "search.aspx?q='+keywords;");
            output.AppendLine("}");
            output.AppendLine("</script>");

            output.AppendLine("<div class=\"col-lg-12\">");
            output.AppendLine("<div class=\"input-group\">");
            output.AppendLine("<input type=\"text\" class=\"" + CssClass + "\" id=\"" + ID + "\" /> ");
            output.AppendLine("<span class=\"input-group-btn\"><input type=\"button\" onclick=\"submitSearch" + ID + "($('#" + ID + "').val())\" class=\"btn btn-secondary\" value=\"" + SepFunctions.LangText("Go") + "\" /></span>");
            output.AppendLine("</div>");
            output.AppendLine("</div>");

            return output.ToString();
        }
    }
}