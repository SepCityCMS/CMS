// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-02-2019
// ***********************************************************************
// <copyright file="Login.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    /// <summary>
    /// Class Login.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
    }

    /// <summary>
    /// Class AccessTokenInfo.
    /// </summary>
    public class AccessTokenInfo
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }
    }
}