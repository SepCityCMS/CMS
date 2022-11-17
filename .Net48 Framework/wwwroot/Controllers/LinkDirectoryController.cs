// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="LinkDirectoryController.cs" company="SepCity, Inc.">
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
    /// Class LinkDirectoryController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class LinkDirectoryController : ApiController
    {
        /// <summary>
        /// Gets the link.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.LinksWebsite.</returns>
        [Route("api/link")]
        [HttpGet]
        public LinksWebsite GetLink([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("LinksAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.LinkDirectory.Website_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.LinksWebsite&gt;.</returns>
        [Route("api/links")]
        [HttpGet]
        public List<LinksWebsite> GetLinks()
        {
            var SEP = RequestHelper.AuthorizeRequest("LinksAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.LinkDirectory.GetLinksWebsite();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}