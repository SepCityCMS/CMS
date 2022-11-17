// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Opportunity.cs" company="SepCity, Inc.">
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
    /// A class which represents the opportunities table.
    /// </summary>
    [ModuleProperty(ModuleName = "Opportunities", TableName = "opportunities")]
	public partial class Opportunity : EntityBase
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

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
        /// Gets or sets the type of the opportunity.
        /// </summary>
        /// <value>The type of the opportunity.</value>
        [JsonProperty(PropertyName = "opportunity_type")]
		public virtual string OpportunityType { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        [JsonProperty(PropertyName = "campaign_id")]
		public virtual string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the lead source.
        /// </summary>
        /// <value>The lead source.</value>
        [JsonProperty(PropertyName = "lead_source")]
		public virtual string LeadSource { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        [JsonProperty(PropertyName = "amount")]
		public virtual double? Amount { get; set; }

        /// <summary>
        /// Gets or sets the amount usdollar.
        /// </summary>
        /// <value>The amount usdollar.</value>
        [JsonProperty(PropertyName = "amount_usdollar")]
		public virtual double? AmountUsdollar { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>The currency identifier.</value>
        [JsonProperty(PropertyName = "currency_id")]
		public virtual string CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the date closed.
        /// </summary>
        /// <value>The date closed.</value>
        [JsonProperty(PropertyName = "date_closed")]
		public virtual DateTime? DateClosed { get; set; }

        /// <summary>
        /// Gets or sets the next step.
        /// </summary>
        /// <value>The next step.</value>
        [JsonProperty(PropertyName = "next_step")]
		public virtual string NextStep { get; set; }

        /// <summary>
        /// Gets or sets the sales stage.
        /// </summary>
        /// <value>The sales stage.</value>
        [JsonProperty(PropertyName = "sales_stage")]
		public virtual string SalesStage { get; set; }

        /// <summary>
        /// Gets or sets the probability.
        /// </summary>
        /// <value>The probability.</value>
        [JsonProperty(PropertyName = "probability")]
		public virtual double? Probability { get; set; }

	}
}