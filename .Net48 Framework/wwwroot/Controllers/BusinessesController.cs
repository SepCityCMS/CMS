// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="BusinessesController.cs" company="SepCity, Inc.">
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
    /// Class BusinessesController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BusinessesController : ApiController
    {
        /// <summary>
        /// Gets the business.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.Businesses.</returns>
        [Route("api/business")]
        [HttpGet]
        public Businesses GetBusiness([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("BusinessAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Businesses.Business_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the businesses.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Businesses&gt;.</returns>
        [Route("api/businesses")]
        [HttpGet]
        public List<Businesses> GetBusinesses()
        {
            var SEP = RequestHelper.AuthorizeRequest("BusinessAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Businesses.GetBusinesses();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}