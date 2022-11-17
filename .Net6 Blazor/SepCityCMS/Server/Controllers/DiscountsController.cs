
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class DiscountsController : ControllerBase
    {
        [CheckOption("username", "DiscountsAccess")]
        [Route("api/discount")]
        [HttpGet]
        public Models.Discounts GetDiscount([FromQuery] long ID)
        {
            return Server.DAL.Discounts.Discount_Get(ID);
        }

        [CheckOption("username", "DiscountsAccess")]
        [Route("api/discounts")]
        [HttpGet]
        public List<Models.Discounts> GetDiscounts()
        {
            return Server.DAL.Discounts.GetDiscounts();
        }
    }
}