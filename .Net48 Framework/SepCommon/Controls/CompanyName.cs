// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CompanyName.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Data.SqlClient;

    /// <summary>
    /// Class CompanyName.
    /// </summary>
    public class CompanyName
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            var companyName = string.Empty;
            var iPortalID = SepFunctions.Get_Portal_ID();

            if (SepFunctions.isUserPage())
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SiteName FROM UPagesSites WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                companyName = SepFunctions.openNull(RS["SiteName"]);
                            }
                        }
                    }
                }
            }
            else
            {
                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")) > 0)
                {
                    iPortalID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"));
                }

                if (iPortalID == 0)
                {
                    companyName = SepFunctions.Setup(991, "CompanyName");
                }
                else
                {
                    companyName = SepFunctions.PortalSetup("COMPANYNAME", iPortalID);
                }

                if (string.IsNullOrWhiteSpace(companyName))
                {
                    companyName = SepFunctions.WebsiteName(iPortalID);
                }
            }

            return companyName;
        }
    }
}