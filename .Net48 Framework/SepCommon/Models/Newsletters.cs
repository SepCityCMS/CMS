// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Newsletters.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class Newsletters.
    /// </summary>
    public class Newsletters
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the join keys.
        /// </summary>
        /// <value>The join keys.</value>
        public string JoinKeys { get; set; }

        /// <summary>
        /// Gets or sets the letter identifier.
        /// </summary>
        /// <value>The letter identifier.</value>
        public long LetterID { get; set; }

        /// <summary>
        /// Gets or sets the name of the letter.
        /// </summary>
        /// <value>The name of the letter.</value>
        public string LetterName { get; set; }

        /// <summary>
        /// Gets or sets the portal i ds.
        /// </summary>
        /// <value>The portal i ds.</value>
        public string PortalIDs { get; set; }
    }
}