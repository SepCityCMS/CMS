// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="twilio_voice.aspx.cs" company="SepCity, Inc.">
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
    /// Class twilio_voice.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class twilio_voice : Page
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
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
            {
                Context.Response.Clear();
                Context.Response.AddHeader("ContentType", "text/xml");

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE Members SET UserPoints=UserPoints-1 WHERE UserID=@UserID AND Status=1", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                        cmd.ExecuteNonQuery();
                    }
                }

                SepFunctions.Activity_Write("CALL", "Call Made to " + SepCommon.SepCore.Request.Item("ToNumber"), 64, SepCommon.SepCore.Strings.ToString(SepFunctions.GetIdentity()), SepCommon.SepCore.Request.Item("UserID"));

                SepCommon.SepCore.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
                SepCommon.SepCore.Response.Write("<Response>" + Environment.NewLine);
                SepCommon.SepCore.Response.Write("<Dial>" + SepCommon.SepCore.Request.Item("ToNumber") + "</Dial>" + Environment.NewLine);
                SepCommon.SepCore.Response.Write("</Response>" + Environment.NewLine);

                Context.ApplicationInstance.CompleteRequest();
                Context.Response.End();
            }
        }
    }
}