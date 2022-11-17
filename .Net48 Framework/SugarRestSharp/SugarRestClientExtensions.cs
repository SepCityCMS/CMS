// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="SugarRestClientExtensions.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using Helpers;
    using MethodCalls;
    using Newtonsoft.Json.Linq;
    using Requests;
    using Responses;
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represent SugarRestClientExtensions class.
    /// </summary>
    internal static class SugarRestClientExtensions
    {
        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteGetById(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                var selectFields = request.Options == null ? new List<string>() : request.Options.SelectFields;
                selectFields = modelInfo.GetJsonPropertyNames(selectFields);
                var readEntryResponse = GetEntry.Run(loginResponse.SessionId, request.Url, request.ModuleName, request.Parameter.ToString(), selectFields);

                if (readEntryResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = readEntryResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = readEntryResponse.JSONRawResponse;

                    var JSONEnityList = readEntryResponse.Entity;
                    if (JSONEnityList != null)
                    {
                        sugarRestResponse.JData = readEntryResponse.Entity.ToString();
                        sugarRestResponse.StatusCode = readEntryResponse.StatusCode;
                        sugarRestResponse.Data = sugarRestResponse.JData.ToObject(modelInfo.Type);
                    }
                    else
                    {
                        sugarRestResponse.Error = readEntryResponse.Error;
                        sugarRestResponse.StatusCode = readEntryResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Gets entity by id - returned data with linked modules data.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteLinkedGetById(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                var selectFields = request.Options == null ? new List<string>() : request.Options.SelectFields;
                var linkedFields = request.Options == null ? null : request.Options.LinkedModules;
                selectFields = modelInfo.GetJsonPropertyNames(selectFields);
                Dictionary<string, List<string>> linkedSelectFields = modelInfo.GetJSONLinkedInfo(linkedFields);
                var readEntryResponse = GetLinkedEntry.Run(loginResponse.SessionId, request.Url, request.ModuleName, request.Parameter.ToString(), selectFields, linkedSelectFields);

                if (readEntryResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = readEntryResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = readEntryResponse.JSONRawResponse;

                    var JSONEnityList = readEntryResponse.Entity;
                    if (JSONEnityList != null)
                    {
                        sugarRestResponse.JData = readEntryResponse.Entity.ToString();
                        sugarRestResponse.StatusCode = readEntryResponse.StatusCode;
                        sugarRestResponse.Data = null;
                    }
                    else
                    {
                        sugarRestResponse.Error = readEntryResponse.Error;
                        sugarRestResponse.StatusCode = readEntryResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Gets all entities limited by MaxResultCount sets in request options.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteGetAll(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);
                var selectFields = modelInfo.GetJsonPropertyNames(request.Options.SelectFields);
                string query = modelInfo.GetQuery(request.Options.QueryPredicates, request.Options.Query);

                var readEntryListResponse = GetEntryList.Run(loginResponse.SessionId, request.Url, request.ModuleName, selectFields, query, request.Options.MaxResult);

                if (readEntryListResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = readEntryListResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = readEntryListResponse.JSONRawResponse;

                    var JSONEnityList = readEntryListResponse.EntityList;
                    if (JSONEnityList != null)
                    {
                        sugarRestResponse.JData = readEntryListResponse.EntityList.ToString();
                        sugarRestResponse.StatusCode = readEntryListResponse.StatusCode;
                        sugarRestResponse.Data = sugarRestResponse.JData.ToObjects(modelInfo.Type);
                    }
                    else
                    {
                        sugarRestResponse.Error = readEntryListResponse.Error;
                        sugarRestResponse.StatusCode = readEntryListResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Gets all entities limited by MaxResultCount sets in request options.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteLinkedGetAll(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                var selectFields = request.Options == null ? new List<string>() : request.Options.SelectFields;
                var linkedFields = request.Options == null ? null : request.Options.LinkedModules;
                selectFields = modelInfo.GetJsonPropertyNames(selectFields);
                Dictionary<string, List<string>> linkedSelectFields = modelInfo.GetJSONLinkedInfo(linkedFields);
                string query = modelInfo.GetQuery(request.Options.QueryPredicates, request.Options.Query);

                var readLinkedEntryListResponse = GetLinkedEntryList.Run(loginResponse.SessionId, request.Url, request.ModuleName, selectFields, linkedSelectFields, query, request.Options.MaxResult);

                if (readLinkedEntryListResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = readLinkedEntryListResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = readLinkedEntryListResponse.JSONRawResponse;

                    var JSONEnityList = readLinkedEntryListResponse.EntityList;
                    if (JSONEnityList != null)
                    {
                        sugarRestResponse.JData = readLinkedEntryListResponse.EntityList.ToString();
                        sugarRestResponse.StatusCode = readLinkedEntryListResponse.StatusCode;
                        sugarRestResponse.Data = null;
                    }
                    else
                    {
                        sugarRestResponse.Error = readLinkedEntryListResponse.Error;
                        sugarRestResponse.StatusCode = readLinkedEntryListResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Gets all entities by page. Page options set in request options.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteGetPaged(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();
            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);
                var selectFields = modelInfo.GetJsonPropertyNames(request.Options.SelectFields);
                string query = modelInfo.GetQuery(request.Options.QueryPredicates, request.Options.Query);

                var readEntryListResponse = GetPagedEntryList.Run(loginResponse.SessionId, loginRequest.Url, request.ModuleName, selectFields, query, request.Options.CurrentPage, request.Options.NumberPerPage);

                if (readEntryListResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = readEntryListResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = readEntryListResponse.JSONRawResponse;

                    var JSONEnityList = readEntryListResponse.EntityList;
                    if (JSONEnityList != null)
                    {
                        sugarRestResponse.JData = readEntryListResponse.EntityList.ToString();
                        sugarRestResponse.StatusCode = readEntryListResponse.StatusCode;
                        sugarRestResponse.Data = sugarRestResponse.JData.ToObjects(modelInfo.Type);
                    }
                    else
                    {
                        sugarRestResponse.Error = readEntryListResponse.Error;
                        sugarRestResponse.StatusCode = readEntryListResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Insert entity.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteInsert(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                JObject jobject = JsonConverterHelper.Serialize(request.Parameter, modelInfo.Type);
                var selectFields = modelInfo.GetJsonPropertyNames(request.Options.SelectFields);
                var insertEntryResponse = InsertEntry.Run(loginResponse.SessionId, loginRequest.Url, request.ModuleName, jobject, selectFields);

                if (insertEntryResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = insertEntryResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = insertEntryResponse.JSONRawResponse;

                    if (!string.IsNullOrWhiteSpace(insertEntryResponse.Id))
                    {
                        sugarRestResponse.Data = insertEntryResponse.Id;
                        sugarRestResponse.JData = JToken.FromObject(insertEntryResponse.Id).ToString();
                        sugarRestResponse.StatusCode = insertEntryResponse.StatusCode;
                    }
                    else
                    {
                        sugarRestResponse.Error = insertEntryResponse.Error;
                        sugarRestResponse.StatusCode = insertEntryResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Insert entities.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteInserts(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                JArray objectList = JsonConverterHelper.SerializeList(request.Parameter, modelInfo.Type);
                var selectFields = modelInfo.GetJsonPropertyNames(request.Options.SelectFields);
                var insertEntriesResponse = InsertEntries.Run(loginResponse.SessionId, loginRequest.Url, request.ModuleName, objectList, selectFields);

                if (insertEntriesResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = insertEntriesResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = insertEntriesResponse.JSONRawResponse;

                    if ((insertEntriesResponse.Ids != null) && (insertEntriesResponse.Ids.Count > 0))
                    {
                        sugarRestResponse.Data = insertEntriesResponse.Ids;
                        sugarRestResponse.JData = JArray.FromObject(insertEntriesResponse.Ids).ToString();
                        sugarRestResponse.StatusCode = insertEntriesResponse.StatusCode;
                    }
                    else
                    {
                        sugarRestResponse.Error = insertEntriesResponse.Error;
                        sugarRestResponse.StatusCode = insertEntriesResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteUpdate(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                JObject jobject = JsonConverterHelper.Serialize(request.Parameter, modelInfo.Type);
                var selectFields = modelInfo.GetJsonPropertyNames(request.Options.SelectFields);
                var updateEntryResponse = UpdateEntry.Run(loginResponse.SessionId, loginRequest.Url, request.ModuleName, jobject, selectFields);

                if (updateEntryResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = updateEntryResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = updateEntryResponse.JSONRawResponse;

                    if (!string.IsNullOrWhiteSpace(updateEntryResponse.Id))
                    {
                        sugarRestResponse.Data = updateEntryResponse.Id;
                        sugarRestResponse.JData = JToken.FromObject(updateEntryResponse.Id).ToString();
                        sugarRestResponse.StatusCode = updateEntryResponse.StatusCode;
                    }
                    else
                    {
                        sugarRestResponse.Error = updateEntryResponse.Error;
                        sugarRestResponse.StatusCode = updateEntryResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Update entities.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteUpdates(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                JArray objectList = JsonConverterHelper.SerializeList(request.Parameter, modelInfo.Type);
                var selectFields = modelInfo.GetJsonPropertyNames(request.Options.SelectFields);
                var updateEntriesResponse = UpdateEntries.Run(loginResponse.SessionId, loginRequest.Url, request.ModuleName, objectList, selectFields);

                if (updateEntriesResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = updateEntriesResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = updateEntriesResponse.JSONRawResponse;

                    if ((updateEntriesResponse.Ids != null) && (updateEntriesResponse.Ids.Count > 0))
                    {
                        sugarRestResponse.Data = updateEntriesResponse.Ids;
                        sugarRestResponse.JData = JArray.FromObject(updateEntriesResponse.Ids).ToString();
                        sugarRestResponse.StatusCode = updateEntriesResponse.StatusCode;
                    }
                    else
                    {
                        sugarRestResponse.Error = updateEntriesResponse.Error;
                        sugarRestResponse.StatusCode = updateEntriesResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="client">SugarRestClient object.</param>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The entity model info.</param>
        /// <returns>SugarRestResponse object.</returns>
        public static SugarRestResponse ExecuteDelete(this SugarRestClient client, SugarRestRequest request, ModelInfo modelInfo)
        {
            var sugarRestResponse = new SugarRestResponse();
            var loginResponse = new LoginResponse();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Url = request.Url,
                    Username = request.Username,
                    Password = request.Password
                };

                loginResponse = Authentication.Login(loginRequest);

                var deleteEntryResponse = DeleteEntry.Run(loginResponse.SessionId, loginRequest.Url, request.ModuleName, request.Parameter.ToString());

                if (deleteEntryResponse != null)
                {
                    sugarRestResponse.JSONRawRequest = deleteEntryResponse.JSONRawRequest;
                    sugarRestResponse.JSONRawResponse = deleteEntryResponse.JSONRawResponse;

                    if (!string.IsNullOrWhiteSpace(deleteEntryResponse.Id))
                    {
                        sugarRestResponse.Data = deleteEntryResponse.Id;
                        sugarRestResponse.JData = JToken.FromObject(deleteEntryResponse.Id).ToString();
                        sugarRestResponse.StatusCode = deleteEntryResponse.StatusCode;
                    }
                    else
                    {
                        sugarRestResponse.Error = deleteEntryResponse.Error;
                        sugarRestResponse.StatusCode = deleteEntryResponse.StatusCode;
                    }
                }

                return sugarRestResponse;
            }
            catch (Exception exception)
            {
                sugarRestResponse.StatusCode = HttpStatusCode.InternalServerError;
                sugarRestResponse.Error = ErrorResponse.Format(exception, string.Empty);
            }
            finally
            {
                Authentication.Logout(request.Url, loginResponse.SessionId);
            }

            return sugarRestResponse;
        }
    }
}