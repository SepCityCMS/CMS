// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CustomFields.cs" company="SepCity, Inc.">
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
    /// A class which represents the custom_fields table.
    /// </summary>
    [ModuleProperty(ModuleName = "CustomFields", TableName = "custom_fields")]
	public partial class CustomFields : EntityBase
	{
        /// <summary>
        /// Gets or sets the bean identifier.
        /// </summary>
        /// <value>The bean identifier.</value>
        [JsonProperty(PropertyName = "bean_id")]
		public virtual string BeanId { get; set; }

        /// <summary>
        /// Gets or sets the set number.
        /// </summary>
        /// <value>The set number.</value>
        [JsonProperty(PropertyName = "set_num")]
		public virtual int? SetNum { get; set; }

        /// <summary>
        /// Gets or sets the field0.
        /// </summary>
        /// <value>The field0.</value>
        [JsonProperty(PropertyName = "field0")]
		public virtual string Field0 { get; set; }

        /// <summary>
        /// Gets or sets the field1.
        /// </summary>
        /// <value>The field1.</value>
        [JsonProperty(PropertyName = "field1")]
		public virtual string Field1 { get; set; }

        /// <summary>
        /// Gets or sets the field2.
        /// </summary>
        /// <value>The field2.</value>
        [JsonProperty(PropertyName = "field2")]
		public virtual string Field2 { get; set; }

        /// <summary>
        /// Gets or sets the field3.
        /// </summary>
        /// <value>The field3.</value>
        [JsonProperty(PropertyName = "field3")]
		public virtual string Field3 { get; set; }

        /// <summary>
        /// Gets or sets the field4.
        /// </summary>
        /// <value>The field4.</value>
        [JsonProperty(PropertyName = "field4")]
		public virtual string Field4 { get; set; }

        /// <summary>
        /// Gets or sets the field5.
        /// </summary>
        /// <value>The field5.</value>
        [JsonProperty(PropertyName = "field5")]
		public virtual string Field5 { get; set; }

        /// <summary>
        /// Gets or sets the field6.
        /// </summary>
        /// <value>The field6.</value>
        [JsonProperty(PropertyName = "field6")]
		public virtual string Field6 { get; set; }

        /// <summary>
        /// Gets or sets the field7.
        /// </summary>
        /// <value>The field7.</value>
        [JsonProperty(PropertyName = "field7")]
		public virtual string Field7 { get; set; }

        /// <summary>
        /// Gets or sets the field8.
        /// </summary>
        /// <value>The field8.</value>
        [JsonProperty(PropertyName = "field8")]
		public virtual string Field8 { get; set; }

        /// <summary>
        /// Gets or sets the field9.
        /// </summary>
        /// <value>The field9.</value>
        [JsonProperty(PropertyName = "field9")]
		public virtual string Field9 { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}