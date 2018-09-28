using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class DaybreakGamesClient : IDaybreakGamesClient
    {
        private readonly HttpClient _httpClient;

        public DaybreakGamesClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
        {
            _httpClient = authenticatedHttpClientFactory.GetHttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.DaybreakGames);
        }

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

        public async Task<JToken> GetAlerts(int pageNumber, int? worldId)
        {
            var response = await _httpClient.GetAsync($"ps2/alert/alerts/{pageNumber}?worldId={worldId}");
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
            var response = await _httpClient.GetAsync($"ps2/map/territory/{worldId}/{zoneId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetWorldPopulation(string worldId, string zoneId)
        {
            var response = await _httpClient.GetAsync($"ps2/map/population/{worldId}/{zoneId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetMonitorState()
        {
            var response = await _httpClient.GetAsync("ps2/worldState");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOnlinePlayers(int worldId)
        {
            var response = await _httpClient.GetAsync($"ps2/worldState/{worldId}/players");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetZoneOwnership(int worldId, int zoneId)
        {
            var response = await _httpClient.GetAsync($"ps2/worldState/{worldId}/{zoneId}/map");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetZoneMap(int zoneId)
        {
            var response = await _httpClient.GetAsync($"ps2/map/{zoneId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> SetupWorldZoneStates(int worldId)
        {
            var response = await _httpClient.PostAsync($"ps2/worldState/{worldId}/zone", null);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetServiceStates()
        {
            var response = await _httpClient.GetAsync("services/status");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetServiceState(string service)
        {
            var response = await _httpClient.GetAsync($"services/{service}/status");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> EnableService(string service)
        {
            var response = await _httpClient.PostAsync($"services/{service}/enable", null);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> DisableService(string service)
        {
            var response = await _httpClient.PostAsync($"services/{service}/disable", null);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetLastOnlinePSBAccounts()
        {
            var response = await _httpClient.GetAsync("psb/sessions");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterStatsByName(string characterName)
        {
            var response = await _httpClient.GetAsync($"ps2/character/byname/{characterName}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterWeaponStatsByName(string characterName, string weaponName)
        {
            var response = await _httpClient.GetAsync($"ps2/character/byname/{characterName}/weapon/{weaponName}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOutfitStatsByAlias(string outfitAlias)
        {
            var response = await _httpClient.GetAsync($"ps2/outfit/byalias/{outfitAlias}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOracleCategory(string categoryId)
        {
            var response = await _httpClient.GetAsync($"ps2/oracle/category/{categoryId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetOracleStats(string statId, IEnumerable<string> weaponIds)
        {
            var response = await _httpClient.GetAsync($"ps2/oracle/stats/{statId}/?q={string.Join(',', weaponIds)}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetPlayerRankings()
        {
            var response = await _httpClient.GetAsync("ps2/ranks");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterOnlineState(string characterId)
        {
            var response = await _httpClient.GetAsync($"ps2/character/{characterId}/state");
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
