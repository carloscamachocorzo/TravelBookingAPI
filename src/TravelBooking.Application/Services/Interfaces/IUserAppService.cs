using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Users;

namespace TravelBooking.Application.Services.Interfaces
{
    /// <summary>
    /// Defines methods for managing users.
    /// </summary>
    public interface IUserAppService
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="createUserDto">The data transfer object containing user creation details.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// with a result of <see cref="RequestResult{T}"/> containing the created user details.
        /// </returns>
        Task<RequestResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to update.</param>
        /// <param name="updateUserDto">The data transfer object containing the updated user details.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// with a result of <see cref="RequestResult{T}"/> containing the updated user details.
        /// </returns>
        Task<RequestResult<UserDto>> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// with a result of <see cref="RequestResult{T}"/> containing a collection of user details.
        /// </returns>
        Task<RequestResult<IEnumerable<UserDto>>> GetAllUsersAsync();
    }

}
