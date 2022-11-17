
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class LinkDirectoryController : ControllerBase
    {
        [CheckOption("username", "LinksAccess")]
        [Route("api/link")]
        [HttpGet]
        public Models.LinksWebsite GetLink([FromQuery] long ID)
        {
            return Server.DAL.LinkDirectory.Website_Get(ID);
        }

        [CheckOption("username", "LinksAccess")]
        [Route("api/links")]
        [HttpGet]
        public List<Models.LinksWebsite> GetLinks()
        {
            return Server.DAL.LinkDirectory.GetLinksWebsite();
        }
    }
}