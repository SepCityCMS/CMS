// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="rating_save.aspx.cs" company="SepCity, Inc.">
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
    /// Class rating_save.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class rating_save : Page
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
            if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("Rating")) > 0)
            {
                if (SepFunctions.Rating_Check(SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")), SepCommon.SepCore.Request.Item("UniqueID")) == false)
                {
                    string sActDesc = SepFunctions.LangText("Rating Successfully Saved") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Rated: ~~" + SepFunctions.toInt(SepCommon.SepCore.Request.Item("Rating")) + "~~ stars") + Environment.NewLine;
                    SepFunctions.Activity_Write("ADDRATING", sActDesc, SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")));

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("INSERT INTO Ratings (ModuleID, UniqueID, IPAddress, UserID, Stars, DatePosted) VALUES(@ModuleID, @UniqueID, @IPAddress, @UserID, @Stars, @DatePosted)", conn))
                        {
                            cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")));
                            cmd.Parameters.AddWithValue("@UniqueID", SepCommon.SepCore.Request.Item("UniqueID"));
                            cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            cmd.Parameters.AddWithValue("@Stars", SepFunctions.toInt(SepCommon.SepCore.Request.Item("Rating")));
                            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    Response.Write(SepFunctions.LangText("Rating has been successfully completed."));
                }
                else
                {
                    Response.Write(SepFunctions.LangText("You have already rated this content."));
                }
            }
            else
            {
                Response.Write(SepFunctions.LangText("Invalid Request."));
            }
        }
    }
}