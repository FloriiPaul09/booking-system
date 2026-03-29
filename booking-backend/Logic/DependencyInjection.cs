using Data;
using Data.Repositories;
using FluentValidation;
using Logic.Mappers;
using Logic.Models;
using Logic.Services;
using Logic.Validators;
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
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ICustomerService, CustomerService>();    
            services.AddScoped<IServiceService, ServiceService>();

            //Mapper dependencies injection
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CustomerModelMapper>();
                cfg.AddProfile<ServiceModelMapper>();
                cfg.AddProfile<AppointmentModelMapper>();
            });

            //Validator dependencies injection
            services.AddScoped<IValidator<AppointmentModel>, AppointmentValidator>();
            services.AddScoped<IValidator<CustomerModel>, CustomerValidator>();
            services.AddScoped<IValidator<ServiceModel>, ServiceValidator>();


            return services;
        }

    }
}
 