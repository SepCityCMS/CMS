// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="JsonExtensions.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Helpers
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents JSONExtensions class.
    /// </summary>
    internal static class JSONExtensions
    {
        /// <summary>
        /// Converts JSON string to dynamic object collections datatable.
        /// </summary>
        /// <param name="JSON">JSON string to extend.</param>
        /// <param name="type">The type.</param>
        /// <returns>DataTable object</returns>
        public static IList ToObjects(this string JSON, Type type)
        {
            IList data = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            JArray jarr = JArray.Parse(JSON);
            foreach (JObject jobject in jarr.Children<JObject>())
            {
                object tempObject = JsonConverterHelper.Deserialize(jobject.ToString(), type);
                data.Add(tempObject);
            }

            return data;
        }

        /// <summary>
        /// Converts JSON string to dynamic object datatable.
        /// </summary>
        /// <param name="JSON">JSON string to extend.</param>
        /// <param name="type">The type.</param>
        /// <returns>DataTable object</returns>
        public static object ToObject(this string JSON, Type type)
        {
            JObject jobject = JObject.Parse(JSON);
            return JsonConverterHelper.Deserialize(jobject.ToString(), type);
        }
    }
}