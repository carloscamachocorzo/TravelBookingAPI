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

            // Mapeo de Reservaciones a DTO
            CreateMap<Reservations, ReservationDetailsDto>()
                .ForMember(dest => dest.HotelName,
                           opt => opt.MapFrom(src => src.Room != null ? src.Room.Hotel.Name : null))
                .ForMember(dest => dest.RoomName,
                           opt => opt.MapFrom(src => src.Room != null ? src.Room.Number : null));
            CreateMap<Reservations, ReservationDetailsDto>()
            // Propiedades que coinciden en nombre y tipo serán mapeadas automáticamente
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))  
            .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.Room.HotelId))    
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Room.Hotel.Name))  
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Number))  
            .ForMember(dest => dest.NumberOfGuests, opt => opt.MapFrom(src => src.TotalGuests))  
            .ReverseMap(); // Habilita el mapeo en ambas direcciones si lo necesitas


        }
    }
}
