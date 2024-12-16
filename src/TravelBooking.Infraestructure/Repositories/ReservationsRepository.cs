using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.DataAccess.Contexts;

namespace TravelBooking.Infraestructure.Repositories
{
    public class ReservationsRepository: IReservationsRepository
    {
        private readonly TravelBookingContext _context;

        public ReservationsRepository(TravelBookingContext travelBookingContext)
        {
            _context=travelBookingContext;
        }
        public async Task<Reservations?> GetByIdAsync(int ReservationId)
        {
            return await _context.Reservations.FindAsync(ReservationId);
        }
        public async Task<List<Reservations>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }
        public async Task AddAsync(Reservations reservations)
        {
            await _context.Reservations.AddAsync(reservations);
            await _context.SaveChangesAsync();
        }
    }
}
