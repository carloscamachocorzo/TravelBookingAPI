using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Application.Services;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsAppService _reservationsAppService;
        public ReservationsController(IReservationsAppService reservationsAppService)
        {
            _reservationsAppService = reservationsAppService;
        }

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
        /// Obtener el detalle de una reserva específica.
        /// </summary>
        /// <param name="reservationId">ID de la reserva</param>
        /// <returns>Detalle de la reserva</returns>
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
        /// Crear una nueva reserva.
        /// </summary>
        /// <param name="createReservationDto">Datos de la reserva</param>
        /// <returns>La reserva creada</returns>
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
