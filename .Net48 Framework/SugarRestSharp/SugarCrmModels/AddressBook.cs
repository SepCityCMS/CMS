// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="AddressBook.cs" company="SepCity, Inc.">
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
    /// A class which represents the address_book table.
    /// </summary>
    [ModuleProperty(ModuleName = "AddressBook", TableName = "address_book")]
	public partial class AddressBook : EntityBase
	{
        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the bean.
        /// </summary>
        /// <value>The bean.</value>
        [JsonProperty(PropertyName = "bean")]
		public virtual string Bean { get; set; }

        /// <summary>
        /// Gets or sets the bean identifier.
        /// </summary>
        /// <value>The bean identifier.</value>
        [JsonProperty(PropertyName = "bean_id")]
		public virtual string BeanId { get; set; }

	}
}