// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="VideoPlayer.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class VideoPlayer.
    /// </summary>
    public class VideoPlayer
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
        /// The m width
        /// </summary>
        private string m_Width;

        /// <summary>
        /// Gets or sets the height of the Web server control.
        /// </summary>
        /// <value>The height.</value>
        public string Height
        {
            get => SepCommon.SepCore.Strings.ToString(m_Height);

            set => m_Height = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string src
        {
            get => SepCommon.SepCore.Strings.ToString(m_src);

            set => m_src = value;
        }

        /// <summary>
        /// Gets or sets the width of the Web server control.
        /// </summary>
        /// <value>The width.</value>
        public string Width
        {
            get => SepCommon.SepCore.Strings.ToString(m_Width);

            set => m_Width = value;
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            output.AppendLine("<p align=\"center\">");
            output.AppendLine("<video width=\"" + Width + "\" height=\"" + Height + "\" id=\"" + ID + "\" controls=\"controls\">");
            output.AppendLine("  <source src=\"" + src + "\" type='video/mp4; codecs=\"avc1.42E01E, mp4a.40.2\"'>");
            output.AppendLine("  <!-- Fallback flash player for no-HTML5 browsers with JavaScript turned off -->");
            output.AppendLine("  <object width=\"" + Width + "\" height=\"" + Height + "\" type=\"application/x-shockwave-flash\" data=\"" + sImageFolder + "js/mediaelementplayer/flashmediaelement.swf\">");
            output.AppendLine("    <param name=\"movie\" value=\"" + sImageFolder + "js/mediaelementplayer/flashmediaelement.swf\" /> ");
            output.AppendLine("    <param name=\"flashvars\" value=\"controls=true&file=" + src + "\" />");
            output.AppendLine("  </object>");
            output.AppendLine("</video>");
            output.AppendLine("</p>");
            return output.ToString();
        }
    }
}