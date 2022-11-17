// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="DiscountsController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon.Models;
    using System.Collections.Generic;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class DiscountsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DiscountsController : ApiController
    {
        /// <summary>
        /// Gets the discount.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.Discounts.</returns>
        [Route("api/discount")]
        [HttpGet]
        public Discounts GetDiscount([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("DiscountsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Discounts.Discount_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the discounts.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Discounts&gt;.</returns>
        [Route("api/discounts")]
        [HttpGet]
        public List<Discounts> GetDiscounts()
        {
            var SEP = RequestHelper.AuthorizeRequest("DiscountsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Discounts.GetDiscounts();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}