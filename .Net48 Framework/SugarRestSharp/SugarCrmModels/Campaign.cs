// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Campaign.cs" company="SepCity, Inc.">
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
    /// A class which represents the campaigns table.
    /// </summary>
    [ModuleProperty(ModuleName = "Campaigns", TableName = "campaigns")]
	public partial class Campaign : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
		public virtual string Name { get; set; }

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
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the tracker key.
        /// </summary>
        /// <value>The tracker key.</value>
        [JsonProperty(PropertyName = "tracker_key")]
		public virtual int TrackerKey { get; set; }

        /// <summary>
        /// Gets or sets the tracker count.
        /// </summary>
        /// <value>The tracker count.</value>
        [JsonProperty(PropertyName = "tracker_count")]
		public virtual int? TrackerCount { get; set; }

        /// <summary>
        /// Gets or sets the refer URL.
        /// </summary>
        /// <value>The refer URL.</value>
        [JsonProperty(PropertyName = "refer_url")]
		public virtual string ReferUrl { get; set; }

        /// <summary>
        /// Gets or sets the tracker text.
        /// </summary>
        /// <value>The tracker text.</value>
        [JsonProperty(PropertyName = "tracker_text")]
		public virtual string TrackerText { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [JsonProperty(PropertyName = "start_date")]
		public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [JsonProperty(PropertyName = "end_date")]
		public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the impressions.
        /// </summary>
        /// <value>The impressions.</value>
        [JsonProperty(PropertyName = "impressions")]
		public virtual int? Impressions { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>The currency identifier.</value>
        [JsonProperty(PropertyName = "currency_id")]
		public virtual string CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        /// <value>The budget.</value>
        [JsonProperty(PropertyName = "budget")]
		public virtual double? Budget { get; set; }

        /// <summary>
        /// Gets or sets the expected cost.
        /// </summary>
        /// <value>The expected cost.</value>
        [JsonProperty(PropertyName = "expected_cost")]
		public virtual double? ExpectedCost { get; set; }

        /// <summary>
        /// Gets or sets the actual cost.
        /// </summary>
        /// <value>The actual cost.</value>
        [JsonProperty(PropertyName = "actual_cost")]
		public virtual double? ActualCost { get; set; }

        /// <summary>
        /// Gets or sets the expected revenue.
        /// </summary>
        /// <value>The expected revenue.</value>
        [JsonProperty(PropertyName = "expected_revenue")]
		public virtual double? ExpectedRevenue { get; set; }

        /// <summary>
        /// Gets or sets the type of the campaign.
        /// </summary>
        /// <value>The type of the campaign.</value>
        [JsonProperty(PropertyName = "campaign_type")]
		public virtual string CampaignType { get; set; }

        /// <summary>
        /// Gets or sets the objective.
        /// </summary>
        /// <value>The objective.</value>
        [JsonProperty(PropertyName = "objective")]
		public virtual string Objective { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty(PropertyName = "content")]
		public virtual string Content { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        [JsonProperty(PropertyName = "frequency")]
		public virtual string Frequency { get; set; }

	}
}