using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class ScheduleEntryConfiguration : IEntityTypeConfiguration<ScheduleEntry>
    {
        public void Configure(EntityTypeBuilder<ScheduleEntry> builder)
        {
            builder
                .Property(s => s.Start)
                .IsRequired()
                .HasColumnType("time without time zone");

            builder
                .Property(s => s.End)
                .IsRequired()
                .HasColumnType("time without time zone");
        }
    }
}
