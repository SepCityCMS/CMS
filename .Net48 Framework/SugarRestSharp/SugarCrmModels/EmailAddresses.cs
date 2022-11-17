// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailAddresses.cs" company="SepCity, Inc.">
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
    /// A class which represents the email_addresses table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailAddresses", TableName = "email_addresses")]
	public partial class EmailAddresses : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        [JsonProperty(PropertyName = "email_address")]
		public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the email address caps.
        /// </summary>
        /// <value>The email address caps.</value>
        [JsonProperty(PropertyName = "email_address_caps")]
		public virtual string EmailAddressCaps { get; set; }

        /// <summary>
        /// Gets or sets the invalid email.
        /// </summary>
        /// <value>The invalid email.</value>
        [JsonProperty(PropertyName = "invalid_email")]
		public virtual sbyte? InvalidEmail { get; set; }

        /// <summary>
        /// Gets or sets the opt out.
        /// </summary>
        /// <value>The opt out.</value>
        [JsonProperty(PropertyName = "opt_out")]
		public virtual sbyte? OptOut { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [JsonProperty(PropertyName = "date_created")]
		public virtual DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}