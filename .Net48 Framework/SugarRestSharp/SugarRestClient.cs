// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="SugarRestClient.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using Responses;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents SugarRestClient class
    /// </summary>
    public class SugarRestClient
    {
        /// <summary>
        /// The URL
        /// </summary>
        private string url;
        /// <summary>
        /// The username
        /// </summary>
        private string username;
        /// <summary>
        /// The password
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes a new instance of the SugarRestClient class.
        /// </summary>
        public SugarRestClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SugarRestClient class.
        /// </summary>
        /// <param name="url">SugarCRM REST API url.</param>
        public SugarRestClient(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// Initializes a new instance of the SugarRestClient class.
        /// </summary>
        /// <param name="url">SugarCRM REST API Url.</param>
        /// <param name="username">SugarCRM REST API Username.</param>
        /// <param name="password">SugarCRM REST API Password.</param>
        public SugarRestClient(string url, string username, string password)
        {
            this.url = url;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Execute client.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>SugarRestResponse object.</returns>
        public SugarRestResponse Execute(SugarRestRequest request)
        {
            SugarRestResponse response = new SugarRestResponse();
            if (!this.IsRequestValidate(ref request, ref response))
            {
                return response;
            }

            ModelInfo modelInfo = ModelInfo.ReadByName(request.ModuleName);
            return this.InternalExceute(request, modelInfo);
        }

        /// <summary>
        /// Execute client based on entity type.
        /// </summary>
        /// <typeparam name="TEntity">Entity type of EntityBase type</typeparam>
        /// <param name="request">The request object</param>
        /// <returns>SugarRestResponse object.</returns>
        public SugarRestResponse Execute<TEntity>(SugarRestRequest request) where TEntity : EntityBase
        {
            ModelInfo modelInfo = ModelInfo.ReadByType(typeof(TEntity));
            request.ModuleName = modelInfo.ModelName;

            SugarRestResponse response = new SugarRestResponse();
            if (!this.IsRequestValidate(ref request, ref response))
            {
                return response;
            }

            return this.InternalExceute(request, modelInfo);
        }

        /// <summary>
        /// Execute request asynchronously using SugarCRM module name.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>SugarRestResponse object.</returns>
        public async Task<SugarRestResponse> ExecuteAsync(SugarRestRequest request)
        {
            SugarRestResponse response = new SugarRestResponse();
            if (!this.IsRequestValidate(ref request, ref response))
            {
                return response;
            }

            ModelInfo modelInfo = ModelInfo.ReadByName(request.ModuleName);
            return await Task.Run(() => { return this.InternalExceute(request, modelInfo); });
        }

        /// <summary>
        /// Execute request asynchronously using the C# SugarCRM model type.
        /// </summary>
        /// <typeparam name="TEntity">The template parameter.</typeparam>
        /// <param name="request">The request object.</param>
        /// <returns>SugarRestResponse object.</returns>
        public async Task<SugarRestResponse> ExecuteAsync<TEntity>(SugarRestRequest request) where TEntity : EntityBase
        {
            ModelInfo modelInfo = ModelInfo.ReadByType(typeof(TEntity));
            request.ModuleName = modelInfo.ModelName;

            SugarRestResponse response = new SugarRestResponse();
            if (!this.IsRequestValidate(ref request, ref response))
            {
                return response;
            }

            return await Task.Run(() => { return this.InternalExceute(request, modelInfo); });
        }

        /// <summary>
        /// Execute request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="modelInfo">The model info for the referenced SugarCRM module.</param>
        /// <returns>SugarRestResponse object.</returns>
        /// <exception cref="Exception">Request type is invalid!</exception>
        private SugarRestResponse InternalExceute(SugarRestRequest request, ModelInfo modelInfo)
        {
            switch (request.RequestType)
            {
                case RequestType.ReadById:
                    {
                        return this.ExecuteGetById(request, modelInfo);
                    }

                case RequestType.BulkRead:
                    {
                        return this.ExecuteGetAll(request, modelInfo);
                    }

                case RequestType.PagedRead:
                    {
                        return this.ExecuteGetPaged(request, modelInfo);
                    }

                case RequestType.Create:
                    {
                        return this.ExecuteInsert(request, modelInfo);
                    }

                case RequestType.BulkCreate:
                    {
                        return this.ExecuteInserts(request, modelInfo);
                    }

                case RequestType.Update:
                    {
                        return this.ExecuteUpdate(request, modelInfo);
                    }

                case RequestType.BulkUpdate:
                    {
                        return this.ExecuteUpdates(request, modelInfo);
                    }

                case RequestType.Delete:
                    {
                        return this.ExecuteDelete(request, modelInfo);
                    }

                case RequestType.LinkedReadById:
                    {
                        return this.ExecuteLinkedGetById(request, modelInfo);
                    }

                case RequestType.LinkedBulkRead:
                    {
                        return this.ExecuteLinkedGetAll(request, modelInfo);
                    }
            }

            throw new Exception("Request type is invalid!");
        }

        /// <summary>
        /// Method checks if request is valid.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="response">The response object.</param>
        /// <returns>True or false.</returns>
        private bool IsRequestValidate(ref SugarRestRequest request, ref SugarRestResponse response)
        {
            if (request == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ErrorResponse.Format("Request is invalid.");
                return false;
            }

            request.Url = string.IsNullOrWhiteSpace(request.Url) ? this.url : request.Url;
            request.Username = string.IsNullOrWhiteSpace(request.Username) ? this.username : request.Username;
            request.Password = string.IsNullOrWhiteSpace(request.Password) ? this.password : request.Password;

            if (!request.IsValid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ErrorResponse.Format(request.ValidationMessage);
                return false;
            }

            return true;
        }
    }
}