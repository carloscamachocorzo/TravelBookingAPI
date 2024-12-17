using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Dtos.Rooms;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Infraestructure;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomAppService _roomAppService;

        public RoomsController(IRoomAppService roomAppService)
        {
            _roomAppService = roomAppService;
        }
        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] UpdateRoomRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede estar vacía.");

            if (roomId != request.RoomId)
                return BadRequest("El ID de la habitación no coincide.");
            
            var result = await _roomAppService.ExecuteUpdateRoomAsync(request);
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
        /// Habilitar/deshabilitar una habitación
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
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
        /// Obtener información de una habitación específica
        /// </summary>
        /// <param name="roomId">ID de la habitación</param>
        /// <returns>Información de la habitación</returns>
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
