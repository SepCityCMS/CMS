
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class PollsController : ControllerBase
    {
        [CheckOption("username", "PollsAccess")]
        [Route("api/poll")]
        [HttpGet]
        public Models.Polls GetPoll([FromQuery] long PollID)
        {
            return Server.DAL.Polls.Poll_Get(PollID);
        }

        [CheckOption("username", "PollsAccess")]
        [Route("api/polls")]
        [HttpGet]
        public List<Models.Polls> GetPolls()
        {
            return Server.DAL.Polls.GetPolls();
        }

        [CheckOption("username", "Everyone")]
        [Route("api/polls/results")]
        [HttpGet]
        public List<Models.ChartData> GetResults([FromQuery] long PollID)
        {
            return Server.DAL.Polls.PollResults(PollID);
        }

        [CheckOption("username", "PollsVote")]
        [Route("api/polls/vote")]
        [HttpPost]
        public Models.API.APIResponse PollsVote(Models.Polls Poll)
        {
            Server.DAL.Polls.Poll_Vote(Poll.PollID, Poll.OptionId, Poll.PortalID);
            var cResponse = new Models.API.APIResponse
            {
                Id = Poll.PollID
            };
            return cResponse;
        }
    }
}