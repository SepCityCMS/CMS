// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="SugarRestRequest.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents SugarRestRequest class
    /// </summary>
    public class SugarRestRequest
    {
        /// <summary>
        /// The validation message
        /// </summary>
        private string validationMessage;

        /// <summary>
        /// Initializes a new instance of the SugarRestRequest class.
        /// </summary>
        public SugarRestRequest()
        {
            this.Options = new Options();
            this.validationMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the SugarRestRequest class.
        /// </summary>
        /// <param name="moduleName">The SugarCRM module name.</param>
        public SugarRestRequest(string moduleName)
        {
            this.ModuleName = moduleName;
            this.Options = new Options();
            this.validationMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the SugarRestRequest class.
        /// </summary>
        /// <param name="requestType">The request type.</param>
        public SugarRestRequest(RequestType requestType)
        {
            this.RequestType = requestType;
            this.Options = new Options();
            this.validationMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the SugarRestRequest class.
        /// </summary>
        /// <param name="moduleName">The SugarCRM module name.</param>
        /// <param name="requestType">The request type.</param>
        public SugarRestRequest(string moduleName, RequestType requestType)
        {
            this.ModuleName = moduleName;
            this.RequestType = requestType;
            this.Options = new Options();
            this.validationMessage = string.Empty;
        }

        /// <summary>
        /// Gets or sets SugarCRM REST API Url.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets REST API Username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets REST API Password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets SugarCRM module name
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets SugarCRM module name.
        /// </summary>
        /// <value>The type of the request.</value>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// Gets or sets request parameter - can be identifier, entity or entities data.
        /// Parameter type set for the following request type:
        /// ReadById - Identifier (Id)
        /// BulkRead - null (Set options if needed.)
        /// PagedRead - null (Set options if needed.)
        /// Create - Entity
        /// BulkCreate - Entity collection
        /// Update - Entity
        /// BulkUpdate - Entity collection
        /// Delete - Identifier (Id)
        /// LinkedReadById - Identifier (Id) (Linked option value must be set.)
        /// LinkedBulkRead - null (Linked option value must be set.)
        /// </summary>
        /// <value>The parameter.</value>
        public object Parameter { get; set; }

        /// <summary>
        /// Gets or sets options object.
        /// </summary>
        /// <value>The options.</value>
        public Options Options { get; set; }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <value>The validation message.</value>
        public string ValidationMessage
        {
            get { return this.validationMessage; }
        }

        /// <summary>
        /// Gets a value indicating whether the request is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                var builder = new StringBuilder();

                try
                {
                    if (string.IsNullOrWhiteSpace(this.Url))
                    {
                        builder.AppendLine(ErrorCodes.UrlInvalid);
                    }

                    if (string.IsNullOrWhiteSpace(this.Username))
                    {
                        builder.AppendLine(ErrorCodes.UsernameInvalid);
                    }

                    if (string.IsNullOrWhiteSpace(this.Password))
                    {
                        builder.AppendLine(ErrorCodes.PasswordInvalid);
                    }

                    if (string.IsNullOrWhiteSpace(this.ModuleName))
                    {
                        builder.AppendLine(ErrorCodes.ModulenameInvalid);
                    }

                    switch (RequestType)
                    {
                        case RequestType.ReadById:
                        case RequestType.Delete:
                            if (this.Parameter == null)
                            {
                                builder.AppendLine(ErrorCodes.IdInvalid);
                            }

                            break;

                        case RequestType.Create:
                        case RequestType.BulkCreate:
                        case RequestType.BulkUpdate:
                            if (this.Parameter == null)
                            {
                                builder.AppendLine(ErrorCodes.DataInvalid);
                            }

                            break;

                        case RequestType.LinkedReadById:
                            if (this.Parameter == null)
                            {
                                builder.AppendLine(ErrorCodes.IdInvalid);
                            }

                            if ((Options.LinkedModules == null) || (Options.LinkedModules.Count == 0))
                            {
                                builder.AppendLine(ErrorCodes.LinkedFieldsInfoMissing);
                            }

                            break;

                        case RequestType.LinkedBulkRead:
                            if ((Options.LinkedModules == null) || (Options.LinkedModules.Count == 0))
                            {
                                builder.AppendLine(ErrorCodes.LinkedFieldsInfoMissing);
                            }

                            break;

                        case RequestType.BulkRead:
                            break;

                        case RequestType.PagedRead:
                            break;

                        case RequestType.Update:
                            break;
                    }
                }
                catch (Exception)
                {
                }

                this.validationMessage = builder.ToString();
                return string.IsNullOrWhiteSpace(this.validationMessage);
            }
        }
    }
}