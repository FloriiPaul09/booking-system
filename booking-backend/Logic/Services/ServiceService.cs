using AutoMapper;
using Data;
using FluentValidation;
using Logic.Models;

namespace Logic.Services
{
    public interface IServiceService
    {
        Task AddServiceAsync(ServiceModel service);

        Task<ServiceModel> GetServiceByIdAsync(int id);

        Task<IEnumerable<ServiceModel>> GetAllServicesAsync();

        Task UpdateServiceAsync(ServiceModel service);

        Task DeleteServiceAsync(int id);
    }

    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ServiceModel> _validator;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ServiceModel> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task AddServiceAsync(ServiceModel service)
        {
            var validationResult = await _validator.ValidateAsync(service);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var serviceEntity = _mapper.Map<Data.Entities.Service>(service);
            await _unitOfWork.Services.AddAsync(serviceEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ServiceModel> GetServiceByIdAsync(int id)
        {
            var serviceEntity = await _unitOfWork.Services.GetByIdAsync(id);
            return _mapper.Map<ServiceModel>(serviceEntity);
        }

        public async Task<IEnumerable<ServiceModel>> GetAllServicesAsync()
        {
            var serviceEntities = await _unitOfWork.Services.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceModel>>(serviceEntities);
        }

        public async Task UpdateServiceAsync(ServiceModel service)
        {
            var validationResult = await _validator.ValidateAsync(service);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var serviceEntity = _mapper.Map<Data.Entities.Service>(service);
            await _unitOfWork.Services.UpdateAsync(serviceEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            await _unitOfWork.Services.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
