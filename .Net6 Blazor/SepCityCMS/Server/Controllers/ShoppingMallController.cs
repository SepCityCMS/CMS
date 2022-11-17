
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.Collections.Generic;

    public class ShoppingMallController : ControllerBase
    {
        [CheckOption("username", "ShopMallAdmin")]
        [Route("api/product")]
        [HttpDelete]
        public string DeleteProduct([FromQuery] long ID)
        {
            return Server.DAL.ShoppingMall.Product_Delete(Server.SepCore.Strings.ToString(ID));
        }

        [CheckOption("username", "ShopMallAccess")]
        [Route("api/product")]
        [HttpGet]
        public Models.ShopProducts GetProduct([FromQuery] long ID)
        {
            return Server.DAL.ShoppingMall.Product_Get(ID);
        }

        [CheckOption("username", "ShopMallAccess")]
        [Route("api/products")]
        [HttpGet]
        public List<Models.ShopProducts> GetProducts()
        {
            return Server.DAL.ShoppingMall.GetShopProducts();
        }

        [CheckOption("username", "ShopMallAdmin")]
        [Route("api/product")]
        [HttpPost]
        public int PostProduct([FromBody] Models.ShopProducts cred)
        {
            int sReturn = Server.DAL.ShoppingMall.Product_Save(cred.ProductID, cred.CatID, SepFunctions.Session_User_ID(), cred.StoreID, cred.ProductName, cred.ShortDescription, cred.FullDescription, cred.PortalID, cred.UnitPrice, cred.RecurringPrice, cred.RecurringCycle, cred.SalePrice, cred.AssocID, cred.NewsletID, cred.ItemWeight, cred.WeightType, cred.DimH, cred.DimW, cred.DimL, cred.UseInventory, cred.Inventory, cred.MinOrderQty, cred.MaxOrderQty, cred.ShippingOption, cred.TaxExempt, cred.Handling, cred.Manufacturer, cred.ModelNumber, cred.SKU, cred.AffiliateUnitPrice, cred.AffiliateUnitPrice, cred.ExcludeAffiliate, cred.ModuleID, string.Empty, string.Empty, string.Empty, cred.ImportID, cred.Status, 0);

            return sReturn;
        }

        [CheckOption("username", "ShopMallAdmin")]
        [Route("api/product")]
        [HttpPut]
        public int PutProduct([FromBody] Models.ShopProducts cred)
        {
            int sReturn = Server.DAL.ShoppingMall.Product_Save(cred.ProductID, cred.CatID, SepFunctions.Session_User_ID(), cred.StoreID, cred.ProductName, cred.ShortDescription, cred.FullDescription, cred.PortalID, cred.UnitPrice, cred.RecurringPrice, cred.RecurringCycle, cred.SalePrice, cred.AssocID, cred.NewsletID, cred.ItemWeight, cred.WeightType, cred.DimH, cred.DimW, cred.DimL, cred.UseInventory, cred.Inventory, cred.MinOrderQty, cred.MaxOrderQty, cred.ShippingOption, cred.TaxExempt, cred.Handling, cred.Manufacturer, cred.ModelNumber, cred.SKU, cred.AffiliateUnitPrice, cred.AffiliateUnitPrice, cred.ExcludeAffiliate, cred.ModuleID, string.Empty, string.Empty, string.Empty, cred.ImportID, cred.Status, 0);

            return sReturn;
        }
    }
}