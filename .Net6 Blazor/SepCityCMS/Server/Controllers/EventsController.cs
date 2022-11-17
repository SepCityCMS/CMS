
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class EventsController : ControllerBase
    {
        [CheckOption("username", "EventsAccess")]
        [Route("api/event")]
        [HttpGet]
        public Models.Events GetEvent([FromQuery] long ID)
        {
            return Server.DAL.Events.Event_Get(ID);
        }

        [CheckOption("username", "EventsAccess")]
        [Route("api/events")]
        [HttpGet]
        public List<Models.Events> GetEvents()
        {
            return Server.DAL.Events.GetEvents();
        }
    }
}