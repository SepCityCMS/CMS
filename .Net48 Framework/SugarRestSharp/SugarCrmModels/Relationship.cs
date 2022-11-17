// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Relationship.cs" company="SepCity, Inc.">
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
    /// A class which represents the relationships table.
    /// </summary>
    [ModuleProperty(ModuleName = "Relationships", TableName = "relationships")]
	public partial class Relationship : EntityBase
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty(PropertyName = "id")]
		public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the relationship.
        /// </summary>
        /// <value>The name of the relationship.</value>
        [JsonProperty(PropertyName = "relationship_name")]
		public virtual string RelationshipName { get; set; }

        /// <summary>
        /// Gets or sets the LHS module.
        /// </summary>
        /// <value>The LHS module.</value>
        [JsonProperty(PropertyName = "lhs_module")]
		public virtual string LhsModule { get; set; }

        /// <summary>
        /// Gets or sets the LHS table.
        /// </summary>
        /// <value>The LHS table.</value>
        [JsonProperty(PropertyName = "lhs_table")]
		public virtual string LhsTable { get; set; }

        /// <summary>
        /// Gets or sets the LHS key.
        /// </summary>
        /// <value>The LHS key.</value>
        [JsonProperty(PropertyName = "lhs_key")]
		public virtual string LhsKey { get; set; }

        /// <summary>
        /// Gets or sets the RHS module.
        /// </summary>
        /// <value>The RHS module.</value>
        [JsonProperty(PropertyName = "rhs_module")]
		public virtual string RhsModule { get; set; }

        /// <summary>
        /// Gets or sets the RHS table.
        /// </summary>
        /// <value>The RHS table.</value>
        [JsonProperty(PropertyName = "rhs_table")]
		public virtual string RhsTable { get; set; }

        /// <summary>
        /// Gets or sets the RHS key.
        /// </summary>
        /// <value>The RHS key.</value>
        [JsonProperty(PropertyName = "rhs_key")]
		public virtual string RhsKey { get; set; }

        /// <summary>
        /// Gets or sets the join table.
        /// </summary>
        /// <value>The join table.</value>
        [JsonProperty(PropertyName = "join_table")]
		public virtual string JoinTable { get; set; }

        /// <summary>
        /// Gets or sets the join key LHS.
        /// </summary>
        /// <value>The join key LHS.</value>
        [JsonProperty(PropertyName = "join_key_lhs")]
		public virtual string JoinKeyLhs { get; set; }

        /// <summary>
        /// Gets or sets the join key RHS.
        /// </summary>
        /// <value>The join key RHS.</value>
        [JsonProperty(PropertyName = "join_key_rhs")]
		public virtual string JoinKeyRhs { get; set; }

        /// <summary>
        /// Gets or sets the type of the relationship.
        /// </summary>
        /// <value>The type of the relationship.</value>
        [JsonProperty(PropertyName = "relationship_type")]
		public virtual string RelationshipType { get; set; }

        /// <summary>
        /// Gets or sets the relationship role column.
        /// </summary>
        /// <value>The relationship role column.</value>
        [JsonProperty(PropertyName = "relationship_role_column")]
		public virtual string RelationshipRoleColumn { get; set; }

        /// <summary>
        /// Gets or sets the relationship role column value.
        /// </summary>
        /// <value>The relationship role column value.</value>
        [JsonProperty(PropertyName = "relationship_role_column_value")]
		public virtual string RelationshipRoleColumnValue { get; set; }

        /// <summary>
        /// Gets or sets the reverse.
        /// </summary>
        /// <value>The reverse.</value>
        [JsonProperty(PropertyName = "reverse")]
		public virtual sbyte? Reverse { get; set; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>The deleted.</value>
        [JsonProperty(PropertyName = "deleted")]
		public virtual sbyte? Deleted { get; set; }

	}
}