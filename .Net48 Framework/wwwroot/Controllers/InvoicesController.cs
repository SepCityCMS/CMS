// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-23-2019
// ***********************************************************************
// <copyright file="InvoicesController.cs" company="SepCity, Inc.">
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
    /// Class InvoicesController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class InvoicesController : ApiController
    {
        /// <summary>
        /// Deletes the invoices.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ResponseHelper.DeleteResponseSuccess.</returns>
        [HttpDelete]
        public ResponseHelper.DeleteResponseSuccess DeleteInvoices(long id)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopCartAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                SepCommon.DAL.Invoices.Invoice_Delete(SepCommon.SepCore.Strings.ToString(id));
                var cResponse = new ResponseHelper.DeleteResponseSuccess
                {
                    Success = true
                };
                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SepCommon.Models.Invoices.</returns>
        [HttpGet]
        public Invoices GetInvoices(long id)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopCartAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var Invoice = SepCommon.DAL.Invoices.Invoice_Get(id);
                return Invoice;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Monthlies the orders.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.ChartData&gt;.</returns>
        [Route("api/invoices/monthlyorders")]
        [HttpGet]
        public List<ChartData> MonthlyOrders()
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopCartAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Invoices.MonthlyOrders();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Posts the invoices.
        /// </summary>
        /// <param name="Invoice">The invoice.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [HttpPost]
        public ResponseHelper.CreateResponse PostInvoices(Invoices Invoice)
        {
            // var SEP = RequestHelper.AuthorizeRequest("ShopCartAdmin");
            // if (SEP == false)
            // throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var productIds = string.Empty;
                var productQty = string.Empty;
                var customProductNames = string.Empty;
                var customProductPrice = string.Empty;
                if (Invoice.Products != null)
                    for (var i = 0; i <= Invoice.Products.Count - 1; i++)
                    {
                        productIds += Invoice.Products[i].ProductID;
                        productQty += SepCommon.SepCore.Strings.ToString(Invoice.Products[i].Quantity);
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
                        customProductPrice += SepCommon.SepCore.Strings.ToString(Invoice.CustomProducts[i].Price);
                        if (i > 0)
                        {
                            customProductNames += "||";
                            customProductPrice += "||";
                        }
                    }

                var InvoiceId = SepFunctions.GetIdentity();
                if (Invoice.InvoiceID > 0) InvoiceId = Invoice.InvoiceID;
                SepCommon.DAL.Invoices.Invoice_Save(InvoiceId, Invoice.UserID, Invoice.Status, Invoice.OrderDate, Invoice.ModuleID, productIds, productQty, string.Empty, string.Empty, Invoice.EmailInvoice, customProductNames, customProductPrice, string.Empty, string.Empty, string.Empty, Invoice.LinkID, Invoice.StoreID, Invoice.PortalID);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = InvoiceId
                };

                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Puts the invoices.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="Invoice">The invoice.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [HttpPut]
        public ResponseHelper.CreateResponse PutInvoices(long id, Invoices Invoice)
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopCartAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var productIds = string.Empty;
                var productQty = string.Empty;
                var customProductNames = string.Empty;
                var customProductPrice = string.Empty;
                if (Invoice.Products != null)
                    for (var i = 0; i <= Invoice.Products.Count - 1; i++)
                    {
                        productIds += Invoice.Products[i].ProductID;
                        productQty += SepCommon.SepCore.Strings.ToString(Invoice.Products[i].Quantity);
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
                        customProductPrice += SepCommon.SepCore.Strings.ToString(Invoice.CustomProducts[i].Price);
                        if (i > 0)
                        {
                            customProductNames += "||";
                            customProductPrice += "||";
                        }
                    }

                SepCommon.DAL.Invoices.Invoice_Save(id, Invoice.UserID, Invoice.Status, Invoice.OrderDate, Invoice.ModuleID, productIds, productQty, string.Empty, string.Empty, Invoice.EmailInvoice, customProductNames, customProductPrice, string.Empty, string.Empty, string.Empty, Invoice.LinkID, Invoice.StoreID, Invoice.PortalID);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = id
                };
                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Searches the invoices.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Invoices&gt;.</returns>
        [HttpGet]
        public List<Invoices> SearchInvoices()
        {
            var SEP = RequestHelper.AuthorizeRequest("ShopCartAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var dInvoices = SepCommon.DAL.Invoices.GetInvoices(RequireUserID: true);
                return dInvoices;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}