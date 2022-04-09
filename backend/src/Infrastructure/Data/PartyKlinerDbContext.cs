using Microsoft.EntityFrameworkCore;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.Infrastructure.Data.KeyValuePairs;
using System.Reflection;

namespace PartyKlinest.Infrastructure.Data
{
    internal class PartyKlinerDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PartyKlinerDbContext(DbContextOptions<PartyKlinerDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Opinion> Opinions { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Cleaner> Cleaners { get; set; }
        public DbSet<ScheduleEntry> ScheduleEntries { get; set; }

        public DbSet<DecimalKeyValuePair> DecimalKeyValuePairs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
