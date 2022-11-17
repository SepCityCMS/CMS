// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="_httpContextAccessor.HttpContext.Request.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.SepCore
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    /// <summary>
    /// A _httpContextAccessor.HttpContext.Request.
    /// </summary>
    public static class Request
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A list of.</returns>
        public static string Form(string name)
        {
            try
            {
                return _httpContextAccessor.HttpContext.Request.Form[name];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <returns>A list of.</returns>
        public static System.Collections.Generic.ICollection<string> Form()
        {
            return _httpContextAccessor.HttpContext.Request.Form.Keys;
        }

        /// <summary>
        /// Items.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A string.</returns>
        public static string Item(string key)
        {
            try
            {
                return _httpContextAccessor.HttpContext.Request.Query[key];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Pair value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public static string PairValue(string name)
        {
            try
            {
                var ctrls = Strings.ToString(_httpContextAccessor.HttpContext.Request.Form).Split('&');
                for (var l = 0; l <= ctrls.Length - 1; l++)
                {
                    if (ctrls[l].Contains(name))
                    {
                        return System.Net.WebUtility.UrlDecode(ctrls[l].Split('=')[1]);
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Path()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }

        /// <summary>
        /// Physical path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string PhysicalPath()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }

        /// <summary>
        /// Queries the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The string.</returns>
        public static string QueryString(string name)
        {
            try
            {
                return _httpContextAccessor.HttpContext.Request.Query[name];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Queries the string.
        /// </summary>
        /// <returns>The string.</returns>
        public static System.Collections.Generic.ICollection<string> QueryString()
        {
            return _httpContextAccessor.HttpContext.Request.Query.Keys;
        }

        /// <summary>
        /// Raw URL.
        /// </summary>
        /// <returns>A string.</returns>
        public static string RawUrl()
        {
            return Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(_httpContextAccessor.HttpContext.Request);
        }

        /// <summary>
        /// Server variables.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public static string ServerVariables(string name)
        {
            // TODO
            //return _httpContextAccessor.HttpContext.Request.ServerVariables[name];
            return "";

        }

        /// <summary>
        /// URL referrer.
        /// </summary>
        /// <returns>An URI.</returns>
        public static string UrlReferrer()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["Referer"];
        }

        /// <summary>
        /// URL referrer absolute path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string UrlReferrer_AbsolutePath()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }

        /// <summary>
        /// User host address.
        /// </summary>
        /// <returns>A string.</returns>
        public static string UserHostAddress()
        {
            return _httpContextAccessor.HttpContext.Response.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// A browser.
        /// </summary>
        public static class Browser
        {
            // Nested types should not be visible
            /// <summary>
            /// Query if this object is mobile device.
            /// </summary>
            /// <returns>True if mobile device, false if not.</returns>
            public static bool IsMobileDevice()
            {
                return false;
            }
        }

        /// <summary>
        /// An url.
        /// </summary>
        public static class Url
        {
            // Nested types should not be visible
            /// <summary>
            /// Absolute path.
            /// </summary>
            /// <returns>A string.</returns>
            public static string AbsolutePath()
            {
                return _httpContextAccessor.HttpContext.Request.Path;
            }

            /// <summary>
            /// Absolute URI.
            /// </summary>
            /// <returns>A string.</returns>
            public static string AbsoluteUri()
            {
                return _httpContextAccessor.HttpContext.Request.PathBase;
            }

            /// <summary>
            /// Gets the host.
            /// </summary>
            /// <returns>A string.</returns>
            public static string Host()
            {
                return _httpContextAccessor.HttpContext.Request.Host.ToString();
            }

            /// <summary>
            /// Gets the port.
            /// </summary>
            /// <returns>An int.</returns>
            public static int? Port()
            {
                return _httpContextAccessor.HttpContext.Request.Host.Port;
            }

            /// <summary>
            /// Gets the query.
            /// </summary>
            /// <returns>A string.</returns>
            public static string Query()
            {
                return _httpContextAccessor.HttpContext.Request.Query.ToString();
            }
        }
    }
}