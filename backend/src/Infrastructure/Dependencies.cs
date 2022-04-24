using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped<OrderFacade>();
            services.AddScoped<CleanerFacade>();
            services.AddScoped<ICommissionService, CommissionService>();
            services.AddScoped<IClientService, ClientService>();
        }
    }
}
