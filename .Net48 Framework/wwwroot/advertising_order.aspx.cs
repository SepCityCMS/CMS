// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="advertising_order.aspx.cs" company="SepCity, Inc.">
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
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class advertising_order.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class advertising_order : Page
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
        /// Saves the advertisement.
        /// </summary>
        /// <param name="AdID">The ad identifier.</param>
        public void Save_Advertisement(long AdID)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            string sReturn;

            var imageType = string.Empty;
            var imageData = string.Empty;

            if (AdImage.PostedFile == null || string.IsNullOrWhiteSpace(AdImage.PostedFile.FileName))
            {
            }
            else
            {
                var imageBytes = new byte[Convert.ToInt32(AdImage.PostedFile.InputStream.Length) + 1];
                AdImage.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                imageType = AdImage.PostedFile.ContentType;
                imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));
            }

            sReturn = SepCommon.DAL.Advertisements.Advertisement_Save(AdID, SepFunctions.Session_User_Name(), WebSiteURL.Value, WebSiteURL.Value, SepFunctions.toLong(Zone.Text), DateTime.Now, DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 12, DateTime.Now), string.Empty, MaxClicks.Value, MaxExposures.Value, string.Empty, string.Empty, Categories.Text, Pages.Text, SepFunctions.GetUserInformation("Country"), SepFunctions.GetUserInformation("State"), Portals.Text, imageData, imageType, 0);

            if (sReturn != SepFunctions.LangText("Advertisement has been successfully added."))
            {
                if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
                else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
            }
            else
            {
                SepFunctions.Redirect(sInstallFolder + "viewcart.aspx?ContinueURL=advertising.aspx");
            }
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Order Advertisement Space");
                    WebSiteURLLabel.InnerText = SepFunctions.LangText("Website URL to send users to when clicking on your image:");
                    AdImageLabel.InnerText = SepFunctions.LangText("Image of your ad (Only accept png, gif, and jpg images):");
                    ZoneLabel.InnerText = SepFunctions.LangText("Select a zone to target the advertisement to:");
                    CategoriesLabel.InnerText = SepFunctions.LangText("Target Ad to Categories:");
                    PagesLabel.InnerText = SepFunctions.LangText("Target Ad to Pages:");
                    PortalsLabel.InnerText = SepFunctions.LangText("Target Ad to Portals:");
                    WebSiteURLRequired.ErrorMessage = SepFunctions.LangText("~~WebSite URL~~ is required.");
                    AdImageRequired.ErrorMessage = SepFunctions.LangText("~~Image of your ad~~ is required.");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CartButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CartButton_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(WebSiteURL.Value, "^http\\://[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(/\\S*)?$") == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must enter a valid Website URL.") + "</div>";
                return;
            }

            var AdID = PlanID.Value;

            SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, GlobalVars.ModuleID, AdID, "1", string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, SepFunctions.Get_Portal_ID());

            // Save Advertisement
            Save_Advertisement(SepFunctions.toLong(AdID));
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 2;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "AdsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("AdsAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "advertising_order.aspx?PlanID=" + SepCommon.SepCore.Request.Item("PlanID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PlanID")))
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select a advertisement plan before entering this page.") + "</div>";
                    AdOrderForm.Visible = false;
                    return;
                }

                PlanID.Value = SepCommon.SepCore.Request.Item("PlanID");

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT ShortDesc FROM ShopProducts WHERE ModuleID='2' AND Status=1 AND ProductID=@PlanID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlanID", SepCommon.SepCore.Request.Item("PlanID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                string sShortDesc = SepFunctions.openNull(RS["ShortDesc"]);

                                if (SepFunctions.toLong(SepFunctions.ParseXML("MAXCLICKS", sShortDesc)) > 0) MaxClicks.Value = Strings.ToString(SepFunctions.toLong(SepFunctions.ParseXML("MAXCLICKS", sShortDesc)));
                                else MaxClicks.Value = "-1";

                                if (SepFunctions.toLong(SepFunctions.ParseXML("MAXEXPOSURES", sShortDesc)) > 0) MaxExposures.Value = Strings.ToString(SepFunctions.toLong(SepFunctions.ParseXML("MAXEXPOSURES", sShortDesc)));
                                else MaxExposures.Value = "-1";

                                string sZone = SepFunctions.ParseXML("ZONE", sShortDesc);
                                if (!string.IsNullOrWhiteSpace(sZone))
                                {
                                    if (Strings.InStr(sZone, "|-1|") > 0) ZonesRow.Visible = false;
                                    else Zone.OnlyInclude = sZone;
                                }
                                else
                                {
                                    ZonesRow.Visible = false;
                                }

                                string sTargetPage = SepFunctions.ParseXML("TARGETPAGE", sShortDesc);
                                if (!string.IsNullOrWhiteSpace(sTargetPage))
                                {
                                    if (Strings.InStr(sTargetPage, "|-1|") > 0 || Strings.InStr(sTargetPage, "|-2|") > 0) WebPagesRow.Visible = false;
                                    else Pages.OnlyInclude = sTargetPage;
                                }
                                else
                                {
                                    WebPagesRow.Visible = false;
                                }

                                string sTargetCat = SepFunctions.ParseXML("TARGETCAT", sShortDesc);
                                if (!string.IsNullOrWhiteSpace(sTargetCat))
                                {
                                    if (Strings.InStr(sTargetCat, "|-1|") > 0 || Strings.InStr(sTargetCat, "|-2|") > 0) CategoriesRow.Visible = false;
                                    else Categories.OnlyInclude = sTargetCat;
                                }
                                else
                                {
                                    CategoriesRow.Visible = false;
                                }

                                string sTargetPortal = SepFunctions.ParseXML("TARGETPORTAL", sShortDesc);
                                if (!string.IsNullOrWhiteSpace(sTargetPortal) && SepFunctions.Setup(60, "PortalsEnable") == "Enable")
                                {
                                    if (Strings.InStr(sTargetPortal, "|-1|") > 0) PortalsRow.Visible = false;
                                    else Portals.OnlyInclude = sTargetPortal;
                                }
                                else
                                {
                                    PortalsRow.Visible = false;
                                }
                            }
                            else
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Uou must select a plan") + "</div>";
                                return;
                            }
                        }
                    }
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
    }
}