using Microsoft.AspNetCore.Mvc;
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
        /// Creates a new user.
        /// </summary>
        /// <param name="createUserDto">The user creation data transfer object (DTO).</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
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
