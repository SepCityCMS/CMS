// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="approval_chains_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class approval_chains_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class approval_chains_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Approval Chain");
                    ChainNameLabel.InnerText = SepFunctions.LangText("Chain Name:");
                    ModulesLabel.InnerText = SepFunctions.LangText("Modules to apply this approval chain to:");
                    PortalsLabel.InnerText = SepFunctions.LangText("Portals to apply this approval chain to:");
                    ChainNameRequired.ErrorMessage = SepFunctions.LangText("~~Chain Name~~ is required.");
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

            GlobalVars.ModuleID = 983;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ChainID")))
            {
                var jApprovalChains = SepCommon.DAL.ApprovalChains.Chain_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChainID")));

                if (jApprovalChains.ApproveID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Approval Chain~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Approval Chain");
                    ChainID.Value = SepCommon.SepCore.Request.Item("ChainID");
                    ChainName.Value = jApprovalChains.ChainName;
                    Modules.Text = jApprovalChains.ModuleIDs;
                    Portals.Text = jApprovalChains.PortalIDs;
                    ApprovalName1.Value = jApprovalChains.ApprovalName1;
                    ApprovalEmail1.Value = jApprovalChains.ApprovalEmail1;
                    Weight1.Value = jApprovalChains.Weight1;
                    ApprovalName2.Value = jApprovalChains.ApprovalName2;
                    ApprovalEmail2.Value = jApprovalChains.ApprovalEmail2;
                    Weight2.Value = jApprovalChains.Weight2;
                    ApprovalName3.Value = jApprovalChains.ApprovalName3;
                    ApprovalEmail3.Value = jApprovalChains.ApprovalEmail3;
                    Weight3.Value = jApprovalChains.Weight3;
                    ApprovalName4.Value = jApprovalChains.ApprovalName4;
                    ApprovalEmail4.Value = jApprovalChains.ApprovalEmail4;
                    Weight4.Value = jApprovalChains.Weight4;
                    ApprovalName5.Value = jApprovalChains.ApprovalName5;
                    ApprovalEmail5.Value = jApprovalChains.ApprovalEmail5;
                    Weight5.Value = jApprovalChains.Weight5;
                    ApprovalName6.Value = jApprovalChains.ApprovalName6;
                    ApprovalEmail6.Value = jApprovalChains.ApprovalEmail6;
                    Weight6.Value = jApprovalChains.Weight6;
                    ApprovalName7.Value = jApprovalChains.ApprovalName7;
                    ApprovalEmail7.Value = jApprovalChains.ApprovalEmail7;
                    Weight7.Value = jApprovalChains.Weight7;
                    ApprovalName8.Value = jApprovalChains.ApprovalName8;
                    ApprovalEmail8.Value = jApprovalChains.ApprovalEmail8;
                    Weight8.Value = jApprovalChains.Weight8;
                    ApprovalName9.Value = jApprovalChains.ApprovalName9;
                    ApprovalEmail9.Value = jApprovalChains.ApprovalEmail9;
                    Weight9.Value = jApprovalChains.Weight9;
                    ApprovalName10.Value = jApprovalChains.ApprovalName10;
                    ApprovalEmail10.Value = jApprovalChains.ApprovalEmail10;
                    Weight10.Value = jApprovalChains.Weight10;
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Modules.Text = SepCommon.SepCore.Request.Item("Modules");
                    Portals.Text = SepCommon.SepCore.Request.Item("Portals");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(ChainID.Value)) ChainID.Value = Strings.ToString(SepFunctions.GetIdentity());
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
            var sReturn = SepCommon.DAL.ApprovalChains.Chain_Save(SepFunctions.toLong(ChainID.Value), ChainName.Value, SepCommon.SepCore.Request.Item("Modules"), SepCommon.SepCore.Request.Item("Portals"), ApprovalName1.Value, ApprovalEmail1.Value, Weight1.Value, ApprovalName2.Value, ApprovalEmail2.Value, Weight2.Value, ApprovalName3.Value, ApprovalEmail3.Value, Weight3.Value, ApprovalName4.Value, ApprovalEmail4.Value, Weight4.Value, ApprovalName5.Value, ApprovalEmail5.Value, Weight5.Value, ApprovalName6.Value, ApprovalEmail6.Value, Weight6.Value, ApprovalName7.Value, ApprovalEmail7.Value, Weight7.Value, ApprovalName8.Value, ApprovalEmail8.Value, Weight8.Value, ApprovalName9.Value, ApprovalEmail9.Value, Weight9.Value, ApprovalName10.Value, ApprovalEmail10.Value, Weight10.Value);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}