// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="InstantMessenger.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Text;

    /// <summary>
    /// Class InstantMessenger.
    /// </summary>
    public class InstantMessenger
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()))
            {
                var sImageFolder = SepFunctions.GetInstallFolder();

                output.AppendLine("<script type=\"text/javascript\" src=\"" + sImageFolder + "js/messenger.js\"></script>");

                output.AppendLine("<script type=\"text/javascript\">waitEvent();</script>");

                output.AppendLine("<div id=\"lbMessages\" title=\"Alert\"></div>");
            }

            return output.ToString();
        }
    }
}