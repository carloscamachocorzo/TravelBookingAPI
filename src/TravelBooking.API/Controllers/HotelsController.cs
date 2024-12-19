using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelBooking.Application.Common;
using TravelBooking.Application.Constants;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    /// <summary>
    /// Controller for managing hotel-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelAppService _hotelAppService;


        /// <summary>
        /// Initializes a new instance of the <see cref="HotelsController"/> class.
        /// </summary>
        /// <param name="createHotelAppService">The service for managing hotel operations.</param>
        public HotelsController(IHotelAppService createHotelAppService)
        {
            _hotelAppService = createHotelAppService;
        }


        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="request">The details of the hotel to create.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) if the operation is successful.
        /// - 400 (Bad Request) if the operation is unsuccessful but without errors.
        /// - 500 (Internal Server Error) if an error occurred during the operation.
        /// </returns>
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto request)
        {
            // Get the role of the decoded token
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!RolePermissions.HasPermission(role, "CreateHotel"))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = "You do not have permission to create hotels." });
            }

            var result = await _hotelAppService.CreateHotel(request);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<CreateHotelResponseDto>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<CreateHotelResponseDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<CreateHotelResponseDto>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Assigns rooms to a specific hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel to which the rooms will be assigned.</param>
        /// <param name="request">The request containing the list of rooms to assign.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) if the operation is successful.
        /// - 400 (Bad Request) if the request is invalid or the operation is unsuccessful.
        /// - 500 (Internal Server Error) if an error occurred during the operation.
        /// </returns>
        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AssignRoomsToHotel(int hotelId, [FromBody] CreateRoomsRequest request)
        {
            if (request.Rooms == null || !request.Rooms.Any())
            {
                return BadRequest("The request must include at least one room.");
            }
            var result = await _hotelAppService.AssignRoomsToHotel(hotelId, request);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<CreateHotelResponseDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<CreateHotelResponseDto>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Updates the details of a specific hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel to be updated.</param>
        /// <param name="updateHotelDto">The updated details of the hotel.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) if the operation is successful.
        /// - 400 (Bad Request) if the request is invalid or the operation is unsuccessful.
        /// - 500 (Internal Server Error) if an error occurred during the operation.
        /// </returns>
        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] UpdateHotelDto updateHotelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _hotelAppService.UpdateHotelAsync(hotelId, updateHotelDto);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<bool>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<bool>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<bool>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Updates the status of a specific hotel (active or inactive).
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel to be updated.</param>
        /// <param name="request">The request containing the new status of the hotel.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) if the operation is successful.
        /// - 400 (Bad Request) if the request is invalid or the operation is unsuccessful.
        /// - 500 (Internal Server Error) if an error occurred during the operation.
        /// </returns>
        [HttpPatch("{hotelId}/status")]
        public async Task<IActionResult> UpdateHotelStatus(int hotelId, [FromBody] UpdateHotelStatusDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _hotelAppService.UpdateHotelStatusAsync(hotelId, request.Status);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<bool>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<bool>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<bool>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) with the list of hotels if the operation is successful.
        /// - 404 (Not Found) if no hotels are found or the operation is unsuccessful.
        /// - 500 (Internal Server Error) if an error occurred during the operation.
        /// </returns>
        [Authorize]
        [HttpGet("GetAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var result = await _hotelAppService.GetAllHotelsAsync();

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<List<CreateHotelResponseDto>>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return NotFound(RequestResult<List<CreateHotelResponseDto>>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<List<CreateHotelResponseDto>>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Searches for hotels based on the specified criteria.
        /// </summary>
        /// <param name="searchHotelsDto">
        /// The search criteria, including check-in date, check-out date, number of guests, and destination city.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) with a list of hotels that match the criteria if the operation is successful.
        /// - 400 (Bad Request) if the request is invalid or the operation is unsuccessful.
        /// </returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchHotels([FromQuery] SearchHotelsDto searchHotelsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _hotelAppService.SearchHotelsAsync(searchHotelsDto);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<List<SearchHotelResponseDto>>.CreateSuccessful(result.Result));
            }
            else
            {
                return BadRequest(RequestResult<List<SearchHotelResponseDto>>.CreateUnsuccessful(result.Messages));
            }
        }

    }
}
