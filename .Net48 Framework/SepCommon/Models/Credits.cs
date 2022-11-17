// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Credits.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class Credits.
    /// </summary>
    public class Credits
    {
        /// <summary>
        /// Gets or sets the credit identifier.
        /// </summary>
        /// <value>The credit identifier.</value>
        public long CreditID { get; set; }

        /// <summary>
        /// Gets or sets the name of the credits.
        /// </summary>
        /// <value>The name of the credits.</value>
        public string CreditsName { get; set; }

        /// <summary>
        /// Gets or sets the number credits.
        /// </summary>
        /// <value>The number credits.</value>
        public int NumCredits { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public string Price { get; set; }
    }
}