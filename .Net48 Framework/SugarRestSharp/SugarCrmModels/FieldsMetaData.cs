// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="FieldsMetaData.cs" company="SepCity, Inc.">
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
    /// A class which represents the fields_meta_data table.
    /// </summary>
    [ModuleProperty(ModuleName = "FieldsMetaData", TableName = "fields_meta_data")]
	public partial class FieldsMetaData : EntityBase
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
        /// Gets or sets the vname.
        /// </summary>
        /// <value>The vname.</value>
        [JsonProperty(PropertyName = "vname")]
		public virtual string Vname { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        [JsonProperty(PropertyName = "comments")]
		public virtual string Comments { get; set; }

        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        [JsonProperty(PropertyName = "help")]
		public virtual string Help { get; set; }

        /// <summary>
        /// Gets or sets the custom module.
        /// </summary>
        /// <value>The custom module.</value>
        [JsonProperty(PropertyName = "custom_module")]
		public virtual string CustomModule { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty(PropertyName = "type")]
		public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        [JsonProperty(PropertyName = "len")]
		public virtual int? Len { get; set; }

        /// <summary>
        /// Gets or sets the required.
        /// </summary>
        /// <value>The required.</value>
        [JsonProperty(PropertyName = "required")]
		public virtual sbyte? Required { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        [JsonProperty(PropertyName = "default_value")]
		public virtual string DefaultValue { get; set; }

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

        /// <summary>
        /// Gets or sets the audited.
        /// </summary>
        /// <value>The audited.</value>
        [JsonProperty(PropertyName = "audited")]
		public virtual sbyte? Audited { get; set; }

        /// <summary>
        /// Gets or sets the massupdate.
        /// </summary>
        /// <value>The massupdate.</value>
        [JsonProperty(PropertyName = "massupdate")]
		public virtual sbyte? Massupdate { get; set; }

        /// <summary>
        /// Gets or sets the duplicate merge.
        /// </summary>
        /// <value>The duplicate merge.</value>
        [JsonProperty(PropertyName = "duplicate_merge")]
		public virtual short? DuplicateMerge { get; set; }

        /// <summary>
        /// Gets or sets the reportable.
        /// </summary>
        /// <value>The reportable.</value>
        [JsonProperty(PropertyName = "reportable")]
		public virtual sbyte? Reportable { get; set; }

        /// <summary>
        /// Gets or sets the importable.
        /// </summary>
        /// <value>The importable.</value>
        [JsonProperty(PropertyName = "importable")]
		public virtual string Importable { get; set; }

        /// <summary>
        /// Gets or sets the ext1.
        /// </summary>
        /// <value>The ext1.</value>
        [JsonProperty(PropertyName = "ext1")]
		public virtual string Ext1 { get; set; }

        /// <summary>
        /// Gets or sets the ext2.
        /// </summary>
        /// <value>The ext2.</value>
        [JsonProperty(PropertyName = "ext2")]
		public virtual string Ext2 { get; set; }

        /// <summary>
        /// Gets or sets the ext3.
        /// </summary>
        /// <value>The ext3.</value>
        [JsonProperty(PropertyName = "ext3")]
		public virtual string Ext3 { get; set; }

        /// <summary>
        /// Gets or sets the ext4.
        /// </summary>
        /// <value>The ext4.</value>
        [JsonProperty(PropertyName = "ext4")]
		public virtual string Ext4 { get; set; }

	}
}