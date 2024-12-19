using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.Dtos.Users
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("Admin|TravelAgent|Guest", ErrorMessage = "Invalid Role.")]
        public string Role { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        public string Password { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
