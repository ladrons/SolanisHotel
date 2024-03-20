using SolanisHotel.ENTITIES.Enums;

namespace SolanisHotel.ENTITIES.Models.Common
{
    public abstract class BaseUser : BaseEntity
    {
        public BaseUser()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Inserted;
        }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public UserRole Role { get; set; }
    }
}