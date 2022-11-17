// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="AuctionAds.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class AuctionAds.
    /// </summary>
    public class AuctionAds
    {
        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>The ad identifier.</value>
        public long AdID { get; set; }

        /// <summary>
        /// Gets or sets the bid increase.
        /// </summary>
        /// <value>The bid increase.</value>
        public string BidIncrease { get; set; }

        /// <summary>
        /// Gets or sets the bid user identifier.
        /// </summary>
        /// <value>The bid user identifier.</value>
        public string BidUserID { get; set; }

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
        /// Gets or sets the current bid.
        /// </summary>
        /// <value>The current bid.</value>
        public string CurrentBid { get; set; }

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
        /// Gets or sets a value indicating whether this <see cref="AuctionAds" /> is featured.
        /// </summary>
        /// <value><c>true</c> if featured; otherwise, <c>false</c>.</value>
        public bool Featured { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AuctionAds" /> is highlight.
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
        /// Gets or sets the maximum bid.
        /// </summary>
        /// <value>The maximum bid.</value>
        public string MaxBid { get; set; }

        /// <summary>
        /// Gets or sets the old ad.
        /// </summary>
        /// <value>The old ad.</value>
        public string OldAd { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

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
        /// Gets or sets the name of the sold user.
        /// </summary>
        /// <value>The name of the sold user.</value>
        public string SoldUserName { get; set; }

        /// <summary>
        /// Gets or sets the start bid.
        /// </summary>
        /// <value>The start bid.</value>
        public string StartBid { get; set; }

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
        /// Gets or sets the total bids.
        /// </summary>
        /// <value>The total bids.</value>
        public long TotalBids { get; set; }

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
    /// Class AuctionAdsFeedback.
    /// </summary>
    public class AuctionAdsFeedback
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