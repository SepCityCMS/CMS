// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="JobQueue.cs" company="SepCity, Inc.">
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
    /// A class which represents the job_queue table.
    /// </summary>
    [ModuleProperty(ModuleName = "JobQueue", TableName = "job_queue")]
	public partial class JobQueue : EntityBase
	{
        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

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
        /// Gets or sets the scheduler identifier.
        /// </summary>
        /// <value>The scheduler identifier.</value>
        [JsonProperty(PropertyName = "scheduler_id")]
		public virtual string SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the execute time.
        /// </summary>
        /// <value>The execute time.</value>
        [JsonProperty(PropertyName = "execute_time")]
		public virtual DateTime? ExecuteTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        /// <value>The resolution.</value>
        [JsonProperty(PropertyName = "resolution")]
		public virtual string Resolution { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [JsonProperty(PropertyName = "message")]
		public virtual string Message { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        [JsonProperty(PropertyName = "target")]
		public virtual string Target { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [JsonProperty(PropertyName = "data")]
		public virtual string Data { get; set; }

        /// <summary>
        /// Gets or sets the requeue.
        /// </summary>
        /// <value>The requeue.</value>
        [JsonProperty(PropertyName = "requeue")]
		public virtual sbyte? Requeue { get; set; }

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>The retry count.</value>
        [JsonProperty(PropertyName = "retry_count")]
		public virtual sbyte? RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the failure count.
        /// </summary>
        /// <value>The failure count.</value>
        [JsonProperty(PropertyName = "failure_count")]
		public virtual sbyte? FailureCount { get; set; }

        /// <summary>
        /// Gets or sets the job delay.
        /// </summary>
        /// <value>The job delay.</value>
        [JsonProperty(PropertyName = "job_delay")]
		public virtual int? JobDelay { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        [JsonProperty(PropertyName = "client")]
		public virtual string Client { get; set; }

        /// <summary>
        /// Gets or sets the percent complete.
        /// </summary>
        /// <value>The percent complete.</value>
        [JsonProperty(PropertyName = "percent_complete")]
		public virtual int? PercentComplete { get; set; }

	}
}