using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    /// <summary>
    /// Interface for managing hotel operations.
    /// </summary>
    public interface IHotelRepository
    {
        /// <summary>
        /// Asynchronously adds a new hotel to the repository.
        /// </summary>
        /// <param name="hotel">The hotel entity to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Hotels hotel);

        /// <summary>
        /// Asynchronously retrieves a hotel by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the hotel entity, or <c>null</c> if not found.</returns>
        Task<Hotels?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all hotels from the repository.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of all hotels.</returns>
        Task<IEnumerable<Hotels>> GetAllAsync();

        /// <summary>
        /// Asynchronously updates the details of an existing hotel.
        /// </summary>
        /// <param name="hotel">The hotel entity with updated details.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Hotels hotel);

        /// <summary>
        /// Asynchronously deletes a hotel by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Asynchronously searches for hotels based on the provided criteria.
        /// </summary>
        /// <param name="checkInDate">The check-in date for the search. If <c>null</c>, it is ignored.</param>
        /// <param name="checkOutDate">The check-out date for the search. If <c>null</c>, it is ignored.</param>
        /// <param name="totalGuests">The number of guests. If <c>null</c>, it is ignored.</param>
        /// <param name="city">The city where the hotel is located. If <c>null</c>, it is ignored.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of hotels that match the search criteria.</returns>
        Task<List<Hotels>> SearchHotelsAsync(DateOnly? checkInDate, DateOnly? checkOutDate, int? totalGuests, string? city);
    }

}
