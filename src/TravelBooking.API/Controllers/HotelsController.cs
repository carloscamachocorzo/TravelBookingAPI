using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Application.Commands;
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
            await _createHotelCommandHandler.Handle(command, CancellationToken.None);

            // Retorna respuesta exitosa
            return Ok("Hotel created successfully");
        }
    }
}
