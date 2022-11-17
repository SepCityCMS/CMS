// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="GetEntry.cs" company="SugarCRM + PocoGen + REST">
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
    /// Represents the GetEntry class
    /// </summary>
    internal static class GetEntry
    {
        /// <summary>
        /// Gets entry [SugarCRM REST method - get_entry]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="identifier">The entity identifier</param>
        /// <param name="selectFields">Selected field list</param>
        /// <returns>ReadEntryResponse object</returns>
        public static ReadEntryResponse Run(string sessionId, string url, string moduleName, string identifier, List<string> selectFields)
        {
            var readEntryResponse = new ReadEntryResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    id = identifier,
                    select_fields = selectFields,
                    link_name_to_fields_array = string.Empty,
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
                    readEntryResponse = JsonConverterHelper.Deserialize<ReadEntryResponse>(content);
                    readEntryResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    readEntryResponse.StatusCode = response.StatusCode;
                    readEntryResponse.Error = ErrorResponse.Format(response);
                }

                readEntryResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                readEntryResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                readEntryResponse.StatusCode = HttpStatusCode.InternalServerError;
                readEntryResponse.Error = ErrorResponse.Format(exception, content);
            }

            return readEntryResponse;
        }
    }
}