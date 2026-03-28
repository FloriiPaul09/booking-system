using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync (Customer customer);

        Task<Customer> GetByIdAsync (int id);

        Task<IEnumerable<Customer>> GetAllAsync ();

        Task UpdateAsync (Customer customer);

        Task DeleteAsync (int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookingDbContext _bookingDbContext;

        public CustomerRepository (BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }

        public async Task AddAsync (Customer customer)
        {
            await _bookingDbContext.Customers.AddAsync (customer);
            await _bookingDbContext.SaveChangesAsync ();
        }

        public async Task<Customer> GetByIdAsync (int id)
        {
            return await _bookingDbContext.Customers.FindAsync(id)
                ?? throw new KeyNotFoundException($"Customer with id {id} not found");
        }

        public async Task<IEnumerable<Customer>> GetAllAsync ()
        {
            return await _bookingDbContext.Customers.ToListAsync ();
        }

        public async Task UpdateAsync (Customer customer)
        {
            _bookingDbContext.Customers.Update (customer);
            await _bookingDbContext.SaveChangesAsync ();
        }

        public async Task DeleteAsync (int id)
        {
            var customer = await _bookingDbContext.Customers.FindAsync (id);
            if (customer != null)
            {
                _bookingDbContext.Customers.Remove (customer);
                await _bookingDbContext.SaveChangesAsync ();
            }
        }
    }
}
