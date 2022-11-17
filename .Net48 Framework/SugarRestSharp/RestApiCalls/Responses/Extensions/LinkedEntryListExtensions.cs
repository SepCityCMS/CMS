// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="LinkedEntryListExtensions.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.RestApiCalls.Responses.Extensions
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class represents LinkedEntryListExtensions class.
    /// </summary>
    internal static class LinkedEntryListExtensions
    {
        /// <summary>
        /// Sets the module linked list.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="linkedListModule">The linked list module.</param>
        public static void SetModuleLinkedList(this JObject module, LinkedListModule linkedListModule)
        {
            if (linkedListModule == null)
            {
                return;
            }

            if ((linkedListModule.ModuleDataList == null) || (linkedListModule.ModuleDataList.Count == 0))
            {
                return;
            }

            foreach (var item in linkedListModule.ModuleDataList)
            {
                module[item.ModuleName] = JToken.FromObject(item.FormattedRecords);
            }
        }
    }
}