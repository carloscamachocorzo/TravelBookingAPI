using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.DataAccess.Contexts;

namespace TravelBooking.Infraestructure.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly TravelBookingContext _context;

        public ReservationsRepository(TravelBookingContext travelBookingContext)
        {
            _context = travelBookingContext;
        }
        public async Task<Reservations?> GetByIdAsync(int ReservationId)
        {
            return await _context.Reservations.AsNoTracking().Include(r => r.Guests)
                .Include(r => r.EmergencyContacts).Include(r => r.User).Include(r => r.Room)
                .Where(r => r.ReservationId == ReservationId).FirstOrDefaultAsync();
        }
        public async Task<List<Reservations>> GetAllAsync()
        {
            return await _context.Reservations.AsNoTracking().Include(r => r.Guests)
                .Include(r => r.EmergencyContacts).ToListAsync();
        }
        public async Task AddAsync(Reservations reservations)
        {
            await _context.Reservations.AddAsync(reservations);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Reservations reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
