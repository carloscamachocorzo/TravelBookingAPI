using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task AddRangeAsync(IEnumerable<Rooms> rooms);
    }
}
