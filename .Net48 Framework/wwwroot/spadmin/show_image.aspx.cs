// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="show_image.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web.UI;

    /// <summary>
    /// Class show_image.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class show_image : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The i height
        /// </summary>
        private decimal iHeight;

        /// <summary>
        /// The i width
        /// </summary>
        private decimal iWidth = 85;

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Images the load sizes.
        /// </summary>
        /// <param name="iModuleID">The i module identifier.</param>
        public void Image_Load_Sizes(int iModuleID)
        {
            var str = string.Empty;

            if (iModuleID == 40)
            {
                iWidth = 400;
                return;
            }

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "image_sizes.xml"))
                using (var objReader = new StreamReader(SepFunctions.GetDirValue("app_data") + "image_sizes.xml"))
                {
                    str = objReader.ReadToEnd();
                }
            else
                str = Image_Load_Sizes_Default();

            str = SepFunctions.ParseXML("MODULE" + iModuleID, str);

            if (SepFunctions.ParseXML("ENABLE", str) == "Yes")
            {
                iWidth = SepFunctions.toInt(SepFunctions.ParseXML("WIDTH", str));
                iHeight = SepFunctions.toInt(SepFunctions.ParseXML("HEIGHT", str));
            }
        }

        /// <summary>
        /// Images the load sizes default.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Image_Load_Sizes_Default()
        {
            var str = new StringBuilder();

            // User Profiles
            str.Append("<MODULE63>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>120</WIDTH>");
            str.Append("</MODULE63>");

            // Downloads
            str.Append("<MODULE10>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>220</WIDTH>");
            str.Append("</MODULE10>");

            // Real Estate
            str.Append("<MODULE32>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE32>");

            // Shopping Mall
            str.Append("<MODULE41>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>150</WIDTH>");
            str.Append("</MODULE41>");

            // Match Maker
            str.Append("<MODULE18>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE18>");

            // News
            str.Append("<MODULE23>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE23>");

            // Articles
            str.Append("<MODULE35>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>100</WIDTH>");
            str.Append("</MODULE35>");

            // Auction
            str.Append("<MODULE31>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE31>");

            // Business Directory
            str.Append("<MODULE20>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE20>");

            // Classified Ads
            str.Append("<MODULE44>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE44>");

            // Event Calendar
            str.Append("<MODULE46>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE46>");

            // Photo Albums
            str.Append("<MODULE28>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>80</WIDTH>");
            str.Append("</MODULE28>");

            // Speaker's Bureau
            str.Append("<MODULE50>");
            str.Append("<ENABLE>Yes</ENABLE>");
            str.Append("<HEIGHT>0</HEIGHT>");
            str.Append("<WIDTH>150</WIDTH>");
            str.Append("</MODULE50>");

            return Strings.ToString(str);
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="imageData">The image data.</param>
        /// <param name="iModuleID">The i module identifier.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] Resize_Image(string FileName, byte[] imageData, int iModuleID)
        {
            // DO NOT MOVE VARIABLES (WILL CAUSE ERRORS!!!!!!)
            Image_Load_Sizes(iModuleID);
            var stream = new MemoryStream(imageData);

            var objImage = Image.FromStream(stream, true, true);
            using (var bm = new Bitmap(stream))
            {
                byte[] fileData = null;

                if (iWidth == 0)
                {
                    decimal iHeightPercent = iHeight / objImage.Height * 100;
                    iWidth = objImage.Width * iHeightPercent / 100;
                }

                if (iHeight == 0)
                {
                    decimal iWidthPercent = iWidth / objImage.Width * 100;
                    iHeight = objImage.Height * iWidthPercent / 100;
                }

                using (var thumb = new Bitmap(objImage, Convert.ToInt16(iWidth), Convert.ToInt16(iHeight)))
                {
                    var g = Graphics.FromImage(thumb);

                    if (iWidth > 0 || iHeight > 0)
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.FillRectangle(Brushes.AliceBlue, 0, 0, Convert.ToInt16(iWidth), Convert.ToInt16(iHeight));
                        g.DrawImage(bm, new Rectangle(0, 0, Convert.ToInt16(iWidth), Convert.ToInt16(iHeight)), new Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel);
                        g.Dispose();

                        switch (Strings.LCase(Path.GetExtension(FileName)))
                        {
                            case ".gif":
                                fileData = BmpToBytes_MemStream(thumb, ImageFormat.Gif);
                                break;

                            case ".jpg":
                            case ".jpeg":
                                fileData = BmpToBytes_MemStream(thumb, ImageFormat.Jpeg);
                                break;

                            case ".png":
                                fileData = BmpToBytes_MemStream(thumb, ImageFormat.Png);
                                break;

                            default:
                                fileData = null;
                                break;
                        }
                    }
                }

                return fileData;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            // Connect to the database and bring back the image contents & MIME type for the specified picture
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                switch (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")))
                {
                    case 2:
                        using (var cmd = new SqlCommand("SELECT ImageType, ImageData FROM Advertisements WHERE [AdID]=@AdID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", Request.QueryString["AdID"]);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Response.ContentType = dr["ImageType"].ToString();
                                    var bits = (byte[])dr["ImageData"];
                                    Response.BinaryWrite(bits);
                                }
                            }
                        }

                        break;

                    case 37:
                        using (var cmd = new SqlCommand("SELECT ContentType, FileData FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=37", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", Request.QueryString["UniqueID"]);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Response.ContentType = dr["ContentType"].ToString();
                                    var bits = (byte[])dr["FileData"];
                                    Response.BinaryWrite(bits);
                                }
                                else
                                {
                                    Response.ContentType = "image/png";
                                    var bits = File.ReadAllBytes(SepFunctions.GetDirValue("images") + "\\public\\no-photo.jpg");
                                    Response.BinaryWrite(bits);
                                }
                            }
                        }

                        break;

                    case 39:
                        using (var cmd = new SqlCommand("SELECT ContentType, FileData FROM Uploads WHERE [UploadID]=@ImageID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ImageID", Request.QueryString["ImageID"]);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Response.ContentType = dr["ContentType"].ToString();
                                    var bits = (byte[])dr["FileData"];
                                    Response.BinaryWrite(bits);
                                }
                            }
                        }

                        break;

                    default:
                        if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("CatID")))
                        {
                            try
                            {
                                using (var cmd = new SqlCommand("SELECT ImageType, ImageData FROM Categories WHERE [CatID]=@CatID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@CatID", Request.QueryString["CatID"]);
                                    using (SqlDataReader dr = cmd.ExecuteReader())
                                    {
                                        if (dr.Read())
                                        {
                                            Response.ContentType = dr["ImageType"].ToString();
                                            var bits = (byte[])dr["ImageData"];
                                            Response.BinaryWrite(bits);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                try
                                {
                                    var bits = File.ReadAllBytes(SepFunctions.GetDirValue("images") + "\\public\\no-photo.jpg");
                                    Response.BinaryWrite(bits);
                                }
                                catch
                                {
                                    var bits = SepFunctions.StringToBytes("Error");
                                    Response.BinaryWrite(bits);
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(Request.QueryString["UploadID"]))
                                try
                                {
                                    using (var cmd = new SqlCommand("SELECT FileName, ContentType, FileData FROM Uploads WHERE [UploadID]=@UploadID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UploadID", Request.QueryString["UploadID"]);
                                        using (SqlDataReader dr = cmd.ExecuteReader())
                                        {
                                            if (dr.Read())
                                                switch (Strings.LCase(Request.QueryString["Size"]))
                                                {
                                                    case "thumb":
                                                        Response.Clear();
                                                        switch (Strings.LCase(Path.GetExtension(SepFunctions.openNull(dr["FileName"]))))
                                                        {
                                                            case ".gif":
                                                                Response.ContentType = "image/gif";
                                                                break;

                                                            case ".jpg":
                                                            case ".jpeg":
                                                                Response.ContentType = "image/jpeg";
                                                                break;

                                                            case ".png":
                                                                Response.ContentType = "image/png";
                                                                break;
                                                        }

                                                        try
                                                        {
                                                            // Downloads Module
                                                            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) == 10)
                                                            {
                                                                var bytes = File.ReadAllBytes(SepFunctions.GetDirValue("downloads") + SepFunctions.openNull(dr["FileName"]));
                                                                var bits = Resize_Image(SepFunctions.openNull(dr["FileName"]), bytes, SepFunctions.toInt(Request.QueryString["ModuleID"]));
                                                                Response.BinaryWrite(bits);
                                                            }
                                                            else
                                                            {
                                                                var bits = Resize_Image(SepFunctions.openNull(dr["FileName"]), (byte[])dr["FileData"], SepFunctions.toInt(Request.QueryString["ModuleID"]));
                                                                Response.BinaryWrite(bits);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                var bits = File.ReadAllBytes(SepFunctions.GetDirValue("images") + "\\public\\no-photo.jpg");
                                                                Response.BinaryWrite(bits);
                                                            }
                                                            catch
                                                            {
                                                                var bits = SepFunctions.StringToBytes("Error");
                                                                Response.BinaryWrite(bits);
                                                            }
                                                        }

                                                        Response.End();
                                                        break;

                                                    default:
                                                        try
                                                        {
                                                            if (!string.IsNullOrWhiteSpace(dr["ContentType"].ToString()))
                                                            {
                                                                Response.ContentType = dr["ContentType"].ToString();
                                                            }
                                                            else
                                                            {
                                                                Response.ContentType = "image/png";
                                                            }

                                                            var bits = (byte[])dr["FileData"];
                                                            Response.BinaryWrite(bits);
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                Response.ContentType = "image/png";
                                                                var bits = File.ReadAllBytes(SepFunctions.GetDirValue("images") + "\\public\\no-photo.jpg");
                                                                Response.BinaryWrite(bits);
                                                            }
                                                            catch
                                                            {
                                                                Response.ContentType = "image/png";
                                                                var bits = SepFunctions.StringToBytes("Error");
                                                                Response.BinaryWrite(bits);
                                                            }
                                                        }

                                                        break;
                                                }
                                        }
                                    }
                                }
                                catch
                                {
                                    Response.ContentType = "image/png";
                                    var bits = SepFunctions.StringToBytes("Error");
                                    Response.BinaryWrite(bits);
                                }
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// BMPs to bytes memory stream.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>System.Byte[].</returns>
        private byte[] BmpToBytes_MemStream(Bitmap bmp, ImageFormat imageFormat)
        {
            using (var ms = new MemoryStream())
            {
                // Save to memory using the Jpeg format
                bmp.Save(ms, imageFormat);

                // read to end
                var bmpBytes = ms.GetBuffer();
                bmp.Dispose();

                return bmpBytes;
            }
        }
    }
}