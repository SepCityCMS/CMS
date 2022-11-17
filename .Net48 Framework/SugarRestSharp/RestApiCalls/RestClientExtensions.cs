// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="RestClientExtensions.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents  RestClientExtensions class.
    /// </summary>
    internal static class RestClientExtensions
    {
        /// <summary>
        /// Execute request.
        /// </summary>
        /// <param name="client">SugarRestClient object</param>
        /// <param name="request">The request object</param>
        /// <returns>SugarRestResponse object</returns>
        public static SugarApiRestResponse ExecuteEx(this IRestClient client, IRestRequest request)
        {
            var sugarApiRestResponse = new SugarApiRestResponse();
            IRestResponse response = null;

            try
            {
                response = client.Execute(request);
                sugarApiRestResponse.RestResponse = response;
            }
            catch (Exception)
            {
            }
            finally
            {
                GetRawRequest(client, sugarApiRestResponse, request, response);
            }

            return sugarApiRestResponse;
        }

        /// <summary>
        /// Gets raw JSON request data.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="sugarApiRestResponse">Response object returned by RestSharp.</param>
        /// <param name="request">The request object.</param>
        /// <param name="response">The response object.</param>
        private static void GetRawRequest(IRestClient client, SugarApiRestResponse sugarApiRestResponse, IRestRequest request, IRestResponse response)
        {
            var requestJSON = new
            {
                resource = request.Resource,

                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),

                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),

                // This will generate the actual Uri used in the request
                uri = client.BuildUri(request),
            };

            var responseJSON = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,

                // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            string JSONRawRequest = JsonConvert.SerializeObject(requestJSON);
            string JSONRawResponse = JsonConvert.SerializeObject(responseJSON);

            sugarApiRestResponse.JSONRawRequest = JSONRawRequest;
            sugarApiRestResponse.JSONRawResponse = JSONRawResponse;
        }
    }
}