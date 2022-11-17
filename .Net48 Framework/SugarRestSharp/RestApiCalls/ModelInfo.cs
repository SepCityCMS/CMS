// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ModelInfo.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// This class represents ModelInfo class.
    /// </summary>
    internal class ModelInfo
    {
        /// <summary>
        /// Gets or sets the SugarCRM model name.
        /// </summary>
        /// <value>The name of the model.</value>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM JSON model name.
        /// This maps to the tablename in model attibute.
        /// </summary>
        /// <value>The name of the json model.</value>
        public string JSONModelName { get; set; }

        /// <summary>
        /// Gets or sets model C# object type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets model properties.
        /// </summary>
        /// <value>The model properties.</value>
        public List<ModelProperty> ModelProperties { get; set; }

        /// <summary>
        /// Gets the module model info object.
        /// </summary>
        /// <param name="type">The module model type.</param>
        /// <returns>The model info object.</returns>
        public static ModelInfo ReadByType(Type type)
        {
            var modelInfo = new ModelInfo();

            if (type != null)
            {
                object[] classAttrs = type.GetCustomAttributes(typeof(ModulePropertyAttribute), false);
                if (classAttrs.Length == 1)
                {
                    string modelName = ((ModulePropertyAttribute)classAttrs[0]).ModuleName;
                    string JSONModelName = ((ModulePropertyAttribute)classAttrs[0]).TableName;
                    modelInfo.ModelName = modelName;
                    modelInfo.JSONModelName = JSONModelName;
                    modelInfo.Type = type;
                    modelInfo.ModelProperties = new List<ModelProperty>();

                    var props = type.GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        object[] propAttrs = prop.GetCustomAttributes(true);
                        foreach (object attr in propAttrs)
                        {
                            var modelProperty = new ModelProperty();
                            var JsonProperty = attr as JsonPropertyAttribute;
                            if (JsonProperty != null)
                            {
                                modelProperty.Name = prop.Name;
                                modelProperty.Type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                modelProperty.JSONName = JsonProperty.PropertyName;
                                modelInfo.ModelProperties.Add(modelProperty);
                            }
                        }
                    }
                }
            }

            return modelInfo;
        }

        /// <summary>
        /// Gets the module model info object.
        /// </summary>
        /// <param name="modelName">The module model name.</param>
        /// <returns>The model info object.</returns>
        public static ModelInfo ReadByName(string modelName)
        {
            var modelInfo = new ModelInfo();

            var types = from type in typeof(ModulePropertyAttribute).Assembly.GetTypes()
                        where Attribute.IsDefined(type, typeof(ModulePropertyAttribute))
                        select type;

            foreach (var type in types)
            {
                object[] classAttrs = type.GetCustomAttributes(typeof(ModulePropertyAttribute), false);
                if (classAttrs.Length == 1)
                {
                    string attrModelName = ((ModulePropertyAttribute)classAttrs[0]).ModuleName;
                    string attrJSONModelName = ((ModulePropertyAttribute)classAttrs[0]).TableName;

                    if (!string.IsNullOrWhiteSpace(attrModelName) && (attrModelName.ToLower() == modelName.ToLower()))
                    {
                        modelInfo.ModelName = attrModelName;
                        modelInfo.JSONModelName = attrJSONModelName;
                        modelInfo.Type = type;
                        modelInfo.ModelProperties = new List<ModelProperty>();

                        var props = type.GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            object[] propAttrs = prop.GetCustomAttributes(true);
                            foreach (object attr in propAttrs)
                            {
                                var modelProperty = new ModelProperty();
                                var JsonProperty = attr as JsonPropertyAttribute;
                                if (JsonProperty != null)
                                {
                                    modelProperty.Name = prop.Name;
                                    modelProperty.Type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                    modelProperty.JSONName = JsonProperty.PropertyName;
                                    modelInfo.ModelProperties.Add(modelProperty);
                                }
                            }
                        }

                        return modelInfo;
                    }
                }
            }

            return modelInfo;
        }
    }
}