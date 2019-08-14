using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.AccessTokenValidation;
using Voidwell.API.Clients;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Authentication;
using Voidwell.API.HttpAuthenticatedClient;
using System.Collections.Generic;
using IdentityModel;
using Voidwell.Logging;
using Microsoft.AspNetCore.HttpOverrides;

namespace Voidwell.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true);

            if (env.IsDevelopment())
            {
                builder.AddJsonFile("devsettings.json", true, true);
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ApiOptions>(Configuration);

            var apiOptions = Configuration.Get<ApiOptions>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddOAuth2Introspection(o =>
                {
                    o.IntrospectionEndpoint = "http://voidwellauth:5000/connect/introspect";

                    o.ClientId = "voidwell-api";
                    o.ClientSecret = apiOptions.ApiResourceSecret;

                    o.CacheDuration = TimeSpan.FromMinutes(2);
                    o.EnableCaching = true;
                    o.SaveToken = true;
                    o.DiscoveryPolicy.RequireHttps = false;
                })
                .AddIdentityServerAuthentication("ClientScheme", o =>
                {
                    o.Authority = "http://voidwellauth:5000/";

                    o.ApiName = "voidwell-api";
                    o.ApiSecret = apiOptions.ApiResourceSecret;

                    o.SupportedTokens = SupportedTokens.Jwt;
                    o.CacheDuration = TimeSpan.FromMinutes(2);
                    o.EnableCaching = true;
                    o.SaveToken = true;
                    o.RequireHttpsMetadata = false;
                });

            services.AddAuthenticatedHttpClient(options =>
            {
                options.TokenServiceAddress = "http://voidwellauth:5000/connect/token";
                options.ClientId = "voidwell-api";
                options.ClientSecret = apiOptions.ClientSecret;
                options.Scopes = new List<string>
                    {
                        "voidwell-daybreakgames",
                        "voidwell-bungienet",
                        "voidwell-usermanagement",
                        "voidwell-internal",
                        "voidwell-auth-admin"
                    };
            });
            services.AddDelegatedHttpClient();

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddMvcOptions(o =>
                {
                    o.Filters.AddService(typeof(ClientResponseExceptionFilter));
                })
                .AddAuthorization(o =>
                {
                    o.AddPolicy(Constants.Policies.Mutterblack, policy =>
                    {
                        policy.AddAuthenticationSchemes("ClientScheme");
                        policy.RequireClaim(JwtClaimTypes.ClientId, "mutterblack");
                    });
                });

            services.AddDelegatedHttpClient();
            services.AddTransient<ITokenRetriever, TokenRetriever>();

            services.AddSingleton<IInternalClient, InternalClient>();
            services.AddSingleton<IDaybreakGamesClient, DaybreakGamesClient>();
            services.AddSingleton<IDaybreakGamesPS4USClient, DaybreakGamesPS4USClient>();
            services.AddSingleton<IDaybreakGamesPS4EUClient, DaybreakGamesPS4EUClient>();
            services.AddSingleton<IBungieNetClient, BungieNetClient>();
            services.AddSingleton<IUserManagementClient, UserManagementClient>();
            services.AddSingleton<IAuthClient, AuthClient>();

            services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();

            services.AddTransient<ClientResponseExceptionFilter>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders(GetForwardedHeaderOptions());

            app.UseLoggingMiddleware();

            app.UseAuthentication();

            app.UseMvc();
        }

        private static ForwardedHeadersOptions GetForwardedHeaderOptions()
        {
            var options = new ForwardedHeadersOptions
            {
                RequireHeaderSymmetry = false,
                ForwardLimit = 15,
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            };

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();

            return options;
        }
    }
}
