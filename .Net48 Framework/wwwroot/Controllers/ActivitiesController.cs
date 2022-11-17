// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="ActivitiesController.cs" company="SepCity, Inc.">
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
    /// Class ActivitiesController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ActivitiesController : ApiController
    {
        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Activities&gt;.</returns>
        [Route("api/activities")]
        [HttpGet]
        public List<Activities> GetActivities()
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminActivities");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Activities.GetActivities();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        [Route("api/activities")]
        [HttpPost]
        public string PostActivities([FromBody] SepCommon.Models.Activities Activity)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminActivities");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Activities.ActivitySave(Activity.ActivityID, Activity.UserID, Activity.ActType, Activity.Description);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        [Route("api/activities")]
        [HttpPut]
        public string PutActivities([FromUri] long ID, [FromBody] SepCommon.Models.Activities Activity)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminActivities");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Activities.ActivitySave(ID, Activity.UserID, Activity.ActType, Activity.Description);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.Activities.</returns>
        [Route("api/activity")]
        [HttpGet]
        public Activities GetActivity([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminActivities");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Activities.ActivityGet(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Monthlies the activities.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.ChartData&gt;.</returns>
        [Route("api/activities/monthlyactivities")]
        [HttpGet]
        public List<ChartData> MonthlyActivities()
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminActivities");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Activities.MonthlyActivities();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}