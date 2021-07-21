using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voidwell.API.HttpAuthenticatedClient;
using Voidwell.API.Models;

namespace Voidwell.API.Clients
{
    public abstract class DaybreakGamesPS4Client : DaybreakGamesClient
    {
        protected abstract string OriginatorId { get; }
        protected abstract string ServiceEndpoint { get; }

        protected DaybreakGamesPS4Client(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
            _httpClient.BaseAddress = new Uri(ServiceEndpoint);
        }

        public override async Task<IEnumerable<ServiceState>> GetServiceStates()
        {
            return SetOriginator(await base.GetServiceStates());
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

        public override async Task<IEnumerable<LastStoreUpdate>> GetAllStoreUpdateLogs()
        {
            return SetOriginator(await base.GetAllStoreUpdateLogs());
        }

        public override async Task<LastStoreUpdate> UpdateStore(string storeName)
        {
            return SetOriginator(await base.UpdateStore(storeName));
        }

        private IEnumerable<T> SetOriginator<T>(IEnumerable<T> values) where T : OriginatorData
        {
            if (values == null)
            {
                return null;
            }

            return values.Select(SetOriginator);
        }

        private T SetOriginator<T>(T value) where T : OriginatorData
        {
            if (value == null)
            {
                return null;
            }

            value.Originator = OriginatorId;
            return value;
        }
    }
}
