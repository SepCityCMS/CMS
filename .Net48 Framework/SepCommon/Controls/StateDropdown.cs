// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="StateDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class StateDropdown.
    /// </summary>
    public class StateDropdown
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
            get => SepCommon.SepCore.Strings.ToString(m_Country);

            set => m_Country = value;
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StateDropdown"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

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
            get => SepCommon.SepCore.Strings.ToString(m_SelectWidth);

            set => m_SelectWidth = value;
        }

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

            output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\"" + sDisabled + sWidth + ">");
            output.AppendLine("<option value=\"\">" + SepFunctions.LangText("--- Select a State/Province ---") + "</option>");
            try
            {
                ProvinceInCountry Provinces = SepFunctions.ProvincesInCountry(Country);
                for (var i = 0; i < Provinces.Provinces.Count; i++)
                {
                    output.AppendLine("<option value=\"" + Provinces.Provinces[i] + "\"" + SepCommon.SepCore.Strings.ToString(Text == Provinces.Provinces[i] ? " selected=\"selected\"" : string.Empty) + ">" + Provinces.Provinces[i] + "</option>");
                }
            }
            catch
            {
                output.AppendLine("<option value=\"Alabama\"" + SepCommon.SepCore.Strings.ToString(Text == "Alabama" ? " selected=\"selected\"" : string.Empty) + ">Alabama</option>");
                output.AppendLine("<option value=\"Alaska\"" + SepCommon.SepCore.Strings.ToString(Text == "Alaska" ? " selected=\"selected\"" : string.Empty) + ">Alaska</option>");
                output.AppendLine("<option value=\"Arizona\"" + SepCommon.SepCore.Strings.ToString(Text == "Arizona" ? " selected=\"selected\"" : string.Empty) + ">Arizona</option>");
                output.AppendLine("<option value=\"Arkansas\"" + SepCommon.SepCore.Strings.ToString(Text == "Arkansas" ? " selected=\"selected\"" : string.Empty) + ">Arkansas</option>");
                output.AppendLine("<option value=\"California\"" + SepCommon.SepCore.Strings.ToString(Text == "California" ? " selected=\"selected\"" : string.Empty) + ">California</option>");
                output.AppendLine("<option value=\"Colorado\"" + SepCommon.SepCore.Strings.ToString(Text == "Colorado" ? " selected=\"selected\"" : string.Empty) + ">Colorado</option>");
                output.AppendLine("<option value=\"Connecticut\"" + SepCommon.SepCore.Strings.ToString(Text == "Connecticut" ? " selected=\"selected\"" : string.Empty) + ">Connecticut</option>");
                output.AppendLine("<option value=\"Delaware\"" + SepCommon.SepCore.Strings.ToString(Text == "Delaware" ? " selected=\"selected\"" : string.Empty) + ">Delaware</option>");
                output.AppendLine("<option value=\"District of Columbia\"" + SepCommon.SepCore.Strings.ToString(Text == "District of Columbia" ? " selected=\"selected\"" : string.Empty) + ">District of Columbia</option>");
                output.AppendLine("<option value=\"Florida\"" + SepCommon.SepCore.Strings.ToString(Text == "Florida" ? " selected=\"selected\"" : string.Empty) + ">Florida</option>");
                output.AppendLine("<option value=\"Georgia\"" + SepCommon.SepCore.Strings.ToString(Text == "Georgia" ? " selected=\"selected\"" : string.Empty) + ">Georgia</option>");
                output.AppendLine("<option value=\"Hawaii\"" + SepCommon.SepCore.Strings.ToString(Text == "Hawaii" ? " selected=\"selected\"" : string.Empty) + ">Hawaii</option>");
                output.AppendLine("<option value=\"Idaho\"" + SepCommon.SepCore.Strings.ToString(Text == "Idaho" ? " selected=\"selected\"" : string.Empty) + ">Idaho</option>");
                output.AppendLine("<option value=\"Illinois\"" + SepCommon.SepCore.Strings.ToString(Text == "Illinois" ? " selected=\"selected\"" : string.Empty) + ">Illinois</option>");
                output.AppendLine("<option value=\"Indiana\"" + SepCommon.SepCore.Strings.ToString(Text == "Indiana" ? " selected=\"selected\"" : string.Empty) + ">Indiana</option>");
                output.AppendLine("<option value=\"Iowa\"" + SepCommon.SepCore.Strings.ToString(Text == "Iowa" ? " selected=\"selected\"" : string.Empty) + ">Iowa</option>");
                output.AppendLine("<option value=\"Kansas\"" + SepCommon.SepCore.Strings.ToString(Text == "Kansas" ? " selected=\"selected\"" : string.Empty) + ">Kansas</option>");
                output.AppendLine("<option value=\"Kentucky\"" + SepCommon.SepCore.Strings.ToString(Text == "Kentucky" ? " selected=\"selected\"" : string.Empty) + ">Kentucky</option>");
                output.AppendLine("<option value=\"Louisiana\"" + SepCommon.SepCore.Strings.ToString(Text == "Louisiana" ? " selected=\"selected\"" : string.Empty) + ">Louisiana</option>");
                output.AppendLine("<option value=\"Maine\"" + SepCommon.SepCore.Strings.ToString(Text == "Maine" ? " selected=\"selected\"" : string.Empty) + ">Maine</option>");
                output.AppendLine("<option value=\"Maryland\"" + SepCommon.SepCore.Strings.ToString(Text == "Maryland" ? " selected=\"selected\"" : string.Empty) + ">Maryland</option>");
                output.AppendLine("<option value=\"Massachusetts\"" + SepCommon.SepCore.Strings.ToString(Text == "Massachusetts" ? " selected=\"selected\"" : string.Empty) + ">Massachusetts</option>");
                output.AppendLine("<option value=\"Michigan\"" + SepCommon.SepCore.Strings.ToString(Text == "Michigan" ? " selected=\"selected\"" : string.Empty) + ">Michigan</option>");
                output.AppendLine("<option value=\"Minnesota\"" + SepCommon.SepCore.Strings.ToString(Text == "Minnesota" ? " selected=\"selected\"" : string.Empty) + ">Minnesota</option>");
                output.AppendLine("<option value=\"Mississippi\"" + SepCommon.SepCore.Strings.ToString(Text == "Mississippi" ? " selected=\"selected\"" : string.Empty) + ">Mississippi</option>");
                output.AppendLine("<option value=\"Missouri\"" + SepCommon.SepCore.Strings.ToString(Text == "Missouri" ? " selected=\"selected\"" : string.Empty) + ">Missouri</option>");
                output.AppendLine("<option value=\"Montana\"" + SepCommon.SepCore.Strings.ToString(Text == "Montana" ? " selected=\"selected\"" : string.Empty) + ">Montana</option>");
                output.AppendLine("<option value=\"Nebraska\"" + SepCommon.SepCore.Strings.ToString(Text == "Nebraska" ? " selected=\"selected\"" : string.Empty) + ">Nebraska</option>");
                output.AppendLine("<option value=\"Nevada\"" + SepCommon.SepCore.Strings.ToString(Text == "Nevada" ? " selected=\"selected\"" : string.Empty) + ">Nevada</option>");
                output.AppendLine("<option value=\"New Hampshire\"" + SepCommon.SepCore.Strings.ToString(Text == "New Hampshire" ? " selected=\"selected\"" : string.Empty) + ">New Hampshire</option>");
                output.AppendLine("<option value=\"New Jersey\"" + SepCommon.SepCore.Strings.ToString(Text == "New Jersey" ? " selected=\"selected\"" : string.Empty) + ">New Jersey</option>");
                output.AppendLine("<option value=\"New Mexico\"" + SepCommon.SepCore.Strings.ToString(Text == "New Mexico" ? " selected=\"selected\"" : string.Empty) + ">New Mexico</option>");
                output.AppendLine("<option value=\"New York\"" + SepCommon.SepCore.Strings.ToString(Text == "New York" ? " selected=\"selected\"" : string.Empty) + ">New York</option>");
                output.AppendLine("<option value=\"North Carolina\"" + SepCommon.SepCore.Strings.ToString(Text == "North Carolina" ? " selected=\"selected\"" : string.Empty) + ">North Carolina</option>");
                output.AppendLine("<option value=\"North Dakota\"" + SepCommon.SepCore.Strings.ToString(Text == "North Dakota" ? " selected=\"selected\"" : string.Empty) + ">North Dakota</option>");
                output.AppendLine("<option value=\"Ohio\"" + SepCommon.SepCore.Strings.ToString(Text == "Ohio" ? " selected=\"selected\"" : string.Empty) + ">Ohio</option>");
                output.AppendLine("<option value=\"Oklahoma\"" + SepCommon.SepCore.Strings.ToString(Text == "Oklahoma" ? " selected=\"selected\"" : string.Empty) + ">Oklahoma</option>");
                output.AppendLine("<option value=\"Oregon\"" + SepCommon.SepCore.Strings.ToString(Text == "Oregon" ? " selected=\"selected\"" : string.Empty) + ">Oregon</option>");
                output.AppendLine("<option value=\"Pennsylvania\"" + SepCommon.SepCore.Strings.ToString(Text == "Pennsylvania" ? " selected=\"selected\"" : string.Empty) + ">Pennsylvania</option>");
                output.AppendLine("<option value=\"Rhode Island\"" + SepCommon.SepCore.Strings.ToString(Text == "Rhode Island" ? " selected=\"selected\"" : string.Empty) + ">Rhode Island</option>");
                output.AppendLine("<option value=\"South Carolina\"" + SepCommon.SepCore.Strings.ToString(Text == "South Carolina" ? " selected=\"selected\"" : string.Empty) + ">South Carolina</option>");
                output.AppendLine("<option value=\"South Dakota\"" + SepCommon.SepCore.Strings.ToString(Text == "South Dakota" ? " selected=\"selected\"" : string.Empty) + ">South Dakota</option>");
                output.AppendLine("<option value=\"Tennessee\"" + SepCommon.SepCore.Strings.ToString(Text == "Tennessee" ? " selected=\"selected\"" : string.Empty) + ">Tennessee</option>");
                output.AppendLine("<option value=\"Texas\"" + SepCommon.SepCore.Strings.ToString(Text == "Texas" ? " selected=\"selected\"" : string.Empty) + ">Texas</option>");
                output.AppendLine("<option value=\"Utah\"" + SepCommon.SepCore.Strings.ToString(Text == "Utah" ? " selected=\"selected\"" : string.Empty) + ">Utah</option>");
                output.AppendLine("<option value=\"Vermont\"" + SepCommon.SepCore.Strings.ToString(Text == "Vermont" ? " selected=\"selected\"" : string.Empty) + ">Vermont</option>");
                output.AppendLine("<option value=\"Virginia\"" + SepCommon.SepCore.Strings.ToString(Text == "Virginia" ? " selected=\"selected\"" : string.Empty) + ">Virginia</option>");
                output.AppendLine("<option value=\"Washington\"" + SepCommon.SepCore.Strings.ToString(Text == "Washington" ? " selected=\"selected\"" : string.Empty) + ">Washington</option>");
                output.AppendLine("<option value=\"West Virginia\"" + SepCommon.SepCore.Strings.ToString(Text == "West Virginia" ? " selected=\"selected\"" : string.Empty) + ">West Virginia</option>");
                output.AppendLine("<option value=\"Wisconsin\"" + SepCommon.SepCore.Strings.ToString(Text == "Wisconsin" ? " selected=\"selected\"" : string.Empty) + ">Wisconsin</option>");
                output.AppendLine("<option value=\"Wyoming\"" + SepCommon.SepCore.Strings.ToString(Text == "Wyoming" ? " selected=\"selected\"" : string.Empty) + ">Wyoming</option>");
            }

            output.AppendLine("</select>");
            return output.ToString();
        }
    }
}