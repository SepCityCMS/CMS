// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="AccountsContacts.cs" company="SepCity, Inc.">
//     Copyright ? SepCity, Inc. 2019
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
    /// A class which represents the accounts_contacts table.
    /// </summary>
    [ModuleProperty(ModuleName = "AccountsContacts", TableName = "accounts_contacts")]
	public partial class AccountsContacts : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>The contact identifier.</value>
        [JsonProperty(PropertyName = "contact_id")]
		public virtual string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        [JsonProperty(PropertyName = "account_id")]
		public virtual string AccountId { get; set; }

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