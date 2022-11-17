// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ProjectTask.cs" company="SepCity, Inc.">
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
    /// A class which represents the project_task table.
    /// </summary>
    [ModuleProperty(ModuleName = "ProjectTask", TableName = "project_task")]
	public partial class ProjectTask : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

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
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonProperty(PropertyName = "project_id")]
		public virtual string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project task identifier.
        /// </summary>
        /// <value>The project task identifier.</value>
        [JsonProperty(PropertyName = "project_task_id")]
		public virtual int? ProjectTaskId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
		public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the predecessors.
        /// </summary>
        /// <value>The predecessors.</value>
        [JsonProperty(PropertyName = "predecessors")]
		public virtual string Predecessors { get; set; }

        /// <summary>
        /// Gets or sets the date start.
        /// </summary>
        /// <value>The date start.</value>
        [JsonProperty(PropertyName = "date_start")]
		public virtual DateTime? DateStart { get; set; }

        /// <summary>
        /// Gets or sets the time start.
        /// </summary>
        /// <value>The time start.</value>
        [JsonProperty(PropertyName = "time_start")]
		public virtual int? TimeStart { get; set; }

        /// <summary>
        /// Gets or sets the time finish.
        /// </summary>
        /// <value>The time finish.</value>
        [JsonProperty(PropertyName = "time_finish")]
		public virtual int? TimeFinish { get; set; }

        /// <summary>
        /// Gets or sets the date finish.
        /// </summary>
        /// <value>The date finish.</value>
        [JsonProperty(PropertyName = "date_finish")]
		public virtual DateTime? DateFinish { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        [JsonProperty(PropertyName = "duration")]
		public virtual int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the duration unit.
        /// </summary>
        /// <value>The duration unit.</value>
        [JsonProperty(PropertyName = "duration_unit")]
		public virtual string DurationUnit { get; set; }

        /// <summary>
        /// Gets or sets the actual duration.
        /// </summary>
        /// <value>The actual duration.</value>
        [JsonProperty(PropertyName = "actual_duration")]
		public virtual int? ActualDuration { get; set; }

        /// <summary>
        /// Gets or sets the percent complete.
        /// </summary>
        /// <value>The percent complete.</value>
        [JsonProperty(PropertyName = "percent_complete")]
		public virtual int? PercentComplete { get; set; }

        /// <summary>
        /// Gets or sets the date due.
        /// </summary>
        /// <value>The date due.</value>
        [JsonProperty(PropertyName = "date_due")]
		public virtual DateTime? DateDue { get; set; }

        /// <summary>
        /// Gets or sets the time due.
        /// </summary>
        /// <value>The time due.</value>
        [JsonProperty(PropertyName = "time_due")]
		public virtual string TimeDue { get; set; }

        /// <summary>
        /// Gets or sets the parent task identifier.
        /// </summary>
        /// <value>The parent task identifier.</value>
        [JsonProperty(PropertyName = "parent_task_id")]
		public virtual int? ParentTaskId { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        [JsonProperty(PropertyName = "priority")]
		public virtual string Priority { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the milestone flag.
        /// </summary>
        /// <value>The milestone flag.</value>
        [JsonProperty(PropertyName = "milestone_flag")]
		public virtual sbyte? MilestoneFlag { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>The order number.</value>
        [JsonProperty(PropertyName = "order_number")]
		public virtual int? OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the task number.
        /// </summary>
        /// <value>The task number.</value>
        [JsonProperty(PropertyName = "task_number")]
		public virtual int? TaskNumber { get; set; }

        /// <summary>
        /// Gets or sets the estimated effort.
        /// </summary>
        /// <value>The estimated effort.</value>
        [JsonProperty(PropertyName = "estimated_effort")]
		public virtual int? EstimatedEffort { get; set; }

        /// <summary>
        /// Gets or sets the actual effort.
        /// </summary>
        /// <value>The actual effort.</value>
        [JsonProperty(PropertyName = "actual_effort")]
		public virtual int? ActualEffort { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the utilization.
        /// </summary>
        /// <value>The utilization.</value>
        [JsonProperty(PropertyName = "utilization")]
		public virtual int? Utilization { get; set; }

	}
}