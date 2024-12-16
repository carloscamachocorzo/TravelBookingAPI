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
            return  _Context.Hotels.FirstOrDefault(h => h.HotelId == id);
        }

        public async Task UpdateAsync(Hotels hotel)
        {
            _Context.Hotels.Update(hotel);
            await _Context.SaveChangesAsync();
        }
        public async Task<List<Hotels>> SearchHotelsAsync(DateOnly? checkInDate, DateOnly? checkOutDate, int? totalGuests, string? city)
        {
            var query = _Context.Hotels.AsQueryable();

            // Filtrar por ciudad
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(h => h.City.Contains(city));
            }

            // Filtrar por fechas
            query = query.Where(h =>
                h.Rooms.Any(r => r.Reservations.Any(res =>
                    (res.CheckInDate <= checkOutDate && res.CheckOutDate >= checkInDate) // Verifica si hay reservas que se solapan
                ))
            );

            // Filtrar por capacidad de personas (puedes ajustar según cómo se maneja la capacidad en tu modelo)
            query = query.Where(h => h.Rooms.Any(r => r.Capacity >= totalGuests));

            // Ejecutar la consulta y devolver los resultados
            return await query.ToListAsync();
        }

    }
}
