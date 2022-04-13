using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class CleanerConfiguration : IEntityTypeConfiguration<Cleaner>
    {
        public void Configure(EntityTypeBuilder<Cleaner> builder)
        {
            builder
                .Property(x => x.CleanerId)
                .HasMaxLength(40);

            builder
                .Property(c => c.MinPrice)
                .HasColumnType("money");
        }
    }
}
