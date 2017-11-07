using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.AccessTokenValidation;
using Voidwell.API.Clients;
using Microsoft.Extensions.Options;

namespace Voidwell.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddDataAnnotations();

            services.AddOptions();
            services.Configure<VoidwellAPIOptions>(Configuration);
            services.AddTransient(a => a.GetRequiredService<IOptions<VoidwellAPIOptions>>().Value);

            services.AddSingleton<IAuthClient, AuthClient>();
            services.AddSingleton<IVoidwellClient, VoidwellClient>();
            services.AddSingleton<IPlanetsideClient, PlanetsideClient>();
            services.AddSingleton<IBungieNetClient, BungieNetClient>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
