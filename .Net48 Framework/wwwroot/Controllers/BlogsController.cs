// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="BlogsController.cs" company="SepCity, Inc.">
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
    /// Class BlogsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BlogsController : ApiController
    {
        /// <summary>
        /// Gets the blog.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.Blogs.</returns>
        [Route("api/blog")]
        [HttpGet]
        public Blogs GetBlog([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("BlogsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Blogs.Blog_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the blogs.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Blogs&gt;.</returns>
        [Route("api/blogs")]
        [HttpGet]
        public List<Blogs> GetBlogs()
        {
            var SEP = RequestHelper.AuthorizeRequest("BlogsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Blogs.GetBlogs();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}