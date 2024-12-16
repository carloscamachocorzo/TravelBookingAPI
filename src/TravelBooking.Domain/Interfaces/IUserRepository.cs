using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="userId">El ID del usuario.</param>
        /// <returns>La entidad de usuario, o null si no existe.</returns>
        Task<Users?> GetByIdAsync(int userId);

        /// <summary>
        /// Verifica si un usuario existe por su ID.
        /// </summary>
        /// <param name="userId">El ID del usuario.</param>
        /// <returns>True si existe, false en caso contrario.</returns>
        Task<bool> ExistsAsync(int userId);
    }
}
