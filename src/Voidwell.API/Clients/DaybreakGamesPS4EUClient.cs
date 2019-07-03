using System;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class DaybreakGamesPS4EUClient : DaybreakGamesClient, IDaybreakGamesPS4EUClient
    {
        public DaybreakGamesPS4EUClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.DaybreakGamesPs4Eu);
        }
    }
}
