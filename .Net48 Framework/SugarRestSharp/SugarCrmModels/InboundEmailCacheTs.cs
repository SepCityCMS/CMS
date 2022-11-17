// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="InboundEmailCacheTs.cs" company="SepCity, Inc.">
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
    /// A class which represents the inbound_email_cache_ts table.
    /// </summary>
    [ModuleProperty(ModuleName = "InboundEmailCacheTs", TableName = "inbound_email_cache_ts")]
	public partial class InboundEmailCacheTs : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the ie timestamp.
        /// </summary>
        /// <value>The ie timestamp.</value>
        [JsonProperty(PropertyName = "ie_timestamp")]
		public virtual uint? IeTimestamp { get; set; }

	}
}