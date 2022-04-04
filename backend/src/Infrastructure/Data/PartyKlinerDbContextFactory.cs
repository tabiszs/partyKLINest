using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PartyKlinest.Infrastructure.Data
{
    public class PartyKlinerDbContextFactory : IDesignTimeDbContextFactory<PartyKlinerDbContext>
    {
        /// <summary>
        /// For using during command line migrations.
        /// </summary>
        /// <param name="args">Arguments passed from commandline. First argument should be a connection string.</param>
        /// <returns></returns>
        public PartyKlinerDbContext CreateDbContext(string[] args)
        {
            var connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<PartyKlinerDbContext>();
            optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();

            return new PartyKlinerDbContext(optionsBuilder.Options);
        }
    }

}
