﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
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
                return BadRequest(RequestResult<HotelDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<HotelDto>.CreateError(result.ErrorMessage));
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
