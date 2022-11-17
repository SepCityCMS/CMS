// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="DeserializerExceptionsContractResolver.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Helpers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// This class represents DeserializerExceptionsContractResolver class.
    /// The class creates exception to some property deserialization.
    /// </summary>
    internal class DeserializerExceptionsContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// As of 7.0.1, JSON.NET suggests using a static instance for "stateless" contract resolvers, for performance reasons.
        /// http://www.newtonsoft.com/JSON/help/html/ContractResolver.htm
        /// http://www.newtonsoft.com/JSON/help/html/M_Newtonsoft_JSON_Serialization_DefaultContractResolver__ctor_1.htm
        /// "Use the parameterless constructor and cache instances of the contract resolver within your application for optimal performance."
        /// </summary>
        private static DeserializerExceptionsContractResolver instance;

        /// <summary>
        /// Initializes static members of the <see cref="DeserializerExceptionsContractResolver" /> class.
        /// </summary>
        protected DeserializerExceptionsContractResolver() : base() { }

        /// <summary>
        /// Initializes static members of the <see cref="DeserializerExceptionsContractResolver" /> class.
        /// </summary>
        static DeserializerExceptionsContractResolver()
        {
            instance = new DeserializerExceptionsContractResolver();
        }

        /// <summary>
        /// Gets the object instance.
        /// </summary>
        /// <value>The instance.</value>
        public static DeserializerExceptionsContractResolver Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the JSON JObject to deserialize.
        /// </summary>
        /// <value>The json object to deserialize.</value>
        public JObject JSONObjectToDeserialize { private get; set; }

        /// <summary>
        /// Gets or sets the JSON string to deserialize.
        /// </summary>
        /// <value>The json to deserialize.</value>
        public string JSONToDeserialize { private get; set; }

        /// <summary>
        /// Gets the JSON JObject to deserialize.
        /// </summary>
        /// <value>The json object.</value>
        private JObject JSONObject
        {
            get
            {
                try
                {
                    if (this.JSONObjectToDeserialize != null)
                    {
                        return this.JSONObjectToDeserialize;
                    }

                    if (!string.IsNullOrWhiteSpace(this.JSONToDeserialize))
                    {
                        return JObject.Parse(this.JSONToDeserialize);
                    }
                }
                catch (Exception)
                {
                    return null;
                }

                return null;
            }
        }

        /// <summary>
        /// Creates a JSON property for the given C# property.
        /// </summary>
        /// <param name="member">C# property.</param>
        /// <param name="memberSerialization">Serialization option.</param>
        /// <returns>JSON property.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (this.JSONObject == null)
            {
                return property;
            }

            Type type = Nullable.GetUnderlyingType(property.PropertyType);
            if (type == typeof(DateTime))
            {
                property.ShouldDeserialize =
                    instance =>
                    {
                        try
                        {
                            IList<string> keys = JSONObject.Properties().Select(p => p.Name).ToList();
                            if (keys.Any(n => n == property.PropertyName))
                            {
                                JProperty jproperty = JSONObject.Properties().SingleOrDefault(p => p.Name == property.PropertyName);
                                if (jproperty != null)
                                {
                                    string dateValue = jproperty.Value.ToString();

                                    if (string.IsNullOrWhiteSpace(dateValue))
                                    {
                                        return false;
                                    }

                                    DateTime dateTime;
                                    return DateTime.TryParse(dateValue, out dateTime);
                                }
                            }

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    };
            }

            return property;
        }
    }
}