namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class CategoriesController : ControllerBase
    {

        [CheckOption("username", "Everyone")]
        [Route("api/categories/{ModuleID:int}/{CategoryID:long}")]
        [HttpGet]
        public List<Models.Categories> GetCategories(int ModuleID, long CategoryID)
        {
            return Server.DAL.Categories.GetCategories(ModuleID, CategoryID: CategoryID);
        }
    }
}
