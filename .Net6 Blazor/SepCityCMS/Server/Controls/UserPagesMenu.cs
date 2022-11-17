// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UserPagesMenu.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class UserPagesMenu.
    /// </summary>
    public class UserPagesMenu
    {
        /// <summary>
        /// The m menu identifier
        /// </summary>
        private int m_MenuID;

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public int MenuID
        {
            get
            {
                var s = Convert.ToInt32(m_MenuID);
                return s;
            }

            set => m_MenuID = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (MenuID == 0)
            {
                output.AppendLine("MenuID is Required");
                return output.ToString();
            }

            var str = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu" + MenuID)) && SepFunctions.isUserPage())
                {
                    str.AppendLine("<div class=\"row\">");
                    str.AppendLine("<ul class=\"sb_menu sb_menu7" + MenuID + "\" id=\"" + ID + "\">");
                    using (SqlCommand cmd = new SqlCommand("SELECT PageID,PageName,PageTitle FROM UPagesPages WHERE MenuID=7" + MenuID + " AND UserID='" + SepFunctions.GetUserID(SepCore.Request.Item("UserName")) + "' ORDER BY Weight,PageName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                str.Append("<li><a href=\"" + sInstallFolder + SepFunctions.openNull(RS["PageName"]) + "\">" + SepFunctions.openNull(RS["PageTitle"]) + "</a></li>");
                            }
                        }
                    }

                    str.AppendLine("</ul>");
                    str.AppendLine("</div>");
                }
            }

            output.Append(SepCore.Strings.ToString(str));

            return output.ToString();
        }
    }
}