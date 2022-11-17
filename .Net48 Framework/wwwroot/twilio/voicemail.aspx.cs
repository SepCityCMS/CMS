// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="voicemail.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.twilio
{
    using SepCommon;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class voicemail.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class voicemail : Page
    {
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
        /// The c common
        /// </summary>
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
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), false) == false)
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

            if (SepCommon.SepCore.Request.Item("DoAction") == "DeleteFile")
            {
                string sXML = string.Empty;

                sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
                sXML += "<Root>" + Environment.NewLine;
                sXML += "<File></File>";
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            if (root.SelectSingleNode("/Root/ReadText") != null)
                            {
                                sXML += "<ReadText>" + root.SelectSingleNode("/Root/ReadText").InnerText + "</ReadText>";
                            }
                        }
                    }
                }
                else
                {
                    sXML += "<ReadText></ReadText>";
                }

                sXML += "</Root>" + Environment.NewLine;

                using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    outfile.Write(sXML);
                }

                FileInfoRow.Visible = false;
                UploadFile.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                Load_Screen();
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string sXML = string.Empty;

            sXML += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
            sXML += "<Root>" + Environment.NewLine;
            try
            {
                HttpPostedFile PostedFile = Request.Files[0];
                if (PostedFile.ContentLength > 0)
                {
                    var sFileExt = string.Empty;
                    var sFileName = string.Empty;
                    sFileExt = SepCommon.SepCore.Strings.LCase(Path.GetExtension(PostedFile.FileName));
                    sFileName = SepFunctions.GetIdentity() + sFileExt;
                    if (sFileExt == ".mp3")
                    {
                        PostedFile.SaveAs(SepFunctions.GetDirValue("images") + sFileName);
                        sXML += "<File>" + sFileName + "</File>";
                        sXML += "<ReadText></ReadText>";
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Voicemail has been successfully saved.") + "</div>";
                    }
                    else
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid MP3 file.") + "</div>";
                    }
                }
                else
                {
                    sXML += "<File></File>";
                    sXML += "<ReadText>" + ReadText.Value + "</ReadText>";
                    if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        XmlDocument doc = new XmlDocument() { XmlResolver = null };
                        using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                        {
                            using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                            {
                                doc.Load(reader);

                                var root = doc.DocumentElement;

                                if (root.SelectSingleNode("/Root/File") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/File").InnerText))
                                {
                                    if (File.Exists(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/File").InnerText))
                                    {
                                        File.Delete(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/File").InnerText);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                sXML += "<File></File>";
                sXML += "<ReadText>" + ReadText.Value + "</ReadText>";
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            var root = doc.DocumentElement;

                            if (root.SelectSingleNode("/Root/File") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/File").InnerText))
                            {
                                if (File.Exists(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/File").InnerText))
                                {
                                    File.Delete(SepFunctions.GetDirValue("images") + root.SelectSingleNode("/Root/File").InnerText);
                                }
                            }
                        }
                    }
                }
            }

            sXML += "</Root>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
            {
                outfile.Write(sXML);
            }

            Load_Screen();
        }

        /// <summary>
        /// Loads the screen.
        /// </summary>
        private void Load_Screen()
        {
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        var root = doc.DocumentElement;

                        if (root.SelectSingleNode("/Root/ReadText") != null)
                        {
                            ReadText.Value = root.SelectSingleNode("/Root/ReadText").InnerText;
                        }

                        if (root.SelectSingleNode("/Root/File") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/File").InnerText))
                        {
                            FileName.InnerHtml = root.SelectSingleNode("/Root/File").InnerText;
                            FileInfoRow.Visible = true;
                            UploadFile.Visible = false;
                        }
                        else
                        {
                            FileInfoRow.Visible = false;
                            UploadFile.Visible = true;
                        }
                    }
                }
            }
            else
            {
                FileInfoRow.Visible = false;
                UploadFile.Visible = true;
            }
        }
    }
}