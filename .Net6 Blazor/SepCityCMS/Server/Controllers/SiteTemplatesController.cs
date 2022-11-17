
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SiteTemplatesController : ControllerBase
    {
        [CheckOption("username", "AdminSiteLooks")]
        [Route("api/sitetemplates/apply")]
        [HttpPost]
        public Models.API.APIResponse ApplySiteTemplate([FromBody] Models.Templates Template)
        {
            Server.DAL.SiteTemplates.Template_Mark_Active(Template.TemplateID, Template.PortalID);
            var cResponse = new Models.API.APIResponse
            {
                Id = Template.TemplateID
            };
            return cResponse;
        }

        [CheckOption("username", "AdminSiteLooks")]
        [Route("api/sitetemplates/save")]
        [HttpPost]
        public Models.API.APIResponse SaveSiteTemplate([FromBody] Models.Templates Template)
        {
            Server.DAL.SiteTemplates.Template_Upload(Template.TemplateID, Server.SepFunctions.HTMLDecode(Template.HTML), Template.CSS, Template.PortalID, Template.Timestamp);
            var cResponse = new Models.API.APIResponse
            {
                Id = Template.TemplateID
            };
            return cResponse;
        }
    }
}