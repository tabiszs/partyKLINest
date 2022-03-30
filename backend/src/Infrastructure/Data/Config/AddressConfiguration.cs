using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(180);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(18);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(90);

            builder.Property(a => a.BuildingNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
