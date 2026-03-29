using Data;
using Data.Repositories;
using Logic.Mappers;
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

            //Repository dependencies injection
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();    
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Service dependencies injection

            //Mapper dependencies injection
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CustomerModelMapper>();
                cfg.AddProfile<ServiceModelMapper>();
                cfg.AddProfile<AppointmentModelMapper>();
            });

            //Validator dependencies injection


            return services;
        }

    }
}
 