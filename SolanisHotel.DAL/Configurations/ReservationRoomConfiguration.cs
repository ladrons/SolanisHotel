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
    public class ReservationRoomConfiguration : BaseConfiguration<ReservationRoom>
    {
        public override void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new
            {
                x.ReservationId,
                x.RoomId
            });
        }
    }
}