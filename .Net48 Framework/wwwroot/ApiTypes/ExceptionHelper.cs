// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ExceptionHelper.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.ApiTypes
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Http;

    /// <summary>
    /// Class ExceptionHelper.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Creates the HTTP response exception.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="code">The code.</param>
        /// <returns>HttpResponseException.</returns>
        /// <exception cref="HttpResponseException"></exception>
        public static HttpResponseException CreateHttpResponseException(string reason, HttpStatusCode code)
        {
            var response = CreateHttpResponseExceptionMessage(reason, code);
            throw new HttpResponseException(response);
        }

        /// <summary>
        /// Creates the HTTP response exception.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="code">The code.</param>
        /// <param name="ErrorCode">The error code.</param>
        /// <returns>HttpResponseException.</returns>
        /// <exception cref="HttpResponseException"></exception>
        public static HttpResponseException CreateHttpResponseException(string reason, HttpStatusCode code, string ErrorCode)
        {
            var response = CreateHttpResponseExceptionMessage(reason, code, ErrorCode);
            throw new HttpResponseException(response);
        }

        /// <summary>
        /// Creates the HTTP response exception message.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="code">The code.</param>
        /// <param name="ErrorCode">The error code.</param>
        /// <returns>HttpResponseMessage.</returns>
        public static HttpResponseMessage CreateHttpResponseExceptionMessage(string reason, HttpStatusCode code, string ErrorCode = "")
        {
            var isJson = false;
            var acceptHeader = "application/json";
            try
            {
                acceptHeader = HttpContext.Current.Request.Headers["Accept"];
            }
            catch
            {
                // ignore the error
            }

            if (acceptHeader == null || acceptHeader == "application/json")
            {
                isJson = true;
            }
            else
            {
                if (acceptHeader.Contains(";") && acceptHeader.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) || acceptHeader == "*/*")
                    isJson = true;
            }

            if (isJson)
            {
                var errorObj = new { errors = reason, ErrorCode };
                return new HttpResponseMessage { StatusCode = code, ReasonPhrase = reason, Content = new StringContent(Json.Encode(errorObj), Encoding.Default, "application/json") };
            }
            else
            {
                return new HttpResponseMessage { StatusCode = code, ReasonPhrase = reason, Content = new StringContent("<errors ErrorCode=\"" + WebUtility.HtmlEncode(ErrorCode) + "\">" + WebUtility.HtmlEncode(reason) + "</errors>", Encoding.Default, "application/xml") };
            }
        }
    }
}