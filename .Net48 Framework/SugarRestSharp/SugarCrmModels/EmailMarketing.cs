// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailMarketing.cs" company="SepCity, Inc.">
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
    /// A class which represents the email_marketing table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailMarketing", TableName = "email_marketing")]
	public partial class EmailMarketing : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        [JsonProperty(PropertyName = "date_entered")]
		public virtual DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
		public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets from name.
        /// </summary>
        /// <value>From name.</value>
        [JsonProperty(PropertyName = "from_name")]
		public virtual string FromName { get; set; }

        /// <summary>
        /// Gets or sets from addr.
        /// </summary>
        /// <value>From addr.</value>
        [JsonProperty(PropertyName = "from_addr")]
		public virtual string FromAddr { get; set; }

        /// <summary>
        /// Gets or sets the name of the reply to.
        /// </summary>
        /// <value>The name of the reply to.</value>
        [JsonProperty(PropertyName = "reply_to_name")]
		public virtual string ReplyToName { get; set; }

        /// <summary>
        /// Gets or sets the reply to addr.
        /// </summary>
        /// <value>The reply to addr.</value>
        [JsonProperty(PropertyName = "reply_to_addr")]
		public virtual string ReplyToAddr { get; set; }

        /// <summary>
        /// Gets or sets the inbound email identifier.
        /// </summary>
        /// <value>The inbound email identifier.</value>
        [JsonProperty(PropertyName = "inbound_email_id")]
		public virtual string InboundEmailId { get; set; }

        /// <summary>
        /// Gets or sets the date start.
        /// </summary>
        /// <value>The date start.</value>
        [JsonProperty(PropertyName = "date_start")]
		public virtual DateTime? DateStart { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        [JsonProperty(PropertyName = "template_id")]
		public virtual string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        [JsonProperty(PropertyName = "campaign_id")]
		public virtual string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets all prospect lists.
        /// </summary>
        /// <value>All prospect lists.</value>
        [JsonProperty(PropertyName = "all_prospect_lists")]
		public virtual sbyte? AllProspectLists { get; set; }

	}
}