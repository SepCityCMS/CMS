// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="RealEstateReviews.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class RealEstateReviews.
    /// </summary>
    public class RealEstateReviews
    {
        /// <summary>
        /// Gets or sets the complaints.
        /// </summary>
        /// <value>The complaints.</value>
        public string Complaints { get; set; }

        /// <summary>
        /// Gets or sets the compliments.
        /// </summary>
        /// <value>The compliments.</value>
        public string Compliments { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tenant.
        /// </summary>
        /// <value><c>true</c> if this instance is tenant; otherwise, <c>false</c>.</value>
        public bool IsTenant { get; set; }

        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>The property identifier.</value>
        public long PropertyID { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        /// <value>The review identifier.</value>
        public long ReviewID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public long TenantID { get; set; }
    }
}