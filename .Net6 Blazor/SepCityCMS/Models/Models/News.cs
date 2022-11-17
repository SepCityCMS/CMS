// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="News.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class News.
    /// </summary>
    public class News
    {
        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the display type.
        /// </summary>
        /// <value>The display type.</value>
        public string DisplayType { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime End_Date { get; set; }

        /// <summary>
        /// Gets or sets the expire date.
        /// </summary>
        /// <value>The expire date.</value>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Gets or sets the headline.
        /// </summary>
        /// <value>The headline.</value>
        public string Headline { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the news identifier.
        /// </summary>
        /// <value>The news identifier.</value>
        public long NewsID { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime Start_Date { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>The topic.</value>
        public string Topic { get; set; }
    }
}