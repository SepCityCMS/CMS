
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class BlogsController : ControllerBase
    {
        [CheckOption("username", "BlogsAccess")]
        [Route("api/blog")]
        [HttpGet]
        public Models.Blogs GetBlog([FromQuery] long ID)
        {
            return Server.DAL.Blogs.Blog_Get(ID);
        }

        [CheckOption("username", "BlogsAccess")]
        [Route("api/blogs")]
        [HttpGet]
        public List<Models.Blogs> GetBlogs()
        {
            return Server.DAL.Blogs.GetBlogs();
        }
    }
}