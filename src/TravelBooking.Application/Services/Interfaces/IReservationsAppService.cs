using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Reservation;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface IReservationsAppService
    {
        Task<RequestResult<IEnumerable<ReservationResponseDto>>> ExecuteAsync();
        Task ExecuteNotifyReservationAsync(int reservationId);
        Task<RequestResult<ReservationDetailsDto>> GetReservationByIdAsync(int reservationId);
        Task<RequestResult<ReservationDetailsDto>> CreateReservationAsync(CreateReservationDto createReservationDto);
    }
}
