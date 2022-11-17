// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_new.aspx.cs" company="SepCity, Inc.">
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
    /// Class site_template_new.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_new : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The s template identifier
        /// </summary>
        private readonly string sTemplateID = "1";

        /// <summary>
        /// The s temporary folder
        /// </summary>
        private string sTempFolder;

        /// <summary>
        /// The s template configuration XML
        /// </summary>
        private string sTemplateConfigXML;

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
        /// Draws the toolbar.
        /// </summary>
        public void Draw_Toolbar()
        {
            long aa = 0;
            long bb = 0;
            var tmpSection = string.Empty;

            var sToolbar = string.Empty;
            var sScript = string.Empty;

            sToolbar += "<table width=\"100%\">";
            sToolbar += "<tr><td>";
            XmlDocument m_xmld = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(sTempFolder + "template-colors.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    m_xmld.Load(reader);
                    XmlNodeList m_nodelist = m_xmld.SelectNodes("//root/Name");
                    foreach (XmlNode m_node_loopVariable in m_nodelist)
                    {
                        XmlNode m_node = m_node_loopVariable;
                        aa += 1;
                        if (m_node.Attributes.GetNamedItem("Section").Value != tmpSection)
                        {
                            if (aa > 1)
                                sToolbar += "</div>";
                            bb = 0;
                            sToolbar += "<div id=\"div" + m_node.Attributes.GetNamedItem("Section").Value + "\" style=\"display:none;\" class=\"TemplateToolbar\">";
                            switch (m_node.Attributes.GetNamedItem("Section").Value)
                            {
                                case "Body":
                                    sToolbar += "<b>" + SepFunctions.LangText("Website Body Style") + "</b><br/>";
                                    break;

                                case "LayoutTopMenu":
                                    sToolbar += "<b>" + SepFunctions.LangText("Top Menu Style") + "</b><br/>";
                                    break;

                                case "MenuLabel":
                                    sToolbar += "<b>" + SepFunctions.LangText("Menu Label Style") + "</b><br/>";
                                    break;

                                case "ContentTopGrad":
                                    sToolbar += "<b>" + SepFunctions.LangText("Website Top Gradient") + "</b><br/>";
                                    break;

                                case "ContentBottomGrad":
                                    sToolbar += "<b>" + SepFunctions.LangText("Website Bottom Gradient") + "</b><br/>";
                                    break;

                                case "HeaderHR":
                                    sToolbar += "<b>" + SepFunctions.LangText("Header Horizonal Line Color") + "</b><br/>";
                                    break;

                                case "Header":
                                    sToolbar += "<b>" + SepFunctions.LangText("Header Styles") + "</b><br/>";
                                    break;

                                case "ContentSpacer":
                                    sToolbar += "<b>" + SepFunctions.LangText("Content Spacer Color") + "</b><br/>";
                                    break;

                                case "Footer":
                                    sToolbar += "<b>" + SepFunctions.LangText("Website Footer Style") + "</b><br/>";
                                    break;

                                case "Fieldset":
                                    sToolbar += "<b>" + SepFunctions.LangText("Fieldset Style") + "</b><br/>";
                                    break;

                                case "Events":
                                    sToolbar += "<b>" + SepFunctions.LangText("Event Calendar Style") + "</b><br/>";
                                    break;

                                case "Tables":
                                    sToolbar += "<b>" + SepFunctions.LangText("Table Style") + "</b><br/>";
                                    break;

                                case "ModuleTopMenu":
                                    sToolbar += "<b>" + SepFunctions.LangText("Module Top Menu") + "</b><br/>";
                                    break;
                            }
                        }

                        sToolbar += "<label>" + m_node.ChildNodes.Item(0).InnerText + "</label>";
                        sToolbar += "<input type=\"text\" name=\"" + m_node.Attributes.GetNamedItem("Type").Value + "\" id=\"" + m_node.Attributes.GetNamedItem("Type").Value + "\" value=\"" + Strings.Replace(m_node.ChildNodes.Item(1).InnerText, "#", string.Empty) + "\"/>";
                        bb += 1;
                        if (bb == 2)
                        {
                            sToolbar += "<div class=\"clear\"></div>";
                            bb = 0;
                        }

                        sScript += "$('#" + m_node.Attributes.GetNamedItem("Type").Value + "').miniColors();" + Environment.NewLine;
                        tmpSection = m_node.Attributes.GetNamedItem("Section").Value;
                    }
                }
            }

            sToolbar += "</div>";
            sToolbar += "</td>";
            sToolbar += "<td align=\"right\" style=\"padding-right:10px\" valign=\"top\">";
            sToolbar += SepFunctions.LangText("Saved Templates") + "<br/>";
            sToolbar += "<select name=\"ModTemp\" style=\"width:130px\" onchange=\"document.location.href='site_template_new.aspx?ModTemp='+this.value+'&PortalID=" + SepFunctions.Get_Portal_ID() + "';\">";
            sToolbar += "<option value=\"\">" + SepFunctions.LangText("New Template") + "</option>";
            var di = new DirectoryInfo(SepFunctions.GetDirValue("App_Data") + "templates\\saved\\");
            var dirArr = di.GetDirectories();
            foreach (var dir_loopVariable in dirArr)
            {
                DirectoryInfo dir = dir_loopVariable;
                sToolbar += "<option value=\"" + dir.Name + "\"" + Strings.ToString(SepCommon.SepCore.Request.Item("ModTemp") == dir.Name ? " selected=\"selected\"" : string.Empty) + ">" + dir.Name + "</option>";
            }

            sToolbar += "</select><br/>";
            sToolbar += "<input type=\"button\" id=\"idApplyChanges\" onclick=\"applyChanges();\" value=\"" + SepFunctions.LangText("Apply Changes") + "\" style=\"display:none;width:130px\"/><br/>";
            sToolbar += "<input type=\"button\" id=\"idDynFunc\" onclick=\"openDynamicFunctions('<iframe name=\\'aDynHrefFrame\\' width=\\'100%\\' height=\\'250\\' frameborder=\\'0\\' src=\\'site_template_functions.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\\'></iframe>';return false;\" value=\"" + SepFunctions.LangText("Widgets") + "\" style=\"width:130px\"/><br/>";
            sToolbar += "<input type=\"button\" id=\"idSaveTemp\" onclick=\"openSaveWindow('<iframe name=\\'aSaveHrefFrame\\' width=\\'100%\\' height=\\'310\\' frameborder=\\'0\\' src=\\'site_template_save.aspx?ModTemp=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("ModTemp")) + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\\'></iframe>';return false;\" value=\"" + SepFunctions.LangText("Save Template") + "\" style=\"width:130px\"/><br/>";
            sToolbar += "</td>";
            sToolbar += "</tr></table>";

            HeaderScript.InnerHtml = "<script type=\"text/javascript\">" + Environment.NewLine;
            HeaderScript.InnerHtml += "$(document).ready(function(){" + Environment.NewLine;
            HeaderScript.InnerHtml += "function init() {" + Environment.NewLine;
            HeaderScript.InnerHtml += sScript;
            HeaderScript.InnerHtml += "}" + Environment.NewLine;
            HeaderScript.InnerHtml += "init();" + Environment.NewLine;
            HeaderScript.InnerHtml += "});" + Environment.NewLine;
            HeaderScript.InnerHtml += "</script>" + Environment.NewLine;

            Toolbar.InnerHtml = sToolbar;
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

            sTemplateConfigXML = SepFunctions.GetDirValue("App_Data") + "templates\\template.xml";

            sTempFolder = SepFunctions.GetDirValue("App_Data") + "templates\\temp\\";

            using (var cTemplate = new site_template())
            {
                string sConfig = null;

                if (Directory.Exists(sTempFolder))
                {
                    if (File.Exists(sTempFolder + "colors.css"))
                        File.Delete(sTempFolder + "colors.css");
                    if (File.Exists(sTempFolder + "layout.css"))
                        File.Delete(sTempFolder + "layout.css");
                    if (File.Exists(sTempFolder + "menus.css"))
                        File.Delete(sTempFolder + "menus.css");
                    if (File.Exists(sTempFolder + "template.master"))
                        File.Delete(sTempFolder + "template.master");
                    if (File.Exists(sTempFolder + "template-colors.xml"))
                        File.Delete(sTempFolder + "template-colors.xml");
                    if (File.Exists(sTempFolder + "template-config.xml"))
                        File.Delete(sTempFolder + "template-config.xml");

                    if (cTemplate.Load_Folder(SepCommon.SepCore.Request.Item("ModTemp")) == sTempFolder)
                        File.Copy(SepFunctions.GetDirValue("App_Data") + "templates\\template-colors.xml", sTempFolder + "template-colors.xml");
                    else
                        File.Copy(cTemplate.Load_Folder(SepCommon.SepCore.Request.Item("ModTemp")) + "template-colors.xml", sTempFolder + "template-colors.xml");

                    XmlDocument m_xmld = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(sTemplateConfigXML))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            m_xmld.Load(reader);
                            var sRightColumn = m_xmld.SelectSingleNode("//root/template" + sTemplateID + "/RightColumn").InnerText;

                            sConfig = "<?xml version = \"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
                            sConfig += "<root>" + Environment.NewLine;
                            sConfig += "\t" + "<RightColumn>" + sRightColumn + "</RightColumn>" + Environment.NewLine;
                            sConfig += "</root>" + Environment.NewLine;
                        }
                    }

                    using (var objWriter = new StreamWriter(cTemplate.Load_Folder(SepCommon.SepCore.Request.Item("ModTemp")) + "template-config.xml"))
                    {
                        objWriter.Write(sConfig);
                    }

                    if (cTemplate.Load_Folder(SepCommon.SepCore.Request.Item("ModTemp")) != sTempFolder)
                        File.Copy(cTemplate.Load_Folder(SepCommon.SepCore.Request.Item("ModTemp")) + "template-config.xml", sTempFolder + "template-config.xml");

                    Draw_Toolbar();
                }
            }
        }
    }
}