// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="OrderStatusDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Text;

    /// <summary>
    /// Class OrderStatusDropdown.
    /// </summary>
    public class OrderStatusDropdown
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
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin"), true) == false)
            {
                return output.ToString();
            }

            output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
            output.AppendLine("<option value=\"0\"" + SepCore.Strings.ToString(Text == "0" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Not Paid") + "</option>");
            output.AppendLine("<option value=\"1\"" + SepCore.Strings.ToString(Text == "1" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Paid") + "</option>");
            output.AppendLine("<option value=\"2\"" + SepCore.Strings.ToString(Text == "2" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipped (All)") + "</option>");
            output.AppendLine("<option value=\"3\"" + SepCore.Strings.ToString(Text == "3" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipped (Partial)") + "</option>");
            output.AppendLine("<option value=\"4\"" + SepCore.Strings.ToString(Text == "4" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipped (Multiple Packages)") + "</option>");
            output.AppendLine("<option value=\"5\"" + SepCore.Strings.ToString(Text == "5" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Temporarity Out of Stock") + "</option>");
            output.AppendLine("<option value=\"6\"" + SepCore.Strings.ToString(Text == "6" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipment Returned") + "</option>");
            output.AppendLine("<option value=\"7\"" + SepCore.Strings.ToString(Text == "7" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Back Order") + "</option>");
            output.AppendLine("<option value=\"8\"" + SepCore.Strings.ToString(Text == "8" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Voided") + "</option>");
            output.AppendLine("</select>");
            return output.ToString();
        }
    }
}