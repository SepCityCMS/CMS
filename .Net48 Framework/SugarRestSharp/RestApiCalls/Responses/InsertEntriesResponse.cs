// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="InsertEntriesResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Represents InsertEntriesResponse class
    /// </summary>
    internal class InsertEntriesResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the entity identifier of inserted entity
        /// </summary>
        /// <value>The ids.</value>
        [JsonProperty(PropertyName = "ids")]
        public List<string> Ids { get; set; }
    }
}