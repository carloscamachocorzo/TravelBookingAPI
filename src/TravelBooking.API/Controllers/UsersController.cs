using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Constants;
using TravelBooking.Application.Dtos.Users;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserAppService _userAppService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userAppService">The application service for user-related operations.</param>

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="createUserDto">The user data to create.</param>
        /// <remarks>
        /// **Roles Available:**
        /// - **Admin**: Manage users, view reports, edit settings.
        /// - **Travel Agent**: Book trips, view clients.
        /// - **Traveler**: View trips.
        ///
        /// **Validation Rules:**
        /// - `FirstName` and `LastName`: Required, max 50 characters.
        /// - `Email`: Required, must be a valid email.
        /// - `PhoneNumber`: Required, must be a valid phone number.
        /// - `Role`: Required, must be one of the allowed roles (`Admin`, `TravelAgent`, `Traveler`).
        /// - `Status`: Required, must be true or false.
        /// </remarks>

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new RequestResult<object>
                {
                    IsSuccessful = false,
                    IsError = true,
                    ErrorMessage = "Validation failed.",
                    Messages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            // Validar que el rol sea válido
            if (!UserRoles.AllRoles.Contains(createUserDto.Role))
            {
                return BadRequest(new RequestResult<object>
                {
                    IsSuccessful = false,
                    IsError = true,
                    ErrorMessage = $"Invalid role. Allowed roles are: {string.Join(", ", UserRoles.AllRoles)}"
                });
            }
            var result = await _userAppService.CreateUserAsync(createUserDto);
            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result);
        }
        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to update.</param>
        /// <param name="updateUserDto">The user update data transfer object (DTO).</param>
        /// <returns>
        /// A <see cref="NoContentResult"/> if the update is successful, 
        /// or a <see cref="NotFoundResult"/> if the user does not exist.
        /// </returns>

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var result = await _userAppService.UpdateUserAsync(userId, updateUserDto);
            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result);
        }
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>
        /// An <see cref="OkObjectResult"/> containing a list of all users.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userAppService.GetAllUsersAsync();
            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result);
        }
         
    }
}
