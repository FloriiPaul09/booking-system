using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{

    public interface IAppointmentRepository
    {
        Task AddAsync (Appointment appointment);

        Task<Appointment> GetByIdAsync (int id);

        Task<IEnumerable<Appointment>> GetAllAsync ();

        Task UpdateAsync (Appointment appointment);

        Task DeleteAsync (int id);
    }


    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly BookingDbContext _bookingDbContext;

        public AppointmentRepository (BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }

        public async Task AddAsync (Appointment appointment)
        {
            await _bookingDbContext.Appointments.AddAsync (appointment);
            await _bookingDbContext.SaveChangesAsync ();
        }

        public async Task<Appointment> GetByIdAsync (int id)
        {
            return await _bookingDbContext.Appointments.FindAsync(id)
                ?? throw new KeyNotFoundException($"Appointment with id {id} not found");
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync ()
        {
            return await _bookingDbContext.Appointments.ToListAsync ();
        }

        public async Task UpdateAsync (Appointment appointment)
        {
            _bookingDbContext.Appointments.Update (appointment);
            await _bookingDbContext.SaveChangesAsync ();
        }

        public async Task DeleteAsync (int id)
        {
            var appointment = await _bookingDbContext.Appointments.FindAsync (id);
            if (appointment != null)
            {
                _bookingDbContext.Appointments.Remove (appointment);
                await _bookingDbContext.SaveChangesAsync ();
            }
        }

    }
}
