// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="TemplateDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class TemplateDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TemplateDropdown runat=server></{0}:TemplateDropdown>")]
    public class TemplateDropdown : WebControl
    {
        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

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
                var s = Context.Request.Form[ID];
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
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder(true);
            long aCounter = 0;

            output.WriteLine("<style type=\"text/css\">");
            output.WriteLine("  #Pics" + ID + " {");
            output.WriteLine("    margin-bottom: 1em;");
            output.WriteLine("  }");
            output.WriteLine("  .selectedImage {");
            output.WriteLine("    border: 3px solid red;");
            output.WriteLine("  }");
            output.WriteLine("</style>");

            output.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + sInstallFolder + "js/jquery/jquery.jcarousel.css\" />");
            output.WriteLine("<script type=\"text/javascript\" src=\"" + sInstallFolder + "js/jquery/jquery.jcarousel.min.js\"></script>");

            output.WriteLine("<script type=\"text/javascript\">");
            output.WriteLine("$(document).ready(function () {");
            output.WriteLine("var jcarousel = $('.jcarousel');");

            output.WriteLine("jcarousel.on('jcarousel:reload jcarousel:create', function () {");
            output.WriteLine("  jcarousel.jcarousel('items').css('width', '200px');");
            output.WriteLine("})");

            output.WriteLine(".jcarousel({");
            output.WriteLine("  wrap: 'circular'");
            output.WriteLine("});");

            output.WriteLine("$('.jcarousel-control-prev').jcarouselControl({");
            output.WriteLine("  target: '-=1'");
            output.WriteLine("});");

            output.WriteLine("$('.jcarousel-control-next').jcarouselControl({");
            output.WriteLine("  target: '+=1'");
            output.WriteLine("});");

            output.WriteLine("$('.jcarousel-pagination').on('jcarouselpagination:active', 'a', function() {");
            output.WriteLine("  $(this).addClass('active');");
            output.WriteLine("})");

            output.WriteLine(".on('jcarouselpagination:inactive', 'a', function() {");
            output.WriteLine("  $(this).removeClass('active');");
            output.WriteLine("})");

            output.WriteLine(".on('click', function(e) {");
            output.WriteLine("  e.preventDefault();");
            output.WriteLine("})");

            output.WriteLine(".jcarouselPagination({");
            output.WriteLine("  perPage: 1,");
            output.WriteLine("  item: function(page) {");
            output.WriteLine("    return '<a href=\"#' + page + '\">' + page + '</a>';");
            output.WriteLine("  }");
            output.WriteLine("});");

            output.WriteLine("if($('.breadcrumb').length > 0) {");
            output.WriteLine("  $('.jcarousel-wrapper').css('width', $('.breadcrumb').width());");
            output.WriteLine("}");

            output.WriteLine("if($('#TemplateConfig h2').length == 0) {");
            output.WriteLine("  $('#TemplateConfig').hide()");
            output.WriteLine("} else {");
            output.WriteLine("  $('#TemplateConfig').show()");
            output.WriteLine("}");

            output.WriteLine("});");

            output.WriteLine("function carouselClick(templateId) {");
            output.WriteLine("  var templateNewId = templateId.replace('Template','');");
            output.WriteLine("  $('#" + ID + "').val(templateNewId);");
            output.WriteLine("  $('img').removeClass('selectedImage');");
            output.WriteLine("  $('#'+templateId).addClass('selectedImage');");
            if (ModuleID == 7 || ModuleID == 60)
            {
                output.WriteLine("  $.get('" + sInstallFolder + "spadmin/template_options.aspx?ModuleID=" + ModuleID + "&TemplateID='+templateNewId+'&UserID=" + SepFunctions.Session_User_ID() + "', function( data ) {");
                output.WriteLine("    $('#TemplateConfig').html(data);");
                output.WriteLine("    if($('#TemplateConfig h2').length == 0) {");
                output.WriteLine("      $('#TemplateConfig').hide()");
                output.WriteLine("    } else {");
                output.WriteLine("      $('#TemplateConfig').show()");
                output.WriteLine("    }");
                output.WriteLine("  });");
            }

            output.WriteLine("}");
            output.WriteLine("</script>");

            output.WriteLine("<div class=\"jcarousel-wrapper\">");
            output.WriteLine("<div class=\"jcarousel\" id=\"Pics" + ID + "\">");
            output.WriteLine("<ul>");

            var enableUserPage = ModuleID == 7 || ModuleID == 60 ? " AND enableUserPage='1'" : string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TemplateID,TemplateName,FolderName,AccessKeys FROM SiteTemplates WHERE Status <> -1" + enableUserPage + " ORDER BY TemplateName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            if (string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["AccessKeys"])) || SepFunctions.CompareKeys(SepFunctions.openNull(RS["AccessKeys"]), false))
                            {
                                aCounter += 1;
                                if (string.IsNullOrWhiteSpace(Text) && aCounter == 1)
                                {
                                    Text = SepFunctions.openNull(RS["TemplateID"]);
                                }


                                string screenShot;
                                if (File.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\screen.jpg"))
                                {
                                    screenShot = SepFunctions.GetDirValue("skins", true) + SepFunctions.openNull(RS["FolderName"]) + "/screen.jpg";
                                }
                                else
                                {
                                    screenShot = SepFunctions.GetDirValue("images", true) + "public/no-photo.jpg";
                                }

                                output.WriteLine("<li><img alt=\"" + aCounter + "\" src=\"" + screenShot + "\"" + Strings.ToString(SepFunctions.openNull(RS["TemplateID"]) == Text ? " class=\"selectedImage\"" : string.Empty) + " id=\"Template" + SepFunctions.openNull(RS["TemplateID"]) + "\" height=\"150\" width=\"190\" style=\"cursor:pointer;\" onclick=\"carouselClick('Template" + SepFunctions.openNull(RS["TemplateID"]) + "')\" /><span>" + SepFunctions.openNull(RS["TemplateName"]) + "</span></li>");
                            }
                        }
                    }
                }
            }

            output.WriteLine("</ul>");
            output.WriteLine("</div>");

            output.WriteLine("<a href=\"javascript:void(0)\" class=\"jcarousel-control-prev\">&lsaquo;</a>");
            output.WriteLine("<a href=\"javascript:void(0)\" class=\"jcarousel-control-next\">&rsaquo;</a>");
            output.WriteLine("</div>");

            output.WriteLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"" + Text + "\" />");
            output.WriteLine("<div style=\"clear:both;\"></div>");
        }
    }
}