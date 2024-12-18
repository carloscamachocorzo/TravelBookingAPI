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
        /// <summary>
        /// Creates a new user in the repository.
        /// </summary>
        /// <param name="user">The user entity to be created.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains the created user entity.</returns>
        Task<Users> CreateAsync(Users user);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user entity with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Users user);

        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a list of all user entities.</returns>
        Task<List<Users>> GetAllAsync();
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the user object 
        /// if found; otherwise, <c>null</c>.
        /// </returns>
        Task<Users?> GetUserByUsernameAsync(string username);
    }

}
