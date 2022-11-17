// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Taxes.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class Taxes.
    /// </summary>
    public class Taxes
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the tax identifier.
        /// </summary>
        /// <value>The tax identifier.</value>
        public long TaxID { get; set; }

        /// <summary>
        /// Gets or sets the name of the tax.
        /// </summary>
        /// <value>The name of the tax.</value>
        public string TaxName { get; set; }

        /// <summary>
        /// Gets or sets the tax percent.
        /// </summary>
        /// <value>The tax percent.</value>
        public string TaxPercent { get; set; }
    }
}