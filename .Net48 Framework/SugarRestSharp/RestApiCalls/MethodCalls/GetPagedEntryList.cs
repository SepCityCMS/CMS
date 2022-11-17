// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="GetPagedEntryList.cs" company="SugarCRM + PocoGen + REST">
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
    /// Represents the GetPagedEntryList class
    /// </summary>
    internal static class GetPagedEntryList
    {
        /// <summary>
        /// Gets entries [SugarCRM REST method - get_entry_list]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="selectFields">Selected field list</param>
        /// <param name="queryString">Formatted query string.</param>
        /// <param name="currentPage">The current page number</param>
        /// <param name="numberPerPage">The number of pages per page</param>
        /// <returns>CreateEntryResponse object</returns>
        public static ReadEntryListResponse Run(string sessionId, string url, string moduleName, List<string> selectFields, string queryString, int currentPage, int numberPerPage)
        {
            var readEntryPagedResponse = new ReadEntryListResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    query = queryString,
                    order_by = string.Empty,
                    offset = (currentPage - 1) * numberPerPage,
                    select_fields = selectFields,
                    link_name_to_fields_array = string.Empty,
                    max_results = numberPerPage,
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
                    readEntryPagedResponse = JsonConverterHelper.Deserialize<ReadEntryListResponse>(content);
                    readEntryPagedResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    readEntryPagedResponse.StatusCode = response.StatusCode;
                    readEntryPagedResponse.Error = ErrorResponse.Format(response);
                }

                readEntryPagedResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                readEntryPagedResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                readEntryPagedResponse.StatusCode = HttpStatusCode.InternalServerError;
                readEntryPagedResponse.Error = ErrorResponse.Format(exception, content);
            }

            return readEntryPagedResponse;
        }
    }
}