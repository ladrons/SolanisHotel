# nullable disable

namespace SolanisHotel.BLL.DTOs
{
    public class PaymentDTO
    {
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCode { get; set; }
    }
}