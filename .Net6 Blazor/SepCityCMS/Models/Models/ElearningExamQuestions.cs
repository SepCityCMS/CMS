// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ElearningExamQuestions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class ElearningExamQuestions.
    /// </summary>
    public class ElearningExamQuestions
    {
        /// <summary>
        /// Gets or sets the answer1.
        /// </summary>
        /// <value>The answer1.</value>
        public string Answer1 { get; set; }

        /// <summary>
        /// Gets or sets the answer2.
        /// </summary>
        /// <value>The answer2.</value>
        public string Answer2 { get; set; }

        /// <summary>
        /// Gets or sets the answer3.
        /// </summary>
        /// <value>The answer3.</value>
        public string Answer3 { get; set; }

        /// <summary>
        /// Gets or sets the answer4.
        /// </summary>
        /// <value>The answer4.</value>
        public string Answer4 { get; set; }

        /// <summary>
        /// Gets or sets the answer5.
        /// </summary>
        /// <value>The answer5.</value>
        public string Answer5 { get; set; }

        /// <summary>
        /// Gets or sets the exam identifier.
        /// </summary>
        /// <value>The exam identifier.</value>
        public long ExamID { get; set; }

        /// <summary>
        /// Gets or sets the question footer.
        /// </summary>
        /// <value>The question footer.</value>
        public string QuestionFooter { get; set; }

        /// <summary>
        /// Gets or sets the question header.
        /// </summary>
        /// <value>The question header.</value>
        public string QuestionHeader { get; set; }

        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>The question identifier.</value>
        public long QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the question no.
        /// </summary>
        /// <value>The question no.</value>
        public long QuestionNo { get; set; }

        /// <summary>
        /// Gets or sets the type of the question.
        /// </summary>
        /// <value>The type of the question.</value>
        public string QuestionType { get; set; }

        /// <summary>
        /// Gets or sets the right answer.
        /// </summary>
        /// <value>The right answer.</value>
        public string RightAnswer { get; set; }
    }
}