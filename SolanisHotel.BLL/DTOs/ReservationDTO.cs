using SolanisHotel.ENTITIES.Enums;

# nullable disable

namespace SolanisHotel.BLL.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public ReservationDTO()
        {
            //ReservationDate = DateTime.Now;
            //ReservationStatus = ReservationStatus.Pending;
        }

        public DateTime ReservationDate { get; set; }
        public decimal TotalPrice { get; set; }
        public byte NumberOfGuests { get; set; }
        public byte NumberOfRooms { get; set; }
        public byte NumberOfExtraBeds { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public int CustomerId { get; set; }
    }
}