// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Downloads.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class Downloads.
    /// </summary>
    public static class Downloads
    {
        /// <summary>
        /// Downloads the change status.
        /// </summary>
        /// <param name="DownloadIDs">The download i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Download_Change_Status(string DownloadIDs, int Status)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrDownloadIDs = Strings.Split(DownloadIDs, ",");

                if (arrDownloadIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrDownloadIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Status=@Status WHERE FileID=@FileID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FileID", arrDownloadIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
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

            sReturn = SepFunctions.LangText("File(s) status has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Downloads the delete.
        /// </summary>
        /// <param name="DownloadIDs">The download i ds.</param>
        /// <returns>System.String.</returns>
        public static string Download_Delete(string DownloadIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrDownloadIDs = Strings.Split(DownloadIDs, ",");

                if (arrDownloadIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrDownloadIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Status='-1', DateDeleted=@DateDeleted WHERE FileID=@FileID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FileID", arrDownloadIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(10, arrDownloadIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("File(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Downloads the get.
        /// </summary>
        /// <param name="FileID">The file identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.Downloads.</returns>
        public static Models.Downloads Download_Get(long FileID, long ChangeID = 0, string UserID = "")
        {
            var returnXML = new Models.Downloads();
            string whereClause = string.Empty;

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                whereClause = " AND LibrariesFiles.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Downloads=Downloads+1 WHERE FileID=@FileID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@FileID", FileID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT LibrariesFiles.*,Categories.CatType FROM LibrariesFiles,Categories WHERE LibrariesFiles.CatID=Categories.CatID AND LibrariesFiles.FileID=@FileID AND LibrariesFiles.Status <> -1" + whereClause, conn))
                {
                    cmd.Parameters.AddWithValue("@FileID", FileID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var logData = string.Empty;
                            if (ChangeID > 0)
                            {
                                logData = SepFunctions.Get_Change_Log(ChangeID);
                            }

                            if (ChangeID > 0 && !string.IsNullOrWhiteSpace(logData))
                            {
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    var fieldName = RS.GetName(i);
                                    var fieldType = RS.GetFieldType(i);
                                    var fieldValue = SepFunctions.openNull(RS[i]);
                                    if (Strings.InStr(logData, "<" + fieldName + ">") > 0)
                                    {
                                        var xmlNode = SepFunctions.ParseXML(fieldName, logData);
                                        if (!string.IsNullOrWhiteSpace(xmlNode))
                                        {
                                            fieldValue = xmlNode;
                                        }
                                    }

                                    var prop = returnXML.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        switch (fieldType.Name)
                                        {
                                            case "Double":
                                                prop.SetValue(returnXML, SepFunctions.toLong(fieldValue), null);
                                                break;

                                            case "DateTime":
                                                prop.SetValue(returnXML, SepFunctions.toDate(fieldValue), null);
                                                break;

                                            case "Boolean":
                                                prop.SetValue(returnXML, SepFunctions.toBoolean(fieldValue), null);
                                                break;

                                            case "Int32":
                                                prop.SetValue(returnXML, SepFunctions.toInt(fieldValue), null);
                                                break;

                                            case "Decimal":
                                                prop.SetValue(returnXML, SepFunctions.toDecimal(fieldValue), null);
                                                break;

                                            default:
                                                prop.SetValue(returnXML, fieldValue, null);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var FileExtsntions = string.Empty;
                                string[] arrFileExistions = null;
                                var fileName = string.Empty;
                                switch (SepFunctions.openNull(RS["CatType"]))
                                {
                                    case "Audio":
                                        FileExtsntions = "mp3,wav";
                                        break;

                                    case "Document":
                                        FileExtsntions = "doc,docx,txt,rtf,pdf";
                                        break;

                                    case "Image":
                                        FileExtsntions = "jpg,gif,png";
                                        break;

                                    case "Software":
                                        FileExtsntions = "zip,rar";
                                        break;

                                    case "Video":
                                        FileExtsntions = "mp4,avi,mpeg,flv";
                                        break;
                                }

                                if (!string.IsNullOrWhiteSpace(FileExtsntions))
                                {
                                    arrFileExistions = Strings.Split(FileExtsntions, ",");

                                    if (arrFileExistions != null)
                                    {
                                        for (var i = 0; i <= Information.UBound(arrFileExistions); i++)
                                        {
                                            if (File.Exists(SepFunctions.GetDirValue("downloads") + SepFunctions.openNull(RS["FileID"]) + "." + arrFileExistions[i]))
                                            {
                                                fileName = SepFunctions.openNull(RS["FileID"]) + "." + arrFileExistions[i];
                                            }
                                        }
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(fileName))
                                {
                                    returnXML.FileID = SepFunctions.toLong(SepFunctions.openNull(RS["FileID"]));
                                    returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                                    returnXML.Field1 = SepFunctions.openNull(RS["Field1"]);
                                    returnXML.Field2 = SepFunctions.openNull(RS["Field2"]);
                                    returnXML.Field3 = SepFunctions.openNull(RS["Field3"]);
                                    returnXML.Field4 = SepFunctions.openNull(RS["Field4"]);
                                    returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                    returnXML.TotalDownloads = SepFunctions.toLong(SepFunctions.openNull(RS["Downloads"]));
                                    returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                    returnXML.eDownload = SepFunctions.toBoolean(SepFunctions.openNull(RS["eDownload"]));
                                    returnXML.FileType = SepFunctions.openNull(RS["CatType"]);
                                    returnXML.FileName = fileName;
                                    returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                    returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                                }
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Downloads the save.
        /// </summary>
        /// <param name="FileID">The file identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="Field1">The field1.</param>
        /// <param name="Field2">The field2.</param>
        /// <param name="Field3">The field3.</param>
        /// <param name="Field4">The field4.</param>
        /// <param name="eDownload">if set to <c>true</c> [e download].</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Download_Save(long FileID, string UserID, long CatID, string Field1, string Field2, string Field3, string Field4, bool eDownload, bool Approved, string FileName, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (FileID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM LibrariesFiles WHERE FileID=@FileID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FileID", FileID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    oldValues.Add(RS.GetName(i), SepFunctions.openNull(RS[i]));
                                }

                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    FileID = SepFunctions.GetIdentity();
                }

                if (!string.IsNullOrWhiteSpace(FileName))
                {
                    var sFileBytes = File.ReadAllBytes(SepFunctions.GetDirValue("downloads") + FileName);
                    for (var i = 0; i <= sFileBytes.Length - 1; i++)
                    {
                        sFileBytes[i] = Convert.ToByte(Math.Pow(floatConversion(BitConverter.GetBytes(sFileBytes[i])), 256));
                    }

                    var FileSize = Convert.ToInt64(sFileBytes.Length);
                    var fileExt = Path.GetExtension(FileName);

                    using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID='10'", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", FileID);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, PortalID) VALUES (@UploadID, @UniqueID, @UserID, '10', @FileName, @FileSize, @ContentType, '0', '1', @DatePosted, @PortalID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@UniqueID", FileID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@FileName", FileName);
                        cmd.Parameters.AddWithValue("@FileSize", FileSize);
                        cmd.Parameters.AddWithValue("@ContentType", string.Empty);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.ExecuteNonQuery();
                    }

                    if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".png")
                    {
                        // using (show_image sResize = new show_image())
                        // {
                        // File.WriteAllBytes(SepFunctions.GetDirValue("downloads") + "thumb_" + FileName, sResize.Resize_Image(FileName, sFileBytes, 10));
                        // }
                    }
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE LibrariesFiles SET CatID=@CatID, UserID=@UserID, Field1=@Field1, Field2=@Field2, Field3=@Field3, Field4=@Field4, eDownload=@eDownload, PortalID=@PortalID WHERE FileID=@FileID";
                }
                else
                {
                    SqlStr = "INSERT INTO LibrariesFiles (FileID, CatID, UserID, Field1, Field2, Field3, Field4, eDownload, PortalID, DatePosted, Status, Downloads) VALUES (@FileID, @CatID, @UserID, @Field1, @Field2, @Field3, @Field4, @eDownload, @PortalID, @DatePosted, @Status, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@FileID", FileID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Field1", !string.IsNullOrWhiteSpace(Field1) ? Field1 : string.Empty);
                    cmd.Parameters.AddWithValue("@Field2", !string.IsNullOrWhiteSpace(Field2) ? Field2 : string.Empty);
                    cmd.Parameters.AddWithValue("@Field3", !string.IsNullOrWhiteSpace(Field3) ? Field3 : string.Empty);
                    cmd.Parameters.AddWithValue("@Field4", !string.IsNullOrWhiteSpace(Field4) ? Field4 : string.Empty);
                    cmd.Parameters.AddWithValue("@Status", Approved ? "1" : "0");
                    cmd.Parameters.AddWithValue("@eDownload", eDownload);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 10, Strings.ToString(FileID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "DOWNLOADS", "Downloads", "UploadFile");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM LibrariesFiles WHERE FileID=@FileID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FileID", FileID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    if (oldValues.ContainsKey(RS.GetName(i)))
                                    {
                                        if (SepFunctions.openNull(RS[i]) != SepFunctions.openNull(oldValues[RS.GetName(i)]))
                                        {
                                            changedValues.Add(RS.GetName(i), SepFunctions.openNull(oldValues[RS.GetName(i)]));
                                        }
                                    }
                                }

                                bUpdate = true;
                            }
                        }
                    }

                    if (changedValues.Count > 0)
                    {
                        var writeLog = new StringBuilder();
                        writeLog.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>");
                        writeLog.AppendLine("<root>");
                        for (var e = changedValues.GetEnumerator(); e.MoveNext();)
                        {
                            writeLog.AppendLine("<" + e.Key + ">" + SepFunctions.HTMLEncode(Strings.ToString(e.Value)) + "</" + e.Key + ">");
                        }

                        writeLog.AppendLine("</root>");
                        SepFunctions.Update_Change_Log(10, Strings.ToString(FileID), Field1, Strings.ToString(writeLog));
                    }
                }
            }

            if (intReturn == 0)
            {
                if (bUpdate)
                {
                    return 2;
                }

                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Gets the downloads.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="catId">The cat identifier.</param>
        /// <param name="CatType">Type of the cat.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.Downloads&gt;.</returns>
        public static List<Models.Downloads> GetDownloads(string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "", long catId = 0, string CatType = "", string UserID = "", string StartDate = "")
        {
            var lDownloads = new List<Models.Downloads>();

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            wClause = "Mod.Status <> -1";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND (Field1 LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Field2 LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Field3 LIKE '" + SepFunctions.FixWord(searchWords) + "%' OR Field4 LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (catId > 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(catId)) + "'";
            }

            if (!string.IsNullOrWhiteSpace(CatType))
            {
                wClause += " AND CAT.CatType='" + SepFunctions.FixWord(CatType) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND Mod.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DatePosted > '" + StartDate + "'";
            }

            SqlDataAdapter da = null;

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.*,CAT.CatType,(SELECT TOP 1 Username FROM Members WHERE UserID=MOD.UserID) AS Username,(SELECT TOP 1 FileName FROM Uploads WHERE ModuleID='10' AND UniqueID=MOD.FileID AND Approved='1' AND isTemp='0') AS FileName,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='10' AND UniqueID=MOD.FileID AND Approved='1' AND isTemp='0') AS UploadID FROM LibrariesFiles AS Mod" + SepFunctions.Category_SQL_Manage_Select(catId, wClause, true) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (da = new SqlDataAdapter(cmd))
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

                    var dDownloads = new Models.Downloads { FileID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FileID"])) };
                    dDownloads.CatID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]));
                    dDownloads.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dDownloads.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dDownloads.Field1 = SepFunctions.openNull(ds.Tables[0].Rows[i]["Field1"]);
                    dDownloads.Field2 = SepFunctions.openNull(ds.Tables[0].Rows[i]["Field2"]);
                    dDownloads.Field3 = SepFunctions.openNull(ds.Tables[0].Rows[i]["Field3"]);
                    dDownloads.Field4 = SepFunctions.openNull(ds.Tables[0].Rows[i]["Field4"]);
                    dDownloads.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dDownloads.TotalDownloads = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Downloads"]));
                    switch (SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"])))
                    {
                        case -1:
                            dDownloads.StatusText = SepFunctions.LangText("Deleted");
                            break;

                        case 0:
                            dDownloads.StatusText = SepFunctions.LangText("Pending");
                            break;

                        case 1:
                            dDownloads.StatusText = SepFunctions.LangText("Active");
                            break;
                    }

                    if (SepFunctions.openNull(ds.Tables[0].Rows[i]["CatType"]) == "Image")
                    {
                        dDownloads.FileName = sImageFolder + "spadmin/show_Image.aspx?UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]) + "&ModuleID=10&Size=thumb";
                    }
                    else
                    {
                        dDownloads.FileName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FileName"]);
                    }

                    dDownloads.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lDownloads.Add(dDownloads);
                }
            }

            return lDownloads;
        }

        /// <summary>
        /// Floats the conversion.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.Double.</returns>
        private static double floatConversion(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes); // Convert big endian to little endian
            }

            double myFloat = BitConverter.ToSingle(bytes, 0);
            return myFloat;
        }
    }
}