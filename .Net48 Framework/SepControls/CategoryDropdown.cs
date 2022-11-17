// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="CategoryDropdown.cs" company="SepCity, Inc.">
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
    /// Class CategoryDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [ValidationProperty("CatID")]
    [DefaultProperty("CatID")]
    [ToolboxData("<{0}:CategoryDropdown runat=server></{0}:CategoryDropdown>")]
    public class CategoryDropdown : WebControl
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
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CatID
        {
            get
            {
                var s = Context.Request.Form[ID];
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
            if (SepFunctions.showCategories() == false)
            {
                return;
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
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT CAT.CatID FROM Categories AS CAT WHERE CAT.Status <> -1 AND CAT.ListUnder='0'" + wclause, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                output.WriteLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"0\" />");
                                output.WriteLine("<script type=\"text/javascript\">");
                                output.WriteLine("$(document).ready(function () {");
                                output.WriteLine("$('#" + ID + "Label').parent().hide();");
                                output.WriteLine("});" + Environment.NewLine);
                                output.WriteLine("</script>");
                                return;
                            }
                        }
                    }
                }
            }

            if (SepFunctions.toLong(CatID) > 0)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT CAT.CategoryName,CAT.CatType,(SELECT TOP 1 SCAT.ListUnder FROM Categories AS SCAT WHERE SCAT.ListUnder=CAT.CatID" + Strings.Replace(wclause, "CAT.", "SCAT.") + ") AS HasSubs FROM Categories AS CAT WHERE CAT.CatID=@CatID" + wclause, conn))
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

            output.WriteLine("<script type=\"text/javascript\">");

            output.WriteLine("function openCategory(CatID, ModuleID, CategoryName, CatType, hasSubs, assignValue, Disabled) {");
            var modifyPortal = ModuleID == 60 ? "+'&ModifyPortal=True'" : string.Empty;
            if (Management)
            {
                output.WriteLine("assignValue = true;");
            }

            output.WriteLine("if(hasSubs == true) {");
            output.WriteLine("$.get('" + sImageFolder + "spadmin/drop_category.aspx?CatID='+CatID+'&PortalID=" + SepFunctions.Get_Portal_ID() + "&Disabled='+Disabled+'&ModuleID='+ModuleID" + modifyPortal + ", function(data) {");
            output.WriteLine("$('#" + ID + "Div').html(data);");
            output.WriteLine("});");
            output.WriteLine("}");
            output.WriteLine("if(CategoryName != '') {");
            if (Request.Item("ShowCat") == "True")
            {
                output.WriteLine("$.get('" + sImageFolder + "spadmin/drop_category.aspx?DoAction=GetHTML&CatID='+CatID+'&PortalID=" + SepFunctions.Get_Portal_ID() + "&ModuleID='+ModuleID" + modifyPortal + ", function(data) {");
                output.WriteLine("  tinyMCE.get('txtPageText').setContent(data);");
                output.WriteLine("});");
            }

            output.WriteLine("document.getElementById('" + ID + "CategoryName').innerHTML = unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Selected Category:")) + "') + ' ' + CategoryName;");
            output.WriteLine("if(assignValue == true) {");
            output.WriteLine("  document.getElementById('" + ID + "').value = CatID;");
            output.WriteLine("  if(hasSubs == false) {");
            output.WriteLine("    document.getElementById('" + ID + "Lowest').value = '1';");
            output.WriteLine("  } else {");
            output.WriteLine("    document.getElementById('" + ID + "Lowest').value = '0';");
            output.WriteLine("  }");
            output.WriteLine("};");

            output.WriteLine("} else {");
            output.WriteLine("document.getElementById('" + ID + "CategoryName').innerHTML = '';");
            output.WriteLine("document.getElementById('" + ID + "').value = '';");
            output.WriteLine("}");
            if (ModuleID == 10)
            {
                output.WriteLine("if(ModuleID == '10') {");

                var cAudio = new UploadFiles
                {
                    ID = "FileUpload",
                    FileType = UploadFiles.EFileTypes.Audio,
                    Mode = UploadFiles.EInputMode.SingleFile,
                    ModuleID = 10,
                    ContentID = FileID
                };
                var swAudio = new StringWriter();
                var htwAudio = new HtmlTextWriter(swAudio);
                cAudio.RenderControl(htwAudio);
                htwAudio.Dispose();
                swAudio.Dispose();
                cAudio.Dispose();

                var cDocument = new UploadFiles
                {
                    ID = "FileUpload",
                    FileType = UploadFiles.EFileTypes.Document,
                    Mode = UploadFiles.EInputMode.SingleFile,
                    ModuleID = 10,
                    ContentID = FileID
                };
                var swDocument = new StringWriter();
                var htwDocument = new HtmlTextWriter(swDocument);
                cDocument.RenderControl(htwDocument);
                htwDocument.Dispose();
                swDocument.Dispose();
                cDocument.Dispose();

                var cImages = new UploadFiles
                {
                    ID = "FileUpload",
                    FileType = UploadFiles.EFileTypes.Images,
                    Mode = UploadFiles.EInputMode.SingleFile,
                    ModuleID = 10,
                    ContentID = FileID
                };
                var swImages = new StringWriter();
                var htwImages = new HtmlTextWriter(swImages);
                cImages.RenderControl(htwImages);
                htwImages.Dispose();
                swImages.Dispose();
                cImages.Dispose();

                var cSoftware = new UploadFiles
                {
                    ID = "FileUpload",
                    FileType = UploadFiles.EFileTypes.Software,
                    Mode = UploadFiles.EInputMode.SingleFile,
                    ModuleID = 10,
                    ContentID = FileID
                };
                var swSoftware = new StringWriter();
                var htwSoftware = new HtmlTextWriter(swSoftware);
                cSoftware.RenderControl(htwSoftware);
                htwSoftware.Dispose();
                swSoftware.Dispose();
                cSoftware.Dispose();

                var cVideo = new UploadFiles
                {
                    ID = "FileUpload",
                    FileType = UploadFiles.EFileTypes.Video,
                    Mode = UploadFiles.EInputMode.SingleFile,
                    ModuleID = 10,
                    ContentID = FileID
                };
                var swVideo = new StringWriter();
                var htwVideo = new HtmlTextWriter(swVideo);
                cVideo.RenderControl(htwVideo);
                htwVideo.Dispose();
                swVideo.Dispose();
                cVideo.Dispose();

                output.WriteLine("document.getElementById('AudioRows').style.display='none';");
                output.WriteLine("document.getElementById('DocumentRows').style.display='none';");
                output.WriteLine("document.getElementById('ImageRows').style.display='none';");
                output.WriteLine("document.getElementById('SoftwareRows').style.display='none';");
                output.WriteLine("document.getElementById('VideoRows').style.display='none';");
                output.WriteLine("$('#PHFileUpload').html('');");
                output.WriteLine("switch(CatType) {");
                output.WriteLine("case 'Audio':");
                output.WriteLine("document.getElementById('AudioRows').style.display='';");
                output.WriteLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(swAudio)) + "'));");
                output.WriteLine("break;");
                output.WriteLine("case 'Document':");
                output.WriteLine("document.getElementById('DocumentRows').style.display='';");
                output.WriteLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(swDocument)) + "'));");
                output.WriteLine("break;");
                output.WriteLine("case 'Image':");
                output.WriteLine("document.getElementById('ImageRows').style.display='';");
                output.WriteLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(swImages)) + "'));");
                output.WriteLine("break;");
                output.WriteLine("case 'Software':");
                output.WriteLine("document.getElementById('SoftwareRows').style.display='';");
                output.WriteLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(swSoftware)) + "'));");
                output.WriteLine("break;");
                output.WriteLine("case 'Video':");
                output.WriteLine("document.getElementById('VideoRows').style.display='';");
                output.WriteLine("$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(swVideo)) + "'));");
                output.WriteLine("break;");
                output.WriteLine("default:");
                output.WriteLine("//do nothing");
                output.WriteLine("}");
                output.WriteLine("}");
            }

            output.WriteLine("}");

            output.WriteLine("$(document).ready(function () {");
            output.WriteLine("openCategory('" + SepFunctions.toLong(CatID) + "', '" + ModuleID + "', unescape('" + SepFunctions.EscQuotes(sCategoryName) + "'), unescape('" + SepFunctions.EscQuotes(sCatType) + "'), true, " + sAssignValue + ", '" + Strings.ToString(Disabled ? "true" : "false") + "');");
            if (SepFunctions.toLong(CatID) > 0)
            {
                output.WriteLine("$('#" + ID + "Lowest').val('1');");
            }

            output.WriteLine("});" + Environment.NewLine);

            output.WriteLine("</script>");

            output.WriteLine("<div id=\"" + ID + "Div\" class=\"CategoryDropdown\"></div>");
            output.WriteLine("<div id=\"" + ID + "CategoryName\" class=\"CategoryDropdownText\"></div>");
            output.WriteLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" />");
            output.WriteLine("<input type=\"hidden\" name=\"" + ID + "Lowest\" id=\"" + ID + "Lowest\" value=\"0\" />");
        }
    }
}