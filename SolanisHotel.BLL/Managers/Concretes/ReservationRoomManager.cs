using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class ReservationRoomManager : BaseManager<ReservationRoom>, IReservationRoomManager
    {
        readonly IReservationRoomRepository _reservationRoomRep;

        public ReservationRoomManager(IReservationRoomRepository reservationRoomRep) : base(reservationRoomRep)
        {
            _reservationRoomRep = reservationRoomRep;
        }

        //----------//----------//

        /// <summary>
        /// Belirtilen tarih aralığında rezervasyon için uygun olmayan odaları listeler.
        /// </summary>
        /// <param name="checkIn">Müşterinin giriş yapacağı tarih.</param>
        /// <param name="checkOut">Müşterinin çıkış yapacağı tarih.</param>
        /// <returns>Uygun olmayan odaların listesi.</returns>
        public List<Room> GetUnavailableRooms(DateTime checkIn, DateTime checkOut)
        {
            return _reservationRoomRep
                .Where(x => x.StartDate <= checkOut && x.EndDate >= checkIn && x.Status != ENTITIES.Enums.DataStatus.Deleted)
                .Select(x => x.Room)
                .ToList();
        }

        //----------//

        /// <summary>
        /// Belirtilen rezervasyon, giriş ve çıkış tarihleri, uygun odalar ve ek yatak sayısı kullanılarak rezervasyon odalarını oluşturur.
        /// </summary>
        /// <param name="currentReservation">Oluşturulan rezervasyon odalarının bağlı olduğu rezervasyon.</param>
        /// <param name="checkIn">Rezervasyonun başlangıç tarihi.</param>
        /// <param name="checkOut">Rezervasyonun bitiş tarihi.</param>
        /// <param name="suitableRooms">Rezervasyon odalarının oluşturulacağı uygun odalar listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task. Oluşturulan rezervasyon odalarını içeren liste.</returns>
        public async Task<List<ReservationRoom>> GenerateReservationRooms(Reservation currentReservation, 
            DateTime checkIn, DateTime checkOut, List<Room> suitableRooms)
        {
            int bedCount = currentReservation.NumberOfExtraBeds;

            List<ReservationRoom> newReservationRooms = suitableRooms.Select(room => new ReservationRoom
            {
                StartDate = checkIn,
                EndDate = checkOut,

                Reservation = currentReservation,
                Room = room,

                ExtraBed = bedCount-- > 0 && bedCount >= 0
            }).ToList();

            await SaveReservationRooms(newReservationRooms);
            return newReservationRooms;
        }

        /// <summary>
        /// Belirtilen rezervasyon odalarını kaldırır ve veritabanından siler.
        /// </summary>
        /// <param name="currentResRooms">Kaldırılacak rezervasyon odalarının listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task.</returns>
        public async Task RemoveReservationRooms(List<ReservationRoom> currentResRooms)
        {
            _reservationRoomRep.DestroyRange(currentResRooms);
            await _reservationRoomRep.SaveChangesAsync();
        }

        /// <summary>
        /// Belirtilen rezervasyon odalarını siler ve veritabanından kaldırır.
        /// </summary>
        /// <param name="currentResRooms">Silinecek rezervasyon odalarının listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task.</returns>
        public async Task DeleteReservationRooms(List<ReservationRoom> currentResRooms)
        {
            _reservationRoomRep.DeleteRange(currentResRooms);
            await _reservationRoomRep.SaveChangesAsync();
        }

        //-----Privates Methods-----//

        /// <summary>
        /// Yeni rezervasyon odalarını ekler ve veritabanına kaydeder.
        /// </summary>
        /// <param name="newReservationRooms">Eklenen rezervasyon odalarının listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task.</returns>
        private async Task SaveReservationRooms(List<ReservationRoom> newReservationRooms)
        {
            await _reservationRoomRep.AddRangeAsync(newReservationRooms);
            await _reservationRoomRep.SaveChangesAsync();
        }

        //-----Privates Methods-----//



        //-----Test Area-----//

        //-----Test Area-----//
    }
}