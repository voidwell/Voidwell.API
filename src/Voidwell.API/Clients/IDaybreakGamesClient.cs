using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Voidwell.API.Clients
{
    public interface IDaybreakGamesClient : IDisposable
    {
        Task<JToken> GetMonitorState();
        Task<JToken> GetOnlinePlayers(int worldId);
        Task<JToken> GetWorldZoneState(int worldId, int zoneId);
        Task<JToken> GetGrades();
        Task<JToken> GetPlanetside2News();
        Task<JToken> GetPlanetside2Updates();
        Task<JToken> Search(string query);
        Task<JToken> GetAlerts();
        Task<JToken> GetAlertsByWorldId(string worldId);
        Task<JToken> GetAlert(string worldId, string alertId);
        Task<JToken> GetCharacter(string characterId);
        Task<JToken> GetCharacterSessions(string characterId);
        Task<JToken> GetCharacterSession(string characterId, string sessionId);
        Task<JToken> GetWeaponLeaderboard(string weaponItemId);
        Task<JToken> GetWorldTerritory(string worldId, string zoneId);
        Task<JToken> GetOutfit(string outfitId);
        Task<JToken> GetOutfitMembers(string outfitId);
        Task<JToken> GetAllPlayableClasses();
        Task<JToken> GetAllVehicles();
        Task<JToken> GetWeaponInfo(string weaponItemId);
        Task<JToken> GetServiceStates();
        Task<JToken> GetServiceState(string service);
        Task<JToken> EnableService(string service);
        Task<JToken> DisableService(string service);
        Task<JToken> GetLastOnlinePSBAccounts();
        Task<JToken> SetupWorldZoneStates(int worldId);
    }
}
