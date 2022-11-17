// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="DeleteEntry.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SugarRestSharp.MethodCalls
{
    using Newtonsoft.Json;
    using Responses;
    using RestSharp;
    using SugarRestSharp.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents the DeleteEntry class
    /// </summary>
    internal static class DeleteEntry
    {
        /// <summary>
        /// Deletes entry [SugarCRM REST method -set_entry (sets deleted to 1]
        /// </summary>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="url">REST API Url</param>
        /// <param name="moduleName">SugarCRM module name</param>
        /// <param name="id">The entity identifier</param>
        /// <returns>DeleteEntryResponse object</returns>
        public static DeleteEntryResponse Run(string sessionId, string url, string moduleName, string id)
        {
            var deleteEntryResponse = new DeleteEntryResponse();
            var content = string.Empty;

            try
            {
                dynamic data = new
                {
                    session = sessionId,
                    module_name = moduleName,
                    name_value_list = DeleteDataToNameValueList(id)
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
                    deleteEntryResponse = JsonConverterHelper.Deserialize<DeleteEntryResponse>(content);
                    deleteEntryResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    deleteEntryResponse.StatusCode = response.StatusCode;
                    deleteEntryResponse.Error = ErrorResponse.Format(response);
                }

                deleteEntryResponse.JSONRawRequest = sugarApiRestResponse.JSONRawRequest;
                deleteEntryResponse.JSONRawResponse = sugarApiRestResponse.JSONRawResponse;
            }
            catch (Exception exception)
            {
                deleteEntryResponse.StatusCode = HttpStatusCode.InternalServerError;
                deleteEntryResponse.Error = ErrorResponse.Format(exception, content);
            }

            return deleteEntryResponse;
        }

        /// <summary>
        /// Format request to JSON friendly dynamic object
        /// </summary>
        /// <param name="id">The entity identifier</param>
        /// <returns>List of name value as object</returns>
        private static List<object> DeleteDataToNameValueList(string id)
        {
            var namevalueList = new List<object>();

            var namevalueDic = new Dictionary<string, object>();
            namevalueDic.Add("name", "id");
            namevalueDic.Add("value", id);
            namevalueList.Add(namevalueDic);

            namevalueDic = new Dictionary<string, object>();
            namevalueDic.Add("name", "deleted");
            namevalueDic.Add("value", 1);
            namevalueList.Add(namevalueDic);

            return namevalueList;
        }
    }
}