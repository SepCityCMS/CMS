// ***********************************************************************
// Assembly         : SepCommon
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

namespace SepCityCMS.Server.SepCore
{
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
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Request.Form.Keys;
            }
            return null;
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
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.Query[key];
                }
                return "";
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
                if (_httpContextAccessor != null)
                {
                    var ctrls = Strings.ToString(_httpContextAccessor.HttpContext.Request.Form).Split('&');
                    for (var l = 0; l <= ctrls.Length - 1; l++)
                    {
                        if (ctrls[l].Contains(name))
                        {
                            return System.Net.WebUtility.UrlDecode(ctrls[l].Split('=')[1]);
                        }
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
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Request.Path;
            }
            return "";
        }

        /// <summary>
        /// Physical path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string PhysicalPath()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Request.Path;
            }
            return "";
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
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.Query[name];
                }
                return "";
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
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Request.Query.Keys;
            }
            return null;
        }

        /// <summary>
        /// Raw URL.
        /// </summary>
        /// <returns>A string.</returns>
        public static string RawUrl()
        {
            if (_httpContextAccessor != null)
            {
                return Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(_httpContextAccessor.HttpContext.Request);
            }
            return "";
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
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Request.Headers["Referer"];
            }
            return "";
        }

        /// <summary>
        /// URL referrer absolute path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string UrlReferrer_AbsolutePath()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Request.Path;
            }
            return "";
        }

        /// <summary>
        /// User host address.
        /// </summary>
        /// <returns>A string.</returns>
        public static string UserHostAddress()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.Response.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return "";
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
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.Path;
                }
                return "";
            }

            /// <summary>
            /// Absolute URI.
            /// </summary>
            /// <returns>A string.</returns>
            public static string AbsoluteUri()
            {
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.PathBase;
                }
                return "";
            }

            /// <summary>
            /// Gets the host.
            /// </summary>
            /// <returns>A string.</returns>
            public static string Host()
            {
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.Host.ToString();
                }
                return "";
            }

            /// <summary>
            /// Gets the port.
            /// </summary>
            /// <returns>An int.</returns>
            public static int? Port()
            {
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.Host.Port;
                }
                return 80;
            }

            /// <summary>
            /// Gets the query.
            /// </summary>
            /// <returns>A string.</returns>
            public static string Query()
            {
                if (_httpContextAccessor != null)
                {
                    return _httpContextAccessor.HttpContext.Request.Query.ToString();
                }
                return "";
            }
        }
    }
}