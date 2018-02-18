using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.AccessTokenValidation;
using Voidwell.API.Clients;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Voidwell.API.HttpAuthenticatedClient;
using System.Collections.Generic;

namespace Voidwell.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddOAuth2Introspection(o =>
                {
                    o.IntrospectionEndpoint = "http://voidwellauth:5000/connect/introspect";

                    o.ClientId = "voidwell-api";
                    o.ClientSecret = "adminApiResourceSecret";

                    o.CacheDuration = TimeSpan.FromMinutes(2);
                    o.EnableCaching = true;
                    o.SaveToken = true;
                    o.DiscoveryPolicy.RequireHttps = false;
                });

            services.AddAuthenticatedHttpClient(options =>
            {
                options.TokenServiceAddress = "http://voidwellauth:5000/connect/token";
                options.ClientId = "voidwell-api";
                options.ClientSecret = "apiSecret";
                options.Scopes = new List<string>
                    {
                        "voidwell-daybreakgames",
                        "voidwell-bungienet",
                        "voidwell-usermanagement",
                        "voidwell-internal"
                    };
            });
            services.AddDelegatedHttpClient();

            /*
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(o =>
                {
                    o.Authority = "http://voidwellauth:5000/";

                    o.ApiName = "voidwell-api";
                    o.ApiSecret = "adminApiResourceSecret";

                    o.SupportedTokens = SupportedTokens.Reference;
                    o.CacheDuration = TimeSpan.FromMinutes(2);
                    o.EnableCaching = true;
                    o.SaveToken = true;
                    o.RequireHttpsMetadata = false;
                });
            */

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddMvcOptions(o =>
                {
                    o.Filters.AddService(typeof(ClientResponseExceptionFilter));
                });

            services.AddOptions();
            services.Configure<VoidwellAPIOptions>(Configuration);
            services.AddTransient(a => a.GetRequiredService<IOptions<VoidwellAPIOptions>>().Value);

            services.AddDelegatedHttpClient();
            services.AddTransient<ITokenRetriever, TokenRetriever>();

            services.AddSingleton<IInternalClient, InternalClient>();
            services.AddSingleton<IDaybreakGamesClient, DaybreakGamesClient>();
            services.AddSingleton<IBungieNetClient, BungieNetClient>();
            services.AddSingleton<IUserManagementClient, UserManagementClient>();

            services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();

            services.AddTransient<ClientResponseExceptionFilter>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
