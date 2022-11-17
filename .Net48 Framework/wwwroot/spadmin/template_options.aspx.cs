// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="template_options.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepControls;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Xml;

    /// <summary>
    /// Class template_options.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class template_options : Page
    {
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
            var sImageFolder = SepFunctions.GetInstallFolder(true);
            var FolderName = string.Empty;

            PageText.InnerHtml = string.Empty;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TemplateID")))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE Status <> -1 AND TemplateID=@TemplateID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", SepCommon.SepCore.Request.Item("TemplateID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                FolderName = SepFunctions.openNull(RS["FolderName"]);
                            }
                            else
                            {
                                PageText.InnerHtml = SepFunctions.LangText("Invalid Template");
                                return;
                            }

                        }
                    }
                }

                var sConfigFile = SepFunctions.GetDirValue("skins") + FolderName + "\\config_default.xml";

                if (File.Exists(SepFunctions.GetDirValue("skins") + FolderName + "\\config-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("UserID")) + ".xml")) sConfigFile = SepFunctions.GetDirValue("skins") + FolderName + "\\config-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("UserID")) + ".xml";

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

                            // nodelist = root.GetElementsByTagName("Feature")
                            // For Each node As XmlElement In nodelist
                            // xmlCount += 1
                            // If xmlCount = 1 Then sReturn += "<h2>" & SepFunctions.LangText("Turn on Features:") & "</h2>"
                            // sReturn += "<p>" & System.Environment.NewLine
                            // sReturn += "<input type=""checkbox"" name=""Feature" & xmlCount & """ id=""Feature" & xmlCount & """ value=""1""" & If(SepFunctions.toLong(node.InnerText) = 1, " checked=""checked""", "") & " />" & System.Environment.NewLine
                            // sReturn += "<label id=""Feature" & xmlCount & "Label"" for=""Feature" & xmlCount & """>" & node.GetAttribute("text") & "</label>" & System.Environment.NewLine
                            // sReturn += "</p>" & System.Environment.NewLine
                            // Next
                            xmlCount = 0;
                            nodelist = root.GetElementsByTagName("Variable");
                            foreach (XmlElement node in nodelist)
                            {
                                xmlCount += 1;
                                if (xmlCount == 1) PageText.InnerHtml += "<h2>" + SepFunctions.LangText("Template Options:") + "</h2>";
                                PageText.InnerHtml += "<p>" + Environment.NewLine;
                                PageText.InnerHtml += "<label id=\"Custom" + xmlCount + "Label\" for=\"Custom" + xmlCount + "\">" + node["Question"].InnerText + ":</label>" + Environment.NewLine;
                                switch (node["Question"].GetAttribute("type"))
                                {
                                    case "HTML":
                                        using (var cHTML = new WYSIWYGEditor())
                                        {
                                            cHTML.ID = "Custom" + xmlCount;
                                            cHTML.Text = node["Value"].InnerText;
                                            cHTML.Height = 300;
                                            cHTML.ClientIDMode = ClientIDMode.Static;
                                            using (var sw = new StringWriter())
                                            {
                                                var htw = new HtmlTextWriter(sw);
                                                cHTML.RenderControl(htw);
                                                PageText.InnerHtml += SepCommon.SepCore.Strings.ToString(sw);
                                                htw.Dispose();
                                            }
                                        }

                                        break;

                                    case "Image":
                                        if (!string.IsNullOrWhiteSpace(node["Value"].InnerText)) PageText.InnerHtml += "<img src=\"" + sImageFolder + "skins/" + FolderName + "/images/" + node["Value"].InnerText + "\" border=\"0\" width=\"200\" height=\"90\" /><br/>";
                                        PageText.InnerHtml += "<input type=\"file\" name=\"Custom" + xmlCount + "\" id=\"Custom" + xmlCount + "\" class=\"form-control\" />" + Environment.NewLine;
                                        break;

                                    default:
                                        PageText.InnerHtml += "<input type=\"text\" name=\"Custom" + xmlCount + "\" id=\"Custom" + xmlCount + "\" class=\"form-control\" value=\"" + node["Value"].InnerText + "\" />" + Environment.NewLine;
                                        break;
                                }

                                PageText.InnerHtml += "</p>" + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            else
            {
                PageText.InnerHtml = SepFunctions.LangText("Invalid Template");
            }
        }
    }
}