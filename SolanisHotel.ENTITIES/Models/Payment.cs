using SolanisHotel.ENTITIES.Enums;
using SolanisHotel.ENTITIES.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Models
{
    public class Payment : BaseEntity
    {
        //Backing fields



        //Constructor
        public Payment()
        {

        }


        //Properties
        public DateTime PaymentDate { get; set; } //Ödemenin gerçekleştirildiği tarih.
        public decimal TotalAmount { get; set; } //Müşterinin toplam ödediği fiyat.
        public int TransactionCode { get; set; } //Ödeme kodu(İlgilenilecek.)
        public string? Notes { get; set; } //Ödeme ile ilgili not.
        public PaymentStatus PaymentStatus { get; set; } //Ödeme durumu.

        //Methods



        //Relational Properties
        public virtual Reservation Reservation { get; set; }
    }
}