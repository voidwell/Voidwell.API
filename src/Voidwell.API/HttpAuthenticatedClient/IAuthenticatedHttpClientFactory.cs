using System.Net.Http;

namespace Voidwell.API.HttpAuthenticatedClient
{
    public interface IAuthenticatedHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
