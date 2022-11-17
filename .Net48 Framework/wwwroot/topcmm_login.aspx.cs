// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="topcmm_login.aspx.cs" company="SepCity, Inc.">
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
    /// Class topcmm_login.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class topcmm_login : Page
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
            var result = 3;

            var LOGIN_SUCCESS = 0;
            var LOGIN_PASSWD_ERROR = 1;
            var LOGIN_ERROR = 3;
            var LOGIN_ERROR_NOUSERID = 4;
            var LOGIN_SUCCESS_ADMIN = 5;

            Response.Clear();

            if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("username")))
            {
                Response.Write(LOGIN_ERROR);
                Response.End();
                return;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT Password, AccessClass FROM Members WHERE UserName=@UserName", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", SepCommon.SepCore.Request.Item("username"));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            if (SepFunctions.openNull(RS["Password"]) == SepCommon.SepCore.Request.Item("password"))
                            {
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["AccessClass"])) == 2)
                                    result = LOGIN_SUCCESS_ADMIN;
                                else
                                    result = LOGIN_SUCCESS;
                            }
                            else
                            {
                                result = LOGIN_PASSWD_ERROR;
                            }
                        }
                        else
                        {
                            result = LOGIN_ERROR_NOUSERID;
                        }

                    }
                }
            }

            Response.Write(result);

            Response.End();
        }
    }
}