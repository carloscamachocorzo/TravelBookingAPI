using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Commands;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos;
using TravelBooking.Application.Services.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ICreateHotelCommandHandler _createHotelCommandHandler;

        // Inyección de dependencias para el manejador de comandos
        public HotelsController(ICreateHotelCommandHandler createHotelCommandHandler)
        {
            _createHotelCommandHandler = createHotelCommandHandler;
        }

        // Endpoint para crear un hotel
        [HttpPost("create")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            // Invoca el manejador de comandos
            var result = await _createHotelCommandHandler.Handle(command);

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
    }
}
