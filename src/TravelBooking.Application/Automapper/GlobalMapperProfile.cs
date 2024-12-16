using AutoMapper;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Infraestructure;

namespace TravelBooking.Application.Automapper
{
    public sealed class GlobalMapperProfile : Profile
    {
        public GlobalMapperProfile() : base()
        {
            CreateMap<Hotels, UpdateHotelDto>().ReverseMap();
        }
    }
}
