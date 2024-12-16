using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Reservation;

namespace TravelBooking.Application.Services.Interfaces
{
    /// <summary>
    /// Defines the operations available for managing reservations.
    /// </summary>
    public interface IReservationsAppService
    {
        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        /// <returns>A collection of reservation details.</returns>
        Task<RequestResult<IEnumerable<ReservationResponseDto>>> ExecuteAsync();

        /// <summary>
        /// Sends a notification for a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation to notify.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ExecuteNotifyReservationAsync(int reservationId);

        /// <summary>
        /// Retrieves the details of a specific reservation by its identifier.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>The details of the specified reservation.</returns>
        Task<RequestResult<ReservationDetailsDto>> GetReservationByIdAsync(int reservationId);

        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        /// <param name="createReservationDto">The details of the reservation to create.</param>
        /// <returns>The details of the newly created reservation.</returns>
        Task<RequestResult<ReservationDetailsDto>> CreateReservationAsync(CreateReservationDto createReservationDto);
    }

}
