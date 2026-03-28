using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IServiceRepository
    {
        Task AddAsync (Service service);

        Task<Service> GetByIdAsync (int id);

        Task<IEnumerable<Service>> GetAllAsync ();

        Task UpdateAsync (Service service);

        Task DeleteAsync (int id);
    }

    public class ServiceRepository : IServiceRepository
    {
        private readonly BookingDbContext _bookingDbContext;

        public ServiceRepository (BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }

        public async Task AddAsync (Service service)
        {
            await _bookingDbContext.Services.AddAsync (service);
            await _bookingDbContext.SaveChangesAsync ();
        }

        public async Task<Service> GetByIdAsync (int id)
        {
            return await _bookingDbContext.Services.FindAsync(id)
                ?? throw new KeyNotFoundException($"Service with id {id} not found");
        }

        public async Task<IEnumerable<Service>> GetAllAsync ()
        {
            return await _bookingDbContext.Services.ToListAsync ();
        }

        public async Task UpdateAsync (Service service)
        {
            _bookingDbContext.Services.Update (service);
            await _bookingDbContext.SaveChangesAsync ();
        }

        public async Task DeleteAsync (int id)
        {
            var service = await _bookingDbContext.Services.FindAsync (id);
            if (service != null)
            {
                _bookingDbContext.Services.Remove (service);
                await _bookingDbContext.SaveChangesAsync ();
            }
        }
    }
}
