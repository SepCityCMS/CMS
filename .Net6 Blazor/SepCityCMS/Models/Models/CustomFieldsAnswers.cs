// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="CustomFieldsAnswers.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class CustomFieldsAnswers.
    /// </summary>
    public class CustomFieldsAnswers
    {
        /// <summary>
        /// Gets or sets the custom data.
        /// </summary>
        /// <value>The custom data.</value>
        public string CustomData { get; set; }

        /// <summary>
        /// Gets or sets the field identifier.
        /// </summary>
        /// <value>The field identifier.</value>
        public long FieldID { get; set; }

        /// <summary>
        /// Gets or sets the field value.
        /// </summary>
        /// <value>The field value.</value>
        public string FieldValue { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public long ProductID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public long UniqueID { get; set; }

        /// <summary>
        /// Gets or sets the user field identifier.
        /// </summary>
        /// <value>The user field identifier.</value>
        public long UserFieldID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }
    }
}