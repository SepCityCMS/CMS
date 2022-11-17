// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="RealEstateTenants.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class RealEstateTenants.
    /// </summary>
    public class RealEstateTenants
    {
        /// <summary>
        /// Gets or sets the average rating.
        /// </summary>
        /// <value>The average rating.</value>
        public double AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the default picture.
        /// </summary>
        /// <value>The default picture.</value>
        public string DefaultPicture { get; set; }

        /// <summary>
        /// Gets or sets the moved in.
        /// </summary>
        /// <value>The moved in.</value>
        public DateTime MovedIn { get; set; }

        /// <summary>
        /// Gets or sets the moved out.
        /// </summary>
        /// <value>The moved out.</value>
        public DateTime MovedOut { get; set; }

        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>The property identifier.</value>
        public long PropertyID { get; set; }

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

        /// <summary>
        /// Gets or sets the name of the tenant.
        /// </summary>
        /// <value>The name of the tenant.</value>
        public string TenantName { get; set; }

        /// <summary>
        /// Gets or sets the tenant number.
        /// </summary>
        /// <value>The tenant number.</value>
        public string TenantNumber { get; set; }
    }
}