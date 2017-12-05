using Voidwell.API.HttpDelegatedClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DelegatedHttpClientExtensions
    {
        public static void AddDelegatedHttpClient(this IServiceCollection services)
        {
            var options = new DelegatedHttpClientOptions();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IDelegatedHttpClientFactory, DelegatedHttpClientFactory>();
            services.AddTransient<DelegatedHttpMessageHandler>();
            services.AddSingleton(options);
            services.AddTransient<Func<string, DelegatedHttpMessageHandler>>(
                sp => targetScope =>
                {
                    var handler = sp.GetService<DelegatedHttpMessageHandler>();
                    handler.TargetScope = targetScope;
                    return handler;
                });
        }
    }
}
