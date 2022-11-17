// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="CustomFieldsOptions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class CustomFieldsOptions.
    /// </summary>
    public class CustomFieldsOptions
    {
        /// <summary>
        /// Gets or sets the field identifier.
        /// </summary>
        /// <value>The field identifier.</value>
        public long FieldID { get; set; }

        /// <summary>
        /// Gets or sets the option identifier.
        /// </summary>
        /// <value>The option identifier.</value>
        public long OptionID { get; set; }

        /// <summary>
        /// Gets or sets the name of the option.
        /// </summary>
        /// <value>The name of the option.</value>
        public string OptionName { get; set; }

        /// <summary>
        /// Gets or sets the option value.
        /// </summary>
        /// <value>The option value.</value>
        public string OptionValue { get; set; }

        /// <summary>
        /// Gets or sets the recurring price.
        /// </summary>
        /// <value>The recurring price.</value>
        public string RecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets the setup price.
        /// </summary>
        /// <value>The setup price.</value>
        public string SetupPrice { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public long Weight { get; set; }
    }
}