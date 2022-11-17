// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_create.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class portals_create.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_create : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Create Portal");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    PortalNameLabel.InnerText = SepFunctions.LangText("Portal Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    LanguageLabel.InnerText = SepFunctions.LangText("Portal Language:");
                    TemplateLabel.InnerText = SepFunctions.LangText("Portal Template:");
                    HidePortal.Text = SepFunctions.LangText("Hide portal from showing on directory list.");
                    PortalNameRequired.ErrorMessage = SepFunctions.LangText("~~Portal Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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

            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 60;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PortalsCreate"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            MainDomain.InnerHtml = SepFunctions.GetMasterDomain(false);

            var dPortals = SepCommon.DAL.Portals.GetPortalPlans();
            if (dPortals.Count > 0)
            {
                var str = new StringBuilder();
                str.AppendLine("<div class=\"row\">");
                for (var i = 0; i < dPortals.Count; i++)
                {
                    string sChecked = string.Empty;
                    if (i == 0) sChecked = " checked=\"checked\"";
                    str.AppendLine("<div class=\"col-xs-12 col-md-4\">");
                    str.AppendLine("    <div class=\"panel panel-primary\">");
                    str.AppendLine("        <div class=\"panel-heading\">");
                    str.AppendLine("            <h3 class=\"panel-title\">" + dPortals[i].PlanName + "</h3>");
                    str.AppendLine("        </div>");
                    str.AppendLine("        <div class=\"panel-body\">");
                    str.AppendLine("            <div class=\"the-price\">");
                    str.AppendLine("                <h4>" + SepFunctions.Format_Long_Price(SepFunctions.toDecimal(dPortals[i].OnetimePrice), SepFunctions.toDecimal(dPortals[i].RecurringPrice), dPortals[i].RecurringCycle) + "</h4>");
                    str.AppendLine("            </div>");
                    str.AppendLine("            <table class=\"table\">");
                    str.AppendLine("                <tbody>");
                    var arrModules = Strings.Split(Strings.Replace(dPortals[i].ModuleIDs, "|", string.Empty), ",");
                    if (arrModules != null)
                    {
                        for (var j = 0; j <= Information.UBound(arrModules); j++)
                        {
                            str.AppendLine("                    <tr>");
                            str.AppendLine("                        <td>" + SepFunctions.GetModuleName(SepFunctions.toInt(arrModules[j])) + "</td>");
                            str.AppendLine("                    </tr>");
                        }
                    }

                    str.AppendLine("                </tbody>");
                    str.AppendLine("            </table>");
                    str.AppendLine("        </div>");
                    str.AppendLine("        <div class=\"panel-footer\">");
                    str.AppendLine("            <label class=\"radio-inline\"><input type=\"radio\" name=\"PlanId\" id=\"PlanId" + dPortals[i].PlanID + "\" value=\"" + dPortals[i].PlanID + "\"" + sChecked + " /> " + SepFunctions.LangText("Select Plan") + "</label>");
                    str.AppendLine("        </div>");
                    str.AppendLine("    </div>");
                    str.AppendLine("</div>");
                }

                str.AppendLine("</div>");
                PricingPlans.InnerHtml = Strings.ToString(str);
            }

            if (!Page.IsPostBack)
            {
                PortalID.Value = Strings.ToString(SepFunctions.GetIdentity());
                if (string.IsNullOrWhiteSpace(SepCommon.DAL.Portals.Validate_Friendly_Name(SepFunctions.toLong(PortalID.Value), SepFunctions.Session_User_Name()))) FriendlyName.Value = SepFunctions.Session_User_Name();
                else FriendlyName.Value = SepFunctions.Session_User_Name() + Strings.Left(Strings.ToString(SepFunctions.GetIdentity()), 5);
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var imageData = string.Empty;
            var ImageFileName = string.Empty;
            var imageContentType = string.Empty;
            var Status = 1;
            var OnetimePrice = string.Empty;
            var RecurringCycle = string.Empty;
            var RecurringPrice = string.Empty;
            var PlanName = string.Empty;
            var addCart = false;

            if (!string.IsNullOrWhiteSpace(SepCommon.DAL.Portals.Validate_Friendly_Name(SepFunctions.toLong(PortalID.Value), FriendlyName.Value)))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepCommon.DAL.Portals.Validate_Friendly_Name(SepFunctions.toLong(PortalID.Value), FriendlyName.Value) + "</div>";
                return;
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (SiteLogo.PostedFile != null && !string.IsNullOrWhiteSpace(SiteLogo.PostedFile.FileName))
            {
                var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(SiteLogo.PostedFile.InputStream.Length)) + 1];
                SiteLogo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));
                ImageFileName = SiteLogo.PostedFile.FileName;
                imageContentType = SiteLogo.PostedFile.ContentType;
            }

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PlanID")) && SepFunctions.CompareKeys("|2|", false) == false)
            {
                var dPortals = SepCommon.DAL.Portals.Portals_Plan_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PlanID")));
                if (SepFunctions.toDecimal(dPortals.OnetimePrice) > 0 || SepFunctions.toDecimal(dPortals.RecurringPrice) > 0)
                {
                    Status = 0;
                    addCart = true;
                    PlanName = dPortals.PlanName;
                    OnetimePrice = dPortals.OnetimePrice;
                    RecurringPrice = dPortals.RecurringPrice;
                    RecurringCycle = dPortals.RecurringCycle;
                }
            }

            var intReturn = SepCommon.DAL.Portals.Portal_Save(SepFunctions.toLong(PortalID.Value), PortalName.Value, Description.Value, SepFunctions.toLong(Category.CatID), string.Empty, "|2|", "|1|,|2|,|3|,|4|", HidePortal.Checked, SepFunctions.Session_User_ID(), Language.Text, Template.Text, ImageFileName, imageData, imageContentType, Status, SepFunctions.toLong(SepCommon.SepCore.Request.Item("PlanID")), FriendlyName.Value);

            Globals.Save_Template(Template.Text, false, 60, SepFunctions.toLong(PortalID.Value));

            if (addCart)
            {
                SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, 60, Strings.ToString(SepFunctions.GetIdentity()), "1", string.Empty, string.Empty, false, PlanName, OnetimePrice, RecurringPrice, RecurringCycle, "1", SepFunctions.toLong(PortalID.Value), 0, SepFunctions.toLong(PortalID.Value));
                SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
            }

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, PortalName.Value);
        }
    }
}