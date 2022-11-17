// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="GetEntryList.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

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
    /// Represents the GetEntryList class
    /// </summary>
    internal static class GetEntryList
    {
        /// <summary>
        /// Gets entries [SugarCRM REST method - get_entry_list]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="selectFields">Selected field list</param>
        /// <param name="queryString">Formatted query string.</param>
        /// <param name="maxCountResult">Maximum number of entries to return</param>
        /// <returns>ReadEntryListResponse object</returns>
        public static ReadEntryListResponse Run(string sessionId, string url, string moduleName, List<string> selectFields, string queryString, int maxCountResult)
        {
            var readEntryListResponse = new ReadEntryListResponse();
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
                    link_name_to_fields_array = string.Empty,
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
                    readEntryListResponse = JsonConverterHelper.Deserialize<ReadEntryListResponse>(content);
                    readEntryListResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    readEntryListResponse.StatusCode = response.StatusCode;
                    readEntryListResponse.Error = ErrorResponse.Format(response);
                }

                readEntryListResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                readEntryListResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                readEntryListResponse.StatusCode = HttpStatusCode.InternalServerError;
                readEntryListResponse.Error = ErrorResponse.Format(exception, content);
            }

            return readEntryListResponse;
        }
    }
}