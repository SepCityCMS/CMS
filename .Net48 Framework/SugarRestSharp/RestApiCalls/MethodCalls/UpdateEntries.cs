// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="UpdateEntries.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.MethodCalls
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Responses;
    using RestSharp;
    using SugarRestSharp.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Represents the UpdateEntries class
    /// </summary>
    internal static class UpdateEntries
    {
        /// <summary>
        /// Updates entry [SugarCRM REST method - set_entries]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="entities">The entity objects collection to create</param>
        /// <param name="selectFields">Selected field list</param>
        /// <returns>UpdateEntriesResponse object</returns>
        public static UpdateEntriesResponse Run(string sessionId, string url, string moduleName, JArray entities, List<string> selectFields)
        {
            var updateEntriesResponse = new UpdateEntriesResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    name_value_lists = EntityToNameValueList(entities, selectFields)
                };

                var client = new RestClient(url);
                var request = new RestRequest(string.Empty, Method.POST);
                string JSONData = JsonConvert.SerializeObject(data);

                request.AddParameter("method", "set_entries");
                request.AddParameter("input_type", "JSON");
                request.AddParameter("response_type", "JSON");
                request.AddParameter("rest_data", JSONData);

                var sugarApiRestResponse = client.ExecuteEx(request);
                var response = sugarApiRestResponse.RestResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    content = response.Content;
                    updateEntriesResponse = JsonConverterHelper.Deserialize<UpdateEntriesResponse>(content);
                    updateEntriesResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    updateEntriesResponse.StatusCode = response.StatusCode;
                    updateEntriesResponse.Error = ErrorResponse.Format(response);
                }

                updateEntriesResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                updateEntriesResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                updateEntriesResponse.StatusCode = HttpStatusCode.InternalServerError;
                updateEntriesResponse.Error = ErrorResponse.Format(exception, content);
            }

            return updateEntriesResponse;
        }

        /// <summary>
        /// Format entity to JSON friendly dynamic object
        /// </summary>
        /// <param name="entities">The entity objects collection to create</param>
        /// <param name="selectFields">Selected field list</param>
        /// <returns>List of name value as object</returns>
        private static List<object> EntityToNameValueList(JArray entities, List<string> selectFields)
        {
            bool useSelectedFields = (selectFields != null) && (selectFields.Count > 0);
            var entityObjectList = new List<object>();

            foreach (var entity in entities)
            {
                var entityObject = new Dictionary<string, object>();
                var jproperties = ((JObject)entity).Properties().ToList();
                foreach (JProperty jproperty in jproperties)
                {
                    string name = jproperty.Name;

                    if (useSelectedFields)
                    {
                        // The identifier must always be added.
                        if (string.Compare("id", name, StringComparison.CurrentCultureIgnoreCase) != 0)
                        {
                            if (selectFields.All(x => x.ToLower() != name.ToLower()))
                            {
                                continue;
                            }
                        }
                    }

                    object value = jproperty.Value;

                    var namevalueDic = new Dictionary<string, object>();
                    namevalueDic.Add("name", name);
                    namevalueDic.Add("value", value);

                    entityObject.Add(name, namevalueDic);
                }

                entityObjectList.Add(entityObject);
            }

            return entityObjectList;
        }
    }
}