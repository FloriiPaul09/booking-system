using AutoMapper;
using Data.Entities;
using Logic.Models;

namespace Logic.Mappers
{
    public class ServiceModelMapper : Profile
    {
        public ServiceModelMapper()
        {
            CreateMap<Service, ServiceModel>().ReverseMap();
        }
    }
}
