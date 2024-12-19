using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.DataAccess.Contexts;

namespace TravelBooking.Infraestructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly TravelBookingContext _Context;
        public HotelRepository(TravelBookingContext travelBookingContext)
        {
            _Context = travelBookingContext;
        }
        public async Task AddAsync(Hotels hotel)
        {
            await _Context.Hotels.AddAsync(hotel);
            await _Context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Hotels>> GetAllAsync()
        {
            return await _Context.Hotels.ToListAsync();
        }

        public async Task<Hotels?> GetByIdAsync(int id)
        {
            return _Context.Hotels.FirstOrDefault(h => h.HotelId == id);
        }

        public async Task UpdateAsync(Hotels hotel)
        {
            _Context.Hotels.Update(hotel);
            await _Context.SaveChangesAsync();
        }
        public async Task<List<Hotels>> SearchHotelsAsync(DateOnly? checkInDate, DateOnly? checkOutDate, int? totalGuests, string? city)
        {
            try
            {
                // Start building the query
                var hotelQuery = _Context.Hotels.Include(r => r.Rooms).ThenInclude(r=> r.Reservations).AsQueryable();

                // Filter by city
                if (!string.IsNullOrEmpty(city))
                {
                    hotelQuery = hotelQuery.Where(h => h.City.Contains(city));
                }

                // Filter by availability based on check-in and check-out dates
                if (checkInDate.HasValue && checkOutDate.HasValue)
                {
                    if (checkInDate >= checkOutDate)
                        throw new ArgumentException("Check-in date must be earlier than check-out date.");
                    
                    hotelQuery = hotelQuery.Where(h =>
                        h.Rooms.Any(r =>
                            !r.Reservations.Any(res =>
                                //res.CheckInDate <= checkOutDate && res.CheckOutDate >= checkInDate // Overlap check
                                checkInDate < res.CheckOutDate && checkOutDate > res.CheckInDate
                            )
                        )
                    );
                }

                // Filter by capacity
                if (totalGuests.HasValue)
                {
                    hotelQuery = hotelQuery.Where(h => h.Rooms.Any(r => r.MaxCapacity >= totalGuests && r.Status));
                }

                // Generate SQL for debugging purposes
                var sql = hotelQuery.ToQueryString();
                Console.WriteLine($"Generated SQL: {sql}");

                // Execute the query and return the results
                return await hotelQuery.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log and rethrow exceptions for higher-level handling
                Console.WriteLine($"Error in SearchHotelsAsync: {ex.Message}");
                throw;
            }

        }
    }
}
