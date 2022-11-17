// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="LinkedModuleData.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents LinkedModuleData class
    /// </summary>
    internal class LinkedModuleData
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
        public List<JObject> Records { get; set; }

        /// <summary>
        /// Gets the formatted records.
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

                foreach (JObject item in this.Records)
                {
                    JObject jentity = new JObject();
                    IList<string> keys = item.Properties().Select(p => p.Name).ToList();
                    foreach (var key in keys)
                    {
                        var newKey = (string)item[key]["name"];
                        var newValue = (string)item[key]["value"];
                        jentity.Add(new JProperty(newKey, newValue));
                    }

                    entities.Add(jentity);
                }

                return entities;
            }
        }
    }
}