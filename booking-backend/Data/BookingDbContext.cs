using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
    }
}