// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="GetLinkedEntryList.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.MethodCalls
{
    using Helpers;
    using Newtonsoft.Json;
    using Responses;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents the GetLinkedEntryList class
    /// </summary>
    internal static class GetLinkedEntryList
    {
        /// <summary>
        /// Gets entries [SugarCRM REST method - get_entry_list]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="selectFields">Selected field list</param>
        /// <param name="linkedSelectFields">Linked field info.</param>
        /// <param name="queryString">Formatted query string.</param>
        /// <param name="maxCountResult">Maximum number of entries to return</param>
        /// <returns>ReadLinkedEntryListResponse object</returns>
        public static ReadLinkedEntryListResponse Run(string sessionId, string url, string moduleName, List<string> selectFields, Dictionary<string, List<string>> linkedSelectFields, string queryString, int maxCountResult)
        {
            var readLinkedEntryListResponse = new ReadLinkedEntryListResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    query = queryString,
                    order_by = string.Empty,
                    offset = 0,
                    select_fields = selectFields,
                    link_name_to_fields_array = LinkedInfoToLinkedFieldsList(linkedSelectFields),
                    max_results = maxCountResult,
                    deleted = 0,
                    favorites = false
                };

                var client = new RestClient(url);

                var request = new RestRequest(string.Empty, Method.POST);
                string JSONData = JsonConvert.SerializeObject(data);

                request.AddParameter("method", "get_entry_list");
                request.AddParameter("input_type", "JSON");
                request.AddParameter("response_type", "JSON");
                request.AddParameter("rest_data", JSONData);

                var sugarApiRestResponse = client.ExecuteEx(request);
                var response = sugarApiRestResponse.RestResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    content = response.Content;
                    readLinkedEntryListResponse = JsonConverterHelper.Deserialize<ReadLinkedEntryListResponse>(content);
                    readLinkedEntryListResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    readLinkedEntryListResponse.StatusCode = response.StatusCode;
                    readLinkedEntryListResponse.Error = ErrorResponse.Format(response);
                }

                readLinkedEntryListResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                readLinkedEntryListResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                readLinkedEntryListResponse.StatusCode = HttpStatusCode.InternalServerError;
                readLinkedEntryListResponse.Error = ErrorResponse.Format(exception, content);
            }

            return readLinkedEntryListResponse;
        }

        /// <summary>
        /// Format linked list info to JSON friendly dynamic object
        /// </summary>
        /// <param name="linkedSelectFields">Linked field info.</param>
        /// <returns>List of linked name value as object.</returns>
        private static List<object> LinkedInfoToLinkedFieldsList(Dictionary<string, List<string>> linkedSelectFields)
        {
            var linkedListInfo = new List<object>();
            foreach (var item in linkedSelectFields)
            {
                var namevalueDic = new Dictionary<string, object>();
                namevalueDic.Add("name", item.Key);
                namevalueDic.Add("value", item.Value);

                linkedListInfo.Add(namevalueDic);
            }

            return linkedListInfo;
        }
    }
}