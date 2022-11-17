// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="AudioPlayer.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class AudioPlayer.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:AudioPlayer runat=server></{0}:AudioPlayer>")]
    public class AudioPlayer : WebControl
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
        /// The m text
        /// </summary>
        private string m_Text;

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
            get => Strings.ToString(m_ContentID);

            set => m_ContentID = value;
        }

        /// <summary>
        /// Gets or sets the height of the Web server control.
        /// </summary>
        /// <value>The height.</value>
        public new string Height
        {
            get => Strings.ToString(m_Height);

            set => m_Height = value;
        }

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
            get => Strings.ToString(m_Text);

            set => m_Text = value;
        }

        /// <summary>
        /// Gets or sets the width of the Web server control.
        /// </summary>
        /// <value>The width.</value>
        public new string Width
        {
            get => Strings.ToString(m_Width);

            set => m_Width = value;
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
            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            long recCount = 0;
            var audioSrc = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FileName,UploadID FROM Uploads WHERE Approved='1' AND UniqueID=@UniqueID AND ModuleID=@ModuleID AND (Right(FileName, 4) = '.mp3' OR Right(FileName, 4) = '.wav') ORDER BY DatePosted DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", ContentID);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.WriteLine("<video width=\"" + Width + "\" height=\"1\" id=\"" + ID + "\" controls=\"controls\">");
                            output.WriteLine("  <source src=\"" + audioSrc + "\" type='video/mp4; codecs=\"avc1.42E01E, mp4a.40.2\"'>");
                            output.WriteLine("  <!-- Fallback flash player for no-HTML5 browsers with JavaScript turned off -->");
                            output.WriteLine("  <object width=\"" + Width + "\" height=\"" + Height + "\" type=\"application/x-shockwave-flash\" data=\"" + sInstallFolder + "js/mediaelementplayer/flashmediaelement.swf\">");
                            output.WriteLine("    <param name=\"movie\" value=\"" + sInstallFolder + "js/mediaelementplayer/flashmediaelement.swf\" /> ");
                            output.WriteLine("    <param name=\"flashvars\" value=\"controls=true&file=" + audioSrc + "\" />");
                            output.WriteLine("  </object>");
                            output.WriteLine("</video>");

                            var iDivWidth = SepFunctions.toLong(Width) - 7;

                            output.WriteLine("<div class=\"ui-widget ui-widget-header wijmo-wijmenu ui-corner-all ui-helper-clearfix wijmo-wijmenu-ipod wijmo-wijmenu-container\" style=\"width: " + iDivWidth + "px\">");
                            output.WriteLine("<div class=\"scrollcontainer checkablesupport wijmo-wijsuperpanel ui-widget ui-widget-content ui-corner-all\" style=\"height: 100px; overflow: hidden; width: " + iDivWidth + "px\">");
                            output.WriteLine("<div class=\"wijmo-wijsuperpanel-statecontainer\" style=\"height: 100px; width: " + iDivWidth + "px;\">");
                            output.WriteLine("<div class=\"wijmo-wijsuperpanel-contentwrapper\" style=\"height: 100px; width: " + iDivWidth + "px; overflow:auto;\">");
                            output.WriteLine("<div class=\"wijmo-wijsuperpanel-templateouterwrapper\" style=\"width: " + (iDivWidth - 20) + "px; display: block; top: 0px;\">");
                            output.WriteLine("<ul id=\"" + ID + "List\" class=\"wijmo-wijmenu-list ui-helper-reset wijmo-wijmenu-content wijmo-wijmenu-current ui-widget-content ui-helper-clearfix\" style=\"width: " + (iDivWidth - 20) + "px;\">");
                            while (RS.Read())
                            {
                                if (recCount == 0)
                                {
                                    audioSrc = sInstallFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                                }

                                output.WriteLine("<li class=\"ui-widget wijmo-wijmenu-item ui-state-default ui-corner-all\">");
                                output.WriteLine("<a href=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "\" class=\"wijmo-wijmenu-link ui-corner-all\" onmouseover=\"$(this).addClass('ui-state-hover').addClass('ui-state-default');\" onmouseout=\"$(this).removeClass('ui-state-hover').removeClass('ui-state-default');\"><span class=\"wijmo-wijmenu-text\">" + SepFunctions.openNull(RS["FileName"]) + "</span></a>");
                                output.WriteLine("</li>");
                                recCount += 1;
                            }

                            output.WriteLine("</ul>");
                            output.WriteLine("</div>");
                            output.WriteLine("</div>");
                            output.WriteLine("</div>");
                            output.WriteLine("</div>");
                            output.WriteLine("</div>");

                            output.WriteLine("<script type=\"text/javascript\">");
                            output.WriteLine("var audio;");
                            output.WriteLine("var playlist;");
                            output.WriteLine("var tracks;");
                            output.WriteLine("var current;");

                            output.WriteLine("init();");

                            output.WriteLine("function init(){");
                            output.WriteLine("  current = 0;");
                            output.WriteLine("  audio = $('#" + ID + "');");
                            output.WriteLine("  playlist = $('#" + ID + "List');");
                            output.WriteLine("  tracks = playlist.find('li a');");
                            output.WriteLine("  len = tracks.length - 1;");
                            output.WriteLine("  audio[0].volume = .10;");
                            output.WriteLine("  audio[0].play();");
                            output.WriteLine("  playlist.find('a').click(function(e){");
                            output.WriteLine("    e.preventDefault();");
                            output.WriteLine("    link = $(this);");
                            output.WriteLine("    current = link.parent().index();");
                            output.WriteLine("    run(link, audio[0]);");
                            output.WriteLine("  });");
                            output.WriteLine("  audio[0].addEventListener('ended',function(e){");
                            output.WriteLine("    current++;");
                            output.WriteLine("    if(current == len){");
                            output.WriteLine("      current = 0;");
                            output.WriteLine("      link = playlist.find('a')[0];");
                            output.WriteLine("    }else{");
                            output.WriteLine("      link = playlist.find('a')[current];");
                            output.WriteLine("    }");
                            output.WriteLine("    run($(link), audio[0]);");
                            output.WriteLine("  });");
                            output.WriteLine("  audio[0].src = '" + audioSrc + "';");
                            output.WriteLine("  audio[0].play();");
                            output.WriteLine("}");

                            output.WriteLine("function run(link, player) {");
                            output.WriteLine("  player.src = link.attr('href');");
                            output.WriteLine("  par = link.parent();");
                            output.WriteLine("  par.addClass('active').siblings().removeClass('active');");
                            output.WriteLine("  audio[0].load();");
                            output.WriteLine("  audio[0].play();");
                            output.WriteLine("}");

                            output.WriteLine("</script>");
                        }
                    }
                }
            }
        }
    }
}