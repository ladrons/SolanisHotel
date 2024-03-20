using Microsoft.Extensions.DependencyInjection;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.Managers.Concretes;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.DAL.Repositories.Concretes;

namespace SolanisHotel.BLL.DependencyResolvers
{
    public static class RegistrationResolver
    {
        public static IServiceCollection AddRegistrations(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IReservationRoomRepository, ReservationRoomRepository>();

            services.AddScoped(typeof(IManager<>), typeof(BaseManager<>));
            services.AddScoped<IHotelManager, HotelManager>();
            services.AddScoped<IRoomManager, RoomManager>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            services.AddScoped<IReservationManager, ReservationManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IReservationRoomManager, ReservationRoomManager>();

            return services;
        }
    }
}
