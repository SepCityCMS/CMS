// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="FormsSubmissions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class FormsSubmissions.
    /// </summary>
    public class FormsSubmissions
    {
        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>The answer.</value>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the answer identifier.
        /// </summary>
        /// <value>The answer identifier.</value>
        public long AnswerID { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        /// <value>The form identifier.</value>
        public long FormID { get; set; }

        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        /// <value>The name of the form.</value>
        public string FormName { get; set; }

        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>The question identifier.</value>
        public long QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>The submission identifier.</value>
        public long SubmissionID { get; set; }

        /// <summary>
        /// Gets or sets the submit date.
        /// </summary>
        /// <value>The submit date.</value>
        public DateTime SubmitDate { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}