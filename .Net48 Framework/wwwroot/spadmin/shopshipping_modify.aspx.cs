// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopshipping_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class shopshipping_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopshipping_modify : Page
    {
        /// <summary>
        /// Handles the SelectedIndexChanged event of the Carrier control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void Carrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_Shipping_Service(Carrier.SelectedValue);
        }

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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Shipping Method");
                    MethodNameLabel.InnerText = SepFunctions.LangText("Method Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    CarrierLabel.InnerText = SepFunctions.LangText("Carrier:");
                    ShippingServiceLabel.InnerText = SepFunctions.LangText("Shipping Service:");
                    DeliveryTimeLabel.InnerText = SepFunctions.LangText("Delivery Time:");
                    MethodNameRequired.ErrorMessage = SepFunctions.LangText("~~Method Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
            }
        }

        /// <summary>
        /// Updates the shipping service.
        /// </summary>
        /// <param name="sCarrier">The s carrier.</param>
        public void Update_Shipping_Service(string sCarrier)
        {
            ShippingService.Items.Clear();

            switch (sCarrier)
            {
                case "FedEx":

                    // ----------------------------------------------- Fed Ex Options -----------------------------------------------
                    ShippingService.Items.Add(new ListItem("FedEx Ground", "FEDEX_GROUND"));
                    ShippingService.Items.Add(new ListItem("FedEx 2 Day", "FEDEX_2_DAY"));
                    ShippingService.Items.Add(new ListItem("FedEx Express Saver", "FEDEX_EXPRESS_SAVER"));
                    ShippingService.Items.Add(new ListItem("FedEx Standard Overnight", "STANDARD_OVERNIGHT"));
                    ShippingService.Items.Add(new ListItem("FedEx Priority Overnight", "PRIORITY_OVERNIGHT"));
                    ShippingService.Items.Add(new ListItem("FedEx Ground Home Delivery", "GROUND_HOME_DELIVERY"));
                    ShippingService.Items.Add(new ListItem("FedEx Inernational Economy", "INTERNATIONAL_ECONOMY"));
                    ShippingService.Items.Add(new ListItem("FedEx International First", "INTERNATIONAL_FIRST"));
                    ShippingService.Items.Add(new ListItem("FedEx International Priority", "INTERNATIONAL_PRIORITY"));
                    ShippingService.Items.Add(new ListItem("FedEx First Overnight", "FIRST_OVERNIGHT"));
                    break;

                case "UPS":

                    // ----------------------------------------------- UPS Options -----------------------------------------------
                    ShippingService.Items.Add(new ListItem("UPS Next Day Air", "01"));
                    ShippingService.Items.Add(new ListItem("UPS 2nd Day Air", "02"));
                    ShippingService.Items.Add(new ListItem("UPS Ground", "03"));
                    ShippingService.Items.Add(new ListItem("UPS Worldwide Express", "07"));
                    ShippingService.Items.Add(new ListItem("UPS Worldwide Expedited", "08"));
                    ShippingService.Items.Add(new ListItem("UPS Standard", "11"));
                    ShippingService.Items.Add(new ListItem("UPS 3 Day Select", "12"));
                    ShippingService.Items.Add(new ListItem("UPS Next Day Air Saver", "13"));
                    ShippingService.Items.Add(new ListItem("UPS Next Day Air Early A.M.", "14"));
                    ShippingService.Items.Add(new ListItem("UPS Worldwide Express Plus", "54"));
                    ShippingService.Items.Add(new ListItem("UPS 2nd Day Air A.M.", "59"));
                    ShippingService.Items.Add(new ListItem("UPS Saver", "65"));
                    break;

                default:

                    // ----------------------------------------------- USPS -----------------------------------------------
                    ShippingService.Items.Add(new ListItem("USPS First Class", "First Class"));
                    ShippingService.Items.Add(new ListItem("USPS First Class Commercial", "First Class Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS First Class  HFP Commercial", "First Class  HFP Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS Priority", "Priority"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Commercial", "Priority Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Cpp", "Priority Cpp"));
                    ShippingService.Items.Add(new ListItem("USPS Priority HFP Commercial", "Priority HFP Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS Priority HFP CPP", "Priority HFP CPP"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express", "Priority Mail Express"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express Commercial", "Priority Mail Express Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express CPP", "Priority Mail Express CPP"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express Sh", "Priority Mail Express Sh"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express Sh Commercial ", "Priority Mail Express Sh Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express", "Priority Mail Express"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express HFP", "Priority Mail Express HFP"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express HFP Commercial", "Priority Mail Express HFP Commercial"));
                    ShippingService.Items.Add(new ListItem("USPS Priority Mail Express HFP CPP", "Priority Mail Express HFP CPP"));
                    ShippingService.Items.Add(new ListItem("USPS Retail Ground", "Retail Ground"));
                    ShippingService.Items.Add(new ListItem("USPS Media", "Media"));
                    ShippingService.Items.Add(new ListItem("USPS Library", "Library"));
                    ShippingService.Items.Add(new ListItem("USPS All", "All"));
                    ShippingService.Items.Add(new ListItem("USPS Online", "Online"));
                    ShippingService.Items.Add(new ListItem("USPS Plus", "Plus"));
                    break;
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ShopCartAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin"), true) == false)
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
                Update_Shipping_Service("FedEx");
                if (SepFunctions.isFedExEnabled(0)) Carrier.Items.Add(new ListItem("FedEx", "FedEx"));
                if (SepFunctions.isUPSEnabled(0)) Carrier.Items.Add(new ListItem("UPS", "UPS"));
                if (SepFunctions.isUSPSEnabled(0)) Carrier.Items.Add(new ListItem("USPS", "USPS"));
                if (SepFunctions.isUSPSEnabled(0)) Carrier.Items.Add(new ListItem("Canada Post", "CanadaPost"));
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("MethodID")))
            {
                var jShipping = SepCommon.DAL.ShoppingMall.Shipping_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("MethodID")), "0");

                if (jShipping.MethodID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Shipping Method~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Shipping Method");
                    MethodID.Value = SepCommon.SepCore.Request.Item("MethodID");
                    MethodName.Value = jShipping.MethodName;
                    Description.Value = jShipping.Description;
                    Carrier.SelectedValue = jShipping.Carrier;
                    Update_Shipping_Service(jShipping.Carrier);
                    ShippingService.Value = jShipping.ShippingService;
                    DeliveryTime.Value = jShipping.DeliveryTime;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(MethodID.Value)) MethodID.Value = Strings.ToString(SepFunctions.GetIdentity());
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sReturn = SepCommon.DAL.ShoppingMall.Shipping_Save(SepFunctions.toLong(MethodID.Value), 0, MethodName.Value, Description.Value, Carrier.SelectedValue, ShippingService.Value, DeliveryTime.Value, "0", SepFunctions.Get_Portal_ID());

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}