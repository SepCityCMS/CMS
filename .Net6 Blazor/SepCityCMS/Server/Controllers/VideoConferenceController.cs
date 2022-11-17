
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;

    public class VideoConferenceController : ControllerBase
    {
        [CheckOption("username", "VideoConferenceCreateKeys")]
        [Route("api/videoconference/scheduletimes")]
        [HttpGet]
        public Models.VideoConfig GetScheduleTimes([FromQuery] string UserID)
        {
            return Server.DAL.VideoConference.VideoConfig_Get(UserID);
        }

        [CheckOption("username", "VideoConferenceCreateKeys")]
        [Route("api/videoconference/lookupuserid")]
        [HttpGet]
        public Models.VideoConfig LookupUserID([FromQuery] string UserName)
        {
            var UserID = SepFunctions.GetUserID(UserName);
            if (!string.IsNullOrWhiteSpace(UserID))
            {
                return Server.DAL.VideoConference.VideoConfig_Get(UserID);
            }
            else
            {
                return null;
            }
        }
    }
}