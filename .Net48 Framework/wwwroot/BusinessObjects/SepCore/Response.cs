// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Response.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.SepCore
{
    /// <summary>
    /// A response.
    /// </summary>
    public static class Response
    {
        /// <summary>
        /// Adds a header to 'value'.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddHeader(string name, string value)
        {
            System.Web.HttpContext.Current.Response.AddHeader(name, value);
        }

        /// <summary>
        /// Clears this object to its blank/initial state.
        /// </summary>
        public static void Clear()
        {
            System.Web.HttpContext.Current.Response.Clear();
        }

        /// <summary>
        /// Ends this object.
        /// </summary>
        public static void End()
        {
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Query if this object is client connected.
        /// </summary>
        /// <returns>True if client connected, false if not.</returns>
        public static bool IsClientConnected()
        {
            return System.Web.HttpContext.Current.Response.IsClientConnected;
        }

        /// <summary>
        /// Redirects the given document.
        /// </summary>
        /// <param name="url">URL of the resource.</param>
        public static void Redirect(string url)
        {
            System.Web.HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// Writes.
        /// </summary>
        /// <param name="s">The s to write.</param>
        public static void Write(string s)
        {
            System.Web.HttpContext.Current.Response.Write(s);
        }
    }
}