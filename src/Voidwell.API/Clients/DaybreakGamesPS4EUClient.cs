using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class DaybreakGamesPS4EUClient : DaybreakGamesPS4Client, IDaybreakGamesPS4EUClient
    {
        protected override string OriginatorId => "ps4eu";
        protected override string ServiceEndpoint => Constants.Endpoints.DaybreakGamesPs4Eu;

        public DaybreakGamesPS4EUClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
        }
    }
}
