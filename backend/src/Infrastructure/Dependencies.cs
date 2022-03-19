using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartyKlinest.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.Infrastracture
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            // add pg database
            services.AddDbContext<PartyKlinerDbContext>(o =>
                o.UseNpgsql(configuration.GetConnectionString("PartyKlinerDbContext")));
        }
    }
}
