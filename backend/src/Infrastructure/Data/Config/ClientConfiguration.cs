using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .Property(x => x.Id)
                .HasMaxLength(40);
        }
    }
}
