// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AdminLink.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class AdminLink.
    /// </summary>
    public class AdminLink
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true))
            {
                output.AppendLine("<a class=\"nav-link\" href=\"" + sImageFolder + "spadmin/default.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\" target=\"_blank\">" + SepFunctions.LangText("Admin Console") + "</a>");
            }

            return output.ToString();
        }
    }
}