using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Results;

#nullable disable

namespace SolanisHotel.BLL.ViewModels
{
    public class ReservationVM
    {
        //DTOs
        public ReservationDTO ReservationDTO { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
        public PaymentDTO PaymentDTO { get; set; }
        public List<RoomDTO> SuitableRoomsDTO { get; set; }

        //Properties
        public DateTime CheckIn { get; set; } //Giriş tarihi
        public DateTime CheckOut { get; set; } //Çıkış tarihi
    }
}