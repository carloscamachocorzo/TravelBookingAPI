using MediatR;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;

namespace TravelBooking.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para la creación de hoteles en la aplicación.
    /// Proporciona un contrato para manejar la lógica relacionada con la creación de registros de hoteles.
    /// </summary>
    public interface IHotelAppService
    {
        /// <summary>
        /// Crea un nuevo hotel en el sistema.
        /// </summary>
        /// <param name="request">
        /// Un objeto <see cref="CreateHotelDto"/> que contiene los detalles necesarios para crear un hotel.
        /// </param>
        /// <returns>
        /// Una tarea que representa el resultado de la operación. El resultado incluye un objeto <see cref="RequestResult{T}"/> con información sobre el hotel creado (<see cref="HotelDto"/>).
        /// </returns>
        Task<RequestResult<HotelDto>> CreateHotel(CreateHotelDto request);
        Task<RequestResult<bool>> AssignRoomsToHotel(int hotelId, CreateRoomsRequest request);
        Task<RequestResult<bool>> UpdateHotelAsync(int hotelId, UpdateHotelDto updateHotelDto);
        Task<RequestResult<bool>> UpdateHotelStatusAsync(int hotelId, bool status);
        Task<RequestResult<List<HotelDto>>> GetAllHotelsAsync(); 
    }
}
