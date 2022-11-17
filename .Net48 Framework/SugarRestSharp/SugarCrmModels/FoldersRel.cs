// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="FoldersRel.cs" company="SepCity, Inc.">
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
    /// A class which represents the folders_rel table.
    /// </summary>
    [ModuleProperty(ModuleName = "FoldersRel", TableName = "folders_rel")]
	public partial class FoldersRel : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the folder identifier.
        /// </summary>
        /// <value>The folder identifier.</value>
        [JsonProperty(PropertyName = "folder_id")]
		public virtual string FolderId { get; set; }

        /// <summary>
        /// Gets or sets the polymorphic module.
        /// </summary>
        /// <value>The polymorphic module.</value>
        [JsonProperty(PropertyName = "polymorphic_module")]
		public virtual string PolymorphicModule { get; set; }

        /// <summary>
        /// Gets or sets the polymorphic identifier.
        /// </summary>
        /// <value>The polymorphic identifier.</value>
        [JsonProperty(PropertyName = "polymorphic_id")]
		public virtual string PolymorphicId { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}