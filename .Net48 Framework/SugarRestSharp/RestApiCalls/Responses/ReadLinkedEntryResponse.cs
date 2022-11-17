// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ReadLinkedEntryResponse.cs" company="SugarCRM + PocoGen + REST">
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
    /// Represents ReadLinkedEntryResponse class
    /// </summary>
    internal class ReadLinkedEntryResponse : BaseResponse
    {
        /// <summary>
        /// Gets the entity returned from SugarCRM to JSON array object
        /// </summary>
        /// <value>The entity.</value>
        [JsonIgnore]
        public JObject Entity
        {
            get
            {
                if (this.EntryListArray == null)
                {
                    return null;
                }

                var entityList = this.EntryListArray.Select(item => item.Entity).ToList();

                JObject jobject = new JObject();
                if (entityList.Count > 0)
                {
                    jobject = entityList[0];

                    foreach (List<LinkedModuleData> linkedModuleData in this.RelationshipList)
                    {
                        if (linkedModuleData != null)
                        {
                            foreach (LinkedModuleData item in linkedModuleData)
                            {
                                jobject[item.ModuleName] = JToken.FromObject(item.FormattedRecords);
                            }
                        }
                    }

                    return jobject;
                }

                return new JObject();
            }
        }

        /// <summary>
        /// Gets or sets the entry list in JSON
        /// </summary>
        /// <value>The entry list array.</value>
        [JsonProperty(PropertyName = "entry_list")]
        public List<EntryListArray> EntryListArray { get; set; }

        /// <summary>
        /// Gets or sets the relationship link entry list in JSON
        /// </summary>
        /// <value>The relationship list.</value>
        [JsonProperty(PropertyName = "relationship_list")]
        public List<List<LinkedModuleData>> RelationshipList { get; set; }
    }
}