using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    /// <summary>
    /// Interface for managing user operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Asynchronously retrieves a user by their identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the user entity, or <c>null</c> if not found.</returns>
        Task<Users?> GetByIdAsync(int userId);

        /// <summary>
        /// Asynchronously checks if a user exists in the repository by their identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing <c>true</c> if the user exists, otherwise <c>false</c>.</returns>
        Task<bool> ExistsAsync(int userId);
    }

}
