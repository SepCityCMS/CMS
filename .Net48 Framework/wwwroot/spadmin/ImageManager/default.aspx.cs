// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="default.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class _default6.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class _default6 : Page
    {
        /// <summary>
        /// The b allow manage
        /// </summary>
        private readonly bool bAllowManage = true;

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The b use absolute URL
        /// </summary>
        private bool bUseAbsoluteUrl;

        /// <summary>
        /// The s base
        /// </summary>
        private string sBase = string.Empty;

        /// <summary>
        /// The s current directory
        /// </summary>
        private string sCurrentDirectory = string.Empty;

        /// <summary>
        /// The s image folder
        /// </summary>
        private string sImageFolder = string.Empty;

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the file URL.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public string GetFileUrl(string s)
        {
            if (bUseAbsoluteUrl)
                return AppSrvPath() + s;
            else
                return s;
        }

        /// <summary>
        /// Previews the specified s icon.
        /// </summary>
        /// <param name="sIcon">The s icon.</param>
        /// <returns>System.String.</returns>
        public string Preview(string sIcon)
        {
            string sHTML;
            if (string.IsNullOrWhiteSpace(sIcon))
                sHTML = string.Empty;
            else if (sIcon.Contains("?path="))
                sHTML = "<a href=\"#\"  id=\"Delete" + SepFunctions.GetIdentity() + "\" onclick=\"document.getElementById('" + hidFilesToDel.ClientID + "').value='" + sIcon + "';document.getElementById('" + btnDelete2.ClientID + "').click();return false;\">" + SepFunctions.LangText("Delete") + "</a>";
            else
                sHTML = "<img src=\"" + sIcon + "\">";

            return sHTML;
        }

        /// <summary>
        /// Shows the CheckBox.
        /// </summary>
        /// <param name="sUrl">The s URL.</param>
        /// <returns>System.String.</returns>
        public string ShowCheckBox(string sUrl)
        {
            string sHTML;
            if (sUrl.Contains("?path="))
            {
                // This is a Folder
                sHTML = "<img src=\"" + sImageFolder + "spadmin/ImageManager/images/ico_folder.gif\"><input name=\"chkSelect\" style=\"display:none\" type=\"checkbox\" class=\"ignore\" />";

                // Hide checkbox, krn user tdk punya role Resource Manager
                if (panelSpecial.Visible == false)
                    sHTML = "<img src=\"" + sImageFolder + "spadmin/ImageManager/images/ico_folder.gif\">";
            }
            else
            {
                // This is a File
                sHTML = "<input name=\"chkSelect\" type=\"checkbox\" class=\"ignore\" />";

                // Hide checkbox, krn user tdk punya role Resource Manager
                if (panelSpecial.Visible == false)
                    sHTML = string.Empty;
            }

            return sHTML + "<input name=\"hidSelect\" type=\"hidden\" value=\"" + sUrl + "\" class=\"ignore\" /> ";
        }

        /// <summary>
        /// Shows the link.
        /// </summary>
        /// <param name="sUrl">The s URL.</param>
        /// <param name="sFileName">Name of the s file.</param>
        /// <param name="sIndex">Index of the s.</param>
        /// <returns>System.String.</returns>
        public string ShowLink(string sUrl, string sFileName, string sIndex)
        {
            string sHTML;
            if (sUrl.Contains("?path="))
                sHTML = "<a href=\"" + sUrl + "&c=" + Request.QueryString["c"] + "&Editor=" + SepCommon.SepCore.Request.Item("Editor") + "&RelativeURLs=" + SepCommon.SepCore.Request.Item("RelativeURLs") + "\" name=\"Folder\">" + sFileName + "</a>";
            else
                sHTML = "<a href=\"javascript:selectImage(" + sIndex + ");\">" + sFileName + "</a>";

            return sHTML;
        }

        /// <summary>
        /// Applications the SRV path.
        /// </summary>
        /// <returns>System.String.</returns>
        protected string AppSrvPath()
        {
            var sDomain = SepFunctions.GetMasterDomain(false);

            if (Strings.Right(sDomain, 1) == "/") sDomain = Strings.Left(sDomain, Strings.Len(sDomain) - 1);

            return sDomain;
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (var Item in hidFilesToDel.Value.Split('|'))
                if (File.Exists(Server.MapPath(Item)))
                    File.Delete(Server.MapPath(Item));

            showFiles(false);
        }

        /// <summary>
        /// Handles the Click event of the btnDelete2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnDelete2_Click(object sender, EventArgs e)
        {
            var Item = hidFilesToDel.Value;
            var sDirectory = Server.MapPath(sBase) + Server.UrlDecode(Item.Substring(Item.LastIndexOf("=", StringComparison.OrdinalIgnoreCase) + 1));

            if (Directory.Exists(sDirectory)) Directory.Delete(sDirectory);

            showFiles(false);
        }

        /// <summary>
        /// Handles the Click event of the btnNewFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnNewFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(sCurrentDirectory + "\\" + txtNewFolder.Value)) lblUploadStatus.Text = SepFunctions.LangText("Folder already exists");
            else
                Directory.CreateDirectory(sCurrentDirectory + "\\" + txtNewFolder.Value);

            showFiles(false);
            txtNewFolder.Value = string.Empty;
        }

        /// <summary>
        /// Handles the Click event of the btnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var _with1 = FileUpload1.PostedFile;
            var sExt = _with1.FileName.Substring(_with1.FileName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase) + 1);

            if (!(_with1.ContentType == "application/octet-stream" || _with1.ContentType == "application/xml" || sExt == "cgi" || sExt == "pl" || sExt == "asp" || sExt == "aspx" || sExt == "php"))
            {
                if (SepFunctions.CompareKeys("|2|") == false)
                {
                    var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(FileUpload1.PostedFile.InputStream.Length)) + 2];
                    FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, FileData, PortalID, ControlID, UserRates, TotalRates, Weight) VALUES (@UploadID, @UniqueID, @UserID, '7', @FileName, @FileSize, @ContentType, '1', '1', @DatePosted, @FileData, @PortalID, '', '0', '0', '0')", conn))
                        {
                            cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            cmd.Parameters.AddWithValue("@FileName", FileUpload1.FileName);
                            cmd.Parameters.AddWithValue("@FileSize", imageBytes.LongLength);
                            cmd.Parameters.AddWithValue("@ContentType", FileUpload1.PostedFile.ContentType);
                            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                            cmd.Parameters.AddWithValue("@FileData", imageBytes);
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    if (!Directory.Exists(sCurrentDirectory + "\\")) Directory.CreateDirectory(sCurrentDirectory + "\\");

                    FileUpload1.SaveAs(sCurrentDirectory + "\\" + FileUpload1.FileName);
                    lblUploadStatus.Text = string.Empty;
                    var sResMapPath = Server.MapPath(sBase);
                    var sResources = sBase;
                    if (sCurrentDirectory.Length > sResMapPath.Length) sResources = sResources + sCurrentDirectory.Substring(sResMapPath.Length).Replace("\\", "/") + "/";
                    string sVirtualPath = sResources + FileUpload1.FileName;
                    if (bUseAbsoluteUrl) txtURL.Value = AppSrvPath() + sVirtualPath;
                    else txtURL.Value = sVirtualPath;
                }

                lblUploadStatus.Text = SepFunctions.LangText("File has been successfully uploaded.");
            }
            else
            {
                lblUploadStatus.Text = SepFunctions.LangText("Upload failed. File type not allowed.");
            }

            showFiles(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the GridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            showFiles(false);
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (SepCommon.SepCore.Request.Item("RelativeURLs") == "True") bUseAbsoluteUrl = true;

            sImageFolder = SepFunctions.GetInstallFolder(true);
            sBase = sImageFolder + "skins/images/";

            // ~~~ Localization ~~~
            idTitle.Text = SepFunctions.LangText("Image Manager");
            GridView1.Columns[1].HeaderText = SepFunctions.LangText("File Name");
            GridView1.Columns[2].HeaderText = SepFunctions.LangText("Last Updated");
            GridView1.Columns[3].HeaderText = string.Empty;
            GridView1.Columns[4].HeaderText = string.Empty;
            lblSource.Text = SepFunctions.LangText("Source");
            btnUpload.Text = SepFunctions.LangText("Upload");
            btnClose.Text = SepFunctions.LangText("Close");
            btnOk.Text = SepFunctions.LangText("Ok");
            lblFolder.Text = SepFunctions.LangText("Folder:");
            btnNewFolder.Text = SepFunctions.LangText("Create Folder");

            // ~~~ /Localization ~~~
            if (!bAllowManage) panelSpecial.Visible = false;

            // ~~~ Show Files ~~~
            if (SepFunctions.CompareKeys("|2|") == false)
            {
                showFiles(false);
                txtNewFolder.Visible = false;
                btnNewFolder.Visible = false;
            }
            else
            {
                sCurrentDirectory = Server.MapPath(sBase) + Request.QueryString["path"];
                if (Directory.Exists(sCurrentDirectory)) showFiles(false);
            }

            // ~~~ /Show Files ~~~
        }

        /// <summary>
        /// Shows the files.
        /// </summary>
        /// <param name="sortDate">if set to <c>true</c> [sort date].</param>
        protected void showFiles(bool sortDate)
        {
            if (SepFunctions.CompareKeys("|2|") == false)
            {

                // ~~~ Breadcrumb ~~~
                string sBreadcrumb = SepFunctions.LangText("root");
                lblPath.Text = sBreadcrumb.Replace("\\", " \\ ");

                // ~~~ /Breadcrumb ~~~

                // ~~~ DataTable ~~~
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("FileName", typeof(string)));
                dt.Columns.Add(new DataColumn("FileUrl", typeof(string)));
                dt.Columns.Add(new DataColumn("LastUpdated", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Size", typeof(string)));
                dt.Columns.Add(new DataColumn("Icon", typeof(string)));
                dt.Columns.Add(new DataColumn("thumbnail", typeof(string)));
                dt.Columns.Add(new DataColumn("index", typeof(string)));
                long aCount = 0;

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT UploadID,FileName,FileSize,DatePosted FROM Uploads WHERE ModuleID='7' AND UserID=@UserID" + Strings.ToString(sortDate ? " ORDER BY DatePosted" : string.Empty), conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                var dr = dt.NewRow();
                                aCount += 1;
                                // ~~~ /DataTable ~~~

                                // ~~~ List Files ~~~
                                string sName = SepFunctions.openNull(RS["FileName"]);
                                dr["FileName"] = sName;
                                dr["LastUpdated"] = SepFunctions.openNull(RS["DatePosted"]);

                                double nFileLength = SepFunctions.toDouble(SepFunctions.openNull(RS["FileSize"]));
                                if (Math.Abs(nFileLength) < 1)
                                    dr["Size"] = "0 KB";
                                else if (nFileLength / 1024 < 1)
                                    dr["Size"] = "1 KB";
                                else
                                    dr["Size"] = Strings.FormatNumber(nFileLength / 1024, 0) + " KB";

                                string sVirtualPath = sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                                var sExt = Strings.Right(SepFunctions.openNull(RS["FileName"]), Strings.Len(SepFunctions.openNull(RS["FileName"])) - Strings.InStrRev(SepFunctions.openNull(RS["FileName"]), ".") + 1);
                                if (sExt == ".jpeg" || sExt == ".jpg" || sExt == ".gif" || sExt == ".png")
                                {
                                    dr["Icon"] = sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&Size=thumb";
                                    dr["thumbnail"] = sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&Size=thumb";
                                }
                                else
                                {
                                    dr["Icon"] = sImageFolder + "spadmin/ImageManager/images/blank.gif";
                                    dr["thumbnail"] = string.Empty;
                                }

                                dr["index"] = aCount;

                                dr["FileUrl"] = sVirtualPath;
                                dt.Rows.Add(dr);
                            }

                        }
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

                // ~~~ /List Files ~~~
            }
            else
            {
                var sResMapPath = Server.MapPath(sBase);

                // ~~~ Breadcrumb ~~~
                var sQueryString = Request.QueryString["path"];
                var sBreadcrumb = string.Empty;
                var slink = string.Empty;
                var count = 0;
                if (!string.IsNullOrWhiteSpace(sQueryString))
                {
                    if (!(sQueryString.IndexOf("\\", StringComparison.OrdinalIgnoreCase) == -1))
                    {
                        int nLength = sQueryString.Split('\\').Length;

                        foreach (var item_loopVariable in sQueryString.Split('\\'))
                        {
                            string item = item_loopVariable;
                            if (count == nLength - 1)
                            {
                                sBreadcrumb = sBreadcrumb + " \\ " + item;
                            }
                            else if (count == 0)
                            {
                                sBreadcrumb = sBreadcrumb + "<a href=\"default.aspx?path=&c=" + Request.QueryString["c"] + "&Editor=" + SepCommon.SepCore.Request.Item("Editor") + "\">" + SepFunctions.LangText("root") + "</a>";
                            }
                            else
                            {
                                slink = slink + "\\" + item;
                                sBreadcrumb = sBreadcrumb + " \\ <a href=\"default.aspx?path=" + Server.UrlEncode(slink) + "&c=" + Request.QueryString["c"] + "&Editor=" + SepCommon.SepCore.Request.Item("Editor") + "\">" + item + "</a>";
                            }

                            count += 1;
                        }
                    }
                }
                else
                {
                    sBreadcrumb = SepFunctions.LangText("root");
                }

                lblPath.Text = sBreadcrumb.Replace("\\", " \\ ");

                // ~~~ /Breadcrumb ~~~

                // ~~~ DataTable ~~~
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("FileName", typeof(string)));
                dt.Columns.Add(new DataColumn("FileUrl", typeof(string)));
                dt.Columns.Add(new DataColumn("LastUpdated", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Size", typeof(string)));
                dt.Columns.Add(new DataColumn("Icon", typeof(string)));
                dt.Columns.Add(new DataColumn("thumbnail", typeof(string)));
                dt.Columns.Add(new DataColumn("index", typeof(string)));

                // ~~~ /DataTable ~~~

                // ~~~ Create Up one Folder ~~~
                if (!string.IsNullOrWhiteSpace(Request.QueryString["path"]))
                {
                    var dr = dt.NewRow();
                    dr["FileName"] = "...";
                    dr["Icon"] = string.Empty;
                    dr["FileUrl"] = "default.aspx?path=" + Server.UrlEncode(Directory.GetParent(sCurrentDirectory).FullName.Substring(sResMapPath.Length - 1));
                    dt.Rows.Add(dr);
                }

                var di = new DirectoryInfo(sCurrentDirectory);
                var diar1 = di.GetDirectories();


                // ~~~ /Create Up one Folder ~~~

                // ~~~ List Folders ~~~
                string sName;
                string sVirtualPath;
                // list the names of all files in the specified directory
                foreach (var dra_loopVariable in diar1)
                {
                    DirectoryInfo dra = dra_loopVariable;
                    var dr = dt.NewRow();
                    sName = dra.Name;
                    dr["FileName"] = sName;
                    dr["LastUpdated"] = Strings.ToString(dra.LastWriteTime);
                    dr["Size"] = string.Empty;
                    sVirtualPath = "default.aspx?path=" + Server.UrlEncode(dra.FullName.Substring(sResMapPath.Length));
                    dr["Icon"] = sVirtualPath;
                    dr["FileUrl"] = sVirtualPath;
                    dt.Rows.Add(dr);
                }

                // ~~~ /List Folders ~~~

                // ~~~ List Files ~~~
                var orderedFiles = new DirectoryInfo(sCurrentDirectory).GetFiles();

                long aCount = 0;

                foreach (var f in orderedFiles)
                {
                    var dr = dt.NewRow();
                    aCount += 1;
                    sName = f.Name;
                    dr["FileName"] = sName;
                    dr["LastUpdated"] = Strings.ToString(f.CreationTime);

                    double nFileLength = f.Length;
                    if (Math.Abs(nFileLength) < 1)
                        dr["Size"] = "0 KB";
                    else if (nFileLength / 1024 < 1)
                        dr["Size"] = "1 KB";
                    else
                        dr["Size"] = Strings.FormatNumber(nFileLength / 1024, 0) + " KB";

                    var sResources = sBase;
                    if (sCurrentDirectory.Length > sResMapPath.Length) sResources = sResources + sCurrentDirectory.Substring(sResMapPath.Length).Replace("\\", "/") + "/";
                    sVirtualPath = sResources + sName;

                    var sExt = f.Extension;
                    if (sExt == ".jpeg" || sExt == ".jpg" || sExt == ".gif" || sExt == ".png")
                    {
                        dr["Icon"] = sImageFolder + "spadmin/ImageManager/image_thumbnail.aspx?file=" + sVirtualPath + "&Size=70";
                        dr["thumbnail"] = sImageFolder + "spadmin/ImageManager/image_thumbnail.aspx?file=" + sVirtualPath + "&Size=";
                    }
                    else
                    {
                        dr["Icon"] = sImageFolder + "spadmin/ImageManager/images/blank.gif";
                        dr["thumbnail"] = string.Empty;
                    }

                    dr["index"] = aCount;

                    dr["FileUrl"] = sVirtualPath;
                    dt.Rows.Add(dr);
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

                // ~~~ /List Files ~~~
            }

            if (GridView1.Rows.Count == 0) btnDelete.Visible = false;
            else btnDelete.Visible = true;

            btnDelete.OnClientClick = "if(_getSelection(document.getElementById('" + hidFilesToDel.ClientID + "'))){return deleteFiles(false)}else{return false}";
            btnDelete.Style.Add("margin-right", "5px");
            btnDelete2.Attributes.Add("onclick", "return deleteFolder(this, false);");
        }
    }
}