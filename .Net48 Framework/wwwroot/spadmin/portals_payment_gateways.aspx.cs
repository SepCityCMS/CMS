// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_payment_gateways.aspx.cs" company="SepCity, Inc.">
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
    /// Class portals_payment_gateways.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_payment_gateways : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Payment Gateways");
                    PayPalEmailLabel.InnerText = SepFunctions.LangText("PayPal Email Address:");
                    AuthorizeNetLoginLabel.InnerText = SepFunctions.LangText("Authorize.Net Login:");
                    AuthorizeNetTransKeyLabel.InnerText = SepFunctions.LangText("Authorize.Net Transaction Key:");
                    eSelectAPILabel.InnerText = SepFunctions.LangText("eSelect Plus API Token:");
                    eSelectStoreIDLabel.InnerText = SepFunctions.LangText("eSelect Plus Store ID:");
                    ChecksEmailLabel.InnerText = SepFunctions.LangText("Check/Money Order Email Payments To:");
                    ChecksAddressLabel.InnerText = SepFunctions.LangText("Check/Money Order Mail to Address:");
                    ChecksInstructionsLabel.InnerText = SepFunctions.LangText("Check/Money Order Payment Instructions:");
                    SSLServerURLLabel.InnerText = SepFunctions.LangText("SSL Server URL (ex. https://www.google.com):");
                    SSLServerHeaderLabel.InnerText = SepFunctions.LangText("SSL Server Header:");
                    SSLServerFooterLabel.InnerText = SepFunctions.LangText("SSL Server Footer:");
                    MultiSafeAccountIDLabel.InnerText = SepFunctions.LangText("MultiSafepay Account ID:");
                    MultiSafeSiteIDLabel.InnerText = SepFunctions.LangText("MultiSafepay Site ID:");
                    MultiSafeSiteCodeLabel.InnerText = SepFunctions.LangText("MultiSafepay Site Code:");
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

            if (!Page.IsPostBack)
            {
                var sXML = string.Empty;

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT ScriptText FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty, conn))
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
                }

                PayPalEmail.Value = SepFunctions.ParseXML("EmailAddress", SepFunctions.ParseXML("PayPal", sXML));

                SquareAccessToken.Value = SepFunctions.ParseXML("AccessToken", SepFunctions.ParseXML("Square", sXML));
                SquareLocationId.Value = SepFunctions.ParseXML("LocationId", SepFunctions.ParseXML("Square", sXML));

                StripePublishableKey.Value = SepFunctions.ParseXML("PublishableKey", SepFunctions.ParseXML("Stripe", sXML));
                StripeSecretKey.Value = SepFunctions.ParseXML("SecretKey", SepFunctions.ParseXML("Stripe", sXML));

                AuthorizeNetLogin.Value = SepFunctions.ParseXML("Login", SepFunctions.ParseXML("AuthorizeNet", sXML));
                AuthorizeNetTransKey.Value = SepFunctions.ParseXML("TransactionKey", SepFunctions.ParseXML("AuthorizeNet", sXML));

                eSelectAPI.Value = SepFunctions.ParseXML("APIToken", SepFunctions.ParseXML("eSelect", sXML));
                eSelectStoreID.Value = SepFunctions.ParseXML("StoreID", SepFunctions.ParseXML("eSelect", sXML));

                ChecksEmail.Value = SepFunctions.ParseXML("EmailTo", SepFunctions.ParseXML("CheckPayment", sXML));
                ChecksAddress.Value = SepFunctions.ParseXML("Address", SepFunctions.ParseXML("CheckPayment", sXML));
                ChecksInstructions.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("Instructions", SepFunctions.ParseXML("CheckPayment", sXML)));

                SSLServerURL.Value = SepFunctions.ParseXML("SSLServer", SepFunctions.ParseXML("SSL", sXML));
                SSLServerHeader.Value = SepFunctions.ParseXML("SSLHeader", SepFunctions.ParseXML("SSL", sXML));
                SSLServerFooter.Value = SepFunctions.ParseXML("SSLFooter", SepFunctions.ParseXML("SSL", sXML));

                MultiSafeAccountID.Value = SepFunctions.ParseXML("AccountID", SepFunctions.ParseXML("MultiSafePay", sXML));
                MultiSafeSiteID.Value = SepFunctions.ParseXML("SiteID", SepFunctions.ParseXML("MultiSafePay", sXML));
                MultiSafeSiteCode.Value = SepFunctions.ParseXML("SiteCode", SepFunctions.ParseXML("MultiSafePay", sXML));
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var bUpdate = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ID FROM PortalScripts WHERE ScriptType='PayGateways' AND PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows) bUpdate = true;
                    }
                }

                string sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                sXML += "<root>" + Environment.NewLine;
                sXML += "<AuthorizeNet>" + Environment.NewLine;
                sXML += "<Login>" + AuthorizeNetLogin.Value + "</Login>" + Environment.NewLine;
                sXML += "<TransactionKey>" + AuthorizeNetTransKey.Value + "</TransactionKey>" + Environment.NewLine;
                sXML += "</AuthorizeNet>" + Environment.NewLine;
                sXML += "<PayPal>" + Environment.NewLine;
                sXML += "<EmailAddress>" + PayPalEmail.Value + "</EmailAddress>" + Environment.NewLine;
                sXML += "</PayPal>" + Environment.NewLine;
                sXML += "<Square>" + Environment.NewLine;
                sXML += "<AccessToken>" + SquareAccessToken.Value + "</AccessToken>" + Environment.NewLine;
                sXML += "<LocationId>" + SquareLocationId.Value + "</LocationId>" + Environment.NewLine;
                sXML += "</Square>" + Environment.NewLine;
                sXML += "<Stripe>" + Environment.NewLine;
                sXML += "<PublishableKey>" + StripePublishableKey.Value + "</PublishableKey>" + Environment.NewLine;
                sXML += "<SecretKey>" + StripeSecretKey.Value + "</SecretKey>" + Environment.NewLine;
                sXML += "</Stripe>" + Environment.NewLine;
                sXML += "<eSelect>" + Environment.NewLine;
                sXML += "<APIToken>" + eSelectAPI.Value + "</APIToken>" + Environment.NewLine;
                sXML += "<StoreID>" + eSelectStoreID.Value + "</StoreID>" + Environment.NewLine;
                sXML += "</eSelect>" + Environment.NewLine;
                sXML += "<CheckPayment>" + Environment.NewLine;
                sXML += "<EmailTo>" + ChecksEmail.Value + "</EmailTo>" + Environment.NewLine;
                sXML += "<Address>" + ChecksAddress.Value + "</Address>" + Environment.NewLine;
                sXML += "<Instructions>" + ChecksInstructions.Value + "</Instructions>" + Environment.NewLine;
                sXML += "</CheckPayment>" + Environment.NewLine;
                sXML += "<SSL>" + Environment.NewLine;
                sXML += "<SSLServer>" + SSLServerURL.Value + "</SSLServer>" + Environment.NewLine;
                sXML += "<SSLHeader>" + SSLServerHeader.Value + "</SSLHeader>" + Environment.NewLine;
                sXML += "<SSLFooter>" + SSLServerFooter.Value + "</SSLFooter>" + Environment.NewLine;
                sXML += "</SSL>" + Environment.NewLine;
                sXML += "<MultiSafePay>" + Environment.NewLine;
                sXML += "<AccountID>" + MultiSafeAccountID.Value + "</AccountID>" + Environment.NewLine;
                sXML += "<SiteID>" + MultiSafeSiteID.Value + "</SiteID>" + Environment.NewLine;
                sXML += "<SiteCode>" + MultiSafeSiteCode.Value + "</SiteCode>" + Environment.NewLine;
                sXML += "</MultiSafePay>" + Environment.NewLine;
                sXML += "</root>" + Environment.NewLine;

                var SqlStr = string.Empty;
                if (bUpdate)
                    SqlStr = "UPDATE PortalScripts SET ScriptText=@ScriptText WHERE ScriptType='PayGateways' AND PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty;
                else
                    SqlStr = "INSERT INTO PortalScripts (ScriptType, ScriptName, ScriptText, PortalID) VALUES ('PayGateways', 'Payment Gateways', @ScriptText, '" + SepFunctions.Get_Portal_ID() + "')";
                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ScriptText", sXML);
                    cmd.ExecuteNonQuery();
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Payment Gateways have been successfully saved.") + "</div>";
        }
    }
}