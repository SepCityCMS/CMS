// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Prospect.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    /// A class which represents the prospects table.
    /// </summary>
    [ModuleProperty(ModuleName = "Prospects", TableName = "prospects")]
	public partial class Prospect : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

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
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>The salutation.</value>
        [JsonProperty(PropertyName = "salutation")]
		public virtual string Salutation { get; set; }

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
        /// Gets or sets the do not call.
        /// </summary>
        /// <value>The do not call.</value>
        [JsonProperty(PropertyName = "do_not_call")]
		public virtual sbyte? DoNotCall { get; set; }

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
        /// Gets or sets the primary address street.
        /// </summary>
        /// <value>The primary address street.</value>
        [JsonProperty(PropertyName = "primary_address_street")]
		public virtual string PrimaryAddressStreet { get; set; }

        /// <summary>
        /// Gets or sets the primary address city.
        /// </summary>
        /// <value>The primary address city.</value>
        [JsonProperty(PropertyName = "primary_address_city")]
		public virtual string PrimaryAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the primary address.
        /// </summary>
        /// <value>The state of the primary address.</value>
        [JsonProperty(PropertyName = "primary_address_state")]
		public virtual string PrimaryAddressState { get; set; }

        /// <summary>
        /// Gets or sets the primary address postalcode.
        /// </summary>
        /// <value>The primary address postalcode.</value>
        [JsonProperty(PropertyName = "primary_address_postalcode")]
		public virtual string PrimaryAddressPostalcode { get; set; }

        /// <summary>
        /// Gets or sets the primary address country.
        /// </summary>
        /// <value>The primary address country.</value>
        [JsonProperty(PropertyName = "primary_address_country")]
		public virtual string PrimaryAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the alt address street.
        /// </summary>
        /// <value>The alt address street.</value>
        [JsonProperty(PropertyName = "alt_address_street")]
		public virtual string AltAddressStreet { get; set; }

        /// <summary>
        /// Gets or sets the alt address city.
        /// </summary>
        /// <value>The alt address city.</value>
        [JsonProperty(PropertyName = "alt_address_city")]
		public virtual string AltAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the alt address.
        /// </summary>
        /// <value>The state of the alt address.</value>
        [JsonProperty(PropertyName = "alt_address_state")]
		public virtual string AltAddressState { get; set; }

        /// <summary>
        /// Gets or sets the alt address postalcode.
        /// </summary>
        /// <value>The alt address postalcode.</value>
        [JsonProperty(PropertyName = "alt_address_postalcode")]
		public virtual string AltAddressPostalcode { get; set; }

        /// <summary>
        /// Gets or sets the alt address country.
        /// </summary>
        /// <value>The alt address country.</value>
        [JsonProperty(PropertyName = "alt_address_country")]
		public virtual string AltAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the assistant.
        /// </summary>
        /// <value>The assistant.</value>
        [JsonProperty(PropertyName = "assistant")]
		public virtual string Assistant { get; set; }

        /// <summary>
        /// Gets or sets the assistant phone.
        /// </summary>
        /// <value>The assistant phone.</value>
        [JsonProperty(PropertyName = "assistant_phone")]
		public virtual string AssistantPhone { get; set; }

        /// <summary>
        /// Gets or sets the tracker key.
        /// </summary>
        /// <value>The tracker key.</value>
        [JsonProperty(PropertyName = "tracker_key")]
		public virtual int TrackerKey { get; set; }

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        /// <value>The birthdate.</value>
        [JsonProperty(PropertyName = "birthdate")]
		public virtual DateTime? Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the lead identifier.
        /// </summary>
        /// <value>The lead identifier.</value>
        [JsonProperty(PropertyName = "lead_id")]
		public virtual string LeadId { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>The name of the account.</value>
        [JsonProperty(PropertyName = "account_name")]
		public virtual string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        [JsonProperty(PropertyName = "campaign_id")]
		public virtual string CampaignId { get; set; }

	}
}