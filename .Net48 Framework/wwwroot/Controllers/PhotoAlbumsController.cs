// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="PhotoAlbumsController.cs" company="SepCity, Inc.">
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
    /// Class PhotoAlbumsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class PhotoAlbumsController : ApiController
    {
        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.PhotoAlbums.</returns>
        [Route("api/album")]
        [HttpGet]
        public PhotoAlbums GetAlbum([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("PhotosAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.PhotoAlbums.Album_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the albums.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.PhotoAlbums&gt;.</returns>
        [Route("api/albums")]
        [HttpGet]
        public List<PhotoAlbums> GetAlbums()
        {
            var SEP = RequestHelper.AuthorizeRequest("PhotosAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.PhotoAlbums.GetPhotoAlbums();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}