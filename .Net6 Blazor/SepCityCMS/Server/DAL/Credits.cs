// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Credits.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Credits.
    /// </summary>
    public static class Credits
    {
        /// <summary>
        /// Gets the credit pricing.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Credits&gt;.</returns>
        public static List<Models.Credits> GetCreditPricing(string SortExpression = "Credits", string SortDirection = "ASC", string searchWords = "")
        {
            var lACredits = new List<Models.Credits>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Credits";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ProductName LIKE '%" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (SortExpression == "Credits")
            {
                SortExpression = "ProductName";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ProductID,ProductName,Inventory,UnitPrice FROM ShopProducts WHERE ModuleID='973' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dCredits = new Models.Credits { CreditID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductID"])) };
                    dCredits.CreditsName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ProductName"]);
                    dCredits.NumCredits = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Inventory"]));
                    dCredits.Price = SepFunctions.Format_Currency(SepFunctions.openNull(ds.Tables[0].Rows[i]["UnitPrice"]));
                    lACredits.Add(dCredits);
                }
            }

            return lACredits;
        }
    }
}