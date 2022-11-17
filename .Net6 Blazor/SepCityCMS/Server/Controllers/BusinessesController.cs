
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class BusinessesController : ControllerBase
    {
        [CheckOption("username", "BusinessAccess")]
        [Route("api/business")]
        [HttpGet]
        public Models.Businesses GetBusiness([FromQuery] long ID)
        {
            return Server.DAL.Businesses.Business_Get(ID);
        }

        [CheckOption("username", "BusinessAccess")]
        [Route("api/businesses")]
        [HttpGet]
        public List<Models.Businesses> GetBusinesses()
        {
            return Server.DAL.Businesses.GetBusinesses();
        }
    }
}