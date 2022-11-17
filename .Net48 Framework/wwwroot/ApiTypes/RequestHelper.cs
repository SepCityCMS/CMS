// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="RequestHelper.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.ApiTypes
{
    using SepCommon;
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Http;

    /// <summary>
    /// Class RequestHelper.
    /// </summary>
    public static class RequestHelper
    {
        /// <summary>
        /// Authorizes the request.
        /// </summary>
        /// <param name="AccessKeys">The access keys.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AuthorizeRequest(string AccessKeys)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_Password()))
            {
                var adminSecurity = AccessKeys;
                var userSecurity = AccessKeys;
                var sUserID = SepFunctions.UserValidated(SepFunctions.Session_User_Name(), SepFunctions.Session_Password(), ref adminSecurity, ref userSecurity);
                if (string.IsNullOrWhiteSpace(sUserID)) throw UnAuthorized("Invalid Username and/or Password");
                if (string.IsNullOrWhiteSpace(adminSecurity) && string.IsNullOrWhiteSpace(userSecurity)) throw Forbidden(null);

                return true;
            }

            return SepCommon.DAL.Members.LoginValidateAPISession(GetSessionId());
        }

        /// <summary>
        /// Forbiddens the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns>HttpResponseException.</returns>
        public static HttpResponseException Forbidden(string reason)
        {
            return ExceptionHelper.CreateHttpResponseException(string.IsNullOrWhiteSpace(reason) ? "Forbidden" : reason, HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// Gets the session identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetSessionId()
        {
            var SessionId = string.Empty;
            try
            {
                var auth = HttpContext.Current.Request.Headers["Authorization"];

                if (auth != null && auth.IndexOf("BEARER", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var spl = auth.Split(' ');

                    if (spl.Length > 1)
                    {
                        SessionId = spl[spl.Length - 1];
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_Password()))
                        {
                            var sToken = SepCommon.DAL.Members.Login(SepFunctions.Session_User_Name(), SepFunctions.Session_Password(), string.Empty, string.Empty, string.Empty, 0, true, string.Empty);
                            SessionId = sToken;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_Password()))
                    {
                        var sToken = SepCommon.DAL.Members.Login(SepFunctions.Session_User_Name(), SepFunctions.Session_Password(), string.Empty, string.Empty, string.Empty, 0, true, string.Empty);
                        SessionId = sToken;
                    }
                }
            }
            catch
            {
            }

            if (string.IsNullOrWhiteSpace(SessionId)) throw UnAuthorized("SessionId is missing");
            return SessionId;
        }

        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns>HttpResponseException.</returns>
        public static HttpResponseException NotFound(string reason = null)
        {
            return ExceptionHelper.CreateHttpResponseException(string.IsNullOrWhiteSpace(reason) ? "Record Not Found" : reason, HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Uns the authorized.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns>HttpResponseException.</returns>
        public static HttpResponseException UnAuthorized(string reason)
        {
            return ExceptionHelper.CreateHttpResponseException(reason, HttpStatusCode.Unauthorized);
        }
    }
}