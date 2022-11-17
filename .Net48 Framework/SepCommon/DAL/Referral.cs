// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Referral.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Referral.
    /// </summary>
    public static class Referral
    {
        /// <summary>
        /// Gets the referral stats.
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <returns>List&lt;Models.ReferralStats&gt;.</returns>
        public static List<ReferralStats> GetReferralStats(string EmailAddress = "")
        {
            var lReferralStats = new List<ReferralStats>();

            var wclause = string.Empty;

            if (!string.IsNullOrWhiteSpace(EmailAddress))
            {
                wclause = " WHERE FromEmailAddress LIKE '" + SepFunctions.FixWord(EmailAddress) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT TOP 10 * FROM ReferralStats" + wclause + " ORDER BY Visitors DESC", conn))
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

                    var dReferralStats = new Models.ReferralStats { StatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                    dReferralStats.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["FromEmailAddress"]);
                    dReferralStats.Visitors = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Visitors"]));
                    lReferralStats.Add(dReferralStats);
                }
            }

            return lReferralStats;
        }
    }
}