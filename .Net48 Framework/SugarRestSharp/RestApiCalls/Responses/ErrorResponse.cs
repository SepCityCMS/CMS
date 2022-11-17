// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ErrorResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Net;

    /// <summary>
    /// Represents ErrorResponse class
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the http status code
        /// </summary>
        /// <value>The status.</value>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the returned error type
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of the error returned
        /// </summary>
        /// <value>The number.</value>
        [JsonProperty(PropertyName = "number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the error message description
        /// </summary>
        /// <value>The message.</value>
        [JsonProperty(PropertyName = "description")]
        public string Message { get; set; }

        /// <summary>
        /// Gets formatted error response based on RestSharp error
        /// </summary>
        /// <param name="response">Response object from RestSharp</param>
        /// <returns>ErrorResponse object</returns>
        public static ErrorResponse Format(IRestResponse response)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Name = string.IsNullOrWhiteSpace(response.StatusDescription) ? "Internal server error." : response.StatusDescription;
            errorResponse.Number = (int)response.StatusCode;
            string message = response.ErrorMessage;
            if (string.IsNullOrWhiteSpace(message))
            {
                if (response.ErrorException != null)
                {
                    message = response.ErrorException.Message;
                }
                else
                {
                    message = errorResponse.Name;
                }
            }

            errorResponse.Message = message;
            return errorResponse;
        }

        /// <summary>
        /// Gets formatted error response exception or SugarCRM error message
        /// </summary>
        /// <param name="exception">Exception from SugarCRM REST API calls or .NET error</param>
        /// <param name="errorContent">Error returned from SugarCRM</param>
        /// <returns>ErrorResponse object</returns>
        public static ErrorResponse Format(Exception exception, string errorContent)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Name = "An error has occurred!";
            errorResponse.Number = (int)HttpStatusCode.SeeOther;
            if (string.IsNullOrWhiteSpace(errorContent))
            {
                errorResponse.Message = exception.Message;
            }
            else
            {
                errorResponse.Message = errorContent;
            }

            return errorResponse;
        }

        /// <summary>
        /// Gets formatted SugarCRM error message
        /// </summary>
        /// <param name="errorContent">Error returned from SugarCRM</param>
        /// <returns>ErrorResponse object</returns>
        public static ErrorResponse Format(string errorContent)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Name = "An error has occurred!";
            errorResponse.Number = (int)HttpStatusCode.SeeOther;
            errorResponse.Message = errorContent;

            return errorResponse;
        }
    }
}