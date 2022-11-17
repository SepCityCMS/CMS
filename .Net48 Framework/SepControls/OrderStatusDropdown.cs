// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="OrderStatusDropdown.cs" company="SepCity, Inc.">
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
    /// Class OrderStatusDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:OrderStatusDropdown runat=server></{0}:OrderStatusDropdown>")]
    public class OrderStatusDropdown : WebControl
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
            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin")) == false)
            {
                return;
            }

            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
            output.WriteLine("<option value=\"0\"" + Strings.ToString(Text == "0" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Not Paid") + "</option>");
            output.WriteLine("<option value=\"1\"" + Strings.ToString(Text == "1" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Paid") + "</option>");
            output.WriteLine("<option value=\"2\"" + Strings.ToString(Text == "2" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipped (All)") + "</option>");
            output.WriteLine("<option value=\"3\"" + Strings.ToString(Text == "3" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipped (Partial)") + "</option>");
            output.WriteLine("<option value=\"4\"" + Strings.ToString(Text == "4" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipped (Multiple Packages)") + "</option>");
            output.WriteLine("<option value=\"5\"" + Strings.ToString(Text == "5" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Temporarity Out of Stock") + "</option>");
            output.WriteLine("<option value=\"6\"" + Strings.ToString(Text == "6" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Shipment Returned") + "</option>");
            output.WriteLine("<option value=\"7\"" + Strings.ToString(Text == "7" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Back Order") + "</option>");
            output.WriteLine("<option value=\"8\"" + Strings.ToString(Text == "8" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Voided") + "</option>");
            output.WriteLine("</select>");
        }
    }
}