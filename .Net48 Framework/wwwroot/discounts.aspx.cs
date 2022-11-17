// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="discounts.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class discounts1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class discounts1 : Page
    {
        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            var dDiscounts = SepCommon.DAL.Discounts.GetDiscounts(CategoryID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")), showAvailable: true);

            if (dDiscounts.Count > 0)
            {
                ListContent.Visible = true;
                NewestContent.Visible = false;
                NewestListings.Visible = false;

                ListContent.DataSource = dDiscounts.ToArray();
                ListContent.DataBind();

                if (dDiscounts.Count <= SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage"))) PagerTemplate.Visible = false;
            }
            else
            {
                PagerTemplate.Visible = false;
                if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) == 0)
                {
                    ListContent.Visible = false;
                    NewestContent.Visible = true;
                    NewestListings.Visible = true;

                    if (SepFunctions.Setup(GlobalVars.ModuleID, "DiscountsDisplayNewest") == "Yes")
                    {
                        var dDiscountssN = SepCommon.DAL.Discounts.GetDiscounts("Mod.DatePosted", "DESC", UserID: SepFunctions.isUserPage() && SepFunctions.Setup(7, "DiscountsDisplayNewest") == "Yes" ? SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")) : string.Empty, showAvailable: true);

                        NewestContent.DataSource = dDiscountssN.Take(10);
                        NewestContent.DataBind();
                    }
                    else
                    {
                        NewestContent.Visible = false;
                        NewestListings.Visible = false;
                    }
                }
                else
                {
                    NewestContent.Visible = false;
                    NewestListings.Visible = false;
                }
            }
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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.LongDate);
        }

        /// <summary>
        /// Formats the ISAPI.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public string Format_ISAPI(object sText)
        {
            return SepFunctions.Format_ISAPI(Strings.ToString(sText));
        }

        /// <summary>
        /// Formats the price.
        /// </summary>
        /// <param name="PriceType">Type of the price.</param>
        /// <param name="PriceOff">The price off.</param>
        /// <returns>System.String.</returns>
        public string FormatPrice(string PriceType, string PriceOff)
        {
            string GetPrice;
            switch (SepFunctions.toLong(PriceType))
            {
                case 0:
                    GetPrice = SepFunctions.Format_Currency(PriceOff) + " " + SepFunctions.LangText("Off");
                    break;

                case 1:
                    GetPrice = PriceOff + "%";
                    break;

                default:
                    GetPrice = SepFunctions.Format_Currency(PriceOff);
                    break;
            }

            return GetPrice;
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortal">if set to <c>true</c> [exclude portal].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortal = false)
        {
            return SepFunctions.GetInstallFolder(excludePortal);
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 5;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "DiscountsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("DiscountsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            if (!Page.IsPostBack)
            {
                BindData();
                PagerTemplate.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));
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
        /// Handles the PreRender event of the PagerTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PagerTemplate_PreRender(object sender, EventArgs e)
        {
            BindData();
        }
    }
}