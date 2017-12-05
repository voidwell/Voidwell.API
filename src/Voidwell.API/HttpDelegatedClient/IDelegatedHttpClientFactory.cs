using System.Net.Http;

namespace Voidwell.API.HttpDelegatedClient
{
    public interface IDelegatedHttpClientFactory
    {
        HttpClient GetHttpClient(string targetScope);
    }
}