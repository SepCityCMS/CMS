// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Polls.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class Polls.
    /// </summary>
    public class Polls
    {
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the option identifier.
        /// </summary>
        /// <value>The option identifier.</value>
        public long OptionId { get; set; }

        /// <summary>
        /// Gets or sets the option ids.
        /// </summary>
        /// <value>The option ids.</value>
        public string OptionIds { get; set; }

        /// <summary>
        /// Gets or sets the option values.
        /// </summary>
        /// <value>The option values.</value>
        public string OptionValues { get; set; }

        /// <summary>
        /// Gets or sets the poll identifier.
        /// </summary>
        /// <value>The poll identifier.</value>
        public long PollID { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }
    }
}