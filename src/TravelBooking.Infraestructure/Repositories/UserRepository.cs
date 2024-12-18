using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.DataAccess.Contexts;

namespace TravelBooking.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TravelBookingContext _context;

        public UserRepository(TravelBookingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        public async Task<Users?> GetByIdAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        /// <summary>
        /// Verifica si un usuario existe por su ID.
        /// </summary>
        public async Task<bool> ExistsAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.UserId == userId);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        public async Task<Users> CreateAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
