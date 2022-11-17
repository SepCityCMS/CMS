// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="ColorPicker.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class ColorPicker.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ColorPicker runat=server></{0}:ColorPicker>")]
    public class ColorPicker : WebControl
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
                    return string.Empty;
                }

                return s;
            }

            set => Text1 = value;
        }

        public string Text1 { get => m_Text; set => m_Text = value; }

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
        /// Raises the <see cref="System.Web.UI.Control.PreRender" /> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs" /> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var cName = "catSelectionJS";
            var cUrl = sImageFolder + "js/jquery/jquery.colourPicker.js";
            var csType = GetType();

            var cs = Page.ClientScript;

            if (!cs.IsClientScriptIncludeRegistered(csType, cName))
            {
                cs.RegisterClientScriptInclude(csType, cName, ResolveClientUrl(cUrl));
            }
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            var AddCssClass = !string.IsNullOrWhiteSpace(CssClass) ? " " + CssClass : string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            output.WriteLine("<link href=\"" + sImageFolder + "js/jquery/jquery.colourPicker.css\" rel=\"stylesheet\" type=\"text/css\" />");

            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"ignore" + AddCssClass + "\">");
            output.WriteLine("<option value=\"\">" + SepFunctions.LangText("Select Color") + "</option>");
            output.WriteLine("<option value=\"ffffff\"" + Strings.ToString(Text == "ffffff" ? " selected=\"selected\"" : string.Empty) + ">#ffffff</option>");
            output.WriteLine("<option value=\"ffccc9\"" + Strings.ToString(Text == "ffccc9" ? " selected=\"selected\"" : string.Empty) + ">#ffccc9</option>");
            output.WriteLine("<option value=\"ffce93\"" + Strings.ToString(Text == "ffce93" ? " selected=\"selected\"" : string.Empty) + ">#ffce93</option>");
            output.WriteLine("<option value=\"fffc9e\"" + Strings.ToString(Text == "fffc9e" ? " selected=\"selected\"" : string.Empty) + ">#fffc9e</option>");
            output.WriteLine("<option value=\"ffffc7\"" + Strings.ToString(Text == "ffffc7" ? " selected=\"selected\"" : string.Empty) + ">#ffffc7</option>");
            output.WriteLine("<option value=\"9aff99\"" + Strings.ToString(Text == "9aff99" ? " selected=\"selected\"" : string.Empty) + ">#9aff99</option>");
            output.WriteLine("<option value=\"96fffb\"" + Strings.ToString(Text == "96fffb" ? " selected=\"selected\"" : string.Empty) + ">#96fffb</option>");
            output.WriteLine("<option value=\"cdffff\"" + Strings.ToString(Text == "cdffff" ? " selected=\"selected\"" : string.Empty) + ">#cdffff</option>");
            output.WriteLine("<option value=\"cbcefb\"" + Strings.ToString(Text == "cbcefb" ? " selected=\"selected\"" : string.Empty) + ">#cbcefb</option>");
            output.WriteLine("<option value=\"cfcfcf\"" + Strings.ToString(Text == "cfcfcf" ? " selected=\"selected\"" : string.Empty) + ">#cfcfcf</option>");
            output.WriteLine("<option value=\"fd6864\"" + Strings.ToString(Text == "fd6864" ? " selected=\"selected\"" : string.Empty) + ">#fd6864</option>");
            output.WriteLine("<option value=\"fe996b\"" + Strings.ToString(Text == "fe996b" ? " selected=\"selected\"" : string.Empty) + ">#fe996b</option>");
            output.WriteLine("<option value=\"fffe65\"" + Strings.ToString(Text == "fffe65" ? " selected=\"selected\"" : string.Empty) + ">#fffe65</option>");
            output.WriteLine("<option value=\"fcff2f\"" + Strings.ToString(Text == "fcff2f" ? " selected=\"selected\"" : string.Empty) + ">#fcff2f</option>");
            output.WriteLine("<option value=\"67fd9a\"" + Strings.ToString(Text == "67fd9a" ? " selected=\"selected\"" : string.Empty) + ">#67fd9a</option>");
            output.WriteLine("<option value=\"38fff8\"" + Strings.ToString(Text == "38fff8" ? " selected=\"selected\"" : string.Empty) + ">#38fff8</option>");
            output.WriteLine("<option value=\"68fdff\"" + Strings.ToString(Text == "68fdff" ? " selected=\"selected\"" : string.Empty) + ">#68fdff</option>");
            output.WriteLine("<option value=\"9698ed\"" + Strings.ToString(Text == "9698ed" ? " selected=\"selected\"" : string.Empty) + ">#9698ed</option>");
            output.WriteLine("<option value=\"c0c0c0\"" + Strings.ToString(Text == "c0c0c0" ? " selected=\"selected\"" : string.Empty) + ">#c0c0c0</option>");
            output.WriteLine("<option value=\"fe0000\"" + Strings.ToString(Text == "fe0000" ? " selected=\"selected\"" : string.Empty) + ">#fe0000</option>");
            output.WriteLine("<option value=\"f8a102\"" + Strings.ToString(Text == "f8a102" ? " selected=\"selected\"" : string.Empty) + ">#f8a102</option>");
            output.WriteLine("<option value=\"ffcc67\"" + Strings.ToString(Text == "ffcc67" ? " selected=\"selected\"" : string.Empty) + ">#ffcc67</option>");
            output.WriteLine("<option value=\"f8ff00\"" + Strings.ToString(Text == "f8ff00" ? " selected=\"selected\"" : string.Empty) + ">#f8ff00</option>");
            output.WriteLine("<option value=\"34ff34\"" + Strings.ToString(Text == "34ff34" ? " selected=\"selected\"" : string.Empty) + ">#34ff34</option>");
            output.WriteLine("<option value=\"68cbd0\"" + Strings.ToString(Text == "68cbd0" ? " selected=\"selected\"" : string.Empty) + ">#68cbd0</option>");
            output.WriteLine("<option value=\"34cdf9\"" + Strings.ToString(Text == "34cdf9" ? " selected=\"selected\"" : string.Empty) + ">#34cdf9</option>");
            output.WriteLine("<option value=\"6665cd\"" + Strings.ToString(Text == "6665cd" ? " selected=\"selected\"" : string.Empty) + ">#6665cd</option>");
            output.WriteLine("<option value=\"9b9b9b\"" + Strings.ToString(Text == "9b9b9b" ? " selected=\"selected\"" : string.Empty) + ">#9b9b9b</option>");
            output.WriteLine("<option value=\"cb0000\"" + Strings.ToString(Text == "cb0000" ? " selected=\"selected\"" : string.Empty) + ">#cb0000</option>");
            output.WriteLine("<option value=\"f56b00\"" + Strings.ToString(Text == "f56b00" ? " selected=\"selected\"" : string.Empty) + ">#f56b00</option>");
            output.WriteLine("<option value=\"ffcb2f\"" + Strings.ToString(Text == "ffcb2f" ? " selected=\"selected\"" : string.Empty) + ">#ffcb2f</option>");
            output.WriteLine("<option value=\"ffc702\"" + Strings.ToString(Text == "ffc702" ? " selected=\"selected\"" : string.Empty) + ">#ffc702</option>");
            output.WriteLine("<option value=\"32cb00\"" + Strings.ToString(Text == "32cb00" ? " selected=\"selected\"" : string.Empty) + ">#32cb00</option>");
            output.WriteLine("<option value=\"00d2cb\"" + Strings.ToString(Text == "00d2cb" ? " selected=\"selected\"" : string.Empty) + ">#00d2cb</option>");
            output.WriteLine("<option value=\"3166ff\"" + Strings.ToString(Text == "3166ff" ? " selected=\"selected\"" : string.Empty) + ">#3166ff</option>");
            output.WriteLine("<option value=\"6434fc\"" + Strings.ToString(Text == "6434fc" ? " selected=\"selected\"" : string.Empty) + ">#6434fc</option>");
            output.WriteLine("<option value=\"656565\"" + Strings.ToString(Text == "656565" ? " selected=\"selected\"" : string.Empty) + ">#656565</option>");
            output.WriteLine("<option value=\"9a0000\"" + Strings.ToString(Text == "9a0000" ? " selected=\"selected\"" : string.Empty) + ">#9a0000</option>");
            output.WriteLine("<option value=\"ce6301\"" + Strings.ToString(Text == "ce6301" ? " selected=\"selected\"" : string.Empty) + ">#ce6301</option>");
            output.WriteLine("<option value=\"cd9934\"" + Strings.ToString(Text == "cd9934" ? " selected=\"selected\"" : string.Empty) + ">#cd9934</option>");
            output.WriteLine("<option value=\"999903\"" + Strings.ToString(Text == "999903" ? " selected=\"selected\"" : string.Empty) + ">#999903</option>");
            output.WriteLine("<option value=\"009901\"" + Strings.ToString(Text == "009901" ? " selected=\"selected\"" : string.Empty) + ">#009901</option>");
            output.WriteLine("<option value=\"329a9d\"" + Strings.ToString(Text == "329a9d" ? " selected=\"selected\"" : string.Empty) + ">#329a9d</option>");
            output.WriteLine("<option value=\"3531ff\"" + Strings.ToString(Text == "3531ff" ? " selected=\"selected\"" : string.Empty) + ">#3531ff</option>");
            output.WriteLine("<option value=\"6200c9\"" + Strings.ToString(Text == "6200c9" ? " selected=\"selected\"" : string.Empty) + ">#6200c9</option>");
            output.WriteLine("<option value=\"343434\"" + Strings.ToString(Text == "343434" ? " selected=\"selected\"" : string.Empty) + ">#343434</option>");
            output.WriteLine("<option value=\"680100\"" + Strings.ToString(Text == "680100" ? " selected=\"selected\"" : string.Empty) + ">#680100</option>");
            output.WriteLine("<option value=\"963400\"" + Strings.ToString(Text == "963400" ? " selected=\"selected\"" : string.Empty) + ">#963400</option>");
            output.WriteLine("<option value=\"986536\"" + Strings.ToString(Text == "986536" ? " selected=\"selected\"" : string.Empty) + ">#986536</option>");
            output.WriteLine("<option value=\"646809\"" + Strings.ToString(Text == "646809" ? " selected=\"selected\"" : string.Empty) + ">#646809</option>");
            output.WriteLine("<option value=\"036400\"" + Strings.ToString(Text == "036400" ? " selected=\"selected\"" : string.Empty) + ">#036400</option>");
            output.WriteLine("<option value=\"34696d\"" + Strings.ToString(Text == "34696d" ? " selected=\"selected\"" : string.Empty) + ">#34696d</option>");
            output.WriteLine("<option value=\"00009b\"" + Strings.ToString(Text == "00009b" ? " selected=\"selected\"" : string.Empty) + ">#00009b</option>");
            output.WriteLine("<option value=\"303498\"" + Strings.ToString(Text == "303498" ? " selected=\"selected\"" : string.Empty) + ">#303498</option>");
            output.WriteLine("<option value=\"000000\"" + Strings.ToString(Text == "000000" ? " selected=\"selected\"" : string.Empty) + ">#000000</option>");
            output.WriteLine("<option value=\"330001\"" + Strings.ToString(Text == "330001" ? " selected=\"selected\"" : string.Empty) + ">#330001</option>");
            output.WriteLine("<option value=\"643403\"" + Strings.ToString(Text == "643403" ? " selected=\"selected\"" : string.Empty) + ">#643403</option>");
            output.WriteLine("<option value=\"663234\"" + Strings.ToString(Text == "663234" ? " selected=\"selected\"" : string.Empty) + ">#663234</option>");
            output.WriteLine("<option value=\"343300\"" + Strings.ToString(Text == "343300" ? " selected=\"selected\"" : string.Empty) + ">#343300</option>");
            output.WriteLine("<option value=\"013300\"" + Strings.ToString(Text == "013300" ? " selected=\"selected\"" : string.Empty) + ">#013300</option>");
            output.WriteLine("<option value=\"003532\"" + Strings.ToString(Text == "003532" ? " selected=\"selected\"" : string.Empty) + ">#003532</option>");
            output.WriteLine("<option value=\"010066\"" + Strings.ToString(Text == "010066" ? " selected=\"selected\"" : string.Empty) + ">#010066</option>");
            output.WriteLine("<option value=\"340096\"" + Strings.ToString(Text == "340096" ? " selected=\"selected\"" : string.Empty) + ">#340096</option>");
            output.WriteLine("</select>");

            output.WriteLine("<script type=\"text/javascript\">");
            output.WriteLine("$('#" + ID + "').colourPicker({ico: '" + sImageFolder + "js/jquery/images/jquery.colourPicker.gif'});");
            output.WriteLine("</script>");
        }
    }
}