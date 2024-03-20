using SolanisHotel.ENTITIES.Enums;
using SolanisHotel.ENTITIES.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Models
{
    public class Reservation : BaseEntity
    {
        //Backing fields



        //Constructor
        public Reservation()
        {

        }


        //Properties
        public DateTime ReservationDate { get; set; } //Rezervasyonun oluşturulduğu tarih.
        public decimal TotalPrice { get; set; } //Rezervasyonun toplam fiyatı.        
        public byte NumberOfGuests { get; set; } //Toplam müşteri sayısı.
        public byte NumberOfRooms { get; set; } //Toplam oda sayısı.
        public byte NumberOfExtraBeds { get; set; } //Gerekli ekstra yatak sayısı.

        public ReservationStatus ReservationStatus { get; set; } //Rezervasyon durumu.

        public int CustomerId { get; set; } //Rezervasyonun ait olduğu müşteri ID'si

        //Methods



        //Relational Properties
        public virtual Customer Customer { get; set; }
        public virtual Payment Payment { get; set; }

        public virtual List<ReservationRoom> ReservationRooms { get; set; }
    }
}