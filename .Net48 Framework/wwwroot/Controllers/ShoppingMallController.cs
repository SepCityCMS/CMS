// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-04-2019
// ***********************************************************************
// <copyright file="ShoppingMallController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using SepCommon.Models;
    using System.Collections.Generic;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class ShoppingMallController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ShoppingMallController : ApiController
    {
        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>System.String.</returns>
        [Route("api/product")]
        [HttpDelete]
        public string DeleteProduct([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopMallAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.ShoppingMall.Product_Delete(SepCommon.SepCore.Strings.ToString(ID));
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.ShopProducts.</returns>
        [Route("api/product")]
        [HttpGet]
        public ShopProducts GetProduct([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopMallAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.ShoppingMall.Product_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.ShopProducts&gt;.</returns>
        [Route("api/products")]
        [HttpGet]
        public List<ShopProducts> GetProducts()
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopMallAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.ShoppingMall.GetShopProducts();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Posts the product.
        /// </summary>
        /// <param name="cred">The cred.</param>
        /// <returns>System.Int32.</returns>
        [Route("api/product")]
        [HttpPost]
        public int PostProduct([FromBody] ShopProducts cred)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopMallAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            if (cred == null)
                throw RequestHelper.UnAuthorized("Cannot pass null value");
            try
            {
                int sReturn = SepCommon.DAL.ShoppingMall.Product_Save(cred.ProductID, cred.CatID, SepFunctions.Session_User_ID(), cred.StoreID, cred.ProductName, cred.ShortDescription, cred.FullDescription, cred.PortalID, cred.UnitPrice, cred.RecurringPrice, cred.RecurringCycle, cred.SalePrice, cred.AssocID, cred.NewsletID, cred.ItemWeight, cred.WeightType, cred.DimH, cred.DimW, cred.DimL, cred.UseInventory, cred.Inventory, cred.MinOrderQty, cred.MaxOrderQty, cred.ShippingOption, cred.TaxExempt, cred.Handling, cred.Manufacturer, cred.ModelNumber, cred.SKU, cred.AffiliateUnitPrice, cred.AffiliateUnitPrice, cred.ExcludeAffiliate, cred.ModuleID, string.Empty, string.Empty, string.Empty, cred.ImportID, cred.Status, 0, 0);

                return sReturn;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Puts the product.
        /// </summary>
        /// <param name="cred">The cred.</param>
        /// <returns>System.Int32.</returns>
        [Route("api/product")]
        [HttpPut]
        public int PutProduct([FromBody] ShopProducts cred)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopMallAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            if (cred == null)
                throw RequestHelper.UnAuthorized("Cannot pass null value");
            try
            {
                int sReturn = SepCommon.DAL.ShoppingMall.Product_Save(cred.ProductID, cred.CatID, SepFunctions.Session_User_ID(), cred.StoreID, cred.ProductName, cred.ShortDescription, cred.FullDescription, cred.PortalID, cred.UnitPrice, cred.RecurringPrice, cred.RecurringCycle, cred.SalePrice, cred.AssocID, cred.NewsletID, cred.ItemWeight, cred.WeightType, cred.DimH, cred.DimW, cred.DimL, cred.UseInventory, cred.Inventory, cred.MinOrderQty, cred.MaxOrderQty, cred.ShippingOption, cred.TaxExempt, cred.Handling, cred.Manufacturer, cred.ModelNumber, cred.SKU, cred.AffiliateUnitPrice, cred.AffiliateUnitPrice, cred.ExcludeAffiliate, cred.ModuleID, string.Empty, string.Empty, string.Empty, cred.ImportID, cred.Status, 0, 0);

                return sReturn;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}