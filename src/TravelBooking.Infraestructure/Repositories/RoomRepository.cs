using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.DataAccess.Contexts;

namespace TravelBooking.Infraestructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TravelBookingContext _context;

        public RoomRepository(TravelBookingContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Rooms> rooms)
        {
            await _context.Rooms.AddRangeAsync(rooms);
            await _context.SaveChangesAsync();
        }
    }
}
