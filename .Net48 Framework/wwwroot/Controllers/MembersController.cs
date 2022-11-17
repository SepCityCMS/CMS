// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="MembersController.cs" company="SepCity, Inc.">
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
    /// Class MembersController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class MembersController : ApiController
    {
        /// <summary>
        /// Dailies the signups.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.ChartData&gt;.</returns>
        [Route("api/members/dailysignups")]
        [HttpGet]
        public List<ChartData> DailySignups()
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminUserMan");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Members.DailySignups();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Monthlies the signups.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.ChartData&gt;.</returns>
        [Route("api/members/monthlysignups")]
        [HttpGet]
        public List<ChartData> MonthlySignups()
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminUserMan");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Members.MonthlySignups();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}