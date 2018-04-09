using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Voidwell.API.Clients
{
    [Route("oidcadmin")]
    [Authorize(Roles = "Administrator")]
    public class OidcAdminController : Controller
    {
        private readonly IAuthClient _authClient;
        private readonly ILogger<OidcAdminController> _logger;

        public OidcAdminController(IAuthClient authClient, ILogger<OidcAdminController> logger)
        {
            _authClient = authClient;
            _logger = logger;
        }

        [HttpGet("client")]
        public async Task<ActionResult> GetAllClients()
        {
            var result = await _authClient.GetAllClients();
            return Ok(result);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult> GetClientById(string clientId)
        {
            var result = await _authClient.GetClientById(clientId);
            return Ok(result);
        }

        [HttpPost("client")]
        public async Task<ActionResult> CreateClient([FromBody]JToken payload)
        {
            var result = await _authClient.CreateClient(payload);
            return Ok(result);
        }

        [HttpPut("client/{clientId}")]
        public async Task<ActionResult> UpdateClient(string clientId, [FromBody]JToken payload)
        {
            var result = await _authClient.UpdateClient(clientId, payload);
            return Ok(result);
        }

        [HttpPost("client/{clientId}/secret")]
        public async Task<ActionResult> CreateClientSecret(string clientId, [FromBody]JToken payload)
        {
            var result = await _authClient.CreateClientSecret(clientId, payload);
            return Ok(result);
        }

        [HttpDelete("client/{clientId}/secret")]
        public async Task<ActionResult> DeleteClientSecret(string clientId)
        {
            await _authClient.DeleteClientSecret(clientId, Request.QueryString.Value);
            return NoContent();
        }

        [HttpGet("resource")]
        public async Task<ActionResult> GetAllApiResources()
        {
            var result = await _authClient.GetAllApiResources();
            return Ok(result);
        }

        [HttpGet("resource/{apiResourceId}")]
        public async Task<ActionResult> GetApiResourceById(string apiResourceId)
        {
            var result = await _authClient.GetApiResourceById(apiResourceId);
            return Ok(result);
        }

        [HttpPost("resource")]
        public async Task<ActionResult> CreateApiResource([FromBody]JToken payload)
        {
            var result = await _authClient.CreateApiResource(payload);
            return Ok(result);
        }

        [HttpPut("resource/{apiResourceId}")]
        public async Task<ActionResult> UpdateApiResource(string apiResourceId, [FromBody]JToken payload)
        {
            var result = await _authClient.UpdateApiResource(apiResourceId, payload);
            return Ok(result);
        }

        [HttpPost("resource/{apiResourceId}/secret")]
        public async Task<ActionResult> CreateApiResourceSecret(string apiResourceId, [FromBody]JToken payload)
        {
            var result = await _authClient.CreateApiResourceSecret(apiResourceId, payload);
            return Ok(result);
        }

        [HttpDelete("resource/{apiResourceId}/secret")]
        public async Task<ActionResult> DeleteApiResourceSecret(string apiResourceId)
        {
            await _authClient.DeleteApiResourceSecret(apiResourceId, Request.QueryString.Value);
            return NoContent();
        }
    }
}
