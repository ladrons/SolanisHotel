using AutoMapper;
using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.Results;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Enums;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class ReservationManager : BaseManager<Reservation>, IReservationManager
    {
        readonly IMapper _mapper;

        readonly IHotelManager _hotelMan;
        readonly IRoomManager _roomMan;
        readonly ICustomerManager _customerMan;
        readonly IReservationRoomManager _reservationRoomMan;

        readonly IReservationRepository _reservationRep;

        public ReservationManager(IReservationRepository reservationRep, IMapper mapper, IHotelManager hotelMan, IRoomManager roomMan, ICustomerManager customerMan, IReservationRoomManager reservationRoomMan) : base(reservationRep)
        {
            _mapper = mapper;

            _reservationRep = reservationRep;
            _hotelMan = hotelMan;
            _roomMan = roomMan;
            _customerMan = customerMan;
            _reservationRoomMan = reservationRoomMan;
        }

        //----------//----------//

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
        public async Task<OperationResult> CreateReservation(ReservationVM rvm)
        {
            if (ValidateGuestAndRoomCount(rvm.ReservationDTO.NumberOfGuests, rvm.ReservationDTO.NumberOfRooms))
            {
                Hotel currentHotel = await _hotelMan.FindHotelById(1); //Test değeri.

                List<Room> suitableRooms = _roomMan.FindSuitableRooms(
                    _roomMan.BringActiveRooms(currentHotel),
                    rvm.CheckIn,
                    rvm.CheckOut,
                    rvm.ReservationDTO.NumberOfRooms);

                rvm.SuitableRoomsDTO = _roomMan.MappingToRoomDTOList(suitableRooms);

                if (ValidateRoomCount(suitableRooms.Count, rvm.ReservationDTO.NumberOfRooms))
                {
                    if (ValidateCapacity(suitableRooms, rvm.ReservationDTO.NumberOfGuests, rvm.ReservationDTO.NumberOfRooms))
                    {
                        rvm.ReservationDTO.NumberOfExtraBeds = (byte)CalculateExtraBedCount(suitableRooms, rvm.ReservationDTO.NumberOfGuests, rvm.ReservationDTO.NumberOfRooms);

                        rvm.ReservationDTO.TotalPrice = CalculateTotalPrice(suitableRooms); //Todo: Ekstra yatak değeri için fiyat eklenmeli! (HighPriority)

                        Customer currentCustomer = await _customerMan.GetOrCreateCustomer(rvm.CustomerDTO.Email);
                        rvm.CustomerDTO = _customerMan.MappingToCustomerDTO(currentCustomer);

                        try
                        {
                            Reservation newReservation = await GenerateReservation(currentCustomer, rvm.ReservationDTO);
                            rvm.ReservationDTO = MappingToReservationDTO(newReservation);

                            await _reservationRoomMan.GenerateReservationRooms(newReservation, rvm.CheckIn, rvm.CheckOut, suitableRooms);

                            return new OperationResult(true, "İşlem başarılı", rvm);
                        }
                        catch (Exception ex) { return new OperationResult(false, "Bir hata meydana geldi. Lütfen tekrar deneyiniz.", ex.Message); }
                    }
                    else { return new OperationResult(false, "Fazla müşteri seçtiniz. Oda sayısını arttırın!"); }
                }
                else { return new OperationResult(false, "Talep ettiğiniz oda sayısı kadar uygun oda bulunamadı!"); }
            }
            else { return new OperationResult(false, "Oda sayısı misafir sayısından fazla olamaz!"); }
        }

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
        public async Task<OperationResult> CancelReservation(ReservationVM rvm)
        {
            try
            {
                Reservation currentReservation = await _reservationRep.FindAsync(rvm.ReservationDTO.Id);
                if (currentReservation != null)
                {
                    await _reservationRoomMan.RemoveReservationRooms(currentReservation.ReservationRooms);

                    Customer currentCustomer = currentReservation.Customer;

                    //if (currentCustomer.Role == UserRole.Guest)
                    //{
                    //    await _customerMan.RemoveGuest(currentCustomer); //ToDo: BURADAN DEVAM! Bu kısmı incele ve düzenle. Şuan da misafir kayıtlarının hepsi tek bir ziyaretçi verisi üzerinden kaydediliyor. Böyle olmamalı.
                    //}

                    await RemoveReservation(currentReservation);
                }

                return new OperationResult(true, "İşlem başarılı");
            }
            catch (Exception ex) { return new OperationResult(false, "Hata meydana geldi", ex); }
        }

        //----------//

        /// <summary>
        /// Bir müşteri ve rezervasyon veri transfer nesnesi (DTO) alarak bir rezervasyon oluşturur ve kaydeder.
        /// </summary>
        /// <param name="currentCustomer">O anki müşteriyi temsil eden bir nesnedir.</param>
        /// <param name="reservationDTO">Rezervasyon oluşturmak için kullanılacak veri transfer nesnesidir.</param>
        /// <returns>Oluşturulan rezervasyon nesnesi.</returns>
        public async Task<Reservation> GenerateReservation(Customer currentCustomer, ReservationDTO reservationDTO)
        {
            Reservation newReservation = _mapper.Map<Reservation>(reservationDTO);

            newReservation.Customer = currentCustomer;

            await _reservationRep.AddAsync(newReservation);
            await _reservationRep.SaveChangesAsync();

            return newReservation;
        }

        /// <summary>
        /// Mevcut rezervasyonun durumunu günceller ve ilgili işlemleri gerçekleştirir.
        /// </summary>
        /// <param name="currentReservation">Güncellenecek rezervasyon bilgisi.</param>
        /// <param name="result">Güncellenmiş rezervasyon durumu (başarılıysa true, başarısızsa false).</param>
        /// <returns>Güncellenmiş rezervasyon bilgisi.</returns>
        public async Task<Reservation> UpdateReservationStatusAndPerformActions(Reservation currentReservation, bool result)
        {
            currentReservation.ReservationStatus = (result) ? ReservationStatus.Confirmed : ReservationStatus.Cancelled;

            await _reservationRep.Update(currentReservation); //DB'deki değeri mevcut değer ile güncelle.

            if (currentReservation.ReservationStatus == ReservationStatus.Cancelled)
            {
                _reservationRep.Delete(currentReservation);
            }
            await _reservationRep.SaveChangesAsync();

            return currentReservation;
        }

        /// <summary>
        /// Rezervasyonu kaldırır ve değişiklikleri veritabanına kaydeder.
        /// </summary>
        /// <param name="currentReservation">Kaldırılacak rezervasyon</param>
        /// <returns>Asenkron bir görev</returns>
        public async Task RemoveReservation(Reservation currentReservation)
        {
            _reservationRep.Destroy(currentReservation);
            await _reservationRep.SaveChangesAsync();
        }

        //----------//

        /// <summary>
        /// Seçilen odaların kapasitesi ve misafir sayısını kullanarak ek yatak sayısını hesaplar.
        /// </summary>
        /// <param name="selectedRooms">Seçilen odaların listesi</param>
        /// <param name="numberOfGuests">Misafir sayısı</param>
        /// <param name="numberOfRooms">Oda sayısı</param>
        /// <returns>Hesaplanan ek yatak sayısı</returns>
        public int CalculateExtraBedCount(List<Room> selectedRooms, int numberOfGuests, int numberOfRooms)
        {
            int selectedRoomsCapacity = selectedRooms.Sum(r => r.Capacity);

            return (numberOfGuests > selectedRoomsCapacity) ? (int)Math.Ceiling((double)numberOfGuests - selectedRoomsCapacity) : 0;
        }

        /// <summary>
        /// Seçilen odaların toplam fiyatını hesaplar.
        /// </summary>
        /// <param name="selectedRooms">Seçilen odaların listesi</param>
        /// <returns>Hesaplanan toplam fiyat</returns>
        public decimal CalculateTotalPrice(List<Room> selectedRooms)
        {
            return selectedRooms.Sum(r => r.Price);
        }

        //----------//

        public ReservationDTO MappingToReservationDTO(Reservation reservation) => _mapper.Map<ReservationDTO>(reservation);

        //-----Privates Methods-----//

        /// <summary>
        /// Misafir sayısı ile oda sayısını karşılaştırarak geçerliliği doğrular.
        /// </summary>
        /// <param name="numberOfGuests">Misafir sayısı</param>
        /// <param name="numberOfRooms">Oda sayısı</param>
        /// <returns>Doğrulama başarılı ise true, aksi halde false</returns>
        private bool ValidateGuestAndRoomCount(int numberOfGuests, int numberOfRooms) => numberOfGuests >= numberOfRooms ? true : false;

        /// <summary>
        /// Mevcut odaların sayısı ile istenen oda sayısını karşılaştırarak geçerliliği doğrular.
        /// </summary>
        /// <param name="availableRooms">Mevcut odaların sayısı</param>
        /// <param name="requestedRooms">İstenen oda sayısı</param>
        /// <returns>Doğrulama başarılı ise true, aksi halde false</returns>
        private bool ValidateRoomCount(int availableRooms, int requestedRooms) => availableRooms >= requestedRooms ? true : false;

        /// <summary>
        /// Seçilen odaların toplam kapasitesi ile misafir sayısı ve oda sayısını karşılaştırarak geçerliliği doğrular.
        /// </summary>
        /// <param name="selectedRooms">Seçilen odaların listesi</param>
        /// <param name="numberOfGuests">Misafir sayısı</param>
        /// <param name="numberOfRooms">Oda sayısı</param>
        /// <returns>Doğrulama başarılı ise true, aksi halde false</returns>
        private bool ValidateCapacity(List<Room> selectedRooms, int numberOfGuests, int numberOfRooms)
        {
            return numberOfGuests <= selectedRooms
                .Sum(r => r.Capacity + numberOfRooms) ? true : false;
        }

        //-----Privates Methods-----//





        //-----Test Area-----//



        //-----Test Area-----//
    }
}