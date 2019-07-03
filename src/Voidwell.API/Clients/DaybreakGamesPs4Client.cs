using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voidwell.API.HttpAuthenticatedClient;
using Voidwell.API.Models;

namespace Voidwell.API.Clients
{
    public abstract class DaybreakGamesPs4Client : DaybreakGamesClient
    {
        protected abstract string OriginatorId { get; }
        protected abstract string ServiceEndpoint { get; }

        protected DaybreakGamesPs4Client(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
            _httpClient.BaseAddress = new Uri(ServiceEndpoint);
        }

        public override async Task<IEnumerable<ServiceState>> GetServiceStates()
        {
            var states = await base.GetServiceStates();
            return states?.Select(SetOriginator);
        }

        public override async Task<ServiceState> GetServiceState(string service)
        {
            return SetOriginator(await base.GetServiceState(service));
        }

        public override async Task<ServiceState> EnableService(string service)
        {
            return SetOriginator(await base.EnableService(service));
        }

        public override async Task<ServiceState> DisableService(string service)
        {
            return SetOriginator(await base.DisableService(service));
        }

        private ServiceState SetOriginator(ServiceState state)
        {
            if (state == null)
            {
                return null;
            }

            state.Originator = OriginatorId;
            return state;
        }
    }
}
