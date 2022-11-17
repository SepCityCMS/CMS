
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class FAQsController : ControllerBase
    {
        [CheckOption("username", "FAQAccess")]
        [Route("api/faq")]
        [HttpGet]
        public Models.FAQs GetFAQ([FromQuery] long ID)
        {
            return Server.DAL.FAQs.FAQ_Get(ID);
        }

        [CheckOption("username", "FAQAccess")]
        [Route("api/faqs")]
        [HttpGet]
        public List<Models.FAQs> GetFAQs()
        {
            return Server.DAL.FAQs.GetFAQs();
        }
    }
}