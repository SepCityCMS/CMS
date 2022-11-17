// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="EmailsText.cs" company="SepCity, Inc.">
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
    /// A class which represents the emails_text table.
    /// </summary>
    [ModuleProperty(ModuleName = "EmailsText", TableName = "emails_text")]
	public partial class EmailsText : EntityBase
	{
        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>The email identifier.</value>
        [JsonProperty(PropertyName = "email_id")]
		public virtual string EmailId { get; set; }

        /// <summary>
        /// Gets or sets from addr.
        /// </summary>
        /// <value>From addr.</value>
        [JsonProperty(PropertyName = "from_addr")]
		public virtual string FromAddr { get; set; }

        /// <summary>
        /// Gets or sets the reply to addr.
        /// </summary>
        /// <value>The reply to addr.</value>
        [JsonProperty(PropertyName = "reply_to_addr")]
		public virtual string ReplyToAddr { get; set; }

        /// <summary>
        /// Converts to addrs.
        /// </summary>
        /// <value>To addrs.</value>
        [JsonProperty(PropertyName = "to_addrs")]
		public virtual string ToAddrs { get; set; }

        /// <summary>
        /// Gets or sets the cc addrs.
        /// </summary>
        /// <value>The cc addrs.</value>
        [JsonProperty(PropertyName = "cc_addrs")]
		public virtual string CcAddrs { get; set; }

        /// <summary>
        /// Gets or sets the BCC addrs.
        /// </summary>
        /// <value>The BCC addrs.</value>
        [JsonProperty(PropertyName = "bcc_addrs")]
		public virtual string BccAddrs { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the description HTML.
        /// </summary>
        /// <value>The description HTML.</value>
        [JsonProperty(PropertyName = "description_html")]
		public virtual string DescriptionHtml { get; set; }

        /// <summary>
        /// Gets or sets the raw source.
        /// </summary>
        /// <value>The raw source.</value>
        [JsonProperty(PropertyName = "raw_source")]
		public virtual string RawSource { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}