// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CompanySlogan.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class CompanySlogan.
    /// </summary>
    public class CompanySlogan
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.isUserPage())
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SiteSlogan FROM UPagesSites WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCommon.Core.SepCore.Request.Item("UserName")));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                output.Append(SepFunctions.openNull(RS["SiteSlogan"]));
                            }
                        }
                    }
                }
            }
            else
            {
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    output.Append(SepFunctions.Setup(991, "CompanySlogan"));
                }
                else
                {
                    output.Append(SepFunctions.PortalSetup("CompanySlogan", SepFunctions.Get_Portal_ID()));
                }
            }

            return output.ToString();
        }
    }
}