// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="AffiliateImages.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class AffiliateImages.
    /// </summary>
    public class AffiliateImages
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

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
        /// Gets or sets the image identifier.
        /// </summary>
        /// <value>The image identifier.</value>
        public long ImageID { get; set; }

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
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public long UniqueID { get; set; }
    }
}