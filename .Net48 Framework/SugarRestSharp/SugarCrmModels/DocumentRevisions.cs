// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="DocumentRevisions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591

namespace SugarRestSharp.Models
{
	using System;
	using Newtonsoft.Json;


    /// <summary>
    /// A class which represents the document_revisions table.
    /// </summary>
    [ModuleProperty(ModuleName = "DocumentRevisions", TableName = "document_revisions")]
	public partial class DocumentRevisions : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the change log.
        /// </summary>
        /// <value>The change log.</value>
        [JsonProperty(PropertyName = "change_log")]
		public virtual string ChangeLog { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>The document identifier.</value>
        [JsonProperty(PropertyName = "document_id")]
		public virtual string DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>The document identifier.</value>
        [JsonProperty(PropertyName = "doc_id")]
		public virtual string DocId { get; set; }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        [JsonProperty(PropertyName = "doc_type")]
		public virtual string DocType { get; set; }

        /// <summary>
        /// Gets or sets the document URL.
        /// </summary>
        /// <value>The document URL.</value>
        [JsonProperty(PropertyName = "doc_url")]
		public virtual string DocUrl { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        [JsonProperty(PropertyName = "date_entered")]
		public virtual DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>The filename.</value>
        [JsonProperty(PropertyName = "filename")]
		public virtual string Filename { get; set; }

        /// <summary>
        /// Gets or sets the file ext.
        /// </summary>
        /// <value>The file ext.</value>
        [JsonProperty(PropertyName = "file_ext")]
		public virtual string FileExt { get; set; }

        /// <summary>
        /// Gets or sets the type of the file MIME.
        /// </summary>
        /// <value>The type of the file MIME.</value>
        [JsonProperty(PropertyName = "file_mime_type")]
		public virtual string FileMimeType { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>The revision.</value>
        [JsonProperty(PropertyName = "revision")]
		public virtual string Revision { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

	}
}