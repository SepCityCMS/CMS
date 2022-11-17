// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="RealEstateBrokers.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class RealEstateBrokers.
    /// </summary>
    public class RealEstateBrokers
    {
        /// <summary>
        /// Gets or sets the approval.
        /// </summary>
        /// <value>The approval.</value>
        public string Approval { get; set; }

        /// <summary>
        /// Gets or sets the broker identifier.
        /// </summary>
        /// <value>The broker identifier.</value>
        public long BrokerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the broker.
        /// </summary>
        /// <value>The name of the broker.</value>
        public string BrokerName { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show commission].
        /// </summary>
        /// <value><c>true</c> if [show commission]; otherwise, <c>false</c>.</value>
        public bool ShowCommission { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
    }
}