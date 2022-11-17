// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-17-2019
// ***********************************************************************
// <copyright file="Paths.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    /// <summary>
    /// Class Paths.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets or sets the authorize path.
        /// </summary>
        /// <value>The authorize path.</value>
        public static string AuthorizePath { get; set; }

        /// <summary>
        /// Gets or sets the login path.
        /// </summary>
        /// <value>The login path.</value>
        public static string LoginPath { get; set; }

        /// <summary>
        /// Gets or sets the logout path.
        /// </summary>
        /// <value>The logout path.</value>
        public static string LogoutPath { get; set; }

        /// <summary>
        /// Gets or sets the token path.
        /// </summary>
        /// <value>The token path.</value>
        public static string TokenPath { get; set; }
    }
}