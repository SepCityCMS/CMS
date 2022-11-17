// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="LanguageDropdowm.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class LanguageDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LanguageDropdown runat=server></{0}:LanguageDropdown>")]
    public class LanguageDropdown : WebControl
    {
        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                var s = Context.Request.Form[ID];
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // -V3013
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
            output.WriteLine("<option value=\"EN-US\"" + Strings.ToString(Text == "EN-US" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("English (United States)") + "</option>");
            output.WriteLine("<option value=\"NL-NL\"" + Strings.ToString(Text == "NL-NL" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Dutch (The Netherlands)") + "</option>");
            output.WriteLine("<option value=\"FR-CA\"" + Strings.ToString(Text == "FR-CA" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("French (Canada)") + "</option>");
            output.WriteLine("<option value=\"FR-FR\"" + Strings.ToString(Text == "FR-FR" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("French (France)") + "</option>");
            output.WriteLine("<option value=\"PT-BR\"" + Strings.ToString(Text == "PT-BR" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Portuguese (Brazil)") + "</option>");
            output.WriteLine("<option value=\"ES-MX\"" + Strings.ToString(Text == "ES-MX" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Spanish (Mexico)") + "</option>");
            output.WriteLine("<option value=\"ES-ES\"" + Strings.ToString(Text == "ES-ES" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Spanish (Spain)") + "</option>");
            output.WriteLine("</select>");
        }
    }
}