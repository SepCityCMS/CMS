// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 11-18-2019
//
// Last Modified By : spink
// Last Modified On : 11-18-2019
// ***********************************************************************
// <copyright file="StripeCharge.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    /// <summary>
    /// Class StripeCharge.
    /// </summary>
    public class StripeCharge
    {
        /// <summary>
        /// Gets or sets the address city.
        /// </summary>
        /// <value>The address city.</value>
        public string AddressCity { get; set; }

        /// <summary>
        /// Gets or sets the address country.
        /// </summary>
        /// <value>The address country.</value>
        public string AddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>The address line1.</value>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>The address line2.</value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the address postcode.
        /// </summary>
        /// <value>The address postcode.</value>
        public string AddressPostcode { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the name of the card holder.
        /// </summary>
        /// <value>The name of the card holder.</value>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }
    }
}