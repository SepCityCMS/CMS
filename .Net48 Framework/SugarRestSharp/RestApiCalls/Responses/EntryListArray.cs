// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EntryListArray.cs" company="SugarCRM + PocoGen + REST">
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
    /// Represents EntryListArray class
    /// </summary>
    internal class EntryListArray
    {
        /// <summary>
        /// Gets the entity  object
        /// </summary>
        /// <value>The entity.</value>
        public JObject Entity
        {
            get
            {
                var entity = new JObject();
                if (this.NameValueList == null)
                {
                    return entity;
                }

                IList<string> keys = this.NameValueList.Properties().Select(p => p.Name).ToList();
                foreach (var key in keys)
                {
                    var newKey = (string)this.NameValueList[key]["name"];
                    var newValue = (string)this.NameValueList[key]["value"];
                    entity.Add(new JProperty(newKey, newValue));
                }

                return entity;
            }
        }

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the entity module name
        /// </summary>
        /// <value>The name of the module.</value>
        [JsonProperty(PropertyName = "module_name")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the returned entity name value list
        /// </summary>
        /// <value>The name value list.</value>
        [JsonProperty(PropertyName = "name_value_list")]
        public JObject NameValueList { get; set; }
    }
}