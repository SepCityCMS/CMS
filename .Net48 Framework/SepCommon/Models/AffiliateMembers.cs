// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="AffiliateMembers.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class AffiliateMembers.
    /// </summary>
    public class AffiliateMembers
    {
        /// <summary>
        /// Gets or sets the affiliate identifier.
        /// </summary>
        /// <value>The affiliate identifier.</value>
        public long AffiliateID { get; set; }

        /// <summary>
        /// Gets or sets the affiliate paid.
        /// </summary>
        /// <value>The affiliate paid.</value>
        public DateTime AffiliatePaid { get; set; }

        /// <summary>
        /// Gets or sets the payment from.
        /// </summary>
        /// <value>The payment from.</value>
        public string PaymentFrom { get; set; }

        /// <summary>
        /// Gets or sets the payout.
        /// </summary>
        /// <value>The payout.</value>
        public string Payout { get; set; }

        /// <summary>
        /// Gets or sets the pay pal.
        /// </summary>
        /// <value>The pay pal.</value>
        public string PayPal { get; set; }

        /// <summary>
        /// Gets or sets the revenue generated.
        /// </summary>
        /// <value>The revenue generated.</value>
        public string RevenueGenerated { get; set; }

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

        /// <summary>
        /// Gets or sets the website URL.
        /// </summary>
        /// <value>The website URL.</value>
        public string WebsiteURL { get; set; }
    }
}