// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="EventsController.cs" company="SepCity, Inc.">
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
    /// Class EventsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EventsController : ApiController
    {
        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>SepCommon.Models.Events.</returns>
        [Route("api/event")]
        [HttpGet]
        public Events GetEvent([FromUri] long ID)
        {
            var SEP = RequestHelper.AuthorizeRequest("EventsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Events.Event_Get(ID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Events&gt;.</returns>
        [Route("api/events")]
        [HttpGet]
        public List<Events> GetEvents()
        {
            var SEP = RequestHelper.AuthorizeRequest("EventsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Events.GetEvents();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}