using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Voidwell.API.Models;

namespace Voidwell.API.Clients
{
    public interface IDaybreakGamesClient : IDisposable
    {
        Task<JToken> GetMonitorState();
        Task<JToken> GetWorldState(int worldId);
        Task<JToken> GetOnlinePlayers(int worldId);
        Task<JToken> GetZoneOwnership(int worldId, int zoneId);
        Task<JToken> GetZoneMap(int zoneId);
        Task<JToken> GetGrades();
        Task<JToken> GetPlanetside2News();
        Task<JToken> GetPlanetside2Updates();
        Task<JToken> Search(string category, string query);
        Task<JToken> GetAlerts(int pageNumber, int? worldId);
        Task<JToken> GetAlert(string worldId, string alertId);
        Task<JToken> GetCharacter(string characterId);
        Task<JToken> GetCharacterSessions(string characterId, int page = 0);
        Task<JToken> GetCharacterSession(string characterId, string sessionId);
        Task<JToken> GetCharacterLiveSession(string characterId);
        Task<JToken> GetCharacterOnlineState(string characterId);
        Task<JToken> GetWeaponLeaderboard(string weaponItemId, int page);
        Task<JToken> GetWorldTerritory(string worldId, string zoneId);
        Task<JToken> GetWorldPopulation(string worldId, string zoneId);
        Task<JToken> GetOutfit(string outfitId);
        Task<JToken> GetOutfitMembers(string outfitId);
        Task<JToken> GetAllPlayableClasses();
        Task<JToken> GetAllVehicles();
        Task<JToken> GetAllWorlds();
        Task<JToken> GetWorldPopulationHistory(IEnumerable<string> worldIds);
        Task<JToken> GetAllZones();
        Task<JToken> GetWeaponInfo(string weaponItemId);
        Task<IEnumerable<ServiceState>> GetServiceStates();
        Task<ServiceState> GetServiceState(string service);
        Task<ServiceState> EnableService(string service);
        Task<ServiceState> DisableService(string service);
        Task<IEnumerable<LastStoreUpdate>> GetAllStoreUpdateLogs();
        Task<LastStoreUpdate> UpdateStore(string storeName);
        Task<JToken> GetLastOnlinePSBAccounts();
        Task<JToken> SetupWorldZoneStates(int worldId);
        Task<JToken> GetCharacterStatsByName(string characterName);
        Task<JToken> GetCharacterWeaponStatsByName(string characterName, string weaponName);
        Task<JToken> GetOutfitStatsByAlias(string outfitAlias);
        Task<JToken> GetWeaponInfoByName(string weaponName);
        Task<JToken> GetOracleCategory(string categoryId);
        Task<JToken> GetOracleStats(string statId, IEnumerable<string> weaponIds);
        Task<JToken> GetPlayerRankings();
        Task<JToken> GetMultipleCharacterStatsByName(IEnumerable<string> characterNames);
        Task<JToken> GetWorldActivity(int? worldId, int? period);
    }
}
