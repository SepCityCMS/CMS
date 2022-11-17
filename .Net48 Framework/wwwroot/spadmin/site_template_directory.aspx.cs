// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_directory.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Ionic.Zip;
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class site_template_directory.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_directory : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The s folder
        /// </summary>
        private string sFolder = string.Empty;

        /// <summary>
        /// The s user folder
        /// </summary>
        private string sUserFolder = "\\";

        /// <summary>
        /// Allows the edit.
        /// </summary>
        /// <param name="sExt">The s ext.</param>
        /// <returns>System.String.</returns>
        public static string AllowEdit(string sExt)
        {
            switch (Strings.LCase(sExt))
            {
                case ".html":
                case ".htm":
                case ".master":
                case ".js":
                case ".sql":
                case ".txt":
                case ".log":
                case ".css":
                    return "editor";

                case ".jpg":
                case ".gif":
                case ".png":
                case ".jpeg":
                case ".bmp":
                    return "image";

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the file icon.
        /// </summary>
        /// <param name="sExt">The s ext.</param>
        /// <returns>System.String.</returns>
        public static string GetFileIcon(string sExt)
        {
            string sImage;
            switch (Strings.LCase(sExt))
            {
                case "folder":
                    sImage = "directory.png";
                    break;

                case ".html":
                case ".htm":
                case ".master":
                    sImage = "html.png";
                    break;

                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".bmp":
                    sImage = "imagesizing.png";
                    break;

                case ".doc":
                case ".docx":
                case ".rtf":
                    sImage = "msword.png";
                    break;

                case ".avi":
                case ".mp4":
                case ".mp3":
                case ".wav":
                case ".mpeg":
                case ".mpg":
                    sImage = "media.png";
                    break;

                case ".pdf":
                    sImage = "pdf.png";
                    break;

                case ".zip":
                    sImage = "zip.png";
                    break;

                case ".rar":
                    sImage = "rar.png";
                    break;

                case ".js":
                    sImage = "javascript.png";
                    break;

                case ".sql":
                    sImage = "sqlfile.png";
                    break;

                case ".exe":
                case ".bat":
                    sImage = "exe.png";
                    break;

                case ".css":
                    sImage = "css.png";
                    break;

                case ".xml":
                    sImage = "xml.png";
                    break;

                default:
                    sImage = "text.png";
                    break;
            }

            return "<img src=\"images/" + sImage + "\" border=\"0\" alt=\"Icon\" />";
        }

        /// <summary>
        /// Binds the datagrid.
        /// </summary>
        public void Bind_Datagrid()
        {
            // Make a reference to a directory.
            var dir = new DirectoryInfo(sFolder);

            // Get a reference to each file in that directory.
            var dirArr = dir.GetDirectories();
            var fileArr = dir.GetFiles();
            var dt = new DataTable();

            dt.Columns.Add().ColumnName = "Image";
            dt.Columns.Add().ColumnName = "FileName";
            dt.Columns.Add().ColumnName = "AllowEdit";
            dt.Columns.Add().ColumnName = "AllowDownload";
            dt.Columns.Add().ColumnName = "Size";
            dt.Columns.Add().ColumnName = "DateCreated";
            dt.Columns.Add().ColumnName = "DateModified";
            dt.Columns.Add().ColumnName = "Folder";

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Folder")) && sUserFolder != "\\")
            {
                var arrUserFolder = Strings.Split(sUserFolder, "\\");
                var sParentFolder = string.Empty;
                if (arrUserFolder != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserFolder) - 2; i++) sParentFolder += arrUserFolder[i] + "\\";
                }

                var rowData = new[] { GetFileIcon("folder"), SepFunctions.LangText("... Parent"), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, SepFunctions.UrlEncode(sParentFolder) };
                dt.Rows.Add(rowData);
            }

            foreach (var dir_loopVariable in dirArr)
            {
                dir = dir_loopVariable;
                if (sUserFolder == "\\" && (dir.Name == "Public" || dir.Name == "BusinessCasual" || dir.Name == "BusinessWay" || dir.Name == "BusinessWebDesign" || dir.Name == "BusinessWorld" || dir.Name == "CreamBurn" || dir.Name == "CyberArray" || dir.Name == "DuetPlasma" || dir.Name == "GlobalHouse" || dir.Name == "IdeaLab" || dir.Name == "ITDesk" || dir.Name == "LinkLine" || dir.Name == "ProIndustries" || dir.Name == "SmartDesign" || dir.Name == "SocialNet" || dir.Name == "WideCommerce" || dir.Name == "WinGlobal" || dir.Name == "YellowStone" || Strings.Right(dir.Name, 9) == "(DELETED)"))
                {
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Strings.Replace(SepCommon.SepCore.Request.Item("Folder"), "\\", string.Empty)))
                    {
                        var rowData = new[] { GetFileIcon("folder"), dir.Name, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, SepFunctions.UrlEncode(sUserFolder + dir.Name + "\\") };
                        dt.Rows.Add(rowData);
                    }
                    else
                    {
                        var rowData = new[] { GetFileIcon("folder"), dir.Name, "Zip", string.Empty, string.Empty, string.Empty, string.Empty, SepFunctions.UrlEncode(sUserFolder + dir.Name + "\\") };
                        dt.Rows.Add(rowData);
                    }
                }
            }

            foreach (var file_loopVariable in fileArr)
            {
                // Display the names of the files.
                FileInfo file = file_loopVariable;
                if (sUserFolder == "\\" && Strings.LCase(file.Name) == "template.master")
                {
                }
                else
                {
                    var rowData = new[] { GetFileIcon(file.Extension), file.Name, AllowEdit(file.Extension), "true", GetFileSize(file.Length), Strings.ToString(file.CreationTime), Strings.ToString(file.LastWriteTime), SepFunctions.UrlEncode(sUserFolder + file.Name) };
                    dt.Rows.Add(rowData);
                }
            }

            var _with1 = ManageGridView;
            _with1.DataSource = dt;
            _with1.DataBind();
        }

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
        /// Gets the size of the file.
        /// </summary>
        /// <param name="iSize">Size of the i.</param>
        /// <returns>System.String.</returns>
        public string GetFileSize(double iSize)
        {
            var sExt = "Bytes";

            if (iSize > 1024)
            {
                iSize /= 1024;
                sExt = "KB";
            }

            if (iSize > 1024)
            {
                iSize /= 1024;
                sExt = "MB";
            }

            if (iSize > 1024)
            {
                iSize /= 1024;
                sExt = "GB";
            }

            if (SepFunctions.toLong(Strings.Right(Strings.ToString(iSize), 1)) > 0) iSize = SepFunctions.toDouble(Strings.FormatNumber(iSize, 1));

            return iSize + " " + sExt;
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.Object.</returns>
        public object GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
        }

        /// <summary>
        /// Makes the zip.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        public void MakeZip(string folderName)
        {
            using (var zip = new ZipFile())
            {
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "temp\\" + folderName + ".zip")) File.Delete(SepFunctions.GetDirValue("app_data") + "temp\\" + folderName + ".zip");
                if (File.Exists(SepFunctions.GetDirValue("skins") + folderName + "\\install.xml")) zip.AddFile(SepFunctions.GetDirValue("skins") + folderName + "\\install.xml", string.Empty);
                zip.AddDirectory(SepFunctions.GetDirValue("skins") + folderName + "\\", folderName);
                zip.Save(SepFunctions.GetDirValue("app_data") + "temp\\" + folderName + ".zip");
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + folderName + ".zip");
                var bytes = File.ReadAllBytes(SepFunctions.GetDirValue("app_data") + "temp\\" + folderName + ".zip");
                Response.BinaryWrite(bytes);
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "temp\\" + folderName + ".zip")) File.Delete(SepFunctions.GetDirValue("app_data") + "temp\\" + folderName + ".zip");
                Response.End();
            }
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    FilterDoAction.Items[0].Text = SepFunctions.LangText("Select an Action");
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Items");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("File Name");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("View/Download");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Size");
                    ManageGridView.Columns[4].HeaderText = SepFunctions.LangText("Date Created");
                    ManageGridView.Columns[5].HeaderText = SepFunctions.LangText("Date Last Modified");
                    RunAction.InnerText = SepFunctions.LangText("GO");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CreateFileButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateFileButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FileName.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must enter a file name.") + "</div>";
                return;
            }

            var sFileName = sFolder + FileName.Value;

            if (!Directory.Exists(sFileName))
            {
                using (var fs = File.Create(sFileName, 1024))
                {
                }
                Bind_Datagrid();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~" + FileName.Value + "~~ already exists.") + "</div>";
            }
        }

        /// <summary>
        /// Handles the Click event of the CreateFolderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateFolderButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FolderName.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must enter a folder name.") + "</div>";
                return;
            }

            var createFolder = sFolder + FolderName.Value;

            if (!Directory.Exists(createFolder))
            {
                Directory.CreateDirectory(createFolder);
                Bind_Datagrid();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~" + FolderName.Value + "~~ already exists.") + "</div>";
            }
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
            TranslatePage();

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSiteLooks")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSiteLooks"), false) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            if (!string.IsNullOrWhiteSpace(Strings.Replace(SepCommon.SepCore.Request.Item("Folder"), "\\", string.Empty)) && SepCommon.SepCore.Request.Item("DoAction") == "DownloadZip") MakeZip(SepFunctions.CleanFileName(Strings.Replace(SepCommon.SepCore.Request.Item("folder"), "\\", string.Empty)));

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Folder"))) sUserFolder = SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("Folder"));

            sFolder = SepFunctions.GetDirValue("skins") + SepFunctions.UrlDecode(sUserFolder);

            Bind_Datagrid();
        }

        /// <summary>
        /// Handles the Click event of the RunAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RunAction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterDoAction.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                return;
            }

            var sIDs = UniqueIDs.Value;

            if (Strings.Len(sIDs) > 0)
            {
                var arrFiles = Strings.Split(sIDs, ",");
                if (arrFiles != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFiles); i++)
                        if (!string.IsNullOrWhiteSpace(arrFiles[i]))
                        {
                            if (Directory.Exists(sFolder + arrFiles[i])) Directory.Delete(sFolder + arrFiles[i], true);
                            if (File.Exists(sFolder + arrFiles[i])) File.Delete(sFolder + arrFiles[i]);
                        }
                }

                Bind_Datagrid();
                DeleteResult.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Items have been successfully deleted.") + "</div>";
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
        }

        /// <summary>
        /// Handles the Click event of the UploadFileButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void UploadFileButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UploadFile.FileName)) DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select a file to upload.") + "</div> : Exit Sub";

            try
            {
                var sFileName = Path.GetFileName(UploadFile.PostedFile.FileName);
                var SaveLocation = sFolder + sFileName;

                UploadFile.PostedFile.SaveAs(SaveLocation);
            }
            catch
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error uploading file.") + "</div>";
            }

            Bind_Datagrid();
        }
    }
}