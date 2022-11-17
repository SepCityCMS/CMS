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

namespace wwwroot.spadmin.templatedesigner
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class _default.
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class _default : Page
    {
        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
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
                loader.Visible = false;
                jscript.Visible = false;
                gjs.Style.Add("display", "");
                gjs.Style.Add("overflow", "");
                templateHTML.Text = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Login is Required") + "</div>";
                return;
            }

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TemplateID")))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", SepCommon.SepCore.Request.Item("TemplateID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (File.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\template.master"))
                                {
                                    if (SepFunctions.Get_Portal_ID() > 0)
                                    {
                                        if (File.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\custom" + SepFunctions.Get_Portal_ID() + ".master"))
                                        {
                                            using (var readfile = new StreamReader(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\custom" + SepFunctions.Get_Portal_ID() + ".master"))
                                            {
                                                templateHTML.Text = SepCommon.SepCore.Strings.Replace(SepFunctions.ParseXML("body", readfile.ReadToEnd()), "<%= SepCommon.SiteTemplate.getVariable(\"HeaderImg\")%>", SepCommon.SiteTemplate.getVariable("HeaderImg"));
                                                templateStyle.Text = "/skins/" + SepFunctions.openNull(RS["FolderName"]) + "/styles/layout.css";
                                                customStyle.Text = "/skins/" + SepFunctions.openNull(RS["FolderName"]) + "/styles/custom" + SepFunctions.Get_Portal_ID() + ".css";
                                                TemplateID.Value = SepCommon.SepCore.Request.Item("TemplateID");
                                            }
                                        }
                                        else
                                        {
                                            using (var readfile = new StreamReader(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\template.master"))
                                            {
                                                templateHTML.Text = SepCommon.SepCore.Strings.Replace(SepFunctions.ParseXML("body", readfile.ReadToEnd()), "<%= SepCommon.SiteTemplate.getVariable(\"HeaderImg\")%>", SepCommon.SiteTemplate.getVariable("HeaderImg"));
                                                templateStyle.Text = "/skins/" + SepFunctions.openNull(RS["FolderName"]) + "/styles/layout.css";
                                                TemplateID.Value = SepCommon.SepCore.Request.Item("TemplateID");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\custom0.master"))
                                        {
                                            using (var readfile = new StreamReader(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\custom0.master"))
                                            {
                                                templateHTML.Text = SepCommon.SepCore.Strings.Replace(SepFunctions.ParseXML("body", readfile.ReadToEnd()), "<%= SepCommon.SiteTemplate.getVariable(\"HeaderImg\")%>", SepCommon.SiteTemplate.getVariable("HeaderImg"));
                                                templateStyle.Text = "/skins/" + SepFunctions.openNull(RS["FolderName"]) + "/styles/layout.css";
                                                customStyle.Text = "/skins/" + SepFunctions.openNull(RS["FolderName"]) + "/styles/custom.css";
                                                TemplateID.Value = SepCommon.SepCore.Request.Item("TemplateID");
                                            }
                                        }
                                        else
                                        {
                                            using (var readfile = new StreamReader(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\template.master"))
                                            {
                                                templateHTML.Text = SepCommon.SepCore.Strings.Replace(SepFunctions.ParseXML("body", readfile.ReadToEnd()), "<%= SepCommon.SiteTemplate.getVariable(\"HeaderImg\")%>", SepCommon.SiteTemplate.getVariable("HeaderImg"));
                                                templateStyle.Text = "/skins/" + SepFunctions.openNull(RS["FolderName"]) + "/styles/layout.css";
                                                TemplateID.Value = SepCommon.SepCore.Request.Item("TemplateID");
                                            }
                                        }
                                    }
                                    ModuleID.Value = SepCommon.SepCore.Request.Item("ModuleID");
                                    PortalID.Value = SepCommon.SepCore.Strings.ToString(SepFunctions.Get_Portal_ID());
                                    if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PageID")))
                                    {
                                        PageID.Value = "16";
                                        ModuleID.Value = "16";
                                    }
                                    else
                                    {
                                        PageID.Value = SepCommon.SepCore.Request.Item("PageID");
                                    }

                                    string strSql;
                                    if (SepFunctions.Get_Portal_ID() == 0)
                                    {
                                        if (SepFunctions.toLong(ModuleID.Value) == 0)
                                        {
                                            strSql = "SELECT UniqueID,LinkText,MenuID FROM ModulesNPages WHERE UniqueID='" + SepFunctions.toLong(PageID.Value) + "'";
                                        }
                                        else
                                        {
                                            strSql = "SELECT UniqueID,LinkText,MenuID FROM ModulesNPages WHERE PageID='" + SepFunctions.toLong(PageID.Value) + "'";
                                        }
                                        using (var cmd2 = new SqlCommand(strSql, conn))
                                        {
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (RS2.HasRows)
                                                {
                                                    RS2.Read();
                                                    PageID.Value = SepFunctions.openNull(RS2["UniqueID"]);
                                                    MenuID.Value = SepFunctions.openNull(RS2["MenuID"]);
                                                    PageTitle.Value = SepFunctions.openNull(RS2["LinkText"]);
                                                    PageName.Text = SepFunctions.openNull(RS2["LinkText"]);
                                                    ModuleID.Value = "0";
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (SepFunctions.toLong(ModuleID.Value) == 0)
                                        {
                                            strSql = "SELECT UniqueID,MenuID,LinkText FROM PortalPages WHERE PortalID=@PortalID AND UniqueID='" + SepFunctions.toLong(PageID.Value) + "' AND Status=1";
                                        }
                                        else
                                        {
                                            strSql = "SELECT UniqueID,MenuID,LinkText FROM PortalPages WHERE PortalID=@PortalID AND PageID='" + SepFunctions.toLong(PageID.Value) + "' AND Status=1";
                                        }
                                        using (var cmd2 = new SqlCommand(strSql, conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (RS2.HasRows)
                                                {
                                                    RS2.Read();
                                                    PageID.Value = SepFunctions.openNull(RS2["UniqueID"]);
                                                    MenuID.Value = SepFunctions.openNull(RS2["MenuID"]);
                                                    PageTitle.Value = SepFunctions.openNull(RS2["LinkText"]);
                                                    PageName.Text = SepFunctions.openNull(RS2["LinkText"]);
                                                    ModuleID.Value = "0";
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                if (File.Exists(SepFunctions.GetDirValue("spadmin") + "templatedesigner\\templates\\linkline.html"))
                {
                    using (var readfile = new StreamReader(SepFunctions.GetDirValue("spadmin") + "templatedesigner\\templates\\linkline.html"))
                    {
                        Replace cReplace = new BusinessObjects.Replace();
                        templateHTML.Text = cReplace.Replace_Widgets(readfile.ReadToEnd(), 0);
                        cReplace.Dispose();
                    }
                }
            }
        }
    }
}