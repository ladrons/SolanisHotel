using Microsoft.EntityFrameworkCore;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.DAL.Context
{
    public class SolanisHotelSeed
    {
        public static async Task SeedAsync(SolanisHotelContext context, int? retry = 0)
        {
			try
			{
				context.Database.Migrate();

				if (!context.Hotels.Any())
				{
					context.Hotels.Add(GetPreConfigureHotel());
					await context.SaveChangesAsync();
				}

				if (!context.Customers.Any())
				{
					context.Customers.Add(GetPreConfigureCustomer());
					await context.SaveChangesAsync();
				}

				if (!context.Rooms.Any())
				{
					context.Rooms.AddRange(GetPreConfigureRoom());
					await context.SaveChangesAsync();
				}
			}
			catch (Exception)
			{
				throw;
			}
        }

		//-----//

		private static Hotel GetPreConfigureHotel()
		{
			return new Hotel
			{
                Name = "Solanis Hotel",
                Address = "Oteller Cd. NO:171 İstanbul/Beykoz",
                PhoneNumber = "1234567890",
                Description = "Description",
                RoomCount = 50
            };
		}
		private static Customer GetPreConfigureCustomer()
		{
			return new Customer
			{
				Password = "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08",
				Email = "test@info.com",
				PhoneNumber = "+905112223344",

                FirstName = "Test",
				LastName = "Verisi",
				BirthDate = DateTime.Now,

				Role = ENTITIES.Enums.UserRole.Customer
			};
		}

		private static List<Room> GetPreConfigureRoom()
		{
			List<Room> newRooms = new List<Room>();

			for (int i = 100; i < 120; i++)
			{
				if (i <= 105)
				{
					Room newRoom = new Room
					{
						RoomNumber = i,
						Description = "Description",
						Price = 10,
						Capacity = 2,

						HotelId = 1
					};
					newRooms.Add(newRoom);
				}
				else if (i > 105 && i <= 110)
				{
                    Room newRoom = new Room
                    {
                        RoomNumber = i,
                        Description = "Description",
						Price = 15,
						Capacity = 2,

						HotelId = 1,
                    };
                    newRooms.Add(newRoom);
                }
				else
				{
                    Room newRoom = new Room
                    {
                        RoomNumber = i,
                        Description = "Description",
                        Price = 20,
                        Capacity = 2,

                        HotelId = 1,
                    };
                    newRooms.Add(newRoom);
                }
			}

			return newRooms;
		}
    }
}