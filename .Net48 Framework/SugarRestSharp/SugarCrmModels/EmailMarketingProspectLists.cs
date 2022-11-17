// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailMarketingProspectLists.cs" company="SepCity, Inc.">
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
    /// A class which represents the email_marketing_prospect_lists table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailMarketingProspectLists", TableName = "email_marketing_prospect_lists")]
	public partial class EmailMarketingProspectLists : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the prospect list identifier.
        /// </summary>
        /// <value>The prospect list identifier.</value>
        [JsonProperty(PropertyName = "prospect_list_id")]
		public virtual string ProspectListId { get; set; }

        /// <summary>
        /// Gets or sets the email marketing identifier.
        /// </summary>
        /// <value>The email marketing identifier.</value>
        [JsonProperty(PropertyName = "email_marketing_id")]
		public virtual string EmailMarketingId { get; set; }

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