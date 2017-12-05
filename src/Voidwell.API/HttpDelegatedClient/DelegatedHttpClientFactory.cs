using System;
using System.Net.Http;

namespace Voidwell.API.HttpDelegatedClient
{
    public class DelegatedHttpClientFactory : IDelegatedHttpClientFactory
    {
        private readonly Func<string, DelegatedHttpMessageHandler> _delegatedHttpMessageHandlerFactory;

        public DelegatedHttpClientFactory(Func<string, DelegatedHttpMessageHandler> delegatedHttpMessageHandlerFactory)
        {
            _delegatedHttpMessageHandlerFactory = delegatedHttpMessageHandlerFactory;
        }

        public HttpClient GetHttpClient(string targetScope)
        {
            return new HttpClient(_delegatedHttpMessageHandlerFactory(targetScope), true);
        }
    }
}
