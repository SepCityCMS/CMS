// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="GetLinkedEntry.cs" company="SugarCRM + PocoGen + REST">
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
    /// Represents the GetLinkedEntry class
    /// </summary>
    internal static class GetLinkedEntry
    {
        /// <summary>
        /// Gets entry [SugarCRM REST method - get_entry]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="identifier">The entity identifier</param>
        /// <param name="selectFields">Selected field list</param>
        /// <param name="linkedSelectFields">Linked field info.</param>
        /// <returns>ReadLinkedEntryResponse object</returns>
        public static ReadLinkedEntryResponse Run(string sessionId, string url, string moduleName, string identifier, List<string> selectFields, Dictionary<string, List<string>> linkedSelectFields)
        {
            var readLinkedEntryResponse = new ReadLinkedEntryResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    id = identifier,
                    select_fields = selectFields,
                    link_name_to_fields_array = LinkedInfoToLinkedFieldsList(linkedSelectFields),
                    track_view = false
                };

                var client = new RestClient(url);
                var request = new RestRequest(string.Empty, Method.POST);
                string JSONData = JsonConvert.SerializeObject(data);

                request.AddParameter("method", "get_entry");
                request.AddParameter("input_type", "JSON");
                request.AddParameter("response_type", "JSON");
                request.AddParameter("rest_data", JSONData);

                var sugarApiRestResponse = client.ExecuteEx(request);
                var response = sugarApiRestResponse.RestResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    content = response.Content;
                    readLinkedEntryResponse = JsonConverterHelper.Deserialize<ReadLinkedEntryResponse>(content);
                    readLinkedEntryResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    readLinkedEntryResponse.StatusCode = response.StatusCode;
                    readLinkedEntryResponse.Error = ErrorResponse.Format(response);
                }

                readLinkedEntryResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                readLinkedEntryResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                readLinkedEntryResponse.StatusCode = HttpStatusCode.InternalServerError;
                readLinkedEntryResponse.Error = ErrorResponse.Format(exception, content);
            }

            return readLinkedEntryResponse;
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