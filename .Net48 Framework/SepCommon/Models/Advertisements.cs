// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Advertisements.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class Advertisements.
    /// </summary>
    public class Advertisements
    {
        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>The ad identifier.</value>
        public long AdID { get; set; }

        /// <summary>
        /// Gets or sets the advertisement preview.
        /// </summary>
        /// <value>The advertisement preview.</value>
        public string AdvertisementPreview { get; set; }

        /// <summary>
        /// Gets or sets the cat i ds.
        /// </summary>
        /// <value>The cat i ds.</value>
        public string CatIDs { get; set; }

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
        /// Gets or sets the HTML code.
        /// </summary>
        /// <value>The HTML code.</value>
        public string HTMLCode { get; set; }

        /// <summary>
        /// Gets or sets the image data.
        /// </summary>
        /// <value>The image data.</value>
        public string ImageData { get; set; }

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        /// <value>The type of the image.</value>
        public string ImageType { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the maximum clicks.
        /// </summary>
        /// <value>The maximum clicks.</value>
        public long MaximumClicks { get; set; }

        /// <summary>
        /// Gets or sets the maximum exposures.
        /// </summary>
        /// <value>The maximum exposures.</value>
        public long MaximumExposures { get; set; }

        /// <summary>
        /// Gets or sets the page i ds.
        /// </summary>
        /// <value>The page i ds.</value>
        public string PageIDs { get; set; }

        /// <summary>
        /// Gets or sets the portal i ds.
        /// </summary>
        /// <value>The portal i ds.</value>
        public string PortalIDs { get; set; }

        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        /// <value>The ratio.</value>
        public string Ratio { get; set; }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>The site URL.</value>
        public string SiteURL { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the total clicks.
        /// </summary>
        /// <value>The total clicks.</value>
        public long TotalClicks { get; set; }

        /// <summary>
        /// Gets or sets the total exposures.
        /// </summary>
        /// <value>The total exposures.</value>
        public long TotalExposures { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use HTML].
        /// </summary>
        /// <value><c>true</c> if [use HTML]; otherwise, <c>false</c>.</value>
        public bool UseHTML { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the zone identifier.
        /// </summary>
        /// <value>The zone identifier.</value>
        public long ZoneID { get; set; }

        /// <summary>
        /// Gets or sets the name of the zone.
        /// </summary>
        /// <value>The name of the zone.</value>
        public string ZoneName { get; set; }
    }
}