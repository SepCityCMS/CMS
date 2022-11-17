// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="NewslettersSent.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class NewslettersSent.
    /// </summary>
    public class NewslettersSent
    {
        /// <summary>
        /// Gets or sets the date sent.
        /// </summary>
        /// <value>The date sent.</value>
        public DateTime DateSent { get; set; }

        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        /// <value>The email body.</value>
        public string EmailBody { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>The email subject.</value>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the sent identifier.
        /// </summary>
        /// <value>The sent identifier.</value>
        public long SentID { get; set; }
    }
}