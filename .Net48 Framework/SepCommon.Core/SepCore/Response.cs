// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="_httpContextAccessor.HttpContext.Response.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Http;

namespace SepCommon.Core.SepCore
{
    /// <summary>
    /// A _httpContextAccessor.HttpContext.Response.
    /// </summary>
    public static class Response
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Adds a header to 'value'.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddHeader(string name, string value)
        {
            _httpContextAccessor.HttpContext.Response.Headers.Add(name, value);
        }

        /// <summary>
        /// Clears this object to its blank/initial state.
        /// </summary>
        public static void Clear()
        {
            _httpContextAccessor.HttpContext.Response.Clear();
        }

        /// <summary>
        /// Ends this object.
        /// </summary>
        public static void End()
        {
        }

        /// <summary>
        /// Query if this object is client connected.
        /// </summary>
        /// <returns>True if client connected, false if not.</returns>
        public static bool IsClientConnected()
        {
            return true;
        }

        /// <summary>
        /// Redirects the given document.
        /// </summary>
        /// <param name="url">URL of the resource.</param>
        public static void Redirect(string url)
        {
            _httpContextAccessor.HttpContext.Response.Redirect(url);
        }

        /// <summary>
        /// Writes.
        /// </summary>
        /// <param name="s">The s to write.</param>
        public static void Write(string s)
        {
            _httpContextAccessor.HttpContext.Response.WriteAsync(s);
        }
    }
}