using AutoMapper;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Application.Dtos.Rooms;
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
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))  
            .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.Room.HotelId))    
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Room.Hotel.Name))  
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Number))  
            .ForMember(dest => dest.NumberOfGuests, opt => opt.MapFrom(src => src.TotalGuests))  
            .ReverseMap(); // Habilita el mapeo en ambas direcciones 

            // Configuración para mapear Room a RoomResponseDto
            CreateMap<Rooms, RoomResponseDto>()
            // Mapeo explícito de RoomName (propiedad 'Number' en Rooms)
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Number))

            // Mapeo explícito de HotelName desde la entidad Hotel relacionada
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel != null ? src.Hotel.Name : string.Empty))

            // Mapeo explícito de RoomType (propiedad 'Type' en Rooms)
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Type))
             

            // Calcular Rate como la suma de BaseCost + Tax
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.BaseCost + src.Tax))

            // Mapeo de IsAvailable basado en el estado de la habitación
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Status));

            CreateMap<Hotels, CreateHotelResponseDto>()
            // Mapeo explícito del identificador del hotel
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HotelId));

            CreateMap<Hotels, SearchHotelResponseDto>()
            // Mapeo explícito para Name -> HotelName
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name));

        }
    }
}
