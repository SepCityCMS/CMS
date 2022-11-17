// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-11-2019
// ***********************************************************************
// <copyright file="Clients.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    /// <summary>
    /// Class Clients.
    /// </summary>
    public static class Clients
    {
        /// <summary>
        /// Gets or sets the client1.
        /// </summary>
        /// <value>The client1.</value>
        public static Client Client1 { get; set; }

        /// <summary>
        /// Gets or sets the client2.
        /// </summary>
        /// <value>The client2.</value>
        public static Client Client2 { get; set; }
    }
}

namespace wwwroot
{
    /// <summary>
    /// Class Client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>The redirect URL.</value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }
    }
}