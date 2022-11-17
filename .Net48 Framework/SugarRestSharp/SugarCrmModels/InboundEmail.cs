// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="InboundEmail.cs" company="SepCity, Inc.">
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
    /// A class which represents the inbound_email table.
    /// </summary>
    [ModuleProperty(ModuleName = "InboundEmail", TableName = "inbound_email")]
	public partial class InboundEmail : EntityBase
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
        /// Gets or sets the server URL.
        /// </summary>
        /// <value>The server URL.</value>
        [JsonProperty(PropertyName = "server_url")]
		public virtual string ServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the email user.
        /// </summary>
        /// <value>The email user.</value>
        [JsonProperty(PropertyName = "email_user")]
		public virtual string EmailUser { get; set; }

        /// <summary>
        /// Gets or sets the email password.
        /// </summary>
        /// <value>The email password.</value>
        [JsonProperty(PropertyName = "email_password")]
		public virtual string EmailPassword { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        [JsonProperty(PropertyName = "port")]
		public virtual int? Port { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        [JsonProperty(PropertyName = "service")]
		public virtual string Service { get; set; }

        /// <summary>
        /// Gets or sets the mailbox.
        /// </summary>
        /// <value>The mailbox.</value>
        [JsonProperty(PropertyName = "mailbox")]
		public virtual string Mailbox { get; set; }

        /// <summary>
        /// Gets or sets the delete seen.
        /// </summary>
        /// <value>The delete seen.</value>
        [JsonProperty(PropertyName = "delete_seen")]
		public virtual sbyte? DeleteSeen { get; set; }

        /// <summary>
        /// Gets or sets the type of the mailbox.
        /// </summary>
        /// <value>The type of the mailbox.</value>
        [JsonProperty(PropertyName = "mailbox_type")]
		public virtual string MailboxType { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        [JsonProperty(PropertyName = "template_id")]
		public virtual string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the stored options.
        /// </summary>
        /// <value>The stored options.</value>
        [JsonProperty(PropertyName = "stored_options")]
		public virtual string StoredOptions { get; set; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>The group identifier.</value>
        [JsonProperty(PropertyName = "group_id")]
		public virtual string GroupId { get; set; }

        /// <summary>
        /// Gets or sets the is personal.
        /// </summary>
        /// <value>The is personal.</value>
        [JsonProperty(PropertyName = "is_personal")]
		public virtual sbyte? IsPersonal { get; set; }

        /// <summary>
        /// Gets or sets the groupfolder identifier.
        /// </summary>
        /// <value>The groupfolder identifier.</value>
        [JsonProperty(PropertyName = "groupfolder_id")]
		public virtual string GroupfolderId { get; set; }

	}
}