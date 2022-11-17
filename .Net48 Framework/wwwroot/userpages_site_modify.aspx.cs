// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userpages_site_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Xml;

    /// <summary>
    /// Class userpages_site_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userpages_site_modify : Page
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
                    ShowList.Items[0].Text = SepFunctions.LangText("Yes");
                    ShowList.Items[1].Text = SepFunctions.LangText("No");
                    InviteOnly.Items[0].Text = SepFunctions.LangText("Yes");
                    InviteOnly.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Create Your Site");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    SiteNameLabel.InnerText = SepFunctions.LangText("Site Name:");
                    SiteSloganLabel.InnerText = SepFunctions.LangText("Site Slogan:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    ShowListLabel.InnerText = SepFunctions.LangText("Show your site on the site listings:");
                    PortalSelectionLabel.InnerText = SepFunctions.LangText("Portal to associate your user site with:");
                    InviteOnlyLabel.InnerText = SepFunctions.LangText("Make your web site invite only:");
                    TemplateLabel.InnerText = SepFunctions.LangText("Web Site Template:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    SiteNameRequired.ErrorMessage = SepFunctions.LangText("~~Site Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 7;

            if ((SepFunctions.Setup(GlobalVars.ModuleID, "UPagesEnable") != "Enable" || SepFunctions.isUserPage()) && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("UPagesCreate"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var mp = Page.Master;
            var form_ = (HtmlForm)mp.FindControl("aspnetForm");
            form_.Attributes.Add("enctype", "multipart/form-data");

            if (SepFunctions.CompareKeys(SepFunctions.Security("UPagesPortalSelection"), false) == false) PortalSelectionRow.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SiteID")))
            {
                var jUserPages = SepCommon.DAL.UserPages.Site_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("SiteID")));

                if (jUserPages.SiteID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Site~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Web Site");
                    SiteID.Value = SepCommon.SepCore.Request.Item("SiteID");
                    Category.CatID = Strings.ToString(jUserPages.CatID);
                    SiteName.Value = jUserPages.SiteName;
                    Description.Value = jUserPages.Description;
                    ShowList.Value = Strings.ToString(jUserPages.ShowList);
                    TemplateID.Text = Strings.ToString(jUserPages.TemplateID);
                    EnableGuestbook.Checked = jUserPages.Guestbook;
                    if (jUserPages.InviteOnly == false)
                    {
                        InviteOnly.Value = "false";
                    }
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                }
                else
                {
                    InviteOnly.Value = "false";
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT SiteID FROM UPagesSites WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        SepFunctions.Redirect(sInstallFolder + "userpages_config.aspx");
                                        return;
                                    }
                                }
                            }
                        }

                    SiteID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostCreateSite", "GetCreateSite", SiteID.Value, true) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(107);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            var intReturn = SepCommon.DAL.UserPages.Save_Web_Site(SepFunctions.toLong(SiteID.Value), SepFunctions.toLong(Category.CatID), SepFunctions.Session_User_ID(), SiteName.Value, SiteSlogan.Value, Description.Value, SepFunctions.toLong(TemplateID.Text), SepFunctions.toBoolean(ShowList.Value), SepFunctions.toBoolean(InviteOnly.Value), EnableGuestbook.Checked, SepFunctions.toLong(PortalSelection.Text));
            if (intReturn == 3)
            {
                var jSiteTemplates = SepCommon.DAL.SiteTemplates.Template_Get(SepFunctions.toLong(TemplateID.Text));

                if (jSiteTemplates.TemplateID > 0)
                {
                    var sConfigFile = SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config_default.xml";

                    if (File.Exists(sConfigFile))
                    {
                        XmlDocument doc = new XmlDocument() { XmlResolver = null };
                        using (StreamReader sreader = new StreamReader(sConfigFile))
                        {
                            using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                            {
                                doc.Load(reader);
                                var root = doc.DocumentElement;
                                XmlNodeList nodelist = null;

                                long xmlCount = 0;
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

                                if (File.Exists(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml") == false)
                                    using (var sw = File.CreateText(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml"))
                                    {
                                        sw.WriteLine(doc.OuterXml);
                                    }
                                else
                                    using (var outfile = new StreamWriter(SepFunctions.GetDirValue("skins") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml"))
                                    {
                                        outfile.Write(doc.OuterXml);
                                    }
                            }
                        }
                    }
                }

                SepFunctions.Redirect(sInstallFolder + "userpages_pages.aspx?DoAction=SiteAdded");
            }

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, SiteName.Value);
        }
    }
}