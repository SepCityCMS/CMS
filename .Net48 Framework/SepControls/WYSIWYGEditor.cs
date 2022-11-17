// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="WYSIWYGEditor.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class WYSIWYGEditor.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WYSIWYGEditor runat=server></{0}:WYSIWYGEditor>")]
    public class WYSIWYGEditor : WebControl
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
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public string Mode
        {
            get => Strings.ToString(m_Mode);

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
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                var s = Context.Request.Form["txt" + ID];
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // -V3013
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Raises the <see cref="System.Web.UI.Control.PreRender" /> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs" /> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            var tinyMCEname = "tinyMCEJS";
            var tinyMCEurl = "https://cdnjs.cloudflare.com/ajax/libs/tinymce/5.0.14/tinymce.min.js";
            var tinyMCEtype = GetType();

            var TinyMCEcs = Page.ClientScript;

            if (!TinyMCEcs.IsClientScriptIncludeRegistered(tinyMCEtype, tinyMCEname))
            {
                TinyMCEcs.RegisterClientScriptInclude(tinyMCEtype, tinyMCEname, ResolveClientUrl(tinyMCEurl));
            }
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var sCSSFiles = string.Empty;
            var sFolderName = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE useTemplate='1'", conn))
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
                    using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
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

                sCSSFiles = "https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css";
                if (File.Exists(SepFunctions.GetDirValue("skins") + sFolderName + "\\styles\\colors.css"))
                {
                    sCSSFiles += "," + sImageFolder + "skins/" + sFolderName + "/styles/colors.css";
                }

                if (File.Exists(SepFunctions.GetDirValue("skins") + sFolderName + "\\styles\\layout.css"))
                {
                    sCSSFiles += "," + sImageFolder + "skins/" + sFolderName + "/styles/layout.css";
                }
            }

            output.WriteLine("<textarea name=\"txt" + ID + "\" id=\"txt" + ID + "\" style=\"width: " + Width + ";\">" + Text + "</textarea>");

            output.WriteLine("<script type=\"text/javascript\">");
            output.WriteLine("tinymce.init({");
            output.WriteLine("selector: 'textarea#txt" + ID + "',");
            output.WriteLine("plugins: [");
            output.WriteLine("\"autoresize advlist autolink autosave link sepimage" + Strings.ToString(Mode == "advanced" ? " sepinsert" : string.Empty) + Strings.ToString(Mode == "userpage" ? " sepinsert2" : string.Empty) + " lists charmap\",");
            output.WriteLine("\"searchreplace wordcount code fullscreen media nonbreaking\",");
            output.WriteLine("\"table directionality template textcolor paste textcolor preview\"");
            output.WriteLine("],");

            if (Request.Browser.IsMobileDevice())
            {
                output.WriteLine("toolbar1: \"bold italic underline alignleft aligncenter alignright alignjustify\",");
                output.WriteLine("toolbar2: \"fontselect fontsizeselect link\",");
                output.WriteLine("toolbar3: \"forecolor backcolor numlist bullist code\",");
            }
            else
            {
                output.WriteLine("toolbar1: \"cut copy paste | undo redo | searchreplace | fontselect fontsizeselect | indent outdent link charmap" + Strings.ToString(Mode == "advanced" ? " sepinsert" : string.Empty) + Strings.ToString(Mode == "userpage" ? " sepinsert2" : string.Empty) + "\",");
                output.WriteLine("toolbar2: \"bold italic underline | forecolor backcolor | alignleft aligncenter alignright alignjustify | numlist bullist | preview code | sepimage media | table\",");
            }

            output.WriteLine("external_filemanager_path: '" + sImageFolder + "spadmin/ImageManager/default.aspx?RelativeURLs=" + RelativeURLs + "',");

            output.WriteLine("menubar: false,");
            output.WriteLine("autoresize_min_height: '" + Height + "',");
            output.WriteLine("relative_urls: false,");
            output.WriteLine("convert_urls: false,");
            output.WriteLine("remove_script_host: false,");
            output.WriteLine("cleanup: false,");
            output.WriteLine("statusbar: false,");
            output.WriteLine("cleanup_on_startup: false,");
            output.WriteLine("trim_span_elements: false,");
            output.WriteLine("verify_html: false,");
            if (!string.IsNullOrWhiteSpace(sCSSFiles))
            {
                output.WriteLine("content_css: '" + sCSSFiles + "',");
                output.WriteLine("body_class: 'content',");
            }

            output.WriteLine("toolbar_items_size:  'small',");
            output.WriteLine("external_plugins: {");
            output.WriteLine("  'sepimage': '" + sImageFolder + "js/tinymce/plugins/sepimage/plugin.min.js',");
            output.WriteLine("  'sepinsert': '" + sImageFolder + "js/tinymce/plugins/sepinsert/plugin.min.js',");
            output.WriteLine("  'sepinsert2': '" + sImageFolder + "js/tinymce/plugins/sepinsert2/plugin.min.js'");
            output.WriteLine("}");
            output.WriteLine("});");
            output.WriteLine("</script>");
        }
    }
}