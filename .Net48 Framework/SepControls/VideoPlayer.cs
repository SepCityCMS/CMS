// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="VideoPlayer.cs" company="SepCity, Inc.">
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
    /// Class VideoPlayer.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:VideoPlayer runat=server></{0}:VideoPlayer>")]
    public class VideoPlayer : WebControl
    {
        /// <summary>
        /// The m height
        /// </summary>
        private string m_Height;

        /// <summary>
        /// The m source
        /// </summary>
        private string m_src;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// The m width
        /// </summary>
        private string m_Width;

        /// <summary>
        /// Gets or sets the height of the Web server control.
        /// </summary>
        /// <value>The height.</value>
        public new string Height
        {
            get => Strings.ToString(m_Height);

            set => m_Height = value;
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string src
        {
            get => Strings.ToString(m_src);

            set => m_src = value;
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
            get => Strings.ToString(m_Text);

            set => m_Text = value;
        }

        /// <summary>
        /// Gets or sets the width of the Web server control.
        /// </summary>
        /// <value>The width.</value>
        public new string Width
        {
            get => Strings.ToString(m_Width);

            set => m_Width = value;
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
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            output.WriteLine("<p align=\"center\">");
            output.WriteLine("<video width=\"" + Width + "\" height=\"" + Height + "\" id=\"" + ID + "\" controls=\"controls\">");
            output.WriteLine("  <source src=\"" + src + "\" type='video/mp4; codecs=\"avc1.42E01E, mp4a.40.2\"'>");
            output.WriteLine("  <!-- Fallback flash player for no-HTML5 browsers with JavaScript turned off -->");
            output.WriteLine("  <object width=\"" + Width + "\" height=\"" + Height + "\" type=\"application/x-shockwave-flash\" data=\"" + sImageFolder + "js/mediaelementplayer/flashmediaelement.swf\">");
            output.WriteLine("    <param name=\"movie\" value=\"" + sImageFolder + "js/mediaelementplayer/flashmediaelement.swf\" /> ");
            output.WriteLine("    <param name=\"flashvars\" value=\"controls=true&file=" + src + "\" />");
            output.WriteLine("  </object>");
            output.WriteLine("</video>");
            output.WriteLine("</p>");
        }
    }
}