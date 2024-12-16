using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
    }
}
