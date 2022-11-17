// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Members.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class Members.
    /// </summary>
    public class Members
    {
        /// <summary>
        /// Gets or sets the access class.
        /// </summary>
        /// <value>The access class.</value>
        public long AccessClass { get; set; }

        /// <summary>
        /// Gets or sets the access keys.
        /// </summary>
        /// <value>The access keys.</value>
        public string AccessKeys { get; set; }

        /// <summary>
        /// Gets or sets the act count.
        /// </summary>
        /// <value>The act count.</value>
        public string ActCount { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier.
        /// </summary>
        /// <value>The affiliate identifier.</value>
        public long AffiliateID { get; set; }

        /// <summary>
        /// Gets or sets the affiliate paid.
        /// </summary>
        /// <value>The affiliate paid.</value>
        public DateTime AffiliatePaid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [approve friends].
        /// </summary>
        /// <value><c>true</c> if [approve friends]; otherwise, <c>false</c>.</value>
        public bool ApproveFriends { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the class changed.
        /// </summary>
        /// <value>The class changed.</value>
        public DateTime ClassChanged { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the facebook identifier.
        /// </summary>
        /// <value>The facebook identifier.</value>
        public string Facebook_Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide tips].
        /// </summary>
        /// <value><c>true</c> if [hide tips]; otherwise, <c>false</c>.</value>
        public bool HideTips { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IPAddress { get; set; }

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
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password reset date.
        /// </summary>
        /// <value>The password reset date.</value>
        public DateTime PasswordResetDate { get; set; }

        /// <summary>
        /// Gets or sets the password reset identifier.
        /// </summary>
        /// <value>The password reset identifier.</value>
        public long PasswordResetID { get; set; }

        /// <summary>
        /// Gets or sets the pay pal.
        /// </summary>
        /// <value>The pay pal.</value>
        public string PayPal { get; set; }

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
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the referral identifier.
        /// </summary>
        /// <value>The referral identifier.</value>
        public long ReferralID { get; set; }

        /// <summary>
        /// Gets or sets the name of the referral user.
        /// </summary>
        /// <value>The name of the referral user.</value>
        public string ReferralUserName { get; set; }

        /// <summary>
        /// Gets or sets the secret answer.
        /// </summary>
        /// <value>The secret answer.</value>
        public string Secret_Answer { get; set; }

        /// <summary>
        /// Gets or sets the secret question.
        /// </summary>
        /// <value>The secret question.</value>
        public string Secret_Question { get; set; }

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
        /// Gets or sets the street address.
        /// </summary>
        /// <value>The street address.</value>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the street number.
        /// </summary>
        /// <value>The street number.</value>
        public string StreetNumber { get; set; }

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
        /// Gets or sets the user notes.
        /// </summary>
        /// <value>The user notes.</value>
        public string UserNotes { get; set; }

        /// <summary>
        /// Gets or sets the user points.
        /// </summary>
        /// <value>The user points.</value>
        public long UserPoints { get; set; }

        /// <summary>
        /// Gets or sets the website URL.
        /// </summary>
        /// <value>The website URL.</value>
        public string WebsiteURL { get; set; }
    }
}