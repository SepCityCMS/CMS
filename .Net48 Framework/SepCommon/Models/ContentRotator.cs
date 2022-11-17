// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ContentRotator.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class ContentRotator.
    /// </summary>
    public class ContentRotator
    {
        /// <summary>
        /// Gets or sets the cat i ds.
        /// </summary>
        /// <value>The cat i ds.</value>
        public string CatIDs { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>The content identifier.</value>
        public long ContentID { get; set; }

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
        /// Gets or sets the content of the HTML.
        /// </summary>
        /// <value>The content of the HTML.</value>
        public string HTMLContent { get; set; }

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
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the zone identifier.
        /// </summary>
        /// <value>The zone identifier.</value>
        public long ZoneID { get; set; }
    }
}