using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{

    /// <summary>
    /// Interface for managing room operations.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Asynchronously adds a collection of rooms to the repository.
        /// </summary>
        /// <param name="rooms">The collection of rooms to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<Rooms> rooms);

        /// <summary>
        /// Asynchronously retrieves a room by its identifier.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the room entity, or <c>null</c> if not found.</returns>
        Task<Rooms?> GetByIdAsync(int roomId);

        /// <summary>
        /// Asynchronously updates an existing room in the repository.
        /// </summary>
        /// <param name="room">The room entity to be updated.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Rooms room);

        /// <summary>
        /// Asynchronously retrieves a room by its identifier. This method returns the room in detail.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the room entity.</returns>
        Task<Rooms> GetRoomByIdAsync(int roomId);
    }

}
