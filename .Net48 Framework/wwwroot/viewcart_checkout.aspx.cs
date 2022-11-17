// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="viewcart_checkout.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Moneris;
    using SepCommon;
    using SepCommon.Integrations.Shipping;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using Square;
    using Square.Apis;
    using Square.Exceptions;
    using Square.Models;
    using Stripe;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class viewcart_checkout.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class viewcart_checkout : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        string RecurringCycle = string.Empty;

        decimal RecurringPrice = 0;

        decimal TotalSetup = 0;

        decimal totalTaxes = 0;

        /// <summary>
        /// Gets or sets the result message.
        /// </summary>
        /// <value>The result message.</value>
        public string ResultMessage { get; set; }

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
        /// Formats the currency.
        /// </summary>
        /// <param name="sNum">The s number.</param>
        /// <returns>System.String.</returns>
        public string Format_Currency(object sNum)
        {
            return SepFunctions.Format_Currency(sNum);
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
                    PaymentMethod.Items[0].Text = SepFunctions.LangText("--- Make Your Selection ---");
                    CardType.Items[0].Text = SepFunctions.LangText("Visa");
                    CardType.Items[1].Text = SepFunctions.LangText("MasterCard");
                    CardType.Items[2].Text = SepFunctions.LangText("American Express");
                    CardType.Items[3].Text = SepFunctions.LangText("Discover");
                    PaymentMethodLabel.InnerText = SepFunctions.LangText("Payment Method:");
                    CardTypeLabel.InnerText = SepFunctions.LangText("Credit / Debit Card Type:");
                    CardNumberLabel.InnerText = SepFunctions.LangText("Credit / Debit Card Number:");
                    NameOnCardLabel.InnerText = SepFunctions.LangText("Name on Card:");
                    ExpireDateLabel.InnerText = SepFunctions.LangText("Expiration Date:");
                    SecurityCodeLabel.InnerText = SepFunctions.LangText("Security Code:");
                    CommentsLabel.InnerText = SepFunctions.LangText("Order Comments: (Optional)");
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
            var shipCount = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var StoreID = SepFunctions.toLong(Strings.ToString(ManageGridView.DataKeys[e.Row.RowIndex].Value));
                var rShipping = (RadioButtonList)e.Row.FindControl("rbMethods");

                var dInvoices = SepCommon.DAL.Invoices.GetInvoicesProducts(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), StoreID);
                var rProducts = (Repeater)e.Row.FindControl("rtProducts");
                rProducts.DataSource = dInvoices;
                rProducts.DataBind();
                var shipperStreet = string.Empty;
                var shipperCity = string.Empty;
                var shipperState = string.Empty;
                var shipperCountry = string.Empty;

                var destinationStreet = string.Empty;
                var destinationCity = string.Empty;
                var destinationState = string.Empty;
                var destinationCountry = string.Empty;

                var packageCount = 0;

                var isFedExEnabled = SepFunctions.isFedExEnabled(StoreID);
                var isUPSEnabled = SepFunctions.isUPSEnabled(StoreID);
                var isUSPSEnabled = SepFunctions.isUSPSEnabled(StoreID);
                var isCanadaPostEnabled = SepFunctions.isCanadaPostEnabled(StoreID);

                string UPSPassword;
                string UPSUserName;
                string UPSAccountNum;
                string USPSUserID;
                string UPSShipperNum;
                string shipperPostalCode;

                string FedExAccountNum;
                string FedExMeterNum;
                string FedExServiceKey;
                string FedExServicePass;
                if (StoreID > 0)
                {
                    var sXML = SepFunctions.Load_Store_Config(StoreID);
                    FedExAccountNum = SepFunctions.ParseXML("FedExAccountNum", sXML);
                    FedExMeterNum = SepFunctions.ParseXML("FedExMeterNum", sXML);
                    FedExServiceKey = SepFunctions.ParseXML("FedExServiceKey", sXML);
                    FedExServicePass = SepFunctions.Decrypt(SepFunctions.ParseXML("FedExServicePass", sXML));
                    UPSAccountNum = SepFunctions.ParseXML("UPSAccountNum", sXML);
                    UPSUserName = SepFunctions.ParseXML("UPSUserName", sXML);
                    UPSPassword = SepFunctions.Decrypt(SepFunctions.ParseXML("UPSPassword", sXML));
                    UPSShipperNum = SepFunctions.ParseXML("UPSShipperNum", sXML);
                    USPSUserID = SepFunctions.ParseXML("USPSUserID", sXML);

                    var storeUserID = SepCommon.DAL.ShoppingMall.Store_Get_UserID(StoreID);
                    if (isUSPSEnabled != true)
                    {
                        shipperStreet = SepFunctions.GetUserInformation("StreetAddress", storeUserID);
                        shipperCity = SepFunctions.GetUserInformation("City", storeUserID);
                        shipperState = SepFunctions.GetUserInformation("State", storeUserID);
                        shipperCountry = SepFunctions.GetUserInformation("Country", storeUserID);
                    }

                    shipperPostalCode = SepFunctions.GetUserInformation("ZipCode", storeUserID);
                }
                else
                {
                    FedExAccountNum = SepFunctions.Setup(989, "FedExAccountNum");
                    FedExMeterNum = SepFunctions.Setup(989, "FedExMeterNum");
                    FedExServiceKey = SepFunctions.Setup(989, "FedExServiceKey");
                    FedExServicePass = SepFunctions.Decrypt(SepFunctions.Setup(989, "FedExServicePass"));
                    UPSAccountNum = SepFunctions.Setup(989, "UPSAccountNum");
                    UPSUserName = SepFunctions.Setup(989, "UPSUserName");
                    UPSPassword = SepFunctions.Decrypt(SepFunctions.Setup(989, "UPSPassword"));
                    UPSShipperNum = SepFunctions.Setup(989, "UPSShipperNum");
                    USPSUserID = SepFunctions.Setup(989, "USPSUserID");

                    if (isUSPSEnabled != true)
                    {
                        shipperStreet = SepFunctions.Setup(991, "CompanyAddressLine1");
                        shipperCity = SepFunctions.Setup(991, "CompanyCity");
                        shipperState = SepFunctions.Setup(991, "CompanyState");
                        shipperCountry = SepFunctions.Setup(991, "CompanyCountry");
                    }

                    shipperPostalCode = SepFunctions.Setup(991, "CompanyZipCode");
                }

                if (isUSPSEnabled != true)
                {
                    destinationStreet = SepFunctions.GetUserInformation("StreetAddress");
                    destinationCity = SepFunctions.GetUserInformation("City");
                    destinationState = SepFunctions.GetUserInformation("State");
                    destinationCountry = SepFunctions.GetUserInformation("Country");
                }

                string destinationPostalCode = SepFunctions.GetUserInformation("ZipCode");

                if (SepFunctions.isShippingEnabled(StoreID) == false)
                {
                    rShipping.Visible = false;
                }
                else
                {
                    var dShipping = SepCommon.DAL.ShoppingMall.GetShopShippingMethods(StoreID: StoreID);
                    for (var i = 0; i <= dShipping.Count - 1; i++)
                        try
                        {
                            decimal dRate = 0;

                            var cFedEx = new FedEx(FedExAccountNum, FedExMeterNum, FedExServiceKey, FedExServicePass, shipperStreet, shipperCity, shipperState, shipperPostalCode, shipperCountry, destinationStreet, destinationCity, destinationState, destinationPostalCode, destinationCountry);
                            var cUPS = new UPS(UPSAccountNum, UPSUserName, UPSPassword, UPSShipperNum, dShipping[i].ShippingService);
                            var cUSPS = new USPS(USPSUserID);
                            var cCanadaPost = new CanadaPost();

                            for (var j = 0; j <= dInvoices.Count - 1; j++)
                            {
                                long totalProductWeight = 0;
                                if (SepFunctions.toLong(dInvoices[j].Weight) > 0)
                                {
                                    if (Strings.UCase(dInvoices[j].WeightType) == "LBS")
                                        totalProductWeight = SepFunctions.toLong(dInvoices[j].Weight);
                                    else
                                        totalProductWeight = SepFunctions.toLong(dInvoices[j].Weight) / 16;
                                    if (isFedExEnabled)
                                        if (dShipping[i].Carrier == "FedEx")
                                            dRate += cFedEx.GetRate(Strings.ToString(totalProductWeight), "LBS");
                                    if (isUPSEnabled)
                                        if (dShipping[i].Carrier == "UPS")
                                            dRate += cUPS.GetRate(Strings.ToString(totalProductWeight), "LBS", shipperPostalCode, shipperCountry, destinationPostalCode, destinationCountry, dShipping[i].ShippingService);
                                    if (isUSPSEnabled)
                                        for (var k = 1; k <= dInvoices[j].Quantity; k++)
                                        {
                                            packageCount += 1;
                                            cUSPS.AddItem(packageCount, Strings.ToString(totalProductWeight), "LBS", SepFunctions.Setup(991, "CompanyZipCode"), SepFunctions.GetUserInformation("ZipCode"), dShipping[i].ShippingService);
                                        }
                                    if (isCanadaPostEnabled)
                                        if (dShipping[i].Carrier == "CanadaPost")
                                            dRate += cCanadaPost.getRates();
                                }
                            }

                            // If dShipping.Item(i).Carrier = "FedEx" Then
                            // dRate = cFedEx.GetRate(cUSPS.GetShippingXML)
                            // End If
                            if (dShipping[i].Carrier == "USPS") dRate = cUSPS.GetRate(cUSPS.GetShippingXML());

                            if (SepFunctions.Format_Currency(dRate) != SepFunctions.Format_Currency("0"))
                            {
                                rShipping.Items.Add(new ListItem(dShipping[i].MethodName + " (" + SepFunctions.Format_Currency(dRate) + ")", dShipping[i].StoreID + "||" + dShipping[i].MethodID + "||" + SepFunctions.Format_Currency(dRate), true));
                                shipCount += 1;
                            }

                            cFedEx.Dispose();
                            cUPS.Dispose();
                            cUSPS.Dispose();
                        }
                        catch
                        {
                        }

                    rShipping.SelectedIndex = 0;
                    if (shipCount == 0) ManageGridView.Columns[1].Visible = false;
                }
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 995;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "viewcart_checkout.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Checkout", SepFunctions.Session_User_Name());

                NameOnCard.Value = SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName");

                for (var i = 1; i <= 12; i++) ExpireMonth.Items.Add(new ListItem(Strings.ToString(Strings.Len(Strings.ToString(i)) == 1 ? "0" : string.Empty) + i + " - " + DateAndTime.MonthName(i), Strings.ToString(i)));

                for (var i = 0; i <= 15; i++) ExpireYear.Items.Add(new ListItem(Strings.ToString(DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Year, i, DateTime.Now))), Strings.ToString(DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Year, i, DateTime.Now)))));

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    if (SepFunctions.Get_Portal_ID() > 0)
                        using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    var sXML = SepFunctions.openNull(RS["ScriptText"]);
                                    var sAuthorize = SepFunctions.ParseXML("AuthorizeNet", sXML);
                                    var sSquare = SepFunctions.ParseXML("Square", sXML);
                                    var sStripe = SepFunctions.ParseXML("Stripe", sXML);
                                    var sPayPal = SepFunctions.ParseXML("PayPal", sXML);
                                    var seSelect = SepFunctions.ParseXML("eSelect", sXML);
                                    var sCheck = SepFunctions.ParseXML("CheckPayment", sXML);
                                    var sMultiSafePay = SepFunctions.ParseXML("MultiSafePay", sXML);

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("Login", sAuthorize)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("TransactionKey", sAuthorize)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Credit Card"), "AuthorizeNet"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("APIToken", seSelect)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("StoreID", seSelect)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Credit Card"), "eSelect"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("EmailTo", sCheck)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("Address", sCheck)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Check / Money Order"), "Check"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("EmailAddress", sPayPal)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("PayPal"), "PayPal"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("AccessToken", sSquare)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("LocationId", sSquare)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Square"), "Square"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("PublishableKey", sStripe)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SecretKey", sStripe)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Stripe"), "Stripe"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("AccountID", sMultiSafePay)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SiteID", sMultiSafePay)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SiteCode", sMultiSafePay)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Credit Card"), "MultiSafePay"));
                                    }
                                }

                            }
                        }
                    else
                        using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    var sXML = SepFunctions.openNull(RS["ScriptText"]);
                                    var sAuthorize = SepFunctions.ParseXML("AuthorizeNet", sXML);
                                    var sSquare = SepFunctions.ParseXML("Square", sXML);
                                    var sStripe = SepFunctions.ParseXML("Stripe", sXML);
                                    var sPayPal = SepFunctions.ParseXML("PayPal", sXML);
                                    var seSelect = SepFunctions.ParseXML("eSelect", sXML);
                                    var sCheck = SepFunctions.ParseXML("CheckPayment", sXML);
                                    var sMultiSafePay = SepFunctions.ParseXML("MultiSafePay", sXML);

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("Login", sAuthorize)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("TransactionKey", sAuthorize)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Credit Card"), "AuthorizeNet"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("APIToken", seSelect)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("StoreID", seSelect)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Credit Card"), "eSelect"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("EmailTo", sCheck)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("Address", sCheck)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Check / Money Order"), "Check"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("EmailAddress", sPayPal)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("PayPal"), "PayPal"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("AccessToken", sSquare)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("LocationId", sSquare)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Square"), "Square"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("PublishableKey", sStripe)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SecretKey", sStripe)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Stripe"), "Stripe"));
                                    }

                                    if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("AccountID", sMultiSafePay)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SiteID", sMultiSafePay)) && !string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SiteCode", sMultiSafePay)))
                                    {
                                        PaymentMethod.Items.Add(new ListItem(SepFunctions.LangText("Credit Card"), "MultiSafePay"));
                                    }
                                }

                            }
                        }
                }

                if (PaymentMethod.Items.Count == 1)
                {
                    OrderDiv.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("The administrator has not configured any payment gateways.") + "</div>";
                }

                if (PaymentMethod.Items.Count == 2)
                {
                    PaymentMethod.SelectedIndex = 1;
                }

                CardTypeRow.Visible = false;
                CardNumRow.Visible = false;
                CardNameRow.Visible = false;
                ExpireDateRow.Visible = false;
                SecurityCodeRow.Visible = false;

                var dInvoicesStores = SepCommon.DAL.Invoices.GetInvoicesUniqueStores(SepFunctions.Session_User_ID(), SepFunctions.toLong(SepFunctions.Session_Invoice_ID()));
                ManageGridView.DataSource = dInvoicesStores;
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your shopping cart is empty.") + "</div>";
                    ManageGridView.Visible = false;
                }

                try
                {
                    using (var cCRM = new CRM())
                    {
                        cCRM.Create_User(CRM.WhenToWriteUser.NewOrder);
                    }
                }
                catch
                {
                }
            }

            TotalSetupPrice();

            // Process stripe payment
            if (!string.IsNullOrWhiteSpace(hdnToken.Value))
            {
                var sXML = string.Empty;
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    if (SepFunctions.Get_Portal_ID() > 0)
                        using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=@PortalID", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    sXML = SepFunctions.ParseXML("Stripe", SepFunctions.openNull(RS["ScriptText"]));
                                }

                            }
                        }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    sXML = SepFunctions.ParseXML("Stripe", SepFunctions.openNull(RS["ScriptText"]));
                                }

                            }
                        }
                    }
                }

                Stripe.StripeConfiguration.ApiKey = SepFunctions.ParseXML("SecretKey", sXML);

                var options = new Stripe.ChargeCreateOptions
                {
                    Amount = SepFunctions.toLong(Strings.Replace(Strings.FormatNumber(TotalSetup, 2), ".", string.Empty)),
                    Currency = "usd",
                    Description = SepFunctions.LangText("Order Number ~~" + SepFunctions.Session_Invoice_ID() + "~~"),
                    Source = hdnToken.Value,
                    Capture = true
                };

                var service = new Stripe.ChargeService();
                Charge charge = service.Create(options);

                if (charge.Captured == true)
                {
                    SepCommon.DAL.Invoices.Invoice_Mark_Paid(SepFunctions.Session_Invoice_ID());
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your credit card has been successfully processed") + "</div>";
                    OrderDiv.Visible = false;
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">There has been an error processing your payment.</div>";
                    return;
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
        /// Handles the Click event of the OrderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void OrderButton_Click(object sender, EventArgs e)
        {
            var str = new StringBuilder();
            var GetCustomerID = SepFunctions.GetIdentity();
            var updateCustomerId = true;
            var sRelayURL = SepFunctions.GetMasterDomain(false) + "payments.aspx";
            var PayPalURL = "https://www.paypal.com/cgi-bin/webscr";

            if (SepFunctions.DebugMode)
            {
                PayPalURL = "https://sandbox.paypal.com/cgi-bin/webscr";
            }

            var sXML = string.Empty;

            switch (RecurringCycle)
            {
                case "1y":
                    RecurringCycle = "12";
                    break;

                case "6m":
                    RecurringCycle = "6";
                    break;

                case "3m":
                    RecurringCycle = "3";
                    break;

                default:
                    RecurringCycle = "1";
                    break;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT CustomerId FROM Members WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["CustomerId"])))
                            {
                                GetCustomerID = SepFunctions.toLong(SepFunctions.openNull(RS["CustomerId"]));
                                updateCustomerId = false;
                            }
                        }

                    }
                }

                if (updateCustomerId)
                    using (var cmd = new SqlCommand("UPDATE Members SET CustomerId=@CustomerId WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", GetCustomerID);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.ExecuteNonQuery();
                    }

                using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sXML = SepFunctions.openNull(RS["ScriptText"]);
                        }

                    }
                }

                var sAuthorizeNet = SepFunctions.ParseXML("AuthorizeNet", sXML);
                var sPayPal = SepFunctions.ParseXML("PayPal", sXML);
                var sSquare = SepFunctions.ParseXML("Square", sXML);
                var seSelect = SepFunctions.ParseXML("eSelect", sXML);

                var GetPayPalEmail = SepFunctions.ParseXML("EmailAddress", sPayPal);
                var GeteSelectAPIToken = SepFunctions.ParseXML("APIToken", seSelect);
                var GeteSelectStoreID = SepFunctions.ParseXML("StoreID", seSelect);

                var GetInvoiceID = SepFunctions.Session_Invoice_ID();

                switch (PaymentMethod.SelectedValue)
                {
                    case "AuthorizeNet":
                        var cAuthorizeNet = new AuthorizeNet();
                        var AuthorizeProfileID = string.Empty;
                        var AuthorizePaymentID = string.Empty;

                        cAuthorizeNet.gstrLoginName = SepFunctions.ParseXML("Login", sAuthorizeNet);
                        cAuthorizeNet.gstrTransactionKey = SepFunctions.ParseXML("TransactionKey", sAuthorizeNet);

                        if (cAuthorizeNet.Create_Profile(Strings.ToString(GetCustomerID), SepFunctions.GetUserInformation("UserName"), SepFunctions.GetUserInformation("EmailAddress"), SepFunctions.GetUserInformation("FirstName"), SepFunctions.GetUserInformation("LastName"), SepFunctions.GetUserInformation("StreetAddress"), SepFunctions.GetUserInformation("City"), SepCommon.SepCore.Request.Item("State"), SepFunctions.GetUserInformation("ZipCode"), SepFunctions.GetUserInformation("Country"), SepFunctions.GetUserInformation("PhoneNumber"), CardNumber.Value, ExpireMonth.Value, ExpireYear.Value, SecurityCode.Value, ref AuthorizeProfileID, ref AuthorizePaymentID) == false)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">There has been an error creating your payment profile.</div>";
                            return;
                        }

                        cAuthorizeNet.Create_Payment(AuthorizeProfileID, AuthorizePaymentID, SepFunctions.Session_User_Name(), SepFunctions.Session_Invoice_ID(), "Invoice #" + SepFunctions.Session_Invoice_ID(), Strings.FormatNumber(TotalSetup, 2));

                        SepCommon.DAL.Invoices.Invoice_Mark_Paid(SepFunctions.Session_Invoice_ID());

                        ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your credit card has been successfully processed") + "</div>";
                        OrderDiv.Visible = false;
                        break;

                    case "eSelect":
                        var strExpDate = string.Empty;

                        var inidata = string.Empty;

                        var sCardNumber = SepCommon.SepCore.Request.Item("x_card_num");
                        var sHost = "www3.moneris.com";

                        if (SepFunctions.Get_Portal_ID() == 0)
                        {
                            using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        sXML = SepFunctions.openNull(RS["ScriptText"]);
                                    }

                                }
                            }

                            seSelect = SepFunctions.ParseXML("eSelect", sXML);
                            GeteSelectAPIToken = SepFunctions.ParseXML("APIToken", seSelect);
                            GeteSelectStoreID = SepFunctions.ParseXML("StoreID", seSelect);
                        }
                        else
                        {
                            using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=@PortalID", conn))
                            {
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        inidata = SepFunctions.openNull(RS["ScriptText"]);
                                        GeteSelectAPIToken = SepFunctions.ParseXML("eSelectAPIToken", inidata);
                                        GeteSelectStoreID = SepFunctions.ParseXML("eSelectStoreID", inidata);
                                    }

                                }
                            }
                        }

                        strExpDate = SepCommon.SepCore.Request.Item("exp_year") + SepCommon.SepCore.Request.Item("exp_month");

                        var mpgReq = new HttpsPostRequest(sHost, GeteSelectStoreID, GeteSelectAPIToken, new Purchase(SepCommon.SepCore.Request.Item("InvoiceID"), Strings.FormatNumber(TotalSetup, 2), sCardNumber, strExpDate, "7"), true);

                        if (mpgReq.GetReceipt().GetResponseCode() != "null")
                        {
                            if (SepFunctions.toLong(mpgReq.GetReceipt().GetResponseCode()) < 50)
                            {
                                str.Append("<p>" + SepFunctions.LangText("Your transaction has been successfully processsed.") + "</p>");
                            }
                            else
                            {
                                str.Append("<p>" + SepFunctions.LangText("There has been an error processing your credit card.") + "</p>");
                                str.Append("Response Code: " + mpgReq.GetReceipt().GetResponseCode() + "<br/>");
                                str.Append("Response Message: " + mpgReq.GetReceipt().GetMessage() + "<br/>");
                            }
                        }
                        else
                        {
                            str.Append("<p>" + SepFunctions.LangText("There has been an error processing your credit card.") + "</p>");
                            str.Append("<p>" + mpgReq.GetReceipt().GetMessage() + "</p>");
                        }

                        OrderDiv.Visible = false;
                        ErrorMessage.InnerHtml = Strings.ToString(str);

                        var sActText = string.Empty;

                        sActText += "CardType = " + mpgReq.GetReceipt().GetCardType() + System.Environment.NewLine;
                        sActText += "TransAmount = " + mpgReq.GetReceipt().GetTransAmount() + System.Environment.NewLine;
                        sActText += "TxnNumber = " + mpgReq.GetReceipt().GetTxnNumber() + System.Environment.NewLine;
                        sActText += "ReceiptId = " + mpgReq.GetReceipt().GetReceiptId() + System.Environment.NewLine;
                        sActText += "TransType = " + mpgReq.GetReceipt().GetTransType() + System.Environment.NewLine;
                        sActText += "ReferenceNum = " + mpgReq.GetReceipt().GetReferenceNum() + System.Environment.NewLine;
                        sActText += "ResponseCode = " + mpgReq.GetReceipt().GetResponseCode() + System.Environment.NewLine;
                        sActText += "ISO = " + mpgReq.GetReceipt().GetISO() + System.Environment.NewLine;
                        sActText += "BankTotals = " + mpgReq.GetReceipt().GetBankTotals() + System.Environment.NewLine;
                        sActText += "Message = " + mpgReq.GetReceipt().GetMessage() + System.Environment.NewLine;
                        sActText += "AuthCode = " + mpgReq.GetReceipt().GetAuthCode() + System.Environment.NewLine;
                        sActText += "Complete = " + mpgReq.GetReceipt().GetComplete() + System.Environment.NewLine;
                        sActText += "TransDate = " + mpgReq.GetReceipt().GetTransDate() + System.Environment.NewLine;
                        sActText += "TransTime = " + mpgReq.GetReceipt().GetTransTime() + System.Environment.NewLine;
                        sActText += "Ticket = " + mpgReq.GetReceipt().GetTicket() + System.Environment.NewLine;
                        sActText += "TimedOut = " + mpgReq.GetReceipt().GetTimedOut() + System.Environment.NewLine;

                        SepFunctions.Activity_Write("PAYMENT", sActText, 995, string.Empty, SepFunctions.Session_User_ID());
                        break;

                    case "Check":
                        var GetEmailSubject = string.Empty;
                        var GetEmailBody = string.Empty;

                        var sText = string.Empty;
                        var sEmailTo = string.Empty;
                        var sAddress = string.Empty;

                        if (SepFunctions.Get_Portal_ID() > 0)
                        {
                            using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=@PortalID", conn))
                            {
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        sXML = SepFunctions.openNull(RS["ScriptText"]);
                                    }

                                }
                            }

                            sText = SepFunctions.HTMLDecode(SepFunctions.ParseXML("CheckInstructions", SepFunctions.ParseXML("PaymentGateway", sXML)));
                            sEmailTo = SepFunctions.ParseXML("CheckEmailTo", SepFunctions.ParseXML("PaymentGateway", sXML));
                            sAddress = SepFunctions.ParseXML("CheckAddress", SepFunctions.ParseXML("PaymentGateway", sXML));
                        }
                        else
                        {
                            using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        sXML = SepFunctions.openNull(RS["ScriptText"]);
                                    }

                                }
                            }

                            sText = SepFunctions.HTMLDecode(SepFunctions.ParseXML("Instructions", SepFunctions.ParseXML("CheckPayment", sXML)));
                            sEmailTo = SepFunctions.ParseXML("EmailTo", SepFunctions.ParseXML("CheckPayment", sXML));
                            sAddress = SepFunctions.ParseXML("Address", SepFunctions.ParseXML("CheckPayment", sXML));
                        }

                        if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(995, "EmailTempAdmin")))
                            using (var cmd = new SqlCommand("SELECT EmailSubject,EmailBody FROM EmailTemplates WHERE TemplateID=@TemplateID", conn))
                            {
                                cmd.Parameters.AddWithValue("@TemplateID", SepFunctions.Setup(995, "EmailTempAdmin"));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        GetEmailSubject = SepFunctions.openNull(RS["EmailSubject"]);
                                        GetEmailBody = SepFunctions.Replace_Fields(SepFunctions.openNull(RS["EmailBody"]), SepFunctions.Session_User_ID());
                                        SepFunctions.Send_Email(sEmailTo, SepFunctions.Setup(991, "AdminEmailAddress"), GetEmailSubject, GetEmailBody, 995);
                                    }

                                }
                            }

                        if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(995, "EmailTempCust")))
                            using (var cmd = new SqlCommand("SELECT EmailSubject,EmailBody FROM EmailTemplates WHERE TemplateID=@TemplateID", conn))
                            {
                                cmd.Parameters.AddWithValue("@TemplateID", SepFunctions.Setup(995, "EmailTempCust"));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        GetEmailSubject = SepFunctions.openNull(RS["EmailSubject"]);
                                        GetEmailBody = SepFunctions.Replace_Fields(SepFunctions.openNull(RS["EmailBody"]), SepFunctions.Session_User_ID());
                                        SepFunctions.Send_Email(SepFunctions.GetUserInformation("EmailAddress"), sEmailTo, GetEmailSubject, GetEmailBody, 995);
                                    }

                                }
                            }

                        str.Append(sText + "<p><b>" + SepFunctions.LangText("Please Send a Check To:") + "</b><br/>" + Strings.Replace(sAddress, System.Environment.NewLine, "<br/>") + "</p>");
                        str.Append("<p>" + SepFunctions.LangText("You MUST include the order number \"~~" + SepFunctions.HTMLEncode(SepFunctions.Session_Invoice_ID()) + "~~\" on your check / money order.") + "</p>");

                        OrderDiv.Visible = false;
                        ErrorMessage.InnerHtml = Strings.ToString(str);

                        break;

                    case "Square":
                        try
                        {
                            // Build base client
                            var client = new SquareClient.Builder()
                                .Environment(Square.Environment.Production)
                                .AccessToken(SepFunctions.ParseXML("AccessToken", sSquare))
                                .Build();

                            ICheckoutApi PaymentsApi = client.CheckoutApi;

                            SepCommon.SepCore.Session.setSession("OrderGUID", SepFunctions.Generate_GUID());

                            var bodyOrderLineItems = new List<OrderLineItem>();

                            var bodyOrderLineItems1BasePriceMoney = new Money.Builder()
                                .Amount(SepFunctions.toLong(Strings.Replace(Strings.FormatNumber(TotalSetup, 2), ".", string.Empty)))
                                .Currency("USD")
                                .Build();

                            var bodyOrderLineItems1 = new OrderLineItem.Builder(
                                    "1")
                                .Name(SepFunctions.LangText("Order Number ~~" + GetInvoiceID + "~~"))
                                .BasePriceMoney(bodyOrderLineItems1BasePriceMoney)
                                .Build();
                            bodyOrderLineItems.Add(bodyOrderLineItems1);

                            var bodyOrder = new Square.Models.Order.Builder(SepFunctions.ParseXML("LocationId", sSquare))
                            .LineItems(bodyOrderLineItems)
                            .Build();

                            var bodyOrder2 = new CreateOrderRequest(bodyOrder, SepCommon.SepCore.Session.getSession("OrderGUID"));

                            var body = new CreateCheckoutRequest(SepCommon.SepCore.Session.getSession("OrderGUID"), bodyOrder2, true, SepFunctions.Setup(991, "AdminEmailAddress"), SepFunctions.GetUserInformation("EmailAddress"), redirectUrl: SepFunctions.GetMasterDomain(true) + "sq_checkout.aspx?OrderGUID=" + SepCommon.SepCore.Session.getSession("OrderGUID"));

                            CreateCheckoutResponse SquareResponse = PaymentsApi.CreateCheckout(SepFunctions.ParseXML("LocationId", sSquare), body);
                            SepFunctions.Redirect(SquareResponse.Checkout.CheckoutPageUrl);
                        }
                        catch (ApiException ex)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Square Error. (~~" + ex.Message + "~~)") + "</div>";
                        }
                        break;

                    case "PayPal":
                        if (!string.IsNullOrWhiteSpace(RecurringCycle) && RecurringPrice > 0)
                        {
                            // -V3063
                            str.Append("<script type=\"text/javascript\">");
                            str.Append("var PaymentForm = document.createElement('form');");
                            str.Append("PaymentForm.setAttribute('method', 'post');");
                            str.Append("PaymentForm.setAttribute('name', 'frmPayment');");
                            str.Append("PaymentForm.setAttribute('action', '" + PayPalURL + "');");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'cmd');");
                            str.Append("hiddenInput.setAttribute('value', '_xclick-subscriptions');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'business');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(GetPayPalEmail) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'item_name');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Order Number ~~" + GetInvoiceID + "~~")) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'item_number');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(GetCustomerID)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'invoice');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(GetInvoiceID) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'no_note');");
                            str.Append("hiddenInput.setAttribute('value', '1');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'currency_code');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.Setup(992, "CurrencyCode")) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'bn');");
                            str.Append("hiddenInput.setAttribute('value', 'PP-SubscriptionsBF');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'a1');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(TotalSetup)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'p1');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(RecurringCycle) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 't1');");
                            str.Append("hiddenInput.setAttribute('value', 'M');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'a3');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(RecurringPrice)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'p3');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(RecurringCycle) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 't3');");
                            str.Append("hiddenInput.setAttribute('value', 'M');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'src');");
                            str.Append("hiddenInput.setAttribute('value', '1');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'sra');");
                            str.Append("hiddenInput.setAttribute('value', '1');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'notify_url');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(sRelayURL) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'return');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.GetMasterDomain(true)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("document.body.appendChild(PaymentForm);");
                            str.Append("PaymentForm.submit();");
                            str.Append("document.body.removeChild(PaymentForm);");
                            str.Append("</script>");
                        }
                        else
                        {
                            str.Append("<script type=\"text/javascript\">");
                            str.Append("var PaymentForm = document.createElement('form');");
                            str.Append("PaymentForm.setAttribute('method', 'post');");
                            str.Append("PaymentForm.setAttribute('name', 'frmPayment');");
                            str.Append("PaymentForm.setAttribute('action', '" + PayPalURL + "');");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'cmd');");
                            str.Append("hiddenInput.setAttribute('value', '_xclick');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'business');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(GetPayPalEmail) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'item_name');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Order Number ~~" + GetInvoiceID + "~~")) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'item_number');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(GetCustomerID)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'invoice');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(GetInvoiceID) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'amount');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(TotalSetup)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'no_note');");
                            str.Append("hiddenInput.setAttribute('value', '1');");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'currency_code');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.Setup(992, "CurrencyCode")) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'notify_url');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(sRelayURL) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'return');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.GetMasterDomain(true)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("document.body.appendChild(PaymentForm);");
                            str.Append("PaymentForm.submit();");
                            str.Append("document.body.removeChild(PaymentForm);");
                            str.Append("</script>");
                        }

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", Strings.ToString(str));
                        break;

                    case "MultiSafePay":
                        var MSP = new MultiSafepay();

                        MSP.Customer.Locale = "NL_nl";
                        MSP.Customer.Firstname = SepFunctions.GetUserInformation("FirstName");
                        MSP.Customer.Lastname = SepFunctions.GetUserInformation("LastName");
                        MSP.Customer.Address1 = SepFunctions.GetUserInformation("StreetAddress");
                        MSP.Customer.Address2 = string.Empty;
                        MSP.Customer.Housenumber = string.Empty;
                        MSP.Customer.Zipcode = SepFunctions.GetUserInformation("ZipCode");
                        MSP.Customer.City = SepFunctions.GetUserInformation("City");
                        MSP.Customer.State = SepFunctions.GetUserInformation("State");
                        MSP.Customer.Country = SepFunctions.GetUserInformation("Country");
                        MSP.Customer.Phone = SepFunctions.GetUserInformation("PhoneNumber");
                        MSP.Customer.Email = SepFunctions.GetUserInformation("EmailAddress");

                        MSP.Transaction.Currency = "EUR";

                        // always set to euro
                        MSP.Transaction.ID = GetInvoiceID;
                        MSP.Transaction.Amount = TotalSetup.ToString();
                        MSP.Transaction.Description = "Order #" + GetInvoiceID;
                        MSP.Transaction.Items = "Order #" + GetInvoiceID;

                        var paymentUrl = MSP.StartTransaction();
                        if (!string.IsNullOrWhiteSpace(paymentUrl))
                        {
                            MSP.Merchant.TestAccount = false;
                            str.Append("<script type=\"text/javascript\">");
                            str.Append("var PaymentForm = document.createElement('form');");
                            str.Append("PaymentForm.setAttribute('method', 'post');");
                            str.Append("PaymentForm.setAttribute('name', 'frmPayment');");
                            str.Append("PaymentForm.setAttribute('action', '" + paymentUrl + "');");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'testAccount');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(MSP.Merchant.TestAccount)) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'accountId');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(MSP.Merchant.AccountId) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'siteId');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(MSP.Merchant.SiteId) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'siteCode');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(MSP.Merchant.SiteCode) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'notificationUrl');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(MSP.Merchant.NotificationUrl) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'returnUrl');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(MSP.Merchant.ReturnUrl) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'cancelUrl');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(MSP.Merchant.CancelUrl) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'transactionid');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(GetInvoiceID) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("var hiddenInput = document.createElement('input');");
                            str.Append("hiddenInput.setAttribute('type', 'hidden');");
                            str.Append("hiddenInput.setAttribute('name', 'description');");
                            str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes("Order #" + GetInvoiceID) + "'));");
                            str.Append("PaymentForm.appendChild(hiddenInput);");

                            str.Append("document.body.appendChild(PaymentForm);");
                            str.Append("PaymentForm.submit();");
                            str.Append("document.body.removeChild(PaymentForm);");
                            str.Append("</script>");
                        }
                        else
                        {
                            str.Append("<script type=\"text/javascript\">alert('Error " + MSP.ErrorCode + ": " + MSP.ErrorDescription + "')</script>");
                        }

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", Strings.ToString(str));
                        MSP.Dispose();
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the PaymentMethod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PaymentMethod.SelectedValue) || PaymentMethod.SelectedValue == "Check" || PaymentMethod.SelectedValue == "PayPal" || PaymentMethod.SelectedValue == "Square")
            {
                CardTypeRow.Visible = false;
                CardNumRow.Visible = false;
                CardNameRow.Visible = false;
                ExpireDateRow.Visible = false;
                SecurityCodeRow.Visible = false;
            }
            else
            {
                CardTypeRow.Visible = true;
                CardNumRow.Visible = true;
                CardNameRow.Visible = true;
                ExpireDateRow.Visible = true;
                SecurityCodeRow.Visible = true;
            }

            if (PaymentMethod.SelectedValue == "Stripe")
            {
                var stripeName = "stripeJS";
                var stripeUrl = "https://js.stripe.com/v2/";
                var stripeType = GetType();

                var TinyMCEcs = Page.ClientScript;

                if (!TinyMCEcs.IsClientScriptIncludeRegistered(stripeType, stripeName)) TinyMCEcs.RegisterClientScriptInclude(stripeType, stripeName, ResolveClientUrl(stripeUrl));

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    string stripeScript = "\n<script type=\"text/javascript\">\n";
                    stripeScript += "$('document').ready(function() {\n";
                    if (SepFunctions.Get_Portal_ID() > 0)
                    {
                        using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    stripeScript += "	Stripe.setPublishableKey('" + SepFunctions.ParseXML("PublishableKey", SepFunctions.ParseXML("Stripe", SepFunctions.openNull(RS["ScriptText"]))) + "');\n";
                                }

                            }
                        }
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    stripeScript += "	Stripe.setPublishableKey('" + SepFunctions.ParseXML("PublishableKey", SepFunctions.ParseXML("Stripe", SepFunctions.openNull(RS["ScriptText"]))) + "');\n";
                                }

                            }
                        }
                    }

                    stripeScript += "	$('#OrderButton').on('click', function(e) {\n";
                    stripeScript += "		e.preventDefault();\n";
                    stripeScript += "		e.stopPropagation();\n";
                    stripeScript += "		Stripe.card.createToken({\n";
                    stripeScript += "			number: $('#CardNumber').val(),\n";
                    stripeScript += "			cvc: $('#SecurityCode').val(),\n";
                    stripeScript += "			exp_month: $('#ExpireMonth').val(),\n";
                    stripeScript += "			exp_year: $('#ExpireYear').val()\n";
                    stripeScript += "		}, stripeResponseHandler);\n";
                    stripeScript += "	});\n";
                    stripeScript += "	function stripeResponseHandler(status, response) {\n";
                    stripeScript += "		var $form = $('#aspnetForm');\n";
                    stripeScript += "		if (response.error) {\n";
                    stripeScript += "			alert(response.error.message);\n";
                    stripeScript += "		} else {\n";
                    stripeScript += "			var token = response.id;\n";
                    stripeScript += "			$('#hdnToken').val(token);\n";
                    stripeScript += "			$form.get(0).submit();\n";
                    stripeScript += "		}\n";
                    stripeScript += "	}\n";
                    stripeScript += "});\n";
                    stripeScript += "\n\n </script>";
                    Page.ClientScript.RegisterStartupScript(GetType(), "stripeKey", stripeScript, false);
                }
            }
        }

        void TotalSetupPrice()
        {
            var dInvoices = SepCommon.DAL.Invoices.GetInvoices(UserID: SepFunctions.Session_User_ID(), InvoiceID: SepFunctions.toLong(SepFunctions.Session_Invoice_ID()));

            TotalSetup = SepFunctions.toDecimal(dInvoices[0].TotalUnitPrice);
            RecurringPrice = SepFunctions.toDecimal(dInvoices[0].TotalRecurring);
            RecurringCycle = dInvoices[0].RecurringCycle;

            decimal shippingCost = 0;

            if (ManageGridView.Columns.Count > 1)
                for (var i = 0; i <= ManageGridView.Rows.Count - 1; i++)
                {
                    try
                    {
                        var rShipping = (RadioButtonList)ManageGridView.Rows[i].FindControl("rbMethods");
                        long StoreID = 0;
                        if (!string.IsNullOrWhiteSpace(rShipping.SelectedValue))
                        {
                            StoreID = SepFunctions.toLong(Strings.Split(rShipping.SelectedValue, "||")[0]);
                        }
                        if (SepFunctions.isShippingEnabled(StoreID))
                        {
                            long MethodID = 0;
                            if (Information.UBound(Strings.Split(rShipping.SelectedValue, "||")) > 0)
                            {
                                MethodID = SepFunctions.toLong(Strings.Split(rShipping.SelectedValue, "||")[1]);
                                shippingCost += SepFunctions.toDecimal(Strings.Split(rShipping.SelectedValue, "||")[2]);
                            }

                            SepCommon.DAL.Invoices.Invoice_Add_Shipping_Method(StoreID, MethodID);
                        }
                    }
                    catch
                    {
                    }
                }

            if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("State")))
            {
                Taxes dTaxes = SepCommon.DAL.ShoppingCart.Tax_Get_By_State(SepFunctions.GetUserInformation("State"));
                if (!string.IsNullOrWhiteSpace(dTaxes.TaxPercent))
                {
                    if (SepFunctions.toDecimal(dTaxes.TaxPercent) > 0)
                    {
                        totalTaxes = TotalSetup * (SepFunctions.toDecimal(dTaxes.TaxPercent) / 100);
                        TotalSetup += totalTaxes + shippingCost;
                        RecurringPrice += (SepFunctions.toDecimal(dTaxes.TaxPercent) / 100);
                        RecurringPrice += totalTaxes;
                    }
                }
            }
        }
    }
}