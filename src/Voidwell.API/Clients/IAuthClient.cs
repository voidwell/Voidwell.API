using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Voidwell.API.Clients
{
    public interface IAuthClient
    {
        Task<JToken> CreateApiResource(JToken apiResource);
        Task<JToken> CreateApiResourceSecret(string apiResourceId, JToken secret);
        Task<JToken> CreateClient(JToken client);
        Task<JToken> CreateClientSecret(string clientId, JToken secret);
        Task<JToken> DeleteApiResourceSecret(string apiResourceId, string queryString);
        Task<JToken> DeleteClientSecret(string clientId, string queryString);
        Task<JToken> GetAllApiResources();
        Task<JToken> GetAllClients();
        Task<JToken> GetApiResourceById(string apiResourceId);
        Task<JToken> GetClientById(string clientId);
        Task<JToken> UpdateApiResource(string apiResourceId, JToken apiResource);
        Task<JToken> UpdateClient(string clientId, JToken client);
    }
}