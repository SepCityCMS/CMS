// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="FriendList.cs" company="SepCity, Inc.">
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
    /// Class FriendList.
    /// </summary>
    public class FriendList
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(33, "FriendsEnable") != "Enable")
            {
                return output.ToString();
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 FL.AddedUserID,M.Username FROM FriendsList AS FL,Members AS M WHERE M.UserID=FL.AddedUserID AND FL.UserID='" + SepFunctions.Session_User_ID() + "' ORDER BY M.Username", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                output.AppendLine("<a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.openNull(RS["AddedUserID"]) + "\">" + SepFunctions.openNull(RS["Username"]) + "</a><br/>");
                            }
                        }
                        else
                        {
                            output.Append(SepFunctions.LangText("No Friends Added"));
                        }
                    }
                }
            }

            output.AppendLine("<center>... <a href=\"" + sInstallFolder + "account.aspx\">" + SepFunctions.LangText("Manage List") + "</a></center>");

            return output.ToString();
        }
    }
}