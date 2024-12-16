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
        public ReservationsAppService(IReservationsRepository reservations, IReservationNotifierService reservationNotifierService)
        {
            _reservationsRepository = reservations;
            _reservationNotifier = reservationNotifierService;
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
        public async Task ExecuteNotifyReservationAsync(int reservationId)
        {
            var reservation = await _reservationsRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new KeyNotFoundException($"No se encontró una reserva con el ID {reservationId}.");

            await _reservationNotifier.NotifyReservationAsync(reservation);
        }
    }
}
