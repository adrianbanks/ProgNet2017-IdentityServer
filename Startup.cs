using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(new Client[0])
                .AddInMemoryApiResources(new ApiResource[0])
                .AddInMemoryIdentityResources(new IdentityResource[0])
                .AddTestUsers(new List<TestUser>())
                .AddTemporarySigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }

        private ApiResource api1 = new ApiResource
        {
            Name = "api1",
            DisplayName = "API #1",
            Scopes =
            {
                new Scope("api1.read"),
                new Scope("api1.write")
            }
        };

        private Client clientCredentials = new Client
        {
            ClientId = "oauth_client",
            ClientSecrets = { new Secret("shhh".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "api1.read" }
        };
    }
}
