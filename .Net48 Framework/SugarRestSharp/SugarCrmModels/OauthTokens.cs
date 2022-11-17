// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="OauthTokens.cs" company="SepCity, Inc.">
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
    /// A class which represents the oauth_tokens table.
    /// </summary>
    [ModuleProperty(ModuleName = "OauthTokens", TableName = "oauth_tokens")]
	public partial class OauthTokens : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        [JsonProperty(PropertyName = "secret")]
		public virtual string Secret { get; set; }

        /// <summary>
        /// Gets or sets the tstate.
        /// </summary>
        /// <value>The tstate.</value>
        [JsonProperty(PropertyName = "tstate")]
		public virtual string Tstate { get; set; }

        /// <summary>
        /// Gets or sets the consumer.
        /// </summary>
        /// <value>The consumer.</value>
        [JsonProperty(PropertyName = "consumer")]
		public virtual string Consumer { get; set; }

        /// <summary>
        /// Gets or sets the token ts.
        /// </summary>
        /// <value>The token ts.</value>
        [JsonProperty(PropertyName = "token_ts")]
		public virtual long? TokenTs { get; set; }

        /// <summary>
        /// Gets or sets the verify.
        /// </summary>
        /// <value>The verify.</value>
        [JsonProperty(PropertyName = "verify")]
		public virtual string Verify { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte Deleted { get; set; }

        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <value>The callback URL.</value>
        [JsonProperty(PropertyName = "callback_url")]
		public virtual string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

	}
}