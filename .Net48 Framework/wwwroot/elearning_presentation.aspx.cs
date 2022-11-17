// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_presentation.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;

    /// <summary>
    /// Class elearning_presentation.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_presentation : Page
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
            if (SepCommon.SepCore.Request.Item("DoAction") == "ShowPresentation")
            {
                Response.Clear();
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
                                    Context.Response.Clear();
                                    Context.Response.Buffer = true;
                                    Context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + dr["FileName"]);
                                    Context.Response.ContentType = dr["ContentType"].ToString();
                                    Context.Response.BinaryWrite((byte[])dr["FileData"]);
                                    Context.Response.End();
                                }
                                catch
                                {
                                    Response.BinaryWrite(SepFunctions.StringToBytes("Error"));
                                }
                        }
                    }
                }

                Response.End();
                return;
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            ViewPresentation.src = sInstallFolder + "elearning_presentation.aspx?DoAction=ShowPresentation&UploadID=" + SepCommon.SepCore.Request.Item("UploadID");
        }
    }
}