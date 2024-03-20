using SolanisHotel.ENTITIES.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Models
{
    public class Room : BaseEntity
    {
        //Backing fields
        private int _roomNumber;
        private decimal _price;


        //Constructor
        public Room()
        {
            IsOccupied = false;
            Availability = true;
        }


        //Properties
        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (value >= 100) { _roomNumber = value; }
                else { throw new ArgumentException("Hatalı oda numarası."); }
            }
        } //Oda numarası.
        public string Description { get; set; } //Oda ile ilgili açıklama.
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value > 0) { _price = value; }
                else { throw new ArgumentException("Hatalı oda fiyatı."); }
            }
        } //Odanın fiyatı. (Fiyat günlüktür)
        public byte Capacity { get; set; } //Oda kapasitesi. (Ekstra yatak dahil değil)

        public bool IsOccupied { get; set; } //Oda doluluk durumu. (True = Dolu \ False = Boş)
        public bool Availability { get; set; } // Oda uygunluk durumu. (False ise bu oda kiralanamaz)

        public int HotelId { get; set; } //Odanın bulunduğu otelin ID'si.


        //Methods



        //Relational Properties
        public virtual Hotel Hotel { get; set; }

        public virtual List<ReservationRoom> ReservationRooms { get; set; }
    }
}