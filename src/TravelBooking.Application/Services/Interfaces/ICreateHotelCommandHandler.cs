using MediatR;
using TravelBooking.Application.Commands;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface ICreateHotelCommandHandler
    { 
        Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken);
    }
}
