// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Bug.cs" company="SepCity, Inc.">
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
    /// A class which represents the bugs table.
    /// </summary>
    [ModuleProperty(ModuleName = "Bugs", TableName = "bugs")]
	public partial class Bug : EntityBase
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
        /// Gets or sets the bug number.
        /// </summary>
        /// <value>The bug number.</value>
        [JsonProperty(PropertyName = "bug_number")]
		public virtual int BugNumber { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty(PropertyName = "type")]
		public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        [JsonProperty(PropertyName = "priority")]
		public virtual string Priority { get; set; }

        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        /// <value>The resolution.</value>
        [JsonProperty(PropertyName = "resolution")]
		public virtual string Resolution { get; set; }

        /// <summary>
        /// Gets or sets the work log.
        /// </summary>
        /// <value>The work log.</value>
        [JsonProperty(PropertyName = "work_log")]
		public virtual string WorkLog { get; set; }

        /// <summary>
        /// Gets or sets the found in release.
        /// </summary>
        /// <value>The found in release.</value>
        [JsonProperty(PropertyName = "found_in_release")]
		public virtual string FoundInRelease { get; set; }

        /// <summary>
        /// Gets or sets the fixed in release.
        /// </summary>
        /// <value>The fixed in release.</value>
        [JsonProperty(PropertyName = "fixed_in_release")]
		public virtual string FixedInRelease { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty(PropertyName = "source")]
		public virtual string Source { get; set; }

        /// <summary>
        /// Gets or sets the product category.
        /// </summary>
        /// <value>The product category.</value>
        [JsonProperty(PropertyName = "product_category")]
		public virtual string ProductCategory { get; set; }

	}
}