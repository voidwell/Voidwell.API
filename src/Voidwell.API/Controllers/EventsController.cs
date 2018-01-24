using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly IInternalClient _internalClient;

        public EventsController(IInternalClient internalClient)
        {
            _internalClient = internalClient;
        }

        [HttpGet]
        public Task<JToken> GetAllEvents()
        {
            return _internalClient.GetAllEvents();
        }

        [HttpGet("game/{gameId}")]
        public Task<JToken> GetAllEventsByGame(string gameId)
        {
            return _internalClient.GetAllEventsByGame(gameId);
        }

        [HttpGet("{eventId}")]
        public Task<JToken> GetEvent(string eventId)
        {
            return _internalClient.GetEvent(eventId);
        }

        [HttpPut("{eventId}")]
        [Authorize(Roles = "Events")]
        public Task<JToken> PutEvent(string eventId, [FromBody]JToken requestContent)
        {
            return _internalClient.UpdateEvent(eventId, requestContent);
        }

        [HttpPost]
        [Authorize(Roles = "Events")]
        public Task<JToken> PostEvent([FromBody]JToken requestContent)
        {
            return _internalClient.CreateEvent(requestContent);
        }
    }
}
