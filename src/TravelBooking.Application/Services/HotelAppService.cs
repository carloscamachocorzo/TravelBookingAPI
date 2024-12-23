using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;


namespace TravelBooking.Application.Services
{
    public class HotelAppService : IHotelAppService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelAppService> _logger;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "HotelAppService";
        /// <summary>
        /// Initializes a new instance of the <see cref="HotelAppService"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository used for hotel data operations.</param>
        /// <param name="roomRepository">The repository used for room data operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entities and DTOs.</param>
        /// <param name="logger">The logger instance for logging service-related information.</param>

        public HotelAppService(IHotelRepository hotelRepository, IRoomRepository roomRepository, IMapper mapper, ILogger<HotelAppService> logger)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequestResult<CreateHotelResponseDto>> CreateHotel(CreateHotelRequestsDto request)
        {
            try
            {

                var hotel = new Hotels
                {
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    Status = request.Status,
                    BaseRate = request.BaseRate,
                    Tax = request.Tax,
                    CreatedDate = DateTime.Now,
                    MaxCapacity = (int)request.MaxCapacity
                };


                await _hotelRepository.AddAsync(hotel);

                return RequestResult<CreateHotelResponseDto>.CreateSuccessful(new CreateHotelResponseDto
                {
                    Id = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    City = hotel.City
                }, new List<string> { "hotel created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing hotel data.");
                // Si ocurrió un error
                return RequestResult<CreateHotelResponseDto>.CreateError("Error al crear el hotel: " + ex.Message);
            }
        }

        public async Task<RequestResult<bool>> AssignRoomsToHotel(int hotelId, CreateRoomsRequest request)
        {
            try
            {
                // Verificar que el hotel exista
                var hotel = await _hotelRepository.GetByIdAsync(hotelId);
                if (hotel == null)
                {

                    return RequestResult<bool>.CreateUnsuccessful(new List<string> { $"Hotel with ID {hotelId} was not found." });
                }
                // Crear las habitaciones asociadas al hotel
                var rooms = request.Rooms.Select(roomDto => new Rooms
                {
                    Number = roomDto.Number,
                    Type = roomDto.Type,
                    Location = roomDto.Location,
                    baseRate = roomDto.BaseRate,
                    Tax = roomDto.Tax,
                    HotelId = hotelId,
                    Status = roomDto.Status,
                    MaxCapacity = roomDto.MaxCapacity
                }).ToList();
                hotel.MaxCapacity = hotel.MaxCapacity + request.Rooms.Sum(r => r.MaxCapacity);
                await _hotelRepository.UpdateAsync(hotel);
                await _roomRepository.AddRangeAsync(rooms);

                return RequestResult<bool>.CreateSuccessful(true, new List<string> { "Room assignment done successfully" });
            }
            catch (Exception ex)
            {
                // Si ocurrió un error
                return RequestResult<bool>.CreateError("Error al crear el hotel: " + ex.Message);
            }
        }
        public async Task<RequestResult<bool>> UpdateHotelAsync(int hotelId, UpdateHotelDto updateHotelDto)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(hotelId);

                if (hotel == null)
                {
                    throw new KeyNotFoundException("Hotel not found.");
                }

                // Actualizar los valores del hotel
                _mapper.Map(updateHotelDto, hotel);
                // Guardar cambios en el repositorio
                await _hotelRepository.UpdateAsync(hotel);
                return RequestResult<bool>.CreateSuccessful(true, new List<string> { "hotel updated successfully" });
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.CreateError("Error al actualizar el hotel: " + ex.Message);
            }

        }
        public async Task<RequestResult<bool>> UpdateHotelStatusAsync(int hotelId, bool status)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(hotelId);

                if (hotel == null)
                {
                    return RequestResult<bool>.CreateError("Hotel not found.");
                }

                hotel.Status = status;

                await _hotelRepository.UpdateAsync(hotel);

                return RequestResult<bool>.CreateSuccessful(true, new List<string> { "Hotel status updated successfully" });
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.CreateError("Error updating hotel status: " + ex.Message);
            }
        }
        public async Task<RequestResult<List<CreateHotelResponseDto>>> GetAllHotelsAsync()
        {
            try
            {
                var hotels = await _hotelRepository.GetAllAsync();

                if (!hotels.Any())
                {
                    return RequestResult<List<CreateHotelResponseDto>>.CreateUnsuccessful(new List<string> { "No hotels found." });
                }

                var hotelDtos = _mapper.Map<List<CreateHotelResponseDto>>(hotels);
                _logger.LogInformation("list of loaded hotels successfully");
                return RequestResult<List<CreateHotelResponseDto>>.CreateSuccessful(hotelDtos, new List<string> { "list of loaded hotels" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod())?.Name + (new StackFrame().GetFileLineNumber()));
                return RequestResult<List<CreateHotelResponseDto>>.CreateError("Error retrieving hotels: " + ex.Message);
            }
        }
        public async Task<RequestResult<List<SearchHotelResponseDto>>> SearchHotelsAsync(SearchHotelsDto searchHotelsDto)
        {
            try
            {
                var hotels = await _hotelRepository.SearchHotelsAsync(
                    searchHotelsDto.CheckInDate,
                    searchHotelsDto.CheckOutDate,
                    searchHotelsDto.NumberOfGuests,
                    searchHotelsDto.DestinationCity
                );

                if (hotels == null || !hotels.Any())
                {
                    return RequestResult<List<SearchHotelResponseDto>>.CreateUnsuccessful(new List<string> { "No hotels found for the given criteria" });
                }


                var hotelDtos = hotels.Select(hotel => new SearchHotelResponseDto
                {
                    HotelName = hotel.Name,
                    Address = hotel.Address,
                    City = hotel.City,
                    BaseRate = hotel.BaseRate,
                    Tax = hotel.Tax,
                    Rooms = hotel.Rooms
                            .Where(r =>
                            r.MaxCapacity >= searchHotelsDto.NumberOfGuests &&
                            r.Status &&
                            !r.Reservations.Any(reservation =>
                                searchHotelsDto.CheckInDate < reservation.CheckOutDate &&
                                searchHotelsDto.CheckOutDate > reservation.CheckInDate))
                            .Select(room => new SearchHotelRoomResponseDto
                            {
                                Number = room.Number,
                                Type = room.Type,
                                MaxCapacity = room.MaxCapacity,
                                Location = room.Location,
                                BaseRate = room.baseRate,
                                Tax = room.Tax,
                                Status = room.Status
                            })
                            .ToList()
                }).ToList();

                return RequestResult<List<SearchHotelResponseDto>>.CreateSuccessful(hotelDtos);
            }
            catch (Exception ex)
            {
                return RequestResult<List<SearchHotelResponseDto>>.CreateError($"Error searching hotels: {ex.Message}");
            }
        }
    }
}
