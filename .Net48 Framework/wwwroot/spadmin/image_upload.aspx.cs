// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="image_upload.aspx.cs" company="SepCity, Inc.">
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
    using System.Web;
    using System.Web.UI;

    /// <summary>
    /// Class image_upload.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class image_upload : Page
    {
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
        /// The c common
        /// </summary>
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
            Context.Server.ScriptTimeout = 999999999;

            if (Context.Request.Files.Count > 0)
                for (var i = 0; i <= Context.Request.Files.Count - 1; i++)
                {
                    // ERROR: Not supported in C#: OnErrorStatement
                    HttpPostedFile File = Context.Request.Files.Get(i);
                    if (!string.IsNullOrWhiteSpace(File.FileName))
                        if (Strings.LCase(File.ContentType) == "image/pjpeg" || Strings.LCase(File.ContentType) == "image/gif" || Strings.LCase(File.ContentType) == "image/jpeg" || Strings.LCase(File.ContentType) == "image/jpg" || Strings.LCase(File.ContentType) == "image/png" || Strings.LCase(File.ContentType) == "image/x-png" || Strings.LCase(File.ContentType) == "application/octet-stream")
                        {
                            // Save to Database
                            var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(File.InputStream.Length)) + 1];
                            File.InputStream.Read(imageBytes, 0, imageBytes.Length);

                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, PortalID, FileData) VALUES(@UploadID, @UniqueID, @UserID, @ModuleID, @FileName, @FileSize, @ContentType, @isTemp, @Approved, @DatePosted, @PortalID, @FileData)", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UploadID", SepFunctions.toLong(SepCommon.SepCore.Request.Item("Identity")));
                                    cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.toLong(SepCommon.SepCore.Request.Item("ContentID")));
                                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")));
                                    cmd.Parameters.AddWithValue("@FileName", File.FileName);
                                    cmd.Parameters.AddWithValue("@FileSize", File.ContentLength);
                                    cmd.Parameters.AddWithValue("@ContentType", Strings.LCase(File.ContentType));
                                    cmd.Parameters.AddWithValue("@isTemp", true);
                                    cmd.Parameters.AddWithValue("@Approved", true);
                                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                    cmd.Parameters.AddWithValue("@FileData", imageBytes);
                                    cmd.ExecuteNonQuery();
                                }

                                using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE isTemp='1' AND DatePosted > DateAdd(Day, 7, GetDate())", conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                }
        }
    }
}