// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Security.cs" company="SepCity, Inc.">
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
    /// Class Security.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Classes the delete.
        /// </summary>
        /// <param name="ClassIDs">The class i ds.</param>
        /// <returns>System.String.</returns>
        public static string Class_Delete(string ClassIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrClassIDs = Strings.Split(ClassIDs, ",");

                if (arrClassIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrClassIDs); i++)
                    {
                        if (SepFunctions.toLong(arrClassIDs[i]) != 1 && SepFunctions.toLong(arrClassIDs[i]) != 2)
                        {
                            using (var cmd = new SqlCommand("UPDATE AccessClasses SET Status='-1', DateDeleted=@DateDeleted WHERE ClassID=@ClassID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ClassID", arrClassIDs[i]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                            {
                                cmd.Parameters.AddWithValue("@ModelNumber", "1-" + arrClassIDs[i]);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                            {
                                cmd.Parameters.AddWithValue("@ModelNumber", "2-" + arrClassIDs[i]);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                            {
                                cmd.Parameters.AddWithValue("@ModelNumber", "3-" + arrClassIDs[i]);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                            {
                                cmd.Parameters.AddWithValue("@ModelNumber", "4-" + arrClassIDs[i]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            return SepFunctions.LangText("Access Class(es) has been successfully deleted.");
        }

        /// <summary>
        /// Classes the get.
        /// </summary>
        /// <param name="ClassID">The class identifier.</param>
        /// <returns>Models.AccessClasses.</returns>
        public static AccessClasses Class_Get(long ClassID)
        {
            var returnXML = new Models.AccessClasses();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM AccessClasses WHERE ClassID=@ClassID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ClassID", ClassID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ClassID = SepFunctions.toLong(SepFunctions.openNull(RS["ClassID"]));
                            returnXML.ClassName = SepFunctions.openNull(RS["ClassName"]);
                            returnXML.KeyIDs = SepFunctions.openNull(RS["KeyIDs"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.PrivateClass = SepFunctions.toBoolean(SepFunctions.openNull(RS["PrivateClass"]));
                            returnXML.LoggedDays = SepFunctions.openNull(RS["LoggedDays"]);
                            returnXML.LoggedSwitchTo = SepFunctions.openNull(RS["LoggedSwitchTo"]);
                            returnXML.InDays = SepFunctions.openNull(RS["InDays"]);
                            returnXML.InSwitchTo = SepFunctions.openNull(RS["InSwitchTo"]);
                            returnXML.SortGroup = SepFunctions.openNull(RS["SortGroup"]);
                            returnXML.PortalIDs = SepFunctions.openNull(RS["PortalIDs"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }

                if (returnXML.ClassID == 0)
                {
                    using (var cmd = new SqlCommand("SELECT UnitPrice,RecurringPrice,RecurringCycle FROM ShopProducts WHERE ModelNumber=@ClassID AND ModuleID='38' AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", "1-" + ClassID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.UnitPrice1 = SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]));
                                returnXML.RecurringPrice1 = SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"]));
                                returnXML.RecurringCycle1 = SepFunctions.openNull(RS["RecurringCycle"]);
                            }
                        }
                    }

                    using (var cmd = new SqlCommand("SELECT UnitPrice,RecurringPrice,RecurringCycle FROM ShopProducts WHERE ModelNumber=@ClassID AND ModuleID='38' AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", "2-" + ClassID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.UnitPrice2 = SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]));
                                returnXML.RecurringPrice2 = SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"]));
                                returnXML.RecurringCycle2 = SepFunctions.openNull(RS["RecurringCycle"]);
                            }
                        }
                    }

                    using (var cmd = new SqlCommand("SELECT UnitPrice,RecurringPrice,RecurringCycle FROM ShopProducts WHERE ModelNumber=@ClassID AND ModuleID='38' AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", "3-" + ClassID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.UnitPrice3 = SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]));
                                returnXML.RecurringPrice3 = SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"]));
                                returnXML.RecurringCycle3 = SepFunctions.openNull(RS["RecurringCycle"]);
                            }
                        }
                    }

                    using (var cmd = new SqlCommand("SELECT UnitPrice,RecurringPrice,RecurringCycle FROM ShopProducts WHERE ModelNumber=@ClassID AND ModuleID='38' AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", "4-" + ClassID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.UnitPrice4 = SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]));
                                returnXML.RecurringPrice4 = SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"]));
                                returnXML.RecurringCycle4 = SepFunctions.openNull(RS["RecurringCycle"]);
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Classes the save.
        /// </summary>
        /// <param name="ClassID">The class identifier.</param>
        /// <param name="ClassName">Name of the class.</param>
        /// <param name="KeyIDs">The key i ds.</param>
        /// <param name="Description">The description.</param>
        /// <param name="UnitPrice1">The unit price1.</param>
        /// <param name="RecurringPrice1">The recurring price1.</param>
        /// <param name="RecurringCycle1">The recurring cycle1.</param>
        /// <param name="UnitPrice2">The unit price2.</param>
        /// <param name="RecurringPrice2">The recurring price2.</param>
        /// <param name="RecurringCycle2">The recurring cycle2.</param>
        /// <param name="UnitPrice3">The unit price3.</param>
        /// <param name="RecurringPrice3">The recurring price3.</param>
        /// <param name="RecurringCycle3">The recurring cycle3.</param>
        /// <param name="UnitPrice4">The unit price4.</param>
        /// <param name="RecurringPrice4">The recurring price4.</param>
        /// <param name="RecurringCycle4">The recurring cycle4.</param>
        /// <param name="PrivateClass">The private class.</param>
        /// <param name="LoggedDays">The logged days.</param>
        /// <param name="LoggedSwitchTo">The logged switch to.</param>
        /// <param name="InDays">The in days.</param>
        /// <param name="InSwitchTo">The in switch to.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <returns>System.String.</returns>
        public static string Class_Save(long ClassID, string ClassName, string KeyIDs, string Description, decimal UnitPrice1, decimal RecurringPrice1, string RecurringCycle1, decimal UnitPrice2, decimal RecurringPrice2, string RecurringCycle2, decimal UnitPrice3, decimal RecurringPrice3, string RecurringCycle3, decimal UnitPrice4, decimal RecurringPrice4, string RecurringCycle4, string PrivateClass, string LoggedDays, string LoggedSwitchTo, string InDays, string InSwitchTo, string PortalIDs)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ClassID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ClassID FROM AccessClasses WHERE ClassID=@ClassID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", ClassID);
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
                    ClassID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE AccessClasses SET ClassName=@ClassName, KeyIDs=@KeyIDs, Description=@Description, PrivateClass=@PrivateClass, LoggedDays=@LoggedDays, LoggedSwitchTo=@LoggedSwitchTo, InDays=@InDays, InSwitchTo=@InSwitchTo, PortalIDs=@PortalIDs WHERE ClassID=@ClassID";
                }
                else
                {
                    SqlStr = "INSERT INTO AccessClasses (ClassID, ClassName, KeyIDs, Description, PrivateClass, LoggedDays, LoggedSwitchTo, InDays, InSwitchTo, PortalIDs, Status) VALUES (@ClassID, @ClassName, @KeyIDs, @Description, @PrivateClass, @LoggedDays, @LoggedSwitchTo, @InDays, @InSwitchTo, @PortalIDs, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ClassID", ClassID);
                    cmd.Parameters.AddWithValue("@ClassName", ClassName);
                    cmd.Parameters.AddWithValue("@KeyIDs", KeyIDs);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@PrivateClass", PrivateClass);
                    cmd.Parameters.AddWithValue("@LoggedDays", LoggedDays);
                    cmd.Parameters.AddWithValue("@LoggedSwitchTo", LoggedSwitchTo);
                    cmd.Parameters.AddWithValue("@InDays", InDays);
                    cmd.Parameters.AddWithValue("@InSwitchTo", InSwitchTo);
                    cmd.Parameters.AddWithValue("@PortalIDs", PortalIDs);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModelNumber", "1-" + ClassID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModelNumber", "2-" + ClassID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModelNumber", "3-" + ClassID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber AND ModuleID='38'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModelNumber", "4-" + ClassID);
                    cmd.ExecuteNonQuery();
                }
            }

            if (SepFunctions.Format_Currency(UnitPrice1) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(RecurringPrice1) != SepFunctions.Format_Currency(0))
            {
                ShoppingMall.Product_Save(SepFunctions.GetIdentity(), 0, string.Empty, 0, "Class Name - " + ClassName, string.Empty, string.Empty, 0, UnitPrice1, RecurringPrice1, RecurringCycle1, 0, string.Empty, string.Empty, 0, string.Empty, 0, 0, 0, false, 0, 0, 0, string.Empty, false, 0, string.Empty, "1-" + ClassID, string.Empty, 0, 0, false, 38, string.Empty, string.Empty, string.Empty, string.Empty, 1, 0, 0);
            }

            if (SepFunctions.Format_Currency(UnitPrice2) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(RecurringPrice2) != SepFunctions.Format_Currency(0))
            {
                ShoppingMall.Product_Save(SepFunctions.GetIdentity(), 0, string.Empty, 0, "Class Name - " + ClassName, string.Empty, string.Empty, 0, UnitPrice2, RecurringPrice2, RecurringCycle2, 0, string.Empty, string.Empty, 0, string.Empty, 0, 0, 0, false, 0, 0, 0, string.Empty, false, 0, string.Empty, "1-" + ClassID, string.Empty, 0, 0, false, 38, string.Empty, string.Empty, string.Empty, string.Empty, 1, 0, 0);
            }

            if (SepFunctions.Format_Currency(UnitPrice3) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(RecurringPrice3) != SepFunctions.Format_Currency(0))
            {
                ShoppingMall.Product_Save(SepFunctions.GetIdentity(), 0, string.Empty, 0, "Class Name - " + ClassName, string.Empty, string.Empty, 0, UnitPrice3, RecurringPrice3, RecurringCycle3, 0, string.Empty, string.Empty, 0, string.Empty, 0, 0, 0, false, 0, 0, 0, string.Empty, false, 0, string.Empty, "1-" + ClassID, string.Empty, 0, 0, false, 38, string.Empty, string.Empty, string.Empty, string.Empty, 1, 0, 0);
            }

            if (SepFunctions.Format_Currency(UnitPrice4) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(RecurringPrice4) != SepFunctions.Format_Currency(0))
            {
                ShoppingMall.Product_Save(SepFunctions.GetIdentity(), 0, string.Empty, 0, "Class Name - " + ClassName, string.Empty, string.Empty, 0, UnitPrice4, RecurringPrice4, RecurringCycle4, 0, string.Empty, string.Empty, 0, string.Empty, 0, 0, 0, false, 0, 0, 0, string.Empty, false, 0, string.Empty, "1-" + ClassID, string.Empty, 0, 0, false, 38, string.Empty, string.Empty, string.Empty, string.Empty, 1, 0, 0);
            }

            string sReturn = SepFunctions.LangText("Access Class has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Access Class has been successfully updated.");
            }

            return sReturn;
        }

        /// <summary>
        /// Classes the switch users.
        /// </summary>
        /// <param name="FromClassID">From class identifier.</param>
        /// <param name="ToClassID">Converts to classid.</param>
        /// <returns>System.String.</returns>
        public static string Class_Switch_Users(long FromClassID, long ToClassID)
        {
            var returnXML = string.Empty;
            var sKeyIDs = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT KeyIDs FROM AccessClasses WHERE ClassID=@ToClassID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ToClassID", ToClassID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sKeyIDs = SepFunctions.openNull(RS["KeyIDs"]);
                        }
                        else
                        {
                            returnXML = SepFunctions.LangText("~~Access Class~~ does not exist.");
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(returnXML))
                {
                    using (var cmd = new SqlCommand("UPDATE Members SET AccessClass=@ToClassID, AccessKeys=@AccessKeys WHERE AccessClass=@FromClassID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@FromClassID", FromClassID);
                        cmd.Parameters.AddWithValue("@ToClassID", ToClassID);
                        cmd.Parameters.AddWithValue("@AccessKeys", sKeyIDs);
                        cmd.ExecuteNonQuery();
                        returnXML = SepFunctions.LangText("Users have successfully been moved to another access class.");
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Gets the access classes.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.AccessClasses&gt;.</returns>
        public static List<AccessClasses> GetAccessClasses(string SortExpression = "ClassName", string SortDirection = "ASC", string searchWords = "")
        {
            var lAccessClasses = new List<AccessClasses>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ClassName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ClassName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ClassID,ClassName,PrivateClass FROM AccessClasses WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dAccessClasses = new Models.AccessClasses { ClassID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ClassID"])) };
                    dAccessClasses.ClassName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ClassName"]);
                    dAccessClasses.PrivateClass = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["PrivateClass"]));
                    lAccessClasses.Add(dAccessClasses);
                }
            }

            return lAccessClasses;
        }

        /// <summary>
        /// Gets the access keys.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.AccessKeys&gt;.</returns>
        public static List<AccessKeys> GetAccessKeys(string SortExpression = "KeyName", string SortDirection = "ASC", string searchWords = "")
        {
            var lAccessKeys = new List<AccessKeys>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "KeyName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (KeyName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT KeyID,KeyName FROM AccessKeys WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dAccessKeys = new Models.AccessKeys { KeyID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["KeyID"])) };
                    dAccessKeys.KeyName = SepFunctions.openNull(ds.Tables[0].Rows[i]["KeyName"]);
                    lAccessKeys.Add(dAccessKeys);
                }
            }

            return lAccessKeys;
        }

        /// <summary>
        /// Gets the memberships.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.AccessClasses&gt;.</returns>
        public static List<AccessClasses> GetMemberships(string SortExpression = "ClassName", string SortDirection = "ASC", string searchWords = "")
        {
            var lAccessClasses = new List<AccessClasses>();

            var wClause = string.Empty;

            long acount = 0;
            var tmpClassID = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ClassName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ProductName LIKE '%" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (SortExpression == "ClassName")
            {
                SortExpression = "SP.ProductName";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT SP.ModelNumber FROM ShopProducts AS SP WHERE SP.ModuleID='38' AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    string[] arrClassID = Strings.Split(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModelNumber"]), "-");
                    Array.Resize(ref arrClassID, 2);
                    if (tmpClassID != arrClassID[1])
                    {
                        using (var ds2 = new DataSet())
                        {
                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                using (var cmd = new SqlCommand("SELECT ClassID,ClassName,Description FROM AccessClasses WHERE ClassID='" + SepFunctions.FixWord(arrClassID[1]) + "' AND PrivateClass='0' AND (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0) AND Status <> -1", conn))
                                {
                                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection.Open();
                                    using (var da = new SqlDataAdapter(cmd))
                                    {
                                        da.Fill(ds2);
                                    }
                                }
                            }

                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                var dAccessClasses = new Models.AccessClasses { ClassID = SepFunctions.toLong(SepFunctions.openNull(ds2.Tables[0].Rows[0]["ClassID"])) };
                                dAccessClasses.ClassName = SepFunctions.openNull(ds2.Tables[0].Rows[0]["ClassName"]);
                                dAccessClasses.Description = SepFunctions.openNull(ds2.Tables[0].Rows[0]["Description"]);

                                using (var ds3 = new DataSet())
                                {
                                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                                    {
                                        using (var cmd = new SqlCommand("SELECT ProductID,ProductName,UnitPrice,RecurringPrice,RecurringCycle,Description FROM ShopProducts WHERE ModuleID='38' AND ModelNumber LIKE '%-" + SepFunctions.FixWord(arrClassID[1]) + "' ORDER BY ProductName", conn))
                                        {
                                            cmd.CommandType = CommandType.Text;
                                            cmd.Connection.Open();
                                            using (var da = new SqlDataAdapter(cmd))
                                            {
                                                da.Fill(ds3);
                                            }
                                        }
                                    }

                                    acount = 0;
                                    for (var j = 0; j <= ds3.Tables[0].Rows.Count - 1; j++)
                                    {
                                        if (ds3.Tables[0].Rows.Count == j)
                                        {
                                            break;
                                        }

                                        acount += 1;
                                        switch (acount)
                                        {
                                            case 1:
                                                dAccessClasses.ProductID1 = SepFunctions.toLong(SepFunctions.openNull(ds3.Tables[0].Rows[j]["ProductID"]));
                                                dAccessClasses.UnitPrice1 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["UnitPrice"]));
                                                dAccessClasses.RecurringCycle1 = SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringCycle"]);
                                                dAccessClasses.RecurringPrice1 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringPrice"]));
                                                break;

                                            case 2:
                                                dAccessClasses.ProductID2 = SepFunctions.toLong(SepFunctions.openNull(ds3.Tables[0].Rows[j]["ProductID"]));
                                                dAccessClasses.UnitPrice2 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["UnitPrice"]));
                                                dAccessClasses.RecurringCycle2 = SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringCycle"]);
                                                dAccessClasses.RecurringPrice2 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringPrice"]));
                                                break;

                                            case 3:
                                                dAccessClasses.ProductID3 = SepFunctions.toLong(SepFunctions.openNull(ds3.Tables[0].Rows[j]["ProductID"]));
                                                dAccessClasses.UnitPrice3 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["UnitPrice"]));
                                                dAccessClasses.RecurringCycle3 = SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringCycle"]);
                                                dAccessClasses.RecurringPrice3 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringPrice"]));
                                                break;

                                            case 4:
                                                dAccessClasses.ProductID4 = SepFunctions.toLong(SepFunctions.openNull(ds3.Tables[0].Rows[j]["ProductID"]));
                                                dAccessClasses.UnitPrice4 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["UnitPrice"]));
                                                dAccessClasses.RecurringCycle4 = SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringCycle"]);
                                                dAccessClasses.RecurringPrice4 = SepFunctions.toDecimal(SepFunctions.openNull(ds3.Tables[0].Rows[j]["RecurringPrice"]));
                                                break;
                                        }
                                    }
                                }

                                lAccessClasses.Add(dAccessClasses);
                            }
                        }
                    }

                    tmpClassID = arrClassID[1];
                }
            }

            return lAccessClasses;
        }

        /// <summary>
        /// Keys the delete.
        /// </summary>
        /// <param name="KeyIDs">The key i ds.</param>
        /// <returns>System.String.</returns>
        public static string Key_Delete(string KeyIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrKeyIDs = Strings.Split(KeyIDs, ",");

                if (arrKeyIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrKeyIDs); i++)
                    {
                        if (SepFunctions.toLong(arrKeyIDs[i]) != 1 && SepFunctions.toLong(arrKeyIDs[i]) != 2)
                        {
                            using (var cmd = new SqlCommand("UPDATE AccessKeys SET Status='-1', DateDeleted=@DateDeleted WHERE KeyID=@KeyID", conn))
                            {
                                cmd.Parameters.AddWithValue("@KeyID", arrKeyIDs[i]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            return SepFunctions.LangText("Access Key(s) has been successfully deleted.");
        }

        /// <summary>
        /// Keys the get.
        /// </summary>
        /// <param name="KeyID">The key identifier.</param>
        /// <returns>Models.AccessKeys.</returns>
        public static AccessKeys Key_Get(long KeyID)
        {
            var returnXML = new Models.AccessKeys();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM AccessKeys WHERE KeyID=@KeyID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@KeyID", KeyID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.KeyID = SepFunctions.toLong(SepFunctions.openNull(RS["KeyID"]));
                            returnXML.KeyName = SepFunctions.openNull(RS["KeyName"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Keys the save.
        /// </summary>
        /// <param name="KeyID">The key identifier.</param>
        /// <param name="KeyName">Name of the key.</param>
        /// <returns>System.String.</returns>
        public static string Key_Save(long KeyID, string KeyName)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (KeyID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT KeyID FROM AccessKeys WHERE KeyID=@KeyID", conn))
                    {
                        cmd.Parameters.AddWithValue("@KeyID", KeyID);
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
                    KeyID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE AccessKeys SET KeyName=@KeyName WHERE KeyID=@KeyID";
                }
                else
                {
                    SqlStr = "INSERT INTO AccessKeys (KeyID, KeyName, Status) VALUES (@KeyID, @KeyName, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@KeyID", KeyID);
                    cmd.Parameters.AddWithValue("@KeyName", KeyName);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Access Key has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Access Key has been successfully updated.");
            }

            return sReturn;
        }
    }
}