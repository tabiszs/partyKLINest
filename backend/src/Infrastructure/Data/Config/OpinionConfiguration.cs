using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class OpinionConfiguration : IEntityTypeConfiguration<Opinion>
    {
        public void Configure(EntityTypeBuilder<Opinion> builder)
        {
            builder
                .Property(o => o.AdditionalInfo)
                .IsRequired()
                .HasMaxLength(4096);
        }
    }
}
