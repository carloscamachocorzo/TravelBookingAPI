using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface for handling reservation notifications.
    /// </summary>
    public interface IReservationNotifierService
    {
        /// <summary>
        /// Asynchronously sends a notification for a given reservation.
        /// </summary>
        /// <param name="reservation">The reservation for which the notification will be sent.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task NotifyReservationAsync(Reservations reservation);
    }

}
