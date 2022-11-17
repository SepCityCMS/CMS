// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Account.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591

namespace SugarRestSharp.Models
{
	using System;
	using Newtonsoft.Json;


    /// <summary>
    /// A class which represents the accounts table.
    /// </summary>
    [ModuleProperty(ModuleName = "Accounts", TableName = "accounts")]
	public partial class Account : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
		public virtual string Name { get; set; }

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
        /// Gets or sets the type of the account.
        /// </summary>
        /// <value>The type of the account.</value>
        [JsonProperty(PropertyName = "account_type")]
		public virtual string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>The industry.</value>
        [JsonProperty(PropertyName = "industry")]
		public virtual string Industry { get; set; }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        /// <value>The annual revenue.</value>
        [JsonProperty(PropertyName = "annual_revenue")]
		public virtual string AnnualRevenue { get; set; }

        /// <summary>
        /// Gets or sets the phone fax.
        /// </summary>
        /// <value>The phone fax.</value>
        [JsonProperty(PropertyName = "phone_fax")]
		public virtual string PhoneFax { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        [JsonProperty(PropertyName = "rating")]
		public virtual string Rating { get; set; }

        /// <summary>
        /// Gets or sets the phone office.
        /// </summary>
        /// <value>The phone office.</value>
        [JsonProperty(PropertyName = "phone_office")]
		public virtual string PhoneOffice { get; set; }

        /// <summary>
        /// Gets or sets the phone alternate.
        /// </summary>
        /// <value>The phone alternate.</value>
        [JsonProperty(PropertyName = "phone_alternate")]
		public virtual string PhoneAlternate { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        [JsonProperty(PropertyName = "website")]
		public virtual string Website { get; set; }

        /// <summary>
        /// Gets or sets the ownership.
        /// </summary>
        /// <value>The ownership.</value>
        [JsonProperty(PropertyName = "ownership")]
		public virtual string Ownership { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonProperty(PropertyName = "email1")]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        [JsonProperty(PropertyName = "employees")]
		public virtual string Employees { get; set; }

        /// <summary>
        /// Gets or sets the ticker symbol.
        /// </summary>
        /// <value>The ticker symbol.</value>
        [JsonProperty(PropertyName = "ticker_symbol")]
		public virtual string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the address street.
        /// </summary>
        /// <value>The address street.</value>
        [JsonProperty(PropertyName = "billing_address_street")]
		public virtual string AddressStreet { get; set; }

        /// <summary>
        /// Gets or sets the address city.
        /// </summary>
        /// <value>The address city.</value>
        [JsonProperty(PropertyName = "billing_address_city")]
		public virtual string AddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the address.
        /// </summary>
        /// <value>The state of the address.</value>
        [JsonProperty(PropertyName = "billing_address_state")]
		public virtual string AddressState { get; set; }

        /// <summary>
        /// Gets or sets the address postalcode.
        /// </summary>
        /// <value>The address postalcode.</value>
        [JsonProperty(PropertyName = "billing_address_postalcode")]
		public virtual string AddressPostalcode { get; set; }

        /// <summary>
        /// Gets or sets the address country.
        /// </summary>
        /// <value>The address country.</value>
        [JsonProperty(PropertyName = "billing_address_country")]
		public virtual string AddressCountry { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        [JsonProperty(PropertyName = "parent_id")]
		public virtual string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the sic code.
        /// </summary>
        /// <value>The sic code.</value>
        [JsonProperty(PropertyName = "sic_code")]
		public virtual string SicCode { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        [JsonProperty(PropertyName = "campaign_id")]
		public virtual string CampaignId { get; set; }

	}
}