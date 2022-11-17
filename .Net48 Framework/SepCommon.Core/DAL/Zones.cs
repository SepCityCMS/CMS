// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Zones.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Zones.
    /// </summary>
    public static class Zones
    {
        /// <summary>
        /// Gets the zones.
        /// </summary>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Zones&gt;.</returns>
        public static List<Models.Zones> GetZones(int ModuleID = 0, string SortExpression = "ZoneName", string SortDirection = "ASC", string searchWords = "")
        {
            var lZones = new List<Models.Zones>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ZoneName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ZoneName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (ModuleID > 0)
            {
                wClause += " AND (ModuleID='" + SepFunctions.FixWord(Strings.ToString(ModuleID)) + "')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ZoneID,ZoneName FROM TargetZones WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dZones = new Models.Zones { ZoneID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ZoneID"])) };
                    dZones.ZoneName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ZoneName"]);
                    lZones.Add(dZones);
                }
            }

            return lZones;
        }

        /// <summary>
        /// Zones the delete.
        /// </summary>
        /// <param name="ZoneIDs">The zone i ds.</param>
        /// <returns>System.String.</returns>
        public static string Zone_Delete(string ZoneIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrZoneIDs = Strings.Split(ZoneIDs, ",");

                if (arrZoneIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrZoneIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE TargetZones SET Status='-1', DateDeleted=@DateDeleted WHERE ZoneID=@ZoneID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ZoneID", arrZoneIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Advertisements SET Status='-1', DateDeleted=@DateDeleted WHERE ZoneID=@ZoneID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ZoneID", arrZoneIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ContentRotator SET Status='-1', DateDeleted=@DateDeleted WHERE ZoneID=@ZoneID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ZoneID", arrZoneIDs[i]);
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

            sReturn = SepFunctions.LangText("Zone(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Zones the get.
        /// </summary>
        /// <param name="ZoneID">The zone identifier.</param>
        /// <returns>Models.Zones.</returns>
        public static Models.Zones Zone_Get(long ZoneID)
        {
            var returnXML = new Models.Zones();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM TargetZones WHERE ZoneID=@ZoneID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ZoneID = SepFunctions.toLong(SepFunctions.openNull(RS["ZoneID"]));
                            returnXML.ModuleID = SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]));
                            returnXML.ZoneName = SepFunctions.openNull(RS["ZoneName"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Zones the save.
        /// </summary>
        /// <param name="ZoneID">The zone identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="ZoneName">Name of the zone.</param>
        /// <returns>System.String.</returns>
        public static string Zone_Save(long ZoneID, int ModuleID, string ZoneName)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ZoneID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ZoneID FROM TargetZones WHERE ZoneID=@ZoneID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
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
                    ZoneID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE TargetZones SET ZoneName=@ZoneName WHERE ZoneID=@ZoneID";
                }
                else
                {
                    SqlStr = "INSERT INTO TargetZones (ZoneID, ModuleID, ZoneName, Status) VALUES (@ZoneID, @ModuleID, @ZoneName, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    cmd.Parameters.AddWithValue("@ZoneName", ZoneName);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Zone has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Zone has been successfully added.");

            return sReturn;
        }
    }
}