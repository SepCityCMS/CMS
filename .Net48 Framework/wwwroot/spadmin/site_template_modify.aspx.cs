// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class site_template_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_modify : Page
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    EnableUserPages.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Template Options");
                    TemplateNameLabel.InnerText = SepFunctions.LangText("Template Name:");
                    AccessKeysLabel.InnerText = SepFunctions.LangText("Access Keys to use this template in the portals and user pages module:");
                    EnableUserPagesLabel.InnerText = SepFunctions.LangText("Allow template to be used in User Pages:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    TemplateNameRequired.ErrorMessage = SepFunctions.LangText("~~Template Name~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
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

            if (SepFunctions.Setup(7, "UPagesEnable") != "Enable") UserPagesRow.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TemplateID")))
            {
                var jSiteTemplates = SepCommon.DAL.SiteTemplates.Template_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("TemplateID")));

                if (jSiteTemplates.TemplateID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Template~~ does not exist.") + "</div>";
                    TemplateForm.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Template");
                    TemplateID.Value = SepCommon.SepCore.Request.Item("TemplateID");
                    TemplateName.Value = jSiteTemplates.TemplateName;
                    if (jSiteTemplates.EnableUserPage)
                    {
                        EnableUserPages.Value = "1";
                    }
                    else
                    {
                        EnableUserPages.Value = "0";
                    }
                    Description.Value = jSiteTemplates.Description;
                    AccessKeys.Text = jSiteTemplates.AccessKeys;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid Template.") + "</div>";
                    TemplateForm.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string sReturn = String.Empty;

            var jSiteTemplates = SepCommon.DAL.SiteTemplates.Template_Get(SepFunctions.toLong(TemplateID.Value));

            if (jSiteTemplates.TemplateID > 0)
            {
                var sConfigFile = SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config_default.xml";

                if (SepFunctions.Setup(7, "UPagesEnable") != "Enable")
                    sReturn = SepCommon.DAL.SiteTemplates.Template_Save(SepFunctions.toLong(TemplateID.Value), jSiteTemplates.TemplateName, 1, Description.Value, AccessKeys.Text);
                else
                    sReturn = SepCommon.DAL.SiteTemplates.Template_Save(SepFunctions.toLong(TemplateID.Value), jSiteTemplates.TemplateName, SepFunctions.toLong(EnableUserPages.Value), Description.Value, AccessKeys.Text);

                TemplateName.Value = jSiteTemplates.TemplateName;

                if (File.Exists(sConfigFile))
                {
                    long xmlCount = 0;
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(sConfigFile))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            XmlNodeList nodelist = null;

                            nodelist = root.GetElementsByTagName("Feature");
                            foreach (XmlElement node in nodelist)
                            {
                                xmlCount += 1;
                                node.InnerText = SepCommon.SepCore.Request.Item("Feature" + xmlCount);
                            }

                            if (SepFunctions.Setup(7, "UPagesEnable") == "Enable")
                            {
                                xmlCount = 0;
                                nodelist = root.GetElementsByTagName("UPFeature");
                                foreach (XmlElement node in nodelist)
                                {
                                    xmlCount += 1;
                                    node.InnerText = SepCommon.SepCore.Request.Item("UPFeature" + xmlCount);
                                }
                            }
                            else
                            {
                                xmlCount = 0;
                                nodelist = root.GetElementsByTagName("UPFeature");
                                foreach (XmlElement node in nodelist)
                                {
                                    xmlCount += 1;
                                    node.InnerText = SepCommon.SepCore.Request.Item("Feature" + xmlCount);
                                }
                            }

                            xmlCount = 0;
                            nodelist = root.GetElementsByTagName("Variable");
                            foreach (XmlElement node in nodelist)
                            {
                                xmlCount += 1;
                                switch (node["Question"].GetAttribute("type"))
                                {
                                    case "Image":
                                        try
                                        {
                                            var userPostedFile = Request.Files["Custom" + xmlCount];
                                            var sFileName = string.Empty;
                                            var sFileExt = string.Empty;

                                            if (userPostedFile != null)
                                            {
                                                if (userPostedFile.ContentLength > 0)
                                                {
                                                    sFileExt = Strings.LCase(Path.GetExtension(userPostedFile.FileName));
                                                    if (sFileExt == ".jpg" || sFileExt == ".jpeg" || sFileExt == ".gif" || sFileExt == ".png")
                                                    {
                                                        sFileName = SepFunctions.GetIdentity() + sFileExt;
                                                        node["Value"].InnerText = sFileName;
                                                        userPostedFile.SaveAs(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\images\\" + sFileName);
                                                    }
                                                }
                                                else
                                                {
                                                    if (File.Exists(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config.xml") == false)
                                                    {
                                                        XmlDocument ReadDoc = new XmlDocument() { XmlResolver = null };
                                                        using (StreamReader sreader2 = new StreamReader(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config_default.xml"))
                                                        {
                                                            using (XmlReader reader3 = XmlReader.Create(sreader2, new XmlReaderSettings() { XmlResolver = null }))
                                                            {
                                                                ReadDoc.Load(reader3);
                                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        XmlDocument ReadDoc = new XmlDocument() { XmlResolver = null };
                                                        using (StreamReader sreader2 = new StreamReader(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config.xml"))
                                                        {
                                                            using (XmlReader reader3 = XmlReader.Create(sreader2, new XmlReaderSettings() { XmlResolver = null }))
                                                            {
                                                                ReadDoc.Load(reader3);
                                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        break;

                                    case "HTML":
                                        node["Value"].InnerText = SepCommon.SepCore.Request.Item("txtCustom" + xmlCount);
                                        break;

                                    default:
                                        node["Value"].InnerText = SepCommon.SepCore.Request.Item("Custom" + xmlCount);
                                        break;
                                }
                            }

                            if (File.Exists(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config.xml") == false)
                                using (var sw = File.CreateText(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config.xml"))
                                {
                                    sw.WriteLine(doc.OuterXml);
                                }
                            else
                                using (var outfile = new StreamWriter(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config.xml"))
                                {
                                    outfile.Write(doc.OuterXml);
                                }
                        }
                    }
                }
            }

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}