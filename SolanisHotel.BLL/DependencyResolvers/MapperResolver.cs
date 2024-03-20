using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SolanisHotel.BLL.MapperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.DependencyResolvers
{
    public static class MapperResolver
    {
        public static void AddMapperService(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ReservationProfile());
                opt.AddProfile(new RoomProfile());
                opt.AddProfile(new CustomerProfile());
                opt.AddProfile(new PaymentProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}