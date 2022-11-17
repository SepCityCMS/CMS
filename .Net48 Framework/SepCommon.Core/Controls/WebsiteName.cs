// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="WebsiteName.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;

    /// <summary>
    /// Class WebsiteName.
    /// </summary>
    public class WebsiteName
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            var iPortalID = SepFunctions.Get_Portal_ID();

            if (SepFunctions.toLong(SepCommon.Core.SepCore.Request.Item("PortalID")) > 0)
            {
                iPortalID = SepFunctions.toLong(SepCommon.Core.SepCore.Request.Item("PortalID"));
            }

            return SepFunctions.WebsiteName(iPortalID);
        }
    }
}