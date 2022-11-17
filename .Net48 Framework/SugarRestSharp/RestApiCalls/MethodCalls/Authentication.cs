// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Authentication.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.MethodCalls
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Requests;
    using Responses;
    using RestSharp;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Base Authentication class for SugarCRM REST API calls
    /// </summary>
    internal static class Authentication
    {
        /// <summary>
        /// Login to SugarCRM via REST API call
        /// </summary>
        /// <param name="loginRequest">LoginRequest object</param>
        /// <returns>LoginResponse object</returns>
        public static LoginResponse Login(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse();

            dynamic authData = new
            {
                user_auth =
                        new
                        {
                            user_name = loginRequest.Username,
                            password = Util.CalculateMd5Hash(loginRequest.Password)
                        },
                application_name = "RestClient"
            };

            var client = new RestClient(loginRequest.Url);
            var request = new RestRequest(string.Empty, Method.POST);

            request.AddParameter("method", "login");
            request.AddParameter("input_type", "JSON");
            request.AddParameter("response_type", "JSON");
            request.AddParameter("rest_data", JsonConvert.SerializeObject(authData));

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;

                JObject JSONObj = JObject.Parse(content);
                IList<string> keys = JSONObj.Properties().Select(p => p.Name).ToList();
                if (keys.Any(n => n == "id"))
                {
                    JProperty property = JSONObj.Properties().SingleOrDefault(p => p.Name == "id");
                    if (property != null)
                    {
                        string value = property.Value.ToString();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            loginResponse.StatusCode = response.StatusCode;
                            loginResponse.SessionId = value;
                        }
                    }
                }
                else
                {
                    loginResponse.StatusCode = response.StatusCode;
                    loginResponse.Error = JSONObj.ToObject<ErrorResponse>();
                }
            }
            else
            {
                loginResponse.StatusCode = HttpStatusCode.InternalServerError;
                loginResponse.Error = ErrorResponse.Format(response);
            }

            return loginResponse;
        }

        /// <summary>
        /// Gets current session
        /// </summary>
        /// <param name="url">REST API Url</param>
        /// <param name="sessionId">Session identifier</param>
        /// <returns>LoginResponse object</returns>
        public static LoginResponse GetCurrentSession(string url, string sessionId)
        {
            var sessionResponse = new LoginResponse();

            dynamic currentSession = new
            {
                session = sessionId
            };

            var client = new RestClient(url);

            var request = new RestRequest(string.Empty, Method.POST);

            request.AddParameter("method", "oauth_access");
            request.AddParameter("input_type", "JSON");
            request.AddParameter("response_type", "JSON");
            request.AddParameter("rest_data", JsonConvert.SerializeObject(currentSession));

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;

                var JSONObj = JObject.Parse(content);
                var keys = JSONObj.Properties().Select(p => p.Name).ToList();
                if (keys.Any(n => n == "id"))
                {
                    JProperty property = JSONObj.Properties().SingleOrDefault(p => p.Name == "id");
                    if (property != null)
                    {
                        string value = property.Value.ToString();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            sessionResponse.StatusCode = response.StatusCode;
                            sessionResponse.SessionId = value;
                        }
                    }
                }
                else
                {
                    sessionResponse.StatusCode = response.StatusCode;
                    sessionResponse.Error = JSONObj.ToObject<ErrorResponse>();
                }
            }
            else
            {
                sessionResponse.StatusCode = HttpStatusCode.InternalServerError;
                sessionResponse.Error = ErrorResponse.Format(response);
            }

            return sessionResponse;
        }

        /// <summary>
        /// Logs out with the session identifier
        /// </summary>
        /// <param name="url">REST API Url</param>
        /// <param name="sessionId">Session identifier</param>
        public static void Logout(string url, string sessionId)
        {
            try
            {
                dynamic currentSession = new
                {
                    session = sessionId
                };

                var client = new RestClient(url);

                var request = new RestRequest(string.Empty, Method.POST);

                request.AddParameter("method", "logout");
                request.AddParameter("input_type", "JSON");
                request.AddParameter("response_type", "JSON");
                request.AddParameter("rest_data", JsonConvert.SerializeObject(currentSession));

                client.Execute(request);
            }
            catch
            {
                // Suppress exception
            }
        }
    }
}