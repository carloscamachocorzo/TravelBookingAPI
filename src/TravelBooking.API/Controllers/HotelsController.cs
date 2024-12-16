using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ICreateHotelAppService _createHotelAppService;

        /// <summary>
        /// Constructor para inyectar las dependencias
        /// </summary>
        /// <param name="createHotelAppService"></param>
        public HotelsController(ICreateHotelAppService createHotelAppService)
        {
            _createHotelAppService = createHotelAppService;
        }

        /// <summary>
        /// Creacion de Hoteles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto request)
        {

            var result = await _createHotelAppService.CreateHotel(request);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<HotelDto>.CreateSuccessful(result.Result, new List<string> { "Hotel creado con éxito." }));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<HotelDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<HotelDto>.CreateError(result.ErrorMessage));
        }

        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AssignRoomsToHotel(int hotelId, [FromBody] CreateRoomsRequest request)
        {
            if (request.Rooms == null || !request.Rooms.Any())
            {
                return BadRequest("The request must include at least one room.");
            }
            var result = await _createHotelAppService.AssignRoomsToHotel(hotelId, request);

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
