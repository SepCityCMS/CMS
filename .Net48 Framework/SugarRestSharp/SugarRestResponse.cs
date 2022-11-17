// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="SugarRestResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using Responses;
    using System.Net;

    /// <summary>
    /// Represents SugarRestResponse class.
    /// </summary>
    public class SugarRestResponse
    {
        /// <summary>
        /// Initializes a new instance of the SugarRestResponse class.
        /// </summary>
        public SugarRestResponse()
        {
            this.Error = new ErrorResponse();
            this.JSONRawRequest = string.Empty;
            this.JSONRawResponse = string.Empty;
            this.JData = string.Empty;
        }

        /// <summary>
        /// Gets or sets the raw JSON request sent by SugarCRM Rest API.
        /// </summary>
        /// <value>The json raw request.</value>
        public string JSONRawRequest { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON response sent by SugarCRM Rest API.
        /// </summary>
        /// <value>The json raw response.</value>
        public string JSONRawResponse { get; set; }

        /// <summary>
        /// Gets or sets identity, identifiers, entity or entities data returned in JSON.
        /// Data type returned for the following request type:
        /// ReadById - Entity
        /// BulkRead - Entity collection
        /// PagedRead - Entity collection
        /// Create - Identifier (Id)
        /// BulkCreate - Identifiers (Ids)
        /// Update - Identifier (Id)
        /// BulkUpdate - Identifiers (Ids)
        /// Delete - Identifier (Id)
        /// LinkedReadById - Entity
        /// LinkedBulkRead - Entity collection
        /// </summary>
        /// <value>The j data.</value>
        public string JData { get; set; }

        /// <summary>
        /// Gets or sets identity, identifiers, entity or entities data returned.
        /// Data type returned for the following request type:
        /// ReadById - Entity
        /// BulkRead - Entity collection
        /// PagedRead - Entity collection
        /// Create - Identifier (Id)
        /// BulkCreate - Identifiers (Ids)
        /// Update - Identifier (Id)
        /// BulkUpdate - Identifiers (Ids)
        /// Delete - Identifier (Id)
        /// LinkedReadById - Entity
        /// LinkedBulkRead - Entity collection
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets status code returned.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets error object.
        /// </summary>
        /// <value>The error.</value>
        public ErrorResponse Error { get; set; }
    }
}