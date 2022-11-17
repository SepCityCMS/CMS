// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="FriendsList.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class FriendsList.
    /// </summary>
    public class FriendsList
    {
        /// <summary>
        /// Gets or sets the added user identifier.
        /// </summary>
        /// <value>The added user identifier.</value>
        public string AddedUserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FriendsList" /> is approved.
        /// </summary>
        /// <value><c>true</c> if approved; otherwise, <c>false</c>.</value>
        public bool Approved { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        /// <value>The friend identifier.</value>
        public long FriendID { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value><c>true</c> if this instance is online; otherwise, <c>false</c>.</value>
        public bool isOnline { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
    }
}