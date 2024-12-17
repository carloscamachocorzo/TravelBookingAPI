using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    /// <summary>
    /// Interface for managing reservation operations.
    /// </summary>
    public interface IReservationsRepository
    {
        /// <summary>
        /// Asynchronously retrieves a reservation by its identifier.
        /// </summary>
        /// <param name="ReservationId">The unique identifier of the reservation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the reservation entity, or <c>null</c> if not found.</returns>
        Task<Reservations?> GetByIdAsync(int ReservationId);

        /// <summary>
        /// Asynchronously adds a new reservation to the repository.
        /// </summary>
        /// <param name="reservations">The reservation entity to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Reservations reservations);

        /// <summary>
        /// Asynchronously retrieves all reservations from the repository.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of all reservations.</returns>
        Task<List<Reservations>> GetAllAsync();

        /// <summary>
        /// Asynchronously creates a new reservation in the repository.
        /// </summary>
        /// <param name="reservation">The reservation entity to be created.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateAsync(Reservations reservation);
    }

}
