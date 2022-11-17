// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Request.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.SepCore
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// A request.
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A list of.</returns>
        public static string Form(string name)
        {
            try
            {
                return System.Web.HttpContext.Current.Request.Form[name];
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
        public static NameValueCollection Form()
        {
            return System.Web.HttpContext.Current.Request.Form;
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
                return System.Web.HttpContext.Current.Request[key];
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
                var ctrls = Strings.ToString(System.Web.HttpContext.Current.Request.Form).Split('&');
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
            return System.Web.HttpContext.Current.Request.Path;
        }

        /// <summary>
        /// Physical path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string PhysicalPath()
        {
            return System.Web.HttpContext.Current.Request.PhysicalPath;
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
                return System.Web.HttpContext.Current.Request.QueryString[name];
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
        public static NameValueCollection QueryString()
        {
            return System.Web.HttpContext.Current.Request.QueryString;
        }

        /// <summary>
        /// Raw URL.
        /// </summary>
        /// <returns>A string.</returns>
        public static string RawUrl()
        {
            return System.Web.HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ServerVariables(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return System.Web.HttpContext.Current.Request.ServerVariables[name];
            }
            return "";
        }

        /// <summary>
        /// URL referrer.
        /// </summary>
        /// <returns>An URI.</returns>
        public static Uri UrlReferrer()
        {
            return System.Web.HttpContext.Current.Request.UrlReferrer;
        }

        /// <summary>
        /// URL referrer absolute path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string UrlReferrer_AbsolutePath()
        {
            return System.Web.HttpContext.Current.Request.UrlReferrer.AbsolutePath;
        }

        /// <summary>
        /// User host address.
        /// </summary>
        /// <returns>A string.</returns>
        public static string UserHostAddress()
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
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
                return System.Web.HttpContext.Current.Request.Browser.IsMobileDevice;
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
                return System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            }

            /// <summary>
            /// Absolute URI.
            /// </summary>
            /// <returns>A string.</returns>
            public static string AbsoluteUri()
            {
                return System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            }

            /// <summary>
            /// Gets the host.
            /// </summary>
            /// <returns>A string.</returns>
            public static string Host()
            {
                return System.Web.HttpContext.Current.Request.Url.Host;
            }

            /// <summary>
            /// Gets the port.
            /// </summary>
            /// <returns>An int.</returns>
            public static int Port()
            {
                return System.Web.HttpContext.Current.Request.Url.Port;
            }

            /// <summary>
            /// Gets the query.
            /// </summary>
            /// <returns>A string.</returns>
            public static string Query()
            {
                return System.Web.HttpContext.Current.Request.Url.Query;
            }
        }
    }
}