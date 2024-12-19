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
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsAppService"/> class.
        /// </summary>
        /// <param name="reservations">The repository that manages reservations.</param>
        /// <param name="reservationNotifierService">The service responsible for sending notifications related to reservations.</param>
        /// <param name="roomRepository">The repository that manages room data.</param>
        /// <param name="hotelRepository">The repository that manages hotel data.</param>
        /// <param name="mapper">The instance of <see cref="IMapper"/> for mapping between entities and DTOs.</param>
        /// <param name="userRepository">The repository that manages user data.</param>
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
                if (!reservations.Any())
                {
                    return RequestResult<IEnumerable<ReservationResponseDto>>.CreateUnsuccessful(new List<string> { "no results found" });
                }
                var result = reservations.Select(r => new ReservationResponseDto
                {
                    ReservationId = r.ReservationId,
                    RoomId = r.RoomId,
                    UserId = r.UserId,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    TotalGuests = r.TotalGuests,
                    TotalCost = r.TotalCost,
                    ReservationDate = r.ReservationDate,
                    EmergencyContacts = r.EmergencyContacts.Select(m => new EmergencyContactsResponseDto
                    {
                        EmergencyContactId = m.EmergencyContactId,
                        FullName = m.FullName,
                        PhoneNumber = m.PhoneNumber,
                        ReservationId = m.ReservationId
                    }).ToList()

                });
                return RequestResult<IEnumerable<ReservationResponseDto>>.CreateSuccessful(result, new List<string> { "query done successfully" });
            }
            catch (Exception ex)
            {
                // Si ocurrió un error
                return RequestResult<IEnumerable<ReservationResponseDto>>.CreateError("Error al crear el hotel: " + ex.Message);
            }

        }
        public async Task<RequestResult<ReservationDetailsResponseDto>> GetReservationByIdAsync(int reservationId)
        {
            try
            {
                var reservation = await _reservationsRepository.GetByIdAsync(reservationId);

                if (reservation == null)
                {
                    return RequestResult<ReservationDetailsResponseDto>.CreateError($"Reservation with ID {reservationId} not found.");
                }

                var reservationDto = _mapper.Map<ReservationDetailsResponseDto>(reservation);

                return RequestResult<ReservationDetailsResponseDto>.CreateSuccessful(reservationDto);
            }
            catch (Exception ex)
            {
                return RequestResult<ReservationDetailsResponseDto>.CreateError($"An error occurred while retrieving the reservation: {ex.Message}");
            }
        }
        public async Task<RequestResult<ReservationDetailsResponseDto>> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            try
            {
                // Validate user existence
                var user = await _userRepository.GetByIdAsync(createReservationDto.UserId);
                if (user == null)
                {
                    return RequestResult<ReservationDetailsResponseDto>.CreateError($"User with ID {createReservationDto.UserId} not found.");
                }

                // Validate room existence
                var room = await _roomRepository.GetByIdAsync(createReservationDto.RoomId);
                if (room == null)
                {
                    return RequestResult<ReservationDetailsResponseDto>.CreateError($"Room with ID {createReservationDto.RoomId} not found.");
                }

                if (room.MaxCapacity < createReservationDto.TotalGuests)
                {
                    return RequestResult<ReservationDetailsResponseDto>.CreateError($"Room capacity ({room.MaxCapacity}) is less than the total guests.");
                }

                // Calculate the total number of days
                var totalDays = (createReservationDto.CheckOutDate.ToDateTime(TimeOnly.MinValue) - createReservationDto.CheckInDate.ToDateTime(TimeOnly.MinValue)).Days;

                // Calculate the daily cost including tax
                var dailyRateWithTax = room.baseRate + (room.baseRate * room.Tax); 

                // Calculate the total cost by multiplying by the days
                var totalCost = dailyRateWithTax * totalDays;


                // Crear entidad de reserva
                var reservation = new Reservations
                {

                    RoomId = createReservationDto.RoomId,
                    UserId = createReservationDto.UserId,
                    CheckInDate = createReservationDto.CheckInDate,
                    CheckOutDate = createReservationDto.CheckOutDate,
                    TotalGuests = createReservationDto.TotalGuests,
                    ReservationDate = DateTime.Now,
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
                reservation.Room = room;
                // Mapear la reserva al DTO de respuesta
                var reservationDetails = _mapper.Map<ReservationDetailsResponseDto>(reservation);

                return RequestResult<ReservationDetailsResponseDto>.CreateSuccessful(reservationDetails);
            }
            catch (Exception ex)
            {
                return RequestResult<ReservationDetailsResponseDto>.CreateError($"An error occurred while creating the reservation: {ex.Message}");
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
