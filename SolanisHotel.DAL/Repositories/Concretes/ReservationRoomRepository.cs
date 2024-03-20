using SolanisHotel.DAL.Context;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.DAL.Repositories.Concretes
{
    public class ReservationRoomRepository : BaseRepository<ReservationRoom>, IReservationRoomRepository
    {
        public ReservationRoomRepository(SolanisHotelContext context) : base(context)
        {
        }
    }
}
