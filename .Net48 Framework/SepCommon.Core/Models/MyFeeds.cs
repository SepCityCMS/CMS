// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="MyFeeds.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class MyFeeds.
    /// </summary>
    public class MyFeeds
    {
        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the feed identifier.
        /// </summary>
        /// <value>The feed identifier.</value>
        public long FeedID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value><c>true</c> if this instance is favorite; otherwise, <c>false</c>.</value>
        public bool isFavorite { get; set; }

        /// <summary>
        /// Gets or sets the json message.
        /// </summary>
        /// <value>The json message.</value>
        public string JsonMessage { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the more link.
        /// </summary>
        /// <value>The more link.</value>
        public string MoreLink { get; set; }

        /// <summary>
        /// Gets or sets the number comments.
        /// </summary>
        /// <value>The number comments.</value>
        public long NumComments { get; set; }

        /// <summary>
        /// Gets or sets the number dislikes.
        /// </summary>
        /// <value>The number dislikes.</value>
        public long NumDislikes { get; set; }

        /// <summary>
        /// Gets or sets the number likes.
        /// </summary>
        /// <value>The number likes.</value>
        public long NumLikes { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>The thumbnail.</value>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the time ago.
        /// </summary>
        /// <value>The time ago.</value>
        public string TimeAgo { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public long UniqueID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}