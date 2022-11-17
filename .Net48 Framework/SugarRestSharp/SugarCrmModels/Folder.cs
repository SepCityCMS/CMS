// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Folder.cs" company="SepCity, Inc.">
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
    /// A class which represents the folders table.
    /// </summary>
    [ModuleProperty(ModuleName = "Folders", TableName = "folders")]
	public partial class Folder : EntityBase
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
        /// Gets or sets the type of the folder.
        /// </summary>
        /// <value>The type of the folder.</value>
        [JsonProperty(PropertyName = "folder_type")]
		public virtual string FolderType { get; set; }

        /// <summary>
        /// Gets or sets the parent folder.
        /// </summary>
        /// <value>The parent folder.</value>
        [JsonProperty(PropertyName = "parent_folder")]
		public virtual string ParentFolder { get; set; }

        /// <summary>
        /// Gets or sets the has child.
        /// </summary>
        /// <value>The has child.</value>
        [JsonProperty(PropertyName = "has_child")]
		public virtual sbyte? HasChild { get; set; }

        /// <summary>
        /// Gets or sets the is group.
        /// </summary>
        /// <value>The is group.</value>
        [JsonProperty(PropertyName = "is_group")]
		public virtual sbyte? IsGroup { get; set; }

        /// <summary>
        /// Gets or sets the is dynamic.
        /// </summary>
        /// <value>The is dynamic.</value>
        [JsonProperty(PropertyName = "is_dynamic")]
		public virtual sbyte? IsDynamic { get; set; }

        /// <summary>
        /// Gets or sets the dynamic query.
        /// </summary>
        /// <value>The dynamic query.</value>
        [JsonProperty(PropertyName = "dynamic_query")]
		public virtual string DynamicQuery { get; set; }

        /// <summary>
        /// Gets or sets the assign to identifier.
        /// </summary>
        /// <value>The assign to identifier.</value>
        [JsonProperty(PropertyName = "assign_to_id")]
		public virtual string AssignToId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [JsonProperty(PropertyName = "modified_by")]
		public virtual string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}