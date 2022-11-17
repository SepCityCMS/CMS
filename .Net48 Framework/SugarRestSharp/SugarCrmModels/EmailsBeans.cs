// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailsBeans.cs" company="SepCity, Inc.">
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
    /// A class which represents the emails_beans table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailsBeans", TableName = "emails_beans")]
	public partial class EmailsBeans : EntityBase
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
        /// Gets or sets the campaign data.
        /// </summary>
        /// <value>The campaign data.</value>
        [JsonProperty(PropertyName = "campaign_data")]
		public virtual string CampaignData { get; set; }

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