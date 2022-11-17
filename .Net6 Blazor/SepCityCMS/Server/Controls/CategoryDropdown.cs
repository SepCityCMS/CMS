// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CategoryDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class CategoryDropdown.
    /// </summary>
    public class CategoryDropdown
    {
        /// <summary>
        /// The m cat identifier
        /// </summary>
        private string m_CatID;

        /// <summary>
        /// The m disabled
        /// </summary>
        private bool m_Disabled;

        /// <summary>
        /// The m file identifier
        /// </summary>
        private string m_FileID;

        /// <summary>
        /// The m management
        /// </summary>
        private bool m_Management;

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public string CatID
        {
            get
            {
                var s = Request.Item(ID);
                if (s == null)
                {
                    s = Strings.ToString(m_CatID);
                }

                if (string.IsNullOrWhiteSpace(s))
                {
                    return "0";
                }

                return s;
            }

            set => m_CatID = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CategoryDropdown" /> is disabled.
        /// </summary>
        /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
        public bool Disabled
        {
            get
            {
                var s = Convert.ToBoolean(m_Disabled);
                return s;
            }

            set => m_Disabled = value;
        }

        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>The file identifier.</value>
        public string FileID
        {
            get => Strings.ToString(m_FileID);

            set => m_FileID = value;
        }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CategoryDropdown" /> is management.
        /// </summary>
        /// <value><c>true</c> if management; otherwise, <c>false</c>.</value>
        public bool Management
        {
            get
            {
                var s = Convert.ToBoolean(m_Management);
                return s;
            }

            set => m_Management = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.showCategories() == false)
            {
                return output.ToString();
            }

            var sCategoryName = string.Empty;
            var sCatType = string.Empty;
            var sAssignValue = string.Empty;
            var wclause = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (ModuleID > 0)
            {
                var sPortal = Strings.ToString(ModuleID == 60 ? "0" : Strings.ToString(SepFunctions.Get_Portal_ID()));
                wclause = " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID=" + ModuleID + " AND CatID=CAT.CatID AND Status <> -1) AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesPortals WHERE (PortalID=" + sPortal + " OR PortalID = -1) AND CatID=CAT.CatID AND Status <> -1)";
            }

            if (ModuleID != 10 && ModuleID != 12)
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT CAT.CatID FROM Categories AS CAT WHERE CAT.Status <> -1 AND CAT.ListUnder='0'" + wclause, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                output.AppendLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"0\" />");
                                output.AppendLine("<script type=\"text/javascript\">");
                                output.AppendLine("$(document).ready(function () {");
                                output.AppendLine("$('#" + ID + "Label').parent().hide();");
                                output.AppendLine("});" + Environment.NewLine);
                                output.AppendLine("</script>");
                                return output.ToString();
                            }
                        }
                    }
                }
            }

            if (SepFunctions.toLong(CatID) > 0)
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT CAT.CategoryName,CAT.CatType,(SELECT TOP 1 SCAT.ListUnder FROM Categories AS SCAT WHERE SCAT.ListUnder=CAT.CatID" + Strings.Replace(wclause, "CAT.", "SCAT.") + ") AS HasSubs FROM Categories AS CAT WHERE CAT.CatID=@CatID" + wclause, conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", CatID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sCategoryName = SepFunctions.openNull(RS["CategoryName"]);
                                sCatType = SepFunctions.openNull(RS["CatType"]);
                                if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes")
                                {
                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["HasSubs"])) && SepFunctions.openNull(RS["HasSubs"]) != "0")
                                    {
                                        sAssignValue = "false";
                                    }
                                    else
                                    {
                                        sAssignValue = "true";
                                    }
                                }
                                else
                                {
                                    sAssignValue = "true";
                                }
                            }
                            else
                            {
                                sAssignValue = "false";
                            }
                        }
                    }
                }
            }
            else
            {
                sAssignValue = "false";
            }

            output.AppendLine("<script type=\"text/javascript\">");

            output.AppendLine("function openCategory(CatID, ModuleID, CategoryName, CatType, hasSubs, assignValue, Disabled) {");
            var modifyPortal = ModuleID == 60 ? "+'&ModifyPortal=True'" : string.Empty;
            if (Management)
            {
                output.AppendLine("assignValue = true;");
            }

            output.AppendLine("if(hasSubs == true) {");
            output.AppendLine("$.get('" + sImageFolder + "spadmin/drop_category.aspx?CatID='+CatID+'&PortalID=" + SepFunctions.Get_Portal_ID() + "&Disabled='+Disabled+'&ModuleID='+ModuleID" + modifyPortal + ", function(data) {");
            output.AppendLine("$('#" + ID + "Div').html(data);");
            output.AppendLine("});");
            output.AppendLine("}");
            output.AppendLine("if(CategoryName != '') {");
            if (Request.Item("ShowCat") == "True")
            {
                output.AppendLine("$.get('" + sImageFolder + "spadmin/drop_category.aspx?DoAction=GetHTML&CatID='+CatID+'&PortalID=" + SepFunctions.Get_Portal_ID() + "&ModuleID='+ModuleID" + modifyPortal + ", function(data) {");
                output.AppendLine("  tinyMCE.get('txtPageText').setContent(data);");
                output.AppendLine("});");
            }

            output.AppendLine("document.getElementById('" + ID + "CategoryName').innerHTML = unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Selected Category:")) + "') + ' ' + CategoryName;");
            output.AppendLine("if(assignValue == true) {");
            output.AppendLine("  document.getElementById('" + ID + "').value = CatID;");
            output.AppendLine("  if(hasSubs == false) {");
            output.AppendLine("    document.getElementById('" + ID + "Lowest').value = '1';");
            output.AppendLine("  } else {");
            output.AppendLine("    document.getElementById('" + ID + "Lowest').value = '0';");
            output.AppendLine("  }");
            output.AppendLine("};");

            output.AppendLine("} else {");
            output.AppendLine("document.getElementById('" + ID + "CategoryName').innerHTML = '';");
            output.AppendLine("document.getElementById('" + ID + "').value = '';");
            output.AppendLine("}");
            if (ModuleID == 10)
            {
                output.AppendLine("if(ModuleID == '10') {");

                var cAudio = new UploadFiles();
                cAudio.ID = "FileUpload";
                cAudio.FileType = UploadFiles.EFileTypes.Audio;
                cAudio.Mode = UploadFiles.EInputMode.SingleFile;
                cAudio.ModuleID = 10;
                cAudio.ContentID = FileID;

                var cDocument = new UploadFiles();
                cDocument.ID = "FileUpload";
                cDocument.FileType = UploadFiles.EFileTypes.Document;
                cDocument.Mode = UploadFiles.EInputMode.SingleFile;
                cDocument.ModuleID = 10;
                cDocument.ContentID = FileID;

                var cImages = new UploadFiles();
                cImages.ID = "FileUpload";
                cImages.FileType = UploadFiles.EFileTypes.Images;
                cImages.Mode = UploadFiles.EInputMode.SingleFile;
                cImages.ModuleID = 10;
                cImages.ContentID = FileID;

                var cSoftware = new UploadFiles();
                cSoftware.ID = "FileUpload";
                cSoftware.FileType = UploadFiles.EFileTypes.Software;
                cSoftware.Mode = UploadFiles.EInputMode.SingleFile;
                cSoftware.ModuleID = 10;
                cSoftware.ContentID = FileID;

                var cVideo = new UploadFiles();
                cVideo.ID = "FileUpload";
                cVideo.FileType = UploadFiles.EFileTypes.Video;
                cVideo.Mode = UploadFiles.EInputMode.SingleFile;
                cVideo.ModuleID = 10;
                cVideo.ContentID = FileID;

                output.AppendLine("document.getElementById('AudioRows').style.display='none';");
                output.AppendLine("document.getElementById('DocumentRows').style.display='none';");
                output.AppendLine("document.getElementById('ImageRows').style.display='none';");
                output.AppendLine("document.getElementById('SoftwareRows').style.display='none';");
                output.AppendLine("document.getElementById('VideoRows').style.display='none';");
                output.AppendLine("$('#PHFileUpload').html('');");
                output.AppendLine("switch(CatType) {");
                output.AppendLine("case 'Audio':");
                output.AppendLine("document.getElementById('AudioRows').style.display='';");
                output.AppendLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(cAudio.Render()) + "'));");
                output.AppendLine("break;");
                output.AppendLine("case 'Document':");
                output.AppendLine("document.getElementById('DocumentRows').style.display='';");
                output.AppendLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(cDocument.Render()) + "'));");
                output.AppendLine("break;");
                output.AppendLine("case 'Image':");
                output.AppendLine("document.getElementById('ImageRows').style.display='';");
                output.AppendLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(cImages.Render())) + "'));");
                output.AppendLine("break;");
                output.AppendLine("case 'Software':");
                output.AppendLine("document.getElementById('SoftwareRows').style.display='';");
                output.AppendLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(cSoftware.Render())) + "'));");
                output.AppendLine("break;");
                output.AppendLine("case 'Video':");
                output.AppendLine("document.getElementById('VideoRows').style.display='';");
                output.AppendLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(cVideo.Render())) + "'));");
                output.AppendLine("break;");
                output.AppendLine("default:");
                output.AppendLine("//do nothing");
                output.AppendLine("}");
                output.AppendLine("}");
            }

            output.AppendLine("}");

            output.AppendLine("$(document).ready(function () {");
            output.AppendLine("openCategory('" + SepFunctions.toLong(CatID) + "', '" + ModuleID + "', unescape('" + SepFunctions.EscQuotes(sCategoryName) + "'), unescape('" + SepFunctions.EscQuotes(sCatType) + "'), true, " + sAssignValue + ", '" + Strings.ToString(Disabled ? "true" : "false") + "');");
            if (SepFunctions.toLong(CatID) > 0)
            {
                output.AppendLine("$('#" + ID + "Lowest').val('1');");
            }

            output.AppendLine("});" + Environment.NewLine);

            output.AppendLine("</script>");

            output.AppendLine("<div id=\"" + ID + "Div\" class=\"CategoryDropdown\"></div>");
            output.AppendLine("<div id=\"" + ID + "CategoryName\" class=\"CategoryDropdownText\"></div>");
            output.AppendLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" />");
            output.AppendLine("<input type=\"hidden\" name=\"" + ID + "Lowest\" id=\"" + ID + "Lowest\" value=\"0\" />");
            return output.ToString();
        }
    }
}