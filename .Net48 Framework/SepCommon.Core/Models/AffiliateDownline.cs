// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="AffiliateDownline.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class AffiliateDownline.
    /// </summary>
    public class AffiliateDownline
    {
        /// <summary>
        /// Gets or sets the affiliate identifier.
        /// </summary>
        /// <value>The affiliate identifier.</value>
        public long AffiliateID { get; set; }

        /// <summary>
        /// Gets or sets the date joined.
        /// </summary>
        /// <value>The date joined.</value>
        public DateTime DateJoined { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the has levels.
        /// </summary>
        /// <value>The has levels.</value>
        public string HasLevels { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the previous affiliate identifier.
        /// </summary>
        /// <value>The previous affiliate identifier.</value>
        public long PrevAffiliateID { get; set; }

        /// <summary>
        /// Gets or sets the referral identifier.
        /// </summary>
        /// <value>The referral identifier.</value>
        public long ReferralID { get; set; }

        /// <summary>
        /// Gets or sets the total earnings.
        /// </summary>
        /// <value>The total earnings.</value>
        public double TotalEarnings { get; set; }

        /// <summary>
        /// Gets or sets the total volume.
        /// </summary>
        /// <value>The total volume.</value>
        public double TotalVolume { get; set; }

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