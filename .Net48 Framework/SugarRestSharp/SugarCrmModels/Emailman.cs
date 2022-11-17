// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Emailman.cs" company="SepCity, Inc.">
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
    /// A class which represents the emailman table.
    /// </summary>
    [ModuleProperty(ModuleName = "Emailman", TableName = "emailman")]
	public partial class Emailman : EntityBase
	{
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonProperty(PropertyName = "user_id")]
		public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        [JsonProperty(PropertyName = "campaign_id")]
		public virtual string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the marketing identifier.
        /// </summary>
        /// <value>The marketing identifier.</value>
        [JsonProperty(PropertyName = "marketing_id")]
		public virtual string MarketingId { get; set; }

        /// <summary>
        /// Gets or sets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        [JsonProperty(PropertyName = "list_id")]
		public virtual string ListId { get; set; }

        /// <summary>
        /// Gets or sets the send date time.
        /// </summary>
        /// <value>The send date time.</value>
        [JsonProperty(PropertyName = "send_date_time")]
		public virtual DateTime? SendDateTime { get; set; }

        /// <summary>
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the in queue.
        /// </summary>
        /// <value>The in queue.</value>
        [JsonProperty(PropertyName = "in_queue")]
		public virtual sbyte? InQueue { get; set; }

        /// <summary>
        /// Gets or sets the in queue date.
        /// </summary>
        /// <value>The in queue date.</value>
        [JsonProperty(PropertyName = "in_queue_date")]
		public virtual DateTime? InQueueDate { get; set; }

        /// <summary>
        /// Gets or sets the send attempts.
        /// </summary>
        /// <value>The send attempts.</value>
        [JsonProperty(PropertyName = "send_attempts")]
		public virtual int? SendAttempts { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the related identifier.
        /// </summary>
        /// <value>The related identifier.</value>
        [JsonProperty(PropertyName = "related_id")]
		public virtual string RelatedId { get; set; }

        /// <summary>
        /// Gets or sets the type of the related.
        /// </summary>
        /// <value>The type of the related.</value>
        [JsonProperty(PropertyName = "related_type")]
		public virtual string RelatedType { get; set; }

	}
}