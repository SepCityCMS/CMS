// ***********************************************************************
// Assembly         : SepCommon
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

namespace SepCommon.SepCore
{
    /// <summary>
    /// A session.
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Gets a cookie.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The cookie.</returns>
        public static string getCookie(string name)
        {
            if (System.Web.HttpContext.Current.Request.Cookies != null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies[name] != null)
                {
                    return System.Web.HttpContext.Current.Request.Cookies[name].Value;
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
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session[name] != null)
                {
                    return Strings.ToString(System.Web.HttpContext.Current.Session[name]);
                }
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
                System.Web.HttpContext.Current.Response.Cookies.Add(new System.Web.HttpCookie(name, value));
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
                System.Web.HttpContext.Current.Session[name] = value;
            }
            catch
            {
            }
        }
    }
}