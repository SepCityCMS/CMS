// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SiteTemplates.cs" company="SepCity, Inc.">
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
    using System.IO;

    /// <summary>
    /// Class SiteTemplates.
    /// </summary>
    public static class SiteTemplates
    {
        /// <summary>
        /// Gets the templates.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>List&lt;Models.Templates&gt;.</returns>
        public static List<Templates> GetTemplates(string SortExpression = "TemplateName", string SortDirection = "ASC", string searchWords = "", long PortalID = 0)
        {
            var lTemplates = new List<Templates>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "TemplateName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (TemplateName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT TemplateID,TemplateName,Description,FolderName,useTemplate,DatePosted FROM SiteTemplates WHERE Status <> -1" + wClause + " ORDER BY useTemplate DESC," + SortExpression + " " + SortDirection, conn))
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

                    var dTemplates = new Models.Templates { TemplateID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TemplateID"])) };
                    dTemplates.TemplateName = SepFunctions.openNull(ds.Tables[0].Rows[i]["TemplateName"]);
                    if (File.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(ds.Tables[0].Rows[i]["FolderName"]) + "\\screen.jpg"))
                    {
                        dTemplates.ScreenShot = SepFunctions.GetDirValue("skins", true) + SepFunctions.openNull(ds.Tables[0].Rows[i]["FolderName"]) + "/screen.jpg";
                    }
                    else
                    {
                        dTemplates.ScreenShot = SepFunctions.GetDirValue("images", true) + "public/no-photo.jpg";
                    }

                    if (File.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(ds.Tables[0].Rows[i]["FolderName"]) + "\\screen.jpg"))
                    {
                        dTemplates.ScreenShotLarge = SepFunctions.GetDirValue("skins", true) + SepFunctions.openNull(ds.Tables[0].Rows[i]["FolderName"]) + "/screen_lrg.jpg";
                    }
                    else
                    {
                        dTemplates.ScreenShotLarge = SepFunctions.GetDirValue("images", true) + "public/no-photo.jpg";
                    }

                    dTemplates.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    if (PortalID > 0)
                    {
                        if (SepFunctions.toLong(SepFunctions.PortalSetup("WEBSITELAYOUT", PortalID)) == SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TemplateID"])))
                        {
                            dTemplates.useTemplate = true;
                        }
                    }
                    else
                    {
                        dTemplates.useTemplate = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["useTemplate"]));
                    }

                    dTemplates.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lTemplates.Add(dTemplates);
                }
            }

            return lTemplates;
        }

        /// <summary>
        /// Templates the delete.
        /// </summary>
        /// <param name="TemplateIDs">The template i ds.</param>
        /// <returns>System.String.</returns>
        public static string Template_Delete(string TemplateIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrTemplateIDs = Strings.Split(TemplateIDs, ",");

                if (arrTemplateIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrTemplateIDs); i++)
                    {
                        using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                        {
                            cmd.Parameters.AddWithValue("@TemplateID", arrTemplateIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (Directory.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\"))
                                    {
                                        Directory.Move(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\", SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + " (DELETED)\\");
                                    }
                                }
                            }
                        }

                        using (var cmd = new SqlCommand("UPDATE SiteTemplates SET Status='-1', DateDeleted=@DateDeleted WHERE TemplateID=@TemplateID AND useTemplate='0'", conn))
                        {
                            cmd.Parameters.AddWithValue("@TemplateID", arrTemplateIDs[i]);
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

            sReturn = SepFunctions.LangText("Template(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Templates the get.
        /// </summary>
        /// <param name="TemplateID">The template identifier.</param>
        /// <returns>Models.Templates.</returns>
        public static Templates Template_Get(long TemplateID)
        {
            var returnXML = new Models.Templates();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM SiteTemplates WHERE TemplateID=@TemplateID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.TemplateID = TemplateID;
                            returnXML.TemplateName = SepFunctions.openNull(RS["TemplateName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.FolderName = SepFunctions.openNull(RS["FolderName"]);
                            returnXML.EnableUserPage = SepFunctions.openBoolean(RS["enableUserPage"]);
                            returnXML.AccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Templates the mark active.
        /// </summary>
        /// <param name="TemplateID">The template identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Template_Mark_Active(long TemplateID, long PortalID)
        {
            var bUpdate = false;
            var strXML = string.Empty;
            var currentTemplate = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (PortalID == 0)
                {
                    using (var cmd = new SqlCommand("UPDATE SiteTemplates SET useTemplate='0'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("UPDATE SiteTemplates SET useTemplate='1' WHERE TemplateID=@TemplateID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    try
                    {
                        if (File.Exists(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                        {
                            bUpdate = true;
                            using (var readfile = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                            {
                                strXML = readfile.ReadToEnd();
                                currentTemplate = "<WebSiteLayout>" + SepFunctions.ParseXML("WebSiteLayout", strXML) + "</WebSiteLayout>";
                                if (Strings.InStr(strXML, currentTemplate) > 0)
                                {
                                    strXML = Strings.Replace(strXML, currentTemplate, "<WebSiteLayout>" + TemplateID + "</WebSiteLayout>");
                                }
                                else
                                {
                                    strXML = Strings.Replace(strXML, "</ROOTLEVEL>", "<WebSiteLayout>" + TemplateID + "</WebSiteLayout>" + "</ROOTLEVEL>");
                                }
                            }
                        }
                    }
                    catch
                    {
                        bUpdate = false;
                    }

                    if (bUpdate == false)
                    {
                        strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                        strXML += "<ROOTLEVEL>" + Environment.NewLine;
                        strXML += "<AdminInfo>" + Environment.NewLine;
                        strXML += "<FullName>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName")) + "</FullName>" + Environment.NewLine;
                        strXML += "<EmailAddress>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("EmailAddress")) + "</EmailAddress>" + Environment.NewLine;
                        strXML += "<CompanyName></CompanyName>" + Environment.NewLine;
                        strXML += "<Street>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("StreetAddress")) + "</Street>" + Environment.NewLine;
                        strXML += "<City>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("City")) + "</City>" + Environment.NewLine;
                        strXML += "<State>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("State")) + "</State>" + Environment.NewLine;
                        strXML += "<PostalCode>" + SepFunctions.HTMLEncode(SepFunctions.GetUserInformation("ZipCode")) + "</PostalCode>" + Environment.NewLine;
                        strXML += "<Country></Country>" + Environment.NewLine;
                        strXML += "</AdminInfo>" + Environment.NewLine;
                        strXML += "<SiteSetup>" + Environment.NewLine;
                        strXML += "<SiteCounter>Yes</SiteCounter>" + Environment.NewLine;
                        strXML += "<SiteLang>en-us</SiteLang>" + Environment.NewLine;
                        strXML += "</SiteSetup>" + Environment.NewLine;
                        strXML += "<SiteLayout>" + Environment.NewLine;
                        strXML += "<SiteMenu1>Top Menu</SiteMenu1>" + Environment.NewLine;
                        strXML += "<SiteMenu2>Reserved Menu 1</SiteMenu2>" + Environment.NewLine;
                        strXML += "<SiteMenu3>Reserved Menu 2</SiteMenu3>" + Environment.NewLine;
                        strXML += "<SiteMenu4>Member Menu</SiteMenu4>" + Environment.NewLine;
                        strXML += "<SiteMenu5>Reserved Menu 3</SiteMenu5>" + Environment.NewLine;
                        strXML += "<SiteMenu6>Reserved Menu 4</SiteMenu6>" + Environment.NewLine;
                        strXML += "<SiteMenu7>Bottom Menu</SiteMenu7>" + Environment.NewLine;
                        strXML += "<WebSiteLayout>" + SepFunctions.HTMLEncode(Strings.ToString(TemplateID)) + "</WebSiteLayout>" + Environment.NewLine;
                        strXML += "<WebSiteTheme></WebSiteTheme>" + Environment.NewLine;
                        strXML += "<WebSiteMobileTheme></WebSiteMobileTheme>" + Environment.NewLine;
                        strXML += "</SiteLayout>" + Environment.NewLine;
                        strXML += "<PaymentGateway>" + Environment.NewLine;
                        strXML += "<UseMasterPortal>1</UseMasterPortal>" + Environment.NewLine;
                        strXML += "<AuthorizeNet></AuthorizeNet>" + Environment.NewLine;
                        strXML += "<PayPal></PayPal>" + Environment.NewLine;
                        strXML += "<GoogleMerchantID></GoogleMerchantID>" + Environment.NewLine;
                        strXML += "<eSelectAPIToken></eSelectAPIToken>" + Environment.NewLine;
                        strXML += "<eSelectStoreID></eSelectStoreID>" + Environment.NewLine;
                        strXML += "<CheckEmailTo></CheckEmailTo>" + Environment.NewLine;
                        strXML += "<CheckAddress></CheckAddress>" + Environment.NewLine;
                        strXML += "<CheckInstructions></CheckInstructions>" + Environment.NewLine;
                        strXML += "<MSPAccountID></MSPAccountID>" + Environment.NewLine;
                        strXML += "<MSPSiteID></MSPSiteID>" + Environment.NewLine;
                        strXML += "<MSPSiteCode></MSPSiteCode>" + Environment.NewLine;
                        strXML += "</PaymentGateway>" + Environment.NewLine;
                        strXML += "</ROOTLEVEL>" + Environment.NewLine;
                    }

                    using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-" + PortalID + ".xml"))
                    {
                        outfile.Write(strXML);
                    }
                }
            }

            SepFunctions.Cache_Remove();

            var sReturn = SepFunctions.LangText("Template has been successfully marked as active.");

            return sReturn;
        }

        /// <summary>
        /// Templates the upload.
        /// </summary>
        /// <param name="TemplateID">The template identifier.</param>
        /// <param name="Html">The HTML.</param>
        /// <param name="Css">The CSS.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Template_Upload(long TemplateID, string Html, string Css, long PortalID, long Timestamp)
        {
            var sReturn = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                {
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            try
                            {
                                using (var outfile = new StreamWriter(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\custom" + PortalID + ".master"))
                                {
                                    outfile.Write(SepCore.Strings.Replace(Html, "[[foldername]]", SepFunctions.openNull(RS["FolderName"])));
                                }
                                var dir = new DirectoryInfo(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\styles\\");
                                foreach (var file in dir.EnumerateFiles("custom" + PortalID + "-*.css"))
                                {
                                    file.Delete();
                                }
                                using (var outfile = new StreamWriter(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\styles\\custom" + PortalID + "-" + Timestamp + ".css"))
                                {
                                    outfile.Write(Css);
                                }
                            }
                            catch
                            {
                                sReturn = SepFunctions.LangText("Error access the template folder. (Folder/Files may be in use)");
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(sReturn))
            {
                return sReturn;
            }

            return SepFunctions.LangText("Template has been successfully saved.");
        }

        /// <summary>
        /// Templates the save.
        /// </summary>
        /// <param name="TemplateID">The template identifier.</param>
        /// <param name="TemplateName">Name of the template.</param>
        /// <param name="enableUserPage">The enable user page.</param>
        /// <param name="Description">The description.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <returns>System.String.</returns>
        public static string Template_Save(long TemplateID, string TemplateName, long enableUserPage, string Description, string AccessKeys)
        {
            var sReturn = string.Empty;

            var sFolderName = Strings.Left(SepFunctions.ReplaceSpecial(Strings.Replace(TemplateName, " ", string.Empty)), 30);

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FolderName,Status FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                {
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            if (Strings.LCase(SepFunctions.openNull(RS["FolderName"])) != Strings.LCase(sFolderName))
                            {
                                try
                                {
                                    if (Directory.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\"))
                                    {
                                        Directory.Move(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\", SepFunctions.GetDirValue("skins") + sFolderName + "\\");
                                    }

                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 2)
                                    {
                                        if (Directory.Exists(SepFunctions.GetDirValue("App_Data") + "templates\\saved\\" + SepFunctions.openNull(RS["FolderName"]) + "\\"))
                                        {
                                            Directory.Move(SepFunctions.GetDirValue("App_Data") + "templates\\saved\\" + SepFunctions.openNull(RS["FolderName"]) + "\\", SepFunctions.GetDirValue("App_Data") + "templates\\saved\\" + sFolderName + "\\");
                                        }
                                    }

                                    var sTemplateFile = string.Empty;

                                    using (var reader = File.OpenText(SepFunctions.GetDirValue("skins") + sFolderName + "\\template.master"))
                                    {
                                        sTemplateFile = Strings.Replace(reader.ReadToEnd(), "/" + SepFunctions.openNull(RS["FolderName"]) + "/", "/" + sFolderName + "/");
                                    }

                                    using (var outfile = new StreamWriter(SepFunctions.GetDirValue("skins") + sFolderName + "\\template.master"))
                                    {
                                        outfile.Write(sTemplateFile);
                                    }
                                }
                                catch
                                {
                                    sReturn = SepFunctions.LangText("Error access the template folder. (Folder/Files may be in use)");
                                }
                            }
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(sReturn))
                {
                    using (var cmd = new SqlCommand("UPDATE SiteTemplates SET TemplateName=@TemplateName, enableUserPage=@enableUserPage, Description=@Description, FolderName=@FolderName, DateUpdated=@DateUpdated, AccessKeys=@AccessKeys WHERE TemplateID=@TemplateID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                        cmd.Parameters.AddWithValue("@TemplateName", TemplateName);
                        cmd.Parameters.AddWithValue("@enableUserPage", enableUserPage);
                        cmd.Parameters.AddWithValue("@Description", Description);
                        cmd.Parameters.AddWithValue("@FolderName", sFolderName);
                        cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(sReturn))
            {
                return sReturn;
            }

            sReturn = SepFunctions.LangText("Template has been successfully updated.");
            return sReturn;
        }
    }
}