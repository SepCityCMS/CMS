// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 06-03-2020
//
// Last Modified By : spink
// Last Modified On : 06-03-2020
// ***********************************************************************
// <copyright file="VideoConferenceController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2020
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using SepCommon.Models;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class VideoConferenceController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class VideoConferenceController : ApiController
    {
        [Route("api/videoconference/scheduletimes")]
        [HttpGet]
        public VideoConfig GetScheduleTimes([FromUri] string UserID)
        {
            var SEP = RequestHelper.AuthorizeRequest("VideoConferenceCreateKeys");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.VideoConference.VideoConfig_Get(UserID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        [Route("api/videoconference/lookupuserid")]
        [HttpGet]
        public VideoConfig LookupUserID([FromUri] string UserName)
        {
            var SEP = RequestHelper.AuthorizeRequest("VideoConferenceCreateKeys");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var UserID = SepFunctions.GetUserID(UserName);
                if (!string.IsNullOrWhiteSpace(UserID))
                {
                    return SepCommon.DAL.VideoConference.VideoConfig_Get(UserID);
                }
                else
                {
                    return null;
                }
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}