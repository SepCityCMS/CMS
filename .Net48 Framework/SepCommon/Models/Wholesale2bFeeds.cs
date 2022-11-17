// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Wholesale2bFeeds.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class Wholesale2bFeeds.
    /// </summary>
    public class Wholesale2bFeeds
    {
        /// <summary>
        /// Gets or sets a value indicating whether [access hide].
        /// </summary>
        /// <value><c>true</c> if [access hide]; otherwise, <c>false</c>.</value>
        public bool AccessHide { get; set; }

        /// <summary>
        /// Gets or sets the access keys.
        /// </summary>
        /// <value>The access keys.</value>
        public string AccessKeys { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exc portal security].
        /// </summary>
        /// <value><c>true</c> if [exc portal security]; otherwise, <c>false</c>.</value>
        public bool ExcPortalSecurity { get; set; }

        /// <summary>
        /// Gets or sets the feed identifier.
        /// </summary>
        /// <value>The feed identifier.</value>
        public long FeedID { get; set; }

        /// <summary>
        /// Gets or sets the name of the feed.
        /// </summary>
        /// <value>The name of the feed.</value>
        public string FeedName { get; set; }

        /// <summary>
        /// Gets or sets the feed URL.
        /// </summary>
        /// <value>The feed URL.</value>
        public string FeedURL { get; set; }

        /// <summary>
        /// Gets or sets the portal i ds.
        /// </summary>
        /// <value>The portal i ds.</value>
        public string PortalIDs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Wholesale2bFeeds" /> is sharing.
        /// </summary>
        /// <value><c>true</c> if sharing; otherwise, <c>false</c>.</value>
        public bool Sharing { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }
    }
}