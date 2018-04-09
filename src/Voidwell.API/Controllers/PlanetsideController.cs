﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("ps2")]
    public class PlanetsideController : Controller
    {
        private readonly IDaybreakGamesClient _ps2Client;

        public PlanetsideController(IDaybreakGamesClient ps2Client)
        {
            _ps2Client = ps2Client;
        }

        [HttpGet("alert")]
        public async Task<ActionResult> GetAllAlerts()
        {
            var result = await _ps2Client.GetAlerts();
            return Ok(result);
        }

        [HttpGet("alert/{worldId}")]
        public async Task<ActionResult> GetAlertsByWorldId(string worldId)
        {
            var result = await _ps2Client.GetAlertsByWorldId(worldId);
            return Ok(result);
        }

        [HttpGet("alert/{worldId}/{alertId}")]
        public async Task<ActionResult> GetAlert(string worldId, string alertId)
        {
            var result = await _ps2Client.GetAlert(worldId, alertId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}")]
        public async Task<ActionResult> GetCharacter(string characterId)
        {
            var result = await _ps2Client.GetCharacter(characterId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}/sessions")]
        public async Task<ActionResult> GetCharacterSessions(string characterId)
        {
            var result = await _ps2Client.GetCharacterSessions(characterId);
            return Ok(result);
        }

        [HttpGet("character/{characterId}/sessions/{sessionId}")]
        public async Task<ActionResult> GetCharacterSession(string characterId, string sessionId)
        {
            var result = await _ps2Client.GetCharacterSession(characterId, sessionId);
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
        public async Task<ActionResult> GetWeaponLeaderboard(string weaponItemId)
        {
            var result = await _ps2Client.GetWeaponLeaderboard(weaponItemId);
            return Ok(result);
        }

        [HttpGet("map/territory/{worldId}/{zoneId}")]
        public async Task<ActionResult> GetWorldTerritory(string worldId, string zoneId)
        {
            var result = await _ps2Client.GetWorldTerritory(worldId, zoneId);
            return Ok(result);
        }

        [HttpGet("outfit/{outfitId}")]
        public async Task<ActionResult> GetOutfit(string outfitId)
        {
            var result = await _ps2Client.GetOutfit(outfitId);
            return Ok(result);
        }

        [HttpGet("outfit/{outfitId}/members")]
        public async Task<ActionResult> GetOutfitMembers(string outfitId)
        {
            var result = await _ps2Client.GetOutfitMembers(outfitId);
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

        [HttpGet("search/{query}")]
        public async Task<ActionResult> Search(string query)
        {
            var result = await _ps2Client.Search(query);
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
            var result = await _ps2Client.GetMonitorState();
            return Ok(result);
        }

        [HttpGet("worldstate/{worldId}/players")]
        public async Task<ActionResult> GetWorldPlayers(int worldId)
        {
            var result = await _ps2Client.GetOnlinePlayers(worldId);
            return Ok(result);
        }

        [HttpGet("worldstate/{worldId}/zone/{zoneId}")]
        public async Task<ActionResult> GetWorldZoneState(int worldId, int zoneId)
        {
            var result = await _ps2Client.GetWorldZoneState(worldId, zoneId);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("worldstate/{worldId}/zone")]
        public Task<JToken> PostSetupWorldZoneStates(int worldId)
        {
            return _ps2Client.SetupWorldZoneStates(worldId);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("services/status")]
        public Task<JToken> GetAllServiceStatus()
        {
            return _ps2Client.GetServiceStates();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("services/{service}/status")]
        public Task<JToken> GetServiceStatus(string service)
        {
            return _ps2Client.GetServiceState(service);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("services/{service}/enable")]
        public Task<JToken> PostEnableService(string service)
        {
            return _ps2Client.EnableService(service);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("services/{service}/disable")]
        public Task<JToken> PostDisableService(string service)
        {
            return _ps2Client.DisableService(service);
        }

        [Authorize(Roles = "Administrator,PSB")]
        [HttpGet("psb/sessions")]
        public Task<JToken> GetLastOnlinePSBAccounts()
        {
            return _ps2Client.GetLastOnlinePSBAccounts();
        }
    }
}
