// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="ChartData.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    /// <summary>
    /// Class ChartData.
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// Gets or sets the name of the day.
        /// </summary>
        /// <value>The name of the day.</value>
        public string DayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the month.
        /// </summary>
        /// <value>The name of the month.</value>
        public string MonthName { get; set; }

        /// <summary>
        /// Gets or sets the number activities.
        /// </summary>
        /// <value>The number activities.</value>
        public double NumActivities { get; set; }

        /// <summary>
        /// Gets or sets the number orders.
        /// </summary>
        /// <value>The number orders.</value>
        public string NumOrders { get; set; }

        /// <summary>
        /// Gets or sets the number signups.
        /// </summary>
        /// <value>The number signups.</value>
        public double NumSignups { get; set; }

        /// <summary>
        /// Gets or sets the number votes.
        /// </summary>
        /// <value>The number votes.</value>
        public long NumVotes { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the poll option.
        /// </summary>
        /// <value>The poll option.</value>
        public string PollOption { get; set; }
    }
}