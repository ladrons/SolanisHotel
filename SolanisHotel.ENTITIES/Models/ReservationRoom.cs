using SolanisHotel.ENTITIES.Enums;
using SolanisHotel.ENTITIES.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Models
{
    public class ReservationRoom : BaseEntity
    {
        //Backing fields
        private DateTime _startDate;
        private DateTime _endDate;

        //Constructor
        public ReservationRoom()
        {

        }


        //Properties
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                // StartDate, bugünkü tarihten önce olamaz
                if (value < DateTime.Today)
                {
                    throw new ArgumentException("Başlangıç tarihi bugünkü tarihten önce olamaz.");
                }

                // EndDate'den küçük veya eşit bir tarih olmalıdır
                if (value < EndDate)
                {
                    throw new ArgumentException("Başlangıç tarihi, bitiş tarihinden önce ya da eşit olmalıdır.");
                }

                _startDate = value;
            }
        } //Rezervasyon başlangıç tarihi.
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                // EndDate, StartDate'den küçük veya eşit olamaz
                if (value < StartDate)
                {
                    throw new ArgumentException("Bitiş tarihi, başlangıç tarihinden küçük veya eşit olamaz.");
                }

                _endDate = value;
            }
        } //Rezervasyon bitiş tarihi.
        public bool ExtraBed { get; set; } //Ekstra yatak durumu.

        public int ReservationId { get; set; } //Bu veriyi kontrol eden rezervasyonun ID'si
        public int RoomId { get; set; } //Bu verinin ait olduğu odanın ID'si



        //Methods


        //Relational Properties
        public virtual Room Room { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}