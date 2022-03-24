using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.Infrastructure.Data.Config
{
    internal class CleanerConfiguration : IEntityTypeConfiguration<Cleaner>
    {
        public void Configure(EntityTypeBuilder<Cleaner> builder)
        {
            builder
                .Property(c => c.MinPrice)
                .HasColumnType("money");
        }
    }
}
