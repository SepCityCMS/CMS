// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="TemplateDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class TemplateDropdown.
    /// </summary>
    public class TemplateDropdown
    {
        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

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

            var sInstallFolder = SepFunctions.GetInstallFolder(true);
            long aCounter = 0;

            output.AppendLine("<style type=\"text/css\">");
            output.AppendLine("  #Pics" + ID + " {");
            output.AppendLine("    margin-bottom: 1em;");
            output.AppendLine("  }");
            output.AppendLine("  .selectedImage {");
            output.AppendLine("    border: 3px solid red;");
            output.AppendLine("  }");
            output.AppendLine("</style>");

            output.AppendLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + sInstallFolder + "js/jquery/jquery.jcarousel.css\" />");
            output.AppendLine("<script type=\"text/javascript\" src=\"" + sInstallFolder + "js/jquery/jquery.jcarousel.min.js\"></script>");

            output.AppendLine("<script type=\"text/javascript\">");
            output.AppendLine("$(document).ready(function () {");
            output.AppendLine("var jcarousel = $('.jcarousel');");

            output.AppendLine("jcarousel.on('jcarousel:reload jcarousel:create', function () {");
            output.AppendLine("  jcarousel.jcarousel('items').css('width', '200px');");
            output.AppendLine("})");

            output.AppendLine(".jcarousel({");
            output.AppendLine("  wrap: 'circular'");
            output.AppendLine("});");

            output.AppendLine("$('.jcarousel-control-prev').jcarouselControl({");
            output.AppendLine("  target: '-=1'");
            output.AppendLine("});");

            output.AppendLine("$('.jcarousel-control-next').jcarouselControl({");
            output.AppendLine("  target: '+=1'");
            output.AppendLine("});");

            output.AppendLine("$('.jcarousel-pagination').on('jcarouselpagination:active', 'a', function() {");
            output.AppendLine("  $(this).addClass('active');");
            output.AppendLine("})");

            output.AppendLine(".on('jcarouselpagination:inactive', 'a', function() {");
            output.AppendLine("  $(this).removeClass('active');");
            output.AppendLine("})");

            output.AppendLine(".on('click', function(e) {");
            output.AppendLine("  e.preventDefault();");
            output.AppendLine("})");

            output.AppendLine(".jcarouselPagination({");
            output.AppendLine("  perPage: 1,");
            output.AppendLine("  item: function(page) {");
            output.AppendLine("    return '<a href=\"#' + page + '\">' + page + '</a>';");
            output.AppendLine("  }");
            output.AppendLine("});");

            output.AppendLine("if($('.breadcrumb').length > 0) {");
            output.AppendLine("  $('.jcarousel-wrapper').css('width', $('.breadcrumb').width());");
            output.AppendLine("}");

            output.AppendLine("if($('#TemplateConfig h2').length == 0) {");
            output.AppendLine("  $('#TemplateConfig').hide()");
            output.AppendLine("} else {");
            output.AppendLine("  $('#TemplateConfig').show()");
            output.AppendLine("}");

            output.AppendLine("});");

            output.AppendLine("function carouselClick(templateId) {");
            output.AppendLine("  var templateNewId = templateId.replace('Template','');");
            output.AppendLine("  $('#" + ID + "').val(templateNewId);");
            output.AppendLine("  $('img').removeClass('selectedImage');");
            output.AppendLine("  $('#'+templateId).addClass('selectedImage');");
            if (ModuleID == 7 || ModuleID == 60)
            {
                output.AppendLine("  $.get('" + sInstallFolder + "spadmin/template_options.aspx?ModuleID=" + ModuleID + "&TemplateID='+templateNewId+'&UserID=" + SepFunctions.Session_User_ID() + "', function( data ) {");
                output.AppendLine("    $('#TemplateConfig').html(data);");
                output.AppendLine("    if($('#TemplateConfig h2').length == 0) {");
                output.AppendLine("      $('#TemplateConfig').hide()");
                output.AppendLine("    } else {");
                output.AppendLine("      $('#TemplateConfig').show()");
                output.AppendLine("    }");
                output.AppendLine("  });");
            }

            output.AppendLine("}");
            output.AppendLine("</script>");

            output.AppendLine("<div class=\"jcarousel-wrapper\">");
            output.AppendLine("<div class=\"jcarousel\" id=\"Pics" + ID + "\">");
            output.AppendLine("<ul>");

            var enableUserPage = ModuleID == 7 || ModuleID == 60 ? " AND enableUserPage='1'" : string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TemplateID,TemplateName,FolderName,AccessKeys FROM SiteTemplates WHERE Status <> -1" + enableUserPage + " ORDER BY TemplateName", conn))
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

                                output.AppendLine("<li><img alt=\"" + aCounter + "\" src=\"" + screenShot + "\"" + SepCommon.SepCore.Strings.ToString(SepFunctions.openNull(RS["TemplateID"]) == Text ? " class=\"selectedImage\"" : string.Empty) + " id=\"Template" + SepFunctions.openNull(RS["TemplateID"]) + "\" height=\"150\" width=\"190\" style=\"cursor:pointer;\" onclick=\"carouselClick('Template" + SepFunctions.openNull(RS["TemplateID"]) + "')\" /><span>" + SepFunctions.openNull(RS["TemplateName"]) + "</span></li>");
                            }
                        }
                    }
                }
            }

            output.AppendLine("</ul>");
            output.AppendLine("</div>");

            output.AppendLine("<a href=\"javascript:void(0)\" class=\"jcarousel-control-prev\">&lsaquo;</a>");
            output.AppendLine("<a href=\"javascript:void(0)\" class=\"jcarousel-control-next\">&rsaquo;</a>");
            output.AppendLine("</div>");

            output.AppendLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"" + Text + "\" />");
            output.AppendLine("<div style=\"clear:both;\"></div>");

            return output.ToString();
        }
    }
}