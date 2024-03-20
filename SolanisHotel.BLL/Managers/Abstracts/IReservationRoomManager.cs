using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IReservationRoomManager : IManager<ReservationRoom>
    {
        /// <summary>
        /// Belirtilen tarih aralığında rezervasyon için uygun olmayan odaları listeler.
        /// </summary>
        /// <param name="checkIn">Müşterinin giriş yapacağı tarih.</param>
        /// <param name="checkOut">Müşterinin çıkış yapacağı tarih.</param>
        /// <returns>Uygun olmayan odaların listesi.</returns>
        List<Room> GetUnavailableRooms(DateTime checkIn, DateTime checkOut);



        /// <summary>
        /// Belirtilen rezervasyon, giriş ve çıkış tarihleri, uygun odalar ve ek yatak sayısı kullanılarak rezervasyon odalarını oluşturur.
        /// </summary>
        /// <param name="currentReservation">Oluşturulan rezervasyon odalarının bağlı olduğu rezervasyon.</param>
        /// <param name="checkIn">Rezervasyonun başlangıç tarihi.</param>
        /// <param name="checkOut">Rezervasyonun bitiş tarihi.</param>
        /// <param name="suitableRooms">Rezervasyon odalarının oluşturulacağı uygun odalar listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task. Oluşturulan rezervasyon odalarını içeren liste.</returns>
        public Task<List<ReservationRoom>> GenerateReservationRooms(Reservation currentReservation, DateTime checkIn, DateTime checkOut, List<Room> suitableRooms);

        /// <summary>
        /// Belirtilen rezervasyon odalarını kaldırır ve veritabanından siler.
        /// </summary>
        /// <param name="currentResRooms">Kaldırılacak rezervasyon odalarının listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task.</returns>
        public Task RemoveReservationRooms(List<ReservationRoom> currentResRooms);

        /// <summary>
        /// Belirtilen rezervasyon odalarını siler ve veritabanından kaldırır.
        /// </summary>
        /// <param name="currentResRooms">Silinecek rezervasyon odalarının listesi.</param>
        /// <returns>Asenkron operasyonu temsil eden bir Task.</returns>
        public Task DeleteReservationRooms(List<ReservationRoom> currentResRooms);
    }
}