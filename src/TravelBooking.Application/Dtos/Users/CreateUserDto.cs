using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.Dtos.Users
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Status { get; set; }
    }
}
