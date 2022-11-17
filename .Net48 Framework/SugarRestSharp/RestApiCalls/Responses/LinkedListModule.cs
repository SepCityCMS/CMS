// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="LinkedListModule.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.RestApiCalls.Responses
{
    using Newtonsoft.Json;
    using SugarRestSharp.Responses;
    using System.Collections.Generic;

    /// <summary>
    /// Represents LinkedListModule class.
    /// </summary>
    internal class LinkedListModule
    {
        /// <summary>
        /// Gets or sets the linked module data list info.
        /// </summary>
        /// <value>The module data list.</value>
        [JsonProperty(PropertyName = "link_list")]
        public List<LinkedListModuleData> ModuleDataList { get; set; }
    }
}