// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CampaignsAudit.cs" company="SepCity, Inc.">
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
    /// A class which represents the campaigns_audit table.
    /// </summary>
    [ModuleProperty(ModuleName = "CampaignsAudit", TableName = "campaigns_audit")]
	public partial class CampaignsAudit : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        [JsonProperty(PropertyName = "parent_id")]
		public virtual string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [JsonProperty(PropertyName = "date_created")]
		public virtual DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        [JsonProperty(PropertyName = "field_name")]
		public virtual string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        [JsonProperty(PropertyName = "data_type")]
		public virtual string DataType { get; set; }

        /// <summary>
        /// Gets or sets the before value string.
        /// </summary>
        /// <value>The before value string.</value>
        [JsonProperty(PropertyName = "before_value_string")]
		public virtual string BeforeValueString { get; set; }

        /// <summary>
        /// Gets or sets the after value string.
        /// </summary>
        /// <value>The after value string.</value>
        [JsonProperty(PropertyName = "after_value_string")]
		public virtual string AfterValueString { get; set; }

        /// <summary>
        /// Gets or sets the before value text.
        /// </summary>
        /// <value>The before value text.</value>
        [JsonProperty(PropertyName = "before_value_text")]
		public virtual string BeforeValueText { get; set; }

        /// <summary>
        /// Gets or sets the after value text.
        /// </summary>
        /// <value>The after value text.</value>
        [JsonProperty(PropertyName = "after_value_text")]
		public virtual string AfterValueText { get; set; }

	}
}