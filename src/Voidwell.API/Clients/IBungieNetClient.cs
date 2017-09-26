using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Voidwell.API.Models;

namespace Voidwell.API.Clients
{
    public interface IBungieNetClient : IDisposable
    {
        Task<JToken> GetCharacter(PlatformType platform, string membershipId, string characterId);
        Task<JToken> GetCharacterInventory(PlatformType platform, string membershipId, string characterId);
        Task<JToken> GetInventory(PlatformType platform, string membershipId);
        Task<JToken> GetItem(PlatformType platform, string membershipId, string itemInstanceId);
        Task<JToken> GetPostgameReport(string activityId);
        Task<JToken> GetProfile(PlatformType platform, string membershipId);
        Task<JToken> GetMilestone();
        Task<JToken> GetMilestoneContent(string milestoneHash);
        Task<JToken> Search(PlatformType platform, string displayName);

        Task<JToken> TransferItem(JToken request);
        Task<JToken> EquipItem(JToken request);
        Task<JToken> EquipItems(JToken request);
        Task<JToken> LockItem(JToken request);
        Task<JToken> UnlockItem(JToken request);
    }
}
