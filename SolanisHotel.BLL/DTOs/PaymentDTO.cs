# nullable disable

namespace SolanisHotel.BLL.DTOs
{
    public class PaymentDTO
    {
        public CardInfoDTO CardInfo { get; set; }


        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCode { get; set; }
    }

    public class CardInfoDTO
    {
        public string CardUsername { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
    }
}