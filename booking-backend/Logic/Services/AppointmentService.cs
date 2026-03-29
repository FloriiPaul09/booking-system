using AutoMapper;
using Data;
using FluentValidation;
using Logic.Models;

namespace Logic.Services
{
    public interface IAppointmentService
    {
        Task CreateAppointmentAsync(AppointmentModel appointment);

        Task<AppointmentModel> GetAppointmentByIdAsync(int id);

        Task<IEnumerable<AppointmentModel>> GetAllAppointmentsAsync();

        Task UpdateAppointmentAsync(AppointmentModel appointment);

        Task DeleteAppointmentAsync(int id);

    }

    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AppointmentModel> _validator;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AppointmentModel> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }   

        public async Task CreateAppointmentAsync(AppointmentModel appointment)
        {
            var validationResult = await _validator.ValidateAsync(appointment);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var appointmentEntity = _mapper.Map<Data.Entities.Appointment>(appointment);
            await _unitOfWork.Appointments.AddAsync(appointmentEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AppointmentModel> GetAppointmentByIdAsync(int id)
        {
            var appointmentEntity = await _unitOfWork.Appointments.GetByIdAsync(id);
            return _mapper.Map<AppointmentModel>(appointmentEntity);
        }

        public async Task<IEnumerable<AppointmentModel>> GetAllAppointmentsAsync()
        {
            var appointmentEntities = await _unitOfWork.Appointments.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentModel>>(appointmentEntities);
        }

        public async Task UpdateAppointmentAsync(AppointmentModel appointment)
        {
            var validationResult = await _validator.ValidateAsync(appointment);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var appointmentEntity = _mapper.Map<Data.Entities.Appointment>(appointment);
            await _unitOfWork.Appointments.UpdateAsync(appointmentEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            await _unitOfWork.Appointments.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
