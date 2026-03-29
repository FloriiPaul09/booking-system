using AutoMapper;
using Data.Entities;
using Logic.Models;

namespace Logic.Mappers
{
    public class CustomerModelMapper : Profile
    {
        public CustomerModelMapper()
        {
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
