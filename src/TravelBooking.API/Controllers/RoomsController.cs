using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Rooms;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    /// <summary>
    /// Controller for handling room-related operations, such as creating, updating, and retrieving room details.
    /// </summary>
    /// <remarks>
    /// This controller exposes endpoints for managing hotel rooms, including:
    /// - Creating a new room
    /// - Updating room details
    /// - Retrieving room details by its ID
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomAppService _roomAppService;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsController"/> class.
        /// </summary>
        /// <param name="roomAppService">
        /// The service responsible for managing room-related operations.
        /// </param>
        public RoomsController(IRoomAppService roomAppService)
        {
            _roomAppService = roomAppService;
        }
        /// <summary>
        /// Updates the details of an existing room based on the specified room ID and the room update request.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to update.</param>
        /// <param name="request">The updated room details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation:
        /// - 200 (OK) if the room was successfully updated.
        /// - 400 (Bad Request) if the request is invalid, such as mismatched room ID or empty request.
        /// - 500 (Internal Server Error) if an unexpected error occurred during the operation.
        /// </returns>
        /// <remarks>
        /// This method is used to update an existing room's details such as room type, location, base cost, etc.
        /// </remarks>
        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] UpdateRoomRequest request)
        {
            if (request == null)
                return BadRequest("The request cannot be empty.");             

            var result = await _roomAppService.ExecuteUpdateRoomAsync(roomId, request);
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
        /// Updates the status of a room based on the provided room ID and the new status.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to update.</param>
        /// <param name="statusDto">The new status of the room.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation:
        /// - 200 (OK) if the room status was successfully updated.
        /// - 400 (Bad Request) if the request is invalid (e.g., invalid model state).
        /// - 500 (Internal Server Error) if an unexpected error occurred during the operation.
        /// </returns>
        /// <remarks>
        /// This method is used to update the status of a room, such as marking it as available, unavailable, etc.
        /// </remarks>
        [HttpPatch("{roomId}/status")]
        public async Task<IActionResult> UpdateRoomStatus(int roomId, [FromBody] UpdateRoomStatusDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roomAppService.UpdateRoomStatusAsync(roomId, statusDto);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<bool>.CreateSuccessful(result.Result, new List<string> { "Room status updated successfully." }));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<bool>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<bool>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Retrieves the details of a specific room based on the provided room ID.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to retrieve.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation:
        /// - 200 (OK) if the room details were successfully retrieved.
        /// - 404 (Not Found) if the room with the provided ID does not exist.
        /// - 500 (Internal Server Error) if an error occurred while retrieving the room details.
        /// </returns>
        /// <remarks>
        /// This method is used to fetch detailed information about a room, such as its type, location, cost, etc.
        /// </remarks>
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            // Llamar al servicio de la aplicación para obtener la información de la habitación
            var result = await _roomAppService.GetRoomDetailsAsync(roomId);

            if (result.IsSuccessful)
            {
                return Ok(result.Result);
            }
            else if (!result.IsError)
            {
                return NotFound(new { Message = "Room not found" });
            }
            return StatusCode(500, new { Message = "Error retrieving room details", Error = result.ErrorMessage });
        }
    }
}
