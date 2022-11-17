
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.Collections.Generic;

    public class InvoicesController : ControllerBase
    {
        [CheckOption("username", "ShopCartAdmin")]
        [HttpDelete]
        public Models.API.APIResponse DeleteInvoices(long Id)
        {
            Server.DAL.Invoices.Invoice_Delete(Server.SepCore.Strings.ToString(Id));
            var cResponse = new Models.API.APIResponse
            {
                Id = Id,
                Success = true
            };
            return cResponse;
        }

        [CheckOption("username", "ShopCartAdmin")]
        [HttpGet]
        public Models.Invoices GetInvoices(long id)
        {
            var Invoice = Server.DAL.Invoices.Invoice_Get(id);
            return Invoice;
        }

        [CheckOption("username", "ShopCartAdmin")]
        [Route("api/invoices/monthlyorders")]
        [HttpGet]
        public List<Models.ChartData> MonthlyOrders()
        {
            return Server.DAL.Invoices.MonthlyOrders();
        }

        [CheckOption("username", "Everyone")]
        [HttpPost]
        public Models.API.APIResponse PostInvoices(Models.Invoices Invoice)
        {
            var productIds = string.Empty;
            var productQty = string.Empty;
            var customProductNames = string.Empty;
            var customProductPrice = string.Empty;
            if (Invoice.Products != null)
                for (var i = 0; i <= Invoice.Products.Count - 1; i++)
                {
                    productIds += Invoice.Products[i].ProductID;
                    productQty += Server.SepCore.Strings.ToString(Invoice.Products[i].Quantity);
                    if (i > 0)
                    {
                        productIds += "||";
                        productQty += "||";
                    }
                }

            if (Invoice.CustomProducts != null)
                for (var i = 0; i <= Invoice.CustomProducts.Count - 1; i++)
                {
                    customProductNames += Invoice.CustomProducts[i].ProductName;
                    customProductPrice += Server.SepCore.Strings.ToString(Invoice.CustomProducts[i].Price);
                    if (i > 0)
                    {
                        customProductNames += "||";
                        customProductPrice += "||";
                    }
                }

            var InvoiceId = SepFunctions.GetIdentity();
            if (Invoice.InvoiceID > 0) InvoiceId = Invoice.InvoiceID;
            Server.DAL.Invoices.Invoice_Save(InvoiceId, Invoice.UserID, Invoice.Status, Invoice.OrderDate, Invoice.ModuleID, productIds, productQty, string.Empty, string.Empty, Invoice.EmailInvoice, customProductNames, customProductPrice, string.Empty, string.Empty, string.Empty, Invoice.LinkID, Invoice.StoreID, Invoice.PortalID);
            var cResponse = new Models.API.APIResponse
            {
                Id = InvoiceId
            };

            return cResponse;
        }

        [CheckOption("username", "ShopCartAdmin")]
        [HttpPut]
        public Models.API.APIResponse PutInvoices(long id, Models.Invoices Invoice)
        {
            var productIds = string.Empty;
            var productQty = string.Empty;
            var customProductNames = string.Empty;
            var customProductPrice = string.Empty;
            if (Invoice.Products != null)
                for (var i = 0; i <= Invoice.Products.Count - 1; i++)
                {
                    productIds += Invoice.Products[i].ProductID;
                    productQty += Server.SepCore.Strings.ToString(Invoice.Products[i].Quantity);
                    if (i > 0)
                    {
                        productIds += "||";
                        productQty += "||";
                    }
                }

            if (Invoice.CustomProducts != null)
                for (var i = 0; i <= Invoice.CustomProducts.Count - 1; i++)
                {
                    customProductNames += Invoice.CustomProducts[i].ProductName;
                    customProductPrice += Server.SepCore.Strings.ToString(Invoice.CustomProducts[i].Price);
                    if (i > 0)
                    {
                        customProductNames += "||";
                        customProductPrice += "||";
                    }
                }

            Server.DAL.Invoices.Invoice_Save(id, Invoice.UserID, Invoice.Status, Invoice.OrderDate, Invoice.ModuleID, productIds, productQty, string.Empty, string.Empty, Invoice.EmailInvoice, customProductNames, customProductPrice, string.Empty, string.Empty, string.Empty, Invoice.LinkID, Invoice.StoreID, Invoice.PortalID);
            var cResponse = new Models.API.APIResponse
            {
                Id = id
            };
            return cResponse;
        }

        [CheckOption("username", "ShopCartAdmin")]
        [HttpGet]
        public List<Models.Invoices> SearchInvoices()
        {
            var dInvoices = Server.DAL.Invoices.GetInvoices(RequireUserID: true);
            return dInvoices;
        }
    }
}