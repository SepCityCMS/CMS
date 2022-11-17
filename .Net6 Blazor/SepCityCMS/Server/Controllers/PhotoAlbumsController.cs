
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class PhotoAlbumsController : ControllerBase
    {
        [CheckOption("username", "PhotosAccess")]
        [Route("api/album")]
        [HttpGet]
        public Models.PhotoAlbums GetAlbum([FromQuery] long ID)
        {
            return Server.DAL.PhotoAlbums.Album_Get(ID);
        }

        [CheckOption("username", "PhotosAccess")]
        [Route("api/albums")]
        [HttpGet]
        public List<Models.PhotoAlbums> GetAlbums()
        {
            return Server.DAL.PhotoAlbums.GetPhotoAlbums();
        }
    }
}