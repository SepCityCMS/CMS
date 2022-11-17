// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="default_tips.aspx.cs" company="SepCity, Inc.">
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
    /// Class default_tips.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class default_tips : Page
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
            Response.Clear();
            if (SepFunctions.isProfessionalEdition())
                try
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("UPDATE Members SET HideTips='1' WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            cmd.ExecuteNonQuery();
                        }
                    }

                    Response.Write(SepFunctions.LangText("You will not see these tips any more."));
                }
                catch
                {
                    Response.Write(SepFunctions.LangText("There has been an error saving."));
                }
            else Response.Write(SepFunctions.LangText("You must purchase the enterprise version of SepCity to hide these tips."));

            Response.End();
        }
    }
}