// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="PortalsPrices.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    /// <summary>
    /// Class PortalsPrices.
    /// </summary>
    public class PortalsPrices
    {
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
        /// Gets or sets the module i ds.
        /// </summary>
        /// <value>The module i ds.</value>
        public string ModuleIDs { get; set; }

        /// <summary>
        /// Gets or sets the onetime price.
        /// </summary>
        /// <value>The onetime price.</value>
        public string OnetimePrice { get; set; }

        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>The plan identifier.</value>
        public long PlanID { get; set; }

        /// <summary>
        /// Gets or sets the name of the plan.
        /// </summary>
        /// <value>The name of the plan.</value>
        public string PlanName { get; set; }

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
    }
}