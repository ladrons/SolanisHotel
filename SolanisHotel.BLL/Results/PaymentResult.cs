using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Results
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; } //true = ödeme başarılı / false = ödeme başarısız.
        public int TransactionCode { get; set; } //Banka tarafında ödemeyi temsil eden sayı kombinasyonu.
        public DateTime PaymentDate { get; set; } //Ödemenin gerçekleştiği tarih/zaman bilgisi.            
    }
}