// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Businesses.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class Businesses.
    /// </summary>
    public class Businesses
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bold title].
        /// </summary>
        /// <value><c>true</c> if [bold title]; otherwise, <c>false</c>.</value>
        public bool BoldTitle { get; set; }

        /// <summary>
        /// Gets or sets the business identifier.
        /// </summary>
        /// <value>The business identifier.</value>
        public long BusinessID { get; set; }

        /// <summary>
        /// Gets or sets the name of the business.
        /// </summary>
        /// <value>The name of the business.</value>
        public string BusinessName { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the claim identifier.
        /// </summary>
        /// <value>The claim identifier.</value>
        public long ClaimID { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        /// <value>The contact email.</value>
        public string ContactEmail { get; set; }

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
        /// Gets or sets the facebook link.
        /// </summary>
        /// <value>The facebook link.</value>
        public string FacebookLink { get; set; }

        /// <summary>
        /// Gets or sets the fax number.
        /// </summary>
        /// <value>The fax number.</value>
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Businesses" /> is featured.
        /// </summary>
        /// <value><c>true</c> if featured; otherwise, <c>false</c>.</value>
        public bool Featured { get; set; }

        /// <summary>
        /// Gets or sets the full description.
        /// </summary>
        /// <value>The full description.</value>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Businesses" /> is highlight.
        /// </summary>
        /// <value><c>true</c> if highlight; otherwise, <c>false</c>.</value>
        public bool Highlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include map].
        /// </summary>
        /// <value><c>true</c> if [include map]; otherwise, <c>false</c>.</value>
        public bool IncludeMap { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include profile].
        /// </summary>
        /// <value><c>true</c> if [include profile]; otherwise, <c>false</c>.</value>
        public bool IncludeProfile { get; set; }

        /// <summary>
        /// Gets or sets the linked in link.
        /// </summary>
        /// <value>The linked in link.</value>
        public string LinkedInLink { get; set; }

        /// <summary>
        /// Gets or sets the link identifier.
        /// </summary>
        /// <value>The link identifier.</value>
        public long LinkID { get; set; }

        /// <summary>
        /// Gets or sets the office hours.
        /// </summary>
        /// <value>The office hours.</value>
        public string OfficeHours { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>The site URL.</value>
        public string SiteURL { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the total comments.
        /// </summary>
        /// <value>The total comments.</value>
        public long TotalComments { get; set; }

        /// <summary>
        /// Gets or sets the twitter link.
        /// </summary>
        /// <value>The twitter link.</value>
        public string TwitterLink { get; set; }

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

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        public string ZipCode { get; set; }
    }
}