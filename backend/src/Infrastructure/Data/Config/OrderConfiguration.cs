using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(x => x.MaxPrice)
                .IsRequired()
                .HasColumnType("money");

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(180);

                a.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);

                a.Property(a => a.PostalCode)
                    .IsRequired()
                    .HasMaxLength(18);

                a.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(90);

                a.Property(a => a.BuildingNumber)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            builder.Navigation(x => x.Address).IsRequired();

            builder.OwnsOne(x => x.CleanersOpinion, o =>
            {
                o.Property(o => o.Rating)
                    .IsRequired();
                o.Property(o => o.AdditionalInfo)
                    .IsRequired()
                    .HasMaxLength(4096);
            });

            builder.OwnsOne(x => x.ClientsOpinion, o =>
            {
                o.Property(o => o.Rating)
                    .IsRequired();
                o.Property(o => o.AdditionalInfo)
                    .IsRequired()
                    .HasMaxLength(4096);
            });
        }
    }
}
