// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Sugarfeed.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    /// A class which represents the sugarfeed table.
    /// </summary>
    [ModuleProperty(ModuleName = "Sugarfeed", TableName = "sugarfeed")]
	public partial class Sugarfeed : EntityBase
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
        /// Gets or sets the related module.
        /// </summary>
        /// <value>The related module.</value>
        [JsonProperty(PropertyName = "related_module")]
		public virtual string RelatedModule { get; set; }

        /// <summary>
        /// Gets or sets the related identifier.
        /// </summary>
        /// <value>The related identifier.</value>
        [JsonProperty(PropertyName = "related_id")]
		public virtual string RelatedId { get; set; }

        /// <summary>
        /// Gets or sets the link URL.
        /// </summary>
        /// <value>The link URL.</value>
        [JsonProperty(PropertyName = "link_url")]
		public virtual string LinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the link.
        /// </summary>
        /// <value>The type of the link.</value>
        [JsonProperty(PropertyName = "link_type")]
		public virtual string LinkType { get; set; }

	}
}