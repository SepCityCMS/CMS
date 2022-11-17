// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailCache.cs" company="SepCity, Inc.">
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
    /// A class which represents the email_cache table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailCache", TableName = "email_cache")]
	public partial class EmailCache : EntityBase
	{
        /// <summary>
        /// Gets or sets the ie identifier.
        /// </summary>
        /// <value>The ie identifier.</value>
        [JsonProperty(PropertyName = "ie_id")]
		public virtual string IeId { get; set; }

        /// <summary>
        /// Gets or sets the mbox.
        /// </summary>
        /// <value>The mbox.</value>
        [JsonProperty(PropertyName = "mbox")]
		public virtual string Mbox { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [JsonProperty(PropertyName = "subject")]
		public virtual string Subject { get; set; }

        /// <summary>
        /// Gets or sets the fromaddr.
        /// </summary>
        /// <value>The fromaddr.</value>
        [JsonProperty(PropertyName = "fromaddr")]
		public virtual string Fromaddr { get; set; }

        /// <summary>
        /// Gets or sets the toaddr.
        /// </summary>
        /// <value>The toaddr.</value>
        [JsonProperty(PropertyName = "toaddr")]
		public virtual string Toaddr { get; set; }

        /// <summary>
        /// Gets or sets the senddate.
        /// </summary>
        /// <value>The senddate.</value>
        [JsonProperty(PropertyName = "senddate")]
		public virtual DateTime? Senddate { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        [JsonProperty(PropertyName = "message_id")]
		public virtual string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the mailsize.
        /// </summary>
        /// <value>The mailsize.</value>
        [JsonProperty(PropertyName = "mailsize")]
		public virtual uint? Mailsize { get; set; }

        /// <summary>
        /// Gets or sets the imap uid.
        /// </summary>
        /// <value>The imap uid.</value>
        [JsonProperty(PropertyName = "imap_uid")]
		public virtual uint? ImapUid { get; set; }

        /// <summary>
        /// Gets or sets the msgno.
        /// </summary>
        /// <value>The msgno.</value>
        [JsonProperty(PropertyName = "msgno")]
		public virtual uint? Msgno { get; set; }

        /// <summary>
        /// Gets or sets the recent.
        /// </summary>
        /// <value>The recent.</value>
        [JsonProperty(PropertyName = "recent")]
		public virtual sbyte? Recent { get; set; }

        /// <summary>
        /// Gets or sets the flagged.
        /// </summary>
        /// <value>The flagged.</value>
        [JsonProperty(PropertyName = "flagged")]
		public virtual sbyte? Flagged { get; set; }

        /// <summary>
        /// Gets or sets the answered.
        /// </summary>
        /// <value>The answered.</value>
        [JsonProperty(PropertyName = "answered")]
		public virtual sbyte? Answered { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the seen.
        /// </summary>
        /// <value>The seen.</value>
        [JsonProperty(PropertyName = "seen")]
		public virtual sbyte? Seen { get; set; }

        /// <summary>
        /// Gets or sets the draft.
        /// </summary>
        /// <value>The draft.</value>
        [JsonProperty(PropertyName = "draft")]
		public virtual sbyte? Draft { get; set; }

	}
}