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
                return BadRequest(RequestResult<HotelDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<HotelDto>.CreateError(result.ErrorMessage));
        }
    }
}
