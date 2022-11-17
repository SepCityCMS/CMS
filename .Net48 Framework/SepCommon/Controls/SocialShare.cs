// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SocialShare.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class SocialShare.
    /// </summary>
    public class SocialShare
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(993, "SocialSharing") == "Yes")
            {
                output.AppendLine("<br /><br />");
                output.AppendLine("<div class=\"row\">");
                output.AppendLine("<div class=\"col-lg-12\">");
                output.AppendLine("<a href=\"http://twitter.com/home?status=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Url.AbsoluteUri()) + "\" title=\"Share on Twitter\" target=\"_blank\" Class=\"btn btn-twitter\"><i Class=\"fa fa-twitter\"></i> Twitter</a>");
                output.AppendLine(" <a href=\"https://www.facebook.com/sharer/sharer.php?u=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Url.AbsoluteUri()) + "\" title=\"Share on Facebook\" target=\"_blank\" Class=\"btn btn-facebook\"><i Class=\"fa fa-facebook\"></i> Facebook</a>");
                output.AppendLine(" <a href=\"http://www.stumbleupon.com/submit?url=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Url.AbsoluteUri()) + "\" title=\"Share on StumbleUpon\" target=\"_blank\" data-placement=\"top\" Class=\"btn btn-stumbleupon\"><i Class=\"fa fa-stumbleupon\"></i> Stumbleupon</a>");
                output.AppendLine(" <a href=\"http://www.linkedin.com/shareArticle?mini=true&url=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Url.AbsoluteUri()) + "&title=&summary=\" title=\"Share on LinkedIn\" target=\"_blank\" Class=\"btn btn-linkedin\"><i Class=\"fa fa-linkedin\"></i> LinkedIn</a>");
                output.AppendLine("</div>");
                output.AppendLine("</div>");
            }

            return output.ToString();
        }
    }
}