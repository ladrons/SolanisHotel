using SolanisHotel.BLL.DTOs;
using SolanisHotel.COMMON.Tools;

#nullable disable

namespace SolanisHotel.BLL.ViewModels
{
    public class PaymentVM
    {
        //DTOs
        public PaymentDTO PaymentDTO { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
        public List<RoomDTO> suitableRoomsDTO { get; set; }

        //VMs
        public ReservationVM ReservationVM { get; set; }

        //PaymentServices Properties
        public PaymentInformation PaymentInformation { get; set; }
    }
}