// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-04-2019
// ***********************************************************************
// <copyright file="UserProfilesController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon.Models;
    using System.Collections.Generic;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class UserProfilesController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UserProfilesController : ApiController
    {
        /// <summary>
        /// Deletes the profile.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>System.String.</returns>
        [Route("api/profile")]
        [HttpDelete]
        public string DeleteProfile([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("ProfilesAdmin");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.UserProfiles.Profile_Delete(SepCommon.SepCore.Strings.ToString(ID));
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.UserProfiles.</returns>
        [Route("api/profile")]
        [HttpGet]
        public UserProfiles GetProfile([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("ProfilesView");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.UserProfiles.Profile_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.UserProfiles&gt;.</returns>
        [Route("api/profiles")]
        [HttpGet]
        public List<UserProfiles> GetProfiles()
        {
            var SEP = RequestHelper.AuthorizeRequest("ProfilesAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.UserProfiles.GetUserProfiles();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Posts the profile.
        /// </summary>
        /// <param name="cred">The cred.</param>
        /// <returns>System.Int32.</returns>
        [Route("api/profile")]
        [HttpPost]
        public int PostProfile([FromBody] UserProfiles cred)
        {
            var SEP = RequestHelper.AuthorizeRequest("ProfilesModify");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            if (cred == null)
                throw RequestHelper.UnAuthorized("Cannot pass null value");
            try
            {
                var approved = true;
                if (cred.Status == 0) approved = false;
                return SepCommon.DAL.UserProfiles.Profile_Save(cred.ProfileID, cred.UserID, cred.AboutMe, cred.EnableComment, cred.HotOrNot, cred.ProfileType, cred.BGColor, cred.TextColor, cred.LinkColor, approved, cred.PortalID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Puts the profile.
        /// </summary>
        /// <param name="cred">The cred.</param>
        /// <returns>System.Int32.</returns>
        [Route("api/profile")]
        [HttpPut]
        public int PutProfile([FromBody] UserProfiles cred)
        {
            var SEP = RequestHelper.AuthorizeRequest("ProfilesModify");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var approved = true;
                if (cred.Status == 0) approved = false;
                return SepCommon.DAL.UserProfiles.Profile_Save(cred.ProfileID, cred.UserID, cred.AboutMe, cred.EnableComment, cred.HotOrNot, cred.ProfileType, cred.BGColor, cred.TextColor, cred.LinkColor, approved, cred.PortalID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}