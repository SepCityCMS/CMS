// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="CustomFields.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class CustomFields.
    /// </summary>
    public class CustomFields
    {
        /// <summary>
        /// Gets or sets the type of the answer.
        /// </summary>
        /// <value>The type of the answer.</value>
        public string AnswerType { get; set; }

        /// <summary>
        /// Gets or sets the field identifier.
        /// </summary>
        /// <value>The field identifier.</value>
        public long FieldID { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>The type of the field.</value>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the list under.
        /// </summary>
        /// <value>The list under.</value>
        public string ListUnder { get; set; }

        /// <summary>
        /// Gets or sets the module i ds.
        /// </summary>
        /// <value>The module i ds.</value>
        public string ModuleIDs { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the portal i ds.
        /// </summary>
        /// <value>The portal i ds.</value>
        public string PortalIDs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomFields" /> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomFields" /> is searchable.
        /// </summary>
        /// <value><c>true</c> if searchable; otherwise, <c>false</c>.</value>
        public bool Searchable { get; set; }

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
        /// Gets or sets the unique i ds.
        /// </summary>
        /// <value>The unique i ds.</value>
        public string UniqueIDs { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public long Weight { get; set; }
    }
}