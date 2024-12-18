using AutoMapper;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Application.Dtos.Rooms;
using TravelBooking.Application.Dtos.Users;
using TravelBooking.Infraestructure;

namespace TravelBooking.Application.Automapper
{
    /// <summary>
    /// Defines the global mapping profile for AutoMapper, which contains mappings for different entities and DTOs.
    /// </summary>
    public sealed class GlobalMapperProfile : Profile
    {
        public GlobalMapperProfile() : base()
        {
            // Mapping between Hotels entity and UpdateHotelDto (bidirectional mapping)
            CreateMap<Hotels, UpdateHotelDto>().ReverseMap();

            // Mapping from Reservations to ReservationDetailsDto
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
                .ReverseMap(); // Enables bidirectional mapping

            // Mapping for Rooms to RoomResponseDto
            CreateMap<Rooms, RoomResponseDto>()
                // Mapping for RoomName (mapped from the 'Number' property in Rooms)
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Number))

                // Mapping for HotelName based on the related Hotel entity
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel != null ? src.Hotel.Name : string.Empty))

                // Mapping for RoomType (mapped from the 'Type' property in Rooms)
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Type))

                // Mapping for Rate as the sum of BaseCost and Tax
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.BaseCost + src.Tax))

                // Mapping for IsAvailable based on the room's status
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Status));

            // Mapping from Hotels to CreateHotelResponseDto
            CreateMap<Hotels, CreateHotelResponseDto>()
                // Mapping for the hotel ID
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HotelId));

            // Mapping from Hotels to SearchHotelResponseDto
            CreateMap<Hotels, SearchHotelResponseDto>()
                // Mapping for HotelName
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name));

            CreateMap<CreateUserDto, Users>();
            CreateMap<UpdateUserDto, Users>();
            CreateMap<Users, UserDto>();
        }
    }
}
