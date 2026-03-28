using Data;
using Data.Repositories;
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

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();    
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }

    }
}
 