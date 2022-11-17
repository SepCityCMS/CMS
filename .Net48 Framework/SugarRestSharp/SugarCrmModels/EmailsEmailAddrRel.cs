// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailsEmailAddrRel.cs" company="SepCity, Inc.">
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
    /// A class which represents the emails_email_addr_rel table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailsEmailAddrRel", TableName = "emails_email_addr_rel")]
	public partial class EmailsEmailAddrRel : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>The email identifier.</value>
        [JsonProperty(PropertyName = "email_id")]
		public virtual string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the type of the address.
        /// </summary>
        /// <value>The type of the address.</value>
        [JsonProperty(PropertyName = "address_type")]
		public virtual string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the email address identifier.
        /// </summary>
        /// <value>The email address identifier.</value>
        [JsonProperty(PropertyName = "email_address_id")]
		public virtual string EmailAddressId { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}