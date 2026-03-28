using Data.Repositories;
namespace Data
{

    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; }

        ICustomerRepository Customers { get; }

        IServiceRepository Services { get; }

        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _bookingDbContext;

        public IAppointmentRepository Appointments { get; }

        public ICustomerRepository Customers { get; }

        public IServiceRepository Services { get; }

        public UnitOfWork (BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
            Appointments = new AppointmentRepository (bookingDbContext);
            Customers = new CustomerRepository (bookingDbContext);
            Services = new ServiceRepository (bookingDbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _bookingDbContext.SaveChangesAsync();
        }

         public void Dispose()
        {
            _bookingDbContext.Dispose();
        }   
    }
}
