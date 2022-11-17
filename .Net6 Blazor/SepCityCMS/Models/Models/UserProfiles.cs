// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UserProfiles.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class UserProfiles.
    /// </summary>
    public class UserProfiles
    {
        /// <summary>
        /// Gets or sets the about me.
        /// </summary>
        /// <value>The about me.</value>
        public string AboutMe { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The age.</value>
        public double Age { get; set; }

        /// <summary>
        /// Gets or sets the color of the bg.
        /// </summary>
        /// <value>The color of the bg.</value>
        public string BGColor { get; set; }

        /// <summary>
        /// Gets or sets the causes.
        /// </summary>
        /// <value>The causes.</value>
        public string Causes { get; set; }

        /// <summary>
        /// Gets or sets the charities.
        /// </summary>
        /// <value>The charities.</value>
        public string Charities { get; set; }

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
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public string Distance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable comment].
        /// </summary>
        /// <value><c>true</c> if [enable comment]; otherwise, <c>false</c>.</value>
        public bool EnableComment { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hot or not].
        /// </summary>
        /// <value><c>true</c> if [hot or not]; otherwise, <c>false</c>.</value>
        public bool HotOrNot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value><c>true</c> if this instance is online; otherwise, <c>false</c>.</value>
        public bool isOnline { get; set; }

        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>The last login.</value>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the color of the link.
        /// </summary>
        /// <value>The color of the link.</value>
        public string LinkColor { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the member since.
        /// </summary>
        /// <value>The member since.</value>
        public DateTime MemberSince { get; set; }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>The occupation.</value>
        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the profile identifier.
        /// </summary>
        /// <value>The profile identifier.</value>
        public long ProfileID { get; set; }

        /// <summary>
        /// Gets or sets the type of the profile.
        /// </summary>
        /// <value>The type of the profile.</value>
        public int ProfileType { get; set; }

        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        /// <value>The sex.</value>
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public string TextColor { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>The views.</value>
        public long Views { get; set; }
    }
}