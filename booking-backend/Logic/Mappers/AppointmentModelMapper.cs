using AutoMapper;
using Data.Entities;
using Logic.Models;

namespace Logic.Mappers
{
    public class AppointmentModelMapper : Profile
    {
        public AppointmentModelMapper()
        {
            CreateMap<Appointment, AppointmentModel>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => (AppointmentModelStatus)src.Status))
                .ReverseMap();
        }
    }
}
