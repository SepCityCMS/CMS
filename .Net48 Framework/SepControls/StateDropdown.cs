// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="StateDropdown.cs" company="SepCity, Inc.">
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
    /// Class StateDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:StateDropdown runat=server></{0}:StateDropdown>")]
    public class StateDropdown : WebControl
    {
        /// <summary>
        /// The m country
        /// </summary>
        private string m_Country;

        /// <summary>
        /// The m select width
        /// </summary>
        private string m_SelectWidth;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country
        {
            get => Strings.ToString(m_Country);

            set => m_Country = value;
        }

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
            var sCity = string.Empty;
            var sState = string.Empty;
            var sCountry = string.Empty;

            if (string.IsNullOrWhiteSpace(Text))
            {
                if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    SepFunctions.IP2Location(SepFunctions.GetUserIP(), ref sCity, ref sState, ref sCountry);
                    Text = sState;
                    Country = sCountry;
                }
                else
                {
                    Text = SepFunctions.GetUserInformation("State");
                    Country = SepFunctions.GetUserInformation("Country");
                }
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                Country = SepFunctions.Setup(991, "CompanyCountry");
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                Country = "us";
            }

            var sWidth = !string.IsNullOrWhiteSpace(SelectWidth) ? " style=\"width:" + SelectWidth + "\"" : string.Empty;
            var sDisabled = Enabled == false ? " disabled=\"disabled\"" : string.Empty;

            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\"" + sDisabled + sWidth + ">");
            output.WriteLine("<option value=\"\">" + SepFunctions.LangText("--- Select a State/Province ---") + "</option>");
            try
            {
                var Provinces = SepFunctions.ProvincesInCountry(Country);
                for (var i = 0; i < Provinces.Provinces.Count; i++)
                {
                    output.WriteLine("<option value=\"" + Provinces.Provinces[i] + "\"" + Strings.ToString(Text == Provinces.Provinces[i] ? " selected=\"selected\"" : string.Empty) + ">" + Provinces.Provinces[i] + "</option>");
                }
            }
            catch
            {
                output.WriteLine("<option value=\"Alabama\"" + Strings.ToString(Text == "Alabama" ? " selected=\"selected\"" : string.Empty) + ">Alabama</option>");
                output.WriteLine("<option value=\"Alaska\"" + Strings.ToString(Text == "Alaska" ? " selected=\"selected\"" : string.Empty) + ">Alaska</option>");
                output.WriteLine("<option value=\"Arizona\"" + Strings.ToString(Text == "Arizona" ? " selected=\"selected\"" : string.Empty) + ">Arizona</option>");
                output.WriteLine("<option value=\"Arkansas\"" + Strings.ToString(Text == "Arkansas" ? " selected=\"selected\"" : string.Empty) + ">Arkansas</option>");
                output.WriteLine("<option value=\"California\"" + Strings.ToString(Text == "California" ? " selected=\"selected\"" : string.Empty) + ">California</option>");
                output.WriteLine("<option value=\"Colorado\"" + Strings.ToString(Text == "Colorado" ? " selected=\"selected\"" : string.Empty) + ">Colorado</option>");
                output.WriteLine("<option value=\"Connecticut\"" + Strings.ToString(Text == "Connecticut" ? " selected=\"selected\"" : string.Empty) + ">Connecticut</option>");
                output.WriteLine("<option value=\"Delaware\"" + Strings.ToString(Text == "Delaware" ? " selected=\"selected\"" : string.Empty) + ">Delaware</option>");
                output.WriteLine("<option value=\"District of Columbia\"" + Strings.ToString(Text == "District of Columbia" ? " selected=\"selected\"" : string.Empty) + ">District of Columbia</option>");
                output.WriteLine("<option value=\"Florida\"" + Strings.ToString(Text == "Florida" ? " selected=\"selected\"" : string.Empty) + ">Florida</option>");
                output.WriteLine("<option value=\"Georgia\"" + Strings.ToString(Text == "Georgia" ? " selected=\"selected\"" : string.Empty) + ">Georgia</option>");
                output.WriteLine("<option value=\"Hawaii\"" + Strings.ToString(Text == "Hawaii" ? " selected=\"selected\"" : string.Empty) + ">Hawaii</option>");
                output.WriteLine("<option value=\"Idaho\"" + Strings.ToString(Text == "Idaho" ? " selected=\"selected\"" : string.Empty) + ">Idaho</option>");
                output.WriteLine("<option value=\"Illinois\"" + Strings.ToString(Text == "Illinois" ? " selected=\"selected\"" : string.Empty) + ">Illinois</option>");
                output.WriteLine("<option value=\"Indiana\"" + Strings.ToString(Text == "Indiana" ? " selected=\"selected\"" : string.Empty) + ">Indiana</option>");
                output.WriteLine("<option value=\"Iowa\"" + Strings.ToString(Text == "Iowa" ? " selected=\"selected\"" : string.Empty) + ">Iowa</option>");
                output.WriteLine("<option value=\"Kansas\"" + Strings.ToString(Text == "Kansas" ? " selected=\"selected\"" : string.Empty) + ">Kansas</option>");
                output.WriteLine("<option value=\"Kentucky\"" + Strings.ToString(Text == "Kentucky" ? " selected=\"selected\"" : string.Empty) + ">Kentucky</option>");
                output.WriteLine("<option value=\"Louisiana\"" + Strings.ToString(Text == "Louisiana" ? " selected=\"selected\"" : string.Empty) + ">Louisiana</option>");
                output.WriteLine("<option value=\"Maine\"" + Strings.ToString(Text == "Maine" ? " selected=\"selected\"" : string.Empty) + ">Maine</option>");
                output.WriteLine("<option value=\"Maryland\"" + Strings.ToString(Text == "Maryland" ? " selected=\"selected\"" : string.Empty) + ">Maryland</option>");
                output.WriteLine("<option value=\"Massachusetts\"" + Strings.ToString(Text == "Massachusetts" ? " selected=\"selected\"" : string.Empty) + ">Massachusetts</option>");
                output.WriteLine("<option value=\"Michigan\"" + Strings.ToString(Text == "Michigan" ? " selected=\"selected\"" : string.Empty) + ">Michigan</option>");
                output.WriteLine("<option value=\"Minnesota\"" + Strings.ToString(Text == "Minnesota" ? " selected=\"selected\"" : string.Empty) + ">Minnesota</option>");
                output.WriteLine("<option value=\"Mississippi\"" + Strings.ToString(Text == "Mississippi" ? " selected=\"selected\"" : string.Empty) + ">Mississippi</option>");
                output.WriteLine("<option value=\"Missouri\"" + Strings.ToString(Text == "Missouri" ? " selected=\"selected\"" : string.Empty) + ">Missouri</option>");
                output.WriteLine("<option value=\"Montana\"" + Strings.ToString(Text == "Montana" ? " selected=\"selected\"" : string.Empty) + ">Montana</option>");
                output.WriteLine("<option value=\"Nebraska\"" + Strings.ToString(Text == "Nebraska" ? " selected=\"selected\"" : string.Empty) + ">Nebraska</option>");
                output.WriteLine("<option value=\"Nevada\"" + Strings.ToString(Text == "Nevada" ? " selected=\"selected\"" : string.Empty) + ">Nevada</option>");
                output.WriteLine("<option value=\"New Hampshire\"" + Strings.ToString(Text == "New Hampshire" ? " selected=\"selected\"" : string.Empty) + ">New Hampshire</option>");
                output.WriteLine("<option value=\"New Jersey\"" + Strings.ToString(Text == "New Jersey" ? " selected=\"selected\"" : string.Empty) + ">New Jersey</option>");
                output.WriteLine("<option value=\"New Mexico\"" + Strings.ToString(Text == "New Mexico" ? " selected=\"selected\"" : string.Empty) + ">New Mexico</option>");
                output.WriteLine("<option value=\"New York\"" + Strings.ToString(Text == "New York" ? " selected=\"selected\"" : string.Empty) + ">New York</option>");
                output.WriteLine("<option value=\"North Carolina\"" + Strings.ToString(Text == "North Carolina" ? " selected=\"selected\"" : string.Empty) + ">North Carolina</option>");
                output.WriteLine("<option value=\"North Dakota\"" + Strings.ToString(Text == "North Dakota" ? " selected=\"selected\"" : string.Empty) + ">North Dakota</option>");
                output.WriteLine("<option value=\"Ohio\"" + Strings.ToString(Text == "Ohio" ? " selected=\"selected\"" : string.Empty) + ">Ohio</option>");
                output.WriteLine("<option value=\"Oklahoma\"" + Strings.ToString(Text == "Oklahoma" ? " selected=\"selected\"" : string.Empty) + ">Oklahoma</option>");
                output.WriteLine("<option value=\"Oregon\"" + Strings.ToString(Text == "Oregon" ? " selected=\"selected\"" : string.Empty) + ">Oregon</option>");
                output.WriteLine("<option value=\"Pennsylvania\"" + Strings.ToString(Text == "Pennsylvania" ? " selected=\"selected\"" : string.Empty) + ">Pennsylvania</option>");
                output.WriteLine("<option value=\"Rhode Island\"" + Strings.ToString(Text == "Rhode Island" ? " selected=\"selected\"" : string.Empty) + ">Rhode Island</option>");
                output.WriteLine("<option value=\"South Carolina\"" + Strings.ToString(Text == "South Carolina" ? " selected=\"selected\"" : string.Empty) + ">South Carolina</option>");
                output.WriteLine("<option value=\"South Dakota\"" + Strings.ToString(Text == "South Dakota" ? " selected=\"selected\"" : string.Empty) + ">South Dakota</option>");
                output.WriteLine("<option value=\"Tennessee\"" + Strings.ToString(Text == "Tennessee" ? " selected=\"selected\"" : string.Empty) + ">Tennessee</option>");
                output.WriteLine("<option value=\"Texas\"" + Strings.ToString(Text == "Texas" ? " selected=\"selected\"" : string.Empty) + ">Texas</option>");
                output.WriteLine("<option value=\"Utah\"" + Strings.ToString(Text == "Utah" ? " selected=\"selected\"" : string.Empty) + ">Utah</option>");
                output.WriteLine("<option value=\"Vermont\"" + Strings.ToString(Text == "Vermont" ? " selected=\"selected\"" : string.Empty) + ">Vermont</option>");
                output.WriteLine("<option value=\"Virginia\"" + Strings.ToString(Text == "Virginia" ? " selected=\"selected\"" : string.Empty) + ">Virginia</option>");
                output.WriteLine("<option value=\"Washington\"" + Strings.ToString(Text == "Washington" ? " selected=\"selected\"" : string.Empty) + ">Washington</option>");
                output.WriteLine("<option value=\"West Virginia\"" + Strings.ToString(Text == "West Virginia" ? " selected=\"selected\"" : string.Empty) + ">West Virginia</option>");
                output.WriteLine("<option value=\"Wisconsin\"" + Strings.ToString(Text == "Wisconsin" ? " selected=\"selected\"" : string.Empty) + ">Wisconsin</option>");
                output.WriteLine("<option value=\"Wyoming\"" + Strings.ToString(Text == "Wyoming" ? " selected=\"selected\"" : string.Empty) + ">Wyoming</option>");
            }

            output.WriteLine("</select>");
        }
    }
}