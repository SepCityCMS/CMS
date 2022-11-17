// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CampaignLog.cs" company="SepCity, Inc.">
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
    /// A class which represents the campaign_log table.
    /// </summary>
    [ModuleProperty(ModuleName = "CampaignLog", TableName = "campaign_log")]
	public partial class CampaignLog : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        [JsonProperty(PropertyName = "campaign_id")]
		public virtual string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the target tracker key.
        /// </summary>
        /// <value>The target tracker key.</value>
        [JsonProperty(PropertyName = "target_tracker_key")]
		public virtual string TargetTrackerKey { get; set; }

        /// <summary>
        /// Gets or sets the target identifier.
        /// </summary>
        /// <value>The target identifier.</value>
        [JsonProperty(PropertyName = "target_id")]
		public virtual string TargetId { get; set; }

        /// <summary>
        /// Gets or sets the type of the target.
        /// </summary>
        /// <value>The type of the target.</value>
        [JsonProperty(PropertyName = "target_type")]
		public virtual string TargetType { get; set; }

        /// <summary>
        /// Gets or sets the type of the activity.
        /// </summary>
        /// <value>The type of the activity.</value>
        [JsonProperty(PropertyName = "activity_type")]
		public virtual string ActivityType { get; set; }

        /// <summary>
        /// Gets or sets the activity date.
        /// </summary>
        /// <value>The activity date.</value>
        [JsonProperty(PropertyName = "activity_date")]
		public virtual DateTime? ActivityDate { get; set; }

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

        /// <summary>
        /// Gets or sets the archived.
        /// </summary>
        /// <value>The archived.</value>
        [JsonProperty(PropertyName = "archived")]
		public virtual sbyte? Archived { get; set; }

        /// <summary>
        /// Gets or sets the hits.
        /// </summary>
        /// <value>The hits.</value>
        [JsonProperty(PropertyName = "hits")]
		public virtual int? Hits { get; set; }

        /// <summary>
        /// Gets or sets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        [JsonProperty(PropertyName = "list_id")]
		public virtual string ListId { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the more information.
        /// </summary>
        /// <value>The more information.</value>
        [JsonProperty(PropertyName = "more_information")]
		public virtual string MoreInformation { get; set; }

        /// <summary>
        /// Gets or sets the marketing identifier.
        /// </summary>
        /// <value>The marketing identifier.</value>
        [JsonProperty(PropertyName = "marketing_id")]
		public virtual string MarketingId { get; set; }

	}
}