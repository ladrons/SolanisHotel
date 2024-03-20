using AutoMapper;
using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class RoomManager : BaseManager<Room>, IRoomManager
    {
        readonly IMapper _mapper;
        readonly IReservationRoomManager _reservationRoomMan;

        readonly IRoomRepository _roomRep;

        public RoomManager(IRoomRepository roomRep, IReservationRoomManager reservationRoomMan, IMapper mapper) : base(roomRep)
        {
            _roomRep = roomRep;
            _reservationRoomMan = reservationRoomMan;
            _mapper = mapper;
        }

        //----------//----------//

        /// <summary>
        /// Belirtilen otelin kayıtlı ve etkin olan odalarını listeler.
        /// </summary>
        /// <param name="currentHotel">Odaların bilgilerinin alınacağı otel</param>
        /// <returns>Etkin odaların listesi (Status değeri 'Deleted' olmayan odalar)</returns>
        /// <exception cref="ArgumentException">Etkin oda bulunamazsa fırlatılır.</exception>
        public List<Room> BringActiveRooms(Hotel currentHotel)
        {
            if (currentHotel.Rooms != null && currentHotel.Rooms.Any(x => x.Status != ENTITIES.Enums.DataStatus.Deleted))
            {
                return currentHotel.Rooms.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).ToList();
            }
            { throw new ArgumentException("Uygun oda bulunamadı."); }
        }

        /// <summary>
        /// Belirtilen tarih aralığında mevcut odalardan uygun olanları bulur.
        /// </summary>
        /// <param name="activeRooms">Mevcut odaların listesi</param>
        /// <param name="checkIn">Giriş tarihi</param>
        /// <param name="checkOut">Çıkış tarihi</param>
        /// <param name="requiredRoomCount">İstenen oda sayısı</param>
        /// <returns>Belirtilen tarih aralığında uygun olan odaların listesi</returns>
        public List<Room> FindSuitableRooms(List<Room> activeRooms, DateTime checkIn, DateTime checkOut, int requiredRoomCount)
        {
            return activeRooms.Except(_reservationRoomMan
                    .GetUnavailableRooms(checkIn, checkOut))
                .Take(requiredRoomCount)
                .ToList();
        }

        //----------//

        /// <summary>
        /// Oda listesini RoomDTO listesine dönüştürür.
        /// </summary>
        /// <param name="roomList">Dönüştürülecek oda listesi.</param>
        /// <returns>Oda listesini temsil eden RoomDTO listesi.</returns>
        public List<RoomDTO> MappingToRoomDTOList(List<Room> roomList) => _mapper.Map<List<RoomDTO>>(roomList);

        //-----Privates Methods-----//


        //-----Privates Methods-----//




        //-----Test Area-----//

        //-----Test Area-----//
    }
}