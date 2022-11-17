// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ProspectListsProspects.cs" company="SepCity, Inc.">
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
    /// A class which represents the prospect_lists_prospects table.
    /// </summary>
    [ModuleProperty(ModuleName = "ProspectListsProspects", TableName = "prospect_lists_prospects")]
	public partial class ProspectListsProspects : EntityBase
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