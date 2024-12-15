using MediatR;
using TravelBooking.Application.Commands;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;


namespace TravelBooking.Application.Services
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Unit>, ICreateHotelCommandHandler
    {
        private readonly IHotelRepository _hotelRepository;
        public CreateHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            // Crear una nueva entidad de hotel
            var hotel = new Hotels
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
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

            return Unit.Value;
        }
    }
}
