// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="EmailTemplates.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class EmailTemplates.
    /// </summary>
    public class EmailTemplates
    {
        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

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
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public long TemplateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }
    }
}