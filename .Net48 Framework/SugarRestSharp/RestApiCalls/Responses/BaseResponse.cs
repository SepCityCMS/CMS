// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="BaseResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    using Newtonsoft.Json;
    using System;
    using System.Net;

    /// <summary>
    /// Base SugarCRM REST API response object
    /// </summary>
    internal class BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the BaseResponse class
        /// </summary>
        public BaseResponse()
        {
            this.Time = DateTime.UtcNow;
            this.Error = new ErrorResponse();
        }

        /// <summary>
        /// Gets or sets the time the API call was made
        /// </summary>
        /// <value>The time.</value>
        [JsonIgnore]
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON request sent by SugarCRM Rest API
        /// </summary>
        /// <value>The json raw request.</value>
        [JsonIgnore]
        public string JSONRawRequest { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON response sent by SugarCRM Rest API
        /// </summary>
        /// <value>The json raw response.</value>
        [JsonIgnore]
        public string JSONRawResponse { get; set; }

        /// <summary>
        /// Gets or sets the error object
        /// </summary>
        /// <value>The error.</value>
        [JsonIgnore]
        public ErrorResponse Error { get; set; }

        /// <summary>
        /// Gets or sets the http status code - either returned from the API call or assigned
        /// </summary>
        /// <value>The status code.</value>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
    }
}