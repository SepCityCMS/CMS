// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="webpages_link_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class webpages_link_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class webpages_link_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add External Link");
                    MenuIDLabel.InnerText = SepFunctions.LangText("Select a Menu:");
                    LinkTextLabel.InnerText = SepFunctions.LangText("Link Text:");
                    PageURLLabel.InnerText = SepFunctions.LangText("Page URL:");
                    TargetWindowLabel.InnerText = SepFunctions.LangText("Target Window:");
                    LinkTextRequired.ErrorMessage = SepFunctions.LangText("~~Link Text~~ is required.");
                    PageURLRequired.ErrorMessage = SepFunctions.LangText("~~Page URL~~ is required.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminEditPage")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminEditPage"), false) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UniqueID")) && string.IsNullOrWhiteSpace(LinkText.Value))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM ModulesNPages WHERE UniqueID=@UniqueID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", SepCommon.SepCore.Request.Item("UniqueID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                ModifyLegend.InnerText = SepFunctions.LangText("Edit Web Page");
                                LinkID.Value = SepFunctions.openNull(RS["UniqueID"]);
                                MenuID.MenuID = SepFunctions.openNull(RS["MenuID"]);
                                LinkText.Value = SepFunctions.openNull(RS["LinkText"]);
                                PageURL.Value = SepFunctions.openNull(RS["UserPageName"]);
                                TargetWindow.Value = SepFunctions.openNull(RS["TargetWindow"]);
                            }
                            else
                            {
                                TargetWindow.Value = "_parent";
                                MenuID.MenuID = SepCommon.SepCore.Request.Item("MenuID");
                            }

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TargetWindow.Value)) TargetWindow.Value = "_parent";
                MenuID.MenuID = SepCommon.SepCore.Request.Item("MenuID");
                if (string.IsNullOrWhiteSpace(LinkID.Value)) LinkID.Value = Strings.ToString(SepFunctions.GetIdentity());
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sReturn = SepCommon.DAL.WebPages.Save_External_Link(SepFunctions.toLong(LinkID.Value), SepFunctions.toLong(MenuID.MenuID), LinkText.Value, PageURL.Value, TargetWindow.Value, SepFunctions.Get_Portal_ID());

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}