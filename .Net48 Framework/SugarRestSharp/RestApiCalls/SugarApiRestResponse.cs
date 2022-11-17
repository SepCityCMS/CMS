// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="SugarApiRestResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using RestSharp;

    /// <summary>
    /// Represents SugarApiRestResponse class.
    /// </summary>
    internal class SugarApiRestResponse
    {
        /// <summary>
        /// Gets or sets the RestSharp response object.
        /// </summary>
        /// <value>The rest response.</value>
        public IRestResponse RestResponse { get; set; }

        /// <summary>
        /// Gets or sets the RestSharp raw JSON request.
        /// </summary>
        /// <value>The json raw request.</value>
        public string JSONRawRequest { get; set; }

        /// <summary>
        /// Gets or sets the RestSharp raw JSON response content.
        /// </summary>
        /// <value>The json raw response.</value>
        public string JSONRawResponse { get; set; }
    }
}