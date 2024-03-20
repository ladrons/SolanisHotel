using Microsoft.Extensions.DependencyInjection;
using SolanisHotel.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.DependencyResolvers
{
    public class SeedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                SolanisHotelContext context = scope.ServiceProvider.GetRequiredService<SolanisHotelContext>();

                await SolanisHotelSeed.SeedAsync(context);
            }
        }
    }
}