using AutoMapper;
using Data;
using FluentValidation;
using Logic.Models;

namespace Logic.Services
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerModel customer);

        Task<CustomerModel> GetCustomerByIdAsync(int id);

        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();

        Task UpdateCustomerAsync(CustomerModel customer);

        Task DeleteCustomerAsync(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerModel> _validator;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerModel> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task AddCustomerAsync(CustomerModel customer) 
        {
            var validationResult = await _validator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var customerEntity = _mapper.Map<Data.Entities.Customer>(customer);
            await _unitOfWork.Customers.AddAsync(customerEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            var customerEntity = await _unitOfWork.Customers.GetByIdAsync(id);
            return _mapper.Map<CustomerModel>(customerEntity);
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            var customerEntities = await _unitOfWork.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerModel>>(customerEntities);
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            var validationResult = await _validator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var customerEntity = _mapper.Map<Data.Entities.Customer>(customer);
            await _unitOfWork.Customers.UpdateAsync(customerEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _unitOfWork.Customers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
