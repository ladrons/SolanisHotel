using SolanisHotel.BLL.DTOs;
using SolanisHotel.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IRoomManager : IManager<Room>
    {
        /// <summary>
        /// Belirtilen otelin kayıtlı ve etkin olan odalarını listeler.
        /// </summary>
        /// <param name="currentHotel">Odaların bilgilerinin alınacağı otel</param>
        /// <returns>Etkin odaların listesi (Status değeri 'Deleted' olmayan odalar)</returns>
        /// <exception cref="ArgumentException">Etkin oda bulunamazsa fırlatılır.</exception>
        public List<Room> BringActiveRooms(Hotel currentHotel);

        /// <summary>
        /// Belirtilen tarih aralığında mevcut odalardan uygun olanları bulur.
        /// </summary>
        /// <param name="activeRooms">Mevcut odaların listesi</param>
        /// <param name="checkIn">Giriş tarihi</param>
        /// <param name="checkOut">Çıkış tarihi</param>
        /// <param name="requiredRoomCount">İstenen oda sayısı</param>
        /// <returns>Belirtilen tarih aralığında uygun olan odaların listesi</returns>
        public List<Room> FindSuitableRooms(List<Room> activeRooms, DateTime checkIn, DateTime checkOut, int requiredRoomCount);


        /// <summary>
        /// Oda listesini RoomDTO listesine dönüştürür.
        /// </summary>
        /// <param name="roomList">Dönüştürülecek oda listesi.</param>
        /// <returns>Oda listesini temsil eden RoomDTO listesi.</returns>
        public List<RoomDTO> MappingToRoomDTOList(List<Room> roomList);
    }
}