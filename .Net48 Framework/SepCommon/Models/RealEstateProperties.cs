// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="RealEstateProperties.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class RealEstateProperties.
    /// </summary>
    public class RealEstateProperties
    {
        /// <summary>
        /// Gets or sets the agent identifier.
        /// </summary>
        /// <value>The agent identifier.</value>
        public long AgentID { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>The county.</value>
        public string County { get; set; }

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
        /// Gets or sets the feature exterior.
        /// </summary>
        /// <value>The feature exterior.</value>
        public string FeatureExterior { get; set; }

        /// <summary>
        /// Gets or sets the feature interior.
        /// </summary>
        /// <value>The feature interior.</value>
        public string FeatureInterior { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [for sale].
        /// </summary>
        /// <value><c>true</c> if [for sale]; otherwise, <c>false</c>.</value>
        public bool ForSale { get; set; }

        /// <summary>
        /// Gets or sets the garage.
        /// </summary>
        /// <value>The garage.</value>
        public string Garage { get; set; }

        /// <summary>
        /// Gets or sets the heating.
        /// </summary>
        /// <value>The heating.</value>
        public string Heating { get; set; }

        /// <summary>
        /// Gets or sets the listing identifier.
        /// </summary>
        /// <value>The listing identifier.</value>
        public long ListingID { get; set; }

        /// <summary>
        /// Gets or sets the MLS number.
        /// </summary>
        /// <value>The MLS number.</value>
        public string MLSNumber { get; set; }

        /// <summary>
        /// Gets or sets the number bathrooms.
        /// </summary>
        /// <value>The number bathrooms.</value>
        public decimal NumBathrooms { get; set; }

        /// <summary>
        /// Gets or sets the number bedrooms.
        /// </summary>
        /// <value>The number bedrooms.</value>
        public decimal NumBedrooms { get; set; }

        /// <summary>
        /// Gets or sets the number rooms.
        /// </summary>
        /// <value>The number rooms.</value>
        public int NumRooms { get; set; }

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
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>The property identifier.</value>
        public long PropertyID { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        public int PropertyType { get; set; }

        /// <summary>
        /// Gets or sets the recurring cycle.
        /// </summary>
        /// <value>The recurring cycle.</value>
        public string RecurringCycle { get; set; }

        /// <summary>
        /// Gets or sets the size dining room.
        /// </summary>
        /// <value>The size dining room.</value>
        public string SizeDiningRoom { get; set; }

        /// <summary>
        /// Gets or sets the size kitchen.
        /// </summary>
        /// <value>The size kitchen.</value>
        public string SizeKitchen { get; set; }

        /// <summary>
        /// Gets or sets the size living room.
        /// </summary>
        /// <value>The size living room.</value>
        public string SizeLivingRoom { get; set; }

        /// <summary>
        /// Gets or sets the size lot.
        /// </summary>
        /// <value>The size lot.</value>
        public string SizeLot { get; set; }

        /// <summary>
        /// Gets or sets the size m bedroom.
        /// </summary>
        /// <value>The size m bedroom.</value>
        public string SizeMBedroom { get; set; }

        /// <summary>
        /// Gets or sets the sq feet.
        /// </summary>
        /// <value>The sq feet.</value>
        public string SQFeet { get; set; }

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
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public int Style { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>The user name.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the visits.
        /// </summary>
        /// <value>The visits.</value>
        public long Visits { get; set; }

        /// <summary>
        /// Gets or sets the year built.
        /// </summary>
        /// <value>The year built.</value>
        public string YearBuilt { get; set; }
    }
}