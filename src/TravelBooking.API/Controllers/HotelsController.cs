using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelAppService _hotelAppService;

        /// <summary>
        /// Constructor para inyectar las dependencias
        /// </summary>
        /// <param name="createHotelAppService"></param>
        public HotelsController(IHotelAppService createHotelAppService)
        {
            _hotelAppService = createHotelAppService;
        }

        /// <summary>
        /// Creacion de Hoteles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto request)
        {

            var result = await _hotelAppService.CreateHotel(request);

            if (result.IsSuccessful)
            {
                return Ok(RequestResult<HotelDto>.CreateSuccessful(result.Result));
            }
            else if (!result.IsError)
            {
                return BadRequest(RequestResult<HotelDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<HotelDto>.CreateError(result.ErrorMessage));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
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
                return BadRequest(RequestResult<HotelDto>.CreateUnsuccessful(result.Messages));
            }
            return StatusCode(500, RequestResult<HotelDto>.CreateError(result.ErrorMessage));
        }

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

    }
}
