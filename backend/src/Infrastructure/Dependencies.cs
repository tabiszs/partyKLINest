using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartyKlinest.Infrastructure.Data;

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
        }
    }
}
