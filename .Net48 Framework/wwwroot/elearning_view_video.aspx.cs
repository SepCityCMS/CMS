// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_view_video.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;

    /// <summary>
    /// Class elearning_view_video.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_view_video : Page
    {
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
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FileName, ContentType, FileData FROM Uploads WHERE [UploadID]=@UploadID", conn))
                {
                    cmd.Parameters.AddWithValue("@UploadID", Request.QueryString["UploadID"]);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            try
                            {
                                Response.ContentType = dr["ContentType"].ToString();
                                Response.BinaryWrite((byte[])dr["FileData"]);
                            }
                            catch
                            {
                                try
                                {
                                    var data = File.ReadAllBytes(SepFunctions.GetDirValue("images") + "\\public\\no-photo.jpg");
                                    Response.BinaryWrite(data);
                                }
                                catch
                                {
                                    Response.BinaryWrite(SepFunctions.StringToBytes("Error"));
                                }
                            }
                    }
                }
            }
        }
    }
}