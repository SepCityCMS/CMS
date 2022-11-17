// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="LoginResponse.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.Responses
{
    /// <summary>
    /// Represents the LoginResponse class
    /// </summary>
    internal class LoginResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the session identifier
        /// </summary>
        /// <value>The session identifier.</value>
        public string SessionId { get; set; }
    }
}