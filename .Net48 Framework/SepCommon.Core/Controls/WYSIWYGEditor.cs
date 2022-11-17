// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="WYSIWYGEditor.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class WYSIWYGEditor.
    /// </summary>
    public class WYSIWYGEditor
    {
        /// <summary>
        /// The m mode
        /// </summary>
        private string m_Mode;

        /// <summary>
        /// The m relative ur ls
        /// </summary>
        private bool m_RelativeURLs;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public string Mode
        {
            get => SepCommon.Core.SepCore.Strings.ToString(m_Mode);

            set => m_Mode = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [relative ur ls].
        /// </summary>
        /// <value><c>true</c> if [relative ur ls]; otherwise, <c>false</c>.</value>
        public bool RelativeURLs
        {
            get
            {
                var s = Convert.ToBoolean(m_RelativeURLs);
                return s;
            }

            set => m_RelativeURLs = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                var s = SepCommon.Core.SepCore.Request.Form("txt" + ID);
                if (s == null)
                {
                    s = SepCommon.Core.SepCore.Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var tinyMCEurl = "https://cdnjs.cloudflare.com/ajax/libs/tinymce/5.0.14/tinymce.min.js";

            output.AppendLine("<script type=\"text/javascript\" src=\"" + tinyMCEurl + "\">");

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var sCSSFiles = string.Empty;
            var sFolderName = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE useTemplate='1'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sFolderName = SepFunctions.openNull(RS["FolderName"]);
                            }
                        }
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", SepFunctions.PortalSetup("WEBSITELAYOUT", SepFunctions.Get_Portal_ID()));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sFolderName = SepFunctions.openNull(RS["FolderName"]);
                            }
                        }
                    }
                }

                sCSSFiles = "https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css";
                if (File.Exists(SepFunctions.GetDirValue("skins") + sFolderName + "\\styles\\colors.css"))
                {
                    sCSSFiles += "," + sImageFolder + "skins/" + sFolderName + "/styles/colors.css";
                }

                if (File.Exists(SepFunctions.GetDirValue("skins") + sFolderName + "\\styles\\layout.css"))
                {
                    sCSSFiles += "," + sImageFolder + "skins/" + sFolderName + "/styles/layout.css";
                }
            }

            output.AppendLine("<textarea name=\"txt" + ID + "\" id=\"txt" + ID + "\" style=\"width: " + Width + ";\">" + Text + "</textarea>");

            output.AppendLine("<script type=\"text/javascript\">");
            output.AppendLine("tinymce.init({");
            output.AppendLine("selector: 'textarea#txt" + ID + "',");
            output.AppendLine("plugins: [");
            output.AppendLine("\"autoresize advlist autolink autosave link sepimage" + SepCommon.Core.SepCore.Strings.ToString(Mode == "advanced" ? " sepinsert" : string.Empty) + SepCommon.Core.SepCore.Strings.ToString(Mode == "userpage" ? " sepinsert2" : string.Empty) + " lists charmap\",");
            output.AppendLine("\"searchreplace wordcount code fullscreen media nonbreaking\",");
            output.AppendLine("\"table directionality template textcolor paste textcolor preview\"");
            output.AppendLine("],");

            if (SepCommon.Core.SepCore.Request.Browser.IsMobileDevice())
            {
                output.AppendLine("toolbar1: \"bold italic underline alignleft aligncenter alignright alignjustify\",");
                output.AppendLine("toolbar2: \"fontselect fontsizeselect link\",");
                output.AppendLine("toolbar3: \"forecolor backcolor numlist bullist code\",");
            }
            else
            {
                output.AppendLine("toolbar1: \"cut copy paste | undo redo | searchreplace | fontselect fontsizeselect | indent outdent link charmap" + SepCommon.Core.SepCore.Strings.ToString(Mode == "advanced" ? " sepinsert" : string.Empty) + SepCommon.Core.SepCore.Strings.ToString(Mode == "userpage" ? " sepinsert2" : string.Empty) + "\",");
                output.AppendLine("toolbar2: \"bold italic underline | forecolor backcolor | alignleft aligncenter alignright alignjustify | numlist bullist | preview code | sepimage media | table\",");
            }

            output.AppendLine("external_filemanager_path: '" + sImageFolder + "spadmin/ImageManager/default.aspx?RelativeURLs=" + RelativeURLs + "',");

            output.AppendLine("menubar: false,");
            output.AppendLine("autoresize_min_height: '" + Height + "',");
            output.AppendLine("relative_urls: false,");
            output.AppendLine("convert_urls: false,");
            output.AppendLine("remove_script_host: false,");
            output.AppendLine("cleanup: false,");
            output.AppendLine("statusbar: false,");
            output.AppendLine("cleanup_on_startup: false,");
            output.AppendLine("trim_span_elements: false,");
            output.AppendLine("verify_html: false,");
            if (!string.IsNullOrWhiteSpace(sCSSFiles))
            {
                output.AppendLine("content_css: '" + sCSSFiles + "',");
                output.AppendLine("body_class: 'content',");
            }

            output.AppendLine("toolbar_items_size:  'small',");
            output.AppendLine("external_plugins: {");
            output.AppendLine("  'sepimage': '" + sImageFolder + "js/tinymce/plugins/sepimage/plugin.min.js',");
            output.AppendLine("  'sepinsert': '" + sImageFolder + "js/tinymce/plugins/sepinsert/plugin.min.js',");
            output.AppendLine("  'sepinsert2': '" + sImageFolder + "js/tinymce/plugins/sepinsert2/plugin.min.js'");
            output.AppendLine("}");
            output.AppendLine("});");
            output.AppendLine("</script>");

            return output.ToString();
        }
    }
}