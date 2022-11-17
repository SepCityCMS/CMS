// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="LinkedRecordItem.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.RestApiCalls.Responses
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents LinkedRecordItem class.
    /// </summary>
    internal class LinkedRecordItem
    {
        /// <summary>
        /// Gets or sets the JSON link value.
        /// </summary>
        /// <value>The value.</value>
        [JsonProperty(PropertyName = "link_value")]
        public JObject Value { get; set; }

        /// <summary>
        /// Gets the JSON formatted link value.
        /// </summary>
        /// <value>The formatted value.</value>
        public JObject FormattedValue
        {
            get
            {
                if (this.Value == null)
                {
                    return null;
                }

                JObject jentity = new JObject();
                IList<string> keys = this.Value.Properties().Select(p => p.Name).ToList();
                foreach (var key in keys)
                {
                    var newKey = (string)this.Value[key]["name"];
                    var newValue = (string)this.Value[key]["value"];
                    jentity.Add(new JProperty(newKey, newValue));
                }

                return jentity;
            }
        }
    }
}