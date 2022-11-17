// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="TwitterLink.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Text;

    /// <summary>
    /// Class TwitterLink.
    /// </summary>
    public class TwitterLink
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(991, "TwitterUsername")))
            {
                output.AppendLine("<a class=\"follow-me\" href=\"http://twitter.com/" + SepFunctions.Setup(991, "TwitterUsername") + "\" target=\"_blank\">" + SepFunctions.LangText("Follow Me on Twitter") + "</a>");
            }
            else
            {
                output.AppendLine("Twitter Username is Empty.");
            }

            return output.ToString();
        }
    }
}