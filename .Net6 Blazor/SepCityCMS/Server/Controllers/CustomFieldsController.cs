
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CustomFieldsController : ControllerBase
    {
        [CheckOption("username", "Everyone")]
        [Route("api/customfields/answers")]
        [HttpPost]
        public Models.API.APIResponse GetAnswers([FromBody] Models.CustomFieldsAnswers Answer)
        {
            Server.DAL.CustomFields.Answers_Save(Answer.UserID, Answer.ModuleID, Answer.UniqueID, Answer.ProductID, Answer.CustomData);
            var cResponse = new Models.API.APIResponse
            {
                Id = Answer.UniqueID
            };
            return cResponse;
        }

        [CheckOption("username", "Everyone")]
        [Route("api/customfields")]
        [HttpGet]
        public List<Models.CustomFields> GetCustomFields([FromQuery] int ModuleID)
        {
            return Server.DAL.CustomFields.GetCustomFields(ModuleID: ModuleID);
        }
    }
}