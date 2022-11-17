// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Options.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents Options class
    /// </summary>
    public class Options
    {
        /// <summary>
        /// The default max result count
        /// </summary>
        private const int MaxCountResult = 100;

        /// <summary>
        /// Initializes a new instance of the Options class
        /// </summary>
        public Options()
        {
            this.MaxResult = MaxCountResult;
            this.SelectFields = new List<string>();
        }

        /// <summary>
        /// Gets or sets the current page number
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of entities per page
        /// </summary>
        /// <value>The number per page.</value>
        public int NumberPerPage { get; set; }

        /// <summary>
        /// Gets or sets the max result entities to return
        /// </summary>
        /// <value>The maximum result.</value>
        public int MaxResult { get; set; }

        /// <summary>
        /// Gets or sets selected module fields to return
        /// </summary>
        /// <value>The select fields.</value>
        public List<string> SelectFields { get; set; }

        /// <summary>
        /// Gets or sets the linked modules.
        /// The "dictionary key" is the module name (e.g - Accounts, Leads etc) or .NET C# object type (e.g - typeof(Account), typeof(Lead)).
        /// The "dictionary value" is the list of select fields.
        /// The select fields (value) can be null or empty, but the module type or name (key) must be valid.
        /// </summary>
        /// <value>The linked modules.</value>
        public Dictionary<object, List<string>> LinkedModules { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// If this value is set, the QueryPredicates value is ignored.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        /// <value>The query predicates.</value>
        public List<QueryPredicate> QueryPredicates { get; set; }
    }
}