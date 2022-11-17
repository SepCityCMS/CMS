// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Document.cs" company="SepCity, Inc.">
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
    /// A class which represents the documents table.
    /// </summary>
    [ModuleProperty(ModuleName = "Documents", TableName = "documents")]
	public partial class Document : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>The date entered.</value>
        [JsonProperty(PropertyName = "date_entered")]
		public virtual DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [JsonProperty(PropertyName = "date_modified")]
		public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the modified user identifier.
        /// </summary>
        /// <value>The modified user identifier.</value>
        [JsonProperty(PropertyName = "modified_user_id")]
		public virtual string ModifiedUserId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty(PropertyName = "created_by")]
		public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
		public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>The assigned user identifier.</value>
        [JsonProperty(PropertyName = "assigned_user_id")]
		public virtual string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the document.
        /// </summary>
        /// <value>The name of the document.</value>
        [JsonProperty(PropertyName = "document_name")]
		public virtual string DocumentName { get; set; }

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
        /// Gets or sets the active date.
        /// </summary>
        /// <value>The active date.</value>
        [JsonProperty(PropertyName = "active_date")]
		public virtual DateTime? ActiveDate { get; set; }

        /// <summary>
        /// Gets or sets the exp date.
        /// </summary>
        /// <value>The exp date.</value>
        [JsonProperty(PropertyName = "exp_date")]
		public virtual DateTime? ExpDate { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        [JsonProperty(PropertyName = "category_id")]
		public virtual string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the subcategory identifier.
        /// </summary>
        /// <value>The subcategory identifier.</value>
        [JsonProperty(PropertyName = "subcategory_id")]
		public virtual string SubcategoryId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>The status identifier.</value>
        [JsonProperty(PropertyName = "status_id")]
		public virtual string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the document revision identifier.
        /// </summary>
        /// <value>The document revision identifier.</value>
        [JsonProperty(PropertyName = "document_revision_id")]
		public virtual string DocumentRevisionId { get; set; }

        /// <summary>
        /// Gets or sets the related document identifier.
        /// </summary>
        /// <value>The related document identifier.</value>
        [JsonProperty(PropertyName = "related_doc_id")]
		public virtual string RelatedDocId { get; set; }

        /// <summary>
        /// Gets or sets the related document rev identifier.
        /// </summary>
        /// <value>The related document rev identifier.</value>
        [JsonProperty(PropertyName = "related_doc_rev_id")]
		public virtual string RelatedDocRevId { get; set; }

        /// <summary>
        /// Gets or sets the is template.
        /// </summary>
        /// <value>The is template.</value>
        [JsonProperty(PropertyName = "is_template")]
		public virtual sbyte? IsTemplate { get; set; }

        /// <summary>
        /// Gets or sets the type of the template.
        /// </summary>
        /// <value>The type of the template.</value>
        [JsonProperty(PropertyName = "template_type")]
		public virtual string TemplateType { get; set; }

	}
}