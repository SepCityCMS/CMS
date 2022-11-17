// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="filter-products.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class filter_products.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class filter_products : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang))) ModifyLegend.InnerHtml = SepFunctions.LangText("Add Custom Product");
            }
        }

        /// <summary>
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void AddButton_Click(object sender, EventArgs e)
        {
            var jScript = string.Empty;

            jScript += "<script type=\"text/javascript\">";
            jScript += "assignProduct(unescape('" + SepFunctions.EscQuotes(SepCommon.SepCore.Request.Item("RowOffset")) + "'), '" + SepFunctions.GetIdentity() + "', unescape('" + SepFunctions.EscQuotes(ProductName.Value) + "'), unescape('" + SepFunctions.EscQuotes(SepFunctions.Format_Currency(UnitPrice.Value)) + "'));";
            jScript += "</script>";

            var cstype = GetType();

            Page.ClientScript.RegisterClientScriptBlock(cstype, "ButtonClickScript", jScript);
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin"), true) == false)
            {
                Response.Write("<div align=\"center\" style=\"margin-top:50px\">");
                Response.Write("<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>");
                Response.Write(SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>");
                Response.Write("</div>");
                return;
            }

            if (SepFunctions.ModuleActivated(41))
            {
                var dShopProducts = SepCommon.DAL.ShoppingMall.GetShopProducts(searchWords: SepCommon.SepCore.Request.Item("Keywords"));

                ManageGridView.DataSource = dShopProducts.Take(SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage")));
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0)
                {
                    FilterGrid.Visible = false;
                    CustomProduct.Visible = true;
                    ManageGridView.Visible = false;
                }
                else
                {
                    FilterGrid.Visible = true;
                    CustomProduct.Visible = false;
                }
            }
            else
            {
                FilterGrid.Visible = false;
                CustomProduct.Visible = true;
            }
        }
    }
}