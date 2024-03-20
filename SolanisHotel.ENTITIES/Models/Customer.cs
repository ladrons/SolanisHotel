using SolanisHotel.ENTITIES.Models.Common;

namespace SolanisHotel.ENTITIES.Models
{
    public class Customer : BaseUser
    {
        //Backing fields



        //Constructor
        public Customer()
        {

        }


        //Properties


        //Methods


        //Relational Properties
        public virtual List<Reservation>? Reservations { get; set; }
    }
}
