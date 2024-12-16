using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;


namespace TravelBooking.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para la creación de hoteles.
    /// Implementa la lógica necesaria para registrar nuevos hoteles en el sistema.
    /// </summary>
    public class CreateHotelAppService : ICreateHotelAppService
    {
        private readonly IHotelRepository _hotelRepository;
        /// <summary>
        /// Constructor para inicializar el servicio con el repositorio de hoteles.
        /// </summary>
        /// <param name="hotelRepository">
        /// Instancia del repositorio de hoteles que se utiliza para persistir los datos.
        /// </param>
        public CreateHotelAppService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        
        public async Task<RequestResult<HotelDto>> CreateHotel(CreateHotelDto request)
        {
            try
            {
                // Crear una nueva entidad de hotel
                var hotel = new Hotels
                {
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    Status = request.Status,
                    BaseRate = request.BaseRate                    
                };

                // Guardar el hotel en el repositorio
                await _hotelRepository.AddAsync(hotel);
                // Si la creación fue exitosa
                return RequestResult<HotelDto>.CreateSuccessful(new HotelDto
                {
                    Id = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    City = hotel.City
                }, new List<string> { "Hotel creado con éxito." });
            }
            catch (Exception ex)
            {

                // Si ocurrió un error
                return RequestResult<HotelDto>.CreateError("Error al crear el hotel: " + ex.Message);
            }
        }        
    }
}
