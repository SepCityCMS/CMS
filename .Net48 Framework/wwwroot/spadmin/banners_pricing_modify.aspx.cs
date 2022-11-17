// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="banners_pricing_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class advertising_pricing_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class advertising_pricing_modify : Page
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
                    RecurringCycle.Items[0].Text = SepFunctions.LangText("1 Month");
                    RecurringCycle.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle.Items[3].Text = SepFunctions.LangText("1 Year");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Price");
                    PlanNameLabel.InnerText = SepFunctions.LangText("Plan Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    OnetimePriceLabel.InnerText = SepFunctions.LangText("One-time Price:");
                    RecurringPriceLabel.InnerText = SepFunctions.LangText("Recurring Price:");
                    RecurringCycleLabel.InnerText = SepFunctions.LangText("Recurring Cycle:");
                    MaximumClicksLabel.InnerText = SepFunctions.LangText("Maximum Clicks (Enter \"-1\" for Unlimited):");
                    MaximumExposuresLabel.InnerText = SepFunctions.LangText("Maximum Exposures (Enter \"-1\" for Unlimited):");
                    ZonesLabel.InnerText = SepFunctions.LangText("Target Pricing to Zones:");
                    CategoriesLabel.InnerText = SepFunctions.LangText("Target Pricing to Categories:");
                    PagesLabel.InnerText = SepFunctions.LangText("Target Pricing to Pages:");
                    PortalsLabel.InnerText = SepFunctions.LangText("Target Pricing to Portals:");
                    PlanNameRequired.ErrorMessage = SepFunctions.LangText("~~Plan Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
                    OnetimePriceRequired.ErrorMessage = SepFunctions.LangText("~~One-time Price~~ is required.");
                    RecurringPriceRequired.ErrorMessage = SepFunctions.LangText("~~Recurring Price~~ is required.");
                    RecurringCycleRequired.ErrorMessage = SepFunctions.LangText("~~Recurring Cycle~~ is required.");
                    MaximumClicksRequired.ErrorMessage = SepFunctions.LangText("~~Maximum Clicks~~ is required.");
                    MaximumExposuresRequired.ErrorMessage = SepFunctions.LangText("~~Maximum Exposures~~ is required.");
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

            GlobalVars.ModuleID = 2;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdsAdmin"), false) == false)
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
                if (SepFunctions.showCategories() == false) CategoriesRow.Visible = false;

                if (SepFunctions.CompareKeys(SepFunctions.Security("AdminEditPage"), true) == false) WebPagesRow.Visible = false;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PriceID")))
            {
                var jPrice = SepCommon.DAL.Advertisements.Advertisement_Price_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PriceID")));

                if (jPrice.PriceID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Advertisement Price~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Price");
                    PriceID.Value = SepCommon.SepCore.Request.Item("PriceID");
                    PlanName.Value = jPrice.PlanName;
                    Description.Value = jPrice.Description;
                    OnetimePrice.Value = jPrice.OnetimePrice;
                    RecurringPrice.Value = jPrice.RecurringPrice;
                    RecurringCycle.Value = jPrice.RecurringCycle;
                    MaximumClicks.Value = Strings.ToString(jPrice.MaximumClicks);
                    MaximumExposures.Value = Strings.ToString(jPrice.MaximumExposures);
                    Zones.Text = jPrice.Zones;
                    Categories.Text = jPrice.Categories;
                    Pages.Text = jPrice.Pages;
                    Portals.Text = jPrice.Portals;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (string.IsNullOrWhiteSpace(PriceID.Value)) PriceID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    OnetimePrice.Value = SepFunctions.Format_Currency("0");
                    RecurringPrice.Value = SepFunctions.Format_Currency("0");
                    MaximumClicks.Value = "-1";
                    MaximumExposures.Value = "-1";
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
            string sReturn;

            sReturn = SepCommon.DAL.Advertisements.Advertisement_Price_Save(SepFunctions.toLong(PriceID.Value), PlanName.Value, Description.Value, SepFunctions.toDecimal(OnetimePrice.Value), SepFunctions.toDecimal(RecurringPrice.Value), RecurringCycle.Value, MaximumClicks.Value, MaximumExposures.Value, Zones.Text, Categories.Text, Pages.Text, Portals.Text);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}