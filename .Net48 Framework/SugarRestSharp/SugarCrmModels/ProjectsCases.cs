// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ProjectsCases.cs" company="SepCity, Inc.">
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
    /// A class which represents the projects_cases table.
    /// </summary>
    [ModuleProperty(ModuleName = "ProjectsCases", TableName = "projects_cases")]
	public partial class ProjectsCases : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the case identifier.
        /// </summary>
        /// <value>The case identifier.</value>
        [JsonProperty(PropertyName = "case_id")]
		public virtual string CaseId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonProperty(PropertyName = "project_id")]
		public virtual string ProjectId { get; set; }

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