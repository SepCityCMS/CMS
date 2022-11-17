// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="upload.ashx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web;

    /// <summary>
    /// Class upload.
    /// Implements the <see cref="System.Web.IHttpHandler" />
    /// </summary>
    /// <seealso cref="System.Web.IHttpHandler" />
    public class upload : IHttpHandler
    {
        // -V3072
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="System.Web.IHttpHandler" /> instance.
        /// </summary>
        /// <value><c>true</c> if this instance is reusable; otherwise, <c>false</c>.</value>
        public bool IsReusable => false;

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            if (SepFunctions.toLong(Request.Item("ModuleID")) == 28)
                if (SepFunctions.Check_User_Points(28, "PostUploadPicture", "GetUploadPicture", Request.Item("Identity"), false) == false)
                {
                    context.Response.Write("Error");
                    return;
                }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (Request.Item("UploadMode") == "single")
                    using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID" + Strings.ToString(SepFunctions.toLong(Request.Item("ModuleID")) == 37 || SepFunctions.toLong(Request.Item("ModuleID")) == 5 || SepFunctions.toLong(Request.Item("ModuleID")) == 65 ? " AND ControlID=@ControlID" : string.Empty), conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(Request.Item("ContentID")));
                        cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(Request.Item("ModuleID")));
                        cmd.Parameters.AddWithValue("@ControlID", Request.Item("ControlID"));
                        cmd.ExecuteNonQuery();
                    }

                using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE isTemp='1' AND DatePosted < DateAdd(Day, -7, GetDate())", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            var oFiles = context.Request.Files;
            if (oFiles.Count > 0)
            {
                for (var i = 0; i <= oFiles.Count - 1; i++)
                {
                    var fileName = oFiles[i].FileName;
                    var idx = fileName.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase);
                    if (idx > -1) fileName = fileName.Substring(idx);
                    if (!string.IsNullOrWhiteSpace(oFiles[i].FileName))
                    {
                        // Save to Database
                        var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(oFiles[i].InputStream.Length)) + 1];
                        oFiles[i].InputStream.Read(imageBytes, 0, imageBytes.Length);
                        bool bUpdate = false;
                        var newImageWeight = 0;

                        if (SepFunctions.toLong(Request.Item("ModuleID")) == 10)
                        {
                            if (!string.IsNullOrWhiteSpace(Request.Item("ContentID")))
                            {
                                fileName = Request.Item("ContentID") + Path.GetExtension(fileName);
                            }
                            else
                            {
                                fileName = SepFunctions.GetIdentity() + Path.GetExtension(fileName);
                            }

                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                if (SepFunctions.toLong(Request.Item("ContentID")) > 0)
                                {
                                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(Request.Item("ContentID")));
                                        cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(Request.Item("ModuleID")));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                bUpdate = true;
                                                File.Delete(SepFunctions.GetDirValue("downloads") + fileName);
                                            }

                                        }
                                    }
                                }
                            }

                            oFiles[i].SaveAs(SepFunctions.GetDirValue("downloads") + fileName);
                        }

                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            if (bUpdate)
                            {
                                using (var cmd = new SqlCommand("UPDATE Uploads SET FileName=@FileName, FileSize=@FileSize, ContentType=@ContentType, FileData=@FileData WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(Request.Item("ContentID")));
                                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(Request.Item("ModuleID")));
                                    cmd.Parameters.AddWithValue("@FileName", fileName);
                                    cmd.Parameters.AddWithValue("@FileSize", oFiles[i].ContentLength);
                                    cmd.Parameters.AddWithValue("@ContentType", Strings.LCase(oFiles[i].ContentType));
                                    if (SepFunctions.toLong(Request.Item("ModuleID")) == 10)
                                    {
                                        dynamic binaryNull = cmd.Parameters.Add("@FileData", SqlDbType.VarBinary, -1);
                                        binaryNull.Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@FileData", imageBytes);
                                    }

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID AND UserID=@UserID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(Request.Item("ModuleID")));
                                    cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(Request.Item("ContentID")));
                                    cmd.Parameters.AddWithValue("@UserID", Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) ? SepFunctions.Session_User_ID() : Request.Item("UserID")));
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            newImageWeight = 99;
                                        }
                                        else
                                        {
                                            newImageWeight = 1;
                                        }

                                    }
                                }

                                using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, PortalID, FileData, ControlID, UserRates, TotalRates, Weight) VALUES(@UploadID, @UniqueID, @UserID, @ModuleID, @FileName, @FileSize, @ContentType, @isTemp, @Approved, @DatePosted, @PortalID, @FileData, @ControlID, '0', '0', @Weight)", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UploadID", SepFunctions.toLong(Request.Item("Identity")));
                                    cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(Request.Item("ContentID")));
                                    cmd.Parameters.AddWithValue("@UserID", Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) ? SepFunctions.Session_User_ID() : Request.Item("UserID")));
                                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(Request.Item("ModuleID")));
                                    cmd.Parameters.AddWithValue("@FileName", fileName);
                                    cmd.Parameters.AddWithValue("@FileSize", oFiles[i].ContentLength);
                                    cmd.Parameters.AddWithValue("@ContentType", Strings.LCase(oFiles[i].ContentType));
                                    cmd.Parameters.AddWithValue("@isTemp", true);
                                    cmd.Parameters.AddWithValue("@Approved", true);
                                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                    if (SepFunctions.toLong(Request.Item("ModuleID")) == 10)
                                    {
                                        dynamic binaryNull = cmd.Parameters.Add("@FileData", SqlDbType.VarBinary, -1);
                                        binaryNull.Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@FileData", imageBytes);
                                    }

                                    cmd.Parameters.AddWithValue("@ControlID", Request.Item("ControlID"));
                                    cmd.Parameters.AddWithValue("@Weight", newImageWeight);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        if (Strings.Right(fileName, 4) == ".avi" || Strings.Right(fileName, 5) == ".mpeg" || Strings.Right(fileName, 4) == ".wmv" || Strings.Right(fileName, 4) == ".mpg" || Strings.Right(fileName, 4) == ".asf" || Strings.Right(fileName, 4) == ".mov" || Strings.Right(fileName, 5) == ".movi" || Strings.Right(fileName, 4) == ".swf") Video_to_FLV(fileName);
                    }
                }

                context.Response.Write("Succeed");
            }
            else
            {
                context.Response.Write("Error");
            }
        }

        /// <summary>
        /// Videoes to FLV.
        /// </summary>
        /// <param name="sFileName">Name of the s file.</param>
        public void Video_to_FLV(string sFileName)
        {
            if (Strings.LCase(Strings.Right(sFileName, 4)) == ".mp4")
                return;

            var _mhandler = new MediaHandler();

            var RootPath = SepFunctions.GetDirValue("downloads");

            _mhandler.FFMPEGPath = HostingEnvironment.MapPath("~\\ffmpeg\\bin\\ffmpeg.exe");
            _mhandler.InputPath = RootPath;
            _mhandler.OutputPath = RootPath;
            _mhandler.FileName = sFileName;
            _mhandler.OutputFileName = Strings.Replace(sFileName, Path.GetExtension(sFileName), string.Empty);
            _mhandler.OutputExtension = ".mp4";
            _mhandler.Force = "mp4";
            _mhandler.Video_Bitrate = 500;
            _mhandler.Channel = 2;
            _mhandler.Audio_SamplingRate = 48000;
            _mhandler.Audio_Bitrate = 192;
            _mhandler.FrameRate = 25;

            _mhandler.Process();

            // Watermark
            // ****************************************
            // For posting watermark on video ffmpeg must support vhook option
            // Download ffmpeg that support vhook from http://www.mediasoftpro.com/downloads/shared_ffmpeg.zip
            // *****************************************
            if (File.Exists(SepFunctions.GetDirValue("skins") + "public\\images\\flv_watermark.gif"))
            {
                _mhandler.WaterMarkPath = SepFunctions.GetDirValue("skins") + "public\\images";
                _mhandler.WaterMarkImage = "flv_watermark.gif";
            }

            var info = _mhandler.Encode_FLV();
            if (info.ErrorCode > 0 && info.ErrorCode != 121)
            {
                if (File.Exists(RootPath + Strings.Replace(sFileName, Path.GetExtension(sFileName), ".mp4"))) File.Delete(RootPath + Strings.Replace(sFileName, Path.GetExtension(sFileName), ".mp4"));
                return;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Uploads SET FileName=@FileName WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(Request.Item("ContentID")));
                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(Request.Item("ModuleID")));
                    cmd.Parameters.AddWithValue("@FileName", Strings.Replace(sFileName, Path.GetExtension(sFileName), ".mp4"));
                    cmd.ExecuteNonQuery();
                }
            }

            if (File.Exists(RootPath + sFileName)) File.Delete(RootPath + sFileName);
        }
    }
}