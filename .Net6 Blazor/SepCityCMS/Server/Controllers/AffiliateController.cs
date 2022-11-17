
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class AffiliateController : ControllerBase
    {
        [CheckOption("username", "AffiliateJoin")]
        [Route("api/affiliate/affiliatetotals")]
        [HttpGet]
        public List<Models.AffiliateDownline> AffiliateTotals()
        {
            return Server.DAL.Affiliate.AffiliateTotals();
        }
    }
}