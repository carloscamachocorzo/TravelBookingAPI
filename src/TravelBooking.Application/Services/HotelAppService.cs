using AutoMapper;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;


namespace TravelBooking.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para la creación de hoteles.
    /// Implementa la lógica necesaria para registrar nuevos hoteles en el sistema.
    /// </summary>
    public class HotelAppService : IHotelAppService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor para inicializar el servicio con el repositorio de hoteles.
        /// </summary>
        /// <param name="hotelRepository">
        /// Instancia del repositorio de hoteles que se utiliza para persistir los datos.
        /// </param>
        public HotelAppService(IHotelRepository hotelRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<RequestResult<CreateHotelResponseDto>> CreateHotel(CreateHotelDto request)
        {
            try
            {
                // Crear una nueva entidad de hotel
                var hotel = new Hotels
                {
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    Status = request.Status,
                    BaseRate = request.BaseRate,
                    Tax = request.Tax,
                    CreatedDate = DateTime.Now,
                    MaxCapacity = request.MaxCapacity
                };

                // Guardar el hotel en el repositorio
                await _hotelRepository.AddAsync(hotel);
                // Si la creación fue exitosa
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
                    BaseCost = roomDto.BaseCost,
                    Tax = roomDto.Tax,
                    HotelId = hotelId,
                    Status = roomDto.Status,
                }).ToList();

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
                // Si ocurrió un error
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

                return RequestResult<List<CreateHotelResponseDto>>.CreateSuccessful(hotelDtos);
            }
            catch (Exception ex)
            {
                return RequestResult<List<CreateHotelResponseDto>>.CreateError("Error retrieving hotels: " + ex.Message);
            }
        }
        public async Task<RequestResult<List<SearchHotelResponseDto>>> SearchHotelsAsync(SearchHotelsDto searchHotelsDto)
        {
            try
            {
                // Realiza la búsqueda en el repositorio o base de datos
                var hotels = await _hotelRepository.SearchHotelsAsync(
                    searchHotelsDto.CheckInDate,
                    searchHotelsDto.CheckOutDate,
                    searchHotelsDto.NumberOfGuests,
                    searchHotelsDto.DestinationCity
                );

                // Si no se encuentran hoteles
                if (hotels == null || !hotels.Any())
                {
                    return RequestResult<List<SearchHotelResponseDto>>.CreateUnsuccessful(new List<string> { "No hotels found for the given criteria" });
                }

                // Mapear las entidades de Hotel a HotelDto
                var hotelDtos = _mapper.Map<List<SearchHotelResponseDto>>(hotels);

                return RequestResult<List<SearchHotelResponseDto>>.CreateSuccessful(hotelDtos);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return RequestResult<List<SearchHotelResponseDto>>.CreateError($"Error searching hotels: {ex.Message}");
            }
        }
    }
}
