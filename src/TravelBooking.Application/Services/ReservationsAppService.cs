using AutoMapper;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Domain.Services.Interfaces;
using TravelBooking.Infraestructure;
using TravelBooking.Infraestructure.Repositories;

namespace TravelBooking.Application.Services
{
    public class ReservationsAppService : IReservationsAppService
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IReservationNotifierService _reservationNotifier;
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ReservationsAppService(IReservationsRepository reservations, IReservationNotifierService reservationNotifierService,
            IRoomRepository roomRepository,
        IHotelRepository hotelRepository, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _reservationsRepository = reservations;
            _reservationNotifier = reservationNotifierService;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
        }
        public async Task<RequestResult<IEnumerable<ReservationResponseDto>>> ExecuteAsync()
        {
            try
            {
                var reservations = await _reservationsRepository.GetAllAsync();
                var result = reservations.Select(r => new ReservationResponseDto
                {
                    ReservationId = r.ReservationId,
                    RoomId = r.RoomId,
                    UserId = r.UserId,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    TotalGuests = r.TotalGuests

                });
                return RequestResult<IEnumerable<ReservationResponseDto>>.CreateSuccessful(result, new List<string> { "query done successfully" });
            }
            catch (Exception ex)
            {
                // Si ocurrió un error
                return RequestResult<IEnumerable<ReservationResponseDto>>.CreateError("Error al crear el hotel: " + ex.Message);
            }

        }
        public async Task<RequestResult<ReservationDetailsDto>> GetReservationByIdAsync(int reservationId)
        {
            try
            {
                var reservation = await _reservationsRepository.GetByIdAsync(reservationId);

                if (reservation == null)
                {
                    return RequestResult<ReservationDetailsDto>.CreateError($"Reservation with ID {reservationId} not found.");
                }

                var reservationDto = _mapper.Map<ReservationDetailsDto>(reservation);

                return RequestResult<ReservationDetailsDto>.CreateSuccessful(reservationDto);
            }
            catch (Exception ex)
            {
                return RequestResult<ReservationDetailsDto>.CreateError($"An error occurred while retrieving the reservation: {ex.Message}");
            }
        }
        public async Task<RequestResult<ReservationDetailsDto>> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            try
            {
                // Validar existencia del usuario
                var user = await _userRepository.GetByIdAsync(createReservationDto.UserId);
                if (user == null)
                {
                    return RequestResult<ReservationDetailsDto>.CreateError($"User with ID {createReservationDto.UserId} not found.");
                }

                // Validar existencia de la habitación
                var room = await _roomRepository.GetByIdAsync(createReservationDto.RoomId);
                if (room == null)
                {
                    return RequestResult<ReservationDetailsDto>.CreateError($"Room with ID {createReservationDto.RoomId} not found.");
                }

                //if (room.Capacity < createReservationDto.TotalGuests)
                //{
                //    return RequestResult<ReservationDetailsDto>.CreateError($"Room capacity ({room.Capacity}) is less than the total guests.");
                //}

                // Calcular el costo total
                var totalDays = (createReservationDto.CheckOutDate.ToDateTime(TimeOnly.MinValue) - createReservationDto.CheckInDate.ToDateTime(TimeOnly.MinValue)).Days;
                var totalCost = room.BaseCost * totalDays;

                // Crear entidad de reserva
                var reservation = new Reservations
                {
                    RoomId = createReservationDto.RoomId,
                    UserId = createReservationDto.UserId,
                    CheckInDate = createReservationDto.CheckInDate,
                    CheckOutDate = createReservationDto.CheckOutDate,
                    TotalGuests = createReservationDto.TotalGuests,
                    ReservationDate = DateTime.UtcNow,
                    TotalCost = totalCost,
                    EmergencyContacts = createReservationDto.EmergencyContacts?
                        .Select(ec => new EmergencyContacts { FullName = ec.Name, PhoneNumber = ec.Phone })
                        .ToList(),
                    Guests = createReservationDto.Guests?
                        .Select(g => new Guests
                        {
                            FirstName = g.FirstName,
                            LastName = g.LastName,
                            BirthDate = g.BirthDate,
                            Gender = g.Gender,
                            DocumentType = g.DocumentType,
                            DocumentNumber = g.DocumentNumber,
                            Email = g.Email,
                            PhoneNumber = g.PhoneNumber
                        })
                        .ToList()
                };

                // Guardar en la base de datos
                await _reservationsRepository.CreateAsync(reservation);

                // Mapear la reserva al DTO de respuesta
                var reservationDetails = _mapper.Map<ReservationDetailsDto>(reservation);

                return RequestResult<ReservationDetailsDto>.CreateSuccessful(reservationDetails);
            }
            catch (Exception ex)
            {
                return RequestResult<ReservationDetailsDto>.CreateError($"An error occurred while creating the reservation: {ex.Message}");
            }
        }
        public async Task ExecuteNotifyReservationAsync(int reservationId)
        {
            var reservation = await _reservationsRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new KeyNotFoundException($"No se encontró una reserva con el ID {reservationId}.");

            await _reservationNotifier.NotifyReservationAsync(reservation);
        }
        private decimal CalculateTotalCost(decimal baseRate, DateTime checkInDate, DateTime checkOutDate)
        {
            var numberOfNights = (checkOutDate - checkInDate).Days;
            return baseRate * numberOfNights;
        }
    }
}
