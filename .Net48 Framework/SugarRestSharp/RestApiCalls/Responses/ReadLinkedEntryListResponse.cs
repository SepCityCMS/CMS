// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ReadLinkedEntryListResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestApiCalls.Responses;
    using RestApiCalls.Responses.Extensions;
    using System.Collections.Generic;

    /// <summary>
    /// Represents ReadLinkedEntryListResponse class
    /// </summary>
    internal class ReadLinkedEntryListResponse : BaseResponse
    {
        /// <summary>
        /// Gets the entity list returned from SugarCRM to JSON array objects
        /// </summary>
        /// <value>The entity list.</value>
        public JArray EntityList
        {
            get
            {
                if (this.EntryList == null)
                {
                    return null;
                }

                var entityList = new JArray();
                int count = this.EntryList.Count;
                for (int i = 0; i < count; i++)
                {
                    JObject entity = this.EntryList[i].Entity;
                    entity.SetModuleLinkedList(this.RelationshipList[i]);
                    entityList.Add(entity);
                }

                return entityList;
            }
        }

        /// <summary>
        /// Gets or sets the number of entries returned
        /// </summary>
        /// <value>The count.</value>
        [JsonProperty(PropertyName = "result_count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the total count of entries available
        /// </summary>
        /// <value>The total count.</value>
        [JsonProperty(PropertyName = "total_count")]
        public string TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the next offset
        /// </summary>
        /// <value>The next offset.</value>
        [JsonProperty(PropertyName = "next_offset")]
        public int NextOffset { get; set; }

        /// <summary>
        /// Gets or sets the entry list in JSON
        /// </summary>
        /// <value>The entry list.</value>
        [JsonProperty(PropertyName = "entry_list")]
        public List<EntryListArray> EntryList { get; set; }

        /// <summary>
        /// Gets or sets the relationship link entry list in JSON
        /// </summary>
        /// <value>The relationship list.</value>
        [JsonProperty(PropertyName = "relationship_list")]
        public List<LinkedListModule> RelationshipList { get; set; }
    }
}