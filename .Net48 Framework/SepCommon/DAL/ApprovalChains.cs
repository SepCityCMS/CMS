// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ApprovalChains.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class ApprovalChains.
    /// </summary>
    public static class ApprovalChains
    {
        /// <summary>
        /// Chains the delete.
        /// </summary>
        /// <param name="ChainIDs">The chain i ds.</param>
        /// <returns>System.String.</returns>
        public static string Chain_Delete(string ChainIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrChainIDs = Strings.Split(ChainIDs, ",");

                if (arrChainIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrChainIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Approvals SET Status='-1', DateDeleted=@DateDeleted WHERE ApproveID=@ApproveID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ApproveID", arrChainIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ApprovalXML SET Status='-1', DateDeleted=@DateDeleted WHERE ApproveID=@ApproveID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ApproveID", arrChainIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ApprovalEmails SET Status='-1', DateDeleted=@DateDeleted WHERE ApproveID=@ApproveID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ApproveID", arrChainIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Approval Chain(s) has been successfully deleted.");
        }

        /// <summary>
        /// Chains the get.
        /// </summary>
        /// <param name="ChainID">The chain identifier.</param>
        /// <returns>Models.ApprovalChains.</returns>
        public static Models.ApprovalChains Chain_Get(long ChainID)
        {
            var returnXML = new Models.ApprovalChains();

            long recordCount = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Approvals WHERE ApproveID=@ChainID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ChainID", ChainID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ApproveID = SepFunctions.toLong(SepFunctions.openNull(RS["ApproveID"]));
                            returnXML.ChainName = SepFunctions.openNull(RS["ChainName"]);
                            returnXML.ModuleIDs = SepFunctions.openNull(RS["ModuleIDs"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ApprovalEmails WHERE ApproveID=@ChainID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ChainID", ChainID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                recordCount += 1;
                                switch (recordCount)
                                {
                                    case 1:
                                        returnXML.ApprovalName1 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail1 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight1 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 2:
                                        returnXML.ApprovalName2 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail2 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight2 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 3:
                                        returnXML.ApprovalName3 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail3 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight3 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 4:
                                        returnXML.ApprovalName4 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail4 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight4 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 5:
                                        returnXML.ApprovalName5 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail5 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight5 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 6:
                                        returnXML.ApprovalName6 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail6 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight6 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 7:
                                        returnXML.ApprovalName7 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail7 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight7 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 8:
                                        returnXML.ApprovalName8 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail8 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight8 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 9:
                                        returnXML.ApprovalName9 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail9 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight9 = SepFunctions.openNull(RS["Weight"]);
                                        break;

                                    case 10:
                                        returnXML.ApprovalName10 = SepFunctions.openNull(RS["FullName"]);
                                        returnXML.ApprovalEmail10 = SepFunctions.openNull(RS["EmailAddress"]);
                                        returnXML.Weight10 = SepFunctions.openNull(RS["Weight"]);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Chains the save.
        /// </summary>
        /// <param name="ChainID">The chain identifier.</param>
        /// <param name="ChainName">Name of the chain.</param>
        /// <param name="ModuleIDs">The module i ds.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="ApprovalName1">The approval name1.</param>
        /// <param name="ApprovalEmail1">The approval email1.</param>
        /// <param name="Weight1">The weight1.</param>
        /// <param name="ApprovalName2">The approval name2.</param>
        /// <param name="ApprovalEmail2">The approval email2.</param>
        /// <param name="Weight2">The weight2.</param>
        /// <param name="ApprovalName3">The approval name3.</param>
        /// <param name="ApprovalEmail3">The approval email3.</param>
        /// <param name="Weight3">The weight3.</param>
        /// <param name="ApprovalName4">The approval name4.</param>
        /// <param name="ApprovalEmail4">The approval email4.</param>
        /// <param name="Weight4">The weight4.</param>
        /// <param name="ApprovalName5">The approval name5.</param>
        /// <param name="ApprovalEmail5">The approval email5.</param>
        /// <param name="Weight5">The weight5.</param>
        /// <param name="ApprovalName6">The approval name6.</param>
        /// <param name="ApprovalEmail6">The approval email6.</param>
        /// <param name="Weight6">The weight6.</param>
        /// <param name="ApprovalName7">The approval name7.</param>
        /// <param name="ApprovalEmail7">The approval email7.</param>
        /// <param name="Weight7">The weight7.</param>
        /// <param name="ApprovalName8">The approval name8.</param>
        /// <param name="ApprovalEmail8">The approval email8.</param>
        /// <param name="Weight8">The weight8.</param>
        /// <param name="ApprovalName9">The approval name9.</param>
        /// <param name="ApprovalEmail9">The approval email9.</param>
        /// <param name="Weight9">The weight9.</param>
        /// <param name="ApprovalName10">The approval name10.</param>
        /// <param name="ApprovalEmail10">The approval email10.</param>
        /// <param name="Weight10">The weight10.</param>
        /// <returns>System.String.</returns>
        public static string Chain_Save(long ChainID, string ChainName, string ModuleIDs, string PortalIDs, string ApprovalName1, string ApprovalEmail1, string Weight1, string ApprovalName2, string ApprovalEmail2, string Weight2, string ApprovalName3, string ApprovalEmail3, string Weight3, string ApprovalName4, string ApprovalEmail4, string Weight4, string ApprovalName5, string ApprovalEmail5, string Weight5, string ApprovalName6, string ApprovalEmail6, string Weight6, string ApprovalName7, string ApprovalEmail7, string Weight7, string ApprovalName8, string ApprovalEmail8, string Weight8, string ApprovalName9, string ApprovalEmail9, string Weight9, string ApprovalName10, string ApprovalEmail10, string Weight10)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ChainID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ApproveID FROM Approvals WHERE ApproveID=@ChainID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ChainID", ChainID);
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
                    ChainID = SepFunctions.GetIdentity();
                }

                if (string.IsNullOrWhiteSpace(PortalIDs))
                {
                    PortalIDs = "|-1|";
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Approvals SET ChainName=@ChainName, ModuleIDs=@ModuleIDs, PortalIDs=@PortalIDs WHERE ApproveID=@ChainID";
                }
                else
                {
                    SqlStr = "INSERT INTO Approvals (ApproveID, ChainName, ModuleIDs, PortalIDs, Status, DatePosted) VALUES (@ChainID, @ChainName, @ModuleIDs, @PortalIDs, '1', @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ChainID", ChainID);
                    cmd.Parameters.AddWithValue("@ChainName", ChainName);
                    cmd.Parameters.AddWithValue("@ModuleIDs", ModuleIDs);
                    cmd.Parameters.AddWithValue("@PortalIDs", PortalIDs);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    using (var cmd2 = new SqlCommand("DELETE FROM ApprovalEmails WHERE ApproveID=@ChainID", conn))
                    {
                        cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                        cmd2.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName1) && !string.IsNullOrWhiteSpace(ApprovalEmail1))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName1);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail1);
                            cmd2.Parameters.AddWithValue("@Weight", Weight1);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName2) && !string.IsNullOrWhiteSpace(ApprovalEmail2))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName2);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail2);
                            cmd2.Parameters.AddWithValue("@Weight", Weight2);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName3) && !string.IsNullOrWhiteSpace(ApprovalEmail3))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName3);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail3);
                            cmd2.Parameters.AddWithValue("@Weight", Weight3);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName4) && !string.IsNullOrWhiteSpace(ApprovalEmail4))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName4);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail4);
                            cmd2.Parameters.AddWithValue("@Weight", Weight4);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName5) && !string.IsNullOrWhiteSpace(ApprovalEmail5))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName5);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail5);
                            cmd2.Parameters.AddWithValue("@Weight", Weight5);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName6) && !string.IsNullOrWhiteSpace(ApprovalEmail6))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName6);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail6);
                            cmd2.Parameters.AddWithValue("@Weight", Weight6);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName7) && !string.IsNullOrWhiteSpace(ApprovalEmail7))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName7);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail7);
                            cmd2.Parameters.AddWithValue("@Weight", Weight7);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName8) && !string.IsNullOrWhiteSpace(ApprovalEmail8))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName8);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail8);
                            cmd2.Parameters.AddWithValue("@Weight", Weight8);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName9) && !string.IsNullOrWhiteSpace(ApprovalEmail9))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName9);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail9);
                            cmd2.Parameters.AddWithValue("@Weight", Weight9);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ApprovalName10) && !string.IsNullOrWhiteSpace(ApprovalEmail10))
                    {
                        using (var cmd2 = new SqlCommand("INSERT INTO ApprovalEmails (EmailID, ApproveID, FullName, EmailAddress, Weight, Status) VALUES(@EmailID, @ChainID, @FullName, @EmailAddress, @Weight, '1')", conn))
                        {
                            cmd2.Parameters.AddWithValue("@EmailID", SepFunctions.GetIdentity());
                            cmd2.Parameters.AddWithValue("@ChainID", ChainID);
                            cmd2.Parameters.AddWithValue("@FullName", ApprovalName10);
                            cmd2.Parameters.AddWithValue("@EmailAddress", ApprovalEmail10);
                            cmd2.Parameters.AddWithValue("@Weight", Weight10);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }

            string sReturn = SepFunctions.LangText("Approval Chain has been successfully added.");
            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Approval Chain has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Gets the approval chains.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.ApprovalChains&gt;.</returns>
        public static List<Models.ApprovalChains> GetApprovalChains(string SortExpression = "ChainName", string SortDirection = "ASC", string searchWords = "")
        {
            var lApprovalChains = new List<Models.ApprovalChains>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ChainName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND ChainName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ApproveID,ChainName,DatePosted FROM Approvals WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dApprovalChains = new Models.ApprovalChains { ApproveID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ApproveID"])) };
                    dApprovalChains.ChainName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ChainName"]);
                    dApprovalChains.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lApprovalChains.Add(dApprovalChains);
                }
            }

            return lApprovalChains;
        }

        /// <summary>
        /// Gets the approval waiting.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.ApprovalChainsWaiting&gt;.</returns>
        public static List<ApprovalChainsWaiting> GetApprovalWaiting(string SortExpression = "ChainName", string SortDirection = "ASC", string searchWords = "")
        {
            var lApprovalChainsWaiting = new List<ApprovalChainsWaiting>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ChainName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND ChainName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT AX.XMLID,AX.ModuleID,AX.UniqueID,A.ChainName FROM ApprovalXML AS AX,Approvals AS A WHERE A.ApproveID=AX.ApproveID AND AX.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dApprovalChainsWaiting = new Models.ApprovalChainsWaiting { XMLID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["XMLID"])) };
                    dApprovalChainsWaiting.ChainName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ChainName"]);
                    dApprovalChainsWaiting.ModuleName = SepFunctions.GetModuleName(SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"])));
                    dApprovalChainsWaiting.UniqueID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]));
                    lApprovalChainsWaiting.Add(dApprovalChainsWaiting);
                }
            }

            return lApprovalChainsWaiting;
        }

        /// <summary>
        /// Waitings the delete.
        /// </summary>
        /// <param name="XMLIDs">The xmli ds.</param>
        /// <returns>System.String.</returns>
        public static string Waiting_Delete(string XMLIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrXMLIDs = Strings.Split(XMLIDs, ",");

                if (arrXMLIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrXMLIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE ApprovalXML SET Status='-1', DateDeleted=@DateDeleted WHERE XMLID=@XMLID", conn))
                        {
                            cmd.Parameters.AddWithValue("@XMLID", arrXMLIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Waiting Approval Chain(s) has been successfully deleted.");
        }
    }
}