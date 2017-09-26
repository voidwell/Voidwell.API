using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Voidwell.API.Clients
{
    public class PlanetsideClient : IPlanetsideClient
    {
        private readonly HttpClient _httpClientCore;
        private readonly HttpClient _httpClientCoreLive;

        public PlanetsideClient()
        {
            _httpClientCore = new HttpClient();
            _httpClientCoreLive = new HttpClient();

            _httpClientCore.BaseAddress = new Uri(Constants.Endpoints.PlanetsideCore);
            _httpClientCoreLive.BaseAddress = new Uri(Constants.Endpoints.PlanetsideCoreLive);
        }

        // Core

        public async Task<JToken> GetPlanetside2News()
        {
            var response = await _httpClientCore.GetAsync("feeds/news");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetPlanetside2Updates()
        {
            var response = await _httpClientCore.GetAsync("feeds/updates");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> Search(string query)
        {
            var response = await _httpClientCore.GetAsync($"search/{query}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllPlayableClasses()
        {
            var response = await _httpClientCore.GetAsync("profile");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllVehicles()
        {
            var response = await _httpClientCore.GetAsync("vehicle");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacter(string characterId)
        {
            var response = await _httpClientCore.GetAsync($"character/{characterId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOutfit(string outfitId)
        {
            var response = await _httpClientCore.GetAsync($"outfit/{outfitId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOutfitMembers(string outfitId)
        {
            var response = await _httpClientCore.GetAsync($"outfit/{outfitId}/members");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWeaponInfo(string weaponItemId)
        {
            var response = await _httpClientCore.GetAsync($"weaponinfo/{weaponItemId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWeaponLeaderboard(string weaponItemId)
        {
            var response = await _httpClientCore.GetAsync($"leaderboard/weapon/{weaponItemId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetGrades()
        {
            var response = await _httpClientCore.GetAsync("grades");
            return await response.GetContentAsync<JToken>();
        }

        // CoreLive

        public async Task<JToken> GetAlerts()
        {
            var response = await _httpClientCoreLive.GetAsync("alert");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAlertsByWorldId(string worldId)
        {
            var response = await _httpClientCoreLive.GetAsync($"alert/{worldId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAlert(string worldId, string alertId)
        {
            var response = await _httpClientCoreLive.GetAsync($"alert/{worldId}/{alertId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterSessions(string characterId)
        {
            var response = await _httpClientCoreLive.GetAsync($"character/{characterId}/sessions");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterSession(string characterId, string sessionId)
        {
            var response = await _httpClientCoreLive.GetAsync($"character/{characterId}/sessions/{sessionId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWorldTerritory(string worldId, string zoneId)
        {
            var response = await _httpClientCoreLive.GetAsync($"map/territory/{worldId}/{zoneId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetMonitorState()
        {
            var response = await _httpClientCoreLive.GetAsync("worldState");
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClientCore.Dispose();
            _httpClientCoreLive.Dispose();
        }
    }
}
