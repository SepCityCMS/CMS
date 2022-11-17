// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="MeetingsLeads.cs" company="SepCity, Inc.">
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
    /// A class which represents the meetings_leads table.
    /// </summary>
    [ModuleProperty(ModuleName = "MeetingsLeads", TableName = "meetings_leads")]
	public partial class MeetingsLeads : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the meeting identifier.
        /// </summary>
        /// <value>The meeting identifier.</value>
        [JsonProperty(PropertyName = "meeting_id")]
		public virtual string MeetingId { get; set; }

        /// <summary>
        /// Gets or sets the lead identifier.
        /// </summary>
        /// <value>The lead identifier.</value>
        [JsonProperty(PropertyName = "lead_id")]
		public virtual string LeadId { get; set; }

        /// <summary>
        /// Gets or sets the required.
        /// </summary>
        /// <value>The required.</value>
        [JsonProperty(PropertyName = "required")]
		public virtual string Required { get; set; }

        /// <summary>
        /// Gets or sets the accept status.
        /// </summary>
        /// <value>The accept status.</value>
        [JsonProperty(PropertyName = "accept_status")]
		public virtual string AcceptStatus { get; set; }

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