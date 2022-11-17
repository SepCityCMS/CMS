// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopping_products_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class shopping_products_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopping_products_modify : Page
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
        /// Formats the currency.
        /// </summary>
        /// <param name="sNum">The s number.</param>
        /// <returns>System.String.</returns>
        public string Format_Currency(object sNum)
        {
            return SepFunctions.Format_Currency(sNum);
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ShippingOption control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void ShippingOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ShippingOption.SelectedValue)
            {
                case "shipping":
                    ShippingOptions.Visible = true;
                    ElectronicOptions.Visible = false;
                    break;

                case "electronic":
                    ShippingOptions.Visible = false;
                    ElectronicOptions.Visible = true;
                    break;

                default:
                    ShippingOptions.Visible = false;
                    ElectronicOptions.Visible = false;
                    break;
            }

            var scriptKey = "openInventory";
            var javaScript = "<script type=\"text/javascript\">openInventory();</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
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
                    RecurringCycle.Items[0].Text = SepFunctions.LangText("Monthly");
                    RecurringCycle.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle.Items[3].Text = SepFunctions.LangText("Yearly");
                    ShippingOption.Items[0].Text = SepFunctions.LangText("Disabled");
                    ShippingOption.Items[1].Text = SepFunctions.LangText("Shipping");
                    ShippingOption.Items[2].Text = SepFunctions.LangText("Electronic");
                    WeightType.Items[0].Text = SepFunctions.LangText("lbs");
                    WeightType.Items[1].Text = SepFunctions.LangText("oz");
                    CustomFields.Columns[0].HeaderText = SepFunctions.LangText("Field Name");
                    CustomFields.Columns[1].HeaderText = SepFunctions.LangText("Answer Type");
                    CustomFields.Columns[2].HeaderText = SepFunctions.LangText("Delete");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Product");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    ProductNameLabel.InnerText = SepFunctions.LangText("Product Name:");
                    ManufacturerLabel.InnerText = SepFunctions.LangText("Manufacturer:");
                    ModelNumberLabel.InnerText = SepFunctions.LangText("Model Number:");
                    ShortDescriptionLabel.InnerText = SepFunctions.LangText("Short Description:");
                    ImagesLabel.InnerText = SepFunctions.LangText("Images:");
                    FullDescriptionLabel.InnerText = SepFunctions.LangText("Full Description:");
                    UnitPriceLabel.InnerText = SepFunctions.LangText("Unit Price:");
                    SalePriceLabel.InnerText = SepFunctions.LangText("Sale Price:");
                    RecurringPriceLabel.InnerText = SepFunctions.LangText("Recurring Price:");
                    RecurringCycleLabel.InnerText = SepFunctions.LangText("Recurring Cycle:");
                    ShippingOptionLabel.InnerText = SepFunctions.LangText("Shipping Option:");
                    ItemWeightLabel.InnerText = SepFunctions.LangText("Item Weight:");
                    DimensionsLabel.InnerText = SepFunctions.LangText("Dimensions (L x W x H):");
                    HandlingLabel.InnerText = SepFunctions.LangText("Handling:");
                    InventoryLabel.InnerText = SepFunctions.LangText("Inventory:");
                    MinOrderQtyLabel.InnerText = SepFunctions.LangText("Min Order Quantity:");
                    MaxOrderQtyLabel.InnerText = SepFunctions.LangText("Max Order Quantity:");
                    SKULabel.InnerText = SepFunctions.LangText("SKU:");
                    SelectFileLabel.InnerText = SepFunctions.LangText("Select File:");
                    AffiliatePriceLabel.InnerText = SepFunctions.LangText("Affiliate Price:");
                    AffiliateRecurringLabel.InnerText = SepFunctions.LangText("Affiliate Recurring:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    ProductNameRequired.ErrorMessage = SepFunctions.LangText("~~Product Name~~ is required.");
                    ShortDescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Short Description~~ is required.");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 41;

            if ((SepFunctions.Setup(GlobalVars.ModuleID, "ShopMallEnable") != "Enable" || SepFunctions.isUserPage()) && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ShopMallStore"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var sStoreID = SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID());
            if (sStoreID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Access Denied. You do not have a store setup.") + "</div>";
                return;
            }

            if (!Page.IsPostBack) StoreID.Value = Strings.ToString(sStoreID);

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ProductID")))
            {
                var jProducts = SepCommon.DAL.ShoppingMall.Product_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ProductID")));

                if (jProducts.ProductID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Product~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Product");
                    ProductID.Value = SepCommon.SepCore.Request.Item("ProductID");
                    Images.ContentID = SepCommon.SepCore.Request.Item("ProductID");
                    SelectFile.ContentID = SepCommon.SepCore.Request.Item("ProductID");
                    Category.CatID = Strings.ToString(jProducts.CatID);
                    ProductName.Value = jProducts.ProductName;
                    Manufacturer.Value = jProducts.Manufacturer;
                    ModelNumber.Value = jProducts.ModelNumber;
                    ShortDescription.Value = jProducts.ShortDescription;
                    FullDescription.Text = jProducts.FullDescription;
                    UnitPrice.Value = SepFunctions.Format_Currency(jProducts.UnitPrice);
                    SalePrice.Value = SepFunctions.Format_Currency(jProducts.SalePrice);
                    RecurringPrice.Value = SepFunctions.Format_Currency(jProducts.RecurringPrice);
                    RecurringCycle.Value = jProducts.RecurringCycle;
                    ShippingOption.SelectedValue = jProducts.ShippingOption;
                    ItemWeight.Value = Strings.ToString(jProducts.ItemWeight);
                    WeightType.Value = jProducts.WeightType;
                    DimL.Value = Strings.ToString(jProducts.DimL);
                    DimW.Value = Strings.ToString(jProducts.DimW);
                    DimH.Value = Strings.ToString(jProducts.DimH);
                    Handling.Value = SepFunctions.Format_Currency(jProducts.Handling);
                    Inventory.Value = Strings.ToString(jProducts.Inventory);
                    MinOrderQty.Value = Strings.ToString(jProducts.MinOrderQty);
                    MaxOrderQty.Value = Strings.ToString(jProducts.MaxOrderQty);
                    SKU.Value = jProducts.SKU;
                    ExcludeAffiliate.Checked = jProducts.ExcludeAffiliate;
                    AffiliateUnitPrice.Value = Strings.ToString(jProducts.AffiliateUnitPrice);
                    AffiliateRecurringPrice.Value = Strings.ToString(jProducts.AffiliateRecurringPrice);
                    var dCustomFields = SepCommon.DAL.CustomFields.GetCustomFields(ModuleID: 41, uniqueIds: "|" + ProductID.Value + "|");
                    CustomFields.DataSource = dCustomFields.Take(50);
                    CustomFields.DataBind();
                    dCustomFields = SepCommon.DAL.CustomFields.GetCustomFields(ModuleID: 41, uniqueIds: "|" + ProductID.Value + "|");
                    for (var i = 0; i <= dCustomFields.Count - 1; i++)
                    {
                        FieldIDs.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty) + dCustomFields[i].FieldID;
                        FieldNames.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty) + dCustomFields[i].FieldName;
                        AnswerTypes.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty) + dCustomFields[i].AnswerType;
                        CustomFieldOptions.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty);
                        var dCustomFieldOptions = SepCommon.DAL.CustomFields.GetCustomFieldOptions(dCustomFields[i].FieldID);
                        for (var j = 0; j <= dCustomFieldOptions.Count - 1; j++)
                        {
                            if (j > 0) CustomFieldOptions.Value += "|%%|";
                            CustomFieldOptions.Value += dCustomFields[i].FieldID + "|!|" + dCustomFieldOptions[j].OptionID + "|!|" + dCustomFieldOptions[j].OptionName + "|!|" + dCustomFieldOptions[j].Weight + "|!|" + dCustomFieldOptions[j].SetupPrice + "|!|" + dCustomFieldOptions[j].RecurringPrice;
                        }

                        Orders.Value += i > 0 ? "|%|" : Strings.ToString(dCustomFields[i].Weight);
                        Requires.Value += i > 0 ? "|%|" : dCustomFields[i].Required ? "1" : "0";
                    }

                    // RelatedItems.Text = jProducts.RelatedItems
                    if (jProducts.Status == 0) DisableProduct.Checked = true;
                    TaxExempt.Checked = jProducts.TaxExempt;
                    switch (ShippingOption.SelectedValue)
                    {
                        case "shipping":
                            ShippingOptions.Visible = true;
                            ElectronicOptions.Visible = false;
                            break;

                        case "electronic":
                            ShippingOptions.Visible = false;
                            ElectronicOptions.Visible = true;
                            break;

                        default:
                            ShippingOptions.Visible = false;
                            ElectronicOptions.Visible = false;
                            break;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ProductID.Value)) ProductID.Value = Strings.ToString(SepFunctions.GetIdentity());
                Images.ContentID = ProductID.Value;
                SelectFile.ContentID = ProductID.Value;
            }

            if (CustomFields.Rows.Count == 0)
            {
                var tbl = new DataTable();
                var col1 = new DataColumn("FieldName", typeof(string));
                var col2 = new DataColumn("AnswerType", typeof(string));
                var col3 = new DataColumn("Offset", typeof(string));
                var col4 = new DataColumn("FieldID", typeof(string));
                tbl.Columns.Add(col1);
                tbl.Columns.Add(col2);
                tbl.Columns.Add(col3);
                tbl.Columns.Add(col4);
                var row = tbl.NewRow();
                row["FieldName"] = string.Empty;
                row["AnswerType"] = string.Empty;
                row["Offset"] = string.Empty;
                row["FieldID"] = string.Empty;
                tbl.Rows.Add(row);
                CustomFields.DataSource = tbl;
                CustomFields.DataBind();
                CustomFields.Rows[0].Visible = false;
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
            var UseInventory = false;
            var AssocID = string.Empty;
            var CustomFieldData = FieldIDs.Value + "|$$|" + FieldNames.Value + "|$$|" + AnswerTypes.Value + "|$$|" + CustomFieldOptions.Value + "|$$|" + Orders.Value + "|$$|" + Requires.Value;

            if (SepFunctions.toInt(Inventory.Value) > 0)
                UseInventory = true;

            var intReturn = SepCommon.DAL.ShoppingMall.Product_Save(SepFunctions.toLong(ProductID.Value), SepFunctions.toLong(Category.CatID), SepFunctions.Session_User_ID(), SepFunctions.toLong(StoreID.Value), ProductName.Value, ShortDescription.Value, FullDescription.Text, SepFunctions.Get_Portal_ID(), SepFunctions.toDecimal(UnitPrice.Value), SepFunctions.toDecimal(RecurringPrice.Value), RecurringCycle.Value, SepFunctions.toDecimal(SalePrice.Value), AssocID, string.Empty, SepFunctions.toDecimal(ItemWeight.Value), WeightType.Value, SepFunctions.toDecimal(DimH.Value), SepFunctions.toDecimal(DimW.Value), SepFunctions.toDecimal(DimL.Value), UseInventory, SepFunctions.toInt(Inventory.Value), SepFunctions.toInt(MinOrderQty.Value), SepFunctions.toInt(MaxOrderQty.Value), ShippingOption.SelectedValue, TaxExempt.Checked, SepFunctions.toDecimal(Handling.Value), Manufacturer.Value, ModelNumber.Value, SKU.Value, SepFunctions.toDecimal(AffiliateUnitPrice.Value), SepFunctions.toDecimal(AffiliateRecurringPrice.Value), ExcludeAffiliate.Checked, 41, DeleteCustomFieldIds.Value, DeleteCustomFieldOptions.Value, CustomFieldData, string.Empty, DisableProduct.Checked ? 0 : 1, 0, 0);

            FieldIDs.Value = string.Empty;
            FieldNames.Value = string.Empty;
            AnswerTypes.Value = string.Empty;
            CustomFieldOptions.Value = string.Empty;
            Orders.Value = string.Empty;
            Requires.Value = string.Empty;

            var dCustomFields = SepCommon.DAL.CustomFields.GetCustomFields(ModuleID: 41, uniqueIds: "|" + ProductID.Value + "|");
            CustomFields.DataSource = dCustomFields.Take(50);
            CustomFields.DataBind();
            dCustomFields = SepCommon.DAL.CustomFields.GetCustomFields(ModuleID: 41, uniqueIds: "|" + ProductID.Value + "|");
            for (var i = 0; i <= dCustomFields.Count - 1; i++)
            {
                FieldIDs.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty) + dCustomFields[i].FieldID;
                FieldNames.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty) + dCustomFields[i].FieldName;
                AnswerTypes.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty) + dCustomFields[i].AnswerType;
                CustomFieldOptions.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty);
                var dCustomFieldOptions = SepCommon.DAL.CustomFields.GetCustomFieldOptions(dCustomFields[i].FieldID);
                for (var j = 0; j <= dCustomFieldOptions.Count - 1; j++)
                {
                    if (j > 0) CustomFieldOptions.Value += "|%%|";
                    CustomFieldOptions.Value += dCustomFields[i].FieldID + "|!|" + dCustomFieldOptions[j].OptionID + "|!|" + dCustomFieldOptions[j].OptionName + "|!|" + dCustomFieldOptions[j].Weight + "|!|" + dCustomFieldOptions[j].SetupPrice + "|!|" + dCustomFieldOptions[j].RecurringPrice;
                }

                CustomFieldOptions.Value += Strings.ToString(i > 0 ? "|%|" : string.Empty);
                Orders.Value += Strings.ToString(i > 0 ? "|%|" : Strings.ToString(dCustomFields[i].Weight));
                Requires.Value += Strings.ToString(i > 0 ? "|%|" : Strings.ToString(dCustomFields[i].Required ? "1" : "0"));
            }

            DeleteCustomFieldIds.Value = string.Empty;

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, ProductName.Value);
        }
    }
}