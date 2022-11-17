// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="HttpRuntime.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.SepCore
{
    /// <summary>
    /// A HTTP runtime.
    /// </summary>
    public static class HttpRuntime
    {
        /// <summary>
        /// Application domain application virtual path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string AppDomainAppVirtualPath()
        {
            return System.Web.HttpRuntime.AppDomainAppVirtualPath;
        }
    }
}