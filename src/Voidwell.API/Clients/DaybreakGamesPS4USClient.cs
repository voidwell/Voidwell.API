using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class DaybreakGamesPS4USClient : DaybreakGamesPs4Client, IDaybreakGamesPS4USClient
    {
        protected override string OriginatorId => "ps4us";
        protected override string ServiceEndpoint => Constants.Endpoints.DaybreakGamesPs4Us;

        public DaybreakGamesPS4USClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
        }
    }
}
