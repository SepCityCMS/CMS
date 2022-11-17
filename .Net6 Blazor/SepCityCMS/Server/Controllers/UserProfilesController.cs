
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class UserProfilesController : ControllerBase
    {
        [CheckOption("username", "ProfilesAdmin")]
        [Route("api/profile")]
        [HttpDelete]
        public string DeleteProfile([FromQuery] long ID)
        {
            return Server.DAL.UserProfiles.Profile_Delete(Server.SepCore.Strings.ToString(ID));
        }

        [CheckOption("username", "ProfilesView")]
        [Route("api/profile")]
        [HttpGet]
        public Models.UserProfiles GetProfile([FromQuery] long ID)
        {
            return Server.DAL.UserProfiles.Profile_Get(ID);
        }

        [CheckOption("username", "ProfilesAccess")]
        [Route("api/profiles")]
        [HttpGet]
        public List<Models.UserProfiles> GetProfiles()
        {
            return Server.DAL.UserProfiles.GetUserProfiles();
        }

        [CheckOption("username", "ProfilesModify")]
        [Route("api/profile")]
        [HttpPost]
        public int PostProfile([FromBody] Models.UserProfiles cred)
        {
            var approved = true;
            if (cred.Status == 0) approved = false;
            return Server.DAL.UserProfiles.Profile_Save(cred.ProfileID, cred.UserID, cred.AboutMe, cred.EnableComment, cred.HotOrNot, cred.ProfileType, cred.BGColor, cred.TextColor, cred.LinkColor, approved, cred.PortalID);
        }

        [CheckOption("username", "ProfilesModify")]
        [Route("api/profile")]
        [HttpPut]
        public int PutProfile([FromBody] Models.UserProfiles cred)
        {
            var approved = true;
            if (cred.Status == 0) approved = false;
            return Server.DAL.UserProfiles.Profile_Save(cred.ProfileID, cred.UserID, cred.AboutMe, cred.EnableComment, cred.HotOrNot, cred.ProfileType, cred.BGColor, cred.TextColor, cred.LinkColor, approved, cred.PortalID);
        }
    }
}