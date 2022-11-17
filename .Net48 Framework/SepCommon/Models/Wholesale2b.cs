// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-04-2019
// ***********************************************************************
// <copyright file="Wholesale2b.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using FileHelpers;

    /// <summary>
    /// Class Wholesale2b. This class cannot be inherited.
    /// </summary>
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    public class Wholesale2b
    {
        /// <summary>
        /// The attribute list
        /// </summary>
        /// <value>The attribute list.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Attribute_list { get; set; }

        /// <summary>
        /// The attributes
        /// </summary>
        /// <value>The attributes.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Attributes { get; set; }

        /// <summary>
        /// The brand
        /// </summary>
        /// <value>The brand.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Brand { get; set; }

        /// <summary>
        /// The canada zone
        /// </summary>
        /// <value>The canada zone.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string CANADA_ZONE { get; set; }

        /// <summary>
        /// The category 1
        /// </summary>
        /// <value>The category 1.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string category_1 { get; set; }

        /// <summary>
        /// The category 2
        /// </summary>
        /// <value>The category 2.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string category_2 { get; set; }

        /// <summary>
        /// The category 3
        /// </summary>
        /// <value>The category 3.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string category_3 { get; set; }

        /// <summary>
        /// The category 4
        /// </summary>
        /// <value>The category 4.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string category_4 { get; set; }

        /// <summary>
        /// The description no HTML
        /// </summary>
        /// <value>The description no HTML.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string description_no_html { get; set; }

        /// <summary>
        /// The extra img 1
        /// </summary>
        /// <value>The extra img 1.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Extra_Img_1 { get; set; }

        /// <summary>
        /// The extra img 2
        /// </summary>
        /// <value>The extra img 2.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Extra_Img_2 { get; set; }

        /// <summary>
        /// The extra img 3
        /// </summary>
        /// <value>The extra img 3.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Extra_Img_3 { get; set; }

        /// <summary>
        /// The extra img 4
        /// </summary>
        /// <value>The extra img 4.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Extra_Img_4 { get; set; }

        /// <summary>
        /// The extra img 5
        /// </summary>
        /// <value>The extra img 5.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Extra_Img_5 { get; set; }

        /// <summary>
        /// The extra img 6
        /// </summary>
        /// <value>The extra img 6.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Extra_Img_6 { get; set; }

        /// <summary>
        /// The image name
        /// </summary>
        /// <value>The name of the image.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Image_name { get; set; }

        /// <summary>
        /// The in stock
        /// </summary>
        /// <value>The in stock.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string In_Stock { get; set; }

        /// <summary>
        /// The item
        /// </summary>
        /// <value>The item.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Item { get; set; }

        /// <summary>
        /// The manufacturer
        /// </summary>
        /// <value>The manufacturer.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string manufacturer { get; set; }

        /// <summary>
        /// The manufacturer product identifier
        /// </summary>
        /// <value>The manufacturer product identifier.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string manufacturer_prod_id { get; set; }

        /// <summary>
        /// The map price
        /// </summary>
        /// <value>The map price.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string MAP_Price { get; set; }

        /// <summary>
        /// The product description
        /// </summary>
        /// <value>The product description.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Product_Description { get; set; }

        /// <summary>
        /// The product description orignal HTML
        /// </summary>
        /// <value>The product description orignal HTML.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Product_description_orignal_html { get; set; }

        /// <summary>
        /// The product name
        /// </summary>
        /// <value>The name of the product.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Product_Name { get; set; }

        /// <summary>
        /// The qty in stock
        /// </summary>
        /// <value>The qty in stock.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Qty_in_stock { get; set; }

        /// <summary>
        /// The refurbished flag
        /// </summary>
        /// <value>The refurbished flag.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string refurbished_flag { get; set; }

        /// <summary>
        /// The retail price
        /// </summary>
        /// <value>The retail price.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Retail_Price { get; set; }

        /// <summary>
        /// The sales tax PCT
        /// </summary>
        /// <value>The sales tax PCT.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string sales_tax_pct { get; set; }

        /// <summary>
        /// The sales tax state
        /// </summary>
        /// <value>The state of the sales tax.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string sales_tax_state { get; set; }

        /// <summary>
        /// The shipping for canada
        /// </summary>
        /// <value>The shipping for canada.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Shipping_for_Canada { get; set; }

        /// <summary>
        /// The shipping for international
        /// </summary>
        /// <value>The shipping for international.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Shipping_for_International { get; set; }

        /// <summary>
        /// The shipping for usa
        /// </summary>
        /// <value>The shipping for usa.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Shipping_for_USA { get; set; }

        /// <summary>
        /// The shipping height
        /// </summary>
        /// <value>The height of the shipping.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string shipping_height { get; set; }

        /// <summary>
        /// The shipping length
        /// </summary>
        /// <value>The length of the shipping.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string shipping_length { get; set; }

        /// <summary>
        /// The shipping width
        /// </summary>
        /// <value>The width of the shipping.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string shipping_width { get; set; }

        /// <summary>
        /// The supplier category 1
        /// </summary>
        /// <value>The supplier category 1.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string supplier_category_1 { get; set; }

        /// <summary>
        /// The supplier category 2
        /// </summary>
        /// <value>The supplier category 2.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string supplier_category_2 { get; set; }

        /// <summary>
        /// The supplier category 3
        /// </summary>
        /// <value>The supplier category 3.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string supplier_category_3 { get; set; }

        /// <summary>
        /// The supplier category 4
        /// </summary>
        /// <value>The supplier category 4.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string supplier_category_4 { get; set; }

        /// <summary>
        /// The supplier handling fee
        /// </summary>
        /// <value>The supplier handling fee.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Supplier_handling_fee { get; set; }

        /// <summary>
        /// The supplier name item number
        /// </summary>
        /// <value>The supplier name item number.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string supplier_name_item_number { get; set; }

        /// <summary>
        /// The upc
        /// </summary>
        /// <value>The upc.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string UPC { get; set; }

        /// <summary>
        /// The URL to large image
        /// </summary>
        /// <value>The URL to large image.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string URL_to_large_image { get; set; }

        /// <summary>
        /// The URL to thumb image
        /// </summary>
        /// <value>The URL to thumb image.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string URL_to_thumb_image { get; set; }

        /// <summary>
        /// The usa zone
        /// </summary>
        /// <value>The usa zone.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string USA_ZONE { get; set; }

        /// <summary>
        /// The w2 b 3 transaction fee canada
        /// </summary>
        /// <value>The w2 b 3 transaction fee canada.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string W2B_3_Transaction_Fee_Canada { get; set; }

        /// <summary>
        /// The w2 b 3 transaction fee international
        /// </summary>
        /// <value>The w2 b 3 transaction fee international.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string W2B_3_Transaction_Fee_International { get; set; }

        /// <summary>
        /// The w2 b 3 transaction fee usa
        /// </summary>
        /// <value>The w2 b 3 transaction fee usa.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string W2B_3_Transaction_Fee_USA { get; set; }

        /// <summary>
        /// The W2B handling fee
        /// </summary>
        /// <value>The W2B handling fee.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string w2b_handling_fee { get; set; }

        /// <summary>
        /// The weight
        /// </summary>
        /// <value>The weight.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Weight { get; set; }

        /// <summary>
        /// The wholesale price before markup
        /// </summary>
        /// <value>The wholesale price before markup.</value>
        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.AllowForBoth)]
        public string Wholesale_Price_Before_Markup { get; set; }
    }
}