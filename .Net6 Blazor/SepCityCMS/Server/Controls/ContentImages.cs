// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ContentImages.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Class ContentImages.
    /// </summary>
    public class ContentImages
    {
        /// <summary>
        /// The m content unique identifier
        /// </summary>
        private string m_ContentUniqueID;

        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

        /// <summary>
        /// Gets or sets the content unique identifier.
        /// </summary>
        /// <value>The content unique identifier.</value>
        public string ContentUniqueID
        {
            get => SepCore.Strings.ToString(m_ContentUniqueID);

            set => m_ContentUniqueID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => SepCore.Strings.ToString(m_UserID);

            set => m_UserID = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sImageFolder = SepFunctions.GetInstallFolder(true);
            var cUrl = sImageFolder + "js/jquery/jquery.prettyPhoto.js";

            output.AppendLine("<script type=\"text/javascript\" src=\"" + cUrl + "\">");

            long nextRow = 0;

            var wc = string.Empty;

            if (!string.IsNullOrWhiteSpace(ContentUniqueID))
            {
                wc += " AND Uploads.UniqueID='" + SepFunctions.FixWord(ContentUniqueID) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wc += " AND Uploads.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            output.AppendLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + sImageFolder + "skins/public/styles/prettyPhoto.css\" />");

            output.AppendLine("<script type=\"text/javascript\" charset=\"utf-8\">");
            output.AppendLine("$(document).ready(function () {");
            output.AppendLine("$(\"a[rel^='prettyPhoto']\").prettyPhoto({");
            output.AppendLine("animationSpeed:  'normal', /* fast/slow/normal */");
            output.AppendLine("opacity: 0.80, /* Value between 0 and 1 */");
            output.AppendLine("showTitle: false /* true/false */");
            output.AppendLine("});");
            output.AppendLine("});");
            output.AppendLine("</script>");

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE Uploads.ModuleID=@ModuleID AND Uploads.isTemp='0' AND Uploads.Approved='1' AND (Right(FileName, 4) = '.png' OR Right(FileName, 4) = '.jpg' OR Right(FileName, 4) = '.gif')" + wc + " ORDER BY Weight ASC,DatePosted DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                nextRow = nextRow + 1;
                                output.AppendLine("<span style=\"padding:5px;\"><a href=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "\" rel=\"prettyPhoto[pp_gal]\"><img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&Size=thumb\" border=\"0\" alt=\"\" /></a></span>");
                                if (nextRow == 2 && ModuleID != 28)
                                {
                                    output.AppendLine("<br/>");
                                    nextRow = 0;
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