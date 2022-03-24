using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyKlinest.ApplicationCore.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder
                .Property(x => x.TimeFrom)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder
                .Property(x => x.TimeTo)
                .IsRequired()
                .HasColumnType("timestamp with time zone");
            
        }
    }
}
