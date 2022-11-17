// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_pages_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class portals_pages_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_pages_modify : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The get module identifier
        /// </summary>
        public static int GetModuleID;

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
                    Enabled.Items[0].Text = SepFunctions.LangText("Yes");
                    Enabled.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Web Page");
                    PageTitleLabel.InnerText = SepFunctions.LangText("Page Title:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Meta Description:");
                    KeywordsLabel.InnerText = SepFunctions.LangText("Meta Keywords:");
                    EnabledLabel.InnerText = SepFunctions.LangText("Enable Web Page:");
                    LinkTextLabel.InnerText = SepFunctions.LangText("Link Text:");
                    MenuIDLabel.InnerText = SepFunctions.LangText("Select a Menu:");
                    CatIDLabel.InnerText = SepFunctions.LangText("Select a Category:");
                    PortalIDsLabel.InnerText = SepFunctions.LangText("List web pages in the following portals:");
                    LinkTextRequired.ErrorMessage = SepFunctions.LangText("~~Link Text~~ is required.");
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

            GlobalVars.ModuleID = 60;
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("PortalsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), true) == false)
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

            if (SepFunctions.Get_Portal_ID() == -1)
            {
                GetModuleID = 60;
                PortalSelection.Visible = true;
            }
            else
            {
                PortalSelection.Visible = false;
                PortalIDs.Text = string.Empty;
            }

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0)
            {
                GetModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                ModuleID.Value = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")).ToString();
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ShowCat"))) ShowCat.Value = SepCommon.SepCore.Request.Item("ShowCat");
                if (ShowCat.Value != "True") CategorySelection.Visible = false;
                GetModuleID = SepFunctions.toInt(ModuleID.Value);
            }
            else
            {
                CategorySelection.Visible = false;
            }

            if (GetModuleID > 0)
            {
                CatID.ModuleID = GetModuleID;
            }

            if (!Page.IsPostBack && (SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0 || !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UniqueID"))))
            {

                string SqlStr;
                if (GetModuleID > 0)
                {
                    SqlStr = "SELECT * FROM PortalPages WHERE PortalID=" + SepFunctions.Get_Portal_ID() + " AND PageID='" + SepFunctions.FixWord(Strings.ToString(GetModuleID)) + "'";

                    CatID.ModuleID = GetModuleID;
                }
                else
                {
                    SqlStr = "SELECT * FROM PortalPages WHERE PortalID=" + SepFunctions.Get_Portal_ID() + " AND UniqueID=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("UniqueID"));
                }

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        conn.Open();
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                ModifyLegend.InnerText = SepFunctions.LangText("Edit Web Page");
                                PageID.Value = SepFunctions.openNull(RS["UniqueID"]);
                                SEOPageTitle.Value = SepFunctions.openNull(RS["PageTitle"]);
                                SEODescription.Value = SepFunctions.openNull(RS["Description"]);
                                SEOKeywords.Value = SepFunctions.openNull(RS["Keywords"]);
                                Enabled.Value = SepFunctions.openNull(RS["Status"]);
                                LinkText.Value = SepFunctions.openNull(RS["LinkText"]);
                                MenuID.MenuID = SepFunctions.openNull(RS["MenuID"]);
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["UniqueID"])) == 200) MenuID.ShowNotOnAMenu = true;
                                CatID.CatID = SepCommon.SepCore.Request.Item("CatID");
                                PortalIDs.Text = SepFunctions.openNull(RS["PortalIDs"]);

                                PageText.Text = SepFunctions.openNull(RS["PageText"]);
                            }
                            else
                            {
                                MenuID.MenuID = SepCommon.SepCore.Request.Item("MenuID");
                            }

                        }
                    }
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    MenuID.MenuID = Request.Form["MenuID"];
                    CatID.CatID = Request.Form["CatID"];
                }
                else
                {
                    MenuID.MenuID = SepCommon.SepCore.Request.Item("MenuID");
                    if (string.IsNullOrWhiteSpace(PageID.Value)) PageID.Value = Strings.ToString(SepFunctions.GetIdentity());

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0)
                    {
                        GetModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                        ModuleID.Value = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")).ToString();
                        if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ShowCat"))) ShowCat.Value = SepCommon.SepCore.Request.Item("ShowCat");
                        if (ShowCat.Value != "True") CategorySelection.Visible = false;
                        GetModuleID = SepFunctions.toInt(ModuleID.Value);
                    }
                    else
                    {
                        CategorySelection.Visible = false;
                    }
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
            var sReturn = SepCommon.DAL.WebPages.Save_Web_Page(SepFunctions.toLong(PageID.Value), SepFunctions.toLong(Request.Form["MenuID"]), SepFunctions.Session_User_ID(), LinkText.Value, PageText.Text, SEOPageTitle.Value, SEODescription.Value, SEOKeywords.Value, string.Empty, string.Empty, SepFunctions.toLong(Enabled.Value), SepFunctions.toLong(CatID.CatID), SepFunctions.toInt(ModuleID.Value), SepFunctions.Get_Portal_ID(), SepFunctions.Get_Portal_ID() == -1 ? PortalIDs.Text : string.Empty);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}