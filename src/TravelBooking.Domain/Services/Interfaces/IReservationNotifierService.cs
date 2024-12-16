using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Services.Interfaces
{
    public interface IReservationNotifierService
    {
        Task NotifyReservationAsync(Reservations reservation);
    }
}
