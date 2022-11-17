﻿// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="QueryBuilder.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp.RestApiCalls.Helpers
{
    using Requests;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This class represents QueryBuilder class.
    /// SugarCRM request query option builder.
    /// </summary>
    internal static class QueryBuilder
    {
        /// <summary>
        /// Build the where clause part of a SugarCRM query.
        /// </summary>
        /// <param name="predicates">The JSON predicates.</param>
        /// <returns>The formatted query.</returns>
        public static string GetWhereClause(List<JSONPredicate> predicates)
        {
            if (predicates == null || predicates.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder queryBuilder = new StringBuilder();
            string subQuery = string.Empty;

            foreach (var predicate in predicates)
            {
                switch (predicate.Operator)
                {
                    case QueryOperator.Equal:
                        {
                            subQuery = predicate.IsNumeric ? string.Format("{0} = {1}", predicate.PropertyName, predicate.Value) : string.Format("{0} = '{1}'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.GreaterThan:
                        {
                            subQuery = predicate.IsNumeric ? string.Format("{0} > {1}", predicate.PropertyName, predicate.Value) : string.Format("{0} > '{1}'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.GreaterThanOrEqualTo:
                        {
                            subQuery = predicate.IsNumeric ? string.Format("{0} >= {1}", predicate.PropertyName, predicate.Value) : string.Format("{0} >= '{1}'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.LessThan:
                        {
                            subQuery = predicate.IsNumeric ? string.Format("{0} < {1}", predicate.PropertyName, predicate.Value) : string.Format("{0} < '{1}'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.LessThanOrEqualTo:
                        {
                            subQuery = predicate.IsNumeric ? string.Format("{0} <= {1}", predicate.PropertyName, predicate.Value) : string.Format("{0} <= '{1}'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.Contains:
                        {
                            subQuery = string.Format("{0} LIKE '%{1}%'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.StartsWith:
                        {
                            subQuery = string.Format("{0} LIKE '{1}%'", predicate.PropertyName, predicate.Value);
                            break;
                        }
                    case QueryOperator.EndsWith:
                        {
                            subQuery = string.Format("{0} LIKE '%{1}'", predicate.PropertyName, predicate.Value);
                            break;
                        }

                    case QueryOperator.Between:
                        {
                            subQuery = predicate.IsNumeric ? string.Format("{0} BETWEEN {1} AND {2}", predicate.PropertyName, predicate.FromValue, predicate.ToValue) : string.Format("{0} BETWEEN '{1}' AND '{2}'", predicate.PropertyName, predicate.FromValue, predicate.ToValue);
                            break;
                        }

                    case QueryOperator.WhereIn:
                        {
                            subQuery = string.Format("{0} IN ({1})", predicate.PropertyName, predicate.Value);
                            break;
                        }
                }

                queryBuilder.Append(subQuery);
                queryBuilder.Append(" AND ");
            }

            string query = queryBuilder.ToString();
            if (string.IsNullOrWhiteSpace(query))
            {
                return string.Empty;
            }

            query = " " + query.Trim().TrimEnd('D').TrimEnd('N').TrimEnd('A').Trim() + " ";
            return query;
        }
    }
}