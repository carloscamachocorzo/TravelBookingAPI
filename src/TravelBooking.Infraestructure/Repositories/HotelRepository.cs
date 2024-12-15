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

        public Task<IEnumerable<Hotels>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hotels?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Hotels hotel)
        {
            throw new NotImplementedException();
        }
    }
}
