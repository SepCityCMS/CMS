
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class FeedsController : ControllerBase
    {
        [CheckOption("username", "Everyone")]
        [HttpGet]
        public List<Models.MyFeeds> SearchFeeds()
        {
            return Server.DAL.MyFeeds.GetMyFeeds();
        }
    }
}