// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="affiliate_downline.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class affiliate_downline.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class affiliate_downline : Page
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
                    ManageGridView.Columns[0].HeaderText = SepFunctions.LangText("Level");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("User Name");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("Full Name / Email Address");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Date Joined");
                    ManageGridView.Columns[4].HeaderText = SepFunctions.LangText("Go");
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
        /// Handles the RowDataBound event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var AffiliateLevel = (HiddenField)e.Row.FindControl("AffiliateLevel");
                if (SepFunctions.toLong(AffiliateLevel.Value) >= 5)
                {
                    var MoreResults = (Button)e.Row.FindControl("MoreResults");
                    MoreResults.Visible = false;
                }

                if (SepFunctions.toLong(AffiliateLevel.Value) == 1)
                {
                    var PrevResults = (Button)e.Row.FindControl("PrevResults");
                    PrevResults.Visible = false;
                }

                var AffiliateHasLevels = (HiddenField)e.Row.FindControl("AffiliateHasLevels");
                if (string.IsNullOrWhiteSpace(AffiliateHasLevels.Value))
                {
                    var MoreResults = (Button)e.Row.FindControl("MoreResults");
                    MoreResults.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the MoreResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void MoreResults_Click(object sender, EventArgs e)
        {
            // Get the button that raised the event
            var btn = (Button)sender;

            switch (btn.CommandName)
            {
                case "AffiliateID":
                    var arrAffiliate = Strings.Split(btn.CommandArgument, "||");
                    Array.Resize(ref arrAffiliate, 2);
                    var iLevel = SepFunctions.toInt(arrAffiliate[1]) + 1;

                    if (iLevel > 5) iLevel = 5;

                    var dAffiliateDownline = SepCommon.DAL.Affiliate.GetAffiliateDownline(SepFunctions.toLong(arrAffiliate[0]), iLevel);

                    ManageGridView.DataSource = dAffiliateDownline.Take(50);
                    ManageGridView.DataBind();
                    break;
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AffiliateAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AffiliateAdmin"), false) == false)
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

            if (!Page.IsPostBack)
            {
                var dAffiliateDownline = SepCommon.DAL.Affiliate.GetAffiliateDownline(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AffiliateID")), 1);

                ManageGridView.DataSource = dAffiliateDownline.Take(50);
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No members are affiliated with this member.") + "</div>";
                    ManageGridView.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the PrevResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PrevResults_Click(object sender, EventArgs e)
        {
            // Get the button that raised the event
            var btn = (Button)sender;

            switch (btn.CommandName)
            {
                case "ReferralID":
                    var arrReferralID = Strings.Split(btn.CommandArgument, "||");
                    Array.Resize(ref arrReferralID, 2);
                    var iLevel = SepFunctions.toInt(arrReferralID[1]) - 1;

                    if (iLevel < 1) iLevel = 1;

                    var dAffiliateDownline = SepCommon.DAL.Affiliate.GetAffiliateDownline(SepFunctions.toLong(arrReferralID[0]), iLevel);

                    ManageGridView.DataSource = dAffiliateDownline.Take(50);
                    ManageGridView.DataBind();
                    break;
            }
        }
    }
}