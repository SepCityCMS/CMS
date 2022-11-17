// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="WhosOnline.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class WhosOnline.
    /// </summary>
    public class WhosOnline
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.CompareKeys(SepFunctions.Security("WhosOnline"), true) == false)
            {
                return output.ToString();
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();
            using (var ds = new DataSet())
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT O.UserID,M.Username,M.Male,O.Location FROM OnlineUsers AS O,Members AS M WHERE M.UserID=O.UserID AND O.Location <> 'Logout' AND (O.CurrentStatus <> 'DELETED' OR O.CurrentStatus IS NULL) AND M.PortalID=@PortalID ORDER BY M.UserName", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();

                        SqlDataAdapter da;
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i || i == 9)
                    {
                        break;
                    }

                    output.AppendLine("<div class=\"col-lg-12\"><a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]) + "\">" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]) + "</a></div>");
                }
            }

            output.AppendLine("<div class=\"col-lg-12\">... <a href=\"" + sInstallFolder + "whos_online.aspx\">" + SepFunctions.LangText("Detailed List") + "</a></div>");

            return output.ToString();
        }
    }
}