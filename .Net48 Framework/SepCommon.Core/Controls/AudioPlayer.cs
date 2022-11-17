// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AudioPlayer.cs" company="SepCity, Inc.">
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
    /// Class AudioPlayer.
    /// </summary>
    public class AudioPlayer
    {
        /// <summary>
        /// The m content identifier
        /// </summary>
        private string m_ContentID;

        /// <summary>
        /// The m height
        /// </summary>
        private string m_Height;

        /// <summary>
        /// The m width
        /// </summary>
        private string m_Width;

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>The content identifier.</value>
        public string ContentID
        {
            get => SepCommon.Core.SepCore.Strings.ToString(m_ContentID);

            set => m_ContentID = value;
        }

        /// <summary>
        /// Gets or sets the height of the Web server control.
        /// </summary>
        /// <value>The height.</value>
        public string Height
        {
            get => SepCommon.Core.SepCore.Strings.ToString(m_Height);

            set => m_Height = value;
        }

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
        /// Gets or sets the width of the Web server control.
        /// </summary>
        /// <value>The width.</value>
        public string Width
        {
            get => SepCommon.Core.SepCore.Strings.ToString(m_Width);

            set => m_Width = value;
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            long recCount = 0;
            var audioSrc = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT FileName,UploadID FROM Uploads WHERE Approved='1' AND UniqueID=@UniqueID AND ModuleID=@ModuleID AND (Right(FileName, 4) = '.mp3' OR Right(FileName, 4) = '.wav') ORDER BY DatePosted DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", ContentID);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.AppendLine("<video width=\"" + Width + "\" height=\"1\" id=\"" + ID + "\" controls=\"controls\">");
                            output.AppendLine("  <source src=\"" + audioSrc + "\" type='video/mp4; codecs=\"avc1.42E01E, mp4a.40.2\"'>");
                            output.AppendLine("  <!-- Fallback flash player for no-HTML5 browsers with JavaScript turned off -->");
                            output.AppendLine("  <object width=\"" + Width + "\" height=\"" + Height + "\" type=\"application/x-shockwave-flash\" data=\"" + sInstallFolder + "js/mediaelementplayer/flashmediaelement.swf\">");
                            output.AppendLine("    <param name=\"movie\" value=\"" + sInstallFolder + "js/mediaelementplayer/flashmediaelement.swf\" /> ");
                            output.AppendLine("    <param name=\"flashvars\" value=\"controls=true&file=" + audioSrc + "\" />");
                            output.AppendLine("  </object>");
                            output.AppendLine("</video>");

                            var iDivWidth = SepFunctions.toLong(Width) - 7;

                            output.AppendLine("<div class=\"ui-widget ui-widget-header wijmo-wijmenu ui-corner-all ui-helper-clearfix wijmo-wijmenu-ipod wijmo-wijmenu-container\" style=\"width: " + iDivWidth + "px\">");
                            output.AppendLine("<div class=\"scrollcontainer checkablesupport wijmo-wijsuperpanel ui-widget ui-widget-content ui-corner-all\" style=\"height: 100px; overflow: hidden; width: " + iDivWidth + "px\">");
                            output.AppendLine("<div class=\"wijmo-wijsuperpanel-statecontainer\" style=\"height: 100px; width: " + iDivWidth + "px;\">");
                            output.AppendLine("<div class=\"wijmo-wijsuperpanel-contentwrapper\" style=\"height: 100px; width: " + iDivWidth + "px; overflow:auto;\">");
                            output.AppendLine("<div class=\"wijmo-wijsuperpanel-templateouterwrapper\" style=\"width: " + (iDivWidth - 20) + "px; display: block; top: 0px;\">");
                            output.AppendLine("<ul id=\"" + ID + "List\" class=\"wijmo-wijmenu-list ui-helper-reset wijmo-wijmenu-content wijmo-wijmenu-current ui-widget-content ui-helper-clearfix\" style=\"width: " + (iDivWidth - 20) + "px;\">");
                            while (RS.Read())
                            {
                                if (recCount == 0)
                                {
                                    audioSrc = sInstallFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                                }

                                output.AppendLine("<li class=\"ui-widget wijmo-wijmenu-item ui-state-default ui-corner-all\">");
                                output.AppendLine("<a href=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "\" class=\"wijmo-wijmenu-link ui-corner-all\" onmouseover=\"$(this).addClass('ui-state-hover').addClass('ui-state-default');\" onmouseout=\"$(this).removeClass('ui-state-hover').removeClass('ui-state-default');\"><span class=\"wijmo-wijmenu-text\">" + SepFunctions.openNull(RS["FileName"]) + "</span></a>");
                                output.AppendLine("</li>");
                                recCount += 1;
                            }

                            output.AppendLine("</ul>");
                            output.AppendLine("</div>");
                            output.AppendLine("</div>");
                            output.AppendLine("</div>");
                            output.AppendLine("</div>");
                            output.AppendLine("</div>");

                            output.AppendLine("<script type=\"text/javascript\">");
                            output.AppendLine("var audio;");
                            output.AppendLine("var playlist;");
                            output.AppendLine("var tracks;");
                            output.AppendLine("var current;");

                            output.AppendLine("init();");

                            output.AppendLine("function init(){");
                            output.AppendLine("  current = 0;");
                            output.AppendLine("  audio = $('#" + ID + "');");
                            output.AppendLine("  playlist = $('#" + ID + "List');");
                            output.AppendLine("  tracks = playlist.find('li a');");
                            output.AppendLine("  len = tracks.length - 1;");
                            output.AppendLine("  audio[0].volume = .10;");
                            output.AppendLine("  audio[0].play();");
                            output.AppendLine("  playlist.find('a').click(function(e){");
                            output.AppendLine("    e.preventDefault();");
                            output.AppendLine("    link = $(this);");
                            output.AppendLine("    current = link.parent().index();");
                            output.AppendLine("    run(link, audio[0]);");
                            output.AppendLine("  });");
                            output.AppendLine("  audio[0].addEventListener('ended',function(e){");
                            output.AppendLine("    current++;");
                            output.AppendLine("    if(current == len){");
                            output.AppendLine("      current = 0;");
                            output.AppendLine("      link = playlist.find('a')[0];");
                            output.AppendLine("    }else{");
                            output.AppendLine("      link = playlist.find('a')[current];");
                            output.AppendLine("    }");
                            output.AppendLine("    run($(link), audio[0]);");
                            output.AppendLine("  });");
                            output.AppendLine("  audio[0].src = '" + audioSrc + "';");
                            output.AppendLine("  audio[0].play();");
                            output.AppendLine("}");

                            output.AppendLine("function run(link, player) {");
                            output.AppendLine("  player.src = link.attr('href');");
                            output.AppendLine("  par = link.parent();");
                            output.AppendLine("  par.addClass('active').siblings().removeClass('active');");
                            output.AppendLine("  audio[0].load();");
                            output.AppendLine("  audio[0].play();");
                            output.AppendLine("}");

                            output.AppendLine("</script>");
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}