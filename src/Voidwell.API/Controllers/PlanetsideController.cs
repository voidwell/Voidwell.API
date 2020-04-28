using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;
using Voidwell.API.Models;

namespace Voidwell.API.Controllers
{
    [Route("ps2")]
    public class PlanetsideController : Controller
    {
        private readonly IDaybreakGamesClient _ps2Client;
        private readonly IDaybreakGamesPS4USClient _ps2Ps4UsClient;
        private readonly IDaybreakGamesPS4EUClient _ps2Ps4EuClient;

        public PlanetsideController(IDaybreakGamesClient ps2Client, IDaybreakGamesPS4USClient ps2Ps4UsClient, IDaybreakGamesPS4EUClient ps2Ps4EuClient)
        {
            _ps2Client = ps2Client;
            _ps2Ps4UsClient = ps2Ps4UsClient;
            _ps2Ps4EuClient = ps2Ps4EuClient;
        }

        [HttpGet("alert/alerts/{pageNumber}")]
        public async Task<ActionResult> GetAlerts(int pageNumber, [FromQuery]int? worldId)
        {
            var result = await GetClient().GetAlerts(pageNumber, worldId);
            return Ok(result);
        }

        [HttpGet("alert/{worldId}/{alertId}")]
        public async Task<ActionResult> GetAlert(string worldId, string alertId)
        {
            var result = await GetClient().GetAlert(worldId, alertId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}")]
        public async Task<ActionResult> GetCharacter(string characterId)
        {
            var result = await GetClient().GetCharacter(characterId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}/sessions")]
        public async Task<ActionResult> GetCharacterSessions(string characterId, [FromQuery]int page = 0)
        {
            var result = await GetClient().GetCharacterSessions(characterId, page);
            return Ok(result);
        }

        [HttpGet("character/{characterId}/sessions/{sessionId}")]
        public async Task<ActionResult> GetCharacterSession(string characterId, string sessionId)
        {
            var result = await GetClient().GetCharacterSession(characterId, sessionId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}/sessions/live")]
        public async Task<ActionResult> GetCharacterLiveSessions(string characterId)
        {
            var result = await GetClient().GetCharacterLiveSession(characterId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}/state")]
        public async Task<ActionResult> GetCharacterOnlineState(string characterId)
        {
            var result = await GetClient().GetCharacterOnlineState(characterId);
            return Ok(result);
        }

        [HttpPost("character/byname")]
        public async Task<ActionResult> GetMultipleCharacterStatsByName([FromBody] IEnumerable<string> characterNames)
        {
            var result = await GetClient().GetMultipleCharacterStatsByName(characterNames);
            return Ok(result);
        }

        [HttpGet("feeds/news")]
        public async Task<ActionResult> GetPlanetside2News()
        {
            var result = await _ps2Client.GetPlanetside2News();
            return Ok(result);
        }

        [HttpGet("feeds/updates")]
        public async Task<ActionResult> GetPlanetside2Updates()
        {
            var result = await _ps2Client.GetPlanetside2Updates();
            return Ok(result);
        }

        [HttpGet("grades")]
        public async Task<ActionResult> GetGrades()
        {
            var result = await _ps2Client.GetGrades();
            return Ok(result);
        }

        [HttpGet("leaderboard/weapon/{weaponItemId}")]
        public async Task<ActionResult> GetWeaponLeaderboard(string weaponItemId, [FromQuery]int page = 0)
        {
            var result = await GetClient().GetWeaponLeaderboard(weaponItemId, page);
            return Ok(result);
        }

        [HttpGet("map/territory/{worldId}/{zoneId}")]
        public async Task<ActionResult> GetWorldTerritory(string worldId, string zoneId)
        {
            var result = await GetClient().GetWorldTerritory(worldId, zoneId);
            return Ok(result);
        }

        [HttpGet("map/population/{worldId}/{zoneId}")]
        public async Task<ActionResult> GetWorldPopulation(string worldId, string zoneId)
        {
            var result = await GetClient().GetWorldPopulation(worldId, zoneId);
            return Ok(result);
        }

        [HttpGet("outfit/{outfitId}")]
        public async Task<ActionResult> GetOutfit(string outfitId)
        {
            var result = await GetClient().GetOutfit(outfitId);
            return Ok(result);
        }

        [HttpGet("outfit/{outfitId}/members")]
        public async Task<ActionResult> GetOutfitMembers(string outfitId)
        {
            var result = await GetClient().GetOutfitMembers(outfitId);
            return Ok(result);
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetAllPlayableClasses()
        {
            var result = await _ps2Client.GetAllPlayableClasses();
            return Ok(result);
        }

        [HttpGet("vehicle")]
        public async Task<ActionResult> GetAllVehicles()
        {
            var result = await _ps2Client.GetAllVehicles();
            return Ok(result);
        }

        [HttpGet("world")]
        public async Task<ActionResult> GetAllWorlds()
        {
            var result = await GetClient().GetAllWorlds();
            return Ok(result);
        }

        [HttpGet("world/population")]
        public Task<JToken> GetWorldPopulationHistory([FromQuery(Name = "q")]string worldIds)
        {
            return GetClient().GetWorldPopulationHistory(worldIds.Split(","));
        }

        [HttpGet("world/activity")]
        public Task<JToken> GetWorldActivity([FromQuery(Name = "worldId")]int? worldId, [FromQuery(Name = "period")]int? period)
        {
            return GetClient().GetWorldActivity(worldId, period);
        }

        [HttpGet("zone")]
        public async Task<ActionResult> GetAllZones()
        {
            var result = await _ps2Client.GetAllZones();
            return Ok(result);
        }

        [HttpGet("search/{category}/{query}")]
        public async Task<ActionResult> Search(string category, string query)
        {
            var result = await GetClient().Search(category, query);
            return Ok(result);
        }

        [HttpGet("weaponinfo/{weaponItemId}")]
        public async Task<ActionResult> GetWeaponInfo(string weaponItemId)
        {
            var result = await _ps2Client.GetWeaponInfo(weaponItemId);
            return Ok(result);
        }

        [HttpGet("worldstate")]
        public async Task<ActionResult> GetMonitorState()
        {
            var result = await GetClient().GetMonitorState();
            return Ok(result);
        }

        [HttpGet("worldstate/{worldId}")]
        public async Task<ActionResult> GetWorldState(int worldId)
        {
            var result = await GetClient().GetWorldState(worldId);
            return Ok(result);
        }

        [HttpGet("worldstate/{worldId}/players")]
        public async Task<ActionResult> GetWorldPlayers(int worldId)
        {
            var result = await GetClient().GetOnlinePlayers(worldId);
            return Ok(result);
        }

        [HttpGet("worldstate/{worldId}/{zoneId}/map")]
        public async Task<ActionResult> GetWorldZoneState(int worldId, int zoneId)
        {
            var result = await GetClient().GetZoneOwnership(worldId, zoneId);
            return Ok(result);
        }

        [HttpGet("map/{zoneId}")]
        public async Task<ActionResult> GetZoneMap(int zoneId)
        {
            var result = await _ps2Client.GetZoneMap(zoneId);
            return Ok(result);
        }

        [HttpGet("oracle/category/{categoryId}")]
        public Task<JToken> GetOracleCategory(string categoryId)
        {
            return _ps2Client.GetOracleCategory(categoryId);
        }

        [HttpGet("oracle/stats/{statId}")]
        public Task<JToken> GetOracleStats(string statId, [FromQuery(Name = "q")]string weaponIds)
        {
            return GetClient().GetOracleStats(statId, weaponIds.Split(","));
        }

        [HttpGet("ranks")]
        public Task<JToken> GetPlayerRankings()
        {
            return GetClient().GetPlayerRankings();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("worldstate/{worldId}/zone")]
        public Task<JToken> PostSetupWorldZoneStates(int worldId)
        {
            return GetClient().SetupWorldZoneStates(worldId);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("services/status")]
        public async Task<IEnumerable<ServiceState>> GetAllServiceStatus()
        {
            var results = await Task.WhenAll(_ps2Client.GetServiceStates(), _ps2Ps4UsClient.GetServiceStates(), _ps2Ps4EuClient.GetServiceStates());
            return results.SelectMany(a => a);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("services/{service}/status")]
        public Task<ServiceState> GetServiceStatus(string service)
        {
            return GetClient().GetServiceState(service);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("services/{service}/enable")]
        public Task<ServiceState> PostEnableService(string service)
        {
            return GetClient().EnableService(service);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("services/{service}/disable")]
        public Task<ServiceState> PostDisableService(string service)
        {
            return GetClient().DisableService(service);
        }

        [Authorize(Roles = "Administrator,PSB")]
        [HttpGet("psb/sessions")]
        public Task<JToken> GetLastOnlinePSBAccounts()
        {
            return _ps2Client.GetLastOnlinePSBAccounts();
        }

        [Authorize(Constants.Policies.Mutterblack)]
        [HttpGet("character/byname/{characterName}")]
        public Task<JToken> GetCharacterStatsByName(string characterName)
        {
            return GetClient().GetCharacterStatsByName(characterName);
        }

        [Authorize(Constants.Policies.Mutterblack)]
        [HttpGet("character/byname/{characterName}/weapon/{weaponName}")]
        public Task<JToken> GetCharacterStatsByName(string characterName, string weaponName)
        {
            return GetClient().GetCharacterWeaponStatsByName(characterName, weaponName);
        }

        [Authorize(Constants.Policies.Mutterblack)]
        [HttpGet("outfit/byalias/{outfitAlias}")]
        public Task<JToken> GetOutfitStatsByAlias(string outfitAlias)
        {
            return GetClient().GetOutfitStatsByAlias(outfitAlias);
        }

        [Authorize(Constants.Policies.Mutterblack)]
        [HttpGet("weaponinfo/byname/{weaponName}")]
        public Task<JToken> GetWeaponInfoByName(string weaponName)
        {
            return GetClient().GetWeaponInfoByName(weaponName);
        }

        private IDaybreakGamesClient GetClient()
        {
            if (HttpContext.Request.Query.TryGetValue("platform", out var platform))
            {
                switch (platform)
                {
                    case "pc":
                        return _ps2Client;
                    case "ps4us":
                        return _ps2Ps4UsClient;
                    case "ps4eu":
                        return _ps2Ps4EuClient;
                }
            }

            return _ps2Client;
        }
    }
}
