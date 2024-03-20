using SolanisHotel.BLL.DTOs;

#nullable disable

namespace SolanisHotel.BLL.ViewModels
{
    public class PaymentVM
    {
        //DTOs
        public PaymentDTO PaymentDTO { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
        public List<RoomDTO> suitableRoomsDTO { get; set; }
    }
}