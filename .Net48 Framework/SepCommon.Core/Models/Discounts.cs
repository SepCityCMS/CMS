// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Discounts.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class Discounts.
    /// </summary>
    public class Discounts
    {
        /// <summary>
        /// Gets or sets the bar code image.
        /// </summary>
        /// <value>The bar code image.</value>
        public string BarCodeImage { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the disclaimer.
        /// </summary>
        /// <value>The disclaimer.</value>
        public string Disclaimer { get; set; }

        /// <summary>
        /// Gets or sets the discount code.
        /// </summary>
        /// <value>The discount code.</value>
        public string DiscountCode { get; set; }

        /// <summary>
        /// Gets or sets the discount identifier.
        /// </summary>
        /// <value>The discount identifier.</value>
        public long DiscountID { get; set; }

        /// <summary>
        /// Gets or sets the expire date.
        /// </summary>
        /// <value>The expire date.</value>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string LabelText { get; set; }

        /// <summary>
        /// Gets or sets the mark off price.
        /// </summary>
        /// <value>The mark off price.</value>
        public string MarkOffPrice { get; set; }

        /// <summary>
        /// Gets or sets the type of the mark off.
        /// </summary>
        /// <value>The type of the mark off.</value>
        public string MarkOffType { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the product image.
        /// </summary>
        /// <value>The product image.</value>
        public string ProductImage { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public long Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show web].
        /// </summary>
        /// <value><c>true</c> if [show web]; otherwise, <c>false</c>.</value>
        public bool ShowWeb { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

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
        /// Gets or sets the visits.
        /// </summary>
        /// <value>The visits.</value>
        public long Visits { get; set; }
    }
}