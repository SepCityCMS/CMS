// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Scheduler.cs" company="SepCity, Inc.">
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
    /// A class which represents the schedulers table.
    /// </summary>
    [ModuleProperty(ModuleName = "Schedulers", TableName = "schedulers")]
	public partial class Scheduler : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        [JsonProperty(PropertyName = "date_entered")]
		public virtual DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
		public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>The job.</value>
        [JsonProperty(PropertyName = "job")]
		public virtual string Job { get; set; }

        /// <summary>
        /// Gets or sets the date time start.
        /// </summary>
        /// <value>The date time start.</value>
        [JsonProperty(PropertyName = "date_time_start")]
		public virtual DateTime? DateTimeStart { get; set; }

        /// <summary>
        /// Gets or sets the date time end.
        /// </summary>
        /// <value>The date time end.</value>
        [JsonProperty(PropertyName = "date_time_end")]
		public virtual DateTime? DateTimeEnd { get; set; }

        /// <summary>
        /// Gets or sets the job interval.
        /// </summary>
        /// <value>The job interval.</value>
        [JsonProperty(PropertyName = "job_interval")]
		public virtual string JobInterval { get; set; }

        /// <summary>
        /// Gets or sets the time from.
        /// </summary>
        /// <value>The time from.</value>
        [JsonProperty(PropertyName = "time_from")]
		public virtual string TimeFrom { get; set; }

        /// <summary>
        /// Gets or sets the time to.
        /// </summary>
        /// <value>The time to.</value>
        [JsonProperty(PropertyName = "time_to")]
		public virtual string TimeTo { get; set; }

        /// <summary>
        /// Gets or sets the last run.
        /// </summary>
        /// <value>The last run.</value>
        [JsonProperty(PropertyName = "last_run")]
		public virtual DateTime? LastRun { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the catch up.
        /// </summary>
        /// <value>The catch up.</value>
        [JsonProperty(PropertyName = "catch_up")]
		public virtual sbyte? CatchUp { get; set; }

	}
}