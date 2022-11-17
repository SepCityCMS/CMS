// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="ReferralStats.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class ReferralStats.
    /// </summary>
    public class ReferralStats
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the stat identifier.
        /// </summary>
        /// <value>The stat identifier.</value>
        public long StatID { get; set; }

        /// <summary>
        /// Gets or sets the visitors.
        /// </summary>
        /// <value>The visitors.</value>
        public long Visitors { get; set; }
    }
}