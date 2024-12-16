using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Reservation;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface IReservationsAppService
    {        
        Task<RequestResult<IEnumerable<ReservationResponseDto>>> ExecuteAsync();
    }
}
