// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="InsertEntry.cs" company="SugarCRM + PocoGen + REST">
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
    /// Represents the InsertEntry class
    /// </summary>
    internal static class InsertEntry
    {
        /// <summary>
        /// Creates entry [SugarCRM REST method - set_entry]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="entity">The entity object to create</param>
        /// <param name="selectFields">Selected field list</param>
        /// <returns>CreateEntryResponse object</returns>
        public static InsertEntryResponse Run(string sessionId, string url, string moduleName, JObject entity, List<string> selectFields)
        {
            var insertEntryResponse = new InsertEntryResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    name_value_list = EntityToNameValueList(entity, selectFields)
                };

                var client = new RestClient(url);
                var request = new RestRequest(string.Empty, Method.POST);
                string JSONData = JsonConvert.SerializeObject(data);

                request.AddParameter("method", "set_entry");
                request.AddParameter("input_type", "JSON");
                request.AddParameter("response_type", "JSON");
                request.AddParameter("rest_data", JSONData);

                var sugarApiRestResponse = client.ExecuteEx(request);
                var response = sugarApiRestResponse.RestResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    content = response.Content;
                    insertEntryResponse = JsonConverterHelper.Deserialize<InsertEntryResponse>(content);
                    insertEntryResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    insertEntryResponse.StatusCode = response.StatusCode;
                    insertEntryResponse.Error = ErrorResponse.Format(response);
                }

                insertEntryResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                insertEntryResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                insertEntryResponse.StatusCode = HttpStatusCode.InternalServerError;
                insertEntryResponse.Error = ErrorResponse.Format(exception, content);
            }

            return insertEntryResponse;
        }

        /// <summary>
        /// Format entity to JSON friendly dynamic object
        /// </summary>
        /// <param name="entity">The entity object to create</param>
        /// <param name="selectFields">Selected field list</param>
        /// <returns>List of name value as object</returns>
        public static Dictionary<string, object> EntityToNameValueList(JObject entity, List<string> selectFields)
        {
            var namevalueList = new Dictionary<string, object>();

            bool useSelectedFields = (selectFields != null) && (selectFields.Count > 0);
            var jproperties = entity.Properties().ToList();
            foreach (JProperty jproperty in jproperties)
            {
                string name = jproperty.Name;
                if (useSelectedFields)
                {
                    if (selectFields.All(x => x.ToLower() != name.ToLower()))
                    {
                        continue;
                    }
                }

                object value = jproperty.Value;

                if (string.Compare("id", name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    continue;
                }

                var namevalueDic = new Dictionary<string, object>();
                namevalueDic.Add("name", name);
                namevalueDic.Add("value", value);

                namevalueList.Add(name, namevalueDic);
            }

            return namevalueList;
        }
    }
}