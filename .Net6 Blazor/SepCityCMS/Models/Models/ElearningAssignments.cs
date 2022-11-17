// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ElearningAssignments.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;

    /// <summary>
    /// Class ElearningAssignments.
    /// </summary>
    public class ElearningAssignments
    {
        /// <summary>
        /// Gets or sets the assignment identifier.
        /// </summary>
        /// <value>The assignment identifier.</value>
        public long AssignmentID { get; set; }

        /// <summary>
        /// Gets or sets the course identifier.
        /// </summary>
        /// <value>The course identifier.</value>
        public long CourseID { get; set; }

        /// <summary>
        /// Gets or sets the date submitted.
        /// </summary>
        /// <value>The date submitted.</value>
        public DateTime DateSubmitted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the download link.
        /// </summary>
        /// <value>The download link.</value>
        public string DownloadLink { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>The due date.</value>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the grade.
        /// </summary>
        /// <value>The grade.</value>
        public string Grade { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the presentation link.
        /// </summary>
        /// <value>The presentation link.</value>
        public string PresentationLink { get; set; }

        /// <summary>
        /// Gets or sets the submit identifier.
        /// </summary>
        /// <value>The submit identifier.</value>
        public string SubmitID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}