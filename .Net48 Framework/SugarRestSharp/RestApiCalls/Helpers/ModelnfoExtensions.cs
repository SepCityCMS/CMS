// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ModelnfoExtensions.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Helpers
{
    using RestApiCalls.Helpers;
    using RestApiCalls.Requests;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class represents ModelnfoExtensions class.
    /// </summary>
    internal static class ModelnfoExtensions
    {
        /// <summary>
        /// Gets list of JSON property names based on the selected properties.
        /// The property name can be of JSON type or C# property name.
        /// And they can be mixed.
        /// </summary>
        /// <param name="modelInfo">SugarCRM module info.</param>
        /// <param name="selectedProperties">The selected property names.</param>
        /// <param name="makeSelectionOptional">Selection is optional. If no selection set, all properties returned if this value is true.</param>
        /// <returns>List of module JSON properties.</returns>
        public static List<string> GetJsonPropertyNames(this ModelInfo modelInfo, List<string> selectedProperties, bool makeSelectionOptional = false)
        {
            var newSelectedProperties = new List<string>();

            if ((modelInfo == null) || (modelInfo.ModelProperties == null))
            {
                return selectedProperties;
            }

            List<ModelProperty> modelProperties = modelInfo.ModelProperties;
            if (((selectedProperties == null) || (selectedProperties.Count == 0)) && makeSelectionOptional)
            {
                return modelProperties.Select(x => x.JSONName).ToList();
            }

            foreach (string property in selectedProperties) //-V3125
            {
                string JSONName = property;

                if (!string.IsNullOrWhiteSpace(property))
                {
                    ModelProperty modelProperty = modelProperties.FirstOrDefault(x => (x.Name.ToLower() == property.ToLower()));
                    if (modelProperty != null)
                    {
                        JSONName = modelProperty.JSONName;
                    }
                }

                newSelectedProperties.Add(JSONName);
            }

            return newSelectedProperties;
        }

        /// <summary>
        /// Gets modules linked information.
        /// </summary>
        /// <param name="modelInfo">SugarCRM module info.</param>
        /// <param name="linkedModuleInfoList">The module linked info list.</param>
        /// <returns>Dictionary map of linked modules.</returns>
        public static Dictionary<string, List<string>> GetJSONLinkedInfo(this ModelInfo modelInfo, Dictionary<object, List<string>> linkedModuleInfoList)
        {
            if ((linkedModuleInfoList == null) || (linkedModuleInfoList.Count == 0))
            {
                return null;
            }

            var linkedInfo = new Dictionary<string, List<string>>();

            foreach (var item in linkedModuleInfoList)
            {
                ModelInfo linkedModelInfo = null;
                if (item.Key is Type)
                {
                    linkedModelInfo = ModelInfo.ReadByType((Type)item.Key);
                }
                else
                {
                    linkedModelInfo = ModelInfo.ReadByName(item.Key.ToString());
                }

                linkedInfo[linkedModelInfo.JSONModelName] = linkedModelInfo.GetJsonPropertyNames(item.Value, true);
            }

            return linkedInfo;
        }

        /// <summary>
        /// Gets query based on either query predicates or raw query.
        /// </summary>
        /// <param name="modelInfo">SugarCRM module info.</param>
        /// <param name="queryPredicates">The query predicate collection.</param>
        /// <param name="queryString">Formatted query string.</param>
        /// <returns>The formatted query.</returns>
        public static string GetQuery(this ModelInfo modelInfo, List<QueryPredicate> queryPredicates, string queryString)
        {
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                return " " + queryString.Trim() + " ";
            }

            List<JSONPredicate> JSONPredicates = modelInfo.GetJSONPredicates(queryPredicates);

            return QueryBuilder.GetWhereClause(JSONPredicates);
        }

        /// <summary>
        /// Converts C# query predicate collection to JSON query predicate collection.
        /// The C# query can have a mixture of both C# property name name JSON property name.
        /// </summary>
        /// <param name="modelInfo">SugarCRM module info.</param>
        /// <param name="queryPredicates">The query predicate collection.</param>
        /// <returns>The JSON predicate collection.</returns>
        private static List<JSONPredicate> GetJSONPredicates(this ModelInfo modelInfo, List<QueryPredicate> queryPredicates)
        {
            if ((queryPredicates == null) || (queryPredicates.Count == 0))
            {
                return null;
            }

            var JSONPredicates = new List<JSONPredicate>();

            List<ModelProperty> modelProperties = modelInfo.ModelProperties;

            foreach (var item in queryPredicates)
            {
                ModelProperty modelProperty = modelProperties.FirstOrDefault(x => ((x.Name.ToLower() == item.PropertyName.ToLower()) ||
                                                                                   (x.JSONName.ToLower() == item.PropertyName.ToLower())));

                if (modelProperty != null)
                {
                    string JSONName = string.Format("{0}.{1}", modelInfo.JSONModelName, modelProperty.JSONName);
                    bool isNumeric = modelProperty.IsNumeric;
                    string value = GetFormattedValue(item.Value, isNumeric);
                    string fromValue = GetFormattedValue(item.FromValue, isNumeric);
                    string toValue = GetFormattedValue(item.ToValue, isNumeric);

                    JSONPredicates.Add(new JSONPredicate(JSONName, item.Operator, value, fromValue, toValue));
                }
            }

            return JSONPredicates;
        }

        /// <summary>
        /// Gets the formatted query value considering whether it is a numeric value or not.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="isNumeric">Boolean value to know if it is numeric or not.</param>
        /// <returns>The formatted query value.</returns>
        private static string GetFormattedValue(object value, bool isNumeric)
        {
            if (value == null)
            {
                return null;
            }

            if (IsList(value))
            {
                try
                {
                    IList valueList = (IList)value;
                    List<string> strValueList = new List<string>();
                    foreach (var item in valueList)
                    {
                        if (isNumeric)
                        {
                            strValueList.Add(item.ToString());
                        }
                        else
                        {
                            strValueList.Add("\'" + item.ToString() + "\'");
                        }
                    }

                    return string.Join(",", strValueList.ToArray());
                }
                catch (Exception)
                {
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// Checks if an object is a list.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True or false.</returns>
        private static bool IsList(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj is IList &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}