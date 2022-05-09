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
                .OwnsOne(x => x.OrderFilter, of =>
                {
                    of.Property(o => o.MinPrice)
                        .HasColumnType("money");
                });
        }
    }
}
