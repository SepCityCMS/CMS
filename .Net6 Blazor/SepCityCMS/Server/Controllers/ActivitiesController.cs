
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class ActivitiesController : ControllerBase
    {
        [CheckOption("username", "AdminActivities")]
        [Route("api/activities")]
        [HttpGet]
        public List<Models.Activities> GetActivities()
        {
            return Server.DAL.Activities.GetActivities();
        }

        [CheckOption("username", "AdminActivities")]
        [Route("api/activities")]
        [HttpPost]
        public Models.API.APIResponse PostActivities([FromBody] Models.Activities Activity)
        {
            Models.API.APIResponse cResponse = new Models.API.APIResponse();
            try
            {
                var ID = Activity.ActivityID;
                cResponse.Message = Server.DAL.Activities.ActivitySave(ref ID, Activity.UserID, Activity.ActType, Activity.Description);
                cResponse.Id = ID;
                cResponse.Success = true;
            }
            catch (Exception ex)
            {
                cResponse.Message = ex.Message;
            }
            return cResponse;
        }

        [CheckOption("username", "AdminActivities")]
        [Route("api/activities")]
        [HttpPut]
        public Models.API.APIResponse PutActivities([FromQuery] long ID, [FromBody] Models.Activities Activity)
        {
            Models.API.APIResponse cResponse = new Models.API.APIResponse();
            try
            {
                cResponse.Message = Server.DAL.Activities.ActivitySave(ref ID, Activity.UserID, Activity.ActType, Activity.Description);
                cResponse.Id = ID;
                cResponse.Success = true;
            }
            catch (Exception ex)
            {
                cResponse.Message = ex.Message;
            }
            return cResponse;
        }

        [CheckOption("username", "AdminActivities")]
        [Route("api/activities")]
        [HttpPut]
        public Models.API.APIResponse DeleteActivity([FromQuery] long ID)
        {
            var cResponse = new Models.API.APIResponse();

            try
            {
                cResponse.Message = Server.DAL.Activities.ActivityDelete(Server.SepCore.Strings.ToString(ID));
                cResponse.Id = ID;
                cResponse.Success = true;
            }
            catch (Exception ex)
            {
                cResponse.Message = ex.Message;
            }

            return cResponse;
        }

        [CheckOption("username", "AdminActivities")]
        [Route("api/activities")]
        [HttpGet]
        public Models.Activities GetActivity([FromQuery] long ID)
        {
            return Server.DAL.Activities.ActivityGet(ID);
        }

        [CheckOption("username", "AdminActivities")]
        [Route("api/activities/monthlyactivities")]
        [HttpGet]
        public List<Models.ChartData> MonthlyActivities()
        {
            return Server.DAL.Activities.MonthlyActivities();
        }
    }
}