// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="OauthNonce.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591

namespace SugarRestSharp.Models
{
	using System;
	using Newtonsoft.Json;


    /// <summary>
    /// A class which represents the oauth_nonce table.
    /// </summary>
    [ModuleProperty(ModuleName = "OauthNonce", TableName = "oauth_nonce")]
	public partial class OauthNonce : EntityBase
	{
        /// <summary>
        /// Gets or sets the conskey.
        /// </summary>
        /// <value>The conskey.</value>
        [JsonProperty(PropertyName = "conskey")]
		public virtual string Conskey { get; set; }

        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        /// <value>The nonce.</value>
        [JsonProperty(PropertyName = "nonce")]
		public virtual string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the nonce ts.
        /// </summary>
        /// <value>The nonce ts.</value>
        [JsonProperty(PropertyName = "nonce_ts")]
		public virtual long? NonceTs { get; set; }

	}
}