using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task AddAsync(Hotels hotel);
        Task<Hotels?> GetByIdAsync(int id);
        Task<IEnumerable<Hotels>> GetAllAsync();
        Task UpdateAsync(Hotels hotel);
        Task DeleteAsync(int id);
        Task<List<Hotels>> SearchHotelsAsync(DateOnly? checkInDate, DateOnly? checkOutDate, int? totalGuests, string? city);
    }
}
