using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.Dtos.Authentication
{
    public class LoginRequestDto
    {
        /// <summary>
        /// Gets or sets the username of the user attempting to log in.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public required string Password { get; set; }
    }
}
