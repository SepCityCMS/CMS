// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ClassifiedAds.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class ClassifiedAds.
    /// </summary>
    public class ClassifiedAds
    {
        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>The ad identifier.</value>
        public long AdID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bold title].
        /// </summary>
        /// <value><c>true</c> if [bold title]; otherwise, <c>false</c>.</value>
        public bool BoldTitle { get; set; }

        /// <summary>
        /// Gets or sets the buyer feedback identifier.
        /// </summary>
        /// <value>The buyer feedback identifier.</value>
        public string BuyerFeedbackID { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the default picture.
        /// </summary>
        /// <value>The default picture.</value>
        public string DefaultPicture { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClassifiedAds" /> is featured.
        /// </summary>
        /// <value><c>true</c> if featured; otherwise, <c>false</c>.</value>
        public bool Featured { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClassifiedAds" /> is highlight.
        /// </summary>
        /// <value><c>true</c> if highlight; otherwise, <c>false</c>.</value>
        public bool Highlight { get; set; }

        /// <summary>
        /// Gets or sets the link identifier.
        /// </summary>
        /// <value>The link identifier.</value>
        public long LinkID { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the pay pal email.
        /// </summary>
        /// <value>The pay pal email.</value>
        public string PayPalEmail { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public long Quantity { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the seller feedback identifier.
        /// </summary>
        /// <value>The seller feedback identifier.</value>
        public string SellerFeedbackID { get; set; }

        /// <summary>
        /// Gets or sets the name of the seller user.
        /// </summary>
        /// <value>The name of the seller user.</value>
        public string SellerUserName { get; set; }

        /// <summary>
        /// Gets or sets the sold date.
        /// </summary>
        /// <value>The sold date.</value>
        public DateTime SoldDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClassifiedAds" /> is soldout.
        /// </summary>
        /// <value><c>true</c> if soldout; otherwise, <c>false</c>.</value>
        public bool Soldout { get; set; }

        /// <summary>
        /// Gets or sets the sold user identifier.
        /// </summary>
        /// <value>The sold user identifier.</value>
        public string SoldUserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the sold user.
        /// </summary>
        /// <value>The name of the sold user.</value>
        public string SoldUserName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

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

    /// <summary>
    /// Class ClassifiedAdsFeedback.
    /// </summary>
    public class ClassifiedAdsFeedback
    {
        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>The ad identifier.</value>
        public long AdID { get; set; }

        /// <summary>
        /// Gets or sets the feedback identifier.
        /// </summary>
        /// <value>The feedback identifier.</value>
        public long FeedbackID { get; set; }
    }
}