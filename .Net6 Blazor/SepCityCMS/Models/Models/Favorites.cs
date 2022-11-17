// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Favorites.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class Favorites.
    /// </summary>
    public class Favorites
    {
        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the favorite identifier.
        /// </summary>
        /// <value>The favorite identifier.</value>
        public long FavoriteID { get; set; }

        /// <summary>
        /// Gets or sets the last accessed.
        /// </summary>
        /// <value>The last accessed.</value>
        public DateTime LastAccessed { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>The page title.</value>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>The page URL.</value>
        public string PageURL { get; set; }
    }
}