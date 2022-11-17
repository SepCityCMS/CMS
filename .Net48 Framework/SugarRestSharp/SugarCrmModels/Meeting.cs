// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Meeting.cs" company="SepCity, Inc.">
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
    /// A class which represents the meetings table.
    /// </summary>
    [ModuleProperty(ModuleName = "Meetings", TableName = "meetings")]
	public partial class Meeting : EntityBase
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
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [JsonProperty(PropertyName = "location")]
		public virtual string Location { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [JsonProperty(PropertyName = "password")]
		public virtual string Password { get; set; }

        /// <summary>
        /// Gets or sets the join URL.
        /// </summary>
        /// <value>The join URL.</value>
        [JsonProperty(PropertyName = "join_url")]
		public virtual string JoinUrl { get; set; }

        /// <summary>
        /// Gets or sets the host URL.
        /// </summary>
        /// <value>The host URL.</value>
        [JsonProperty(PropertyName = "host_url")]
		public virtual string HostUrl { get; set; }

        /// <summary>
        /// Gets or sets the displayed URL.
        /// </summary>
        /// <value>The displayed URL.</value>
        [JsonProperty(PropertyName = "displayed_url")]
		public virtual string DisplayedUrl { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        /// <value>The creator.</value>
        [JsonProperty(PropertyName = "creator")]
		public virtual string Creator { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>The external identifier.</value>
        [JsonProperty(PropertyName = "external_id")]
		public virtual string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the duration hours.
        /// </summary>
        /// <value>The duration hours.</value>
        [JsonProperty(PropertyName = "duration_hours")]
		public virtual int? DurationHours { get; set; }

        /// <summary>
        /// Gets or sets the duration minutes.
        /// </summary>
        /// <value>The duration minutes.</value>
        [JsonProperty(PropertyName = "duration_minutes")]
		public virtual int? DurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the date start.
        /// </summary>
        /// <value>The date start.</value>
        [JsonProperty(PropertyName = "date_start")]
		public virtual DateTime? DateStart { get; set; }

        /// <summary>
        /// Gets or sets the date end.
        /// </summary>
        /// <value>The date end.</value>
        [JsonProperty(PropertyName = "date_end")]
		public virtual DateTime? DateEnd { get; set; }

        /// <summary>
        /// Gets or sets the type of the parent.
        /// </summary>
        /// <value>The type of the parent.</value>
        [JsonProperty(PropertyName = "parent_type")]
		public virtual string ParentType { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty(PropertyName = "type")]
		public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        [JsonProperty(PropertyName = "parent_id")]
		public virtual string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the reminder time.
        /// </summary>
        /// <value>The reminder time.</value>
        [JsonProperty(PropertyName = "reminder_time")]
		public virtual int? ReminderTime { get; set; }

        /// <summary>
        /// Gets or sets the email reminder time.
        /// </summary>
        /// <value>The email reminder time.</value>
        [JsonProperty(PropertyName = "email_reminder_time")]
		public virtual int? EmailReminderTime { get; set; }

        /// <summary>
        /// Gets or sets the email reminder sent.
        /// </summary>
        /// <value>The email reminder sent.</value>
        [JsonProperty(PropertyName = "email_reminder_sent")]
		public virtual sbyte? EmailReminderSent { get; set; }

        /// <summary>
        /// Gets or sets the outlook identifier.
        /// </summary>
        /// <value>The outlook identifier.</value>
        [JsonProperty(PropertyName = "outlook_id")]
		public virtual string OutlookId { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        [JsonProperty(PropertyName = "sequence")]
		public virtual int? Sequence { get; set; }

        /// <summary>
        /// Gets or sets the type of the repeat.
        /// </summary>
        /// <value>The type of the repeat.</value>
        [JsonProperty(PropertyName = "repeat_type")]
		public virtual string RepeatType { get; set; }

        /// <summary>
        /// Gets or sets the repeat interval.
        /// </summary>
        /// <value>The repeat interval.</value>
        [JsonProperty(PropertyName = "repeat_interval")]
		public virtual int? RepeatInterval { get; set; }

        /// <summary>
        /// Gets or sets the repeat dow.
        /// </summary>
        /// <value>The repeat dow.</value>
        [JsonProperty(PropertyName = "repeat_dow")]
		public virtual string RepeatDow { get; set; }

        /// <summary>
        /// Gets or sets the repeat until.
        /// </summary>
        /// <value>The repeat until.</value>
        [JsonProperty(PropertyName = "repeat_until")]
		public virtual DateTime? RepeatUntil { get; set; }

        /// <summary>
        /// Gets or sets the repeat count.
        /// </summary>
        /// <value>The repeat count.</value>
        [JsonProperty(PropertyName = "repeat_count")]
		public virtual int? RepeatCount { get; set; }

        /// <summary>
        /// Gets or sets the repeat parent identifier.
        /// </summary>
        /// <value>The repeat parent identifier.</value>
        [JsonProperty(PropertyName = "repeat_parent_id")]
		public virtual string RepeatParentId { get; set; }

        /// <summary>
        /// Gets or sets the recurring source.
        /// </summary>
        /// <value>The recurring source.</value>
        [JsonProperty(PropertyName = "recurring_source")]
		public virtual string RecurringSource { get; set; }

	}
}