// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="Invoices.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class Invoices.
    /// </summary>
    public class Invoices
    {
        /// <summary>
        /// Gets or sets the average price.
        /// </summary>
        /// <value>The average price.</value>
        public string AveragePrice { get; set; }

        /// <summary>
        /// Gets or sets the custom products.
        /// </summary>
        /// <value>The custom products.</value>
        public List<CustomProducts> CustomProducts { get; set; }

        /// <summary>
        /// Gets or sets the discount code.
        /// </summary>
        /// <value>The discount code.</value>
        public string DiscountCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email invoice].
        /// </summary>
        /// <value><c>true</c> if [email invoice]; otherwise, <c>false</c>.</value>
        public bool EmailInvoice { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>The invoice identifier.</value>
        public long InvoiceID { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the link identifier.
        /// </summary>
        /// <value>The link identifier.</value>
        public long LinkID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the name of the month.
        /// </summary>
        /// <value>The name of the month.</value>
        public string MonthName { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>The order date.</value>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public List<Products> Products { get; set; }

        /// <summary>
        /// Gets or sets the recurring cycle.
        /// </summary>
        /// <value>The recurring cycle.</value>
        public string RecurringCycle { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <value>The status text.</value>
        public string StatusText { get; set; }

        /// <summary>
        /// Gets or sets the store identifier.
        /// </summary>
        /// <value>The store identifier.</value>
        public long StoreID { get; set; }

        /// <summary>
        /// Gets or sets the name of the store.
        /// </summary>
        /// <value>The name of the store.</value>
        public string StoreName { get; set; }

        /// <summary>
        /// Gets or sets the total handling price.
        /// </summary>
        /// <value>The total handling price.</value>
        public string TotalHandlingPrice { get; set; }

        /// <summary>
        /// Gets or sets the total invoices.
        /// </summary>
        /// <value>The total invoices.</value>
        public string TotalInvoices { get; set; }

        /// <summary>
        /// Gets or sets the total paid.
        /// </summary>
        /// <value>The total paid.</value>
        public string TotalPaid { get; set; }

        /// <summary>
        /// Gets or sets the total recurring.
        /// </summary>
        /// <value>The total recurring.</value>
        public string TotalRecurring { get; set; }

        /// <summary>
        /// Gets or sets the total unit price.
        /// </summary>
        /// <value>The total unit price.</value>
        public string TotalUnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }

    /// <summary>
    /// Class Products.
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public long ProductID { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Class CustomProducts.
    /// </summary>
    public class CustomProducts
    {
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>The name of the product.</value>
        public string ProductName { get; set; }
    }
}