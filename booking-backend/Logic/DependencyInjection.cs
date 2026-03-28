using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogicServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options =>
                options.UseNpgsql(configuration
                    .GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
 