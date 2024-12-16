using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    public interface IReservationsRepository
    {
        Task<Reservations?> GetByIdAsync(int ReservationId);
        Task AddAsync(Reservations reservations);
        Task<List<Reservations>> GetAllAsync();
    }
}
