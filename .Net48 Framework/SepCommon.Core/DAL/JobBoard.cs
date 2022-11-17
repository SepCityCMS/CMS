// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="JobBoard.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Xml;

    /// <summary>
    /// Class JobBoard.
    /// </summary>
    public static class JobBoard
    {
        /// <summary>
        /// Customizes the get setting.
        /// </summary>
        /// <param name="CustomType">Type of the custom.</param>
        /// <returns>System.String.</returns>
        public static string CustomizeGetSetting(string CustomType)
        {
            var sReturn = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ModuleIDs LIKE '%|66|%' AND PortalIDs LIKE '%|66|%' AND ScriptType='" + SepFunctions.FixWord(CustomType) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sReturn = SepFunctions.openNull(RS["ScriptText"]);
                        }
                    }
                }
            }

            return sReturn;
        }

        /// <summary>
        /// Customizes the save.
        /// </summary>
        /// <param name="CustomType">Type of the custom.</param>
        /// <param name="CustomSetting">The custom setting.</param>
        public static void CustomizeSave(string CustomType, string CustomSetting)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ID FROM Scripts WHERE ModuleIDs LIKE '%|66|%' AND PortalIDs LIKE '%|66|%' AND ScriptType='" + SepFunctions.FixWord(CustomType) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            using (var cmd2 = new SqlCommand("UPDATE Scripts SET ScriptText=@ScriptText WHERE ScriptType=@ScriptType AND ModuleIDs LIKE '%|66|%' AND PortalIDs LIKE '%|66|%'", conn))
                            {
                                cmd2.Parameters.AddWithValue("@ScriptText", CustomSetting);
                                cmd2.Parameters.AddWithValue("@ScriptType", CustomType);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (var cmd2 = new SqlCommand("INSERT INTO Scripts (ModuleIDs, PortalIDs, ScriptType, Description, ScriptText, DatePosted) VALUES(@ModuleIDs, @PortalIDs, @ScriptType, @Description, @ScriptText, @DatePosted)", conn))
                            {
                                cmd2.Parameters.AddWithValue("@ModuleIDs", "|66|");
                                cmd2.Parameters.AddWithValue("@PortalIDs", "|66|");
                                cmd2.Parameters.AddWithValue("@ScriptType", CustomType);
                                cmd2.Parameters.AddWithValue("@Description", CustomType);
                                cmd2.Parameters.AddWithValue("@ScriptText", CustomSetting);
                                cmd2.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Settings to array.
        /// </summary>
        /// <param name="recordType">Type of the record.</param>
        /// <param name="CustomType">Type of the custom.</param>
        /// <param name="xAttribute">The x attribute.</param>
        /// <returns>ArrayList.</returns>
        public static ArrayList SettingToArray(PCRecruiter.PCR_RecordType recordType, string CustomType, string xAttribute)
        {
            var inidata = string.Empty;
            var arrData = new ArrayList();
            string[] arrInidata = null;

            var cPCR = new PCRecruiter();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ModuleIDs LIKE '%|66|%' AND PortalIDs LIKE '%|66|%' AND ScriptType='" + SepFunctions.FixWord(CustomType) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            inidata = SepFunctions.openNull(RS["ScriptText"]);
                            inidata = Strings.Right(inidata, inidata.Length - 2);
                            inidata = Strings.Left(inidata, inidata.Length - 2);
                            arrInidata = Strings.Split(inidata, "||||");
                            for (var i = 0; i <= arrInidata.Length - 1; i++)
                            {
                                arrData.Add(arrInidata[i]);
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(inidata))
            {
                XmlNodeList nodes = null;
                nodes = cPCR.GetFieldNodes(recordType, xAttribute, "Y", false);

                foreach (XmlNode node in nodes)
                {
                    arrData.Add(node.Attributes["name"].InnerText);
                }
            }

            cPCR.Dispose();

            return arrData;
        }
    }
}