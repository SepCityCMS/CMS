// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="LinkedListModuleData.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestApiCalls.Responses;
    using System.Collections.Generic;

    /// <summary>
    /// Represents LinkedListModuleData class
    /// </summary>
    internal class LinkedListModuleData
    {
        /// <summary>
        /// Gets or sets the linked module name.
        /// </summary>
        /// <value>The name of the module.</value>
        [JsonProperty(PropertyName = "name")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the linked module data.
        /// </summary>
        /// <value>The records.</value>
        [JsonProperty(PropertyName = "records")]
        public List<LinkedRecordItem> Records { get; set; }

        /// <summary>
        /// Gets the formatted record in JSON.
        /// </summary>
        /// <value>The formatted records.</value>
        public List<JObject> FormattedRecords
        {
            get
            {
                var entities = new List<JObject>();
                if (this.Records == null)
                {
                    return new List<JObject>();
                }

                foreach (LinkedRecordItem item in this.Records)
                {
                    entities.Add(item.FormattedValue);
                }

                return entities;
            }
        }
    }
}