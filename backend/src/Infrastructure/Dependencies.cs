using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.Infrastructure.Data;
using PartyKlinest.Infrastructure.Services;

namespace PartyKlinest.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<PartyKlinerDbContext>(o =>
                o.UseNpgsql(configuration.GetConnectionString("PartyKlinerDbContext"))
                .UseSnakeCaseNamingConvention()
                );

            // Add Azure AD B2C authentication to the ASP.NET Core pipeline
            services.AddSingleton<IAuthenticationProvider>(o =>
            {
                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(configuration.GetSection("AzureAdB2C")["ClientId"])
                    .WithTenantId(configuration.GetSection("AzureAdB2C")["TenantId"])
                    .WithClientSecret(configuration.GetSection("AzureAdB2C")["ClientSecret"])
                    .Build();

                return new Microsoft.Graph.Auth.ClientCredentialProvider(confidentialClientApplication);
            });

            services.AddSingleton(conf => new GraphServiceClient(conf.GetService<IAuthenticationProvider>()));

            services.AddSingleton(o => new ExtensionPropertyNameBuilder(configuration.GetSection("AzureAdB2C")["ExtensionAppId"]));

            configuration["AzureAdB2C:AllowWebApiToBeAuthorizedByACL"] = "true";
            services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAdB2C");

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientOnly", policy => policy.RequireClaim("extension_AccountType", "Client"));
                options.AddPolicy("CleanerOnly", policy => policy.RequireClaim("extension_AccountType", "Cleaner"));
                options.AddPolicy("ClientOrCleaner", policy => policy.RequireClaim("extension_AccountType", "Client", "Cleaner"));
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("extension_AccountType", "Administrator"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped<OrderFacade>();
            services.AddScoped<CleanerFacade>();
            services.AddScoped<ICommissionService, CommissionService>();
            services.AddScoped<IClientService, ClientService>();
        }
    }
}
