using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanisHotel.DAL.Configurations.Common;
using SolanisHotel.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.DAL.Configurations
{
    public class ReservationConfiguration : BaseConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(x => x.Payment).WithOne(x => x.Reservation).HasForeignKey<Payment>(x => x.Id);

            builder.Property(x => x.TotalPrice).HasPrecision(18, 2);
        }
    }
}