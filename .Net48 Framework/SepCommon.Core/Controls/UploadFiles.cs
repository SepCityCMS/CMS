// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UploadFiles.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class UploadFiles.
    /// </summary>
    public class UploadFiles
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
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

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
            var sReturn = string.Empty;

            if (iSize > 1024)
            {
                iSize = iSize / 1024;
                sReturn = iSize + " KB";
                if (iSize > 1024)
                {
                    iSize = iSize / 1024;
                    sReturn = iSize + " MB";
                    if (iSize > 1024)
                    {
                        iSize = iSize / 1024;
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
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var cUrl = "https://cdnjs.cloudflare.com/ajax/libs/plupload/2.1.8/plupload.full.min.js";

            output.AppendLine("<script type=\"text/javascript\" src=\"" + cUrl + "\">");

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

            output.AppendLine("<div id=\"" + ID + "Existing\" class=\"uploadcontrol\">");
            switch (FileType)
            {
                case EFileTypes.Images:
                    if (Strings.ToString(Mode) == "SingleFile")
                    {
                        output.AppendLine("<p>Select one of the following formats(jpg, png, gif), with a maximum size of " + SepFunctions.toLong(SepFunctions.Setup(992, "MaxImageSize")) + " KB's.</p>");
                    }
                    else
                    {
                        output.AppendLine("<p>Upload up to " + maxFiles + " image files(jpg, png, gif), each having maximum size of " + SepFunctions.toLong(SepFunctions.Setup(992, "MaxImageSize")) + " KB's.</p>");
                    }

                    break;
            }

            output.AppendLine("<div id=\"Success" + ID + "\"></div>");
            output.AppendLine("<p id=\"" + ID + "queuestatus\"></p>");
            output.AppendLine("<ol id=\"" + ID + "log\" class=\"uploadLog\">");
            if (!string.IsNullOrWhiteSpace(ContentID))
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
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

                    using (SqlCommand cmd = new SqlCommand("SELECT UploadID,ModuleID,FileName,FileSize,Weight FROM Uploads WHERE ModuleID=@ModuleID AND Approved='1' AND UniqueID=@UniqueID" + sControlID + wClause + " ORDER BY Weight", conn))
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
                                        output.AppendLine("<li id=\"li" + SepFunctions.openNull(RS["UploadID"]) + "\" class=\"success\">");
                                        output.AppendLine("<div class=\"uploadleft\">");
                                        output.AppendLine("<img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&ModuleID=" + SepFunctions.openNull(RS["ModuleID"]) + "&Size=thumb\" border=\"0\" alt=\"\" />");
                                        output.AppendLine("</div>");
                                        output.AppendLine("<div class=\"uploadright\">");
                                        output.AppendLine("File: <em>" + SepFunctions.openNull(RS["FileName"]) + "</em> (" + FormatSize(SepFunctions.toLong(SepFunctions.openNull(RS["FileSize"]))) + ") <span class=\"progressvalue\"></span>");
                                        if (maxFiles > 1)
                                        {
                                            if (SepFunctions.toLong(SepFunctions.openNull(RS["Weight"])) == 1)
                                            {
                                                output.AppendLine("<br/><span class=\"DefaultImage\" id=\"" + SepFunctions.openNull(RS["UploadID"]) + "\">" + SepFunctions.LangText("Default Image") + "</span>");
                                            }
                                            else
                                            {
                                                output.AppendLine("<br/><span class=\"DefaultImage\" id=\"" + SepFunctions.openNull(RS["UploadID"]) + "\"><a href=\"javascript:void(0)\" onclick=\"markDefault" + ID + "('" + SepFunctions.openNull(RS["UploadID"]) + "')\">" + SepFunctions.LangText("Mark as Default") + "</a></span>");
                                            }
                                        }

                                        output.AppendLine("<div><div style=\"width: 100%;\"></div></div>");
                                        output.AppendLine("<p class=\"status\"></p>");
                                        output.AppendLine("<span class=\"cancel\" style=\"display: none;\">&nbsp;</span>");
                                        output.AppendLine("</div>");
                                        output.AppendLine("<div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "('li" + imgCount + "', '" + SepFunctions.openNull(RS["UploadID"]) + "')\" alt=\"" + SepFunctions.LangText("Delete Image") + "\" /></div>");
                                        output.AppendLine("</li>");
                                        break;

                                    default:
                                        output.AppendLine("<li id=\"li" + SepFunctions.openNull(RS["UploadID"]) + "\" class=\"success\">");
                                        output.AppendLine("<div class=\"uploadleft\" style=\"width:128px;\">");
                                        switch (FileType)
                                        {
                                            case EFileTypes.Audio:
                                                output.AppendLine("<img src=\"" + sImageFolder + "images/public/audio-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Document:
                                                output.AppendLine("<img src=\"" + sImageFolder + "images/public/document-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Software:
                                                output.AppendLine("<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />");
                                                break;

                                            case EFileTypes.Video:
                                                output.AppendLine("<img src=\"" + sImageFolder + "images/public/video-128.png\" border=\"0\" alt=\"\" />");
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

                                                output.AppendLine("<img src=\"" + sImageFolder + fileImage + "\" border=\"0\" alt=\"\" />");
                                                break;

                                            default:
                                                output.AppendLine("<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />");
                                                break;
                                        }

                                        output.AppendLine("</div>");
                                        output.AppendLine("<div class=\"uploadright\">");
                                        output.AppendLine("File: <em>" + SepFunctions.openNull(RS["FileName"]) + "</em> (" + FormatSize(SepFunctions.toLong(SepFunctions.openNull(RS["FileSize"]))) + ") <span class=\"progressvalue\"></span>");
                                        output.AppendLine("<div><div style=\"width: 100%;\"></div></div>");
                                        output.AppendLine("<p class=\"status\"></p>");
                                        output.AppendLine("<span class=\"cancel\" style=\"display: none;\">&nbsp;</span>");
                                        output.AppendLine("</div>");
                                        output.AppendLine("<div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "('li" + imgCount + "', '" + SepFunctions.openNull(RS["UploadID"]) + "')\" alt=\"" + SepFunctions.LangText("Delete Image") + "\" /></div>");
                                        output.AppendLine("</li>");
                                        break;
                                }

                                imgCount = imgCount + 1;
                            }
                        }
                    }
                }
            }

            output.AppendLine("</ol>");
            output.AppendLine("</div>");

            output.AppendLine("<br />");

            output.AppendLine("<div id=\"" + ID + "container\">");
            if (Strings.ToString(Mode) == "SingleFile")
            {
                switch (FileType)
                {
                    case EFileTypes.Images:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select an Image File") + "\" />");
                        break;

                    case EFileTypes.Audio:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select an Audio File") + "\" />");
                        break;

                    case EFileTypes.Document:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a Document File") + "\" />");
                        break;

                    case EFileTypes.Software:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a Compressed File") + "\" />");
                        break;

                    case EFileTypes.Video:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a Video File") + "\" />");
                        break;

                    case EFileTypes.Any:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a File") + "\" />");
                        break;
                }
            }
            else
            {
                switch (FileType)
                {
                    case EFileTypes.Images:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Image Files") + "\" />");
                        break;

                    case EFileTypes.Audio:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Audio Files") + "\" />");
                        break;

                    case EFileTypes.Document:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Document Files") + "\" />");
                        break;

                    case EFileTypes.Software:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Compressed Files") + "\" />");
                        break;

                    case EFileTypes.Video:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select Video Files") + "\" />");
                        break;

                    case EFileTypes.Any:
                        output.AppendLine("<input type=\"button\" class=\"btn btn-light\" id=\"pickfiles" + ID + "\" value=\"" + SepFunctions.LangText("Select a File(s)") + "\" />");
                        break;
                }
            }

            output.AppendLine("</div>" + Environment.NewLine);

            output.AppendLine("<script type=\"text/javascript\">");
            output.AppendLine("var sIdentity" + ID + " = getIdentity();");
            output.AppendLine("var sFileId = 0;");
            output.AppendLine("var uploader" + ID + " = new plupload.Uploader({");
            output.AppendLine("\truntimes: 'html5,flash,silverlight,html4',");
            output.AppendLine("\tbrowse_button: 'pickfiles" + ID + "',");
            output.AppendLine("\tcontainer: document.getElementById('" + ID + "container'),");
            output.AppendLine("\turl: '" + sImageFolder + "js/upload.ashx?Identity='+sIdentity" + ID + "+'&ControlID=" + ID + "&ContentID=" + ContentID + "&ModuleID=" + ModuleID + "&UserID=" + SepFunctions.UrlEncode(UserID) + "&UploadMode=" + Strings.ToString(Strings.ToString(Mode) == "SingleFile" ? "single" : "multiple") + "',");
            output.AppendLine("\tflash_swf_url: '" + sImageFolder + "js/plupload/Moxie.swf',");
            output.AppendLine("\tsilverlight_xap_url: '" + sImageFolder + "js/plupload/Moxie.xap',");
            output.AppendLine("  multi_selection: false,");

            output.AppendLine("\tfilters: {");
            if (FileType == EFileTypes.Images)
            {
                output.AppendLine("\t\tmax_file_size: '" + SepFunctions.toLong(SepFunctions.Setup(992, "MaxImageSize")) + "kb',");
            }
            else
            {
                output.AppendLine("\t\tmax_file_size: '" + SepFunctions.toLong(SepFunctions.Setup(10, "LibraryMaxUpload")) + "mb',");
            }

            if (FileType != EFileTypes.Any)
            {
                output.AppendLine("\t\tmime_types: [");
                switch (FileType)
                {
                    case EFileTypes.Images:
                        output.AppendLine("      {title: \"Image files\", extensions: \"jpg,gif,png\"}");
                        break;

                    case EFileTypes.Audio:
                        output.AppendLine("      {title: \"Audio files\", extensions: \"mp3,wav\"}");
                        break;

                    case EFileTypes.Document:
                        output.AppendLine("      {title: \"Document files\", extensions: \"doc,docx,txt,rtf,pdf\"}");
                        break;

                    case EFileTypes.Software:
                        output.AppendLine("      {title: \"Compress files\", extensions: \"zip,rar\"}");
                        break;

                    case EFileTypes.Video:
                        output.AppendLine("      {title: \"Video files\", extensions: \"mp4,avi,mpeg,flv\"}");
                        break;
                }

                output.AppendLine("\t\t]");
            }

            output.AppendLine("\t},");

            output.AppendLine("\tinit: {");
            if (ModuleID == 10 && SepFunctions.Setup(992, "CatLowestLvl") == "Yes")
            {
                output.AppendLine("\t\tBrowse: function() {");
                output.AppendLine("        if($('#CategoryLowest').val() != '1') {");
                output.AppendLine("          alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("You must select the lowest level of category available.")) + "'));");
                output.AppendLine("          return false;");
                output.AppendLine("        } else {");
                output.AppendLine("          return true;");
                output.AppendLine("        }");
                output.AppendLine("\t\t},");
            }

            output.AppendLine("\t\tFilesAdded: function(up, files) {");
            output.AppendLine("          $('#" + ID + "Bar').html('<strong>' + unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Please wait while files are being uploaded.")) + "') + '</strong>');");
            output.AppendLine("\t\t\tuploader" + ID + ".start();");
            output.AppendLine("\t\t},");

            output.AppendLine("\t\tUploadComplete: function(up, files) {");
            output.AppendLine("\t\t\tuploadFinished" + ID + "(files[0].name);");
            if (ModuleID == 10)
            {
                output.AppendLine("        uploadToDownloads();");
            }

            output.AppendLine("\t\t},");

            output.AppendLine("\t\tError: function(up, err) {");
            output.AppendLine("          $('#" + ID + "Bar').html('Error #' + err.code + ': ' + err.message);");
            output.AppendLine("\t\t}");
            output.AppendLine("\t}");
            output.AppendLine("});");

            output.AppendLine("uploader" + ID + ".bind('FilesAdded', function(up, files) {");
            output.AppendLine("uploader" + ID + ".start();");
            output.AppendLine("});");

            output.AppendLine("uploader" + ID + ".bind('QueueChanged', function (up, files) {");
            output.AppendLine("uploader" + ID + ".start();");
            output.AppendLine("up.refresh();");
            output.AppendLine("});");

            output.AppendLine("uploader" + ID + ".init();");

            output.AppendLine("function markDefault" + ID + "(sUploadId) {");
            if (maxFiles > 1)
            {
                output.Append("\t" + "$.ajax({");
                output.Append("\t" + "\t" + "url: '" + sImageFolder + "spadmin/image_upload_default.aspx?UploadID='+sUploadId+'&ModuleID=" + ModuleID + "&UserID=" + UserID + "',");
                output.Append("\t" + "\t" + "success: function(data) {");
                output.Append("\t" + "\t" + "$('#" + ID + "log li .DefaultImage').each(function(index) {");
                output.Append("\t" + "\t" + "$(this).html(unescape('" + SepFunctions.EscQuotes("<a href=\"javascript:void(0)\" onclick=\"markDefault" + ID + "('") + "') + $(this).attr('id') + unescape('" + SepFunctions.EscQuotes("')\">" + SepFunctions.LangText("Mark as Default") + "</a>") + "'));");
                output.Append("\t" + "\t" + "});");
                output.Append("\t" + "\t" + "$('#li'+sUploadId+' .DefaultImage').html(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Default Image")) + "'));");
                output.Append("\t" + "\t" + "}");
                output.Append("\t" + "});");
            }

            output.AppendLine("}");

            output.AppendLine("function deleteFile" + ID + "(sId, sUploadId) {");
            output.Append("\t" + "$.ajax({");
            output.Append("\t" + "\t" + "url: '" + sImageFolder + "spadmin/image_upload_delete.aspx?UploadID='+sUploadId,");
            output.Append("\t" + "\t" + "success: function(data) {");
            output.Append("\t" + "\t" + "$('#li'+sUploadId).remove();");
            output.Append("\t" + "\t" + "$('#Success" + ID + "').html(data);");
            output.Append("\t" + "$('#" + ID + "container').show();");
            output.Append("\t" + "\t" + "}");
            output.Append("\t" + "});");
            output.AppendLine("}");

            output.AppendLine("function uploadFinished" + ID + "(fileName) {");
            output.Append("\t" + "sFileId = (sIdentity" + ID + " + " + imgCount + ");");
            switch (FileType)
            {
                case EFileTypes.Images:
                    output.Append("\t" + "var listitem='<li id=\"li'+sFileId+'\"><div class=\"uploadleft\" style=\"width:128px;\"></div>'+");
                    output.Append("\t" + "'<div class=\"uploadright\"><p class=\"status\">" + SepFunctions.LangText("Pending") + "</p></div>'+");
                    output.Append("\t" + "'</div><div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "(\\'li" + imgCount + "\\', \\''+sFileId+'\\')\" alt=\"" + SepFunctions.LangText("Delete Image") + "\" /></div></li>';");
                    output.Append("\t" + "$('#" + ID + "log').append(listitem);");
                    output.Append("\t" + "var item=$('#" + ID + "log li#li'+sFileId);");
                    output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID='+sIdentity" + ID + "+'&Size=thumb&ModuleID=" + ModuleID + "\" border=\"0\" alt=\"\" />');");
                    output.Append("\t" + "item.addClass('success').find('p.status').html('" + SepFunctions.LangText("Image has been successfully uploaded.") + "');");
                    if (maxFiles > 1)
                    {
                        output.AppendLine("item.find('p.status').append(unescape('" + SepFunctions.EscQuotes("<br/><span class=\"DefaultImage\" id=\"") + "') + sFileId + unescape('" + SepFunctions.EscQuotes("\"><a href=\"javascript:void(0)\" onclick=\"markDefault" + ID + "('") + "') + sFileId + unescape('" + SepFunctions.EscQuotes("')\">" + SepFunctions.LangText("Mark as Default") + "</a></span>") + "'));");
                    }

                    break;

                default:
                    output.Append("\t" + "var listitem='<li id=\"li'+sFileId+'\"><div class=\"uploadleft\" style=\"width:128px;\"></div>'+");
                    output.Append("\t" + "'<p class=\"status\">" + SepFunctions.LangText("Pending") + "</p>'+");
                    output.Append("\t" + "'</div><div class=\"uploaddelete\"><img src=\"" + sImageFolder + "spadmin/images/delete.png\" border=\"0\" style=\"cursor:pointer;\" onclick=\"deleteFile" + ID + "(\\'li" + imgCount + "\\', \\''+sFileId+'\\')\" alt=\"" + SepFunctions.LangText("Delete File") + "\" /></div></li>';");
                    output.Append("\t" + "$('#" + ID + "log').append(listitem);");
                    output.Append("\t" + "var item=$('#" + ID + "log li#li'+sFileId);");
                    switch (FileType)
                    {
                        case EFileTypes.Audio:
                            output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/audio-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Document:
                            output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/document-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Software:
                            output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Video:
                            output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/video-128.png\" border=\"0\" alt=\"\" />');");
                            break;

                        case EFileTypes.Any:
                            output.Append("\t" + "var fileImage = 'images/public/any-file-128.png';");
                            output.Append("\t" + "var fileExtension = fileName.split('.');");
                            output.Append("\t" + "try{fileExtension = fileExtension[fileExtension.length - 1];}catch(e){};");
                            output.Append("\t" + "if (fileExtension == 'png' || fileExtension == 'gif' || fileExtension == 'jpg' || fileExtension == 'jpeg' || fileExtension == 'bmp')");
                            output.Append("\t" + "{");
                            output.Append("\t" + "fileImage = 'spadmin/show_image.aspx?UploadID='+sIdentity" + ID + "+'&Size=thumb&ModuleID=" + ModuleID + "';");
                            output.Append("\t" + "}");
                            output.Append("\t" + "if (fileExtension == 'wav' || fileExtension == 'mp3')");
                            output.Append("\t" + "{");
                            output.Append("\t" + "fileImage = 'images/public/audio-128.png';");
                            output.Append("\t" + "}");
                            output.Append("\t" + "if (fileExtension == 'txt' || fileExtension == 'rtf' || fileExtension == 'pdf' || fileExtension == 'docx' || fileExtension == 'doc')");
                            output.Append("\t" + "{");
                            output.Append("\t" + "fileImage = 'images/public/document-128.png';");
                            output.Append("\t" + "}");
                            output.Append("\t" + "if (fileExtension == 'mp4' || fileExtension == 'avi' || fileExtension == 'flv' || fileExtension == 'mpeg')");
                            output.Append("\t" + "{");
                            output.Append("\t" + "fileImage = 'images/public/document-128.png';");
                            output.Append("\t" + "}");
                            output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "' + fileImage + '\" border=\"0\" alt=\"\" />');");
                            break;

                        default:
                            output.Append("\t" + "$('#li'+sFileId+' .uploadleft').html('<img src=\"" + sImageFolder + "images/public/software-128.png\" border=\"0\" alt=\"\" />');");
                            break;
                    }

                    output.Append("\t" + "item.addClass('success').find('p.status').html('" + SepFunctions.LangText("File has been successfully uploaded.") + "');");
                    break;
            }

            output.AppendLine("\t" + "$('#" + ID + "Existing').show();");
            output.AppendLine("\t" + "sIdentity" + ID + " = getIdentity();");
            output.AppendLine("\t" + "uploader" + ID + ".settings.url = '" + SepFunctions.GetInstallFolder(true) + "js/upload.ashx?Identity='+sIdentity" + ID + "+'&ContentID=" + ContentID + "&ControlID=" + ID + "&ModuleID=" + ModuleID + "&UserID=" + SepFunctions.UrlEncode(UserID) + "&UploadMode=" + Strings.ToString(Strings.ToString(Mode) == "SingleFile" ? "single" : "multiple") + "';");
            output.AppendLine("\t" + "$('#" + ID + "Bar').html('');");
            if (maxFiles > 0)
            {
                output.AppendLine("\t" + "if($('#" + ID + "log li').length >= " + maxFiles + ") {");
                output.AppendLine("\t" + "$('#" + ID + "container').hide();");
                output.AppendLine("\t" + "}");
            }

            output.AppendLine("}");
            if (ModuleID == 10)
            {
                output.AppendLine("function uploadToDownloads() {");
                output.AppendLine("        var params = new Object();");
                output.AppendLine("        params.FileID = '" + ContentID + "';");
                output.AppendLine("        params.CatID = $('#Category').val();");
                output.AppendLine("        params.CatType = '" + FileType + "';");
                output.AppendLine("        switch('" + Strings.LCase(Strings.ToString(FileType)) + "') {");
                output.AppendLine("          case 'audio':");
                output.AppendLine("            params.Field1 = $('#SongTitle').val();");
                output.AppendLine("            params.Field2 = $('#Album').val();");
                output.AppendLine("            params.Field3 = $('#Artist').val();;");
                output.AppendLine("            params.Field4 = '';");
                output.AppendLine("            break;");
                output.AppendLine("          case 'document':");
                output.AppendLine("            params.Field1 = $('#DocumentTitle').val();");
                output.AppendLine("            params.Field2 = $('#Description').val();");
                output.AppendLine("            params.Field3 = '';");
                output.AppendLine("            params.Field4 = '';");
                output.AppendLine("            break;");
                output.AppendLine("          case 'images':");
                output.AppendLine("            params.Field1 = $('#Caption').val();");
                output.AppendLine("            params.Field2 = '';");
                output.AppendLine("            params.Field3 = '';");
                output.AppendLine("            params.Field4 = '';");
                output.AppendLine("            break;");
                output.AppendLine("          case 'software':");
                output.AppendLine("            params.Field1 = $('#ApplicationName').val();");
                output.AppendLine("            params.Field2 = $('#Version').val();");
                output.AppendLine("            params.Field3 = $('#Price').val();");
                output.AppendLine("            params.Field4 = $('#AppDesc').val();");
                output.AppendLine("            break;");
                output.AppendLine("          case 'video':");
                output.AppendLine("            params.Field1 = $('#VideoTitle').val();");
                output.AppendLine("            params.Field2 = $('#VideoDesc').val();");
                output.AppendLine("            params.Field3 = '';");
                output.AppendLine("            params.Field4 = '';");
                output.AppendLine("            break;");
                output.AppendLine("          default:");
                output.AppendLine("            //do nothing");
                output.AppendLine("        }");
                output.AppendLine("        params.eDownload = 'false';");
                output.AppendLine("        params.Approved = 'true';");
                output.AppendLine("        params.FileName = '';");
                output.AppendLine("        params.PortalID = '" + SepFunctions.Get_Portal_ID() + "';");
                output.AppendLine("        $.ajax({");
                output.AppendLine("          type: 'POST',");
                output.AppendLine("          data: JSON.stringify(params),");
                output.AppendLine("          url: '" + sImageFolder + "api/downloads',");
                output.AppendLine("          dataType: 'json',");
                output.AppendLine("          contentType: 'application/json',");
                output.AppendLine("          complete: function (response) {");
                output.AppendLine("          },");
                output.AppendLine("          error: function (xhr, ajaxOptions, thrownError) {");
                output.AppendLine("            alert('There has been an error saving.\\n\\n' + xhr.responseText);");
                output.AppendLine("          },");
                output.AppendLine("          success: function (response) {");
                output.AppendLine("            $('#failureNotification').html('<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("File has been successfully uploaded.") + "</div>');");
                output.AppendLine("            $('.ModFieldset').hide();");
                output.AppendLine("            $('.ModFormDiv').hide();");
                output.AppendLine("          }");
                output.AppendLine("        });");
                output.AppendLine("}");
            }

            output.AppendLine("</script>");

            output.AppendLine("<div id=\"" + ID + "Div\">");
            output.AppendLine("<div id=\"" + ID + "Bar\"></div>");
            output.AppendLine("<div style=\"clear:both;\"></div>");
            output.AppendLine("</div>");

            if (imgCount >= maxFiles)
            {
                output.AppendLine("<script type=\"text/javascript\">");
                output.AppendLine("$(document).ready(function () {");
                output.Append("\t" + "$('#" + ID + "container').hide();");
                output.AppendLine("});");
                output.AppendLine("</script>");
            }

            return output.ToString();
        }
    }
}