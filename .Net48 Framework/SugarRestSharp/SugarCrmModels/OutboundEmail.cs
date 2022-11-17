// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="OutboundEmail.cs" company="SepCity, Inc.">
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
    /// A class which represents the outbound_email table.
    /// </summary>
    [ModuleProperty(ModuleName = "OutboundEmail", TableName = "outbound_email")]
	public partial class OutboundEmail : EntityBase
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
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty(PropertyName = "type")]
		public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonProperty(PropertyName = "user_id")]
		public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets the mail sendtype.
        /// </summary>
        /// <value>The mail sendtype.</value>
        [JsonProperty(PropertyName = "mail_sendtype")]
		public virtual string MailSendtype { get; set; }

        /// <summary>
        /// Gets or sets the mail smtptype.
        /// </summary>
        /// <value>The mail smtptype.</value>
        [JsonProperty(PropertyName = "mail_smtptype")]
		public virtual string MailSmtptype { get; set; }

        /// <summary>
        /// Gets or sets the mail smtpserver.
        /// </summary>
        /// <value>The mail smtpserver.</value>
        [JsonProperty(PropertyName = "mail_smtpserver")]
		public virtual string MailSmtpserver { get; set; }

        /// <summary>
        /// Gets or sets the mail smtpport.
        /// </summary>
        /// <value>The mail smtpport.</value>
        [JsonProperty(PropertyName = "mail_smtpport")]
		public virtual int? MailSmtpport { get; set; }

        /// <summary>
        /// Gets or sets the mail smtpuser.
        /// </summary>
        /// <value>The mail smtpuser.</value>
        [JsonProperty(PropertyName = "mail_smtpuser")]
		public virtual string MailSmtpuser { get; set; }

        /// <summary>
        /// Gets or sets the mail smtppass.
        /// </summary>
        /// <value>The mail smtppass.</value>
        [JsonProperty(PropertyName = "mail_smtppass")]
		public virtual string MailSmtppass { get; set; }

        /// <summary>
        /// Gets or sets the mail smtpauth req.
        /// </summary>
        /// <value>The mail smtpauth req.</value>
        [JsonProperty(PropertyName = "mail_smtpauth_req")]
		public virtual sbyte? MailSmtpauthReq { get; set; }

        /// <summary>
        /// Gets or sets the mail SMTPSSL.
        /// </summary>
        /// <value>The mail SMTPSSL.</value>
        [JsonProperty(PropertyName = "mail_smtpssl")]
		public virtual int? MailSmtpssl { get; set; }

	}
}