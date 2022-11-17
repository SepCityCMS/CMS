// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="ShopShippingMethods.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class ShopShippingMethods.
    /// </summary>
    public class ShopShippingMethods
    {
        /// <summary>
        /// Gets or sets the calculation.
        /// </summary>
        /// <value>The calculation.</value>
        public int Calculation { get; set; }

        /// <summary>
        /// Gets or sets the carrier.
        /// </summary>
        /// <value>The carrier.</value>
        public string Carrier { get; set; }

        /// <summary>
        /// Gets or sets the delivery time.
        /// </summary>
        /// <value>The delivery time.</value>
        public string DeliveryTime { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the method identifier.
        /// </summary>
        /// <value>The method identifier.</value>
        public long MethodID { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the shipping service.
        /// </summary>
        /// <value>The shipping service.</value>
        public string ShippingService { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the store identifier.
        /// </summary>
        /// <value>The store identifier.</value>
        public long StoreID { get; set; }

        /// <summary>
        /// Gets or sets the weight limit.
        /// </summary>
        /// <value>The weight limit.</value>
        public string WeightLimit { get; set; }
    }
}