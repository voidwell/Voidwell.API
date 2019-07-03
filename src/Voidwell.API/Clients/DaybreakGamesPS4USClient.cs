using System;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class DaybreakGamesPS4USClient : DaybreakGamesClient, IDaybreakGamesPS4USClient
    {
        public DaybreakGamesPS4USClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.DaybreakGamesPs4Us);
        }
    }
}