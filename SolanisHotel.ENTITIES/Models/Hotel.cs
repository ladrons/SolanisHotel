using SolanisHotel.ENTITIES.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Models
{
    public class Hotel : BaseEntity
    {
        //Backing fields
        private string _name;
        private int _roomCount;


        //Constructor
        public Hotel()
        {

        }


        //Properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { _name = value; }
                else { throw new ArgumentException("Geçersiz otel ismi."); }
            }
        }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public int RoomCount
        {
            get { return _roomCount; }
            set
            {
                if (value >= 0) { _roomCount = value; }
                else { throw new ArgumentException("Geçersiz oda sayısı."); }
            }
        }


        //Methods



        //Relational Properties
        public virtual List<Room> Rooms { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}