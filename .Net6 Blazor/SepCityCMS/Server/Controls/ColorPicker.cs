// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ColorPicker.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Text;

    /// <summary>
    /// Class ColorPicker.
    /// </summary>
    public class ColorPicker
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
                    return string.Empty;
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

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var cUrl = sImageFolder + "js/jquery/jquery.colourPicker.js";

            output.AppendLine("<script type=\"text/javascript\" src=\"" + cUrl + "\">");

            var AddCssClass = !string.IsNullOrWhiteSpace(CssClass) ? " " + CssClass : string.Empty;

            output.AppendLine("<link href=\"" + sImageFolder + "js/jquery/jquery.colourPicker.css\" rel=\"stylesheet\" type=\"text/css\" />");

            output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"ignore" + AddCssClass + "\">");
            output.AppendLine("<option value=\"\">" + SepFunctions.LangText("Select Color") + "</option>");
            output.AppendLine("<option value=\"ffffff\"" + SepCore.Strings.ToString(Text == "ffffff" ? " selected=\"selected\"" : string.Empty) + ">#ffffff</option>");
            output.AppendLine("<option value=\"ffccc9\"" + SepCore.Strings.ToString(Text == "ffccc9" ? " selected=\"selected\"" : string.Empty) + ">#ffccc9</option>");
            output.AppendLine("<option value=\"ffce93\"" + SepCore.Strings.ToString(Text == "ffce93" ? " selected=\"selected\"" : string.Empty) + ">#ffce93</option>");
            output.AppendLine("<option value=\"fffc9e\"" + SepCore.Strings.ToString(Text == "fffc9e" ? " selected=\"selected\"" : string.Empty) + ">#fffc9e</option>");
            output.AppendLine("<option value=\"ffffc7\"" + SepCore.Strings.ToString(Text == "ffffc7" ? " selected=\"selected\"" : string.Empty) + ">#ffffc7</option>");
            output.AppendLine("<option value=\"9aff99\"" + SepCore.Strings.ToString(Text == "9aff99" ? " selected=\"selected\"" : string.Empty) + ">#9aff99</option>");
            output.AppendLine("<option value=\"96fffb\"" + SepCore.Strings.ToString(Text == "96fffb" ? " selected=\"selected\"" : string.Empty) + ">#96fffb</option>");
            output.AppendLine("<option value=\"cdffff\"" + SepCore.Strings.ToString(Text == "cdffff" ? " selected=\"selected\"" : string.Empty) + ">#cdffff</option>");
            output.AppendLine("<option value=\"cbcefb\"" + SepCore.Strings.ToString(Text == "cbcefb" ? " selected=\"selected\"" : string.Empty) + ">#cbcefb</option>");
            output.AppendLine("<option value=\"cfcfcf\"" + SepCore.Strings.ToString(Text == "cfcfcf" ? " selected=\"selected\"" : string.Empty) + ">#cfcfcf</option>");
            output.AppendLine("<option value=\"fd6864\"" + SepCore.Strings.ToString(Text == "fd6864" ? " selected=\"selected\"" : string.Empty) + ">#fd6864</option>");
            output.AppendLine("<option value=\"fe996b\"" + SepCore.Strings.ToString(Text == "fe996b" ? " selected=\"selected\"" : string.Empty) + ">#fe996b</option>");
            output.AppendLine("<option value=\"fffe65\"" + SepCore.Strings.ToString(Text == "fffe65" ? " selected=\"selected\"" : string.Empty) + ">#fffe65</option>");
            output.AppendLine("<option value=\"fcff2f\"" + SepCore.Strings.ToString(Text == "fcff2f" ? " selected=\"selected\"" : string.Empty) + ">#fcff2f</option>");
            output.AppendLine("<option value=\"67fd9a\"" + SepCore.Strings.ToString(Text == "67fd9a" ? " selected=\"selected\"" : string.Empty) + ">#67fd9a</option>");
            output.AppendLine("<option value=\"38fff8\"" + SepCore.Strings.ToString(Text == "38fff8" ? " selected=\"selected\"" : string.Empty) + ">#38fff8</option>");
            output.AppendLine("<option value=\"68fdff\"" + SepCore.Strings.ToString(Text == "68fdff" ? " selected=\"selected\"" : string.Empty) + ">#68fdff</option>");
            output.AppendLine("<option value=\"9698ed\"" + SepCore.Strings.ToString(Text == "9698ed" ? " selected=\"selected\"" : string.Empty) + ">#9698ed</option>");
            output.AppendLine("<option value=\"c0c0c0\"" + SepCore.Strings.ToString(Text == "c0c0c0" ? " selected=\"selected\"" : string.Empty) + ">#c0c0c0</option>");
            output.AppendLine("<option value=\"fe0000\"" + SepCore.Strings.ToString(Text == "fe0000" ? " selected=\"selected\"" : string.Empty) + ">#fe0000</option>");
            output.AppendLine("<option value=\"f8a102\"" + SepCore.Strings.ToString(Text == "f8a102" ? " selected=\"selected\"" : string.Empty) + ">#f8a102</option>");
            output.AppendLine("<option value=\"ffcc67\"" + SepCore.Strings.ToString(Text == "ffcc67" ? " selected=\"selected\"" : string.Empty) + ">#ffcc67</option>");
            output.AppendLine("<option value=\"f8ff00\"" + SepCore.Strings.ToString(Text == "f8ff00" ? " selected=\"selected\"" : string.Empty) + ">#f8ff00</option>");
            output.AppendLine("<option value=\"34ff34\"" + SepCore.Strings.ToString(Text == "34ff34" ? " selected=\"selected\"" : string.Empty) + ">#34ff34</option>");
            output.AppendLine("<option value=\"68cbd0\"" + SepCore.Strings.ToString(Text == "68cbd0" ? " selected=\"selected\"" : string.Empty) + ">#68cbd0</option>");
            output.AppendLine("<option value=\"34cdf9\"" + SepCore.Strings.ToString(Text == "34cdf9" ? " selected=\"selected\"" : string.Empty) + ">#34cdf9</option>");
            output.AppendLine("<option value=\"6665cd\"" + SepCore.Strings.ToString(Text == "6665cd" ? " selected=\"selected\"" : string.Empty) + ">#6665cd</option>");
            output.AppendLine("<option value=\"9b9b9b\"" + SepCore.Strings.ToString(Text == "9b9b9b" ? " selected=\"selected\"" : string.Empty) + ">#9b9b9b</option>");
            output.AppendLine("<option value=\"cb0000\"" + SepCore.Strings.ToString(Text == "cb0000" ? " selected=\"selected\"" : string.Empty) + ">#cb0000</option>");
            output.AppendLine("<option value=\"f56b00\"" + SepCore.Strings.ToString(Text == "f56b00" ? " selected=\"selected\"" : string.Empty) + ">#f56b00</option>");
            output.AppendLine("<option value=\"ffcb2f\"" + SepCore.Strings.ToString(Text == "ffcb2f" ? " selected=\"selected\"" : string.Empty) + ">#ffcb2f</option>");
            output.AppendLine("<option value=\"ffc702\"" + SepCore.Strings.ToString(Text == "ffc702" ? " selected=\"selected\"" : string.Empty) + ">#ffc702</option>");
            output.AppendLine("<option value=\"32cb00\"" + SepCore.Strings.ToString(Text == "32cb00" ? " selected=\"selected\"" : string.Empty) + ">#32cb00</option>");
            output.AppendLine("<option value=\"00d2cb\"" + SepCore.Strings.ToString(Text == "00d2cb" ? " selected=\"selected\"" : string.Empty) + ">#00d2cb</option>");
            output.AppendLine("<option value=\"3166ff\"" + SepCore.Strings.ToString(Text == "3166ff" ? " selected=\"selected\"" : string.Empty) + ">#3166ff</option>");
            output.AppendLine("<option value=\"6434fc\"" + SepCore.Strings.ToString(Text == "6434fc" ? " selected=\"selected\"" : string.Empty) + ">#6434fc</option>");
            output.AppendLine("<option value=\"656565\"" + SepCore.Strings.ToString(Text == "656565" ? " selected=\"selected\"" : string.Empty) + ">#656565</option>");
            output.AppendLine("<option value=\"9a0000\"" + SepCore.Strings.ToString(Text == "9a0000" ? " selected=\"selected\"" : string.Empty) + ">#9a0000</option>");
            output.AppendLine("<option value=\"ce6301\"" + SepCore.Strings.ToString(Text == "ce6301" ? " selected=\"selected\"" : string.Empty) + ">#ce6301</option>");
            output.AppendLine("<option value=\"cd9934\"" + SepCore.Strings.ToString(Text == "cd9934" ? " selected=\"selected\"" : string.Empty) + ">#cd9934</option>");
            output.AppendLine("<option value=\"999903\"" + SepCore.Strings.ToString(Text == "999903" ? " selected=\"selected\"" : string.Empty) + ">#999903</option>");
            output.AppendLine("<option value=\"009901\"" + SepCore.Strings.ToString(Text == "009901" ? " selected=\"selected\"" : string.Empty) + ">#009901</option>");
            output.AppendLine("<option value=\"329a9d\"" + SepCore.Strings.ToString(Text == "329a9d" ? " selected=\"selected\"" : string.Empty) + ">#329a9d</option>");
            output.AppendLine("<option value=\"3531ff\"" + SepCore.Strings.ToString(Text == "3531ff" ? " selected=\"selected\"" : string.Empty) + ">#3531ff</option>");
            output.AppendLine("<option value=\"6200c9\"" + SepCore.Strings.ToString(Text == "6200c9" ? " selected=\"selected\"" : string.Empty) + ">#6200c9</option>");
            output.AppendLine("<option value=\"343434\"" + SepCore.Strings.ToString(Text == "343434" ? " selected=\"selected\"" : string.Empty) + ">#343434</option>");
            output.AppendLine("<option value=\"680100\"" + SepCore.Strings.ToString(Text == "680100" ? " selected=\"selected\"" : string.Empty) + ">#680100</option>");
            output.AppendLine("<option value=\"963400\"" + SepCore.Strings.ToString(Text == "963400" ? " selected=\"selected\"" : string.Empty) + ">#963400</option>");
            output.AppendLine("<option value=\"986536\"" + SepCore.Strings.ToString(Text == "986536" ? " selected=\"selected\"" : string.Empty) + ">#986536</option>");
            output.AppendLine("<option value=\"646809\"" + SepCore.Strings.ToString(Text == "646809" ? " selected=\"selected\"" : string.Empty) + ">#646809</option>");
            output.AppendLine("<option value=\"036400\"" + SepCore.Strings.ToString(Text == "036400" ? " selected=\"selected\"" : string.Empty) + ">#036400</option>");
            output.AppendLine("<option value=\"34696d\"" + SepCore.Strings.ToString(Text == "34696d" ? " selected=\"selected\"" : string.Empty) + ">#34696d</option>");
            output.AppendLine("<option value=\"00009b\"" + SepCore.Strings.ToString(Text == "00009b" ? " selected=\"selected\"" : string.Empty) + ">#00009b</option>");
            output.AppendLine("<option value=\"303498\"" + SepCore.Strings.ToString(Text == "303498" ? " selected=\"selected\"" : string.Empty) + ">#303498</option>");
            output.AppendLine("<option value=\"000000\"" + SepCore.Strings.ToString(Text == "000000" ? " selected=\"selected\"" : string.Empty) + ">#000000</option>");
            output.AppendLine("<option value=\"330001\"" + SepCore.Strings.ToString(Text == "330001" ? " selected=\"selected\"" : string.Empty) + ">#330001</option>");
            output.AppendLine("<option value=\"643403\"" + SepCore.Strings.ToString(Text == "643403" ? " selected=\"selected\"" : string.Empty) + ">#643403</option>");
            output.AppendLine("<option value=\"663234\"" + SepCore.Strings.ToString(Text == "663234" ? " selected=\"selected\"" : string.Empty) + ">#663234</option>");
            output.AppendLine("<option value=\"343300\"" + SepCore.Strings.ToString(Text == "343300" ? " selected=\"selected\"" : string.Empty) + ">#343300</option>");
            output.AppendLine("<option value=\"013300\"" + SepCore.Strings.ToString(Text == "013300" ? " selected=\"selected\"" : string.Empty) + ">#013300</option>");
            output.AppendLine("<option value=\"003532\"" + SepCore.Strings.ToString(Text == "003532" ? " selected=\"selected\"" : string.Empty) + ">#003532</option>");
            output.AppendLine("<option value=\"010066\"" + SepCore.Strings.ToString(Text == "010066" ? " selected=\"selected\"" : string.Empty) + ">#010066</option>");
            output.AppendLine("<option value=\"340096\"" + SepCore.Strings.ToString(Text == "340096" ? " selected=\"selected\"" : string.Empty) + ">#340096</option>");
            output.AppendLine("</select>");

            output.AppendLine("<script type=\"text/javascript\">");
            output.AppendLine("$('#" + ID + "').colourPicker({ico: '" + sImageFolder + "js/jquery/images/jquery.colourPicker.gif'});");
            output.AppendLine("</script>");
            return output.ToString();
        }
    }
}