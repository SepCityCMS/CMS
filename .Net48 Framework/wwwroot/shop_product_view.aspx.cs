// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shop_product_view.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class shop_product_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shop_product_view : Page
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
        /// Gets the sale percentage.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sUnitPrice">The s unit price.</param>
        /// <returns>System.String.</returns>
        public string Get_Sale_Percentage(decimal sSalePrice, decimal sUnitPrice)
        {
            double sSavePercent = SepFunctions.Format_Double(Strings.FormatNumber(100 - sSalePrice / sUnitPrice * 100, 0));
            return sSavePercent + "%";
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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

            GlobalVars.ModuleID = 41;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ShopMallEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ShopMallAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ProductID")))
                {
                    var sProductId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ProductID"));

                    var jProducts = SepCommon.DAL.ShoppingMall.Product_Get(sProductId);

                    if (jProducts.ProductID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Product~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                    else
                    {
                        Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jProducts.ProductName);
                        ProductName.InnerHtml = jProducts.ProductName;
                        FullDescription.InnerHtml = jProducts.FullDescription;
                        if (!string.IsNullOrWhiteSpace(jProducts.Manufacturer)) Manufacturer.InnerHtml = jProducts.Manufacturer;
                        else Manufacturer.Visible = false;
                        if (!string.IsNullOrWhiteSpace(jProducts.ModelNumber)) Model.InnerHtml = jProducts.ModelNumber;
                        else ModelData.Visible = false;
                        if (jProducts.ItemWeight > 0) Weight.InnerHtml = jProducts.ItemWeight + jProducts.WeightType;
                        else WeightData.Visible = false;
                        if (jProducts.DimH > 0 || jProducts.DimL > 0 || jProducts.DimW > 0) Dimensions.InnerHtml = jProducts.DimH + " x " + jProducts.DimL + " x " + jProducts.DimW;
                        else DimensionsData.Visible = false;
                        if (jProducts.SalePrice > 0 && jProducts.SalePrice < jProducts.UnitPrice)
                        {
                            Price.InnerHtml = SepFunctions.Format_Long_Price(jProducts.SalePrice, jProducts.RecurringPrice, jProducts.RecurringCycle);
                            SavePercent.InnerHtml = "<div style=\"padding-top:25px;\">" + Get_Sale_Percentage(jProducts.SalePrice, jProducts.UnitPrice) + "</div>";
                            SavePercent.Style.Add("background-image", "url(" + sInstallFolder + "images/admin/save_money.gif)");
                        }
                        else
                        {
                            Price.InnerHtml = SepFunctions.Format_Long_Price(jProducts.UnitPrice, jProducts.RecurringPrice, jProducts.RecurringCycle);
                            SaleColumn.Visible = false;
                        }

                        // Show Images
                        Images.ContentUniqueID = Strings.ToString(sProductId);
                        Images.ModuleID = GlobalVars.ModuleID;

                        // Custom Options
                        var dCustomFields = SepCommon.DAL.CustomFields.GetCustomFields(ModuleID: 41, uniqueIds: "|" + sProductId + "|");
                        if (dCustomFields.Count == 0 && jProducts.Inventory < 2)
                        {
                            CustomOptions.Visible = false;
                        }
                        else
                        {
                            if (jProducts.Inventory > 1)
                            {
                                QuantityRow.Visible = true;
                                for (var i = 1; i <= jProducts.Inventory; i++)
                                {
                                    Quantity.Items.Add(new ListItem(Strings.ToString(i), Strings.ToString(i)));
                                    if (i == 20)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                QuantityRow.Visible = false;
                                LitQty.InnerHtml = "<input type=\"hidden\" id=\"Quantity\" name=\"Quantity\" value=\"1\" />";
                            }
                        }

                        // Validate Custom Options
                        var sJavascript = string.Empty;
                        var sCustomData = string.Empty;
                        var scriptKey = string.Empty;
                        var javaScript = string.Empty;
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
                        if (dCustomFields.Count > 0)
                        {
                            for (var i = 0; i <= dCustomFields.Count - 1; i++)
                            {
                                if (dCustomFields[i].Required) sJavascript += "if($('#Custom" + dCustomFields[i].FieldID + "').val() == '') { alert(unescape('" + SepFunctions.EscQuotes(dCustomFields[i].FieldName) + "') + ' is required.');return false; };" + Environment.NewLine;
                                sCustomData += "str += \"<Custom" + dCustomFields[i].FieldID + ">\"+$('#Custom" + dCustomFields[i].FieldID + "').val()+\"</Custom" + dCustomFields[i].FieldID + ">\";" + Environment.NewLine;
                            }

                            sJavascript += "return true;";
                        }
                        else
                        {
                            sJavascript = "return true;";
                        }

                        scriptKey = "ValidateCustomOptions";
                        javaScript = "<script type=\"text/javascript\">function ValidateCustomOptions() { " + sJavascript + " }</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);

                        scriptKey = "hasCustomOptions";
                        javaScript = "<script type=\"text/javascript\">function hasCustomOptions() { var str = ''; " + sCustomData + "; return str; }</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);

                        // Wish List Button
                        if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID())) WishListButton.OnClientClick = "saveWishList('" + sProductId + "');return false;";
                        else WishListButton.OnClientClick = "document.location.href='" + sInstallFolder + "login.aspx';return false;";

                        // Add to Cart Button
                        AddCartButton.OnClientClick = "orderProduct('" + sProductId + "', '" + jProducts.StoreID + "');return false;";
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