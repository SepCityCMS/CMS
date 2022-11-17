// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="UpgradeHistory.cs" company="SepCity, Inc.">
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
    /// A class which represents the upgrade_history table.
    /// </summary>
    [ModuleProperty(ModuleName = "UpgradeHistory", TableName = "upgrade_history")]
	public partial class UpgradeHistory : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>The filename.</value>
        [JsonProperty(PropertyName = "filename")]
		public virtual string Filename { get; set; }

        /// <summary>
        /// Gets or sets the md5sum.
        /// </summary>
        /// <value>The md5sum.</value>
        [JsonProperty(PropertyName = "md5sum")]
		public virtual string Md5sum { get; set; }

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
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [JsonProperty(PropertyName = "version")]
		public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
		public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the identifier.
        /// </summary>
        /// <value>The name of the identifier.</value>
        [JsonProperty(PropertyName = "id_name")]
		public virtual string IdName { get; set; }

        /// <summary>
        /// Gets or sets the manifest.
        /// </summary>
        /// <value>The manifest.</value>
        [JsonProperty(PropertyName = "manifest")]
		public virtual string Manifest { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        [JsonProperty(PropertyName = "date_entered")]
		public virtual DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the enabled.
        /// </summary>
        /// <value>The enabled.</value>
        [JsonProperty(PropertyName = "enabled")]
		public virtual sbyte? Enabled { get; set; }

	}
}