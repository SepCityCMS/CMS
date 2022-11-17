// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shoppingmall_wholesale2b_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class shoppingmall_wholesale2b_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    partial class shoppingmall_wholesale2b_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Feed");
                    FeedNameLabel.InnerText = SepFunctions.LangText("Feed Name:");
                    FeedURLLabel.InnerText = SepFunctions.LangText("Feed URL:");
                    AccessKeysLabel.InnerText = SepFunctions.LangText("Access keys required to access this category:");
                    PortalSelectionLabel.InnerText = SepFunctions.LangText("Select the portals to show this category in:");
                    FeedNameRequired.ErrorMessage = SepFunctions.LangText("~~Feed Name~~ is required.");
                    FeedURLRequired.ErrorMessage = SepFunctions.LangText("~~Feed URL~~ is required.");
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

            GlobalVars.ModuleID = 41;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ShopMallAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopMallAdmin"), false) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FeedID")))
            {
                var jFeeds = SepCommon.DAL.ShoppingMall.Wholesale2b_Feed_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FeedID")));

                if (jFeeds.FeedID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Product~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Feed");
                    FeedID.Value = SepCommon.SepCore.Request.Item("FeedID");
                    FeedName.Value = jFeeds.FeedName;
                    FeedURL.Value = jFeeds.FeedURL;
                    AccessKeysSelection.Text = jFeeds.AccessKeys;
                    AccessKeysHide.Checked = jFeeds.AccessHide;
                    PortalSelection.Text = jFeeds.PortalIDs;
                    ShareContent.Checked = jFeeds.Sharing;
                    ExcludePortalSecurity.Checked = jFeeds.ExcPortalSecurity;
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    AccessKeysSelection.Text = Request.Form["AccessKeysSelection"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(FeedID.Value)) FeedID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    PortalSelection.Text = "|" + SepFunctions.Get_Portal_ID() + "|";
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
            var sReturn = SepCommon.DAL.ShoppingMall.Wholesale2b_Feed_Save(SepFunctions.toLong(FeedID.Value), FeedName.Value, FeedURL.Value, AccessKeysSelection.Text, AccessKeysHide.Checked, PortalSelection.Text, ShareContent.Checked, ExcludePortalSecurity.Checked, 1);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}