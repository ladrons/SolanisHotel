using SolanisHotel.ENTITIES.Models.Common;

namespace SolanisHotel.ENTITIES.Models
{
    public class Employee : BaseUser
    {
        //Backing fields


        //Constructor
        public Employee()
        {

        }

        //Properties
        public string Username { get; set; }

        public string Title { get; set; }
        public decimal Salary { get; set; }

        public int HotelId { get; set; }


        //Methods


        //Relational Properties
        public virtual Hotel Hotel { get; set; }
    }
}