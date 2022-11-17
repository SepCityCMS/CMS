// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="FormsQuestionsOptions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class FormsQuestionsOptions.
    /// </summary>
    public class FormsQuestionsOptions
    {
        /// <summary>
        /// Gets or sets the option identifier.
        /// </summary>
        /// <value>The option identifier.</value>
        public long OptionID { get; set; }

        /// <summary>
        /// Gets or sets the option value.
        /// </summary>
        /// <value>The option value.</value>
        public string OptionValue { get; set; }

        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>The question identifier.</value>
        public long QuestionID { get; set; }
    }
}