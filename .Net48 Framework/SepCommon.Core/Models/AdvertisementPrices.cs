// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="AdvertisementPrices.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    /// <summary>
    /// Class AdvertisementPrices.
    /// </summary>
    public class AdvertisementPrices
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public string Categories { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the long price.
        /// </summary>
        /// <value>The long price.</value>
        public string LongPrice { get; set; }

        /// <summary>
        /// Gets or sets the maximum clicks.
        /// </summary>
        /// <value>The maximum clicks.</value>
        public long MaximumClicks { get; set; }

        /// <summary>
        /// Gets or sets the maximum exposures.
        /// </summary>
        /// <value>The maximum exposures.</value>
        public long MaximumExposures { get; set; }

        /// <summary>
        /// Gets or sets the onetime price.
        /// </summary>
        /// <value>The onetime price.</value>
        public string OnetimePrice { get; set; }

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>The pages.</value>
        public string Pages { get; set; }

        /// <summary>
        /// Gets or sets the name of the plan.
        /// </summary>
        /// <value>The name of the plan.</value>
        public string PlanName { get; set; }

        /// <summary>
        /// Gets or sets the portals.
        /// </summary>
        /// <value>The portals.</value>
        public string Portals { get; set; }

        /// <summary>
        /// Gets or sets the price identifier.
        /// </summary>
        /// <value>The price identifier.</value>
        public long PriceID { get; set; }

        /// <summary>
        /// Gets or sets the recurring cycle.
        /// </summary>
        /// <value>The recurring cycle.</value>
        public string RecurringCycle { get; set; }

        /// <summary>
        /// Gets or sets the recurring price.
        /// </summary>
        /// <value>The recurring price.</value>
        public string RecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets the zones.
        /// </summary>
        /// <value>The zones.</value>
        public string Zones { get; set; }
    }
}