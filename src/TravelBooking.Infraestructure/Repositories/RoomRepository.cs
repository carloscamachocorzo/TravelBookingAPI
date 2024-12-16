using Microsoft.EntityFrameworkCore;
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
        public async Task<Rooms?> GetByIdAsync(int roomId)
        {
            return await _context.Rooms.AsNoTracking().Include(r => r.Hotel)
                .Where(r => r.RoomId == roomId).FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(Rooms room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

    }
}
