// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="PollsController.cs" company="SepCity, Inc.">
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
    /// Class PollsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class PollsController : ApiController
    {
        /// <summary>
        /// Gets the poll.
        /// </summary>
        /// <param name="PollID">The poll identifier.</param>
        /// <returns>SepCommon.Models.Polls.</returns>
        [Route("api/poll")]
        [HttpGet]
        public Polls GetPoll([FromUri] long PollID)
        {
            var SEP = RequestHelper.AuthorizeRequest("PollsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Polls.Poll_Get(PollID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the polls.
        /// </summary>
        /// <returns>List&lt;SepCommon.Models.Polls&gt;.</returns>
        [Route("api/polls")]
        [HttpGet]
        public List<Polls> GetPolls()
        {
            var SEP = RequestHelper.AuthorizeRequest("PollsAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Polls.GetPolls();
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <param name="PollID">The poll identifier.</param>
        /// <returns>List&lt;SepCommon.Models.ChartData&gt;.</returns>
        [Route("api/polls/results")]
        [HttpGet]
        public List<ChartData> GetResults([FromUri] long PollID)
        {
            // var SEP = RequestHelper.AuthorizeRequest("PollsAccess");
            // if (SEP == false)
            // throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                return SepCommon.DAL.Polls.PollResults(PollID);
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Pollses the vote.
        /// </summary>
        /// <param name="Poll">The poll.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [Route("api/polls/vote")]
        [HttpPost]
        public ResponseHelper.CreateResponse PollsVote(Polls Poll)
        {
            var SEP = RequestHelper.AuthorizeRequest("PollsVote");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                SepCommon.DAL.Polls.Poll_Vote(Poll.PollID, Poll.OptionId, Poll.PortalID);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = Poll.PollID
                };
                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}