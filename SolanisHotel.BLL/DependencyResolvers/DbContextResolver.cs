using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolanisHotel.DAL.Context;


namespace SolanisHotel.BLL.DependencyResolvers
{
    public static class DbContextResolver
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            IConfiguration configuration = provider.GetService<IConfiguration>();

            services.AddDbContextPool<SolanisHotelContext>(opt => opt.UseSqlServer(configuration.GetConnectionString
                ("MyConnection")).UseLazyLoadingProxies());

            return services;
        }
    }
}
