﻿// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="AffiliateController.cs" company="SepCity, Inc.">
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
    /// Class AffiliateController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AffiliateController : ApiController
    {
        /// <summary>
        /// Affiliates the totals.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.AffiliateDownline&gt;.</returns>
        [Route("api/affiliate/affiliatetotals")]
        [HttpGet]
        public List<AffiliateDownline> AffiliateTotals()
        {
            var SEP = RequestHelper.AuthorizeRequest("AffiliateJoin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Affiliate.AffiliateTotals();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}