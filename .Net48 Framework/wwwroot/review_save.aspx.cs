// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="review_save.aspx.cs" company="SepCity, Inc.">
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
    /// Class review_save.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class review_save : Page
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
                if (SepFunctions.Check_Rating(SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")), SepCommon.SepCore.Request.Item("UniqueID"), SepCommon.SepCore.Request.Item("UserID")) == false)
                {
                    string sActDesc = SepFunctions.LangText("Review Successfully Saved") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Review: ~~" + SepFunctions.toInt(SepCommon.SepCore.Request.Item("Rating")) + "~~ stars") + Environment.NewLine;
                    SepFunctions.Activity_Write("RATING", sActDesc, SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")), SepCommon.SepCore.Request.Item("UniqueID"), SepCommon.SepCore.Request.Item("UserID"));

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("SELECT ReviewID FROM ReviewsUsers WHERE ReviewID=@ReviewID AND UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                            cmd.Parameters.AddWithValue("@ReviewID", SepCommon.SepCore.Request.Item("UniqueID"));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                    using (var cmd2 = new SqlCommand("UPDATE ReviewsUsers SET TotalStars=TotalStars+@TotalStars, TotalUsers=TotalUsers+1 WHERE ReviewID=@ReviewID AND UserID=@UserID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                                        cmd2.Parameters.AddWithValue("@TotalStars", SepCommon.SepCore.Request.Item("Rating"));
                                        cmd2.Parameters.AddWithValue("@ReviewID", SepCommon.SepCore.Request.Item("UniqueID"));
                                        cmd2.ExecuteNonQuery();
                                    }
                                else
                                    using (var cmd2 = new SqlCommand("INSERT INTO ReviewsUsers (UserID, TotalStars, TotalUsers, ReviewID) VALUES(@UserID, @TotalStars, @TotalUsers, @ReviewID)", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                                        cmd2.Parameters.AddWithValue("@TotalStars", SepCommon.SepCore.Request.Item("Rating"));
                                        cmd2.Parameters.AddWithValue("@TotalUsers", 1);
                                        cmd2.Parameters.AddWithValue("@ReviewID", SepCommon.SepCore.Request.Item("UniqueID"));
                                        cmd2.ExecuteNonQuery();
                                    }

                            }
                        }
                    }
                }

            Response.Write("Success");
        }
    }
}