// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UnreadMessages.cs" company="SepCity, Inc.">
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
    /// Class UnreadMessages.
    /// </summary>
    public class UnreadMessages
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(17, "MessengerEnable") != "Enable")
            {
                return output.ToString();
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("MessengerAccess"), true) == false)
            {
                return output.ToString();
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()))
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ToUserID FROM Messenger WHERE ToUserID='" + SepFunctions.Session_User_ID() + "' AND ReadNew='0'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                output.AppendLine("<a href=\"" + sInstallFolder + "messenger.aspx\">" + SepFunctions.LangText("You have new message(s)") + "</a>");
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}