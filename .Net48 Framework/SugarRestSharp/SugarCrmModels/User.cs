// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="User.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591

namespace SugarRestSharp.Models
{
	using System;
	using Newtonsoft.Json;


    /// <summary>
    /// A class which represents the users table.
    /// </summary>
    [ModuleProperty(ModuleName = "Users", TableName = "users")]
	public partial class User : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [JsonProperty(PropertyName = "user_name")]
		public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user hash.
        /// </summary>
        /// <value>The user hash.</value>
        [JsonProperty(PropertyName = "user_hash")]
		public virtual string UserHash { get; set; }

        /// <summary>
        /// Gets or sets the system generated password.
        /// </summary>
        /// <value>The system generated password.</value>
        [JsonProperty(PropertyName = "system_generated_password")]
		public virtual sbyte? SystemGeneratedPassword { get; set; }

        /// <summary>
        /// Gets or sets the password last changed.
        /// </summary>
        /// <value>The password last changed.</value>
        [JsonProperty(PropertyName = "pwd_last_changed")]
		public virtual DateTime? PwdLastChanged { get; set; }

        /// <summary>
        /// Gets or sets the authenticate identifier.
        /// </summary>
        /// <value>The authenticate identifier.</value>
        [JsonProperty(PropertyName = "authenticate_id")]
		public virtual string AuthenticateId { get; set; }

        /// <summary>
        /// Gets or sets the sugar login.
        /// </summary>
        /// <value>The sugar login.</value>
        [JsonProperty(PropertyName = "sugar_login")]
		public virtual sbyte? SugarLogin { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [JsonProperty(PropertyName = "first_name")]
		public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [JsonProperty(PropertyName = "last_name")]
		public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the is admin.
        /// </summary>
        /// <value>The is admin.</value>
        [JsonProperty(PropertyName = "is_admin")]
		public virtual sbyte? IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the external authentication only.
        /// </summary>
        /// <value>The external authentication only.</value>
        [JsonProperty(PropertyName = "external_auth_only")]
		public virtual sbyte? ExternalAuthOnly { get; set; }

        /// <summary>
        /// Gets or sets the receive notifications.
        /// </summary>
        /// <value>The receive notifications.</value>
        [JsonProperty(PropertyName = "receive_notifications")]
		public virtual sbyte? ReceiveNotifications { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        [JsonProperty(PropertyName = "date_entered")]
		public virtual DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty(PropertyName = "title")]
		public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>The department.</value>
        [JsonProperty(PropertyName = "department")]
		public virtual string Department { get; set; }

        /// <summary>
        /// Gets or sets the phone home.
        /// </summary>
        /// <value>The phone home.</value>
        [JsonProperty(PropertyName = "phone_home")]
		public virtual string PhoneHome { get; set; }

        /// <summary>
        /// Gets or sets the phone mobile.
        /// </summary>
        /// <value>The phone mobile.</value>
        [JsonProperty(PropertyName = "phone_mobile")]
		public virtual string PhoneMobile { get; set; }

        /// <summary>
        /// Gets or sets the phone work.
        /// </summary>
        /// <value>The phone work.</value>
        [JsonProperty(PropertyName = "phone_work")]
		public virtual string PhoneWork { get; set; }

        /// <summary>
        /// Gets or sets the phone other.
        /// </summary>
        /// <value>The phone other.</value>
        [JsonProperty(PropertyName = "phone_other")]
		public virtual string PhoneOther { get; set; }

        /// <summary>
        /// Gets or sets the phone fax.
        /// </summary>
        /// <value>The phone fax.</value>
        [JsonProperty(PropertyName = "phone_fax")]
		public virtual string PhoneFax { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
		public virtual string Status { get; set; }

        /// <summary>
        /// Gets or sets the address street.
        /// </summary>
        /// <value>The address street.</value>
        [JsonProperty(PropertyName = "address_street")]
		public virtual string AddressStreet { get; set; }

        /// <summary>
        /// Gets or sets the address city.
        /// </summary>
        /// <value>The address city.</value>
        [JsonProperty(PropertyName = "address_city")]
		public virtual string AddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the address.
        /// </summary>
        /// <value>The state of the address.</value>
        [JsonProperty(PropertyName = "address_state")]
		public virtual string AddressState { get; set; }

        /// <summary>
        /// Gets or sets the address country.
        /// </summary>
        /// <value>The address country.</value>
        [JsonProperty(PropertyName = "address_country")]
		public virtual string AddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the address postalcode.
        /// </summary>
        /// <value>The address postalcode.</value>
        [JsonProperty(PropertyName = "address_postalcode")]
		public virtual string AddressPostalcode { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the portal only.
        /// </summary>
        /// <value>The portal only.</value>
        [JsonProperty(PropertyName = "portal_only")]
		public virtual sbyte? PortalOnly { get; set; }

        /// <summary>
        /// Gets or sets the show on employees.
        /// </summary>
        /// <value>The show on employees.</value>
        [JsonProperty(PropertyName = "show_on_employees")]
		public virtual sbyte? ShowOnEmployees { get; set; }

        /// <summary>
        /// Gets or sets the employee status.
        /// </summary>
        /// <value>The employee status.</value>
        [JsonProperty(PropertyName = "employee_status")]
		public virtual string EmployeeStatus { get; set; }

        /// <summary>
        /// Gets or sets the messenger identifier.
        /// </summary>
        /// <value>The messenger identifier.</value>
        [JsonProperty(PropertyName = "messenger_id")]
		public virtual string MessengerId { get; set; }

        /// <summary>
        /// Gets or sets the type of the messenger.
        /// </summary>
        /// <value>The type of the messenger.</value>
        [JsonProperty(PropertyName = "messenger_type")]
		public virtual string MessengerType { get; set; }

        /// <summary>
        /// Gets or sets the reports to identifier.
        /// </summary>
        /// <value>The reports to identifier.</value>
        [JsonProperty(PropertyName = "reports_to_id")]
		public virtual string ReportsToId { get; set; }

        /// <summary>
        /// Gets or sets the is group.
        /// </summary>
        /// <value>The is group.</value>
        [JsonProperty(PropertyName = "is_group")]
		public virtual sbyte? IsGroup { get; set; }

	}
}