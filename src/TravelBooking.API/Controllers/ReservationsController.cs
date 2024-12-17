using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    /// <summary>
    /// Controller for handling reservation-related operations, such as creating, retrieving, and notifying reservations.
    /// </summary>
    /// <remarks>
    /// This controller exposes endpoints for managing reservations, including:
    /// - Creating a new reservation
    /// - Retrieving a reservation by its ID
    /// - Notifying about a reservation
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsAppService _reservationsAppService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// </summary>
        /// <param name="reservationsAppService">
        /// The service responsible for managing reservation-related operations.
        /// </param>
        public ReservationsController(IReservationsAppService reservationsAppService)
        {
            _reservationsAppService = reservationsAppService;
        }
        /// <summary>
        /// Retrieves a list of all reservations.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) with the list of reservations if the operation is successful.
        /// - 400 (Bad Request) if the operation is unsuccessful but no error occurred.
        /// - 500 (Internal Server Error) if an unexpected error occurred during the operation.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _reservationsAppService.ExecuteAsync();
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
        /// Retrieves the details of a reservation by its unique identifier.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation to retrieve.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) with the reservation details if the operation is successful.
        /// - 404 (Not Found) if the reservation is not found or the operation is unsuccessful.
        /// - 500 (Internal Server Error) if an unexpected error occurred during the operation.
        /// </returns>
        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationById(int reservationId)
        {
            var result = await _reservationsAppService.GetReservationByIdAsync(reservationId);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<ReservationDetailsDto>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return NotFound(RequestResult<ReservationDetailsDto>.CreateUnsuccessful(result.Messages));
            }

            return StatusCode(500, RequestResult<ReservationDetailsDto>.CreateError(result.ErrorMessage));
        }

        /// <summary>
        /// Creates a new reservation with the specified details.
        /// </summary>
        /// <param name="createReservationDto">The details of the reservation to be created.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 201 (Created) with the reservation details if the operation is successful.
        /// - 400 (Bad Request) if the request is invalid or the operation is unsuccessful.
        /// - 500 (Internal Server Error) if an unexpected error occurred during the operation.
        /// </returns>
        /// <remarks>
        /// If the reservation is successfully created, the response includes a "Location" header with the URL to retrieve the newly created reservation.
        /// </remarks> 
        [HttpPost("create")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto createReservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reservationsAppService.CreateReservationAsync(createReservationDto);

            if (result.IsSuccessful)
            {
                return CreatedAtAction(nameof(GetReservationById), new { reservationId = result.Result.ReservationId }, RequestResult<ReservationDetailsDto>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<ReservationDetailsDto>.CreateUnsuccessful(result.Messages));
            }

            return StatusCode(500, RequestResult<ReservationDetailsDto>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// Sends a notification for the specified reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation to notify.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the operation:
        /// - 200 (OK) with a success message if the notification was sent successfully.
        /// - 404 (Not Found) if the reservation was not found.
        /// - 500 (Internal Server Error) if an unexpected error occurred during the operation.
        /// </returns>
        /// <remarks>
        /// This method is used to notify the customer or relevant parties about the reservation.
        /// </remarks>
        [HttpPost("{reservationId}/notify")]
        public async Task<IActionResult> NotifyReservation(int reservationId)
        {
            try
            {
                await _reservationsAppService.ExecuteNotifyReservationAsync(reservationId);
                return Ok(new { Message = "Notificación enviada exitosamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
