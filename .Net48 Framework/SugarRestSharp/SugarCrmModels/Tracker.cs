// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Tracker.cs" company="SepCity, Inc.">
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
    /// A class which represents the tracker table.
    /// </summary>
    [ModuleProperty(ModuleName = "Tracker", TableName = "tracker")]
	public partial class Tracker : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the monitor identifier.
        /// </summary>
        /// <value>The monitor identifier.</value>
        [JsonProperty(PropertyName = "monitor_id")]
		public virtual string MonitorId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonProperty(PropertyName = "user_id")]
		public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        [JsonProperty(PropertyName = "module_name")]
		public virtual string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonProperty(PropertyName = "item_id")]
		public virtual string ItemId { get; set; }

        /// <summary>
        /// Gets or sets the item summary.
        /// </summary>
        /// <value>The item summary.</value>
        [JsonProperty(PropertyName = "item_summary")]
		public virtual string ItemSummary { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        [JsonProperty(PropertyName = "action")]
		public virtual string Action { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        [JsonProperty(PropertyName = "session_id")]
		public virtual string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the visible.
        /// </summary>
        /// <value>The visible.</value>
        [JsonProperty(PropertyName = "visible")]
		public virtual sbyte? Visible { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}