// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-19-2019
// ***********************************************************************
// <copyright file="Session.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Http;

namespace SepCityCMS.Server.SepCore
{
    /// <summary>
    /// A session.
    /// </summary>
    public static class Session
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Gets a cookie.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The cookie.</returns>
        public static string getCookie(string name)
        {
            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext.Request.Cookies != null)
                {
                    if (_httpContextAccessor.HttpContext.Request.Cookies[name] != null)
                    {
                        return _httpContextAccessor.HttpContext.Request.Cookies[name];
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets a session.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The session.</returns>
        public static string getSession(string name)
        {
            try
            {
                if (_httpContextAccessor != null)
                {
                    if (_httpContextAccessor.HttpContext.Session != null)
                    {
                        if (_httpContextAccessor.HttpContext.Session.Get(name) != null)
                        {
                            return Strings.ToString(_httpContextAccessor.HttpContext.Session.Get(name));
                        }
                    }
                }
            }
            catch
            {

            }
            return string.Empty;
        }

        /// <summary>
        /// Sets a cookie.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void setCookie(string name, string value)
        {
            try
            {
                // TODO
                //_httpContextAccessor.HttpContext.Response.Cookies.Add(new System.Web.HttpCookie(name, value));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Sets a session.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void setSession(string name, string value)
        {
            try
            {
                // TODO
                //_httpContextAccessor.HttpContext.Session[name] = value;
            }
            catch
            {
            }
        }
    }
}