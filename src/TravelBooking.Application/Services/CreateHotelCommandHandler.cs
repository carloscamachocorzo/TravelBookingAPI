using MediatR;
using TravelBooking.Application.Commands;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;


namespace TravelBooking.Application.Services
{
    public class CreateHotelCommandHandler : ICreateHotelCommandHandler
    {
        private readonly IHotelRepository _hotelRepository;
        public CreateHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<RequestResult<HotelDto>> Handle(CreateHotelCommand request)
        {
            try
            {
                // Crear una nueva entidad de hotel
                var hotel = new Hotels
                {
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    Status = request.Status
                    //IsEnabled = request.IsEnabled,
                    //Rooms = request.Rooms.Select(r => new Room
                    //{
                    //    RoomType = r.RoomType,
                    //    BaseCost = r.BaseCost,
                    //    Taxes = r.Taxes,
                    //    Location = r.Location
                    //}).ToList()
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


        Task<Unit> ICreateHotelCommandHandler.Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
