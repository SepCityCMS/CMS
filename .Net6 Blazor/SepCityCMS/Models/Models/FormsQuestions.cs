// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="FormsQuestions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class FormsQuestions.
    /// </summary>
    public class FormsQuestions
    {
        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        /// <value>The form identifier.</value>
        public long FormID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FormsQuestions" /> is mandatory.
        /// </summary>
        /// <value><c>true</c> if mandatory; otherwise, <c>false</c>.</value>
        public bool Mandatory { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>The question identifier.</value>
        public long QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the row number.
        /// </summary>
        /// <value>The row number.</value>
        public long RowNumber { get; set; }

        /// <summary>
        /// Gets or sets the section identifier.
        /// </summary>
        /// <value>The section identifier.</value>
        public long SectionID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public string TypeID { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public long Weight { get; set; }
    }
}