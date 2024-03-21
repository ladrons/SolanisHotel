using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Results;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IReservationManager : IManager<Reservation>
    {
        /// <summary>
        /// Rezervasyon oluşturma işlemi için kullanılan metottur.
        /// </summary>
        /// <param name="rvm">Rezervasyonun oluşturulması için gerekli verileri içeren ViewModel.</param>
        /// <returns>
        /// Oluşturulan rezervasyonun başarı durumunu içeren <see cref="OperationResult"/> nesnesi.
        /// Başarılıysa: true, Hata durumunda: false.
        /// Başarılıysa: "İşlem başarılı" mesajı ve oluşturulan rezervasyonun bilgileri.
        /// Hata durumunda: "Bir hata meydana geldi. Lütfen tekrar deneyiniz." mesajı ve hata detayları.
        /// </returns>
        public Task<OperationResult> CreateReservation(ReservationVM rvm);
        /// <summary>
        /// Rezervasyonu iptal etme işlemi için kullanılan metottur.
        /// </summary>
        /// <param name="rvm">İptal edilecek rezervasyonun bilgilerini içeren ViewModel.</param>
        /// <returns>
        /// İptal işleminin başarı durumunu içeren <see cref="OperationResult"/> nesnesi.
        /// Başarılıysa: true, Hata durumunda: false.
        /// Başarılıysa: "İşlem başarılı" mesajı.
        /// Hata durumunda: "Hata meydana geldi" mesajı ve hata detayları.
        /// </returns>
        public Task<OperationResult> CancelReservation(ReservationVM rvm);

        //----------//

        /// <summary>
        /// Bir müşteri ve rezervasyon veri transfer nesnesi (DTO) alarak bir rezervasyon oluşturur ve kaydeder.
        /// </summary>
        /// <param name="currentCustomer">O anki müşteriyi temsil eden bir nesnedir.</param>
        /// <param name="reservationDTO">Rezervasyon oluşturmak için kullanılacak veri transfer nesnesidir.</param>
        /// <returns>Oluşturulan rezervasyon nesnesi.</returns>
        public Task<Reservation> GenerateReservation(Customer currentCustomer, ReservationDTO reservationDTO);

        /// <summary>
        /// Mevcut rezervasyonun durumunu günceller ve ilgili işlemleri gerçekleştirir.
        /// </summary>
        /// <param name="currentReservation">Güncellenecek rezervasyon bilgisi.</param>
        /// <param name="result">Güncellenmiş rezervasyon durumu (başarılıysa true, başarısızsa false).</param>
        /// <returns>Güncellenmiş rezervasyon bilgisi.</returns>
        public Task<Reservation> UpdateReservationStatusAndPerformActions(Reservation currentReservation, bool result);

        /// <summary>
        /// Rezervasyonu kaldırır ve değişiklikleri veritabanına kaydeder.
        /// </summary>
        /// <param name="currentReservation">Kaldırılacak rezervasyon</param>
        /// <returns>Asenkron bir görev</returns>
        public Task RemoveReservation(Reservation currentReservation);

        //----------//

        /// <summary>
        /// Seçilen odaların kapasitesi ve misafir sayısını kullanarak ek yatak sayısını hesaplar.
        /// </summary>
        /// <param name="selectedRooms">Seçilen odaların listesi</param>
        /// <param name="numberOfGuests">Misafir sayısı</param>
        /// <param name="numberOfRooms">Oda sayısı</param>
        /// <returns>Hesaplanan ek yatak sayısı</returns>
        public int CalculateExtraBedCount(List<Room> selectedRooms, int numberOfGuests, int numberOfRooms);

        /// <summary>
        /// Seçilen odaların belirtilen süre boyunca ek yatak talebiyle birlikte toplam fiyatını hesaplar.
        /// </summary>
        /// <param name="selectedRooms">Seçilen odaların listesi.</param>
        /// <param name="totalDays">Toplam kalınacak gün sayısı.</param>
        /// <param name="extraBedCount">Ek yatak sayısı.</param>
        /// <returns>Toplam fiyat.</returns>
        decimal CalculateTotalPrice(List<Room> selectedRooms, int totalDays, int extraBedCount);

        //----------//

        ReservationDTO MappingToReservationDTO(Reservation reservation);





        //-----Test Area-----//
        

        //-----Test Area-----//
    }
}