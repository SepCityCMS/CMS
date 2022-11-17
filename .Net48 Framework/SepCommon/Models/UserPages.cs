// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="UserPages.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class UserPages.
    /// </summary>
    public class UserPages
    {
        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserPages" /> is guestbook.
        /// </summary>
        /// <value><c>true</c> if guestbook; otherwise, <c>false</c>.</value>
        public bool Guestbook { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [invite only].
        /// </summary>
        /// <value><c>true</c> if [invite only]; otherwise, <c>false</c>.</value>
        public bool InviteOnly { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show list].
        /// </summary>
        /// <value><c>true</c> if [show list]; otherwise, <c>false</c>.</value>
        public bool ShowList { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public long SiteID { get; set; }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the slogan.
        /// </summary>
        /// <value>The slogan.</value>
        public string Slogan { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public long TemplateID { get; set; }

        /// <summary>
        /// Gets or sets the total comments.
        /// </summary>
        /// <value>The total comments.</value>
        public long TotalComments { get; set; }

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

        /// <summary>
        /// Gets or sets the visits.
        /// </summary>
        /// <value>The visits.</value>
        public long Visits { get; set; }
    }
}