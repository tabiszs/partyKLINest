using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.Infrastructure.Data;
using PartyKlinest.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;

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
            configuration["AzureAdB2C:AllowWebApiToBeAuthorizedByACL"] = "true";
            services
                .AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAdB2C");

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientOnly", policy => policy.RequireClaim("extension_AccountType", "0"));
                options.AddPolicy("CleanerOnly", policy => policy.RequireClaim("extension_AccountType", "1"));
                options.AddPolicy("ClientOrCleaner", policy => policy.RequireClaim("extension_AccountType", "0", "1"));
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("extension_AccountType", "2"));
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
