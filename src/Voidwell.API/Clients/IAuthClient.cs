using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Voidwell.API.Clients
{
    public interface IAuthClient
    {
        Task<JToken> CreateApiResource(JToken apiResource);
        Task<JToken> GetApiResourceSecrets(string apiResourceId);
        Task<JToken> CreateApiResourceSecret(string apiResourceId, JToken secret);
        Task<JToken> CreateClient(JToken client);
        Task<JToken> GetClientSecrets(string clientId);
        Task<JToken> CreateClientSecret(string clientId, JToken secret);
        Task<JToken> DeleteApiResourceSecret(string apiResourceId, string secretId);
        Task<JToken> DeleteClientSecret(string clientId, string secretId);
        Task<JToken> GetAllApiResources(string search = "", int page = 1);
        Task<JToken> GetAllClients(string search = "", int page = 1);
        Task<JToken> GetApiResourceById(string apiResourceId);
        Task<JToken> GetClientById(string clientId);
        Task<JToken> UpdateApiResource(string apiResourceId, JToken apiResource);
        Task<JToken> UpdateClient(string clientId, JToken client);
        Task<JToken> DeleteClient(string clientId);
        Task<JToken> DeleteApiResource(string apiResourceId);
    }
}