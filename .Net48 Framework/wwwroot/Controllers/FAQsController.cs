// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="FAQsController.cs" company="SepCity, Inc.">
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
    /// Class FAQsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class FAQsController : ApiController
    {
        /// <summary>
        /// Gets the FAQ.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.FAQs.</returns>
        [Route("api/faq")]
        [HttpGet]
        public FAQs GetFAQ([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("FAQAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.FAQs.FAQ_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the fa qs.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.FAQs&gt;.</returns>
        [Route("api/faqs")]
        [HttpGet]
        public List<FAQs> GetFAQs()
        {
            var SEP = RequestHelper.AuthorizeRequest("FAQAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.FAQs.GetFAQs();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}