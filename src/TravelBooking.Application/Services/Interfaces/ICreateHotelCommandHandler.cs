using MediatR;
using TravelBooking.Application.Commands;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface ICreateHotelCommandHandler
    { 
        Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken);
        Task<RequestResult<HotelDto>> Handle(CreateHotelCommand command);
    }
}
