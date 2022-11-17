// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailAddrBeanRel.cs" company="SepCity, Inc.">
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
    /// A class which represents the email_addr_bean_rel table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailAddrBeanRel", TableName = "email_addr_bean_rel")]
	public partial class EmailAddrBeanRel : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the email address identifier.
        /// </summary>
        /// <value>The email address identifier.</value>
        [JsonProperty(PropertyName = "email_address_id")]
		public virtual string EmailAddressId { get; set; }

        /// <summary>
        /// Gets or sets the bean identifier.
        /// </summary>
        /// <value>The bean identifier.</value>
        [JsonProperty(PropertyName = "bean_id")]
		public virtual string BeanId { get; set; }

        /// <summary>
        /// Gets or sets the bean module.
        /// </summary>
        /// <value>The bean module.</value>
        [JsonProperty(PropertyName = "bean_module")]
		public virtual string BeanModule { get; set; }

        /// <summary>
        /// Gets or sets the primary address.
        /// </summary>
        /// <value>The primary address.</value>
        [JsonProperty(PropertyName = "primary_address")]
		public virtual sbyte? PrimaryAddress { get; set; }

        /// <summary>
        /// Gets or sets the reply to address.
        /// </summary>
        /// <value>The reply to address.</value>
        [JsonProperty(PropertyName = "reply_to_address")]
		public virtual sbyte? ReplyToAddress { get; set; }

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