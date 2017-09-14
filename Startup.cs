using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                .AddInMemoryClients(new[] { clientCredentials, mvcApp })
                .AddInMemoryApiResources(new[] { api1 })
                .AddInMemoryIdentityResources(new IdentityResource[]
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                })
                .AddTestUsers(new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "123",
                        Username = "scott",
                        Password = "password",
                        Claims =
                        {
                            new Claim("email", "scott@scottbrady91.com"),
                            new Claim("website", "http://scottbrady91.com")
                        }
                    }
                })
                .AddTemporarySigningCredential();

            services.AddMvc();
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

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
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

        private Client mvcApp = new Client
        {
            ClientId = "prog_net",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Hybrid,
            AllowedScopes = { "openid", "profile", "email", "api1.read" },
            RedirectUris = { "http://localhost:5001/signin-oidc" },
            LogoUri = "http://localhost:5000/signout-oidc"
        };
    }
}
