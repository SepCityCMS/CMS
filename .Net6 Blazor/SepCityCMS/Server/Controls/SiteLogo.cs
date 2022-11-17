// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SiteLogo.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class SiteLogo.
    /// </summary>
    public class SiteLogo
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
                    using (SqlCommand cmd = new SqlCommand("SELECT FileData FROM Uploads WHERE ModuleID='7' AND UserID=@UserID AND isTemp='0'", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCore.Request.Item("UserName")));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                output.AppendLine("<img src=\"data:image/png;base64," + SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"])) + "\" alt=\"Website Logo\" border=\"0\" />");
                            }
                        }
                    }
                }
            }
            else
            {
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "SiteLogo")))
                    {
                        output.AppendLine("<img src=\"" + "data:image/png;base64," + SepFunctions.Setup(993, "SiteLogo") + "\" alt=\"Website Logo\" border=\"0\" />");
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT FileData FROM Uploads WHERE ModuleID='60' AND UniqueID=@UniqueID AND PortalID=@PortalID AND isTemp='0'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.Get_Portal_ID());
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    output.AppendLine("<img src=\"data:image/png;base64," + SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"])) + "\" alt=\"Website Logo\" border=\"0\" />");
                                }
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}