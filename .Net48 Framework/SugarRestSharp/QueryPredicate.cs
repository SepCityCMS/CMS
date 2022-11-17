// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="QueryPredicate.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    /// <summary>
    /// Represents QueryPredicate class.
    /// </summary>
    public class QueryPredicate
    {
        /// <summary>
        /// Initializes a new instance of the QueryPredicate class.
        /// </summary>
        /// <param name="propertyName">The property name. This can be a C# model property name or JSON property name.</param>
        /// <param name="queryOperator">The query operator.</param>
        /// <param name="value">The predicate value.</param>
        /// <param name="fromValue">The predicate from value.</param>
        /// <param name="toValue">The predicate to value.</param>
        public QueryPredicate(string propertyName, QueryOperator queryOperator, object value, object fromValue = null, object toValue = null)
        {
            this.PropertyName = propertyName;
            this.Operator = queryOperator;
            this.Value = value;
            this.FromValue = fromValue;
            this.ToValue = toValue;
        }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        public QueryOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the from value.
        /// </summary>
        /// <value>From value.</value>
        public object FromValue { get; set; }

        /// <summary>
        /// Gets or sets the to value.
        /// </summary>
        /// <value>To value.</value>
        public object ToValue { get; set; }
    }
}