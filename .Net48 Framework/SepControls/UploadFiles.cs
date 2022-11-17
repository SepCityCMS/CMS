// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UploadFiles.cs" company="SepCity, Inc.">
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
    /// Class UploadFiles.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:UploadFiles runat=server></{0}:UploadFiles>")]
    public class UploadFiles : WebControl
    {
        /// <summary>
        /// The m content identifier
        /// </summary>
        private string m_ContentID;

        /// <summary>
        /// The m show temporary
        /// </summary>
        private bool m_showTemp;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

        /// <summary>
        /// Enum EFileTypes
        /// </summary>
        [Flags]
        public enum EFileTypes
        {
            /// <summary>
            /// The images
            /// </summary>
            Images = 0,

            /// <summary>
            /// The audio
            /// </summary>
            Audio = 1,

            /// <summary>
            /// The video
            /// </summary>
            Video = 2,

            /// <summary>
            /// The document
            /// </summary>
            Document = 3,

            /// <summary>
            /// The software
            /// </summary>
            Software = 4,

            /// <summary>
            /// Any
            /// </summary>
            Any = 5
        }

        /// <summary>
        /// Enum EInputMode
        /// </summary>
        public enum EInputMode
        {
            /// <summary>
            /// The single file
            /// </summary>
            SingleFile = 0,

            /// <summary>
            /// The multiple files
            /// </summary>
            MultipleFiles = 1
        }

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
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        public EFileTypes FileType { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public EInputMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show temporary].
        /// </summary>
        /// <value><c>true</c> if [show temporary]; otherwise, <c>false</c>.</value>
        public bool showTemp
        {
            get => Convert.ToBoolean(m_showTemp);

            set => m_showTemp = value;
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
            get => Strings.ToString(m_Text);

            set => m_Text = value;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => Strings.ToString(m_UserID);

            set => m_UserID = value;
        }

        /// <summary>
        /// Formats the size.
        /// </summary>
        /// <param name="iSize">Size of the i.</param>
        /// <returns>System.String.</returns>
        public string FormatSize(long iSize)
        {
            string sReturn;

            if (iSize > 1024)
            {
                iSize /= 1024;
                sReturn = iSize + " KB";
                if (iSize > 1024)
                {
                    iSize /= 1024;
                    sReturn = iSize + " MB";
                    if (iSize > 1024)
                    {
                        iSize /= 1024;
                        sReturn = iSize + " KB";
                    }
                }
            }
            else
            {
                sReturn = iSize + " bytes";
            }

            return sReturn;
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
            var cName = "PLUploadJS";
            var cUrl = "https://cdnjs.cloudflare.com/ajax/libs/plupload/2.1.8/plupload.full.min.js";
            var csType = GetType();

            var cs = Page.ClientScript;

            if (!cs.IsClientScriptIncludeRegistered(csType, cName))
            {
                cs.RegisterClientScriptInclude(csType, cName, ResolveClientUrl(cUrl));
            }
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            long imgCount = 0;
            long maxFiles = 5;

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(UserID))
            {
                UserID = SepFunctions.Session_User_ID();
            }

            switch (ModuleID)
            {
                case 4:
                    maxFiles = SepFunctions.toLong(SepFunctions.Setup(4, "ContactMaxFiles"));
                    if (maxFiles == 0)
                    {
                        maxFiles = 1;
                    }

                    break;

                case 28:
                    maxFiles = SepFunctions.toLong(SepFunctions.Setup(28, "PhotoNumber"));
                    if (maxFiles == 0)
                    {
                        maxFiles = 25;
                    }

                    break;

                case 32:
                    maxFiles = SepFunctions.toLong(SepFunctions.Setup(32, "RStateMaxPhotos"));
                    if (maxFiles == 0)
                    {
                        maxFiles = 5;
                    }

                    break;

                case 40:
                    maxFiles = 20;
                    break;

                case 37:
                    maxFiles = 100;
                    break;

                case 63:
                    if (FileType == EFileTypes.Images)
                    {
                        maxFiles = SepFunctions.toLong(SepFunctions.Setup(63, "ProfilesPicNo"));
                    }
                    else
                    {
                        maxFiles = SepFunctions.toLong(SepFunctions.Setup(63, "ProfilesAudio"));
                    }

                    break;
            }

            if (maxFiles == 0)
            {
                maxFiles = 5;
            }

            if (Strings.ToString(Mode) == "SingleFile")
            {
                maxFiles = 1;
            }

            output.WriteLine("<div id=\"" + ID + "Existing\" class=\"uploadcontrol\">");
            switch (FileType)
            {
                case EFileTypes.Images:
                    if (Strings.ToString(Mode) == "SingleFile")
                    {
                        output.WriteLine("<p>Select one of the following formats(jpg, png, gif), with a maximum size of " + SepFunctions.toLong(SepFunctions.Setup(992, "MaxImageSize")) + " KB's.</p>");
                    }
                    else
                    {
                        output.WriteLine("<p>Upload up to " + maxFiles + " image files(jpg, png, gif), each having maximum size of " + SepFunctions.toLong(SepFunctions.Setup(992, "MaxImageSize")) + " KB's.</p>");
                    }

                    break;
            }

            output.WriteLine("<div id=\"Success" + ID + "\"></div>");
            output.WriteLine("<p id=\"" + ID + "queuestatus\"></p>");
            output.WriteLine("<ol id=\"" + ID + "log\" class=\"uploadLog\">");
            if (!string.IsNullOrWhiteSpace(ContentID))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    switch (FileType)
                    {
                        case EFileTypes.Images:
                            wClause = " AND (Right(FileName, 4) = '.png' OR Right(FileName, 4) = '.gif' OR Right(FileName, 4) = '.jpg' OR Right(FileName, 5) = '.jpeg' OR Right(FileName, 4) = '.bmp')";
                            break;

                        case EFileTypes.Audio:
                            wClause = " AND (Right(FileName, 4) = '.wav' OR Right(FileName, 4) = '.mp3')";
                            break;

                        case EFileTypes.Document:
                            wClause = " AND (Right(FileName, 4) = '.txt' OR Right(FileName, 4) = '.rtf' OR Right(FileName, 4) = '.pdf' OR Right(FileName, 5) = '.docx' OR Right(FileName, 4) = '.doc')";
                            break;

                        case EFileTypes.Software:
                            wClause = " AND (Right(FileName, 4) = '.zip' OR Right(FileName, 4) = '.rar')";
                            break;

                        case EFileTypes.Video:
                            wClause = " AND (Right(FileName, 4) = '.mp4' OR Right(FileName, 4) = '.avi' OR Right(FileName, 4) = '.flv' OR Right(FileName, 5) = '.mpeg')";
                            break;
                    }

                    var sControlID = ModuleID == 37 || ModuleID == 5 || ModuleID == 41 || ModuleID == 63 || ModuleID == 65 ? " AND ControlID=@ControlID" : string.Empty;

                    if (showTemp)
                    {
                        sControlID += " AND isTemp='1'";
                    }
                    else
                    {
                        sControlID += " AND isTemp='0'";
                    }

                    using (var cmd = new SqlCommand("SELECT UploadID,ModuleID,FileName,FileSize,Weight FROM Uploads WHERE ModuleID=@ModuleID AND Approved='1' AND UniqueID=@UniqueID" + sControlID + wClause + " ORDER BY Weight", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        cmd.Parameters.AddWithValue("@UniqueID", ContentID);
                        cmd.Parameters.AddWithValue("@ControlID", ID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                switch (FileType)
                                {
                                    case EFileTypes.Images:
                                        output.WriteLine("<li id=\"li" + SepFunctions.openNull(RS["UploadID"]) + "\" class=\"success\">");
                                        output.WriteLine("<div class=\"uploadleft\">");
                                        output.WriteLine("<img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&ModuleID=" + SepFunctions.openNull(RS["ModuleID"]) + "&Size=thumb\" border=\"0\" alt=\"\" />");
                                        output.WriteLine("</div>");
                                        output.WriteLine("<div class=\"uploadright\">");
                                        output.WriteLine("File: <em>" + SepFunctions.openNull(RS["FileName"]) + "</em> (" + FormatSize(SepFunctions.toLong(SepFunctions.openNull(RS["FileSize"]))) + ") <span class=\"progressvalue\"></span>");
                                        if (maxFiles > 1)
                                        {
                                            if (SepFunctions.toLong(SepFunctions.openNull(RS["Weight"])) == 1)
                                            {
                                                output.WriteLine("<br/><span class=\"DefaultImage\" id=\"" + SepFunctions.openNull(RS["UploadID"]) + "\">" + SepFunctions.LangText("Default Image") + "</span>");
                                            }
                                            else
                                            {
                                                output.WriteLine("<br/><span class=\"DefaultImage\" id=\"" + SepFunctions.openNull(RS["UploadID"]) + "\"><a href=\"javascript:void(0)\" onclick=\"markDefault" + ID + "('" + SepFunctions.openNull(RS["UploadID"]) + "')\">" + SepFunctions.LangText("Mark as Default") + "</a></span>");
                                            }
                                        }

                                        output.WriteLine("<div><div style=\"width: 100%;\"></div></div>");
                                        output.WriteLine("<p class=\"status\"></p>");
                                        output.WriteLine("<span class=\"cancel\" style=\"display: none;\">&nbsp;</span>");
                                        output.WriteLine("</div>");
                                        output.WriteLine("<div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "('li" + imgCount + "', '" + SepFunctions.openNull(RS["UploadID"]) + "')\" alt=\"" + SepFunctions.LangText("Delete Image") + "\" /></div>");
                                        output.WriteLine("</li>");
                                        break;

                                    default:
                                        output.WriteLine("<li id=\"li" + SepFunctions.openNull(RS["UploadID"]) + "\" class=\"success\">");
                                        output.WriteLine("<div class=\"uploadleft\" style=\"width:128px;\">");
                                        switch (FileType)
                                        {
                                            case EFileTypes.Audio:
                                                output.WriteLine("<img src=\"" + sImageFolder + "images/public/audio-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Document:
                                                output.WriteLine("<img src=\"" + sImageFolder + "images/public/document-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Software:
                                                output.WriteLine("<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Video:
                                                output.WriteLine("<img src=\"" + sImageFolder + "images/public/video-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Any:
                                                var fileImage = "images/public/any-file-128.png";
                                                var fileExtension = Path.GetExtension(SepFunctions.openNull(RS["FileName"]));
                                                if (fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".bmp")
                                                {
                                                    fileImage = "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&ModuleID=" + SepFunctions.openNull(RS["ModuleID"]) + "&Size=thumb";
                                                }

                                                if (fileExtension == ".wav" || fileExtension == ".mp3")
                                                {
                                                    fileImage = "images/public/audio-128.png";
                                                }

                                                if (fileExtension == ".txt" || fileExtension == ".rtf" || fileExtension == ".pdf" || fileExtension == ".docx" || fileExtension == ".doc")
                                                {
                                                    fileImage = "images/public/document-128.png";
                                                }

                                                if (fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".flv" || fileExtension == ".mpeg")
                                                {
                                                    fileImage = "images/public/document-128.png";
                                                }

                                                output.WriteLine("<img src=\"" + sImageFolder + fileImage + "\" border=\"0\" alt=\"\" />");
                                                break;

                                            default:
                                                output.WriteLine("<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />");
                                                break;
                                        }

                                        output.WriteLine("</div>");
                                        output.WriteLine("<div class=\"uploadright\">");
                                        output.WriteLine("File: <em>" + SepFunctions.openNull(RS["FileName"]) + "</em> (" + FormatSize(SepFunctions.toLong(SepFunctions.openNull(RS["FileSize"]))) + ") <span class=\"progressvalue\"></span>");
                                        output.WriteLine("<div><div style=\"width: 100%;\"></div></div>");
                                        output.WriteLine("<p class=\"status\"></p>");
                                        output.WriteLine("<span class=\"cancel\" style=\"display: none;\">&nbsp;</span>");
                                        output.WriteLine("</div>");
                                        output.WriteLine("<div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "('li" + imgCount + "', '" + SepFunctions.openNull(RS["UploadID"]) + "')\" alt=\"" + SepFunctions.LangText("Delete Image") + "\" /></div>");
                                        output.WriteLine("</li>");
                                        break;
                                }

                                imgCount += 1;
                            }
                        }
                    }
                }
            }

            output.WriteLine("</ol>");
            output.WriteLine("</div>");

            output.WriteLine("<br />");

            output.WriteLine("<div id=\"" + ID + "container\">");
            if (Strings.ToString(Mode) == "SingleFile")
            {
                switch (FileType)
                {
                    case EFileTypes.Images:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select an Image File") + "\" />");
                        break;

                    case EFileTypes.Audio:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select an Audio File") + "\" />");
                        break;

                    case EFileTypes.Document:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a Document File") + "\" />");
                        break;

                    case EFileTypes.Software:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a Compressed File") + "\" />");
                        break;

                    case EFileTypes.Video:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a Video File") + "\" />");
                        break;

                    case EFileTypes.Any:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a File") + "\" />");
                        break;
                }
            }
            else
            {
                switch (FileType)
                {
                    case EFileTypes.Images:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Image Files") + "\" />");
                        break;

                    case EFileTypes.Audio:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Audio Files") + "\" />");
                        break;

                    case EFileTypes.Document:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Document Files") + "\" />");
                        break;

                    case EFileTypes.Software:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Compressed Files") + "\" />");
                        break;

                    case EFileTypes.Video:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Video Files") + "\" />");
                        break;

                    case EFileTypes.Any:
                        output.WriteLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a File(s)") + "\" />");
                        break;
                }
            }

            output.WriteLine("</div>" + Environment.NewLine);

            output.WriteLine("<script type=\"text/javascript\">");
            output.WriteLine("var sIdentity" + ID + " = getIdentity();");
            output.WriteLine("var sFileId = 0;");
            output.WriteLine("var uploader" + ID + " = new plupload.Uploader({");
            output.WriteLine("\truntimes: 'html5,flash,silverlight,html4',");
            output.WriteLine("\tbrowse_button: 'pickfiles" + ID + "',");
            output.WriteLine("\tcontainer: document.getElementById('" + ID + "container'),");
            output.WriteLine("\turl: '" + sImageFolder + "js/upload.ashx?Identity='+sIdentity" + ID + "+'&ControlID=" + ID + "&ContentID=" + ContentID + "&ModuleID=" + ModuleID + "&UserID=" + SepFunctions.UrlEncode(UserID) + "&UploadMode=" + Strings.ToString(Strings.ToString(Mode) == "SingleFile" ? "single" : "multiple") + "',");
            output.WriteLine("\tflash_swf_url: '" + sImageFolder + "js/plupload/Moxie.swf',");
            output.WriteLine("\tsilverlight_xap_url: '" + sImageFolder + "js/plupload/Moxie.xap',");
            output.WriteLine("  multi_selection: false,");

            output.WriteLine("\tfilters: {");
            if (FileType == EFileTypes.Images)
            {
                output.WriteLine("\t\tmax_file_size: '" + SepFunctions.toLong(SepFunctions.Setup(992, "MaxImageSize")) + "kb',");
            }
            else
            {
                if (ModuleID == 10)
                {
                    output.WriteLine("\t\tmax_file_size: '" + SepFunctions.toLong(SepFunctions.Setup(10, "LibraryMaxUpload")) + "mb',");
                }
            }

            if (FileType != EFileTypes.Any)
            {
                output.WriteLine("\t\tmime_types: [");
                switch (FileType)
                {
                    case EFileTypes.Images:
                        output.WriteLine("      {title: \"Image files\", extensions: \"jpg,gif,png\"}");
                        break;

                    case EFileTypes.Audio:
                        output.WriteLine("      {title: \"Audio files\", extensions: \"mp3,wav\"}");
                        break;

                    case EFileTypes.Document:
                        output.WriteLine("      {title: \"Document files\", extensions: \"doc,docx,txt,rtf,pdf\"}");
                        break;

                    case EFileTypes.Software:
                        output.WriteLine("      {title: \"Compress files\", extensions: \"zip,rar\"}");
                        break;

                    case EFileTypes.Video:
                        output.WriteLine("      {title: \"Video files\", extensions: \"mp4,avi,mpeg,flv\"}");
                        break;
                }

                output.WriteLine("\t\t]");
            }

            output.WriteLine("\t},");

            output.WriteLine("\tinit: {");
            if (ModuleID == 10 && SepFunctions.Setup(992, "CatLowestLvl") == "Yes")
            {
                output.WriteLine("\t\tBrowse: function() {");
                output.WriteLine("        if($('#CategoryLowest').val() != '1') {");
                output.WriteLine("          alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("You must select the lowest level of category available.")) + "'));");
                output.WriteLine("          return false;");
                output.WriteLine("        } else {");
                output.WriteLine("          return true;");
                output.WriteLine("        }");
                output.WriteLine("\t\t},");
            }

            output.WriteLine("\t\tFilesAdded: function(up, files) {");
            output.WriteLine("          $('#" + ID + "Bar').html('<strong>' + unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Please wait while files are being uploaded.")) + "') + '</strong>');");
            output.WriteLine("\t\t\tuploader" + ID + ".start();");
            output.WriteLine("\t\t},");

            output.WriteLine("\t\tUploadComplete: function(up, files) {");
            output.WriteLine("\t\t\tuploadFinished" + ID + "(files[0].name);");
            if (ModuleID == 10)
            {
                output.WriteLine("        uploadToDownloads();");
            }

            output.WriteLine("\t\t},");

            output.WriteLine("\t\tError: function(up, err) {");
            output.WriteLine("          $('#" + ID + "Bar').html('Error #' + err.code + ': ' + err.message);");
            output.WriteLine("\t\t}");
            output.WriteLine("\t}");
            output.WriteLine("});");

            output.WriteLine("uploader" + ID + ".bind('FilesAdded', function(up, files) {");
            output.WriteLine("uploader" + ID + ".start();");
            output.WriteLine("});");

            output.WriteLine("uploader" + ID + ".bind('QueueChanged', function (up, files) {");
            output.WriteLine("uploader" + ID + ".start();");
            output.WriteLine("up.refresh();");
            output.WriteLine("});");

            output.WriteLine("uploader" + ID + ".init();");

            output.WriteLine("function markDefault" + ID + "(sUploadId) {");
            if (maxFiles > 1)
            {
                output.Write("\t" + "$.ajax({");
                output.Write("\t" + "\t" + "url: '" + sImageFolder + "spadmin/image_upload_default.aspx?UploadID='+sUploadId+'&ModuleID=" + ModuleID + "&UserID=" + UserID + "',");
                output.Write("\t" + "\t" + "success: function(data) {");
                output.Write("\t" + "\t" + "$('#" + ID + "log li .DefaultImage').each(function(index) {");
                output.Write("\t" + "\t" + "$(this).html(unescape('" + SepFunctions.EscQuotes("<a href=\"javascript:void(0)\" onclick=\"markDefault" + ID + "('") + "') + $(this).attr('id') + unescape('" + SepFunctions.EscQuotes("')\">" + SepFunctions.LangText("Mark as Default") + "</a>") + "'));");
                output.Write("\t" + "\t" + "});");
                output.Write("\t" + "\t" + "$('#li'+sUploadId+' .DefaultImage').html(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Default Image")) + "'));");
                output.Write("\t" + "\t" + "}");
                output.Write("\t" + "});");
            }

            output.WriteLine("}");

            output.WriteLine("function deleteFile" + ID + "(sId, sUploadId) {");
            output.Write("\t" + "$.ajax({");
            output.Write("\t" + "\t" + "url: '" + sImageFolder + "spadmin/image_upload_delete.aspx?UploadID='+sUploadId,");
            output.Write("\t" + "\t" + "success: function(data) {");
            output.Write("\t" + "\t" + "$('#li'+sUploadId).remove();");
            output.Write("\t" + "\t" + "$('#Success" + ID + "').html(data);");
            output.Write("\t" + "$('#" + ID + "container').show();");
            output.Write("\t" + "\t" + "}");
            output.Write("\t" + "});");
            output.WriteLine("}");

            output.WriteLine("function uploadFinished" + ID + "(fileName) {");
            output.Write("\t" + "sFileId = (sIdentity" + ID + " + " + imgCount + ");");
            switch (FileType)
            {
                case EFileTypes.Images:
                    output.Write("\t" + "var listitem='<li id=\"li'+sFileId+'\"><div class=\"uploadleft\" style=\"width:128px;\"></div>'+");
                    output.Write("\t" + "'<div class=\"uploadright\"><p class=\"status\">" + SepFunctions.LangText("Pending") + "</p></div>'+");
                    output.Write("\t" + "'</div><div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "(\\'li" + imgCount + "\\', \\''+sFileId+'\\')\" alt=\"" + SepFunctions.LangText("Delete Image") + "\" /></div></li>';");
                    output.Write("\t" + "$('#" + ID + "log').append(listitem);");
                    output.Write("\t" + "var item=$('#" + ID + "log li#li'+sFileId);");
                    output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID='+sIdentity" + ID + "+'&Size=thumb&ModuleID=" + ModuleID + "\" border=\"0\" alt=\"\" />');");
                    output.Write("\t" + "item.addClass('success').find('p.status').html('" + SepFunctions.LangText("Image has been successfully uploaded.") + "');");
                    if (maxFiles > 1)
                    {
                        output.WriteLine("if($('#" + ID + "log li').length == 1) {");
                        output.WriteLine("  item.find('p.status').append(unescape('" + SepFunctions.EscQuotes("<br/><span class=\"DefaultImage\" id=\"") + "') + sFileId + unescape('" + SepFunctions.EscQuotes("\">" + SepFunctions.LangText("Default Image") + "</span>") + "'));");
                        output.WriteLine("} else {");
                        output.WriteLine("  item.find('p.status').append(unescape('" + SepFunctions.EscQuotes("<br/><span class=\"DefaultImage\" id=\"") + "') + sFileId + unescape('" + SepFunctions.EscQuotes("\"><a href=\"javascript:void(0)\" onclick=\"markDefault" + ID + "('") + "') + sFileId + unescape('" + SepFunctions.EscQuotes("')\">" + SepFunctions.LangText("Mark as Default") + "</a></span>") + "'));");
                        output.WriteLine("}");
                    }

                    break;

                default:
                    output.Write("\t" + "var listitem='<li id=\"li'+sFileId+'\"><div class=\"uploadleft\" style=\"width:128px;\"></div>'+");
                    output.Write("\t" + "'<p class=\"status\">" + SepFunctions.LangText("Pending") + "</p>'+");
                    output.Write("\t" + "'</div><div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "(\\'li" + imgCount + "\\', \\''+sFileId+'\\')\" alt=\"" + SepFunctions.LangText("Delete File") + "\" /></div></li>';");
                    output.Write("\t" + "$('#" + ID + "log').append(listitem);");
                    output.Write("\t" + "var item=$('#" + ID + "log li#li'+sFileId);");
                    switch (FileType)
                    {
                        case EFileTypes.Audio:
                            output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/audio-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Document:
                            output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/document-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Software:
                            output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Video:
                            output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/video-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Any:
                            output.Write("\t" + "var fileImage = 'images/public/any-file-128.png';");
                            output.Write("\t" + "var fileExtension = fileName.split('.');");
                            output.Write("\t" + "try{fileExtension = fileExtension[fileExtension.length - 1];}catch(e){};");
                            output.Write("\t" + "if (fileExtension == 'png' || fileExtension == 'gif' || fileExtension == 'jpg' || fileExtension == 'jpeg' || fileExtension == 'bmp')");
                            output.Write("\t" + "{");
                            output.Write("\t" + "fileImage = 'spadmin/show_image.aspx?UploadID='+sIdentity" + ID + "+'&Size=thumb&ModuleID=" + ModuleID + "';");
                            output.Write("\t" + "}");
                            output.Write("\t" + "if (fileExtension == 'wav' || fileExtension == 'mp3')");
                            output.Write("\t" + "{");
                            output.Write("\t" + "fileImage = 'images/public/audio-128.png';");
                            output.Write("\t" + "}");
                            output.Write("\t" + "if (fileExtension == 'txt' || fileExtension == 'rtf' || fileExtension == 'pdf' || fileExtension == 'docx' || fileExtension == 'doc')");
                            output.Write("\t" + "{");
                            output.Write("\t" + "fileImage = 'images/public/document-128.png';");
                            output.Write("\t" + "}");
                            output.Write("\t" + "if (fileExtension == 'mp4' || fileExtension == 'avi' || fileExtension == 'flv' || fileExtension == 'mpeg')");
                            output.Write("\t" + "{");
                            output.Write("\t" + "fileImage = 'images/public/document-128.png';");
                            output.Write("\t" + "}");
                            output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "' + fileImage + '\" border=\"0\" alt=\"\" />');");
                            break;

                        default:
                            output.Write("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />');");
                            break;
                    }

                    output.Write("\t" + "item.addClass('success').find('p.status').html('" + SepFunctions.LangText("File has been successfully uploaded.") + "');");
                    break;
            }

            output.WriteLine("\t" + "$('#" + ID + "Existing').show();");
            output.WriteLine("\t" + "sIdentity" + ID + " = getIdentity();");
            output.WriteLine("\t" + "uploader" + ID + ".settings.url = '" + SepFunctions.GetInstallFolder(true) + "js/upload.ashx?Identity='+sIdentity" + ID + "+'&ContentID=" + ContentID + "&ControlID=" + ID + "&ModuleID=" + ModuleID + "&UserID=" + SepFunctions.UrlEncode(UserID) + "&UploadMode=" + Strings.ToString(Strings.ToString(Mode) == "SingleFile" ? "single" : "multiple") + "';");
            output.WriteLine("\t" + "$('#" + ID + "Bar').html('');");
            if (maxFiles > 0)
            {
                output.WriteLine("\t" + "if($('#" + ID + "log li').length >= " + maxFiles + ") {");
                output.WriteLine("\t" + "$('#" + ID + "container').hide();");
                output.WriteLine("\t" + "}");
            }

            output.WriteLine("}");
            if (ModuleID == 10)
            {
                output.WriteLine("function uploadToDownloads() {");
                output.WriteLine("        var params = new Object();");
                output.WriteLine("        params.FileID = '" + ContentID + "';");
                output.WriteLine("        params.CatID = $('#Category').val();");
                output.WriteLine("        params.CatType = '" + FileType + "';");
                output.WriteLine("        switch('" + Strings.LCase(Strings.ToString(FileType)) + "') {");
                output.WriteLine("          case 'audio':");
                output.WriteLine("            params.Field1 = $('#SongTitle').val();");
                output.WriteLine("            params.Field2 = $('#Album').val();");
                output.WriteLine("            params.Field3 = $('#Artist').val();;");
                output.WriteLine("            params.Field4 = '';");
                output.WriteLine("            break;");
                output.WriteLine("          case 'document':");
                output.WriteLine("            params.Field1 = $('#DocumentTitle').val();");
                output.WriteLine("            params.Field2 = $('#Description').val();");
                output.WriteLine("            params.Field3 = '';");
                output.WriteLine("            params.Field4 = '';");
                output.WriteLine("            break;");
                output.WriteLine("          case 'images':");
                output.WriteLine("            params.Field1 = $('#Caption').val();");
                output.WriteLine("            params.Field2 = '';");
                output.WriteLine("            params.Field3 = '';");
                output.WriteLine("            params.Field4 = '';");
                output.WriteLine("            break;");
                output.WriteLine("          case 'software':");
                output.WriteLine("            params.Field1 = $('#ApplicationName').val();");
                output.WriteLine("            params.Field2 = $('#Version').val();");
                output.WriteLine("            params.Field3 = $('#Price').val();");
                output.WriteLine("            params.Field4 = $('#AppDesc').val();");
                output.WriteLine("            break;");
                output.WriteLine("          case 'video':");
                output.WriteLine("            params.Field1 = $('#VideoTitle').val();");
                output.WriteLine("            params.Field2 = $('#VideoDesc').val();");
                output.WriteLine("            params.Field3 = '';");
                output.WriteLine("            params.Field4 = '';");
                output.WriteLine("            break;");
                output.WriteLine("          default:");
                output.WriteLine("            //do nothing");
                output.WriteLine("        }");
                output.WriteLine("        params.eDownload = 'false';");
                output.WriteLine("        params.Approved = 'true';");
                output.WriteLine("        params.FileName = '';");
                output.WriteLine("        params.PortalID = '" + SepFunctions.Get_Portal_ID() + "';");
                output.WriteLine("        $.ajax({");
                output.WriteLine("          type: 'POST',");
                output.WriteLine("          data: JSON.stringify(params),");
                output.WriteLine("          url: '" + sImageFolder + "api/downloads',");
                output.WriteLine("          dataType: 'json',");
                output.WriteLine("          contentType: 'application/json',");
                output.WriteLine("          complete: function (response) {");
                output.WriteLine("          },");
                output.WriteLine("          error: function (xhr, ajaxOptions, thrownError) {");
                output.WriteLine("            alert('There has been an error saving.\\n\\n' + xhr.responseText);");
                output.WriteLine("          },");
                output.WriteLine("          success: function (response) {");
                output.WriteLine("            $('#failureNotification').html('<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("File has been successfully uploaded.") + "</div>');");
                output.WriteLine("            $('.ModFieldset').hide();");
                output.WriteLine("            $('.ModFormDiv').hide();");
                output.WriteLine("          }");
                output.WriteLine("        });");
                output.WriteLine("}");
            }

            output.WriteLine("</script>");

            output.WriteLine("<div id=\"" + ID + "Div\">");
            output.WriteLine("<div id=\"" + ID + "Bar\"></div>");
            output.WriteLine("<div style=\"clear:both;\"></div>");
            output.WriteLine("</div>");

            if (imgCount >= maxFiles)
            {
                output.WriteLine("<script type=\"text/javascript\">");
                output.WriteLine("$(document).ready(function () {");
                output.Write("\t" + "$('#" + ID + "container').hide();");
                output.WriteLine("});");
                output.WriteLine("</script>");
            }
        }
    }
}