# nullable disable

namespace SolanisHotel.BLL.DTOs
{
    public class RoomDTO
    {
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte Capacity { get; set; }

        public bool IsOccupied { get; set; }
        public bool Availability { get; set; }
    }
}