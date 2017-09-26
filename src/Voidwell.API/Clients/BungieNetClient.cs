using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Voidwell.API.Models;

namespace Voidwell.API.Clients
{
    public class BungieNetClient : IBungieNetClient
    {
        private readonly HttpClient _httpClient;

        public BungieNetClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri (new Uri(Constants.Endpoints.BungieNet), "destiny2");
        }

        public async Task<JToken> GetMilestone()
        {
            var response = await _httpClient.GetAsync("milestone");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetMilestoneContent(string milestoneHash)
        {
            var response = await _httpClient.GetAsync($"milestone/content/{milestoneHash}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetPostgameReport(string activityId)
        {
            var response = await _httpClient.GetAsync($"postgamereport/{activityId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> Search(PlatformType platform, string displayName)
        {
            var response = await _httpClient.GetAsync($"{platform}/search/{displayName}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetProfile(PlatformType platform, string membershipId)
        {
            var response = await _httpClient.GetAsync($"{platform}/{membershipId}/profile");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetInventory(PlatformType platform, string membershipId)
        {
            var response = await _httpClient.GetAsync($"{platform}/{membershipId}/inventory");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetItem(PlatformType platform, string membershipId, string itemInstanceId)
        {
            var response = await _httpClient.GetAsync($"{platform}/{membershipId}/item/{itemInstanceId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacter(PlatformType platform, string membershipId, string characterId)
        {
            var response = await _httpClient.GetAsync($"{platform}/{membershipId}/character/{characterId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetCharacterInventory(PlatformType platform, string membershipId, string characterId)
        {
            var response = await _httpClient.GetAsync($"{platform}/{membershipId}/character/{characterId}/inventory");
            return await response.GetContentAsync<JToken>();
        }


        // ----Inventory Management----

        public async Task<JToken> EquipItem(JToken request)
        {
            var content = JsonContent.FromObject(request);
            var response = await _httpClient.PostAsync("inventory/equipitem", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> EquipItems(JToken request)
        {
            var content = JsonContent.FromObject(request);
            var response = await _httpClient.PostAsync("inventory/equipitems", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> TransferItem(JToken request)
        {
            var content = JsonContent.FromObject(request);
            var response = await _httpClient.PostAsync("inventory/transferitem", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> LockItem(JToken request)
        {
            var content = JsonContent.FromObject(request);
            var response = await _httpClient.PostAsync("inventory/lock", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> UnlockItem(JToken request)
        {
            var content = JsonContent.FromObject(request);
            var response = await _httpClient.PostAsync("inventory/unlock", content);
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
