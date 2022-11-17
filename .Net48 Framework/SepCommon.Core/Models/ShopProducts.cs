// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ShopProducts.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class ShopProducts.
    /// </summary>
    public class ShopProducts
    {
        /// <summary>
        /// Gets or sets the affiliate recurring price.
        /// </summary>
        /// <value>The affiliate recurring price.</value>
        public decimal AffiliateRecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets the affiliate unit price.
        /// </summary>
        /// <value>The affiliate unit price.</value>
        public decimal AffiliateUnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the assoc identifier.
        /// </summary>
        /// <value>The assoc identifier.</value>
        public string AssocID { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the default picture.
        /// </summary>
        /// <value>The default picture.</value>
        public string DefaultPicture { get; set; }

        /// <summary>
        /// Gets or sets the dim h.
        /// </summary>
        /// <value>The dim h.</value>
        public decimal DimH { get; set; }

        /// <summary>
        /// Gets or sets the dim l.
        /// </summary>
        /// <value>The dim l.</value>
        public decimal DimL { get; set; }

        /// <summary>
        /// Gets or sets the dim w.
        /// </summary>
        /// <value>The dim w.</value>
        public decimal DimW { get; set; }

        /// <summary>
        /// Gets or sets the display price.
        /// </summary>
        /// <value>The display price.</value>
        public string DisplayPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exclude affiliate].
        /// </summary>
        /// <value><c>true</c> if [exclude affiliate]; otherwise, <c>false</c>.</value>
        public bool ExcludeAffiliate { get; set; }

        /// <summary>
        /// Gets or sets the full description.
        /// </summary>
        /// <value>The full description.</value>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets the handling.
        /// </summary>
        /// <value>The handling.</value>
        public decimal Handling { get; set; }

        /// <summary>
        /// Gets or sets the import identifier.
        /// </summary>
        /// <value>The import identifier.</value>
        public string ImportID { get; set; }

        /// <summary>
        /// Gets or sets the inventory.
        /// </summary>
        /// <value>The inventory.</value>
        public int Inventory { get; set; }

        /// <summary>
        /// Gets or sets the item weight.
        /// </summary>
        /// <value>The item weight.</value>
        public decimal ItemWeight { get; set; }

        /// <summary>
        /// Gets or sets the long price.
        /// </summary>
        /// <value>The long price.</value>
        public string LongPrice { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer.
        /// </summary>
        /// <value>The manufacturer.</value>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the maximum order qty.
        /// </summary>
        /// <value>The maximum order qty.</value>
        public int MaxOrderQty { get; set; }

        /// <summary>
        /// Gets or sets the minimum order qty.
        /// </summary>
        /// <value>The minimum order qty.</value>
        public int MinOrderQty { get; set; }

        /// <summary>
        /// Gets or sets the model number.
        /// </summary>
        /// <value>The model number.</value>
        public string ModelNumber { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the newslet identifier.
        /// </summary>
        /// <value>The newslet identifier.</value>
        public string NewsletID { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public long ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>The name of the product.</value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the recurring cycle.
        /// </summary>
        /// <value>The recurring cycle.</value>
        public string RecurringCycle { get; set; }

        /// <summary>
        /// Gets or sets the recurring price.
        /// </summary>
        /// <value>The recurring price.</value>
        public decimal RecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets the sale price.
        /// </summary>
        /// <value>The sale price.</value>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the shipping option.
        /// </summary>
        /// <value>The shipping option.</value>
        public string ShippingOption { get; set; }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>The short description.</value>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>The sku.</value>
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the store identifier.
        /// </summary>
        /// <value>The store identifier.</value>
        public long StoreID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [tax exempt].
        /// </summary>
        /// <value><c>true</c> if [tax exempt]; otherwise, <c>false</c>.</value>
        public bool TaxExempt { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>The unit price.</value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use inventory].
        /// </summary>
        /// <value><c>true</c> if [use inventory]; otherwise, <c>false</c>.</value>
        public bool UseInventory { get; set; }

        /// <summary>
        /// Gets or sets the type of the weight.
        /// </summary>
        /// <value>The type of the weight.</value>
        public string WeightType { get; set; }
    }
}