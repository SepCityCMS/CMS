// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PortalDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class PortalDropdown.
    /// </summary>
    public class PortalDropdown
    {
        /// <summary>
        /// The m select width
        /// </summary>
        private string m_SelectWidth;

        /// <summary>
        /// The m show all portals
        /// </summary>
        private bool m_ShowAllPortals;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the width of the select.
        /// </summary>
        /// <value>The width of the select.</value>
        public string SelectWidth
        {
            get => SepCommon.SepCore.Strings.ToString(m_SelectWidth);

            set => m_SelectWidth = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show all portals].
        /// </summary>
        /// <value><c>true</c> if [show all portals]; otherwise, <c>false</c>.</value>
        public bool ShowAllPortals
        {
            get
            {
                var s = Convert.ToBoolean(m_ShowAllPortals);
                return s;
            }

            set => m_ShowAllPortals = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                var s = SepCommon.SepCore.Request.Item(ID);
                if (s == null)
                {
                    s = SepCommon.SepCore.Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable")
            {
                return output.ToString();
            }

            var sInstallFolder = SepFunctions.GetInstallFolder(true);

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                output.AppendLine("<script src=\"https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.5/js/select2.min.js\" type=\"text/javascript\"></script>");
                output.AppendLine("<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.5/css/select2.min.css\">");
                output.AppendLine("<link rel=\"stylesheet\" href=\"" + sInstallFolder + "skins/public/styles/select2-bootstrap.css\">");

                output.AppendLine("<script type=\"text/javascript\">");
                output.AppendLine("$(document).ready(function() {");
                output.AppendLine("  $('#" + ID + "').select2({");
                output.AppendLine("    theme: 'bootstrap4',");
                output.AppendLine("    width: '100%'");
                output.AppendLine("  });");
                output.AppendLine("});");
                output.AppendLine("</script>");

                var sWidth = !string.IsNullOrWhiteSpace(SelectWidth) ? " style=\"width: " + SelectWidth + "\"" : string.Empty;

                output.AppendLine("<Select name=\"" + ID + "\" id=\"" + ID + "\"" + sWidth + " Class=\"" + CssClass + "\">");
                output.AppendLine("<option value=\"-1\">" + SepFunctions.LangText("All Portals") + "</option>");
                output.AppendLine("<option value=\"0\"" + SepCommon.SepCore.Strings.ToString("0" == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Main Portal") + "</option>");
                using (SqlCommand cmd = new SqlCommand("SELECT PortalID,PortalTitle FROM Portals WHERE Status <> -1 ORDER BY PortalTitle", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.AppendLine("<option value=\"" + SepFunctions.openNull(RS["PortalID"]) + "\"" + SepCommon.SepCore.Strings.ToString(SepFunctions.openNull(RS["PortalID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["PortalTitle"]) + "</option>");
                        }
                    }
                }

                output.AppendLine("</select>");
            }

            return output.ToString();
        }
    }
}