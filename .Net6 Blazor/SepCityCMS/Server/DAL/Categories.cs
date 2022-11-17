// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Categories.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Categories.
    /// </summary>
    public static class Categories
    {
        /// <summary>
        /// Categories the delete.
        /// </summary>
        /// <param name="CatIDs">The cat i ds.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns>System.String.</returns>
        public static string Category_Delete(string CatIDs, int ModuleID)
        {
            var bError = string.Empty;

            var removeCatRecord = false;
            Hashtable arrModuleCatIDs = new Hashtable();
            int iModuleCatIDCount = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrCatIDs = Strings.Split(CatIDs, ",");

                if (arrCatIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrCatIDs); i++)
                    {
                        iModuleCatIDCount += 1;
                        arrModuleCatIDs.Add(iModuleCatIDCount, arrCatIDs[i]);
                        removeCatRecord = false;
                        if (ModuleID == 0)
                        {
                            removeCatRecord = true;
                        }
                        else
                        {
                            // Delete Sub Categories
                            using (var cmd = new SqlCommand("SELECT CatID FROM Categories WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrCatIDs[i]);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    while (RS.Read())
                                    {
                                        iModuleCatIDCount += 1;
                                        arrModuleCatIDs.Add(iModuleCatIDCount, SepFunctions.openNull(RS["CatID"]));
                                        using (var cmd2 = new SqlCommand("UPDATE CategoriesModules SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID AND ModuleID=@ModuleID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS["CatID"]));
                                            cmd2.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                            cmd2.Parameters.AddWithValue("@ModuleID", ModuleID);
                                            cmd2.ExecuteNonQuery();
                                        }

                                        // Delete Sub-Sub Categories
                                        using (var cmd2 = new SqlCommand("SELECT CatID FROM Categories WHERE ListUnder=@ListUnder", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@ListUnder", SepFunctions.openNull(RS["CatID"]));
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                while (RS2.Read())
                                                {
                                                    iModuleCatIDCount += 1;
                                                    arrModuleCatIDs.Add(iModuleCatIDCount, SepFunctions.openNull(RS2["CatID"]));
                                                    using (var cmd3 = new SqlCommand("UPDATE CategoriesModules SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID AND ModuleID=@ModuleID", conn))
                                                    {
                                                        cmd3.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS2["CatID"]));
                                                        cmd3.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                                        cmd3.Parameters.AddWithValue("@ModuleID", ModuleID);
                                                        cmd3.ExecuteNonQuery();
                                                    }

                                                    // Delete Sub-Sub-Sub Categories
                                                    using (var cmd4 = new SqlCommand("SELECT CatID FROM Categories WHERE ListUnder=@ListUnder", conn))
                                                    {
                                                        cmd4.Parameters.AddWithValue("@ListUnder", SepFunctions.openNull(RS2["CatID"]));
                                                        using (SqlDataReader RS3 = cmd4.ExecuteReader())
                                                        {
                                                            while (RS3.Read())
                                                            {
                                                                iModuleCatIDCount += 1;
                                                                arrModuleCatIDs.Add(iModuleCatIDCount, SepFunctions.openNull(RS3["CatID"]));
                                                                using (var cmd5 = new SqlCommand("UPDATE CategoriesModules SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID AND ModuleID=@ModuleID", conn))
                                                                {
                                                                    cmd5.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS3["CatID"]));
                                                                    cmd5.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                                                    cmd5.Parameters.AddWithValue("@ModuleID", ModuleID);
                                                                    cmd5.ExecuteNonQuery();
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            using (var cmd = new SqlCommand("UPDATE CategoriesModules SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID AND ModuleID=@ModuleID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrCatIDs[i]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("SELECT CatID FROM CategoriesModules WHERE CatID=@CatID AND Status <> -1", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrCatIDs[i]);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (!RS.HasRows)
                                    {
                                        removeCatRecord = true;
                                    }
                                }
                            }
                        }

                        if (removeCatRecord)
                        {
                            using (var cmd = new SqlCommand("UPDATE Categories SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrCatIDs[i]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("UPDATE CategoriesModules SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrCatIDs[i]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Delete Module Content
                    ICollection key = arrModuleCatIDs.Keys;

                    foreach (int k in key)
                    {
                        if (removeCatRecord || ModuleID == 35)
                        {
                            using (var cmd = new SqlCommand("UPDATE Articles SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 31)
                        {
                            using (var cmd = new SqlCommand("UPDATE AuctionAds SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 20)
                        {
                            using (var cmd = new SqlCommand("UPDATE BusinessListings SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 44)
                        {
                            using (var cmd = new SqlCommand("UPDATE ClassifiedsAds SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 9)
                        {
                            using (var cmd = new SqlCommand("UPDATE FAQ SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 12)
                        {
                            using (var cmd = new SqlCommand("UPDATE ForumsMessages SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 10)
                        {
                            using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 19)
                        {
                            using (var cmd = new SqlCommand("UPDATE LinksWebSites SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 41)
                        {
                            using (var cmd = new SqlCommand("UPDATE ShopProducts SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 60)
                        {
                            using (var cmd = new SqlCommand("UPDATE Portals SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 5)
                        {
                            using (var cmd = new SqlCommand("UPDATE DiscountSystem SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (removeCatRecord || ModuleID == 65)
                        {
                            using (var cmd = new SqlCommand("UPDATE Vouchers SET Status='-1', DateDeleted=@DateDeleted WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", arrModuleCatIDs[k]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Category(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Categories the delete image.
        /// </summary>
        /// <param name="CatID">The cat identifier.</param>
        /// <returns>System.String.</returns>
        public static string Category_Delete_Image(long CatID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("UPDATE Categories SET ImageData=null, ImageType=null WHERE CatID=@CatID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            sReturn = SepFunctions.LangText("Image has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Categories the get.
        /// </summary>
        /// <param name="CatID">The cat identifier.</param>
        /// <returns>Models.Categories.</returns>
        public static Models.Categories Category_Get(long CatID)
        {
            var returnXML = new Models.Categories();

            var recCount = 0;
            var sModules = string.Empty;
            var sPortals = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Categories WHERE CatID=@CatID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                            returnXML.ListUnder = SepFunctions.toLong(SepFunctions.openNull(RS["ListUnder"]));
                            returnXML.CategoryName = SepFunctions.openNull(RS["CategoryName"]);
                            returnXML.CatType = SepFunctions.openNull(RS["CatType"]);
                            returnXML.Moderator = SepFunctions.openNull(RS["Moderator"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.Keywords = SepFunctions.openNull(RS["Keywords"]);
                            returnXML.SEODescription = SepFunctions.openNull(RS["SEODescription"]);
                            returnXML.SEOPageTitle = SepFunctions.openNull(RS["SEOPageTitle"]);
                            returnXML.AccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                            returnXML.WriteKeys = SepFunctions.openNull(RS["WriteKeys"]);
                            returnXML.ManageKeys = SepFunctions.openNull(RS["ManageKeys"]);
                            returnXML.AccessHide = SepFunctions.toBoolean(SepFunctions.openNull(RS["AccessHide"]));
                            returnXML.WriteHide = SepFunctions.toBoolean(SepFunctions.openNull(RS["WriteHide"]));
                            returnXML.ExcPortalSecurity = SepFunctions.toBoolean(SepFunctions.openNull(RS["ExcPortalSecurity"]));
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.ShowList = SepFunctions.toBoolean(SepFunctions.openNull(RS["ShowList"]));
                            returnXML.Sharing = SepFunctions.toBoolean(SepFunctions.openNull(RS["Sharing"]));
                            if (Information.IsDBNull(RS["ImageData"]) == false)
                            {
                                returnXML.ImageData = Strings.ToString(Information.IsDBNull(RS["ImageData"]) ? string.Empty : SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["ImageData"])));
                            }

                            returnXML.ImageType = SepFunctions.openNull(RS["ImageType"]);
                        }
                    }
                }

                if (returnXML.CatID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ModuleID FROM CategoriesModules WHERE CatID=@CatID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", CatID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                recCount = recCount + 1;
                                if (recCount > 1)
                                {
                                    sModules += ",";
                                }

                                sModules += "|" + SepFunctions.openNull(RS["ModuleID"]) + "|";
                            }
                        }
                    }

                    returnXML.ModuleIDs = sModules;

                    recCount = 0;

                    using (var cmd = new SqlCommand("SELECT PortalID FROM CategoriesPortals WHERE CatID=@CatID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", CatID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                recCount = recCount + 1;
                                if (recCount > 1)
                                {
                                    sPortals += ",";
                                }

                                sPortals += "|" + SepFunctions.openNull(RS["PortalID"]) + "|";
                            }
                        }
                    }

                    returnXML.PortalIDs = sPortals;
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Categories the name of the get by.
        /// </summary>
        /// <param name="CategoryName">Name of the category.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>Models.Categories.</returns>
        public static Models.Categories Category_Get_By_Name(string CategoryName, long FeedID)
        {
            var returnXML = new Models.Categories();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryName=@CategoryName AND FeedID=@FeedID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                            returnXML.ListUnder = SepFunctions.toLong(SepFunctions.openNull(RS["ListUnder"]));
                            returnXML.CategoryName = SepFunctions.openNull(RS["CategoryName"]);
                            returnXML.CatType = SepFunctions.openNull(RS["CatType"]);
                            returnXML.Moderator = SepFunctions.openNull(RS["Moderator"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.Keywords = SepFunctions.openNull(RS["Keywords"]);
                            returnXML.SEODescription = SepFunctions.openNull(RS["SEODescription"]);
                            returnXML.SEOPageTitle = SepFunctions.openNull(RS["SEOPageTitle"]);
                            returnXML.AccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                            returnXML.WriteKeys = SepFunctions.openNull(RS["WriteKeys"]);
                            returnXML.ManageKeys = SepFunctions.openNull(RS["ManageKeys"]);
                            returnXML.AccessHide = SepFunctions.toBoolean(SepFunctions.openNull(RS["AccessHide"]));
                            returnXML.WriteHide = SepFunctions.toBoolean(SepFunctions.openNull(RS["WriteHide"]));
                            returnXML.ExcPortalSecurity = SepFunctions.toBoolean(SepFunctions.openNull(RS["ExcPortalSecurity"]));
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.ShowList = SepFunctions.toBoolean(SepFunctions.openNull(RS["ShowList"]));
                            returnXML.Sharing = SepFunctions.toBoolean(SepFunctions.openNull(RS["Sharing"]));
                            returnXML.ImageData = Strings.ToString(Information.IsDBNull(RS["ImageData"]) ? string.Empty : SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["ImageData"])));

                            returnXML.ImageType = SepFunctions.openNull(RS["ImageType"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Categories the mass save.
        /// </summary>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Modules">The modules.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="AccessKeysHide">if set to <c>true</c> [access keys hide].</param>
        /// <param name="WriteKeys">The write keys.</param>
        /// <param name="WriteKeysHide">if set to <c>true</c> [write keys hide].</param>
        /// <param name="ManageKeys">The manage keys.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="ShareContent">if set to <c>true</c> [share content].</param>
        /// <param name="ExcludePortalSecurity">if set to <c>true</c> [exclude portal security].</param>
        /// <returns>System.String.</returns>
        public static string Category_Mass_Save(long CatID, string Modules, string AccessKeys, bool AccessKeysHide, string WriteKeys, bool WriteKeysHide, string ManageKeys, string PortalIDs, bool ShareContent, bool ExcludePortalSecurity)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("UPDATE Categories SET AccessKeys=@AccessKeys, WriteKeys=@WriteKeys, ManageKeys=@ManageKeys, AccessHide=@AccessHide, WriteHide=@WriteHide, ExcPortalSecurity=@ExcPortalSecurity, Sharing=@Sharing WHERE CatID=@CatID", conn))
                {
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                    cmd.Parameters.AddWithValue("@WriteKeys", WriteKeys);
                    cmd.Parameters.AddWithValue("@ManageKeys", ManageKeys);
                    cmd.Parameters.AddWithValue("@AccessHide", AccessKeysHide);
                    cmd.Parameters.AddWithValue("@WriteHide", WriteKeysHide);
                    cmd.Parameters.AddWithValue("@ExcPortalSecurity", ExcludePortalSecurity);
                    cmd.Parameters.AddWithValue("@ShowList", true);
                    cmd.Parameters.AddWithValue("@Sharing", ShareContent);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM CategoriesPortals WHERE CatID=@CatID", conn))
                {
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.ExecuteNonQuery();
                }

                var arrPortals = Strings.Split(PortalIDs, ",");

                if (arrPortals != null)
                {
                    for (var i = 0; i <= Information.UBound(arrPortals); i++)
                    {
                        using (var cmd = new SqlCommand("INSERT INTO CategoriesPortals (UniqueID, CatID, PortalID) VALUES(@UniqueID, @CatID, @PortalID)", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@CatID", CatID);
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.toLong(Strings.Replace(arrPortals[i], "|", string.Empty)));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                using (var cmd = new SqlCommand("DELETE FROM CategoriesModules WHERE CatID=@CatID", conn))
                {
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.ExecuteNonQuery();
                }

                var arrModules = Strings.Split(Modules, ",");

                if (arrModules != null)
                {
                    for (var i = 0; i <= Information.UBound(arrModules); i++)
                    {
                        using (var cmd = new SqlCommand("INSERT INTO CategoriesModules (UniqueID, CatID, ModuleID, Status) VALUES(@UniqueID, @CatID, @ModuleID, '1')", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@CatID", CatID);
                            cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toInt(Strings.Replace(arrModules[i], "|", string.Empty)));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            sReturn = SepFunctions.LangText("Category has been successfully updated.");

            return sReturn;
        }

        /// <summary>
        /// Categories the save.
        /// </summary>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="ListUnder">The list under.</param>
        /// <param name="CategoryName">Name of the category.</param>
        /// <param name="Description">The description.</param>
        /// <param name="Modules">The modules.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="AccessKeysHide">if set to <c>true</c> [access keys hide].</param>
        /// <param name="WriteKeys">The write keys.</param>
        /// <param name="WriteKeysHide">if set to <c>true</c> [write keys hide].</param>
        /// <param name="ManageKeys">The manage keys.</param>
        /// <param name="PageTitle">The page title.</param>
        /// <param name="MetaDescription">The meta description.</param>
        /// <param name="MetaKeywords">The meta keywords.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <param name="CatType">Type of the cat.</param>
        /// <param name="Moderator">The moderator.</param>
        /// <param name="ShareContent">if set to <c>true</c> [share content].</param>
        /// <param name="ExcludePortalSecurity">if set to <c>true</c> [exclude portal security].</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="ImageData">The image data.</param>
        /// <param name="ImageType">Type of the image.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>System.String.</returns>
        public static string Category_Save(long CatID, string ListUnder, string CategoryName, string Description, string Modules, string AccessKeys, bool AccessKeysHide, string WriteKeys, bool WriteKeysHide, string ManageKeys, string PageTitle, string MetaDescription, string MetaKeywords, string PortalIDs, string CatType, string Moderator, bool ShareContent, bool ExcludePortalSecurity, long Weight, string ImageData, string ImageType, long FeedID)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrCategories = Strings.Split(CategoryName, Environment.NewLine);

                if (arrCategories != null)
                {
                    for (var j = 0; j <= Information.UBound(arrCategories); j++)
                    {
                        if (j > 0)
                        {
                            CatID = SepFunctions.GetIdentity();
                        }

                        if (CatID > 0)
                        {
                            using (var cmd = new SqlCommand("SELECT CatID FROM Categories WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", CatID);
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
                            CatID = SepFunctions.GetIdentity();
                        }

                        if (!string.IsNullOrWhiteSpace(arrCategories[j]))
                        {
                            var SqlStr = string.Empty;
                            if (bUpdate)
                            {
                                var sImageData = !string.IsNullOrWhiteSpace(ImageData) ? ", ImageData=@ImageData, ImageType=@ImageType" : string.Empty;
                                SqlStr = "UPDATE Categories SET ListUnder=@ListUnder, CategoryName=@CategoryName, CatType=@CatType, Moderator=@Moderator, Description=@Description, Keywords=@Keywords, SEODescription=@SEODescription, SEOPageTitle=@SEOPageTitle, AccessKeys=@AccessKeys, WriteKeys=@WriteKeys, ManageKeys=@ManageKeys, AccessHide=@AccessHide, WriteHide=@WriteHide, ExcPortalSecurity=@ExcPortalSecurity, Weight=@Weight, Sharing=@Sharing, FeedID=@FeedID" + sImageData + " WHERE CatID=@CatID";
                            }
                            else
                            {
                                SqlStr = "INSERT INTO Categories (CatID, ListUnder, CategoryName, CatType, Moderator, Description, Keywords, SEODescription, SEOPageTitle, AccessKeys, WriteKeys, ManageKeys, AccessHide, WriteHide, ExcPortalSecurity, Weight, ShowList, Sharing, ImageData, ImageType, FeedID, Status) VALUES(@CatID, @ListUnder, @CategoryName, @CatType, @Moderator, @Description, @Keywords, @SEODescription, @SEOPageTitle, @AccessKeys, @WriteKeys, @ManageKeys, @AccessHide, @WriteHide, @ExcPortalSecurity, @Weight, @ShowList, @Sharing, @ImageData, @ImageType, @FeedID, '1')";
                            }

                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", CatID);
                                cmd.Parameters.AddWithValue("@ListUnder", ListUnder);
                                cmd.Parameters.AddWithValue("@CategoryName", Strings.Trim(arrCategories[j]));
                                cmd.Parameters.AddWithValue("@CatType", !string.IsNullOrWhiteSpace(CatType) ? CatType : string.Empty);
                                cmd.Parameters.AddWithValue("@Moderator", Moderator);
                                cmd.Parameters.AddWithValue("@Description", Description);
                                cmd.Parameters.AddWithValue("@Keywords", MetaKeywords);
                                cmd.Parameters.AddWithValue("@SEODescription", MetaDescription);
                                cmd.Parameters.AddWithValue("@SEOPageTitle", PageTitle);
                                cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                                cmd.Parameters.AddWithValue("@WriteKeys", WriteKeys);
                                cmd.Parameters.AddWithValue("@ManageKeys", ManageKeys);
                                cmd.Parameters.AddWithValue("@AccessHide", AccessKeysHide);
                                cmd.Parameters.AddWithValue("@WriteHide", WriteKeysHide);
                                cmd.Parameters.AddWithValue("@ExcPortalSecurity", ExcludePortalSecurity);
                                cmd.Parameters.AddWithValue("@Weight", Weight);
                                cmd.Parameters.AddWithValue("@ShowList", true);
                                cmd.Parameters.AddWithValue("@Sharing", ShareContent);
                                cmd.Parameters.AddWithValue("@FeedID", FeedID);
                                if (!string.IsNullOrWhiteSpace(ImageData))
                                {
                                    cmd.Parameters.AddWithValue("@ImageData", SepFunctions.StringToBytes(SepFunctions.Base64Decode(ImageData)));
                                    cmd.Parameters.AddWithValue("@ImageType", ImageType);
                                }
                                else
                                {
                                    var sImageData = cmd.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1);
                                    sImageData.Value = DBNull.Value;
                                    var sImageType = cmd.Parameters.Add("@ImageType", SqlDbType.NVarChar, -1);
                                    sImageType.Value = DBNull.Value;
                                }

                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM CategoriesPortals WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", CatID);
                                cmd.ExecuteNonQuery();
                            }

                            var arrPortals = Strings.Split(PortalIDs, ",");
                            if (arrPortals != null)
                            {
                                for (var i = 0; i <= Information.UBound(arrPortals); i++)
                                {
                                    using (var cmd = new SqlCommand("INSERT INTO CategoriesPortals (UniqueID, CatID, PortalID) VALUES(@UniqueID, @CatID, @PortalID)", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.GetIdentity());
                                        cmd.Parameters.AddWithValue("@CatID", CatID);
                                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.toLong(Strings.Replace(arrPortals[i], "|", string.Empty)));
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            using (var cmd = new SqlCommand("DELETE FROM CategoriesModules WHERE CatID=@CatID", conn))
                            {
                                cmd.Parameters.AddWithValue("@CatID", CatID);
                                cmd.ExecuteNonQuery();
                            }

                            var arrModules = Strings.Split(Modules, ",");
                            if (arrModules != null)
                            {
                                for (var i = 0; i <= Information.UBound(arrModules); i++)
                                {
                                    using (var cmd = new SqlCommand("INSERT INTO CategoriesModules (UniqueID, CatID, ModuleID, Status) VALUES(@UniqueID, @CatID, @ModuleID, '1')", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.GetIdentity());
                                        cmd.Parameters.AddWithValue("@CatID", CatID);
                                        cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toInt(Strings.Replace(arrModules[i], "|", string.Empty)));
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var sReturn = string.Empty;
            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Category has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Category has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Categories&gt;.</returns>
        public static List<Models.Categories> GetCategories(int ModuleID, string SortExpression = "CategoryName", string SortDirection = "ASC", string searchWords = "", long CategoryID = 0)
        {
            var lCategories = new List<Models.Categories>();

            var wClause = string.Empty;

            var strGetPageURL = string.Empty;

            var SqlStr = string.Empty;

            string[] arrUserKeys = null;

            var ImageData = string.Empty;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var countTable = string.Empty;
            var countID = string.Empty;
            var countWC = string.Empty;
            var CountSqlStr = string.Empty;
            long iCount = 0;

            var UserAccessKeys = SepFunctions.GetUserInformation("AccessKeys");

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "CAT.Weight,CAT.CategoryName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND CategoryName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (ModuleID > 0)
            {
                wClause += " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1)";
            }

            if (ModuleID != 60)
            {
                wClause += " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesPortals WHERE (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalID = -1) AND CatID=CAT.CatID)";
            }

            if (CategoryID > 0)
            {
                wClause += " AND CAT.ListUnder='" + SepFunctions.FixWord(Strings.ToString(CategoryID)) + "'";
            }
            else
            {
                wClause += " AND CAT.ListUnder='0'";
            }

            if (!string.IsNullOrWhiteSpace(UserAccessKeys))
            {
                wClause += " AND (";
                arrUserKeys = Strings.Split(UserAccessKeys, ",");
                if (arrUserKeys != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserKeys); i++)
                    {
                        if (i > 0)
                        {
                            wClause += " OR ";
                        }

                        wClause += "(CAT.AccessKeys LIKE '%|" + SepFunctions.FixWord(Strings.Replace(arrUserKeys[i], "|", string.Empty)) + "|%' OR CAT.AccessHide='0')";
                    }
                }

                wClause += ")";
            }
            else
            {
                wClause += " AND CAT.AccessKeys LIKE '%|1|%' OR CAT.AccessHide='0'";
            }

            switch (ModuleID)
            {
                case 5:
                    countTable = "DiscountSystem";
                    countID = "DiscountID";
                    countWC = " AND ExpireDate > getDate() AND Quantity > '0' AND ShowWeb='1' AND DiscountSystem.Status=1";
                    break;

                case 7:
                    countTable = "UPagesSites";
                    countID = "SiteID";
                    countWC = " AND UPagesSites.Status=1";
                    break;

                case 9:
                    countTable = "FAQ";
                    countID = "FAQID";
                    countWC = " AND FAQ.Status=1";
                    break;

                case 10:
                    countTable = "LibrariesFiles";
                    countID = "FileID";
                    countWC = " AND LibrariesFiles.Status=1";
                    break;

                case 12:
                    countTable = "ForumsMessages";
                    countID = "TopicID";
                    countWC = " AND ReplyID='0' AND ForumsMessages.Status=1";
                    break;

                case 19:
                    countTable = "LinksWebsites";
                    countID = "LinkID";
                    countWC = " AND LinksWebsites.Status=1";
                    break;

                case 20:
                    countTable = "BusinessListings";
                    countID = "DISTINCT LinkID";
                    countWC = " AND BusinessListings.Status=1";
                    break;

                case 23:
                    countTable = "News";
                    countID = "NewsID";
                    countWC = " AND News.Status=1 AND News.StartDate <= GETDATE() AND News.EndDate >= GETDATE()";
                    break;

                case 25:
                    countTable = "PNQQuestions";
                    countID = "PollID";
                    countWC = " AND PNQQuestions.Status=1 AND PNQQuestions.StartDate <= GETDATE() AND PNQQuestions.EndDate >= GETDATE()";
                    break;

                case 31:
                    countTable = "AuctionAds";
                    countID = "AdID";
                    countWC = " AND AuctionAds.Status=1 AND EndDate > getDate() AND AdID=LinkID";
                    break;

                case 32:
                    countTable = "RStateProperty";
                    countID = "PropertyID";
                    countWC = " AND RStateProperty.Status=1";
                    break;

                case 35:
                    countTable = "Articles";
                    countID = "ArticleID";
                    countWC = " AND Articles.Status=1 AND Articles.Start_Date < GETDATE() AND Articles.End_Date > GETDATE()";
                    break;

                case 37:
                    countTable = "ELearnCourses";
                    countID = "CourseID";
                    countWC = " AND ELearnCourses.StartDate > getDate() AND ELearnCourses.Status=1";
                    break;

                case 41:
                    countTable = "ShopProducts";
                    countID = "ProductID";
                    countWC = " AND ShopProducts.Status=1";
                    break;

                case 44:
                    countTable = "ClassifiedsAds";
                    countID = "AdID";
                    countWC = " AND SoldOut='0' AND ClassifiedsAds.Status=1 AND EndDate > getDate() AND AdID=LinkID";
                    break;

                case 60:
                    countTable = "Portals";
                    countID = "PortalID";
                    countWC = " AND HideList='0' AND Portals.Status=1";
                    break;

                case 61:
                    countTable = "Blog";
                    countID = "BlogID";
                    countWC = " AND Blog.Status=1 AND StartDate <= GETDATE() AND EndDate >= GETDATE()";
                    break;

                case 65:
                    countTable = "Vouchers";
                    countID = "VoucherID";
                    countWC = " AND Vouchers.Status=1 AND BuyEndDate > getDate()";
                    break;
            }

            CountSqlStr = "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + " WHERE ";
            CountSqlStr += "CatID IN (SELECT CatID FROM Categories WHERE CatID=" + countTable + ".CatID AND CatID=CAT.CatID)" + countWC + ") AS Count1, ";
            CountSqlStr += "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + " WHERE ";
            CountSqlStr += "CatID IN (SELECT CatID FROM Categories WHERE CatID=" + countTable + ".CatID AND ListUnder=CAT.CatID)" + countWC + ") AS Count2, ";
            CountSqlStr += "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + ",Categories AS CCAT1 WHERE ";
            CountSqlStr += "CCAT1.ListUnder=CAT.CatID " + countWC + " AND " + countTable + ".CatID IN (SELECT CatID FROM Categories WHERE ListUnder=CCAT1.CatID)) AS Count3, ";
            if (CategoryID == 0)
            {
                CountSqlStr += "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + ",Categories AS CCAT1 WHERE ";
                CountSqlStr += "CCAT1.ListUnder=CAT.CatID " + countWC + " AND " + countTable + ".CatID IN (SELECT ListUnder FROM Categories WHERE ListUnder=CCAT1.ListUnder)) AS Count4 ";
            }
            else
            {
                CountSqlStr += "'0' AS Count4 ";
            }

            if (ModuleID == 12)
            {
                SqlStr = "SELECT CAT.CatID,CAT.ListUnder,CAT.CategoryName,CAT.Description,Cat.ImageDatA FROM Categories AS CAT WHERE CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1) AND CAT.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection;
            }
            else
            {
                SqlStr = "SELECT CAT.CatID,CAT.ListUnder,CAT.CategoryName,CAT.Description,Cat.ImageData," + CountSqlStr + " FROM Categories AS CAT WHERE CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1) AND CAT.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection;
            }
            ////////////////////////////////////////////////////////////////////////

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand(SqlStr, conn))
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

                    iCount = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Count1"])) + SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Count2"])) + SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Count3"])) + SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Count4"]));
                    var dCateories = new Models.Categories { CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"])) };
                    dCateories.CategoryName = SepFunctions.openNull(ds.Tables[0].Rows[i]["CategoryName"]);
                    dCateories.ContentCount = iCount;
                    dCateories.ModuleID = ModuleID;
                    lCategories.Add(dCateories);
                }
            }

            return lCategories;
        }
    }
}