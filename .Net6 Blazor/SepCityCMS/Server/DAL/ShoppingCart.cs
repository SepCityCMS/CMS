// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ShoppingCart.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class ShoppingCart.
    /// </summary>
    public static class ShoppingCart
    {
        /// <summary>
        /// Gets the taxes.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Taxes&gt;.</returns>
        public static List<Taxes> GetTaxes(string SortExpression = "State", string SortDirection = "ASC", string searchWords = "")
        {
            var lTaxes = new List<Taxes>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "State";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (TaxName LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR State LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ID,TaxName,TaxPercent,State FROM TaxCalculator WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dTaxes = new Models.Taxes { TaxID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                    dTaxes.TaxName = SepFunctions.openNull(ds.Tables[0].Rows[i]["TaxName"]);
                    dTaxes.TaxPercent = SepFunctions.toPercent(SepFunctions.openNull(ds.Tables[0].Rows[i]["TaxPercent"]));
                    dTaxes.State = SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    lTaxes.Add(dTaxes);
                }
            }

            return lTaxes;
        }

        /// <summary>
        /// Taxes the delete.
        /// </summary>
        /// <param name="TaxIDs">The tax i ds.</param>
        /// <returns>System.String.</returns>
        public static string Tax_Delete(string TaxIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrTaxIDs = Strings.Split(TaxIDs, ",");

                if (arrTaxIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTaxIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE TaxCalculator SET Status='-1', DateDeleted=@DateDeleted WHERE ID=@TaxID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TaxID", arrTaxIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Tax(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Taxes the get.
        /// </summary>
        /// <param name="TaxID">The tax identifier.</param>
        /// <returns>Models.Taxes.</returns>
        public static Taxes Tax_Get(long TaxID)
        {
            var returnXML = new Models.Taxes();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM TaxCalculator WHERE ID=@TaxID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TaxID", TaxID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TaxID = TaxID;
                            returnXML.TaxName = SepFunctions.openNull(RS["TaxName"]);
                            returnXML.TaxPercent = SepFunctions.openNull(RS["TaxPercent"]);
                            returnXML.State = SepFunctions.openNull(RS["State"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Taxes the state of the get by.
        /// </summary>
        /// <param name="StateName">Name of the state.</param>
        /// <returns>Models.Taxes.</returns>
        public static Taxes Tax_Get_By_State(string StateName)
        {
            var returnXML = new Models.Taxes();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM TaxCalculator WHERE State=@State AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@State", StateName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TaxID = SepFunctions.toLong(SepFunctions.openNull(RS["ID"]));
                            returnXML.TaxName = SepFunctions.openNull(RS["TaxName"]);
                            returnXML.TaxPercent = SepFunctions.openNull(RS["TaxPercent"]);
                            returnXML.State = SepFunctions.openNull(RS["State"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Taxes the save.
        /// </summary>
        /// <param name="TaxID">The tax identifier.</param>
        /// <param name="TaxName">Name of the tax.</param>
        /// <param name="TaxPercent">The tax percent.</param>
        /// <param name="State">The state.</param>
        /// <returns>System.String.</returns>
        public static string Tax_Save(long TaxID, string TaxName, string TaxPercent, string State)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (TaxID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ID FROM TaxCalculator WHERE ID=@TaxID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TaxID", TaxID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    TaxID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE TaxCalculator SET TaxName=@TaxName, TaxPercent=@TaxPercent, State=@State WHERE ID=@TaxID";
                }
                else
                {
                    SqlStr = "INSERT INTO TaxCalculator (TaxName, TaxPercent, State, Status) VALUES (@TaxName, @TaxPercent, @State, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@TaxID", TaxID);
                    cmd.Parameters.AddWithValue("@TaxName", TaxName);
                    cmd.Parameters.AddWithValue("@TaxPercent", TaxPercent);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Tax has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Tax has been successfully added.");

            return sReturn;
        }
    }
}