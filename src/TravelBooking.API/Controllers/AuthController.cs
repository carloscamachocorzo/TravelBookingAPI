using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Dtos.Authentication;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authService;
        private readonly IUserAppService _userAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The service responsible for handling authentication-related operations.</param>
        /// <param name="userAppService">The service responsible for managing user-related operations.</param>

        public AuthController(IAuthAppService authService, IUserAppService userAppService)
        {
            _authService = authService;
            _userAppService = userAppService;
        }
        /// <summary>
        /// Authenticates a user and generates a JWT token upon successful login.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>
        /// Returns an HTTP 200 response with the generated token if authentication is successful.
        /// Returns HTTP 401 Unauthorized if the credentials are invalid.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Validate request model
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request data." });
            }
            string token = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(new { Token = token, TokenType = "Bearer" });
        }
    }
}
