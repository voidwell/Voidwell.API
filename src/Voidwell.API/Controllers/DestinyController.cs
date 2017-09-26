using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;
using Voidwell.API.Models;

namespace Voidwell.API.Controllers
{
    [Route("destiny2")]
    public class DestinyController : Controller
    {
        private readonly IBungieNetClient _bNetClient;

        public DestinyController(IBungieNetClient bNetClient)
        {
            _bNetClient = bNetClient;
        }

        [HttpGet("{platform:PlatformType}/{membershipId}/character/{characterId}")]
        public async Task<ActionResult> GetCharacter(PlatformType platform, string membershipId, string characterId)
        {
            var result = await _bNetClient.GetCharacter(platform, membershipId, characterId);
            return Ok(result);
        }

        [HttpGet("{platform:PlatformType}/{membershipId}/character/{characterId}/inventory")]
        public async Task<ActionResult> GetCharacterInventory(PlatformType platform, string membershipId, string characterId)
        {
            var result = await _bNetClient.GetCharacterInventory(platform, membershipId, characterId);
            return Ok(result);
        }

        [HttpGet("platform:PlatformType}/{membershipId}/inventory")]
        public async Task<ActionResult> GetInventory(PlatformType platform, string membershipId)
        {
            var result = await _bNetClient.GetInventory(platform, membershipId);
            return Ok(result);
        }

        [HttpGet("{platform:PlatformType}/{membershipId}/item/{itemInstanceId}")]
        public async Task<ActionResult> GetItem(PlatformType platform, string membershipId, string itemInstanceId)
        {
            var result = await _bNetClient.GetItem(platform, membershipId, itemInstanceId);
            return Ok(result);
        }

        [HttpGet("postgamereport/{activityId}")]
        public async Task<ActionResult> GetPostgameReport(string activityId)
        {
            var result = await _bNetClient.GetPostgameReport(activityId);
            return Ok(result);
        }

        [HttpGet("{platform:PlatformType}/{membershipId}/profile")]
        public async Task<ActionResult> GetProfile(PlatformType platform, string membershipId)
        {
            var result = await _bNetClient.GetProfile(platform, membershipId);
            return Ok(result);
        }

        [HttpGet("milestone")]
        public async Task<ActionResult> GetMilestone()
        {
            var result = await _bNetClient.GetMilestone();
            return Ok(result);
        }

        [HttpGet("milestone/content/{milestoneHash}")]
        public async Task<ActionResult> GetMilestoneContent(string milestoneHash)
        {
            var result = await _bNetClient.GetMilestoneContent(milestoneHash);
            return Ok(result);
        }

        [HttpGet("{platform:PlatformType}/search/{displayName}")]
        public async Task<ActionResult> SearchBNet(PlatformType platform, string displayName)
        {
            var result = await _bNetClient.Search(platform, displayName);
            return Ok(result);
        }

        [HttpPost("transferitem")]
        public async Task<ActionResult> TransferItem([FromBody]JToken request)
        {
            var result = await _bNetClient.TransferItem(request);
            return Ok(result);
        }

        [HttpPost("equipitem")]
        public async Task<ActionResult> EquipItem([FromBody]JToken request)
        {
            var result = await _bNetClient.EquipItem(request);
            return Ok(result);
        }

        [HttpPost("equipitems")]
        public async Task<ActionResult> EquipItems([FromBody]JToken request)
        {
            var result = await _bNetClient.EquipItems(request);
            return Ok(result);
        }

        [HttpPost("lock")]
        public async Task<ActionResult> LockItem([FromBody]JToken request)
        {
            var result = await _bNetClient.LockItem(request);
            return Ok(result);
        }

        [HttpPost("unlock")]
        public async Task<ActionResult> UnlockItem([FromBody]JToken request)
        {
            var result = await _bNetClient.UnlockItem(request);
            return Ok(result);
        }
    }
}
