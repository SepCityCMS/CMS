// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="HostingEnvironment.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.SepCore
{
    /// <summary>
    /// A hosting environment.
    /// </summary>
    public static class HostingEnvironment
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Map path.
        /// </summary>
        /// <param name="virtualPath">Full pathname of the virtual file.</param>
        /// <returns>A string.</returns>
        public static string MapPath(string virtualPath)
        {
            // TODO
            //return _httpContextAccessor.HttpContext.HostingEnvironment.MapPath(virtualPath);
            return "";
        }
    }
}