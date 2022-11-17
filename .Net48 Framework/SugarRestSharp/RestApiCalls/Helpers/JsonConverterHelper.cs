// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="JsonConverterHelper.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Helpers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// This class represents JsonConverterHelper class.
    /// </summary>
    internal static class JsonConverterHelper
    {
        /// <summary>
        /// Deserialize JSON string to C# type.
        /// </summary>
        /// <typeparam name="T">SugarCRM C# model template type.</typeparam>
        /// <param name="content">JSON data to deserialize.</param>
        /// <returns>Object instance of type SugarCRM C# model type..</returns>
        public static T Deserialize<T>(string content)
        {
            var settings = new JsonSerializerSettings();
            DeserializerExceptionsContractResolver resolver = DeserializerExceptionsContractResolver.Instance;
            resolver.JSONToDeserialize = content;
            settings.ContractResolver = resolver;

            return JsonConvert.DeserializeObject<T>(content, settings);
        }

        /// <summary>
        /// Deserialize JSON string to C# object instance based on C# type.
        /// </summary>
        /// <param name="content">JSON data to deserialize.</param>
        /// <param name="type">SugarCRM C# model type.</param>
        /// <returns>Object instance.</returns>
        public static object Deserialize(string content, Type type)
        {
            var settings = new JsonSerializerSettings();
            DeserializerExceptionsContractResolver resolver = DeserializerExceptionsContractResolver.Instance;
            resolver.JSONToDeserialize = content;
            settings.ContractResolver = resolver;

            return JsonConvert.DeserializeObject(content, type, settings);
        }

        /// <summary>
        /// Deserialize JSON JObject to C# object instance.
        /// </summary>
        /// <typeparam name="T">SugarCRM C# model template type.</typeparam>
        /// <param name="jobject">JSON JObject data to deserialize.</param>
        /// <returns>Object instance of type T.</returns>
        public static T Deserialize<T>(JObject jobject)
        {
            var settings = new JsonSerializerSettings();
            DeserializerExceptionsContractResolver resolver = DeserializerExceptionsContractResolver.Instance;
            resolver.JSONObjectToDeserialize = jobject;
            settings.ContractResolver = resolver;

            return JsonConvert.DeserializeObject<T>(jobject.ToString(), settings);
        }

        /// <summary>
        /// Deserialize JSON JObject to C# object instance based on C# type.
        /// </summary>
        /// <param name="jobject">JSON JObject data to deserialize.</param>
        /// <param name="type">SugarCRM C# model type.</param>
        /// <returns>Object instance.</returns>
        public static object Deserialize(JObject jobject, Type type)
        {
            var settings = new JsonSerializerSettings();
            DeserializerExceptionsContractResolver resolver = DeserializerExceptionsContractResolver.Instance;
            resolver.JSONObjectToDeserialize = jobject;
            settings.ContractResolver = resolver;

            return JsonConvert.DeserializeObject(jobject.ToString(), type, settings);
        }

        /// <summary>
        /// Serialize C# object list instance to JSON JArray instance.
        /// </summary>
        /// <param name="objects">Objects to serialize.</param>
        /// <param name="type">SugarCRM C# model type.</param>
        /// <returns>JSON JArray instance.</returns>
        public static JArray SerializeList(object objects, Type type)
        {
            string JSON = JsonConvert.SerializeObject(objects, type, Formatting.Indented, null);
            return JArray.Parse(JSON);
        }

        /// <summary>
        /// Serialize C# object instance to JSON JObject instance.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="type">SugarCRM C# model type.</param>
        /// <returns>The serialized JSON object.</returns>
        public static JObject Serialize(object obj, Type type)
        {
            string JSON = JsonConvert.SerializeObject(obj, type, Formatting.Indented, null);
            return JObject.Parse(JSON);
        }
    }
}