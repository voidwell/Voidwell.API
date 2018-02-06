using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class PlanetsideClient : IPlanetsideClient
    {
        private readonly HttpClient _httpClient;

        public PlanetsideClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
        {
            _httpClient = authenticatedHttpClientFactory.GetHttpClient();
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.DaybreakGames);
        }

        // Core

        public async Task<JToken> GetPlanetside2News()
        {
            var response = await _httpClient.GetAsync("ps2/feeds/news");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetPlanetside2Updates()
        {
            var response = await _httpClient.GetAsync("ps2/feeds/updates");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> Search(string query)
        {
            var response = await _httpClient.GetAsync($"ps2/search/{query}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllPlayableClasses()
        {
            var response = await _httpClient.GetAsync("ps2/profile");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllVehicles()
        {
            var response = await _httpClient.GetAsync("ps2/vehicle");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacter(string characterId)
        {
            var response = await _httpClient.GetAsync($"ps2/character/{characterId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOutfit(string outfitId)
        {
            var response = await _httpClient.GetAsync($"ps2/outfit/{outfitId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOutfitMembers(string outfitId)
        {
            var response = await _httpClient.GetAsync($"ps2/outfit/{outfitId}/members");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWeaponInfo(string weaponItemId)
        {
            var response = await _httpClient.GetAsync($"ps2/weaponInfo/{weaponItemId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWeaponLeaderboard(string weaponItemId)
        {
            var response = await _httpClient.GetAsync($"ps2/leaderboard/weapon/{weaponItemId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetGrades()
        {
            var response = await _httpClient.GetAsync("ps2/grades");
            return await response.GetContentAsync<JToken>();
        }

        // CoreLive

        public async Task<JToken> GetAlerts()
        {
            var response = await _httpClient.GetAsync("ps2/alert");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAlertsByWorldId(string worldId)
        {
            var response = await _httpClient.GetAsync($"ps2/alert/{worldId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAlert(string worldId, string alertId)
        {
            var response = await _httpClient.GetAsync($"ps2/alert/{worldId}/{alertId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterSessions(string characterId)
        {
            var response = await _httpClient.GetAsync($"ps2/character/{characterId}/sessions");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterSession(string characterId, string sessionId)
        {
            var response = await _httpClient.GetAsync($"ps2/character/{characterId}/sessions/{sessionId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWorldTerritory(string worldId, string zoneId)
        {
            var response = await _httpClient.GetAsync($"map/territory/{worldId}/{zoneId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetMonitorState()
        {
            var response = await _httpClient.GetAsync("worldState");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOnlinePlayers(int worldId)
        {
            var response = await _httpClient.GetAsync($"worldState/{worldId}/players");
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
