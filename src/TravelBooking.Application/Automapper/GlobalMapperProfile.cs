using AutoMapper;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Infraestructure;

namespace TravelBooking.Application.Automapper
{
    public sealed class GlobalMapperProfile : Profile
    {
        public GlobalMapperProfile() : base()
        {
            CreateMap<Hotels, UpdateHotelDto>().ReverseMap();
            CreateMap<Reservations, ReservationDetailsDto>()
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Room.Hotel.Name))
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Number));

        }
    }
}
